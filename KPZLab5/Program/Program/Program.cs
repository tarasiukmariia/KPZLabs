using System;
using System.Collections.Generic;
using System.Text;

public abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
    protected virtual void OnCreated() { }
    public virtual void OnInserted() { }
    protected virtual void OnRemoved() { }
    protected virtual void OnStylesApplied() { }
    protected virtual void OnClassListApplied() { }
    protected virtual void OnTextRendered() { }
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

    protected override void OnTextRendered()
    {
        Console.WriteLine("Text node rendered.");
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
            OnCreated();
            StringBuilder sb = new StringBuilder();
            sb.Append($"<{TagName}");

            if (CssClasses.Count > 0)
            {
                sb.Append($" class=\"{string.Join(" ", CssClasses)}\"");
                OnClassListApplied();
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
                child.OnInserted();
            }

            return sb.ToString();
        }
    }

    public void AddChild(LightNode child)
    {
        Children.Add(child);
    }

    protected override void OnRemoved()
    {
        Console.WriteLine("Element node removed.");
    }

    protected override void OnStylesApplied()
    {
        Console.WriteLine("Styles applied to element node.");
    }
}
