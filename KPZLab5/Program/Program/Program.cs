using System;
using System.Collections.Generic;
using System.Text;

public interface IIterator<T>
{
    bool HasNext();
    T Next();
}
public class DepthFirstIterator : IIterator<LightNode>
{
    private Stack<LightNode> stack = new Stack<LightNode>();

    public DepthFirstIterator(LightNode root)
    {
        stack.Push(root);
    }

    public bool HasNext()
    {
        return stack.Count > 0;
    }

    public LightNode Next()
    {
        if (!HasNext()) throw new InvalidOperationException();

        var current = stack.Pop();

        if (current is LightElementNode elementNode)
        {
            for (int i = elementNode.Children.Count - 1; i >= 0; i--)
            {
                stack.Push(elementNode.Children[i]);
            }
        }

        return current;
    }
}
public class BreadthFirstIterator : IIterator<LightNode>
{
    private Queue<LightNode> queue = new Queue<LightNode>();

    public BreadthFirstIterator(LightNode root)
    {
        queue.Enqueue(root);
    }

    public bool HasNext()
    {
        return queue.Count > 0;
    }

    public LightNode Next()
    {
        if (!HasNext()) throw new InvalidOperationException();

        var current = queue.Dequeue();

        if (current is LightElementNode elementNode)
        {
            foreach (var child in elementNode.Children)
            {
                queue.Enqueue(child);
            }
        }

        return current;
    }
}

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
    public IIterator<LightNode> GetDepthFirstIterator()
    {
        return new DepthFirstIterator(this);
    }

    public IIterator<LightNode> GetBreadthFirstIterator()
    {
        return new BreadthFirstIterator(this);
    }
}
