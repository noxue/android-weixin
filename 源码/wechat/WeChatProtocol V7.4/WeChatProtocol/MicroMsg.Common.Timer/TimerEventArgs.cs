namespace MicroMsg.Common.Timer
{
    using System;

    public class TimerEventArgs : EventArgs
    {
        public object mArgs;

        public TimerEventArgs(object args)
        {
            this.mArgs = args;
        }

        public static object getObject(EventArgs e)
        {
            TimerEventArgs args = e as TimerEventArgs;
            if (args == null)
            {
                return null;
            }
            return args.mArgs;
        }
    }
}

