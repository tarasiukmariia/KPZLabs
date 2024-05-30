using System;
using System.Collections.Generic;
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


public abstract class Command
{
    public abstract void Execute(LightElementNode node);
}

public class AddCssClassCommand : Command
{
    private string _cssClass;

    public AddCssClassCommand(string cssClass)
    {
        _cssClass = cssClass;
    }

    public override void Execute(LightElementNode node)
    {
        node.CssClasses.Add(_cssClass);
    }
}

public class AddChildCommand : Command
{
    private LightNode _child;

    public AddChildCommand(LightNode child)
    {
        _child = child;
    }

    public override void Execute(LightElementNode node)
    {
        node.AddChild(_child);
    }
}
public class CommandManager
{
    private List<Command> _commands = new List<Command>();

    public void ExecuteCommand(Command command, LightElementNode node)
    {
        command.Execute(node);
        _commands.Add(command);
    }
}