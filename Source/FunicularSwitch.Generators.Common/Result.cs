using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace FunicularSwitch.Generators.Common;

public static class GenerationResult

{
    public static GenerationResult<T> Create<T>(T? value, EquatableArray<DiagnosticInfo> diagnostics, bool hasValue)
        => new(value, diagnostics, hasValue);
}
    
public readonly record struct GenerationResult<T>(T? Value, EquatableArray<DiagnosticInfo> Diagnostics, bool HasValue)
{
    public static readonly GenerationResult<T> Empty = new(default, ImmutableArray<DiagnosticInfo>.Empty, false); 
    
    public GenerationResult<T> AddDiagnostics(DiagnosticInfo diagnosticInfo) => 
        this with { Diagnostics = Diagnostics.AsImmutableArray().Add(diagnosticInfo) };
    
    public GenerationResult<T> SetValue(T value) => 
        this with { Value = value, HasValue = true };

    public static implicit operator GenerationResult<T>(DiagnosticInfo diagnostic) => Empty.AddDiagnostics(diagnostic);
    
    public static implicit operator GenerationResult<T>(EquatableArray<DiagnosticInfo> diagnostics) => new(default, diagnostics, false);
    
    public static implicit operator GenerationResult<T>(T value) => Empty.SetValue(value);

    public GenerationResult<TResult> Bind<TResult>(Func<T, GenerationResult<TResult>> bind)
    {
        if (!HasValue)
            return Diagnostics;
        
        var newValue = bind(Value!);
        return newValue with { Diagnostics = Diagnostics.AsImmutableArray().AddRange(newValue.Diagnostics) };
    }
    
    public GenerationResult<TResult> Map<TResult>(Func<T, TResult> map)
    {
        var newValue = !HasValue
            ? default
            : map(Value!);
        return new(newValue, Diagnostics, HasValue);
    }
}

public sealed record DiagnosticInfo
{
    // Explicit constructor to convert Location into LocationInfo
    public DiagnosticInfo(DiagnosticDescriptor descriptor, Location? location)
    {
        Descriptor = descriptor;
        Location = location is not null ? LocationInfo.CreateFrom(location) : null;
    }

    public DiagnosticInfo(Diagnostic diagnostic)
    {
        Descriptor = diagnostic.Descriptor;
        Location = LocationInfo.CreateFrom(diagnostic.Location);
    }

    public static implicit operator DiagnosticInfo(Diagnostic diagnostic) => new(diagnostic);

    public DiagnosticDescriptor Descriptor { get; }
    public LocationInfo? Location { get; }
}

public record LocationInfo(string FilePath, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    public Location ToLocation()
        => Location.Create(FilePath, TextSpan, LineSpan);

    public static LocationInfo? CreateFrom(SyntaxNode node)
        => CreateFrom(node.GetLocation());

    public static LocationInfo? CreateFrom(Location location) =>
        location.SourceTree is null
            ? null
            : new(location.SourceTree.FilePath, location.SourceSpan, location.GetLineSpan().Span);
}