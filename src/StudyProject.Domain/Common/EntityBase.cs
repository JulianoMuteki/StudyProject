using System;

namespace StudyProject.Domain.Common
{
    public abstract class EntityBase
    {
        public Guid ID { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime DateModified { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return ID.Equals(compareTo.ID);
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + ID.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + ID + "]";
        }
    }
}