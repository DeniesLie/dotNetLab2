using Data.Entities;

namespace LinqLab1;

public static class QueryPrinter
{
    public static void Print(this Item item, int offset)
    {
        var offsetStr = new string('\t', offset);
        Console.WriteLine($"\t{offsetStr}{item.Name}:");
        Console.WriteLine($"\t\t{offsetStr}PricePerUnit: {item.PricePerUnit}");
        Console.WriteLine($"\t\t{offsetStr}Manufacturer: (ManufacturerId: {item.ManufacturerId}, Name: {item.Manufacturer?.Name ?? "null"})");
    }
        
    
    public static void Print(this IDictionary<Manufacturer, IEnumerable<DateTime>> dict)
    {
        foreach (var (manufacturer, dateTimes) in dict)
        {
            Console.WriteLine($"\t{manufacturer.Name}:");
            foreach (var dateTime in dateTimes)
            {
                Console.WriteLine($"\t\t{dateTime}");
            }
        }
    }

    public static void Print(this IDictionary<Manufacturer, int> dict)
    {
        foreach (var (manufacturer, amount) in dict)
        {
            Console.WriteLine($"\t{manufacturer.Name}: {amount}");
        }
    }
    
    public static void Print(this IDictionary<Manufacturer, IEnumerable<ItemCategory>> dict)
    {
        foreach (var (manufacturer, categories) in dict)
        {
            Console.WriteLine($"\t{manufacturer.Name}:");
            foreach (var category in categories)
            {
                Console.WriteLine($"\t\t{category.Name}");
            }
        }
    }
    
    public static void Print(this IDictionary<Manufacturer, IEnumerable<Item>> dict)
    {
        foreach (var (manufacturer, items) in dict)
        {
            Console.WriteLine($"\t{manufacturer.Name}:");
            foreach (var item in items)
            {
                item.Print(offset: 1);
            }
        }
    }
    
    public static void Print(this IDictionary<Manufacturer, Item> dict)
    {
        foreach (var (manufacturer, item) in dict)
        {
            Console.WriteLine($"\t{manufacturer.Name}: {item.Name}({item.PricePerUnit}$)");
        }
    }

    public static void Print(this IEnumerable<Manufacturer> manufacturers)
    {
        foreach (var manufacturer in manufacturers)
        {
            Console.WriteLine($"\t{manufacturer.Name}; Id = {manufacturer.Id}");
        }
    }
    
    public static void Print(this IEnumerable<Item> items)
    {
        foreach (var item in items)
        {
            item.Print(offset: 0);
        }
    }

    public static void Print(this IEnumerable<ItemCategory> categories)
    {
        foreach (var category in categories)
        {
            Console.WriteLine($"\t{category.Name}");
        }
            
    }
    
    public static void PrintLastSupplyDateTimeInfo(this IEnumerable<Item> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine($"\t{item.Name}: {item.SupplyDateTimes.Max()}");
        }
    }
    
    public static void PrintPriceInfo(this IEnumerable<Item> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine($"\t{item.Name}: {item.PricePerUnit}$");
        }
    }
    
    public static void Print(this IDictionary<DateTimeOffset, IEnumerable<Item>> dict)
    {
        foreach (var (dateTime, items) in dict)
        {
            Console.WriteLine($"\t{dateTime}:");
            foreach (var item in items)
            {
                item.Print(offset: 1);
            }
        }
    }
}