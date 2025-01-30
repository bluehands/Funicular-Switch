using System.Text;

namespace FunicularSwitch.Generators.Generation;

public class CSharpBuilder
{
    readonly Stack<string> m_Indents = new();

    public StringBuilder Content { get; } = new();

    public string CurrentIndent => string.Join("", m_Indents);

    public static implicit operator string(CSharpBuilder transformation) => transformation.ToString();

    public void Write(string format, params object[] args)
    {
        foreach (var indent in m_Indents)
        {
            Content.Append(indent);
        }

        if (args.Any())
            Content.AppendFormat(format, args);
        else
            Content.Append(format);
    }

    public void WriteLine(string format, params object[] args)
    {
        Write(format, args);
        Content.AppendLine();
    }

    public void WriteLine(string format)
    {
        Write(format);
        Content.AppendLine();
    }

    public void PushIndent(string indent) => m_Indents.Push(indent);

    public string PopIndent() => m_Indents.Count > 0 ? m_Indents.Pop() : string.Empty;

    public override string ToString() => Content.ToString();

    public void Append(string str)
    {
        Content.Append(str);
    }
}

public class Class(CSharpBuilder tt, string name, string? modifiers, params string[] baseClassNames)
    : Scope(tt,
        $"{(modifiers != null ? modifiers + " " : "")}class {name}{(baseClassNames.Any() ? " : " + string.Join(", ", baseClassNames) : "")}")
{
    public string Name { get; } = name;
}

public class PublicStaticClass(CSharpBuilder tt, string name) : Class(tt, name, "public static");