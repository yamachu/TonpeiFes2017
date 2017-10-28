using System;
namespace TonpeiFes.MobileCore.Configurations
{
    // 今回は共通ユーザを使うことでデータベースを参照できるようにしている
    // ToDo: 本来の安全な使い方ではないので，もっといい方法を探す
    public interface IRealmConsts
    {
        string UserId { get; }
        string Password { get; }
        string AuthUrl { get; }
        string DbUri { get; }
    }
}
