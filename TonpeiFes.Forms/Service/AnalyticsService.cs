using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using TonpeiFes.MobileCore.Services;

namespace TonpeiFes.Forms.Service
{
    public class AnalyticsService : IAnalyticsService
    {
        public async Task<string> GetUserId()
        {
            var guid = await AppCenter.GetInstallIdAsync();
            return guid?.ToString() ?? "DUMMY";
        }

        public async Task SendFavoritedExhibition(string id, string name)
        {
            Analytics.TrackEvent("FavoritedExhibition", new Dictionary<string, string>
            {
                { "ID", id },
                { "NAME", name },
            });
        }

        public async Task SendFavoritedStage(string id, string name)
        {
            Analytics.TrackEvent("FavoritedStage", new Dictionary<string, string>
            {
                { "ID", id },
                { "NAME", name },
            });
        }

        public async Task SendFavoritedStall(string id, string name)
        {
            Analytics.TrackEvent("FavoritedStall", new Dictionary<string, string>
            {
                { "ID", id },
                { "NAME", name },
            });
        }

        public async Task SendUserAttributes(string age, string member, string residence, string _where, string access)
        {
            Analytics.TrackEvent("UserAttributes", new Dictionary<string, string>
            {
                { "AGE", age },
                { "MEMBER", member },
                { "RESIDENCE",  residence },
                { "WHERE", _where },
                { "ACCESS", access },
            });
        }
    }
}
