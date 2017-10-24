using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TonpeiFes.Forms.Views.Extensions
{
    // http://kamusoft.hatenablog.jp/entry/2017/03/09/154717
    [ContentProperty("Key")]
    public class BindableImageResourceExtension : BindableObject, IMarkupExtension<BindingBase>
    {
        public string Key { get; set; }
        public string Param { get; set; }

        public static BindableProperty InnerParamProperty =
            BindableProperty.Create(
                nameof(InnerParam),
                typeof(object),
                typeof(BindableImageResourceExtension),
                default(object),
             defaultBindingMode: BindingMode.OneWay
            );

        public object InnerParam
        {
            get { return (object)GetValue(InnerParamProperty); }
            set { SetValue(InnerParamProperty, value); }
        }

        public static BindableProperty BindImageSourceProperty =
            BindableProperty.Create(
                nameof(BindImageSource),
                typeof(ImageSource),
                typeof(BindableImageResourceExtension),
                default(ImageSource),
             defaultBindingMode: BindingMode.OneWay
            );

        public ImageSource BindImageSource
        {
            get { return (ImageSource)GetValue(BindImageSourceProperty); }
            set { SetValue(BindImageSourceProperty, value); }
        }

        BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        {
            //呼び出し元を取得
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var targetObject = (BindableObject)target.TargetObject;

            EventHandler bindingChanged = null;
            bindingChanged = (sender, e) => {
                BindingContext = targetObject.BindingContext;
                //メモリーリークが怖いのでBindingContext拾ったら即解除
                //2回目以降の変更は反映されないがよしとする
                targetObject.BindingContextChanged -= bindingChanged;
            };

            //この時点ではBindingContextが設定されてないのでイベントで待ち受ける
            targetObject.BindingContextChanged += bindingChanged;

            if (!string.IsNullOrEmpty(Param))
            {
                //内部用のプロパティにバインディング
                this.SetBinding(InnerParamProperty, Param);
            }

            //自身のTranslateプロパティへのBindingを返す
            return new Binding(BindImageSourceProperty.PropertyName, BindingMode.Default, null, null, null, this);
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == InnerParamProperty.PropertyName)
            {
                UpdateImageSource();
            }

            base.OnPropertyChanged(propertyName);
        }

        void UpdateImageSource()
        {
            switch(this.Key)
            {
                case "Resource":
                    this.BindImageSource = ImageSource.FromResource(InnerParam as string);
                    break;
                case "Uri":
                    this.BindImageSource = new UriImageSource
                    {
                        Uri = new Uri(InnerParam as string),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(30, 0, 0, 0)
                    };
                    break;
            }
        }
    }
}
