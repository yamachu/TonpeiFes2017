using Android.Content;
using Android.Views;
using Android.Widget;
using TonpeiFes.Droid.Renderers;
using TonpeiFes.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(TableView), typeof(CustomTableViewRenderer))]
namespace TonpeiFes.Droid.Renderers
{
    // https://forums.xamarin.com/discussion/32379/changing-the-title-color-of-a-tablesection
    public class CustomTableViewRenderer : TableViewRenderer
    {
        protected override TableViewModelRenderer GetModelRenderer(ListView listView, TableView view)
        {
            return new CustomTableViewModelRenderer(Context, listView, view);
        }
    }

    public class CustomTableViewModelRenderer : TableViewModelRenderer
    {
        public CustomTableViewModelRenderer(Context context, ListView listView, TableView view)
            : base(context, listView, view)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            if (GetCellForPosition(position).GetType() != typeof(TextCell)) return view;
            var layout = (LinearLayout)view;
            var text = (TextView)((LinearLayout)((LinearLayout)layout.GetChildAt(0)).GetChildAt(1)).GetChildAt(0);
            text.SetTextColor(((Color)App.Current.Resources["PrimaryText"]).ToAndroid());
            var divider = layout.GetChildAt(1);
            divider.SetBackgroundColor(((Color)App.Current.Resources["Separator"]).ToAndroid());
            var contentView = (LinearLayout)layout.GetChildAt(0);
            contentView.SetBackgroundColor(((Color)App.Current.Resources["WhiteGray"]).ToAndroid());
            return view;
        }
    }
}
