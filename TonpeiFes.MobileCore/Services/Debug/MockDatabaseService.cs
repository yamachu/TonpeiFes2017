using System;
using System.Threading.Tasks;

namespace TonpeiFes.MobileCore.Services.Debug
{
    public class MockDatabaseService : IDatabaseService
    {
        public async Task<bool> InitializeDatabaseConnection()
        {
            return true;
        }
    }
}
