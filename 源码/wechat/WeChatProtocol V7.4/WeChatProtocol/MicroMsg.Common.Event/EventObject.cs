namespace MicroMsg.Common.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventObject
    {
        public BaseEventArgs args;
        public List<EventWatcher> listEventWatcher = new List<EventWatcher>();
        public EventConst mID;

        public EventObject(EventConst id)
        {
            this.mID = id;
        }

        public bool addWatcher(EventWatcher eventWatcher)
        {
            if ((eventWatcher == null) || (eventWatcher.mHandler == null))
            {
                return false;
            }
            if (this.listEventWatcher.Contains(eventWatcher))
            {
                return false;
            }
            this.listEventWatcher.Add(eventWatcher);
            return true;
        }

        public bool beginInvoke(BaseEventArgs evtArgs)
        {
            foreach (EventWatcher watcher in this.listEventWatcher)
            {
                if (watcher != null)
                {
                    watcher.beginInvoke(evtArgs);
                }
            }
            return true;
        }

        public int getCount()
        {
            return this.listEventWatcher.Count;
        }

        public void printInfo()
        {
            string str = "";
            foreach (EventWatcher watcher in this.listEventWatcher)
            {
                if (watcher != null)
                {
                    str = str + watcher.objName + ":" + watcher.objMethod;
                }
            }
        }

        public bool removeWatcher(EventHandlerDelegate eventHandler)
        {
            if (eventHandler == null)
            {
                return false;
            }
            EventWatcher item = (from e in this.listEventWatcher
                where e.mHandler == eventHandler
                select e).FirstOrDefault<EventWatcher>();
            if (item == null)
            {
                return false;
            }
            return this.listEventWatcher.Remove(item);
        }

        public bool removeWatcher(EventWatcher eventWatcher)
        {
            if (eventWatcher == null)
            {
                return false;
            }
            return this.listEventWatcher.Remove(eventWatcher);
        }

        public bool removeWatcher(string watherName)
        {
            if (string.IsNullOrEmpty(watherName))
            {
                return false;
            }
            EventWatcher item = (from e in this.listEventWatcher
                where e.objName == watherName
                select e).FirstOrDefault<EventWatcher>();
            if (item == null)
            {
                return false;
            }
            return this.listEventWatcher.Remove(item);
        }
    }
}

