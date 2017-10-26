using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Reactive.Disposables;

namespace TonpeiFes.MobileCore.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IDisposable
    {
        protected CompositeDisposable Disposable { get; private set; }

        public ViewModelBase()
        {
            this.Disposable = new CompositeDisposable();
        }

        void IDisposable.Dispose()
        {
            this.Disposable.Dispose();
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}
