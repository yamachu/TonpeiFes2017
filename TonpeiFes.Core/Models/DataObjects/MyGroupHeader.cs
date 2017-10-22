using System;
using Realms;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class MyGroupHeader : RealmObject, IComparable
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Key { get; set; }

        public string Source { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            return (obj as MyGroupHeader)?.Key.CompareTo(this.Key)
                                          ?? throw new ArgumentException("Object is not a MyGroupHeader");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Key == ((obj as MyGroupHeader)?.Key ?? null);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }
    }
}
