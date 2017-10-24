using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.Core.Models.Consts;
using System.Linq;

namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockMapRegionRepository : IRepository<MapRegion>
    {
        private static List<MapRegion> Source;

        public MockMapRegionRepository()
        {
            Source = new List<MapRegion>();

            var so = new MapRegion { Name = "A棟", MapObjectType = (MapObjectEnum.EXHIBITION | MapObjectEnum.TOILET), ContainsPlannings = true };
            so.Points.Add(new Point(38.259759, 140.850935));
            so.Points.Add(new Point(38.259623, 140.850996));
            so.Points.Add(new Point(38.259766, 140.851731));
            so.Points.Add(new Point(38.259981, 140.851669));

            Source.Add(so);

            so = new MapRegion { Name = "A棟前", MapObjectType = MapObjectEnum.STALL, ContainsPlannings = true };
            so.Points.Add(new Point(38.259682, 140.852219));
            so.Points.Add(new Point(38.259707, 140.852299));
            so.Points.Add(new Point(38.260067, 140.852130));
            so.Points.Add(new Point(38.260033, 140.852020));

            var tmp = new MapRegion { Name = "A棟前", MapObjectType = MapObjectEnum.STALL, ContainsPlannings = false };
            tmp.Points.Add(new Point(38.259650, 140.851902));
            tmp.Points.Add(new Point(38.259669, 140.851974));
            tmp.Points.Add(new Point(38.259473, 140.852081));
            tmp.Points.Add(new Point(38.259450, 140.851990));

            Source.Add(tmp);
            so.ChildMapRegion.Add(tmp);

            Source.Add(so);
        }

        public void Add(MapRegion item)
        {
            throw new NotImplementedException();
        }

        public void Delete(MapRegion item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MapRegion> GetAll()
        {
            return Source;
        }

        public MapRegion GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public MapRegion GetOne(string id)
        {
            return Source.FirstOrDefault(item => item.Id == id);
        }
    }
}
