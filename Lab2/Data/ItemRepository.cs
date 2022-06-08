using System.Xml;
using Data.Entities;
using System.Xml.Linq;
using System.Xml.Serialization;
using Data.MapExtension;

namespace Data;

public class ItemRepository
{
    private readonly XmlDocument _xmlDoc;
    private readonly XDocument _xDoc;
    private readonly string _filePath;
    public ItemRepository(string filePath)
    {
        _filePath = filePath;
        _xmlDoc = new XmlDocument();
        _xmlDoc.Load(_filePath);
        _xDoc = XDocument.Load(_filePath);
    }
    
    public IEnumerable<Item> GetAll()
    {
        var xmlItems = _xmlDoc.GetElementsByTagName("Item");
        List<Item> result = new List<Item>();
        foreach (XmlElement itemNode in xmlItems)
        {
            var item = new Item();
            item.Id = Int32.Parse(itemNode.Attributes.GetNamedItem("Id").Value);
            item.Name = itemNode.Attributes.GetNamedItem("Name").Value;
            foreach (XmlNode childNode in itemNode.ChildNodes)
            {
                if (childNode.Name == "Amount")
                {
                    item.Amount = UInt32.Parse(childNode.InnerText);
                }
                if (childNode.Name == "PricePerUnit")
                {
                    item.PricePerUnit = Double.Parse(childNode.InnerText);
                }
                if (childNode.Name == "SupplyDateTimes")
                {
                    foreach (XmlNode dateTimeNode in childNode.ChildNodes)
                    {
                        item.SupplyDateTimes.Add(DateTimeOffset.Parse(dateTimeNode.InnerText));
                    }
                }
                if (childNode.Name == "ManufacturerId")
                {
                    item.ManufacturerId = Int32.Parse(childNode.InnerText);
                }
                if (childNode.Name == "Manufacturer")
                {
                    item.Manufacturer = new Manufacturer()
                    {
                        Name = childNode.Attributes.GetNamedItem("Name").Value
                    };
                }
                if (childNode.Name == "ItemCategories")
                {
                    foreach (XmlNode categoryNode in childNode.ChildNodes)
                    {
                        item.ItemCategories.Add(
                            new ItemCategory()
                            {
                                Name = categoryNode.Attributes.GetNamedItem("Name").Value
                            });
                    }
                }
            }
            result.Add(item);
        }
        return result;
    }

    public IEnumerable<XElement> Query()
    {
        return _xDoc.Element("Items")!.Elements("Item");
    }
    public void Create(IEnumerable<Item> items)
    {
        XmlWriterSettings settings = new XmlWriterSettings() {Indent = true};
        using (XmlWriter writer = XmlWriter.Create(_filePath, settings))
        {
            writer.WriteStartElement("Items");
            foreach (var item in items)
            {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("Id",  item.Id.ToString());
                writer.WriteAttributeString("Name", item.Name);
                writer.WriteElementString("PricePerUnit", item.PricePerUnit.ToString());
                writer.WriteElementString("Amount", item.Amount.ToString());
                
                writer.WriteStartElement("SupplyDateTimes");
                foreach (var dateTime in item.SupplyDateTimes) {
                    writer.WriteElementString("SupplyDateTime", dateTime.ToString());
                }
                writer.WriteEndElement();
                
                writer.WriteStartElement("Manufacturer");
                writer.WriteAttributeString("Id", item.ManufacturerId.ToString());
                writer.WriteAttributeString("Name", item.Manufacturer.Name);
                writer.WriteEndElement();
                
                writer.WriteStartElement("ItemCategories");
                foreach (var category in item.ItemCategories)
                {
                    writer.WriteStartElement("ItemCategory");
                    writer.WriteAttributeString("Name", category.Name);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

    }

    public void Add(Item item)
    {
        XmlNode? root = _xmlDoc.GetElementsByTagName("Items")[0];
        
        // Create item element
        XmlElement itemElement = _xmlDoc.CreateElement("Item");
        
        // Id (attribute)
        XmlAttribute itemIdAttr = _xmlDoc.CreateAttribute("Id");
        XmlText itemIdValue = _xmlDoc.CreateTextNode(item.Id.ToString());
        itemIdAttr.AppendChild(itemIdValue);
        itemElement.Attributes.Append(itemIdAttr);
        
        // Name (attribute)
        XmlAttribute itemNameAttr = _xmlDoc.CreateAttribute("Name");
        XmlText itemNameValue = _xmlDoc.CreateTextNode(item.Name);
        itemNameAttr.AppendChild(itemNameValue);
        itemElement.Attributes.Append(itemNameAttr);
        
        // Price
        XmlElement pricePerUnitElement = _xmlDoc.CreateElement("PricePerUnit");
        XmlText priceValue = _xmlDoc.CreateTextNode(item.PricePerUnit.ToString());
        pricePerUnitElement.AppendChild(priceValue);
        itemElement.AppendChild(pricePerUnitElement);
        
        // Amount
        XmlElement amountElement = _xmlDoc.CreateElement("Amount");
        XmlText amountValue = _xmlDoc.CreateTextNode(item.Amount.ToString());
        amountElement.AppendChild(amountValue);
        itemElement.AppendChild(amountElement);
        
        //SupplyDateTimes
        XmlElement supplyDateTimesElement = _xmlDoc.CreateElement("SupplyDateTimes");
        foreach (var dateTime in item.SupplyDateTimes)
        {
            var supplyDateTimeElement = _xmlDoc.CreateElement("SupplyDateTime");
            var dateTimeValue = _xmlDoc.CreateTextNode(dateTime.ToString());
            supplyDateTimeElement.AppendChild(dateTimeValue);
            supplyDateTimesElement.AppendChild(supplyDateTimeElement);
        }
        itemElement.AppendChild(supplyDateTimesElement);
        
        // Manufacturer
        XmlElement manufacturerElement = _xmlDoc.CreateElement("Manufacturer");
        XmlAttribute manufacturerNameAttr = _xmlDoc.CreateAttribute("Name");
        XmlAttribute manufacturerIdAttr = _xmlDoc.CreateAttribute("Id");
        XmlText manufacturerNameAttrValue = _xmlDoc.CreateTextNode(item.Manufacturer.Name);
        XmlText manufacturerIdAttrValue = _xmlDoc.CreateTextNode(item.ManufacturerId.ToString());
        manufacturerNameAttr.AppendChild(manufacturerNameAttrValue);
        manufacturerIdAttr.AppendChild(manufacturerIdAttrValue);
        manufacturerElement.Attributes.Append(manufacturerNameAttr);
        manufacturerElement.Attributes.Append(manufacturerIdAttr);
        itemElement.AppendChild(manufacturerElement);
        
        // ItemCategories
        XmlElement categoriesElement = _xmlDoc.CreateElement("ItemCategories");
        foreach (var category in item.ItemCategories)
        {
            var categoryElement = _xmlDoc.CreateElement("ItemCategory");
            var categoryNameAttr = _xmlDoc.CreateAttribute("Name");
            XmlText nameAttrValue = _xmlDoc.CreateTextNode(category.Name);
            categoryNameAttr.AppendChild(nameAttrValue);
            categoryElement.Attributes.Append(categoryNameAttr);
            categoriesElement.AppendChild(categoryElement);
        }
        itemElement.AppendChild(categoriesElement);
        
        // Attach item to items element
        root.AppendChild(itemElement);
        _xmlDoc.Save(_filePath);
    }

    public void SaveToXDoc()
    {
        _xDoc.Save(_filePath);
    }
}