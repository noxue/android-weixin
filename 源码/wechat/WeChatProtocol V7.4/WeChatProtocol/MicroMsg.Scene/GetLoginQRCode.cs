using Google.ProtocolBuffers;
using MicroMsg.Common.Event;
using MicroMsg.Common.Utils;
using MicroMsg.Manager;
using MicroMsg.Network;
using MicroMsg.Protocol;
using System.IO;
using System.Text;
using System.Threading;
using WeChatProtocol.MicroMsg.Storage;

namespace MicroMsg.Scene
{
    class NetSceneGetLoginQRCode
    {


        public void doScene()
        {
            SessionPack sessionPack = new SessionPack
            {
                mCmdID = 232,
                mCmdUri = "/cgi-bin/micromsg-bin/getloginqrcode",
                mRequestObject = new object()
            };
            sessionPack.mProcRequestToByteArray += new RequestToByteArrayDelegate(this.requestToByteArray);
            sessionPack.mResponseParser += new OnResponseParserDelegate(this.onParserResponse);
            sessionPack.mCompleted += new SessionPackCompletedDelegate(this.onCompleted);
            Sender.getInstance().sendPack(sessionPack);
        }

        private void onCompleted(object sender, PackEventArgs e)
        {
            SessionPack pack = sender as SessionPack;
            byte[] mResponseBuffer = pack.mResponseBuffer;
            if (e.isSuccess() && (mResponseBuffer != null))
            {
                //  
                //  int num2 = Util.readInt(mResponseBuffer, ref offset);
                //Log.d("GetLoginQRCode", "mResponseBuffer" + Util.byteToHexStr(mResponseBuffer));


                byte[] response = Util.ReadProtoRawData(mResponseBuffer, 1);
                uint Ret = Util.ReadProtoInt(response, 1);

                RetConst ret = (RetConst)Ret;
                int offset = 0;

                if (ret == RetConst.MM_OK)
                {
            


                    GetLoginQrcode qrcode = new GetLoginQrcode();
                    response = Util.ReadProtoRawData(mResponseBuffer, 2);
                    qrcode.ImgBuf = Util.ReadProtoRawData(response, 2);
                    qrcode.Uuid = Encoding.UTF8.GetString(Util.ReadProtoRawData(mResponseBuffer, 3));
                    qrcode.CheckTime = Util.ReadProtoInt(mResponseBuffer, 4);

                    response = Util.ReadProtoRawData(mResponseBuffer, 5);
                    qrcode.NotifyKey = Util.ReadProtoRawData(response, 2);
                    qrcode.ExpiredTime = Util.ReadProtoInt(mResponseBuffer, 6);
                  //  Log.d("GetLoginQRCode", " thread id " + Thread.CurrentThread.ManagedThreadId.ToString());
                    EventCenter.postEvent(EventConst.ON_LOGIN_GETQRCODE, qrcode,null);
                }

            }
            else
            {
                Log.e("GetLoginQRCode", "GetLoginQRCode failed. ");
            }
        }

        private object onParserResponse(SessionPack sessionPack)
        {
            return sessionPack.mResponseBuffer;
        }

        private byte[] requestToByteArray(object obj)
        {

            SessionPack sessionPack = obj as SessionPack;

            using (var stream = new MemoryStream())
            {
                CodedOutputStream Writer = CodedOutputStream.CreateInstance(stream);
                var ss_67 = new MemoryStream();
                CodedOutputStream Writer_67 = CodedOutputStream.CreateInstance(ss_67);

                byte[] bytes = new byte[0x24];

                Writer_67.WriteBytes(1, "", ByteString.CopyFrom(bytes));

                Writer_67.WriteInt32(2, "", 0);

                Writer_67.WriteBytes(3, "", ByteString.CopyFromUtf8(Util.getDeviceUniqueId()));
                


                Writer_67.WriteInt32(4, "", 369302817);

                Writer_67.WriteBytes(5, "", ByteString.CopyFrom(Encoding.UTF8.GetBytes(ConstantsProtocol.DEVICE_TYPE)));

                Writer_67.WriteInt32(6, "", 0);

                Writer_67.Flush();
                ss_67.Position = 0;
                Writer.WriteBytes(1, "", ByteString.CopyFrom(ss_67.ToArray()));
                ss_67.Dispose();
                var ss_20 = new MemoryStream();
                CodedOutputStream Writer_20 = CodedOutputStream.CreateInstance(ss_20);

                Writer_20.WriteInt32(1, "", 16);

                Writer_20.WriteBytes(2, "", ByteString.CopyFrom(new byte[] { 104, 84, 125, 199, 142, 226, 48, 218, 83, 195, 3, 84, 3, 123, 208, 162 }));

                Writer_20.Flush();
                ss_20.Position = 0;
                Writer.WriteBytes(2, "", ByteString.CopyFrom(ss_20.ToArray()));
                ss_20.Dispose();

                Writer.WriteInt32(3, "", 0);
                Writer.Flush();
                stream.Position = 0;
               // Log.e("GetLoginQRCode", " hex deviceid " + Util.byteToHexStr(stream.ToArray()));
                return stream.ToArray();
            }
        }
    }
}
