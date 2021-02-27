namespace MicroMsg.Manager
{
    using MicroMsg.Common.Utils;
    using System;

    public class SyncInfo
    {
        public bool bSetCurSyncKey;
        public bool bSetInitStatus;
        public bool bSetMaxSyncKey;
        public bool bSetSyncKey;
        public byte[] bytesCurSyncKey;
        public byte[] bytesMaxSyncKey;
        public byte[] bytesSyncKeyBuf;
        public int nInitStatus;

        public SyncInfo()
        {
        }

        public SyncInfo(byte[] syncKey)
        {
            this.bytesSyncKeyBuf = Util.byteArrayClone(syncKey);
            this.bSetSyncKey = true;
        }

        public SyncInfo(byte[] curKey, byte[] maxKey, byte[] syncKey, int state)
        {
            this.bytesCurSyncKey = Util.byteArrayClone(curKey);
            this.bSetCurSyncKey = true;
            this.bytesMaxSyncKey = Util.byteArrayClone(maxKey);
            this.bSetMaxSyncKey = true;
            this.bytesSyncKeyBuf = Util.byteArrayClone(syncKey);
            this.bSetSyncKey = true;
            this.nInitStatus = state;
            this.bSetInitStatus = true;
        }
    }
}

