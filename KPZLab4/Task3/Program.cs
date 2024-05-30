using System;
using System.Collections.Generic;
using System.Text;

class LightNode
{
    public virtual string OuterHTML { get { return ""; } }
    public virtual string InnerHTML { get { return ""; } }
    public virtual void AddEventListener(string eventName, Action eventHandler) { }
}

class LightTextNode : LightNode
{
    private string text;

    public LightTextNode(string text)
    {
        this.text = text;
    }

    public override string OuterHTML
    {
        get { return text; }
    }

    public override string InnerHTML
    {
        get { return text; }
    }
}

class LightElementNode : LightNode
{
    protected string tagName;
    protected string displayType;
    protected string closingType;
    protected Dictionary<string, Action> eventListeners;
    protected List<LightNode> children;

    public LightElementNode(string tagName, string displayType, string closingType)
    {
        this.tagName = tagName;
        this.displayType = displayType;
        this.closingType = closingType;
        this.children = new List<LightNode>();
        this.eventListeners = new Dictionary<string, Action>();
    }

    public void AddChild(LightNode child)
    {
        children.Add(child);
    }

    public override void AddEventListener(string eventName, Action eventHandler)
    {
        eventListeners[eventName] = eventHandler;
    }

    public override string OuterHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<{tagName}>");
            foreach (var child in children)
            {
                sb.Append(child.OuterHTML);
            }
            if (closingType == "double")
            {
                sb.Append($"</{tagName}>");
            }
            else if (closingType == "single")
            {
                sb.Append($"/>");
            }
            return sb.ToString();
        }
    }

    public override string InnerHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in children)
            {
                sb.Append(child.OuterHTML);
            }
            return sb.ToString();
        }
    }
}

class LightImageNode : LightNode
{
    private string src;

    public LightImageNode(string src)
    {
        this.src = src;
    }

    public override string OuterHTML
    {
        get
        {
            return $"<img src=\"{src}\">";
        }
    }

    public override string InnerHTML
    {
        get { return ""; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        LightElementNode div = new LightElementNode("div", "block", "double");
        LightElementNode ul = new LightElementNode("ul", "block", "double");
        LightElementNode li1 = new LightElementNode("li", "block", "double");
        LightTextNode text1 = new LightTextNode("Item 1");
        li1.AddChild(text1);
        LightElementNode li2 = new LightElementNode("li", "block", "double");
        LightTextNode text2 = new LightTextNode("Item 2");
        li2.AddChild(text2);
        ul.AddChild(li1);
        ul.AddChild(li2);
        div.AddChild(ul);

        div.AddEventListener("click", () => Console.WriteLine("Div clicked!"));
        li1.AddEventListener("click", () => Console.WriteLine("Item 1 clicked!"));

        LightImageNode img = new LightImageNode("https://example.com/image.jpg");
        div.AddChild(img);

        Console.WriteLine("Зовнішній HTML:");
        Console.WriteLine(div.OuterHTML);
        Console.WriteLine();
        Console.WriteLine("Внутрішній HTML:");
        Console.WriteLine(div.InnerHTML);

        div.AddEventListener("click", () => Console.WriteLine("Div clicked!"));

        li1.AddEventListener("click", () => Console.WriteLine("Item 1 clicked!"));
    }
}

