using System;
using System.Threading.Tasks;

namespace TonpeiFes.MobileCore.Services
{
    public interface ILocalConfigService
    {
        Task SetEnqueteSentAsync(bool sent);
        Task<bool> GetEnqueteSentAsync();
        Task SetUserIdAsync(string id);
        Task<string> GetUserIdAsync();
    }
}
