using System.Xml.Linq;
using Data.Comparers;
using Data.Entities;
using Data.MapExtension;

namespace Data;

public class ItemService
{
    private readonly ItemRepository _itemRepo;
    private readonly ManufacturerComparer _manufacturerComparer = new ManufacturerComparer();
    private readonly ItemCategoryComparer _itemCategoryComparer = new ItemCategoryComparer();
    private readonly ItemComparer _itemComparer = new ItemComparer();
    
    public ItemService(ItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public void Add(Item item) => _itemRepo.Add(item);
    
    public IEnumerable<Item> GetAll() => _itemRepo.GetAll();

    public void AddSupplyDateTimeToItem(int itemId, DateTime dateTime)
    {
        DateTimeOffset dateTimeOffset = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        var xItemToUpdate = _itemRepo.Query().Single(i => Int32.Parse(i.Attribute("Id").Value) == itemId);
        xItemToUpdate.Element("SupplyDateTimes")
            .Add(new XElement("SupplyDateTime", dateTimeOffset));
        _itemRepo.SaveToXDoc();
    }
    
    public IEnumerable<Item> GetAllItemsFromManufacturer(string manufacturerName)
    {
        return _itemRepo.Query()
            .Where(i => i.Element("Manufacturer").Attribute("Name").Value == manufacturerName)
            .Select(xItem => xItem.ToItem());
    }

    public IEnumerable<ItemCategory> GetCategoriesOfItemById(int itemId)
    {
        return _itemRepo.Query()
            .SingleOrDefault(i => Int32.Parse(i.Attribute("Id").Value) == itemId)
            .ToItem()
            .ItemCategories;
    }

    public IEnumerable<Manufacturer> GetAllManufacturers()
    {
        return _itemRepo.Query()
            .Select(i => i.Element("Manufacturer").ToManufacturer())
            .Distinct(_manufacturerComparer);
    }

    public IEnumerable<Item> GetItemsThatContainsCategory(string categoryName)
    {
        return _itemRepo.Query()
            .Where(i =>
                i.Element("ItemCategories").Elements("ItemCategory").Select(c => c.Attribute("Name").Value)
                    .Contains(categoryName))
            .Select(xItem => xItem.ToItem());
    }

    // query
    public IEnumerable<Item> GetItemsSortedByPrice()
    {
        return _itemRepo.Query().OrderByDescending(i => Double.Parse(i.Element("PricePerUnit").Value))
            .Select(xItem => xItem.ToItem());
    }

    public IDictionary<Item, DateTimeOffset> GetItemsSortedByLastSupplyDate()
    {
        return _itemRepo.Query()
            .OrderByDescending(i =>
                i.Element("SupplyDateTimes").Elements("SupplyDateTime").Select(dt => DateTimeOffset.Parse(dt.Value)).Max(dt => dt))
            .Select(xItem => xItem.ToItem())
            .ToDictionary(i => i,
                i => i.SupplyDateTimes.Max(dt => dt));
    }
    
    // query
    public IDictionary<Manufacturer, int> GetItemsAmountPerManufacturer()
    {
        var grouping = _itemRepo.Query()
            .GroupBy(i => i.Element("Manufacturer").ToManufacturer(), comparer: _manufacturerComparer);

        return grouping.ToDictionary(g => g.Key, g => g.Count());
    }

    public IEnumerable<Item> GetTop3CheapestItems()
    {
        return _itemRepo.Query().OrderBy(i => Double.Parse(i.Element("PricePerUnit").Value))
            .Take(3)
            .Select(xItem => xItem.ToItem());
    }

    // query
    public IDictionary<Manufacturer, IEnumerable<ItemCategory>> GetItemCategoriesSoldByManufacturer()
    {
        var grouping = _itemRepo.Query()
            .GroupBy(i => i.Element("Manufacturer").ToManufacturer(), comparer: _manufacturerComparer);

        return grouping.ToDictionary(
            g => g.Key,
            g =>
                g.SelectMany(i =>
                    i.Element("ItemCategories").Elements("ItemCategory")
                        .ToItemCategories()).Distinct(_itemCategoryComparer));
    }

    public IEnumerable<Item> FindLaptopsByPriceRange(decimal fromPrice, decimal toPrice)
    {
        return _itemRepo.Query()
            .Where(i =>
                i.Element("ItemCategories").Elements("ItemCategory")
                    .Select(c => c.Attribute("Name").Value).Contains("laptop")
                && Decimal.Parse(i.Element("PricePerUnit").Value) >= fromPrice
                && Decimal.Parse(i.Element("PricePerUnit").Value) <= toPrice)
            .Select(xItem => xItem.ToItem());
    }

    // query
    public IEnumerable<Item> GetWiredChargers()
    {
        return _itemRepo.Query()
            .Where(i =>
                i.Element("ItemCategories").Elements("ItemCategory")
                    .Select(c => c.Attribute("Name").Value).Contains("charger")
                && !i.Element("ItemCategories").Elements("ItemCategory")
                    .Select(c => c.Attribute("Name").Value).Contains("wireless"))
            .Select(xItem => xItem.ToItem());
    }

    public IDictionary<Manufacturer, IEnumerable<Item>> GetManufacturersWithAtLeastOneLaptop()
    {
        return _itemRepo.Query()
            .GroupBy(i => i.Element("Manufacturer").ToManufacturer(), comparer: _manufacturerComparer)
            .Where(g =>
                g.Any(i => i.Element("ItemCategories").Elements("ItemCategory")
                    .Select(c => c.Attribute("Name").Value).Contains("laptop")))
            .ToDictionary(g => g.Key,
                g => g.Where(i =>
                    i.Element("ItemCategories").Elements("ItemCategory")
                        .Select(c => c.Attribute("Name").Value).Contains("laptop"))
                    .Select(xItem => xItem.ToItem()));
    }

    // query
    public double GetAveragePriceOfPhones()
    {
        return _itemRepo.Query()
            .Where(i =>
                i.Element("ItemCategories").Elements("ItemCategory")
                    .Select(c => c.Attribute("Name").Value).Contains("smartphone"))
            .Select(i => Double.Parse(i.Element("PricePerUnit").Value))
            .Average(price => price);
    }


    public IDictionary<DateTimeOffset, IEnumerable<Item>> GetSuppliesInfo()
    {
        return _itemRepo.Query()
            .SelectMany(i => i.Element("SupplyDateTimes").Elements("SupplyDateTime").Select(dt => new
            {
                Item = i.ToItem(),
                SupplyDateTime = DateTimeOffset.Parse(dt.Value)
            }))
            .GroupBy(s => s.SupplyDateTime)
            .OrderByDescending(s => s.Key)
            .ToDictionary(key => key.Key,
                val => val.Select(s => s.Item));
    }

    public IDictionary<Manufacturer, Item> GetTheMostExpensiveItemsPerManufacturer()
    {
        return _itemRepo.Query()
            .GroupBy(xItem => xItem.ToItem().Manufacturer, comparer: _manufacturerComparer)
            .ToDictionary(g => g.Key,
                g => g.Aggregate((i1, i2) => 
                    Double.Parse(i1.Element("PricePerUnit").Value) > Double.Parse(i2.Element("PricePerUnit").Value) 
                    ? i1 : i2).ToItem());
    }
    
    
}