using System;

class SupportSystem
{
    static void Main(string[] args)
    {
        SupportHandler level1 = new Level1Support();
        SupportHandler level2 = new Level2Support();
        SupportHandler level3 = new Level3Support();
        SupportHandler level4 = new Level4Support();
        while (true)
        {
            Console.WriteLine("Виберіть тип вашої проблеми:");
            Console.WriteLine("1. Проблеми з підключенням до мережі");
            Console.WriteLine("2. Проблеми з програмним забезпеченням");
            Console.WriteLine("3. Проблеми з обладнанням");
            Console.WriteLine("4. Інші проблеми");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        level1.HandleRequest();
                        break;
                    case 2:
                        level2.HandleRequest();
                        break;
                    case 3:
                        level3.HandleRequest();
                        break;
                    case 4:
                        level4.HandleRequest();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Будь ласка, спробуйте ще раз.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Невірний ввід. Будь ласка, спробуйте ще раз.");
            }
        }
    }
}

abstract class SupportHandler
{
    protected SupportHandler successor;

    public void SetSuccessor(SupportHandler successor)
    {
        this.successor = successor;
    }

    public abstract void HandleRequest();
}

class Level1Support : SupportHandler
{
    public override void HandleRequest()
    {
        Console.WriteLine("Це рівень 1 підтримки. Ваше запитання буде вирішено найшвидше.");
    }
}

class Level2Support : SupportHandler
{
    public override void HandleRequest()
    {
        Console.WriteLine("Це рівень 2 підтримки. Ваше запитання буде вирішено у найближчому часі.");

    }
}

class Level3Support : SupportHandler
{
    public override void HandleRequest()
    {
        Console.WriteLine("Це рівень 3 підтримки. Ваше запитання буде вирішено якнайшвидше.");
    }
}

class Level4Support : SupportHandler
{
    public override void HandleRequest()
    {
        Console.WriteLine("Це рівень 4 підтримки. Ваше запитання буде вирішено найповніше.");
    }
}
