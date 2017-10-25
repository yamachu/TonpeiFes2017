using System;
using Realms;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class Announcement : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Index { get; set; } = 0;
        public string Title { get; set; }
        public bool HasContents { get; set; } = false;
        public bool IsOutWebPage { get; set; } = false;
        public string Uri { get; set; }
        public string Contents { get; set; }
    }
}
