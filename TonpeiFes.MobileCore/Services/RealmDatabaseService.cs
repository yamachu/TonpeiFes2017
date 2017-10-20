using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;

namespace TonpeiFes.MobileCore.Services
{
    public class RealmDatabaseService : IDatabaseService
    {
        public SyncConfiguration MasterDataConnectionConfiguration { get; private set; }
        public RealmConfiguration LocalDataConnectionConfiguration { get; private set; }
        private bool IsInitialized;

        public async Task<bool> InitializeDatabaseConnection()
        {
            if (IsInitialized) return true;

            User user = null;
            try
            {
                user = User.Current;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
            }

            try
            {
                if (user == null)
                {
                    var credentials = Credentials.UsernamePassword("Username", "Password", createUser: false);
                    user = await User.LoginAsync(credentials, new Uri($"http://serverAddress"));
                }
                MasterDataConnectionConfiguration = new SyncConfiguration(user, new Uri($"realm://serverAddress/~/realmtasks"));
                await Realm.GetInstanceAsync(MasterDataConnectionConfiguration);
                LocalDataConnectionConfiguration = new RealmConfiguration();
                LocalDataConnectionConfiguration.ObjectClasses = new[] { typeof(Core.Models.DataObjects.FavoritedPlanning) };
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return false;
            }
            IsInitialized = true;
            return true;
        }
    }
}
