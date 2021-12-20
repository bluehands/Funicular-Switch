using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypeInfo = Microsoft.CodeAnalysis.TypeInfo;

namespace FunicularSwitch.Generators;

[Generator]
public class ResultTypeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var resultTypeClasses =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)

                )
                .Where(static target => target != null)
                .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(resultTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses, 
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, SourceProductionContext context)
    {
        if (resultTypeClasses.IsDefaultOrEmpty) return;

        var resultTypeSchemata = Parser.GetResultTypes(compilation, resultTypeClasses, context.ReportDiagnostic, context.CancellationToken).ToImmutableArray();

        //throw new Exception(string.Join(",", resultTypeSchemata));

        var source = new StringBuilder();
        foreach (var resultTypeClass in resultTypeSchemata)
        {
            source.Append(Generator.Emit(resultTypeClass, context.ReportDiagnostic, context.CancellationToken));
            //context.ReportDiagnostic(DebugError(message));
        }

           
        context.AddSource("ResultTypes.g.cs", $@"// Hello from funicular generator!!! 
{source}");
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
        syntaxNode is ClassDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };

    const string ResultTypeAttribute = "FunicularSwitch.Test.ResultTypeAttribute";

    static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                    continue;

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                var attributeFullName = attributeContainingTypeSymbol.ToDisplayString();

                if (attributeFullName == ResultTypeAttribute)
                {
                    return classDeclarationSyntax;
                }
            }
        }

        return null;
    }
}

static class Generator
{
    const string Template = @"using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Template
{
    public abstract partial class MyResult
    {
        public static MyResult<T> Error<T>(MyError details) => new Error<T>(details);
        public static MyResult<T> Ok<T>(T value) => new Ok<T>(value);
        public bool IsError => GetType().GetGenericTypeDefinition() == typeof(Error<>);
        public bool IsOk => !IsError;
        public abstract MyError? GetErrorOrDefault();

        public static MyResult<T> Try<T>(Func<T> action, Func<Exception, MyError> formatError)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                return Error<T>(formatError(e));
            }
        }

        public static async Task<MyResult<T>> Try<T>(Func<Task<T>> action, Func<Exception, MyError> formatError)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                return Error<T>(formatError(e));
            }
        }
    }

    public abstract partial class MyResult<T> : MyResult, IEnumerable<T>
    {
        public static MyResult<T> Error(MyError message) => Error<T>(message);
        public static MyResult<T> Ok(T value) => Ok<T>(value);

        public static implicit operator MyResult<T>(T value) => MyResult.Ok(value);

        public static bool operator true(MyResult<T> result) => result.IsOk;
        public static bool operator false(MyResult<T> result) => result.IsError;

        public static bool operator !(MyResult<T> result) => result.IsError;

        public void Match(Action<T> ok, Action<MyError>? error = null) => Match(
            v =>
            {
                ok.Invoke(v);
                return 42;
            },
            err =>
            {
                error?.Invoke(err);
                return 42;
            });
        public T1 Match<T1>(Func<T, T1> ok, Func<MyError, T1> error)
        {
            return this switch
            {
                Ok<T> okMyResult => ok(okMyResult.Value),
                Error<T> errorMyResult => error(errorMyResult.Details),
                _ => throw new InvalidOperationException($""Unexpected derived result type: {GetType()}"")
            };
        }
        public async Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<MyError, Task<T1>> error)
        {
            return this switch
            {
                Ok<T> okMyResult => await ok(okMyResult.Value).ConfigureAwait(false),
                Error<T> errorMyResult => await error(errorMyResult.Details).ConfigureAwait(false),
                _ => throw new InvalidOperationException($""Unexpected derived result type: {GetType()}"")
            };
        }
        public Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<MyError, T1> error) => Match(ok, e => Task.FromResult(error(e)));
        public async Task Match(Func<T, Task> ok)
        {
            switch (this)
            {
                case Ok<T> okMyResult:
                    await ok(okMyResult.Value).ConfigureAwait(false);
                    break;
                case Error<T>:
                    break;
                default:
                    throw new InvalidOperationException($""Unexpected derived result type: {GetType()}"");
            }
        }
        public T Match(Func<MyError, T> error) => Match(v => v, error);

        public MyResult<T1> Bind<T1>(Func<T, MyResult<T1>> bind)
        {
            switch (this)
            {
                case Ok<T> ok:
                    return bind(ok.Value);
                case Error<T> error:
                    return error.Convert<T1>();
                default:
                    throw new InvalidOperationException($""Unexpected derived result type: {GetType()}"");
            }
        }
        public async Task<MyResult<T1>> Bind<T1>(Func<T, Task<MyResult<T1>>> bind)
        {
            switch (this)
            {
                case Ok<T> ok:
                    return await bind(ok.Value).ConfigureAwait(false);
                case Error<T> error:
                    return error.Convert<T1>();
                default:
                    throw new InvalidOperationException($""Unexpected derived result type: {GetType()}"");
            }
        }

        public MyResult<T1> Map<T1>(Func<T, T1> map)
            => Bind(value => Ok(map(value)));

        public Task<MyResult<T1>> Map<T1>(Func<T, Task<T1>> map)
            => Bind(async value => Ok(await map(value).ConfigureAwait(false)));

        public T? GetValueOrDefault(Func<T>? defaultValue = null)
            => Match(
                v => v,
                _ => defaultValue != null ? defaultValue() : default);

        public T GetValueOrThrow()
            => Match(
                v => v,
                details => throw new InvalidOperationException($""Cannot access error result value. Error: {details}""));

        public IEnumerator<T> GetEnumerator() => Match(ok => new[] { ok }, _ => Enumerable.Empty<T>()).GetEnumerator();

        public override string ToString() => Match(ok => $""Ok {ok?.ToString()}"", error => $""Error {error}"");
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public sealed partial class Ok<T> : MyResult<T>
    {
        public T Value { get; }

        public Ok(T value) => Value = value;

        public override MyError? GetErrorOrDefault() => null;

        public bool Equals(Ok<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Ok<T> other && Equals(other);
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(Ok<T> left, Ok<T> right) => Equals(left, right);

        public static bool operator !=(Ok<T> left, Ok<T> right) => !Equals(left, right);
    }

    public sealed partial class Error<T> : MyResult<T>
    {
        public MyError Details { get; }

        public Error(MyError details) => Details = details;

        public Error<T1> Convert<T1>() => new(Details);

        public override MyError GetErrorOrDefault() => Details;

        public bool Equals(Error<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Details, other.Details);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Error<T> other && Equals(other);
        }

        public override int GetHashCode() => Details.GetHashCode();

        public static bool operator ==(Error<T> left, Error<T> right) => Equals(left, right);

        public static bool operator !=(Error<T> left, Error<T> right) => !Equals(left, right);
    }

    public static partial class MyResultExtension
    {
        #region bind

        public static async Task<MyResult<T1>> Bind<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, MyResult<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        public static async Task<MyResult<T1>> Bind<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<MyResult<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        public static async Task<MyResult<T1>> Map<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        public static Task<MyResult<T1>> Map<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<T1>> bind)
            => Bind(result, async v => MyResult.Ok(await bind(v).ConfigureAwait(false)));

        public static MyResult<T> MapError<T>(this MyResult<T> result, Func<MyError, MyError> mapError) =>
            result.Match(ok => ok, error => MyResult.Error<T>(mapError(error)));

        #endregion

        #region match
        public static async Task<T1> Match<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<T1>> ok,
            Func<MyError, Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<T1>> ok,
            Func<MyError, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, T1> ok,
            Func<MyError, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);
        #endregion

        #region choose

        public static IEnumerable<T1> Choose<T, T1>(
            this IEnumerable<T> items,
            Func<T, MyResult<T1>> choose,
            Action<MyError> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static IEnumerable<T> Choose<T>(
            this IEnumerable<MyResult<T>> results,
            Action<MyError> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        #endregion

        public static MyResult<T> Flatten<T>(this MyResult<MyResult<T>> result) => result.Bind(r => r);

        public static MyResult<T1> As<T, T1>(this MyResult<T> result, Func<MyError> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return MyResult.Error<T1>(errorTIsNotT1());
            });

        public static MyResult<T1> As<T1>(this MyResult<object> result, Func<MyError> errorIsNotT1) => result.As<object, T1>(errorIsNotT1);

        public static MyResult<T> As<T>(this object item, Func<MyError> error) =>
            !(item is T t) ? MyResult.Error<T>(error()) : t;

        public static MyResult<T> NotNull<T>(this T? item, Func<MyError> error) =>
            item ?? MyResult.Error<T>(error());

        public static MyResult<string> NotNullOrEmpty(this string s, Func<MyError> error)
            => string.IsNullOrEmpty(s) ? MyResult.Error<string>(error()) : s;

        public static MyResult<string> NotNullOrWhiteSpace(this string s, Func<MyError> error)
            => string.IsNullOrWhiteSpace(s) ? MyResult.Error<string>(error()) : s;

        public static MyResult<T> First<T>(this IEnumerable<T> candidates, Func<T, bool> predicate, Func<MyError> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
}
";

    public static string Emit(ResultTypeSchema resultTypeSchema, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var @namespace = resultTypeSchema.ResultType.GetContainingNamespace();

        return Template
            .Replace("namespace FunicularSwitch.Generators.Template", $"namespace {@namespace}")
            .Replace("MyResult", resultTypeSchema.ResultType.Identifier.ToFullString())
            .Replace("MyError", resultTypeSchema.ErrorType.Name);
    }
}

static class Diagnostics
{
    const string Category = nameof(ResultTypeGenerator);

    public static Diagnostic DebugError(string message) => Diagnostic.Create(new DiagnosticDescriptor("FUN9999", "Hello", message, "Source Generator", DiagnosticSeverity.Error, true), Location.None);

    public static Diagnostic InvalidAttributeUsage(string message, Location location) =>
        Diagnostic.Create(
            new (
                id: "FUN0001",
                title: "Invalid attribute usage",
                messageFormat: $"{message} -  Please use ResultType attribute with typeof expression like [ResultType(typeof(MyError))]",
                category: Category,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true
            ),
            location
        );
}

static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {

        if (resultTypeClasses.Length == 0)
            throw new Exception("No result types");

        foreach (var resultTypeClass in resultTypeClasses)
        {
            var semanticModel = compilation.GetSemanticModel(resultTypeClass.SyntaxTree);

            //var resultTypeInfo = semanticModel.GetTypeInfo(resultTypeClass);

            //var symbol = semanticModel.GetSymbolInfo(resultTypeClass, cancellationToken).Symbol;
            //if (symbol is not INamedTypeSymbol
            //    resultTypeSymbol)
            //{
            //    throw new Exception($"Unexpteced symbol: {symbol}");
            //    continue;
            //}
            
            var attribute = resultTypeClass.AttributeLists
                .Select(l => l.Attributes.First(a => a.Name.ToFullString() == "ResultType"))
                .First();

            var errorType = TryGetErrorType(attribute, reportDiagnostic);

            if (errorType == null)
            {
                throw new Exception("Error type not found");
                continue;
            }

            if (semanticModel.GetSymbolInfo(errorType, cancellationToken)
                    .Symbol is not INamedTypeSymbol errorTypeSymbol)
            {
                throw new Exception("Error type not unexptected");
                continue;
            }

            var mergeMethod = errorTypeSymbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .FirstOrDefault(m =>
                    m.Name == "Merge" &&
                    m.Parameters.Length == 2 &&
                    m.Parameters.All(p => p.Type.Name == errorTypeSymbol.Name) &&
                    m.ReturnType.Name == errorTypeSymbol.Name
                );

            yield return new(resultTypeClass, errorTypeSymbol, mergeMethod);
        }
    }

    static TypeSyntax? TryGetErrorType(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
    {
        var expressionSyntax = attribute.ArgumentList!.Arguments[0].Expression;
        if (expressionSyntax is not TypeOfExpressionSyntax es)
        {
            reportDiagnostics(Diagnostics.InvalidAttributeUsage("Expected typeof expression for error type parameter", attribute.GetLocation()));
            return null;
        }

        return es.Type;
    }
}

class ResultTypeSchema
{
    public ClassDeclarationSyntax ResultType { get; }
    public INamedTypeSymbol ErrorType { get; }
    public IMethodSymbol? MergeMethod { get; }

    public ResultTypeSchema(ClassDeclarationSyntax resultType, INamedTypeSymbol errorType, IMethodSymbol? mergeMethod)
    {
        ResultType = resultType;
        ErrorType = errorType;
        MergeMethod = mergeMethod;
    }

    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}, {nameof(MergeMethod)}: {MergeMethod}";
}

static class Helpers
{
    public static string GetContainingNamespace(this SyntaxNode node)
    {
        var current = node;
        do
        {
            if (current is NamespaceDeclarationSyntax n)
                return n.Name.ToFullString();
            if (current is FileScopedNamespaceDeclarationSyntax f)
                return f.Name.ToFullString();
            current = current.Parent;
        } while (current != null);

        throw new InvalidOperationException($"No containing namespace found for node {node}");
    }
}