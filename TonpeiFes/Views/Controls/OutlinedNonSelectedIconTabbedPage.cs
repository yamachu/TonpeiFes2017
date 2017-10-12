using System;
using FormsPlugin.Iconize;
using Xamarin.Forms;

namespace TonpeiFes.Views.Controls
{
    // https://github.com/jsmarcus/Xamarin.Plugins/blob/master/Iconize/FormsPlugin.Iconize/IconTabbedPage.cs
    public class OutlinedNonSelectedIconTabbedPage : TabbedPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutlinedNonSelectedIconTabbedPage" /> class.
        /// </summary>
        public OutlinedNonSelectedIconTabbedPage()
        {
            CurrentPageChanged += OnCurrentPageChanged;
        }

        /// <summary>
        /// Called when [current page changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCurrentPageChanged(Object sender, EventArgs e)
        {
            MessagingCenter.Send(sender, IconToolbarItem.UpdateToolbarItemsMessage);
        }
    }
}
