using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using TonpeiFes.Core.Models.Consts;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class MapRegion : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public IList<Point> Points { get; }

        [Ignored]
        public Point PinPoint
        { 
            get
            {
                if (Points.Count == 0) throw new ArgumentException("Must set more than one Point");
                return Points.Aggregate((acc, next) => acc + next) / (uint)Points.Count;
            }
        }

        private int MapObjectType_ { get; set; }

        [Ignored]
        public MapObjectEnum MapObjectType
        {
            get
            {
                return (MapObjectEnum)Enum.ToObject(typeof(MapObjectEnum), MapObjectType_);
            }
            set
            {
                MapObjectType_ = (int)value;
            }
        }

        public bool ContainsPlannings { get; set; } = true;
    }
}
