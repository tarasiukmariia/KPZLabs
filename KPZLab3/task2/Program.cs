using System;
using System.Collections.Generic;

// Базовий клас Hero
public abstract class Hero
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool ArmorEquipped;
    public bool WeaponEquipped;
    public bool ArtifactEquipped;

    public abstract void Display();
}

// Конкретні класи героїв
public class Warrior : Hero
{
    public Warrior(string name)
    {
        Name = name;
        Health = 100;
        Attack = 10;
        ArmorEquipped = false;
        WeaponEquipped = false;
        ArtifactEquipped = false;
    }

    public override void Display()
    {
        Console.WriteLine($"Warrior {Name}, Health: {Health}, Attack: {Attack}");
    }
}

public class Mage : Hero
{
    public Mage(string name)
    {
        Name = name;
        Health = 80;
        Attack = 15;
        ArmorEquipped = false;
        WeaponEquipped = false;
        ArtifactEquipped = false;
    }

    public override void Display()
    {
        Console.WriteLine($"Mage {Name}, Health: {Health}, Attack: {Attack}");
    }
}

public class Paladin : Hero
{
    public Paladin(string name)
    {
        Name = name;
        Health = 120;
        Attack = 8;
        ArmorEquipped = false;
        WeaponEquipped = false;
        ArtifactEquipped = false;
    }

    public override void Display()
    {
        Console.WriteLine($"Paladin {Name}, Health: {Health}, Attack: {Attack}");
    }
}

// Абстрактний клас декоратора
public abstract class InventoryDecorator : Hero
{
    protected Hero _hero;

    public InventoryDecorator(Hero hero)
    {
        _hero = hero;
    }

    public override void Display()
    {
        _hero.Display();
    }
}

// Конкретні декоратори для інвентаря
public class Armor : InventoryDecorator
{
    public Armor(Hero hero) : base(hero)
    {
        _hero.ArmorEquipped = true;
    }

    public override void Display()
    {
        Console.WriteLine($"{_hero.Name} equipped with Artifact. Additional health: 50.");
    }
}

public class Weapon : InventoryDecorator
{
    public Weapon(Hero hero) : base(hero)
    {
        _hero.WeaponEquipped = true;
    }

    public override void Display()
    {
        Console.WriteLine($"{_hero.Name} equipped with Artifact. Additional attack: 10.");
    }
}

public class Artifact : InventoryDecorator
{
    public Artifact(Hero hero) : base(hero)
    {
        _hero.WeaponEquipped = true;
    }

    public override void Display()
    {
        Console.WriteLine($"{_hero.Name} equipped with Artifact. Additional health: 10, Additional attack: 5");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Hero warrior = new Warrior("Aragorn");
        warrior.Display();

        // Додаємо інвентар до воїна
        warrior = new Armor(warrior);
        warrior.Display();

        warrior = new Weapon(warrior);
        warrior.Display();

        warrior = new Artifact(warrior);
        warrior.Display();

        // Створюємо мага і додаємо йому інвентар
        Hero mage = new Mage("Gandalf");
        mage.Display();

        mage = new Armor(mage);
        mage.Display();

        mage = new Weapon(mage);
        mage.Display();

        mage = new Artifact(mage);
        mage.Display();

        // Створюємо паладина і додаємо йому інвентар
        Hero paladin = new Paladin("Lancelot");
        paladin.Display();

        paladin = new Armor(paladin);
        paladin.Display();

        paladin = new Weapon(paladin);
        paladin.Display();

        paladin = new Artifact(paladin);
        paladin.Display();
    }
}
