using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public interface IGroupable
    {
        MyGroupHeader IconedGroupHeader { get; }
        string GroupHeader { get; }
    }
}
