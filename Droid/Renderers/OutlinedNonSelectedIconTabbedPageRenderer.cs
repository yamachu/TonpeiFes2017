using System;
using FormsPlugin.Iconize.Droid;
using TonpeiFes.Droid.Renderers;
using TonpeiFes.Views.Controls;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(OutlinedNonSelectedIconTabbedPage), typeof(OutlinedNonSelectedIconTabbedPageRenderer))]
namespace TonpeiFes.Droid.Renderers
{
    // Android側は使わないのでダミー実装のみ
    public class OutlinedNonSelectedIconTabbedPageRenderer : IconTabbedPageRenderer
    {
    }
}
