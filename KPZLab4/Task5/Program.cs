using System;
using System.Collections.Generic;

class TextDocument
{
    private string content;

    public TextDocument(string content)
    {
        this.content = content;
    }

    public string GetContent()
    {
        return content;
    }

    public void SetContent(string newContent)
    {
        content = newContent;
    }
}

class TextDocumentMemento
{
    private string content;

    public TextDocumentMemento(string content)
    {
        this.content = content;
    }

    public string GetContent()
    {
        return content;
    }
}

class TextEditor
{
    private TextDocument document;
    private Stack<TextDocumentMemento> history;

    public TextEditor(TextDocument document)
    {
        this.document = document;
        history = new Stack<TextDocumentMemento>();
    }

    public void Save()
    {
        history.Push(new TextDocumentMemento(document.GetContent()));
    }
    public void Undo()
    {
        if (history.Count > 0)
        {
            TextDocumentMemento memento = history.Pop();
            document.SetContent(memento.GetContent());
        }
    }
}

class Program
{
    static void Main(string[] args)
    {

        TextDocument document = new TextDocument("Initial content.");

        TextEditor editor = new TextEditor(document);
        editor.Save();

        document.SetContent("New content.");
        editor.Save();

        Console.WriteLine("Current content:");
        Console.WriteLine(document.GetContent());

        editor.Undo();

        Console.WriteLine("\nContent after undo:");
        Console.WriteLine(document.GetContent());
    }
}
