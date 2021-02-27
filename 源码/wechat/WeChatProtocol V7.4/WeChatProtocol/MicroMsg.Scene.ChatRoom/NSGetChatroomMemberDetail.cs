namespace MicroMsg.Scene.ChatRoom
{
    using ChatRoomXmlData;
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;

    public class NSGetChatroomMemberDetail : NetSceneBaseEx<GetChatroomMemberDetailRequest, GetChatroomMemberDetailResponse, GetChatroomMemberDetailRequest.Builder>
    {
        private ModContact mChatRoomContact;
        private const string TAG = "NSGetChatroomMemberDetail";

        public static bool doScene(string ChatRoomName, ModContact mContact)
        {

            NSGetChatroomMemberDetail detail = new NSGetChatroomMemberDetail();
            detail.mChatRoomContact = mContact;
            return detail.doSceneEx(ChatRoomName);
        }
        public static bool doScene(string ChatRoomName)
        {

            NSGetChatroomMemberDetail detail = new NSGetChatroomMemberDetail();

            return detail.doSceneEx(ChatRoomName);
        }
        private bool doSceneEx(string ChatRoomName)
        {
            //if (ct == null)
            //{
            //    return false;
            //}
            //if (!ContactHelper.isChatRoom(ct.strUsrName))
            //{
            //    Log.d("NSGetChatroomMemberDetail", "is not chatroom, strUsrName = " + ct.strUsrName);
            //    return false;
            //}
            //this.mChatRoomContact = ct;
            //uint num = ChatRoomMgr.getLocalVersionFromRoomData(this.mChatRoomContact.strChatRoomData);
            //if ((ChatRoomMgr.getServerVersionFromRoomData(this.mChatRoomContact.strChatRoomData) <= num) && (ChatRoomMgr.getChatRoomMemberContactList(ct.strUsrName) != null))
            //{
            //    Log.d("NSGetChatroomMemberDetail", "no need to get new ChatroomMemberDetail");
            //    return false;
            //}
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, (int)ConstantsProtocol.CLIENT_MAX_VERSION);
            base.mBuilder.ChatroomUserName = ChatRoomName;
            base.mBuilder.ClientVersion = 0;
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/getchatroommemberdetail";
            base.mSessionPack.mConnectMode = 2;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(GetChatroomMemberDetailRequest request, GetChatroomMemberDetailResponse response)
        {
            Log.e("NSGetChatroomMemberDetail", "send failed");
        }

        protected override void onSuccess(GetChatroomMemberDetailRequest request, GetChatroomMemberDetailResponse response)
        {
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NSGetChatroomMemberDetail", "send failed ret =" + ret);
            }
            else
            {
                //string str = "";
                List<Contact> contactList = new List<Contact>();
                string ChatroomPath = ConstantsProtocol.CurrentDirectory + "\\User\\" + AccountMgr.curUserName + "\\" + ConstantsProtocol.ChatRoomPath + "\\" + response.ChatroomUserName + ".xml";
                if (response.NewChatroomData.ChatRoomMemberList.Count > 0)
                {
                    ChatRoomData ChatRoomData = new ChatRoomData();
                    ChatRoomData.ChatRoomOwner = mChatRoomContact.ChatRoomOwner;


                    if (File.Exists(ChatroomPath))
                    {

                        XDocument xdocument;


                        try
                        {
                            xdocument = XDocument.Load(ChatroomPath);
                        }
                        catch (Exception e)
                        {

                            Log.e("NSGetChatroomMemberDetail", "xml filed" + e.StackTrace.ToString());
                            return;
                        }
                        ChatRoomData = Util.Deserialize<ChatRoomData>(ChatRoomData, xdocument.ToString());
                        XElement xmlMemberCount = xdocument.Root.Element("ChatRoomMemberCount");
                        int MemberCount = int.Parse(xmlMemberCount.Value);
                        bool MemberFlag = false;
                        if (MemberCount == response.NewChatroomData.ChatRoomMemberList.Count)
                        {
                            return;
                        }
                        if (response.NewChatroomData.ChatRoomMemberList.Count > MemberCount)//有人入群
                        {
                            MemberFlag = true;
                        }




                        if (MemberFlag)
                        {
                            foreach (ChatRoomMemberInfo info in response.NewChatroomData.ChatRoomMemberList)
                            {


                                var elements = xdocument.Root.Elements("ChatRoomMemberData")
                                           .Where(e => (string)e.Element("UserName").Value == info.UserName)
                                           // .OrderByDescending(e => (string)e.Element("UserName"))
                                           .ToList();

                                if (elements.Count == 1)
                                {

                                    ///替换成新的节点  
                                    elements.First().ReplaceNodes(
                                            new XElement("UserName", info.UserName),
                                            new XElement("NickName", info.NickName),
                                            new XElement("InviterUserName", info.InviterUserName),
                                            new XElement("BigHeadImgUrl", info.BigHeadImgUrl),
                                            new XElement("DisplayName", info.DisplayName)
                                        );


                                }

                                if (elements.Count == 0)
                                {

                                    string strInfo = MemberFlag == true ? "有人入群了" : "有退群了";

                                    Log.w("NSGetChatroomMemberDetail", strInfo + "nickname " + info.NickName + "user name " + info.UserName + "  headUrl  " + info.BigHeadImgUrl);
                                    XElement xmlMemberData = new XElement("ChatRoomMemberData",
                                            new XElement("UserName", info.UserName),
                                            new XElement("NickName", info.NickName),
                                            new XElement("InviterUserName", info.InviterUserName),
                                            new XElement("BigHeadImgUrl", info.BigHeadImgUrl),
                                            new XElement("DisplayName", info.DisplayName)
                                             );
                                    xdocument.Root.Add(xmlMemberData);


                                    //ServiceCenter.sendAppMsg.doSceneSendAppMsg(mChatRoomContact.UserName.String, 1, "<msg><appmsg appid=\"\" sdkver=\"0\"><title>好友入群提示</title><des>" + info.NickName + "</des><action></action><type>16</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url></url><lowurl></lowurl><dataurl></dataurl><lowdataurl></lowdataurl><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl>" + info.BigHeadImgUrl + "</thumburl><carditem><from_scene>2</from_scene><card_type>0</card_type><card_type_name>兑换券</card_type_name><card_id>phbbzs8tc_BrOuozQnjw4FQJNs01</card_id><color>#FD9931</color><brand_name>加我为您服务</brand_name><card_ext></card_ext><share_from_scene>1</share_from_scene></carditem>(null)<md5></md5></appmsg></msg>");

                                }

                            }
                            xmlMemberCount.ReplaceNodes(response.NewChatroomData.ChatRoomMemberList.Count);


                        }
                        else
                        {


                            for (int i = ChatRoomData.ChatRoomMemberData.Count - 1; i >= 0; i--)
                            {

                                var elements = response.NewChatroomData.ChatRoomMemberList
                                            .Where(e => e.UserName == ChatRoomData.ChatRoomMemberData[i].UserName)
                                            .ToList();

                                if (elements.Count == 0)//说明有人退群了
                                {
                                    string strInfo = MemberFlag == true ? "有人入群了" : "有退入群了";




                                    Log.w("NSGetChatroomMemberDetail", strInfo + "nickname " + ChatRoomData.ChatRoomMemberData[i].NickName + "user name " + ChatRoomData.ChatRoomMemberData[i].UserName + "  headUrl  " + ChatRoomData.ChatRoomMemberData[i].BigHeadImgUrl);                             
                                   // ServiceCenter.sendAppMsg.doSceneSendAppMsg(mChatRoomContact.UserName.String, 1, "<msg><appmsg appid=\"\" sdkver=\"0\"><title>好友退群提示</title><des>" + ChatRoomData.ChatRoomMemberData[i].NickName + "</des><action></action><type>16</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url></url><lowurl></lowurl><dataurl></dataurl><lowdataurl></lowdataurl><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl>" + ChatRoomData.ChatRoomMemberData[i].BigHeadImgUrl + "</thumburl><carditem><from_scene>2</from_scene><card_type>0</card_type><card_type_name>兑换券</card_type_name><card_id>phbbzs8tc_BrOuozQnjw4FQJNs01</card_id><color>#FD9931</color><brand_name>加我为您服务</brand_name><card_ext></card_ext><share_from_scene>1</share_from_scene></carditem>(null)<md5></md5></appmsg></msg>");
                                    ChatRoomData.ChatRoomMemberData.RemoveAt(i);


                                }


                            }
                            ChatRoomData.ChatRoomMemberCount = response.NewChatroomData.ChatRoomMemberList.Count;
                            string xmlString = Util.Serializer<ChatRoomData>(ChatRoomData);
                            XDocument.Parse(xmlString).Save(ChatroomPath);
                            return;


                        }


                        try
                        {
                            xdocument.Save(ChatroomPath);
                        }
                        catch (Exception e)
                        {

                            Log.e("NSGetChatroomMemberDetail", "Save xml filed" + e.StackTrace.ToString());
                            return;
                        }



                    }
                    else
                    {



                        ChatRoomData.ChatRoomMemberData = new List<ChatRoomXmlData.ChatRoomMemberData>();
                        foreach (ChatRoomMemberInfo info in response.NewChatroomData.ChatRoomMemberList)
                        {
                            ChatRoomXmlData.ChatRoomMemberData item = new ChatRoomXmlData.ChatRoomMemberData
                            {
                                UserName = info.UserName,
                                NickName = info.NickName,
                                InviterUserName = info.InviterUserName,
                                BigHeadImgUrl = info.BigHeadImgUrl,
                                DisplayName = info.DisplayName
                            };
                            ChatRoomData.ChatRoomMemberData.Add(item);
                        }

                        ChatRoomData.ChatRoomMemberCount = response.NewChatroomData.ChatRoomMemberList.Count;
                        string xmlString = Util.Serializer<ChatRoomData>(ChatRoomData);
                        XDocument.Parse(xmlString).Save(ChatroomPath);



                    }



                }

                if (response.NewChatroomData.ChatRoomMemberList.Count == 0)//被移除群聊
                {

                    File.Delete(ChatroomPath);
                }

            }
        }
    }
}
