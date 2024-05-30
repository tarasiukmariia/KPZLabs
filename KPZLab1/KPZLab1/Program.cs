using System;

class Money
{
    public int Dollars { get; set; }
    public int Cents { get; set; }

    public Money(int dollars, int cents)
    {
        Dollars = dollars;
        Cents = cents;
    }

    public void Print()
    {
        Console.WriteLine($"Total: {Dollars} dollars and {Cents} cents");
    }
}

class Product
{
    public string Name { get; set; }
    public Money Price { get; set; }

    public Product(string name, Money price)
    {
        Name = name;
        Price = price;
    }

    public void ReducePrice(float amount)
    {
        int totalCents = Price.Dollars * 100 + Price.Cents;
        totalCents -= (int)(amount * 100);
        Price.Dollars = totalCents / 100;
        Price.Cents = totalCents % 100;
    }
}

class Warehouse
{
    public string Name { get; set; }
    public string Unit { get; set; }
    public Money UnitPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime LastDeliveryDate { get; set; }

    public Warehouse(string name, string unit, Money unitPrice, int quantity, DateTime lastDeliveryDate)
    {
        Name = name;
        Unit = unit;
        UnitPrice = unitPrice;
        Quantity = quantity;
        LastDeliveryDate = lastDeliveryDate;
    }
}

class Reporting
{
    public static void RegisterArrival(Warehouse warehouse, int quantity)
    {
        warehouse.Quantity += quantity;
        warehouse.LastDeliveryDate = DateTime.Now;
    }

    public static void RegisterShipment(Warehouse warehouse, int quantity)
    {
        warehouse.Quantity -= quantity;
    }

    public static void InventoryReport(Warehouse warehouse)
    {
        Console.WriteLine($"Inventory of {warehouse.Name}: {warehouse.Quantity} {warehouse.Unit}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Money price = new Money(10, 50);
        Product product = new Product("Apple", price);
        product.ReducePrice(2.25f);
        product.Price.Print();

        Money unitPrice = new Money(5, 75);
        Warehouse warehouse = new Warehouse("Apples", "kg", unitPrice, 100, DateTime.Now.AddDays(-10));
        Reporting.InventoryReport(warehouse);

        Reporting.RegisterArrival(warehouse, 50);
        Reporting.InventoryReport(warehouse);

        Reporting.RegisterShipment(warehouse, 30);
        Reporting.InventoryReport(warehouse);
    }
}

