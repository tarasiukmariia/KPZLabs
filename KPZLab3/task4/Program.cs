using System;
using System.IO;
using System.Text.RegularExpressions;

public class SmartTextReader
{
    public char[][] ReadFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        string[] lines = File.ReadAllLines(filePath);
        char[][] result = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].ToCharArray();
        }

        return result;
    }
}

public class SmartTextChecker
{
    private SmartTextReader _smartTextReader;

    public SmartTextChecker(SmartTextReader smartTextReader)
    {
        _smartTextReader = smartTextReader;
    }

    public char[][] ReadFile(string filePath)
    {
        Console.WriteLine("Attempting to open file...");
        char[][] result = _smartTextReader.ReadFile(filePath);
        Console.WriteLine("File successfully read.");

        int lineCount = result.Length;
        int charCount = 0;

        foreach (var line in result)
        {
            charCount += line.Length;
        }

        Console.WriteLine($"File contains {lineCount} lines and {charCount} characters.");
        return result;
    }
}

public class SmartTextReaderLocker
{
    private SmartTextReader _smartTextReader;
    private Regex _accessPattern;

    public SmartTextReaderLocker(SmartTextReader smartTextReader, string pattern)
    {
        _smartTextReader = smartTextReader;
        _accessPattern = new Regex(pattern, RegexOptions.IgnoreCase);
    }

    public char[][] ReadFile(string filePath)
    {
        if (_accessPattern.IsMatch(filePath))
        {
            Console.WriteLine("Access denied!");
            return null;
        }

        return _smartTextReader.ReadFile(filePath);
    }
}

public class Program
{
    public static void Main()
    {
        // Отримуємо шлях до директорії запуску програми
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, "example.txt"); // Використовуємо відносний шлях

        // Створення об'єкта SmartTextReader
        SmartTextReader reader = new SmartTextReader();

        // Проксі для логування
        SmartTextChecker checker = new SmartTextChecker(reader);
        char[][] checkedContent = checker.ReadFile(filePath);

        // Проксі з обмеженням доступу
        SmartTextReaderLocker locker = new SmartTextReaderLocker(reader, @"restricted\.txt$");
        char[][] lockedContent1 = locker.ReadFile(filePath); // Доступ дозволено
        char[][] lockedContent2 = locker.ReadFile("restricted.txt"); // Доступ заборонено
    }
}
