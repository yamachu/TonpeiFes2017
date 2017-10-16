using System;
using Realms;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class SNSInfomations : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string HomePage { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string YouTube { get; set; }
    }
}
