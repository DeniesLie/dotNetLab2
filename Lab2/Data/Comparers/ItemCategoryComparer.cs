using Data.Entities;

namespace Data.Comparers;

public class ItemCategoryComparer : IEqualityComparer<ItemCategory?>
{
    public bool Equals(ItemCategory? x, ItemCategory? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name;
    }

    public int GetHashCode(ItemCategory obj)
    {
        return obj.Name.GetHashCode();
    }
}