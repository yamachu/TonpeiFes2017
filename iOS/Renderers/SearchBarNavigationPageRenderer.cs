using CoreGraphics;
using Foundation;
using TonpeiFes.Forms.Views.Controls;
using TonpeiFes.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SearchBarNavigationPage), typeof(SearchBarNavigationPageRenderer))]
namespace TonpeiFes.iOS.Renderers
{
    // https://stackoverflow.com/questions/29047041/placing-a-searchbar-in-the-top-navigation-bar
    public class SearchBarNavigationPageRenderer : SideBySideButtonItemContentPageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            SetSearchToolbar();
        }

        public override void WillMoveToParentViewController(UIKit.UIViewController parent)
        {
            base.WillMoveToParentViewController(parent);

            if (parent != null)
            {
                parent.NavigationItem.TitleView = NavigationItem.TitleView;
            }
        }

        private void SetSearchToolbar()
        {
            var element = Element as SearchBarNavigationPage;
            if (element == null)
            {
                return;
            }

            var widht = NavigationController.NavigationBar.Bounds.Width;
            var height = NavigationController.NavigationBar.Bounds.Height;
            // 制約のうまい付け方分からないからマジックナンバーで
            var searchBar = new UIStackView(new CGRect(0, 0, widht * 0.80, height));

            searchBar.Alignment = UIStackViewAlignment.Center;
            searchBar.Axis = UILayoutConstraintAxis.Horizontal;
            searchBar.Spacing = 3;
            searchBar.Distribution = UIStackViewDistribution.Fill;
            searchBar.WidthAnchor.ConstraintEqualTo(searchBar.Bounds.Width).Active = true;


            var searchTextField = new UITextField(searchBar.Bounds);
            searchTextField.BackgroundColor = UIColor.FromRGB(239, 239, 239);
            NSAttributedString strAttr = new NSAttributedString("キーワード検索", foregroundColor: UIColor.FromRGB(146, 146, 146));
            searchTextField.AttributedPlaceholder = strAttr;
            searchTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center;

            // Delete button
            UIButton textDeleteButton = new UIButton(new CGRect(0, 0, searchTextField.Frame.Size.Height + 5, searchTextField.Frame.Height));
            textDeleteButton.Font = UIFont.FromName("Ionicons", 18f);
            textDeleteButton.BackgroundColor = UIColor.Clear;
            textDeleteButton.SetTitleColor(UIColor.FromRGB(146, 146, 146), UIControlState.Normal);
            textDeleteButton.SetTitle("\uf406", UIControlState.Normal);
            textDeleteButton.TouchUpInside += (sender, e) =>
            {
                searchTextField.Text = string.Empty;
                element.SetValue(SearchBarNavigationPage.SearchTextProperty, searchTextField.Text);
            };

            searchTextField.RightView = textDeleteButton;
            searchTextField.RightViewMode = UITextFieldViewMode.Always;
            
            // Border
            searchTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            searchTextField.Layer.BorderColor = UIColor.FromRGB(239, 239, 239).CGColor;
            searchTextField.Layer.BorderWidth = 1;
            searchTextField.Layer.CornerRadius = 5;
            searchTextField.EditingChanged += (sender, e) =>
            {
                element.SetValue(SearchBarNavigationPage.SearchTextProperty, searchTextField.Text);
            };

            searchBar.AddArrangedSubview(searchTextField);

            NavigationItem.TitleView = searchBar;
            NavigationItem.TitleView.Frame = searchBar.Frame;

            if (ParentViewController != null)
            {
                ParentViewController.NavigationItem.TitleView = NavigationItem.TitleView;
                ParentViewController.NavigationItem.TitleView.Frame = NavigationItem.TitleView.Frame;
            }
        }
    }
}