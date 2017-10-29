using System;
using System.Threading.Tasks;
using TonpeiFes.MobileCore.Services;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Service
{
    public class OpenWebPageService : IOpenWebPageService
    {
        public async Task OpenUri(string uri)
        {
            try
            {
                var _uri = uri.StartsWith("'@") ? new Uri($"https://twitter.com/{(uri.Substring(2))}")
                              : (uri.StartsWith("@") ? new Uri($"https://twitter.com/{(uri.Substring(1))}")
                                 :new Uri(uri));
                Device.OpenUri(_uri);
            }
            catch(System.UriFormatException ex)
            {
                
            }
            catch(System.Exception ex)
            {
                
            }
        }
    }
}
