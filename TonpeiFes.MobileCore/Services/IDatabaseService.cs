using System;
using System.Threading.Tasks;

namespace TonpeiFes.MobileCore.Services
{
    public interface IDatabaseService
    {
        Task<bool> InitializeDatabaseConnection();
    }
}
