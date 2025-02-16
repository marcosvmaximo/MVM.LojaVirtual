namespace MVM.LojaVirtual.Core.Domain;

public abstract class Entity
{
    public Guid Id { get; set; }

    public override bool Equals(object? obj)
    {
        var compareToo = obj as Entity;

        if (ReferenceEquals(this, compareToo)) return true;
        if (ReferenceEquals(null, compareToo)) return false;
        
        return Id.Equals(compareToo.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
    
    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }
}