namespace MicroMsg.Scene
{
    using System;

    public interface IContextBase
    {
        bool isRunning();
        bool needToClean();
        bool needToHandle();
    }
}

