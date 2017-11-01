using System;
using System.Threading.Tasks;

namespace TonpeiFes.MobileCore.Services
{
    public interface IAnalyticsService
    {
        Task SendUserAttributes(string age, string member, string residence, string _where, string access);
        Task SendFavoritedStage(string id, string name);
        Task SendFavoritedStall(string id, string name);
        Task SendFavoritedExhibition(string id, string name);
        Task<string> GetUserId();
    }
}
