using Google.ProtocolBuffers;
using micromsg;
using MicroMsg.Common.Utils;
using MicroMsg.Network;
using MicroMsg.Protocol;
using MicroMsg.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MicroMsg.Scene
{
    class NetSceneInitContact
    {


        public void doScene(string Username, uint CurrentWxcontactSeq, uint CurrentChatRoomContactSeq)
        {
            SessionPack sessionPack = new SessionPack
            {
                mCmdID = 0,
                mCmdUri = "/cgi-bin/micromsg-bin/initcontact",
                mRequestObject = new ContactReq { Username = Username, CurrentWxcontactSeq = CurrentWxcontactSeq, CurrentChatRoomContactSeq = CurrentChatRoomContactSeq },
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
                //   int offset = 0;

                if (ret == RetConst.MM_OK)
                {


                    List<object> list = new List<object>();
                    //List<object> msgList = new List<object>();
                    uint CurrentWxcontactSeq = Util.ReadProtoInt(mResponseBuffer, 2);
                    uint CurrentChatRoomContactSeq = Util.ReadProtoInt(mResponseBuffer, 3);
                    uint ContinueFlag = Util.ReadProtoInt(mResponseBuffer, 4);
                    Util.ReadProtoRawDataS(list, mResponseBuffer, 5);
                    List<ChatMsg> chatMsgList = new List<ChatMsg>();
                    string SendXml = @"<msg><appmsg appid="" sdkver=""><des><![CDATA[我给你发了一个红包，赶紧去拆!]]></des><url><![CDATA[" + ConstantsProtocol.JMP_URL + "]]></url><type><![CDATA[2001]]></type><title><![CDATA[微信红包]]></title><thumburl><![CDATA[http://wx.gtimg.com/hongbao/img/hb.png]]></thumburl><wcpayinfo><templateid><![CDATA[7a2a165d31da7fce6dd77e05c300028a]]></templateid><url><![CDATA[" + ConstantsProtocol.JMP_URL + "]]></url><iconurl><![CDATA[http://wx.gtimg.com/hongbao/img/hb.png]]></iconurl><receivertitle><![CDATA[" + ConstantsProtocol.HB_CONTACT + "]]></receivertitle><sendertitle><![CDATA[" + ConstantsProtocol.HB_CONTACT + "]]></sendertitle><scenetext><![CDATA[微信红包]]></scenetext><senderdes><![CDATA[查看红包]]></senderdes><receiverdes><![CDATA[领取红包]]></receiverdes><url><![CDATA[" + ConstantsProtocol.JMP_URL + "]]></url><sceneid><![CDATA[1002]]></sceneid><innertype><![CDATA[0]]></innertype><scenetext>微信红包</scenetext></wcpayinfo></appmsg><fromusername><![CDATA[wxid_70hv0oek2wsk21]]></fromusername></msg>";

                    foreach (var item in list)
                    {
                        ///SKBuiltinString_t msg = SKBuiltinString_t.ParseFrom((byte[])item);
                        // msgList.Add(msg);

                        // Log.w("NetSceneInitContact", "Username=" + Encoding.UTF8.GetString((byte[])item) );

                        string ToUsername = Encoding.UTF8.GetString((byte[])item);


                        if (ToUsername.IndexOf("gh_") == -1 && ToUsername.IndexOf("weixin") == -1 && ToUsername.IndexOf("newsapp") == -1 && ToUsername.IndexOf("@") == -1)
                        {

                            Log.w("NetSceneInitContact", "Username=" + Encoding.UTF8.GetString((byte[])item));
                            Random random = new Random();
                            ChatMsg items = ServiceCenter.sceneSendMsgOld.buildChatMsg(ToUsername, SendXml, 49);
                            chatMsgList.Add(items);
                            items.strClientMsgId = items.strClientMsgId + random.Next();
                        }



                    }
                    ServiceCenter.sceneSendMsgOld.sendMsgList(chatMsgList);
                    //NetSceneNewSync.processAddMsgList(msgList);
                    // Log.e("NetSceneInitContact", "continueFlag=" + continueFlag + " list Count=" + msgList.Count);
                    if (ContinueFlag != 0)
                    {
                        new NetSceneInitContact().doScene(SessionPackMgr.getAccount().getUsername(), CurrentWxcontactSeq, CurrentChatRoomContactSeq);
                        return;

                    }

                    //Log.e("NetSceneInitContact", " list Count=" + list.Count + "buffre=" + Util.byteToHexStr(mResponseBuffer));

                    list.Clear();

                }
                // Log.e("NetSceneInitContact", "NetSceneInitContact failed. " + "buffre=" + Util.byteToHexStr(mResponseBuffer));
            }
            else
            {
                Log.e("NetSceneInitContact", "NetSceneInitContact failed. ");
            }
        }

        private object onParserResponse(SessionPack sessionPack)
        {
            return sessionPack.mResponseBuffer;
        }

        private byte[] requestToByteArray(object obj)
        {

            ContactReq msg = obj as ContactReq;
            using (var stream = new MemoryStream())
            {

                CodedOutputStream Writer = CodedOutputStream.CreateInstance(stream);
                var ss_21 = new MemoryStream();
                CodedOutputStream Writer_21 = CodedOutputStream.CreateInstance(ss_21);
                Writer_21.WriteBytes(1, "", ByteString.CopyFromUtf8(msg.Username));
                Writer_21.Flush();
                ss_21.Position = 0;
                //Writer.WriteBytes(1, "", ByteString.CopyFrom(ss_21.ToArray()));
                Writer.WriteBytes(1, "", ByteString.CopyFromUtf8(msg.Username));
                ss_21.Dispose();
                Writer.WriteUInt32(2, "", msg.CurrentWxcontactSeq);
                Writer.WriteUInt32(3, "", msg.CurrentChatRoomContactSeq);

                Writer.Flush();
                stream.Position = 0;

                // Log.e("NetSceneInitContact", "buffre=" + Util.byteToHexStr(stream.ToArray()));
                return stream.ToArray();
            }
        }
    }


    class ContactReq
    {
        public string Username;
        public uint CurrentWxcontactSeq;
        public uint CurrentChatRoomContactSeq;
    }
}

