﻿using System;
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

public class Program
{
    public static void Main()
    {
        // Створення елементів
        LightElementNode div = new LightElementNode("div", DisplayType.Block, ClosingType.WithClosingTag);
        div.CssClasses.Add("container");

        LightElementNode ul = new LightElementNode("ul", DisplayType.Block, ClosingType.WithClosingTag);
        ul.CssClasses.Add("list");

        LightElementNode li1 = new LightElementNode("li", DisplayType.Block, ClosingType.WithClosingTag);
        li1.AddChild(new LightTextNode("Item 1"));

        LightElementNode li2 = new LightElementNode("li", DisplayType.Block, ClosingType.WithClosingTag);
        li2.AddChild(new LightTextNode("Item 2"));

        LightElementNode li3 = new LightElementNode("li", DisplayType.Block, ClosingType.WithClosingTag);
        li3.AddChild(new LightTextNode("Item 3"));

        // Створення структури
        ul.AddChild(li1);
        ul.AddChild(li2);
        ul.AddChild(li3);

        div.AddChild(ul);

        // Виведення HTML
        Console.WriteLine(div.OuterHTML);
    }
}
