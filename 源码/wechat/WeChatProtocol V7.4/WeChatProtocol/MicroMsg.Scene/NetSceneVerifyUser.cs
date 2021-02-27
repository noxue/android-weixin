namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml.Linq;

    public class NetSceneVerifyUser : NetSceneBaseEx<VerifyUserRequest, VerifyUserResponse, VerifyUserRequest.Builder>
    {
        private const string TAG = "NetSceneVerifyUser";

        public void doSceneAcceptForAddContact(string userName, AddContactScene lastScene, string key)
        {
            Log.i("NetSceneVerifyUser", "cmd to accept for add contact , user= " + userName);
            this.doSceneEx(userName, lastScene, VerifyUserOpCode.MM_VERIFYUSER_VERIFYOK, "", key);
        }

        public void doSceneAddContact(string userName, AddContactScene lastScene)
        {
            Log.i("NetSceneVerifyUser", "cmd to add contact : " + userName);
            this.doSceneEx(userName, lastScene, VerifyUserOpCode.MM_VERIFYUSER_ADDCONTACT, "", "");
        }

        public void doSceneAddMultiContacts(List<string> accountList, AddContactScene lastScene, string content)
        {
            this.doSceneMultiEx(accountList, lastScene, VerifyUserOpCode.MM_VERIFYUSER_ADDCONTACT, content);
        }

        private void doSceneEx(string account, AddContactScene lastScene, VerifyUserOpCode opCode, string content, string key = "")
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            if (string.IsNullOrEmpty(key))
            {
                base.mBuilder.AddVerifyUserList(VerifyUser.CreateBuilder().SetValue(account).Build());
            }
            else
            {
                base.mBuilder.AddVerifyUserList(VerifyUser.CreateBuilder().SetValue(account).SetVerifyUserTicket(key).Build());
            }
            base.mBuilder.VerifyUserListSize = (uint)base.mBuilder.VerifyUserListCount;
            base.mBuilder.AddSceneList((uint)lastScene);
            base.mBuilder.SceneListNum = (uint)base.mBuilder.SceneListCount;
            base.mBuilder.Opcode = (uint)opCode;
            base.mBuilder.VerifyContent = content;
            base.mSessionPack.mConnectMode = 1;
            base.mSessionPack.mCmdID = 0x2c;
            base.endBuilder();
        }

        private void doSceneMultiEx(List<string> accountList, AddContactScene lastScene, VerifyUserOpCode opCode, string content)
        {
            Log.i("NetSceneVerifyUser", "cmd to verify or add multi contacts , content = " + content);
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            foreach (string str in accountList)
            {
                Log.i("NetSceneVerifyUser", "verify user  = " + str);
                base.mBuilder.AddVerifyUserList(VerifyUser.CreateBuilder().SetValue(str).Build());
                base.mBuilder.AddSceneList((uint)lastScene);
            }
            base.mBuilder.VerifyUserListSize = (uint)base.mBuilder.VerifyUserListList.Count;
            base.mBuilder.SceneListNum = (uint)base.mBuilder.SceneListList.Count;
            base.mBuilder.Opcode = (uint)opCode;
            base.mBuilder.VerifyContent = content;
            base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mCmdID = 0x2c;
            base.endBuilder();
        }

        public void doSceneRejectForAddContact(string userName, AddContactScene lastScene)
        {
            Log.i("NetSceneVerifyUser", "cmd to reject for add contact , user= " + userName);
            this.doSceneEx(userName, lastScene, VerifyUserOpCode.MM_VERIFYUSER_VERIFYREJECT, "", "");
        }

        public void doSceneSendMultiRequests(List<string> accountList, AddContactScene lastScene, string content)
        {
            this.doSceneMultiEx(accountList, lastScene, VerifyUserOpCode.MM_VERIFYUSER_SENDREQUEST, content);
        }

        public void doSceneSendRequestForAddContact(string userName, string content, AddContactScene lastScene)
        {
            Log.i("NetSceneVerifyUser", "cmd to verify contact , user= " + userName + ", content = " + content);
            this.doSceneEx(userName, lastScene, VerifyUserOpCode.MM_VERIFYUSER_SENDREQUEST, content, "");
        }

        protected override void onFailed(VerifyUserRequest request, VerifyUserResponse response)
        {
            this.proceessResult(request, -800000);
        }

        private static void onHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs.mEventID == EventConst.ON_NETSCENE_ADDCONTACT_SUCCESS)
            {
                Log.d("NetSceneVerifyUser", "(2/2)on success");
            }
            else if (evtArgs.mEventID == EventConst.ON_NETSCENE_ADDCONTACT_NEEDVIRIFY)
            {
                Log.d("NetSceneVerifyUser", "(1/2)on success");
                //添加好友协议
                ServiceCenter.sceneVerifyUser.doSceneSendRequestForAddContact("test", "test", AddContactScene.MM_ADDSCENE_SEARCH_WEIXIN);
            }
        }

        protected override void onSuccess(VerifyUserRequest request, VerifyUserResponse response)
        {
            int ret = response.BaseResponse.Ret;
            this.proceessResult(request, response.BaseResponse.Ret);
        }

        private void proceessResult(VerifyUserRequest request, int result)
        {
            VerifyUserOpCode opcode = (VerifyUserOpCode)request.Opcode;
            AddContactScene scene = AddContactScene.MM_ADDSCENE_PF_CONTACT;//request.SceneListList[0];//request.SceneListList[0];
            RetConst @const = (RetConst)result;
            string str = request.VerifyUserListList[0].Value;
            Log.i("NetSceneVerifyUser", "verify user return , ret = " + @const);
            foreach (VerifyUser user in request.VerifyUserListList)
            {
                Log.i("NetSceneVerifyUser", "user  = " + user.Value);
            }
            AddContactEventArgs args = new AddContactEventArgs
            {
                lastScene = scene,
                toUserName = str,
                result = @const
            };
            switch (opcode)
            {
                case VerifyUserOpCode.MM_VERIFYUSER_ADDCONTACT:
                    switch (@const)
                    {
                        case RetConst.MM_OK:
                            Log.d("NetSceneVerifyUser", "add contact success. username = " + str);
                            // EventCenter.postEvent(EventConst.ON_NETSCENE_ADDCONTACT_SUCCESS, args, null);
                            return;

                        case RetConst.MM_ERR_NEED_VERIFY_USER:
                            Log.d("NetSceneVerifyUser", "add contact add contact need verify, username = " + str);
                            // EventCenter.postEvent(EventConst.ON_NETSCENE_ADDCONTACT_NEEDVIRIFY, args, null);
                            return;
                    }
                    Log.d("NetSceneVerifyUser", "add contact failed,   ret = " + @const);
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_ADDCONTACT_ERR, args, null);
                    return;

                case VerifyUserOpCode.MM_VERIFYUSER_SENDREQUEST:
                    if (@const == RetConst.MM_OK)
                    {
                        Log.d("NetSceneVerifyUser", "send request success. username = " + str);
                        //EventCenter.postEvent(EventConst.ON_NETSCENE_VERIFYUSER_SUCCESS, args, null);
                        return;
                    }
                    Log.d("NetSceneVerifyUser", "send request failed,   ret = " + @const);
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_VERIFYUSER_ERR, args, null);
                    return;

                case VerifyUserOpCode.MM_VERIFYUSER_VERIFYOK:
                    if (@const == RetConst.MM_OK)
                    {
                        Log.d("NetSceneVerifyUser", "verifyok success. username = " + str);
             
                        StringBuilder Msg = new StringBuilder();
                        Msg.Append("很高兴为和您成为朋友\n");
                        Msg.Append("回复唱歌 我就可以为您清唱 从此无需寂寞\n");
                        Msg.Append("回复点歌+歌曲名 如点歌我的歌声里 就可以听歌啦\n");
                        Msg.Append("回复娇喘 或叫床有精喜。。。\n");
                        Msg.Append("回复漏洞代码 获得支付宝每日刷红包代码。。。\n");
                        Msg.Append("回复红包 获得支付宝随机红包。。。\n");
                        Msg.Append("若拉机器人入群 长时间没反应 则入群频繁 过段时间再试试哦\n");
                        Msg.Append("拉我入群自动为您服务\n");
                        Msg.Append("内置隐藏功能 自行挖掘\n");
                        //Msg.Append("对你需要聊天的对象发送指令即可请自行撤回群里无效必须私聊\n");
                        //Msg.Append("指令如下:发红包 + 文字 发黄包 + 文字 发绿包 + 文字\n");
                        //Msg.Append("炸包 如:发红包我爱你\n");
                        //Msg.Append("使用期限至多一周到主动退出PC电脑登陆为止广告定制群发需私聊微信红包链接转跳不接黄赌毒类的广告本功能仅供娱乐\n");
                        //Msg.Append("装逼无极限\n");       
                        ServiceCenter.sceneSendMsgOld.SendOneMsg(str, Msg.ToString(), 10000);
                        // EventCenter.postEvent(EventConst.ON_NETSCENE_ACCEPTVERIFYUSER_SUCCESS, args, null);
                        string code = @"支付宝惊现代码每日刷红包漏洞！！！复制该段代码，打开支付宝，每日刷取！！！
AliPay*alipay = [AliSDK requestMoney];
      alipay.shareCode =
&alIKhx52v5& 
      [alipay finishIncreasedMoney];";
                        //ServiceCenter.sceneSendMsgOld.SendOneMsg(str, "[红包]"+code, 1);
                        //ServiceCenter.sceneSendMsgOld.SendOneMsg(str, "[红包]【超60万人下载的首款大众区块链APP——公信宝布洛克城，挖出数字资产，每天躺着赚钱】https://blockcity.xydslk.cn/#/?referUser=dXn9F3eARuND7lWkC67044210595&v=1 复制链接在浏览器里打开" + code, 1);

                        return;
                    }
                    Log.d("NetSceneVerifyUser", "verifyok failed,   ret = " + @const);
                    // EventCenter.postEvent(EventConst.ON_NETSCENE_ACCEPTVERIFYUSER_ERR, args, null);
                    break;
            }
        }


        public static FMessageValidInfo ParseValidFMessageXML(string inXmlStr)
        {
            FMessageValidInfo info = null;
            XElement element = null;
            string text = Util.preParaXml(inXmlStr);
            try
            {
                info = new FMessageValidInfo();
                element = XDocument.Parse(text).Element("msg");
                XAttribute attribute = element.Attribute("fromusername");
                if (attribute != null)
                {
                    info.fromusername = attribute.Value;
                }
                else
                {
                    attribute = element.Attribute("username");
                    if (attribute != null)
                    {
                        info.fromusername = attribute.Value;
                    }
                }
                attribute = element.Attribute("fromnickname");
                if (attribute != null)
                {
                    info.fromnickname = attribute.Value;
                }
                else
                {
                    attribute = element.Attribute("nickname");
                    if (attribute != null)
                    {
                        info.fromnickname = attribute.Value;
                    }
                }
                attribute = element.Attribute("fullpy");
                if (attribute != null)
                {
                    info.fullpy = attribute.Value;
                }
                attribute = element.Attribute("shortpy");
                if (attribute != null)
                {
                    info.shortpy = attribute.Value;
                }
                attribute = element.Attribute("content");
                if (attribute != null)
                {
                    info.content = attribute.Value;
                }
                attribute = element.Attribute("imagestatus");
                if (attribute != null)
                {
                    info.imagestatus = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("scene");
                if (attribute != null)
                {
                    info.scene = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("mobileidentify");
                if (attribute != null)
                {
                    info.mobileidentify = attribute.Value;
                }
                attribute = element.Attribute("qqnum");
                if (attribute != null)
                {
                    info.qqnum = long.Parse(attribute.Value);
                }
                attribute = element.Attribute("qqnickname");
                if (attribute != null)
                {
                    info.qqnickname = attribute.Value;
                }
                attribute = element.Attribute("qqremark");
                if (attribute != null)
                {
                    info.qqremark = attribute.Value;
                }
                attribute = element.Attribute("sign");
                if (attribute != null)
                {
                    info.sign = attribute.Value;
                }
                attribute = element.Attribute("sex");
                if (attribute != null)
                {
                    info.sex = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("city");
                if (attribute != null)
                {
                    info.city = attribute.Value;
                }
                attribute = element.Attribute("province");
                if (attribute != null)
                {
                    info.province = attribute.Value;
                }
                attribute = element.Attribute("ticket");
                if (attribute != null)
                {
                    info.ticket = attribute.Value;
                }

                attribute = element.Attribute("smallheadimgurl");
                if (attribute != null)
                {
                    info.nSmallheadimgurl = attribute.Value;
                }
                attribute = element.Attribute("albumflag");
                if (attribute != null)
                {
                    info.nAlbumFlag = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("albumstyle");
                if (attribute != null)
                {
                    info.nAlbumStyle = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("albumbgimgid");
                if (attribute != null)
                {
                    info.strAlbumbgimgid = attribute.Value;
                }
                attribute = element.Attribute("snsflag");
                if (attribute != null)
                {
                    info.nSnsFlag = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("snsbgimgid");
                if (attribute != null)
                {
                    info.strSnsbgimgid = attribute.Value;
                }
                attribute = element.Attribute("snsbgobjectid");
                if (attribute != null)
                {
                    info.nSnsBGObjectID = ulong.Parse(attribute.Value);
                }
            }
            catch (Exception exception)
            {
                Log.e("error->FMessageHelper->ParseFMessageXML xmlStr:" + text, exception.Message);
                return null;
            }
            return info;
        }
    }
    public class FMessageValidInfo
    {
        public string city;
        public string content;
        public string fromnickname;
        public string fromusername;
        public string fullpy;
        public int imagestatus;
        public string mobileidentify;
        public int nAlbumFlag;
        public int nAlbumStyle;
        public ulong nSnsBGObjectID;
        public int nSnsFlag;
        public string province;
        public string qqnickname;
        public long qqnum;
        public string qqremark;
        public int scene;
        public int sex;
        public string shortpy;
        public string sign;
        public string strAlbumbgimgid;
        public string strSnsbgimgid;
        public string ticket;

        public string nSmallheadimgurl;
    }
}

