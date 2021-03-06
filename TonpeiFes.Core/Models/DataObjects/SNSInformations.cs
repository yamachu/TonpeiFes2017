﻿using System;
using Realms;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class SNSInformations : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string HomePage { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string YouTube { get; set; }
    }
}
