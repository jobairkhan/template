namespace Template.Infrastructure
{
    public abstract class Entity
    {
        protected Entity() : this(0) { }

        protected Entity(long id)
        {
            Id = id;
        }

        public virtual long Id { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (GetRealType() != other.GetRealType()) return false;

            if (Id == 0 || other.Id == 0) return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType() + Id).GetHashCode();
        }

        private string GetRealType()
        {
            return GetType().Name;
        }
    }
}
