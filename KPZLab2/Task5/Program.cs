using System;
using System.Collections.Generic;


class Character
{
    public string Name { get; set; }
    public int Height { get; set; }
    public string HairColor { get; set; }
    public string EyeColor { get; set; }
    public List<string> Inventory { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Ім'я: {Name}, Висота: {Height}cm, Колір волосся: {HairColor}, Колір очей: {EyeColor}");
        Console.WriteLine("Інвентар:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }
}


class HeroBuilder
{
    private Character _hero = new Character();

    public HeroBuilder SetName(string name)
    {
        _hero.Name = name;
        return this;
    }

    public HeroBuilder SetHeight(int height)
    {
        _hero.Height = height;
        return this;
    }

    public HeroBuilder SetHairColor(string hairColor)
    {
        _hero.HairColor = hairColor;
        return this;
    }

    public HeroBuilder SetEyeColor(string eyeColor)
    {
        _hero.EyeColor = eyeColor;
        return this;
    }

    public HeroBuilder AddToInventory(string item)
    {
        if (_hero.Inventory == null)
            _hero.Inventory = new List<string>();
        _hero.Inventory.Add(item);
        return this;
    }

    public Character Build()
    {
        return _hero;
    }
}


class EnemyBuilder : HeroBuilder
{
  
    public EnemyBuilder AddEvilDeed(string deed)
    {
        base.AddToInventory($"Злий вчинок: {deed}");
        return this;
    }

    public new Character Build()
    {
        base.AddToInventory("Сильний меч");
        return base.Build();
    }
}

class Program
{
    static void Main(string[] args)
    {

        Character hero = new HeroBuilder()
            .SetName("Арина")
            .SetHeight(162)
            .SetHairColor("Блондинка")
            .SetEyeColor("Блакитний")
            .AddToInventory("Меч")
            .AddToInventory("Щит")
            .Build();

        Console.WriteLine("Герой:");
        hero.ShowInfo();

        Character enemy = new EnemyBuilder()
            .SetName("Машка")
            .SetHeight(164)
            .SetHairColor("Русява")
            .SetEyeColor("Блакитні")
            .AddToInventory("Меч")
            .AddToInventory("Щит")
            .Build();

        Console.WriteLine("\nВорог:");
        enemy.ShowInfo();
    }
}

