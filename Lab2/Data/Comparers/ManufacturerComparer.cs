using Data.Entities;

namespace Data.Comparers;

public class ManufacturerComparer : IEqualityComparer<Manufacturer?>
{
    public bool Equals(Manufacturer? x, Manufacturer? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Name == y.Name;
    }

    public int GetHashCode(Manufacturer obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        return HashCode.Combine(obj.Id, obj.Name);
    }
}