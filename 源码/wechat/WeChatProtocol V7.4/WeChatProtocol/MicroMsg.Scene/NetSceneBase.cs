namespace MicroMsg.Scene
{
    using Google.ProtocolBuffers;
    using micromsg;
    using MicroMsg.Common.Utils;
    //using MicroMsg.Manager;
    using MicroMsg.Network;
    using MicroMsg.Protocol;
    //using MicroMsg.Storage;
    using System;
    using MicroMsg.Storage;
    using MicroMsg.Manager;
    using System.Text;

    public abstract class NetSceneBase
    {
        private static int gSceneCount = 1;
        public int mSceneID = gSceneCount++;
        protected SessionPack mSessionPack;
        private const string TAG = "NetSceneBase";

        protected NetSceneBase()
        {
        }

        public void cancel()
        {
            if (this.mSessionPack != null)
            {
                Log.d("NetSceneBase", "do cancel scene... seq = " + this.mSessionPack.mSeqID);
                this.mSessionPack.mCanceled = true;
                this.mSessionPack = null;
            }
        }
        //1678050096
        //1711278097
        public static BaseRequest makeBaseRequest(int scene, int version = 1711278097)
        {
            BaseRequest.Builder builder = BaseRequest.CreateBuilder();
            Account account = AccountMgr.getCurAccount();
            if (account.nUin == 0)
            {
                byte[] bytes = new byte[0x24];
                builder.SessionKey = ByteString.CopyFrom(bytes);
                builder.Uin = 0;
            }
            else
            {
                builder.SessionKey = ByteString.CopyFrom(account.bytesSessionkey);
                builder.Uin = account.nUin;
            }
            builder.DeviceID = ByteString.CopyFromUtf8(Util.getDeviceUniqueId());
            // builder.ClientVersion =(int)ConstantsProtocol.CLIENT_VERSION;
           // builder.DeviceID = ByteString.CopyFrom(Util.getDeviceUniqueId());
            builder.ClientVersion = version;
            builder.DeviceType = ByteString.CopyFromUtf8(ConstantsProtocol.DEVICE_TYPE);//ByteString.CopyFrom(Util.toFixLenString(ConstantsProtocol.DEVICE_TYPE, 0x84));
            builder.Scene = (uint) scene;
          //  Log.e("BaseRequest", " hex deviceid " + Util.byteToHexStr(builder.Build().ToByteArray()));
            return builder.Build();
        }
    }
}

