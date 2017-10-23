using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Controls
{
    /// <summary>
    /// ItemsControl 風 View
    /// </summary>
    /// http://matatabi-ux.hateblo.jp/entry/2015/01/30/120000
    /// http://kmycode.hatenablog.jp/entry/2017/03/26/112826
    public class ItemsControl : ContentView
    {
        #region ItemsPanel

        /// <summary>
        /// ItemsPanel BindableProperty
        /// </summary>
        public static readonly BindableProperty ItemsPanelProperty = BindableProperty.Create(
            nameof(ItemsPanel),
            typeof(Layout<View>),
            typeof(ItemsControl),
            null,
            BindingMode.OneWay,
            null,
            OnItemsPanelChanged);

        /// <summary>
        /// ItemsPanel CLR プロパティ
        /// </summary>
        public Layout<View> ItemsPanel
        {
            get { return (Layout<View>)this.GetValue(ItemsPanelProperty); }
            set { this.SetValue(ItemsPanelProperty, value); }
        }

        /// <summary>
        /// ItemsPanel 変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">古い値</param>
        /// <param name="newValue">新しい値</param>
        private static void OnItemsPanelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as ItemsControl;
            if (control == null)
            {
                return;
            }

            if ((oldValue as Layout<View>) != null)
            {
                (oldValue as Layout<View>).Children.Clear();
            }

            if ((newValue as Layout<View>) == null)
            {
                return;
            }

            control.ItemsRender();
            control.Content = newValue as Layout<View>;
            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }

        #endregion //ItemsPanel

        #region ItemsSource

        /// <summary>
        /// ItemsSource BindableProperty
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(ItemsControl),
            new ObservableCollection<object>(),
            BindingMode.OneWay,
            null,
            OnItemsSourceChanged);

        /// <summary>
        /// ItemsSource CLR プロパティ
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemsSource 変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">古い値</param>
        /// <param name="newValue">新しい値</param>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as ItemsControl;
            if (control == null)
            {
                return;
            }

            var oldCollection = oldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= control.OnCollectionChanged;
            }

            if (newValue == null)
            {
                return;
            }

            control.ItemsRender();

            var newCollection = newValue as INotifyCollectionChanged;
            if (newCollection != null)
            {
                newCollection.CollectionChanged += control.OnCollectionChanged;
            }

            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }

        #endregion //ItemsSource

        #region ItemTemplate

        /// <summary>
        /// ItemTemplate BindableProperty
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ItemsControl),
            default(DataTemplate));

        /// <summary>
        /// ItemTemplate CLR プロパティ
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        #endregion //ItemTemplate

        #region ItemTemplateSelector

        /// <summary>
        /// ItemTemplate BindableProperty
        /// </summary>
        public static readonly BindableProperty ItemTemplateSelectorProperty = BindableProperty.Create(
            nameof(ItemTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(ItemsControl),
            default(DataTemplateSelector),
            BindingMode.OneWay,
            null,
            OnItemTemplateSelectorChanged);

        /// <summary>
        /// ItemTemplate CLR プロパティ
        /// </summary>
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)this.GetValue(ItemTemplateSelectorProperty); }
            set { this.SetValue(ItemTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// ItemTemplateSelector 変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">古い値</param>
        /// <param name="newValue">新しい値</param>
        private static void OnItemTemplateSelectorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as ItemsControl;
            if (control == null)
            {
                return;
            }

            control.ItemsRender();
        }

        #endregion //ItemTemplate

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemsControl()
        {
            this.ItemsPanel = new StackLayout();
            this.ItemsSource = new ObservableCollection<object>();

            this.Content = this.ItemsPanel;
        }

        /// <summary>
        /// アイテムの表示更新
        /// </summary>
        private void ItemsRender()
        {
            this.ItemsPanel.Children.Clear();

            var index = 0;
            foreach (var item in this.ItemsSource)
            {
                var template = this.ItemTemplateSelector != null
                    ? this.ItemTemplateSelector.SelectTemplate(item, null, index)
                    : this.ItemTemplate;
                var content = template.CreateContent();
                View view;
                var cell = content as ViewCell;
                if (cell != null)
                {
                    view = cell.View;
                }
                else
                {
                    view = (View)content;
                }
                view.BindingContext = item;
                this.ItemsPanel.Children.Add(view);
                index++;
            }
        }

        /// <summary>
        /// Items の変更イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                this.ItemsPanel.Children.RemoveAt(e.OldStartingIndex);
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }

            var collection = this.ItemsSource as IEnumerable<object>;
            if (e.NewItems == null || collection == null)
            {
                return;
            }
            foreach (var item in e.NewItems)
            {
                var content = this.ItemTemplate.CreateContent();

                View view;
                var cell = content as ViewCell;
                if (cell != null)
                {
                    view = cell.View;
                }
                else
                {
                    view = (View)content;
                }

                view.BindingContext = item;

                int itemIndex = 0;
                foreach (var collectionItem in collection)
                {
                    if (collectionItem == item)
                    {
                        break;
                    }
                    itemIndex++;
                }

                view.BindingContext = item;
                this.ItemsPanel.Children.Insert(itemIndex, view);
            }

            this.UpdateChildrenLayout();
            this.InvalidateLayout();
        }
    }
}

