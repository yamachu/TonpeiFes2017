using System;
namespace TonpeiFes.Forms.Extensions
{
    public static class ViewModelNameExtensions
    {
        public static string GetViewNameFromRule(this string viewModelName)
        {
            var li = viewModelName.LastIndexOf("ViewModel", StringComparison.Ordinal);
            if (li == -1) throw new ArgumentException($"Naming rule violation -> {viewModelName}");

            return viewModelName.Remove(li);
        }
    }
}
