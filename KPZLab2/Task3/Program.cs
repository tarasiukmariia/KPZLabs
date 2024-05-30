using System;

public class Authenticator
{

    private static Authenticator instance;

    private Authenticator() { }


    public static Authenticator GetInstance()
    {

        if (instance == null)
        {
            instance = new Authenticator();
        }
        return instance;
    }

    // Додаткові методи класу
    public void Login(string username, string password)
    {
        Console.WriteLine($"Користувач {username} успішно ввійшов.");
    }

    public void Logout(string username)
    {
        Console.WriteLine($"Користувач {username} успішно вийшов із системи.");
    }
}

class Program
{
    static void Main(string[] args)
    {

        Authenticator authenticator = Authenticator.GetInstance();
        authenticator.Login("user123", "password123");
        authenticator.Logout("user123");
    }
}
