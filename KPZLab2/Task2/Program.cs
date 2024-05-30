using System;


public abstract class Device
{
    public string Model { get; set; }
    public string Brand { get; set; }

    public Device(string model, string brand)
    {
        Model = model;
        Brand = brand;
    }

    public abstract void DisplayInfo();
}


public class Laptop : Device
{
    public Laptop(string model, string brand) : base(model, brand) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Laptop: {Brand} {Model}");
    }
}


public class Netbook : Device
{
    public Netbook(string model, string brand) : base(model, brand) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Netbook: {Brand} {Model}");
    }
}


public class EBook : Device
{
    public EBook(string model, string brand) : base(model, brand) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"EBook: {Brand} {Model}");
    }
}


public class Smartphone : Device
{
    public Smartphone(string model, string brand) : base(model, brand) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Smartphone: {Brand} {Model}");
    }
}


public class DeviceFactory
{
    public Device CreateDevice(string type, string model, string brand)
    {
        switch (type.ToLower())
        {
            case "laptop":
                return new Laptop(model, brand);
            case "netbook":
                return new Netbook(model, brand);
            case "ebook":
                return new EBook(model, brand);
            case "smartphone":
                return new Smartphone(model, brand);
            default:
                throw new ArgumentException($"Unknown device type: {type}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        DeviceFactory factory = new DeviceFactory();

        Device laptop = factory.CreateDevice("laptop", "XPS 15", "Dell");
        laptop.DisplayInfo();

        Device smartphone = factory.CreateDevice("smartphone", "Galaxy S21", "Samsung");
        smartphone.DisplayInfo();
    }
}
