using System;
using Xamarin.Forms;

namespace TonpeiFes.Views.Controls
{
    /// <summary>
    /// DataTemplateSelector の基底クラス
    /// </summary>
    /// http://matatabi-ux.hateblo.jp/entry/2015/01/30/120000
    public class DataTemplateSelector
    {
        /// <summary>
        /// テンプレートを選択する
        /// </summary>
        /// <param name="item">アイテムのデータソース</param>
        /// <param name="container">アイテムのコンテナ</param>
        /// <param name="index">アイテムのインデックス</param>
        /// <returns>選択されたテンプレート</returns>
        public virtual DataTemplate SelectTemplate(object item, BindableObject container, int? index = null)
        {
            return null;
        }
    }
}
