using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(SideBySideButtonItemContentPageRenderer))]
namespace TonpeiFes.iOS.Renderers
{
    // http://mikazuki.hatenablog.jp/entry/2015/12/14/023000
    public class SideBySideButtonItemContentPageRenderer : PageRenderer
    {
        public bool IsLeftItemUpdated = false;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (NavigationController == null) return;

            var barItems = (this.Element as ContentPage).ToolbarItems.OrderBy(w => w.Priority);
            var items = this.NavigationController.TopViewController.NavigationItem;

            var rightItems = new List<UIBarButtonItem>();
            var leftItems = items.LeftBarButtonItems?.ToList() ?? new List<UIBarButtonItem>();
            foreach (var item in barItems)
            {
                if (item.Priority < 0)
                {
                    leftItems.Add(item.ToUIBarButtonItem());
                }
                else
                {
                    rightItems.Add(item.ToUIBarButtonItem());
                }
            }

            if (!IsLeftItemUpdated)
            {
                items.SetLeftBarButtonItems(leftItems.ToArray(), animated);
                IsLeftItemUpdated = true;
            }
            items.SetRightBarButtonItems(rightItems.ToArray(), animated);
        }
    }
}
