using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using TonpeiFes.MobileCore.Configurations;

namespace TonpeiFes.MobileCore.Services
{
    public class RealmDatabaseService : IDatabaseService
    {
        public SyncConfiguration MasterDataConnectionConfiguration { get; private set; }
        public RealmConfiguration LocalDataConnectionConfiguration { get; private set; }
        private bool IsInitialized;

        private IRealmConsts _consts;

        public RealmDatabaseService(IRealmConsts consts)
        {
            _consts = consts;
        }

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
                    var credentials = Credentials.UsernamePassword(_consts.UserId , _consts.Password, createUser: false);
                    user = await User.LoginAsync(credentials, new Uri(_consts.AuthUrl));
                }
                MasterDataConnectionConfiguration = new SyncConfiguration(user, new Uri(_consts.DbUri));
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
