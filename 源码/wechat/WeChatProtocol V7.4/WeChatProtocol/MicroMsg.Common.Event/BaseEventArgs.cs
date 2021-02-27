namespace MicroMsg.Common.Event
{
    using System;
    using System.Collections.Generic;

    public sealed class BaseEventArgs
    {
        public EventConst mEventID;
        public object mObject;
        public object mObject1;

        public BaseEventArgs()
        {
        }

        public BaseEventArgs(EventConst id, object obj)
        {
            this.mEventID = id;
            this.mObject = obj;
        }

        public BaseEventArgs(EventConst id, object obj, object obj1)
        {
            this.mEventID = id;
            this.mObject = obj;
            this.mObject1 = obj1;
        }

        public bool checkType<T>()
        {
            if (this.mObject == null)
            {
                return false;
            }
            return (this.mObject is T);
        }

        public List<T> getListObject<T>()
        {
            if (!this.checkType<List<object>>())
            {
                return null;
            }
            List<object> mObject = this.mObject as List<object>;
            if ((mObject == null) || (mObject.Count <= 0))
            {
                return null;
            }
            List<T> list2 = new List<T>();
            foreach (object obj2 in mObject)
            {
                if (obj2 is T)
                {
                    list2.Add((T) obj2);
                }
            }
            return list2;
        }
    }
}

