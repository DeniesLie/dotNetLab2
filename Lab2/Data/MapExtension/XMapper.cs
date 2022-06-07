using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Data.Entities;

namespace Data.MapExtension;

public static class XMapper
{
    public static Item ToItem(this XElement xElement)
    {
        if (xElement == null) throw new ArgumentNullException(nameof(xElement));

        Item result = new Item();
        
        result.Id = Int32.Parse(xElement.Attribute("Id")!.Value);
        result.Name = xElement.Attribute("Name")!.Value;
        result.Amount = UInt32.Parse(xElement.Element("Amount")!.Value);
        result.PricePerUnit = Double.Parse(xElement.Element("PricePerUnit")!.Value);
        foreach (var xDateTime in xElement.Element("SupplyDateTimes").Elements("SupplyDateTime"))
        {
            result.SupplyDateTimes.Add(DateTimeOffset.Parse(xDateTime.Value));
        }
        var manufacturerId = Int32.Parse(xElement.Element("Manufacturer").Attribute("Id").Value);
        result.ManufacturerId = manufacturerId;
        result.Manufacturer = new Manufacturer()
        {
            Id = manufacturerId,
            Name = xElement.Element("Manufacturer").Attribute("Name").Value
        };
        foreach (var xCategory in xElement.Element("ItemCategories").Elements("ItemCategory"))
        {
            result.ItemCategories.Add(new ItemCategory(){Name = xCategory.Attribute("Name").Value});
        }

        return result;
    }

    public static Manufacturer ToManufacturer(this XElement xElement)
    {
        if (xElement == null) throw new ArgumentNullException(nameof(xElement));

        Manufacturer result = new Manufacturer();
        
        result.Id = Int32.Parse(xElement.Attribute("Id")!.Value);
        result.Name = xElement.Attribute("Name")!.Value;

        return result;
    }

    public static ItemCategory ToItemCategory(this XElement xElement)
    {
        if (xElement == null) throw new ArgumentNullException(nameof(xElement));

        ItemCategory result = new ItemCategory();

        result.Name = xElement.Attribute("Name")!.Value;

        return result;
    }
    
    public static IEnumerable<ItemCategory> ToItemCategories(this IEnumerable<XElement> xElements)
    {
        if (xElements == null) throw new ArgumentNullException(nameof(xElements));

        List<ItemCategory> result = new List<ItemCategory>();

        foreach (var xElement in xElements)
        {
            result.Add(xElement.ToItemCategory());
        }

        return result;
    }
}