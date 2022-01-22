using System.Text;

namespace FunicularSwitch.Generators.Generation;

public class CSharpBuilder
{
    readonly Stack<string> m_Indents = new();

    readonly StringBuilder m_Content = new();

    public string CurrentIndent => string.Join("", m_Indents);

    public static implicit operator string(CSharpBuilder transformation) => transformation.ToString();

    public void Write(string format, params object[] args)
    {
        foreach (var indent in m_Indents)
        {
            m_Content.Append(indent);
        }

        if (args.Any())
            m_Content.AppendFormat(format, args);
        else
            m_Content.Append(format);
    }

    public void WriteLine(string format, params object[] args)
    {
        Write(format, args);
        m_Content.AppendLine();
    }

    public void WriteLine(string format)
    {
        Write(format);
        m_Content.AppendLine();
    }

    public void PushIndent(string indent) => m_Indents.Push(indent);

    public string PopIndent() => m_Indents.Count > 0 ? m_Indents.Pop() : string.Empty;

    public override string ToString() => m_Content.ToString();
}

public class Class : Scope
{
    public string Name { get; }

    public Class(CSharpBuilder tt, string name, string? modifiers, params string[] baseClassNames)
        : base(tt, $"{(modifiers != null ? modifiers + " " : "")}class {name}{(baseClassNames.Any() ? " : " + string.Join(", ", baseClassNames) : "")}") =>
        Name = name;
}

public class PublicStaticClass : Class
{
    public PublicStaticClass(CSharpBuilder tt, string name) : base(tt, name, "public static")
    {
    }
}