using System;
using System.Collections.Generic;


class Virus
{
    public double Weight { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public List<Virus> Children { get; set; }


    public Virus(double weight, int age, string name, string type)
    {
        Weight = weight;
        Age = age;
        Name = name;
        Type = type;
        Children = new List<Virus>();
    }


    public Virus Clone()
    {

        Virus clone = new Virus(Weight, Age, Name, Type);

        foreach (var child in Children)
        {
            clone.Children.Add(child.Clone());
        }

        return clone;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Virus grandparent = new Virus(1.5, 3, "Сімейство", "Грип");
        Virus parent1 = new Virus(1.3, 2, "Parent 1", "Вітрянка");
        Virus parent2 = new Virus(1.7, 4, "Parent 2", "COVID-19");
        Virus child1 = new Virus(0.8, 1, "Child 1", "ГРВІ");
        Virus child2 = new Virus(0.9, 1, "Child 2", "Бронхіт");

        grandparent.Children.Add(parent1);
        grandparent.Children.Add(parent2);
        parent1.Children.Add(child1);
        parent1.Children.Add(child2);
        Virus clonedGrandparent = grandparent.Clone();

        Console.WriteLine("Клонование сімейство:");
        Console.WriteLine($"Ім'я: {clonedGrandparent.Name}, Тип: {clonedGrandparent.Type}");
        foreach (var child in clonedGrandparent.Children)
        {
            Console.WriteLine($"- Child: Ім'я: {child.Name}, Тип: {child.Type}");
        }
    }
}
