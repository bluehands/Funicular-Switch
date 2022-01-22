namespace FunicularSwitch.Generators.Generation;

public class Scope : Indent
{
    public Scope(CSharpBuilder tt, string? preamble = null, string? postamble = null)
        : base(tt, (preamble != null ? preamble + Environment.NewLine + tt.CurrentIndent : "") + "{", "}" + postamble) { }
}