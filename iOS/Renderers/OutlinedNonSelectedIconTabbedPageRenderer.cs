using System;
using System.Collections.Generic;
using Plugin.Iconize.iOS;
using TonpeiFes.iOS.Renderers;
using TonpeiFes.Forms.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(OutlinedNonSelectedIconTabbedPage), typeof(OutlinedNonSelectedIconTabbedPageRenderer))]
namespace TonpeiFes.iOS.Renderers
{
    // https://github.com/jsmarcus/Xamarin.Plugins/blob/master/Iconize/FormsPlugin.Iconize.iOS/IconTabbedPageRenderer.cs
    public class OutlinedNonSelectedIconTabbedPageRenderer : TabbedRenderer
    {
        private readonly List<String> _icons = new List<String>();

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="VisualElementChangedEventArgs" /> instance containing the event data.</param>
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            _icons.Clear();
            if (e.NewElement != null)
            {
                foreach (var page in ((TabbedPage)e.NewElement).Children)
                {
                    _icons.Add(page.Icon.File);
                    page.Icon = null;
                }
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Called prior to the <see cref="P:UIKit.UIViewController.View" /> being added to the view hierarchy.
        /// </summary>
        /// <param name="animated">If the appearance will be animated.</param>
        /// <remarks>
        /// <para>This method is called prior to the <see cref="T:UIKit.UIView" /> that is this <see cref="T:UIKit.UIViewController" />’s <see cref="P:UIKit.UIViewController.View" /> property being added to the display <see cref="T:UIKit.UIView" /> hierarchy. </para>
        /// <para>Application developers who override this method must call <c>base.ViewWillAppear()</c> in their overridden method.</para>
        /// </remarks>
        public override void ViewWillAppear(Boolean animated)
        {
            base.ViewWillAppear(animated);

            foreach (var tab in TabBar.Items)
            {
                var defaultIconKey = _icons?[(Int32)tab.Tag] ?? "";
                var defaultIcon = Plugin.Iconize.Iconize.FindIconForKey(defaultIconKey);
                if (defaultIcon == null)
                    continue;

                using (var image = defaultIcon.ToUIImage(25))
                {
                    tab.SelectedImage = image;
                }

                var outlinedIconKey = $"{defaultIconKey}-outline";
                var outlinedIcon = Plugin.Iconize.Iconize.FindIconForKey(outlinedIconKey);
                if (outlinedIcon == null)
                {
                    using (var image = defaultIcon.ToUIImage(25))
                    {
                        tab.Image = image;
                    }
                    continue;
                }

                using (var image = outlinedIcon.ToUIImage(25))
                {
                    tab.Image = image;
                }
            }
        }
    }
}
