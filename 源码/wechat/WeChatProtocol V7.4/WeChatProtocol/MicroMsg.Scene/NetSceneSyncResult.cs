namespace MicroMsg.Scene
{
    using MicroMsg.Protocol;
    using System;

    public class NetSceneSyncResult
    {
        public bool hasNewMsg;
        public bool isAppActive;
        public RetConst retCode;
        public int syncCount;
        public SyncStatus syncStatus;
    }
}

