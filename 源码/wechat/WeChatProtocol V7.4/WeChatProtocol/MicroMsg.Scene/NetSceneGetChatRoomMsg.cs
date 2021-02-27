using Google.ProtocolBuffers;
using micromsg;
using MicroMsg.Common.Utils;
using MicroMsg.Network;
using MicroMsg.Protocol;
using System.Collections.Generic;
using System.IO;
namespace MicroMsg.Scene
{
    class NetSceneGetChatRoomMsg
    {


        public void doScene(string ChatRoom, uint msgSeq)
        {
            SessionPack sessionPack = new SessionPack
            {
                mCmdID = 0,
                mCmdUri = "/cgi-bin/micromsg-bin/getcrmsg",
                mRequestObject = new ChatRoomMsg { ChatRoomName = ChatRoom, msgSeq = msgSeq },
                mConnectMode = 2
                // NotifyKey = NotifyKey
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
                byte[] response = Util.ReadProtoRawData(mResponseBuffer, 1);
                uint Ret = Util.ReadProtoInt(response, 1);

                RetConst ret = (RetConst)Ret;
                int offset = 0;

                if (ret == RetConst.MM_OK)
                {

                    uint continueFlag = Util.ReadProtoInt(mResponseBuffer, 2);
                    List<object> list = new List<object>();
                    List<object> msgList = new List<object>();
                    Util.ReadProtoRawDataS(list, mResponseBuffer, 3);
                    foreach (var item in list)
                    {
                        AddMsg msg = AddMsg.ParseFrom((byte[])item);
                        msgList.Add(msg);
                    }

                    NetSceneNewSync.processAddMsgList(msgList);
                    Log.e("NetSceneGetChatRoomMsg", "continueFlag=" + continueFlag + " list Count=" + msgList.Count);

                    if (continueFlag != 0)
                    {

                        Log.e("NetSceneGetChatRoomMsg", "continueFlag=" + continueFlag + " list Count=" + msgList.Count + "buffre=" + Util.byteToHexStr(mResponseBuffer));
                    }

                    list.Clear();
                    msgList.Clear();
                }

            }
            else
            {
                Log.e("NetSceneGetChatRoomMsg", "NetSceneGetChatRoomMsg failed. ");
            }
        }

        private object onParserResponse(SessionPack sessionPack)
        {
            return sessionPack.mResponseBuffer;
        }

        private byte[] requestToByteArray(object obj)
        {

            ChatRoomMsg msg = obj as ChatRoomMsg;
            using (var stream = new MemoryStream())
            {

                CodedOutputStream Writer = CodedOutputStream.CreateInstance(stream);
                var ss_21 = new MemoryStream();
                CodedOutputStream Writer_21 = CodedOutputStream.CreateInstance(ss_21);
                Writer_21.WriteBytes(1, "", ByteString.CopyFromUtf8(msg.ChatRoomName));
                Writer_21.Flush();
                ss_21.Position = 0;
                Writer.WriteBytes(1, "", ByteString.CopyFrom(ss_21.ToArray()));
                ss_21.Dispose();
                Writer.WriteUInt32(2, "", msg.msgSeq);
                Writer.WriteInt32(3, "", 20);
                Writer.WriteInt32(4, "", 0);
                Writer.Flush();
                stream.Position = 0;

                //Log.e("NetSceneGetChatRoomMsg",  "buffre=" + Util.byteToHexStr(stream.ToArray()));
                return stream.ToArray();
            }
        }
    }


    class ChatRoomMsg
    {
        public string ChatRoomName;
        public uint msgSeq;
    }
}

