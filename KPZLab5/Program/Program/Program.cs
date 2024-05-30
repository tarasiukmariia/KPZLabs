using System;
using System.Collections.Generic;
using System.Text;

public interface INodeVisitor
{
    void VisitTextNode(LightTextNode node);
    void VisitElementNode(LightElementNode node);
}

public abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
    public abstract void Accept(INodeVisitor visitor);
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
    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitTextNode(this);
    }
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
    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitElementNode(this);
    }
}
public class NodePrinterVisitor : INodeVisitor
{
    private StringBuilder _resultBuilder;

    public NodePrinterVisitor()
    {
        _resultBuilder = new StringBuilder();
    }

    public string GetResult()
    {
        return _resultBuilder.ToString();
    }

    public void VisitTextNode(LightTextNode node)
    {
        _resultBuilder.Append(node.OuterHTML);
    }

    public void VisitElementNode(LightElementNode node)
    {
        _resultBuilder.Append(node.OuterHTML);
    }
}