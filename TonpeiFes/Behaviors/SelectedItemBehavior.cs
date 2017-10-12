using System;
using System.Windows.Input;
using Prism.Behaviors;
using Xamarin.Forms;

namespace TonpeiFes.Behaviors
{
    // http://www.nuits.jp/entry/item-selected-to-command-behavior
    public class SelectedItemBehavior : BehaviorBase<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SelectedItemBehavior));

        public static readonly BindableProperty ClearSelectedProperty =
            BindableProperty.Create(nameof(Command), typeof(bool), typeof(SelectedItemBehavior), true);

        private ListView AssociatedObject { get; set; }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool ClearSelected
        {
            get => (bool)GetValue(ClearSelectedProperty);
            set => SetValue(ClearSelectedProperty, value);
        }

        protected override void OnAttachedTo(ListView bindableObject)
        {
            base.OnAttachedTo(bindableObject);
            AssociatedObject = bindableObject;
            BindingContext = AssociatedObject.BindingContext;
            bindableObject.ItemSelected += OnItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindableObject)
        {
            bindableObject.ItemSelected -= OnItemSelected;
            AssociatedObject = null;
            BindingContext = null;
            base.OnDetachingFrom(bindableObject);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Command == null || e.SelectedItem == null)
                return;

            if (Command.CanExecute(e.SelectedItem))
            {
                Command.Execute(e.SelectedItem);
            }

            if (ClearSelected)
            {
                AssociatedObject.SelectedItem = null;
            }
        }
    }
}
