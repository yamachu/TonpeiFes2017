using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public interface IDescription
    {
        string Title { get; }
        string Detail { get; }
        FileObject AttachFile { get; }
    }
}
