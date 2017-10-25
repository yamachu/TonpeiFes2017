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
        private IEventAggregator _eventAggregator;

        public FestaMapRootPage()
        {
            InitializeComponent();

            var container = (Application.Current as App).Container;

            _eventAggregator = container.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<SwitchToClosablePageEvent>().Subscribe((args) =>
            {
                if (args.Name != nameof(FestaMapRootPage) || (BindingContext as FestaMapRootPageViewModel).IsGlobalMap) return;
                var item = new ToolbarItem
                {
                    Priority = 1,
                    Icon = "ic_clear",
                    Command = new Command(async () =>
                    {
                        ToolbarItems.Clear();
                        await Navigation.PopModalAsync();
                    })
                };

                ToolbarItems.Add(item);
            });
        }
    }
}
