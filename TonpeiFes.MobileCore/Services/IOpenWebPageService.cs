using System;
using System.Threading.Tasks;

namespace TonpeiFes.MobileCore.Services
{
    public interface IOpenWebPageService
    {
        Task OpenUri(string uri);
    }
}
