using System;

public class Logger
{
    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

public class FileWriter
{
    private string filePath;

    public FileWriter(string filePath)
    {
        this.filePath = filePath;
    }

    public void Write(string message)
    {
        File.AppendAllText(filePath, message);
    }

    public void WriteLine(string message)
    {
        File.AppendAllText(filePath, message + Environment.NewLine);
    }
}

public class FileLogger
{
    private FileWriter fileWriter;

    public FileLogger(string filePath)
    {
        fileWriter = new FileWriter(filePath);
    }

    public void Log(string message)
    {
        fileWriter.WriteLine("LOG: " + message);
    }

    public void Error(string message)
    {
        fileWriter.WriteLine("ERROR: " + message);
    }

    public void Warn(string message)
    {
        fileWriter.WriteLine("WARN: " + message);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Logger consoleLogger = new Logger();
        consoleLogger.Log("This is a log message.");
        consoleLogger.Error("This is an error message.");
        consoleLogger.Warn("This is a warning message.");

        string logFilePath = "log.txt";
        FileLogger fileLogger = new FileLogger(logFilePath);
        fileLogger.Log("This is a log message.");
        fileLogger.Error("This is an error message.");
        fileLogger.Warn("This is a warning message.");

        Console.WriteLine("Messages have been logged to the console and the file.");
    }
}
