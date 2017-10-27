using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public class StageEventDescription : Realms.RealmObject, IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }

        public FileObject AttachFile { get; set; }
    }
}
