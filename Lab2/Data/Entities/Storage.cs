namespace Data.Entities;

public class Storage
{
    private List<Item> _items = new();
    private List<Manufacturer> _manufacturers = new();
    private List<ItemCategory> _itemCategories = new();
    
    public List<Item> Items
    {
        get => _items;
        set => _items = value;
    }
    public List<Manufacturer> Manufacturers
    {
        get => _manufacturers;
    }
    public List<ItemCategory> ItemCategories
    {
        get => _itemCategories;
    }

    public void SeedData()
    {
        _manufacturers = new()
        {
            new()
            {
                Id = 1,
                Name = "Samsung",
            },
            new()
            {
                Id = 2,
                Name = "DELL",
            },
            new()
            {
                Id = 3,
                Name = "Apple",
            },
            new()
            {
                Id = 4,
                Name = "Xiaomi",
            },
            new()
            {
                Id = 5,
                Name = "Huawei",
            }
        };
        _itemCategories = new()
        {
            new(){Id = 1, Name = "laptop"},
            new() {Id = 2, Name = "smartphone"},
            new() {Id = 3, Name = "headset"},
            new() {Id = 4, Name = "computer mouse"},
            new () {Id = 5, Name = "wireless"},
            new() {Id = 6, Name = "accessory"},
            new() {Id = 7, Name = "charger"},
            new () {Id = 8, Name = "phone case"},
        };
        
        _items = new()
        {
            new()
            {
                Id = 1,
                Name="iPhone 10", 
                PricePerUnit = (decimal) 499.99, 
                Amount = 25, 
                ManufacturerId = 3,
                ItemCategories = _itemCategories.Where(p => p.Name is "smartphone").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 05, 23, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 2, 26, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 3, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 14, 12, 0, 0, TimeSpan.Zero)
                }
            },
            new()
            {
                Id = 2,
                Name="Macbook Pro 16 (2021)", 
                PricePerUnit = (decimal) 1199.99, 
                Amount = 12, 
                ManufacturerId = 3,
                ItemCategories = _itemCategories.Where(p => p.Name is "laptop").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 15, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 1, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 16, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 3, 12, 0, 0, TimeSpan.Zero)
                }
            },
            new()
            { 
                Id = 3,
                Name="Samsung Galaxy S22 Ultra", 
                PricePerUnit = (decimal) 35.99, 
                Amount = 11, 
                ManufacturerId = 1,
                ItemCategories = _itemCategories.Where(p => p.Name == "smartphone").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 5, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 2, 20, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 4, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 23, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 4, 12, 0, 0, TimeSpan.Zero),
                }
            },
            new()
            {
                Id = 4,
                Name="AirPods Pro", 
                PricePerUnit = (decimal) 149.99, 
                Amount = 16, 
                ManufacturerId = 3,
                ItemCategories = _itemCategories.Where(p => p.Name is "headset" or "wireless").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 5, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 8, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 7, 12, 0, 0, TimeSpan.Zero),
                }
            },
            new()
            {
                Id = 5,
                Name="Dell Latitude 16", 
                PricePerUnit = (decimal) 89.99, 
                Amount = 3, 
                ManufacturerId = 2,
                ItemCategories = _itemCategories.Where(p => p.Name == "laptop").ToList(),
                SupplyDateTimes = new()
                {
                    new (2020, 2, 7, 12, 0, 0, TimeSpan.Zero),
                    new (2020, 2, 28, 12, 0, 0, TimeSpan.Zero),
                }
            },
            new()
            {
                Id = 6, 
                Name="Xiaomi Mi Dual Mode Wireless Mouse", 
                PricePerUnit = (decimal) 19.99, 
                Amount = 67, 
                ManufacturerId = 4,
                ItemCategories = _itemCategories.Where(p => p.Name is "computer mouse" or "wireless" or "accessory").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 9, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 2, 25, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 4, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 20, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 17, 12, 0, 0, TimeSpan.Zero)
                }
            },
            new()
            {
                Id = 7,
                Name="Xiaomi Mi Wireless Charger 10W", 
                PricePerUnit = (decimal) 24.99, 
                Amount = 10, 
                ManufacturerId = 4,
                ItemCategories = _itemCategories.Where(p => p.Name is "charger" or "wireless" or "accessory").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 15, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 1, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 22, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 5, 12, 0, 0, TimeSpan.Zero),
                }
            },
            new()
            {
                Id = 8,
                Name="Huawei SuperCharger 40W", 
                PricePerUnit = (decimal) 21.99, 
                Amount = 18, 
                ManufacturerId = 5,
                ItemCategories = _itemCategories.Where(p => p.Name is "charger" or "accessory").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 9, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 2, 23, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 6, 12, 0, 0, TimeSpan.Zero),
                }
            },
            new()
            {
                Id = 9,
                Name="iPhone 13 Pro Phone Case Blue Fog", 
                PricePerUnit = (decimal) 9.99,
                Amount = 30, 
                ManufacturerId = 3,
                ItemCategories = _itemCategories.Where(p => p.Name is "phone case" or "accessory").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 20, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 22, 12, 0, 0, TimeSpan.Zero),
                }
            },
            new()
            {
                Id = 10,
                Name="Dell Inspiron 16 (2021)", 
                PricePerUnit = (decimal) 159.99, 
                Amount = 57, 
                ManufacturerId = 2,
                ItemCategories = _itemCategories.Where(p => p.Name == "laptop").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 9, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 2, 28, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 4, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 30, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 15, 12, 0, 0, TimeSpan.Zero)
                }
            },
            new()
            {
                Id = 11,
                Name="Xiaomi Mi Notebook Air 12,5", 
                PricePerUnit = (decimal) 109.99, 
                Amount = 57, 
                ManufacturerId = 4,
                ItemCategories = _itemCategories.Where(p => p.Name == "laptop").ToList(),
                SupplyDateTimes = new()
                {
                    new (2022, 2, 9, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 2, 28, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 4, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 3, 30, 12, 0, 0, TimeSpan.Zero),
                    new (2022, 4, 15, 12, 0, 0, TimeSpan.Zero)
                }
            }
        };
        foreach (var item in _items)
        {
            item.Manufacturer = _manufacturers.Find(m => m.Id == item.ManufacturerId);
        }
    }
}