namespace MicroMsg.Storage
{
    using System;
    using System.Reflection;

    public abstract class StorageItem
    {
        public long modify;

        public T clone<T>() where T: class
        {
            return (base.MemberwiseClone() as T);
        }

        public void copy(object a)
        {
            foreach (MemberInfo info2 in a.GetType().GetMembers())
            {
                if (info2.MemberType == MemberTypes.Field)
                {
                    FieldInfo info = info2 as FieldInfo;
                    if (!(info.Name == "_version") && (info.GetValue(this) != info.GetValue(a)))
                    {
                        info.SetValue(this, info.GetValue(a));
                    }
                }
            }
        }

        public virtual void merge(object o)
        {
            this.copy(o);
        }
    }
}

