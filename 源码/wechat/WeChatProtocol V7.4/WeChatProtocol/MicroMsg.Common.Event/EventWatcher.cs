namespace MicroMsg.Common.Event
{
    using System;
    using System.Windows;

    public class EventWatcher
    {
        public EventHandlerDelegate mHandler;
        public string objName;
        private const string TAG = "EventWatcher";
        public  delegate void beingInvokeMethod(BaseEventArgs Args);


        public EventWatcher(string name, EventHandlerDelegate handler)
        {
            this.mHandler = handler;
            this.objName = name;
        }

        public EventWatcher(object obj, object userArgs, EventHandlerDelegate handler)
        {
            this.mHandler = handler;
            this.objName = this.mHandler.Method.ReflectedType.Name;
        }

        public bool beginInvoke(BaseEventArgs evtArgs)
        {
           // Deployment.get_Current().get_Dispatcher().BeginInvoke(delegate {
            //
            beingInvokeMethod handler = new beingInvokeMethod(doInvoke);
            handler.BeginInvoke(evtArgs,null,null);

            return true;
        }

        public void doInvoke(BaseEventArgs evtArgs)
        {
            this.mHandler(this, evtArgs);
            //return true;
        }

        public void printInfo()
        {
        }

        public string objMethod
        {
            get
            {
                return this.mHandler.Method.Name;
            }
        }
    }
}

