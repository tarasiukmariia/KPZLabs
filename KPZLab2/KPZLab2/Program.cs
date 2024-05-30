using System;
using System.Collections.Generic;


public abstract class Subscription
{
    public decimal MonthlyFee { get; set; }
    public int MinimumSubscriptionPeriod { get; set; }
    public List<string> Channels { get; set; }

    public Subscription(decimal monthlyFee, int minSubscriptionPeriod)
    {
        MonthlyFee = monthlyFee;
        MinimumSubscriptionPeriod = minSubscriptionPeriod;
        Channels = new List<string>();
    }

    public abstract void AddChannels(List<string> channels);
}


public class DomesticSubscription : Subscription
{
    public DomesticSubscription(decimal monthlyFee, int minSubscriptionPeriod)
        : base(monthlyFee, minSubscriptionPeriod)
    {

    }

    public override void AddChannels(List<string> channels)
    {
        Channels.AddRange(channels);
    }
}


public class EducationalSubscription : Subscription
{
    public EducationalSubscription(decimal monthlyFee, int minSubscriptionPeriod)
        : base(monthlyFee, minSubscriptionPeriod)
    {

    }

    public override void AddChannels(List<string> channels)
    {
        Channels.AddRange(channels);
    }
}


public class PremiumSubscription : Subscription
{
    public PremiumSubscription(decimal monthlyFee, int minSubscriptionPeriod)
        : base(monthlyFee, minSubscriptionPeriod)
    {

    }

    public override void AddChannels(List<string> channels)
    {
        Channels.AddRange(channels);
    }
}


public class WebSite
{
    public Subscription PurchaseSubscription(decimal monthlyFee, int minSubscriptionPeriod)
    {

        return new DomesticSubscription(monthlyFee, minSubscriptionPeriod);
    }
}


public class MobileApp
{
    public Subscription PurchaseSubscription(decimal monthlyFee, int minSubscriptionPeriod)
    {

        return new EducationalSubscription(monthlyFee, minSubscriptionPeriod);
    }
}


public class ManagerCall
{
    public Subscription PurchaseSubscription(decimal monthlyFee, int minSubscriptionPeriod)
    {

        return new PremiumSubscription(monthlyFee, minSubscriptionPeriod);
    }
}

class Program
{
    static void Main(string[] args)
    {

        WebSite webSite = new WebSite();
        Subscription domesticSubscription = webSite.PurchaseSubscription(10.99m, 1);
        domesticSubscription.AddChannels(new List<string> { "News", "Entertainment" });

        Console.WriteLine("Channels in Domestic Subscription:");
        foreach (var channel in domesticSubscription.Channels)
        {
            Console.WriteLine(channel);
        }
    }
}
