using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;

namespace FunicularSwitch.Generators.Generation;

public class Indent : IDisposable
{
    protected readonly CSharpBuilder Builder;
    readonly string? m_End;
    readonly string? m_Start;

    public Indent(CSharpBuilder tt, string? start = null, string? end = null)
    {
        Builder = tt;
        m_End = end;
        m_Start = start;
        if (start != null) Builder.WriteLine(start);
        Builder.PushIndent("\t");
    }

    public virtual void Dispose()
    {
        Builder.PopIndent();
        if (m_End != null) Builder.WriteLine(m_End);
    }

    public override string ToString()
    {
        var printTransformation = new CSharpBuilder();
        using (new Indent(printTransformation, m_Start, m_End))
        {
        }
        return printTransformation.ToString();
    }
}

public class Parameter
{
    public string Type { get; }
    public string Name { get; }
    public string? DefaultValue { get; }

    public Parameter(string type, string name)
    {
        Type = type;
        Name = name;
    }

    public Parameter(string type, string name, string defaultValue)
        : this(type, name) =>
        DefaultValue = defaultValue;

    public override string ToString() => Type + " " + Name + (DefaultValue == null ? "" : " = " + DefaultValue);
}

public class Interface : Scope
{
    public string Name { get; }

    public Interface(CSharpBuilder tt, string name, string? modifiers, params string[] baseInterfaceNames)
        : base(tt,
            $"{(modifiers != null ? modifiers + " " : "")}interface {name}{(baseInterfaceNames.Any() ? " : " + string.Join(", ", baseInterfaceNames) : "")}") =>
        Name = name;
}

public class ObjectInitScope : Scope
{
    public ObjectInitScope(CSharpBuilder textTransformation)
        : base(textTransformation, null, ";")
    {
    }
}

public class ScopeWithComma : Scope
{
    public ScopeWithComma(CSharpBuilder textTransformation)
        : base(textTransformation, null, ",")
    {
    }
}

public class Namespace : Scope
{
    public Namespace(CSharpBuilder tt, string name)
        : base(tt, $"namespace {name}")
    {
    }
}

public static class BuildExtensions
{
    public static Class StaticPartialClass(this CSharpBuilder builder, string name, string accessModifier) => new(builder, name, $"{accessModifier} static partial");

    public static Scope Scope(this CSharpBuilder tt) => new(tt);

    public static Class Class(this CSharpBuilder tt, string modifiers, string name, params string[] baseClassNames) => new(tt, name, modifiers, baseClassNames);

    public static Interface Interface(this CSharpBuilder tt, string modifiers, string name, params string[] baseInterfaceNames) => new(tt, name, modifiers, baseInterfaceNames);

    public static Scope Method(this CSharpBuilder tt, string returnType, string methodName, IEnumerable<Parameter> parameters, string? modifiers = null)
    {
        tt.WriteMethodSignature(returnType, methodName, parameters, modifiers);
        return tt.Scope();
    }

    public static void CallMethod(this CSharpBuilder tt, string left, string methodName, params object[] parameters)
    {
        tt.WriteLine(left.CallMethod(methodName, parameters));
    }

    public static string CallMethod(this string left, string methodName, params object[] parameters) => $"{(!string.IsNullOrEmpty(left) ? left + "." : string.Empty)}{methodName}({String.Join(", ", parameters)});";

    public static Scope If(this CSharpBuilder tt, string condition)
    {
        tt.WriteLine("if ({0})", condition);
        return tt.Scope();
    }

    public static Scope Else(this CSharpBuilder tt)
    {
        tt.WriteLine("else");
        return tt.Scope();
    }

    public static Scope ElseIf(this CSharpBuilder tt, string condition)
    {
        tt.WriteLine("else if ({0})", condition);
        return tt.Scope();
    }

    public static Scope IfIsNull(this CSharpBuilder tt, string variable) => tt.If($"{variable} == null");

    public static Scope IfIsNotNull(this CSharpBuilder tt, string variable) => tt.If($"{variable} != null");

    public static Scope ScopeWithComma(this CSharpBuilder tt) => new ScopeWithComma(tt);

    public static Scope ScopeWithSemicolon(this CSharpBuilder tt) => new ObjectInitScope(tt);

    public static Namespace Namespace(this CSharpBuilder tt, string name) => new Namespace(tt, name);

    public static Indent Indent(this CSharpBuilder tt, string? start = null, string? end = null) => new(tt, start, end);

    public static string Assign(this CSharpBuilder tt, string variableName, string value, string terminator = ";")
    {
        tt.WriteLine("{0} = {1}{2}", variableName, value, terminator);
        return variableName.Split(' ').Last();
    }

    public static void WriteInterfaceMethod(this CSharpBuilder tt, string returnType, string methodName, IEnumerable<string> parametersTypes, IEnumerable<string> parameterNames)
    {
        tt.WriteLine("{0} {1}({2});", returnType, methodName, String.Join(", ", parametersTypes.Zip(parameterNames, (t, n) =>
            $"{t} {n}")));
    }
    public static void WriteMethodSignature(this CSharpBuilder tt, string returnType, string methodName, IEnumerable<string> parametersTypes, IEnumerable<string> parameterNames)
    {
        if (!returnType.StartsWith("private ") && !returnType.StartsWith("protected "))
            tt.Write("public ");
        tt.WriteLine("{0} {1}({2})", returnType, methodName, String.Join(", ", parametersTypes.Zip(parameterNames, (t, n) =>
            $"{t} {n}")));
    }
    public static void WriteMethodSignature(this CSharpBuilder tt, string returnType, string methodName)
    {
        WriteMethodSignature(tt, returnType, methodName, new string[] { }, new string[] { });
    }
    public static void WriteMethodSignature(this CSharpBuilder tt, string returnType, string methodName, IEnumerable<Parameter> parameters, string? modifiers = null, bool lambda = false)
    {
        tt.WriteLine("{0}{1} {2}({3}){4}", modifiers != null ? modifiers + " " : "", returnType, methodName, String.Join(", ", parameters), lambda ? " =>" : "");
    }
    public static void WriteUsings(this CSharpBuilder tt, params string[] usings)
    {
        foreach (var s in usings)
        {
            tt.WriteLine("using {0};", s);
        }
        tt.NewLine();
    }
    public static void WriteAttribute(this CSharpBuilder tt, string attributeName)
    {
        tt.WriteLine("[{0}]", attributeName);
    }

    public static void WriteAutoProperty(this CSharpBuilder tt, string returnType, string propertyName)
    {
        tt.WriteLine("public {0} {1} {{ get; set; }}", returnType, propertyName);
    }

    public static void WriteGetOnlyProperty(this CSharpBuilder tt, string returnType, string propertyName)
    {
        tt.WriteLine("public {0} {1} {{ get; }}", returnType, propertyName);
    }

    public static void InitializeListProperty(this CSharpBuilder tt, string type, string name, IEnumerable<string> values, bool withComma = true, string? emptyCondition = null)
    {
        var emptyProcessing = emptyCondition == null ? "" : $"{emptyCondition} ? new {type}() : ";
        tt.Write("{0} = {2}new {1}", name, type, emptyProcessing);
        values = values.ToList();
        if (!values.Any())
        {
            tt.WriteLine("(){0}", withComma ? "," : "");
            return;
        }
        tt.NewLine();
        using (withComma ? tt.ScopeWithComma() : tt.Scope())
        {
            var first = true;
            foreach (var value in values)
            {
                if (!first) tt.WriteLine(","); else first = false;
                tt.Write(value);
            }
            tt.NewLine();
        }
    }

    public static void NewLine(this CSharpBuilder tt)
    {
        tt.WriteLine(String.Empty);
    }

    public static void Const(this CSharpBuilder tt, string typeName, string name, object value)
    {
        tt.WriteLine("public const {0} {1} = {2};", typeName, name, value);
    }

    public static string ToParameterName(this string name)
    {
	    var typeNameWithoutOuter = name.Split('.').Last();
        var parameterName = typeNameWithoutOuter.FirstToLower();
        return PrefixAtIfKeyword(parameterName);
    }

    public static string PrefixAtIfKeyword(this string parameterName) => parameterName.IsAnyKeyWord() ? $"@{parameterName}" : parameterName;

    public static string TrimBaseTypeName(this string value, string baseTypeName)
    {
        if (value.Length <= baseTypeName.Length)
            return value;

        if (value.EndsWith(baseTypeName))
            value = value.Substring(0, value.Length - baseTypeName.Length);
        else if (value.StartsWith(baseTypeName))
            value = value.Substring(baseTypeName.Length);
        return value;
    }

    public static string ToMatchExtensionFilename(this string fullTypeName, EquatableArray<string> typeParameters) => $"{fullTypeName.Replace(".", "")}{RoslynExtensions.FormatTypeParameterForFileName(typeParameters)}MatchExtension.g.cs";

    
}