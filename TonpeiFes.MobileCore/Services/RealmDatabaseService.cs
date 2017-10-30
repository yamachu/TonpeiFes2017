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

        public async Task<bool> InitializeDatabaseConnection(bool force = false)
        {
            if (IsInitialized && !force) return true;

            if (!force)
            {
                LocalDataConnectionConfiguration = new RealmConfiguration();
                LocalDataConnectionConfiguration.ObjectClasses = new[] { typeof(Core.Models.DataObjects.FavoritedPlanning) };
            }

            User user = null;
            try
            {
                user = User.Current;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Cannot BIND User");
                System.Diagnostics.Debug.Write(e.StackTrace);
            }

            try
            {
                if (user == null && !force)
                {
                    var credentials = Credentials.UsernamePassword(_consts.UserId , _consts.Password, createUser: false);
                    user = await User.LoginAsync(credentials, new Uri(_consts.AuthUrl));
                }
                if (!force)
                {
                    MasterDataConnectionConfiguration = new SyncConfiguration(user, new Uri(_consts.DbUri));
                }
                if (user != null && force)
                {
                    //user.LogOut();
                    //var credentials = Credentials.UsernamePassword(_consts.UserId, _consts.Password, createUser: false);
                    //user = await User.LoginAsync(credentials, new Uri(_consts.AuthUrl));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Configuration setup failed");
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return false;
            }
            IsInitialized = true;
            return true;
        }
    }
}
