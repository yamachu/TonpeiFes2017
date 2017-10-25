using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Practices.Unity;
using Prism.Events;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Models.EventArgs;
using TonpeiFes.MobileCore.ViewModels.Pages;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.Forms.Views.Pages
{
    public partial class FestaMapRootPage : ContentPage
    {
        public FestaMapRootPage()
        {
            InitializeComponent();
        }
    }
}
