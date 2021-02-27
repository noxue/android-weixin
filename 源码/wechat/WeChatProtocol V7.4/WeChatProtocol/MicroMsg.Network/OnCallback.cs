namespace MicroMsg.Network
{
    using MicroMsg.Common.Utils;
    using System;

    public class OnCallback
    {
        public static void onAllError(PackResult ret)
        {
            Log.e("Network", "onAllError");
            SessionPackMgr.cleanAllTimeoutPoint(3);
            SessionPackMgr.markAllToNotSended(3);
            for (SessionPack pack = SessionPackMgr.getFirstNotSended(); pack != null; pack = SessionPackMgr.getFirstNotSended())
            {
                pack.mSendStatus = 6;
                onError(pack, ret);
            }
        }

        public static void onError(SessionPack sessionPack, PackResult ret)
        {
            sessionPack.mCacheBodyBuffer = null;
            sessionPack.mResponseBuffer = null;
            sessionPack.onError(ret);
            sessionPack.timeInNetCompleted = Util.getNowMilliseconds();
        }

        public static void onSuccess(SessionPack sessionPack)
        {
            sessionPack.mCacheBodyBuffer = null;
            sessionPack.onSuccess();
            sessionPack.timeInNetCompleted = Util.getNowMilliseconds();
        }
    }
}

