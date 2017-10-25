using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public class IDescriptionImpl : IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }

        public string ImageUrl
        {
            get
            {
                // Dummy impl
                return "";
            }
        }
    }
}
