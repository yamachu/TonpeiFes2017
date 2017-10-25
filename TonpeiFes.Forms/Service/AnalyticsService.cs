using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Analytics;
using TonpeiFes.MobileCore.Services;

namespace TonpeiFes.Forms.Service
{
    public class AnalyticsService : IAnalyticsService
    {
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
