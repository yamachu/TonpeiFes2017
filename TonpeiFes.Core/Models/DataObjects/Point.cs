using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public class Point : Realms.RealmObject
    {
        public double Langitude { get; set; }
        public double Longitude { get; set; }

        public static Point operator +(Point a, Point b)
        {
            return new Point
            {
                Langitude = a.Langitude + b.Langitude,
                Longitude = a.Longitude + b.Longitude
            };
        }

        public static Point operator /(Point a, uint div)
        {
            return new Point
            {
                Langitude = a.Langitude / div,
                Longitude = a.Longitude / div
            };
        }
    }
}
