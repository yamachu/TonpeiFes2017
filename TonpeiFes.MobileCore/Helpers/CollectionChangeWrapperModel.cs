using System;
namespace TonpeiFes.MobileCore.Helpers
{
    public class CollectionChangeWrapperModel<T>
    {
        public CollectionChangeTypeEnum ChangeType { get; private set; }
        public T Value { get; private set; }

        public CollectionChangeWrapperModel(CollectionChangeTypeEnum type, T model)
        {
            ChangeType = type;
            Value = model;
        }
    }
}
