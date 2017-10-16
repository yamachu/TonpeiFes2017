using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public class EventDate : Realms.RealmObject
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
