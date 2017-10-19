using System;
using System.Reflection;

namespace TonpeiFes.MobileCore
{
    // https://github.com/amay077/XamarinFormsGachiSample2016Winter/blob/master/XamarinFormsGachiSample2016Winter.Core/ViewTypeToViewModelTypeResolver.cs
    public class ViewModelTypeResolver
    {
        private static readonly Assembly LocalAssembly = typeof(ViewModelTypeResolver).GetTypeInfo().Assembly;
        public static Type Resolve(Type viewType)
        {
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            var vmTypeName = $"{viewType.Namespace.Replace("Forms", "MobileCore").Replace("Views", "ViewModels")}.{viewType.Name}ViewModel";
            return LocalAssembly.GetType(vmTypeName);
        }
    }

    public class DesignViewModelTypeResolver
    {
        private static readonly Assembly LocalAssembly = typeof(ViewModelTypeResolver).GetTypeInfo().Assembly;
        public static Type Resolve(Type viewType)
        {
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            var vmTypeName = $"{viewType.Namespace.Replace("Forms", "MobileCore").Replace("Views", "DesignViewModels")}.{viewType.Name}ViewModel";
            return LocalAssembly.GetType(vmTypeName);
        }
    }
}
