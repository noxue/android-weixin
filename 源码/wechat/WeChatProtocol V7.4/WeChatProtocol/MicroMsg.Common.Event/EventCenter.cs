namespace MicroMsg.Common.Event
{
    using MicroMsg.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class EventCenter
    {
        private static object lockEventMap = new object();
        private static Dictionary<EventConst, EventObject> mapEventObject = new Dictionary<EventConst, EventObject>();
        private const string TAG = "EventCenter";

        private static void onDispatchEvent(EventObject evtObject)
        {
            BaseEventArgs evtArgs = null;
            List<EventWatcher> list = null;
            lock (lockEventMap)
            {
                evtArgs = evtObject.args;
                evtObject.args = null;
                list = new List<EventWatcher>(evtObject.listEventWatcher);
            }
            foreach (EventWatcher watcher in list)
            {
                watcher.doInvoke(evtArgs);
            }
        }

        public static bool postCombineEvent(EventConst eventID, object obj)
        {
            lock (lockEventMap)
            {
                if (!mapEventObject.ContainsKey(eventID))
                {
                    return false;
                }
                EventObject obj2 = mapEventObject[eventID];
                if (obj2.args == null)
                {
                    obj2.args = new BaseEventArgs(eventID, new List<object>());
                    //Deployment.get_Current().get_Dispatcher().BeginInvoke(new DispatcherEventDelegate(EventCenter.onDispatchEvent), new object[] { obj2 });
                    DispatcherEventDelegate dde = new DispatcherEventDelegate(EventCenter.onDispatchEvent);
                    dde.BeginInvoke(obj2, null, null);
                }
                ((List<object>) obj2.args.mObject).Add(obj);
                return true;
            }
        }

        public static bool postEvent(EventConst eventID, object param1 = null, object param2 = null)
        {
            lock (lockEventMap)
            {
                if (!mapEventObject.ContainsKey(eventID))
                {
                    return false;
                }
                EventObject obj2 = mapEventObject[eventID];
                return obj2.beginInvoke(new BaseEventArgs(eventID, param1, param2));
            }
        }

        public static void printfInfo()
        {
            lock (lockEventMap)
            {
                foreach (KeyValuePair<EventConst, EventObject> pair in mapEventObject)
                {
                    EventConst key = pair.Key;
                    EventObject obj2 = pair.Value;
                    if (obj2 != null)
                    {
                        obj2.printInfo();
                    }
                }
            }
        }

        public static bool registerEventHandler(EventConst eventID, EventHandlerDelegate eventHandler)
        {
            EventWatcher eventWatcher = new EventWatcher(null, null, eventHandler);
            return registerEventWatcher(eventID, eventWatcher);
        }

        public static bool registerEventWatcher(EventConst eventID, EventWatcher eventWatcher)
        {
            if ((eventWatcher == null) || (eventWatcher.mHandler == null))
            {
                Log.e("EventCenter", "register event id without watcher, ignored.  eventid = " + eventID);
                return false;
            }
            lock (lockEventMap)
            {
                EventObject obj2;
                if (mapEventObject.ContainsKey(eventID))
                {
                    obj2 = mapEventObject[eventID];
                }
                else
                {
                    obj2 = new EventObject(eventID);
                    mapEventObject.Add(eventID, obj2);
                }
                return obj2.addWatcher(eventWatcher);
            }
        }

        public static bool registerEventWatcher(EventConst eventID, string watchName, EventHandlerDelegate eventHandler)
        {
            EventWatcher eventWatcher = new EventWatcher(watchName, eventHandler);
            return registerEventWatcher(eventID, eventWatcher);
        }

        public static bool removeEventHandler(EventConst eventID, EventHandlerDelegate eventHandler)
        {
            lock (lockEventMap)
            {
                if ((eventHandler == null) || !mapEventObject.ContainsKey(eventID))
                {
                    Log.e("EventCenter", "remove event handler failed,  eventid= " + eventID);
                    return false;
                }
                EventObject obj2 = mapEventObject[eventID];
                bool flag = obj2.removeWatcher(eventHandler);
                if (obj2.getCount() == 0)
                {
                    mapEventObject.Remove(eventID);
                }
                return flag;
            }
        }

        public static bool removeEventWatcher(EventConst eventID, EventWatcher watcher)
        {
            lock (lockEventMap)
            {
                if ((watcher == null) || !mapEventObject.ContainsKey(eventID))
                {
                    Log.e("EventCenter", "remove event watcher failed,  eventid= " + eventID);
                    return false;
                }
                EventObject obj2 = mapEventObject[eventID];
                bool flag = obj2.removeWatcher(watcher);
                if (obj2.getCount() == 0)
                {
                    mapEventObject.Remove(eventID);
                }
                return flag;
            }
        }

        public static bool removeEventWatcher(EventConst eventID, string watchName)
        {
            lock (lockEventMap)
            {
                if (!mapEventObject.ContainsKey(eventID))
                {
                    Log.e("EventCenter", "remove event handler failed,  eventid= " + eventID);
                    return false;
                }
                EventObject obj2 = mapEventObject[eventID];
                bool flag = obj2.removeWatcher(watchName);
                if (obj2.getCount() == 0)
                {
                    mapEventObject.Remove(eventID);
                }
                return flag;
            }
        }

        public static bool sendEvent(EventConst eventID, object obj = null)
        {
            //if (!DebugEx.IsMainThread())
            //{
            //    return false;
            //}
            List<EventWatcher> list = null;
            lock (lockEventMap)
            {
                if (!mapEventObject.ContainsKey(eventID))
                {
                    Log.i("EventCenter", "post event = " + eventID + ", no watcher.");
                    return false;
                }
                EventObject obj2 = mapEventObject[eventID];
                list = new List<EventWatcher>(obj2.listEventWatcher);
            }
            BaseEventArgs evtArgs = new BaseEventArgs(eventID, obj);
            foreach (EventWatcher watcher in list)
            {
                watcher.doInvoke(evtArgs);
            }
            return true;
        }
    }
}

