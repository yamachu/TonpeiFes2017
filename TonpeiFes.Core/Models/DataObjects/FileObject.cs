using System;
using Realms;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class FileObject : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Indexed]
        public string OriginalName { get; set; }

        public string IconOptimizedFileUrl { get; set; }

        public string DescriptionOptimizedFileUrl { get; set; }
    }
}
