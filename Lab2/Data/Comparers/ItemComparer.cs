using Data.Entities;

namespace Data.Comparers;

public class ItemComparer : IEqualityComparer<Item?>
{
    public bool Equals(Item? x, Item? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id 
               && x.Name == y.Name
               && x.ManufacturerId == y.ManufacturerId 
               && Equals(x.Manufacturer, y.Manufacturer) 
               && x.ItemCategories.Equals(y.ItemCategories);
    }

    public int GetHashCode(Item obj)
    {
        return HashCode.Combine(obj.Id, obj.Name, obj.PricePerUnit, obj.Amount, obj.SupplyDateTimes, obj.ManufacturerId, obj.Manufacturer, obj.ItemCategories);
    }
}