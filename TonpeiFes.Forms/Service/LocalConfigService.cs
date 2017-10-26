using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using TonpeiFes.MobileCore.Services;

namespace TonpeiFes.Forms.Service
{
    public class LocalConfigService : ILocalConfigService
    {
        private readonly string FinishEnquete = "FinishEnquete";
        private readonly string UserId = "UserId";

        private ISettings AppSettings;

        public LocalConfigService()
        {
            AppSettings = CrossSettings.Current;
        }

        public async Task<bool> GetEnqueteSentAsync()
        {
            return AppSettings.GetValueOrDefault(FinishEnquete, false);
        }

        public async Task<string> GetUserIdAsync()
        {
            return AppSettings.GetValueOrDefault(UserId, null);
        }

        public async Task SetEnqueteSentAsync(bool sent)
        {
            AppSettings.AddOrUpdateValue(FinishEnquete, sent);
        }

        public async Task SetUserIdAsync(string id)
        {
            AppSettings.AddOrUpdateValue(UserId, id);
        }
    }
}
