using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
}

public class LightTextNode : LightNode
{
    public string Text { get; set; }

    public LightTextNode(string text)
    {
        Text = text;
    }

    public override string OuterHTML => Text;
    public override string InnerHTML => Text;
}

public enum DisplayType
{
    Block,
    Inline
}

public enum ClosingType
{
    SelfClosing,
    WithClosingTag
}

public class LightElementNode : LightNode
{
    public string TagName { get; set; }
    public DisplayType Display { get; set; }
    public ClosingType Closing { get; set; }
    public List<string> CssClasses { get; set; }
    public List<LightNode> Children { get; set; }

    public LightElementNode(string tagName, DisplayType display, ClosingType closing)
    {
        TagName = tagName;
        Display = display;
        Closing = closing;
        CssClasses = new List<string>();
        Children = new List<LightNode>();
    }

    public override string OuterHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<{TagName}");

            if (CssClasses.Count > 0)
            {
                sb.Append($" class=\"{string.Join(" ", CssClasses)}\"");
            }

            if (Closing == ClosingType.SelfClosing)
            {
                sb.Append(" />");
            }
            else
            {
                sb.Append(">");
                sb.Append(InnerHTML);
                sb.Append($"</{TagName}>");
            }

            return sb.ToString();
        }
    }

    public override string InnerHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();

            foreach (var child in Children)
            {
                sb.Append(child.OuterHTML);
            }

            return sb.ToString();
        }
    }

    public void AddChild(LightNode child)
    {
        Children.Add(child);
    }
}

public class LightElementFactory
{
    private readonly Dictionary<string, LightElementNode> _elementCache = new Dictionary<string, LightElementNode>();

    public LightElementNode GetLightElementNode(string tagName, DisplayType display, ClosingType closing)
    {
        string key = $"{tagName}_{display}_{closing}";
        if (!_elementCache.ContainsKey(key))
        {
            _elementCache[key] = new LightElementNode(tagName, display, closing);
        }
        return _elementCache[key];
    }
}

public class Program
{
    public static void Main()
    {
        // Example book text
        string[] bookLines = new string[]
        {
            "The Great Gatsby",
            "Chapter 1",
            "In my younger and more vulnerable years my father gave me some advice that I've been turning over in my mind ever since.",
            " ",
            "Whenever you feel like criticizing any one, he told me, just remember that all the people in this world haven't had the advantages that you've had."
        };

        // Factory for creating LightElementNode
        LightElementFactory factory = new LightElementFactory();

        // Root element
        LightElementNode root = factory.GetLightElementNode("div", DisplayType.Block, ClosingType.WithClosingTag);
        
        // Process book lines
        bool firstLine = true;
        foreach (var line in bookLines)
        {
            LightElementNode element;
            if (firstLine)
            {
                element = factory.GetLightElementNode("h1", DisplayType.Block, ClosingType.WithClosingTag);
                firstLine = false;
            }
            else if (line.Trim().Length < 20)
            {
                element = factory.GetLightElementNode("h2", DisplayType.Block, ClosingType.WithClosingTag);
            }
            else if (line.StartsWith(" "))
            {
                element = factory.GetLightElementNode("blockquote", DisplayType.Block, ClosingType.WithClosingTag);
            }
            else
            {
                element = factory.GetLightElementNode("p", DisplayType.Block, ClosingType.WithClosingTag);
            }
            element.AddChild(new LightTextNode(line));
            root.AddChild(element);
        }

        // Output HTML
        Console.WriteLine(root.OuterHTML);

        // Measure memory usage
        long memoryUsage = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory Usage: {memoryUsage} bytes");
    }
}
