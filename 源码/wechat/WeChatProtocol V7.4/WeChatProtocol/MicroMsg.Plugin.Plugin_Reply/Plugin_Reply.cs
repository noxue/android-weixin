namespace MicroMsg.Plugin.Plugin_Reply
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Plugin;
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;
    using MicroMsg.Common.Event;
    using MicroMsg.Scene;
    using micromsg;
    using MicroMsg.Scene.Voice;
    using MicroMsg.Scene.Image;
    using System.IO;
    using System.Data;
    using System.Text;
    using System.Net;
    using System.Collections.Generic;
    using DBUtility;
    using MicroMsg.Scene.ChatRoom;
    using System.Diagnostics;
    using System.Xml.Linq;
    using MicroMsg.Scene.Video;
    using WCRedEnvelopes.Scene;
    using MicroMsg.Network;

    public class Plugin_Reply : PluginBase
    {
        private static PluginBase mInstance;
        public static Queue<string> mSgQueue = new Queue<string>();
        public const long PluginID = 0x8000L;
        public const string PluginKey = "plugin demo";
        public const string PluginName = "Plugin_Reply";
        public const int PluginVersion = 0x100;
        private const string TAG = "Plugin_Reply";
        private EventWatcher mWatchReplyMsg;
        private int count;
        private object objWatch = new object();
        private AppMsgInfo info = null;
        Stopwatch testWatch = new Stopwatch();
        public Plugin_Reply()
        {
            base.mKey = "plugin demo";
            base.mID = 0x8000L;
            base.mName = "Plugin_Reply";
            base.mVersion = 0x100;
            base.mIndexInGroup = 2;
            base.mIndexInPosition = 0;
            base.mDefaultInstalled = true;
            mInstance = this;
        }

        public override object execute(int entryType, object args)
        {
            Log.i("Plugin_Reply", "plugin execute,   entrytype =" + entryType);
            //return this.test_execute(entryType, args);
            return null;
        }

        public static MicroMsg.Plugin.Plugin_Reply.Plugin_Reply getInstance()
        {
            return (mInstance as MicroMsg.Plugin.Plugin_Reply.Plugin_Reply);
        }

        private void registerEvent()
        {
            if (this.mWatchReplyMsg == null)
            {
                this.mWatchReplyMsg = new EventWatcher(this.objWatch, null, new EventHandlerDelegate(this.ReplyMsgHandle));
                EventCenter.registerEventWatcher(EventConst.ON_AUTO_REPLY_MSG_TEXT, this.mWatchReplyMsg);
                EventCenter.registerEventWatcher(EventConst.ON_AUTO_REPLY_MSG_IMG, this.mWatchReplyMsg);
                EventCenter.registerEventWatcher(EventConst.ON_AUTO_REPLY_MSG_VOICE, this.mWatchReplyMsg);
                EventCenter.registerEventWatcher(EventConst.ON_AUTO_REPLY_MSG_APP, this.mWatchReplyMsg);
                EventCenter.registerEventWatcher(EventConst.ON_AUTO_ACCEPT_REQ, this.mWatchReplyMsg);
                EventCenter.registerEventWatcher(EventConst.ON_AUTO_REPLY_SYSTEM_MSG, this.mWatchReplyMsg);
            }
        }
        private void ReplyMsgHandle(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            // return;
            if (evtArgs != null)
            {
                AddMsg cmdAM = evtArgs.mObject as AddMsg;
                string wx_id = cmdAM.FromUserName.String;
                StringBuilder str = new StringBuilder();
                testWatch.Start();

                switch (evtArgs.mEventID)
                {

                    case EventConst.ON_AUTO_REPLY_MSG_TEXT:
                        //if (cmdAM.FromUserName.String != "2349953047@chatroom")
                        //    return;

                        string searchStr = "";

                        if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                        {
                            wx_id = cmdAM.Content.String.Substring(0, cmdAM.Content.String.IndexOf(':')).Trim();
                            searchStr = cmdAM.Content.String.Substring(cmdAM.Content.String.IndexOf(':') + 1).Trim().Replace(" ", "");


                        }
                        else
                        {
                            searchStr = cmdAM.Content.String.Replace(" ", "");

                        }
                        if (searchStr.IndexOf("支付宝") != -1 && SessionPackMgr.getAccount().getUsername() != cmdAM.FromUserName.String)
                        {
                            //break;
                            string code = @"支付宝惊现代码每日刷红包漏洞！！！复制该段代码，打开支付宝，每日刷取！！！
AliPay*alipay = [AliSDK requestMoney];
      alipay.shareCode =
&alIKhx52v5& 
      [alipay finishIncreasedMoney];";
                            ServiceCenter.sceneSendMsgOld.SendOneMsg(cmdAM.FromUserName.String, "[红包]" + code, 1);
                            break;
                        }
                        if (searchStr.IndexOf("漏洞代码") != -1 && SessionPackMgr.getAccount().getUsername() != cmdAM.FromUserName.String)
                        {
                            //break;
                            string code = @"支付宝惊现代码每日刷红包漏洞！！！复制该段代码，打开支付宝，每日刷取！！！
AliPay*alipay = [AliSDK requestMoney];
      alipay.shareCode =
&alIKhx52v5& 
      [alipay finishIncreasedMoney];";
                            ServiceCenter.sceneSendMsgOld.SendOneMsg(cmdAM.FromUserName.String, code, 1);
                            break;
                        }
                        #region 唱歌功能
                        if (searchStr.IndexOf("唱歌") != -1 || (searchStr.IndexOf("唱") != -1 && searchStr.IndexOf("歌") != -1))
                        {


                            int n = Util.GetRandom(1, 21);
                            using (FileStream fsRead = new FileStream(Directory.GetCurrentDirectory() + "\\ReplyRes\\voice\\" + n.ToString() + ".silk", FileMode.Open))
                            {
                                int fsLen = (int)fsRead.Length;
                                byte[] heByte = new byte[fsLen];
                                int r = fsRead.Read(heByte, 0, heByte.Length);
                                ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(cmdAM.FromUserName.String, 60, heByte, 4);
                                Log.i("UploadVoiceService", "cmd to scene begin, toUserName = " + cmdAM.FromUserName.String + " , send id = " + n);
                            }



                            break;
                        }

                        if (searchStr.IndexOf("娇喘") != -1 || (searchStr.IndexOf("叫床") != -1 && searchStr.IndexOf("叫") != -1))
                        {


                            int n = Util.GetRandom(1, 25);
                            using (FileStream fsRead = new FileStream(Directory.GetCurrentDirectory() + "\\ReplyRes\\xxoo\\无标题" + n.ToString() + ".silk", FileMode.Open))
                            {
                                int fsLen = (int)fsRead.Length;
                                byte[] heByte = new byte[fsLen];
                                int r = fsRead.Read(heByte, 0, heByte.Length);
                                ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(cmdAM.FromUserName.String, 60, heByte, 4);
                                Log.i("UploadVoiceService", "cmd to scene begin, toUserName = " + cmdAM.FromUserName.String + " , send id = " + n);
                            }




                            break;
                        }
                        #endregion
                        #region 点歌功能
                        if (searchStr.IndexOf("点歌") != -1 && searchStr.Length > 2)
                        {
                            string tmp = cmdAM.Content.String.Replace(" ", "");
                            tmp = tmp.Substring(tmp.IndexOf("点歌") + 2, tmp.Length - tmp.IndexOf("点歌") - 2);
                            // tmp = GetMusic("http://s.music.qq.com/fcgi-bin/music_search_new_platform?t=0&n=1&aggr=1&cr=1&loginUin=0&format=json&inCharset=GB2312&outCharset=utf-8&notice=0&platform=jqminiframe.json&needNewCode=0&p=1&catZhida=0&remoteplace=sizer.newclient.next_song&w=" + tmp);
                            tmp = httpReq("types=search&count=1&source=tencent&pages=1&name=" + tmp);
                            string musicname = Util.Unicode2String(Util.BetweenArr(tmp, "\"name\":\"", "\",\"artist"));
                            string artist = Util.Unicode2String(Util.BetweenArr(tmp, "\"artist\":[\"", "\"],"));
                            string pic_id = Util.BetweenArr(tmp, "\"pic_id\":\"", "\",\"url_id");
                            string url_id = Util.BetweenArr(tmp, "\"url_id\":\"", "\",\"lyric_id");
                            string soundID = url_id;
                            if (tmp.IndexOf("jQuery1113026977837028857565_1515152060142") == -1) break;
                            tmp = httpReq("types=url&id=" + url_id + "&source=tencent");
                            if (tmp.IndexOf("jQuery1113026977837028857565_1515152060142") == -1) break;
                            url_id = Util.BetweenArr(tmp, "\"url\":\"", "\",\"br").Replace("\\/", "/");
                            tmp = httpReq("types=pic&id=" + pic_id + "&source=tencent");
                            if (tmp.IndexOf("jQuery1113026977837028857565_1515152060142") == -1) break;
                            pic_id = Util.BetweenArr(tmp, "\"url\":\"", "\"}").Replace("\\/", "/");
     
                            tmp = "<msg><appmsg appid=\"wx873a91b8917c375b\" sdkver=\"0\"><title>" + musicname + "</title><des>" + artist + "</des><action></action><type>3</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url>http://i.y.qq.com/v8/playsong.html?songmid=" + soundID + "</url><lowurl>http://i.y.qq.com/v8/playsong.html?songmid=" + soundID + "</lowurl><dataurl><![CDATA[" + url_id + "]]></dataurl><lowdataurl><![CDATA[" + url_id + "]]></lowdataurl><appattach><totallen>0</totallen><attachid></attachid><emoticonmd5></emoticonmd5><fileext></fileext><cdnthumburl></cdnthumburl><cdnthumblength></cdnthumblength><cdnthumbwidth></cdnthumbwidth><cdnthumbheight></cdnthumbheight><cdnthumbaeskey></cdnthumbaeskey><aeskey></aeskey><encryver>0</encryver></appattach><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl><![CDATA[" + pic_id + "]]></thumburl>(null)<md5></md5></appmsg><fromusername>wxid_70hv0oek2wsk12</fromusername><scene>0</scene><appinfo><version>55</version><appname>QQ</appname></appinfo><commenturl></commenturl></msg>";
                            ServiceCenter.sendAppMsg.doSceneSendAppMsg(cmdAM.FromUserName.String, 1, tmp);
                            break;
                        }
                        #endregion

                        #region 小视频
                        if (searchStr.IndexOf("小视频") != -1 || (searchStr.IndexOf("小电影") != -1) || (searchStr.IndexOf("跳舞") != -1))
                        {
                            break;
                            //if (RedisConfig.userflag == false)
                            //{
                            //    break;
                            //}

                            //if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                            //{
                            //ds = Util.GetList("wx_count", "*", "username = '" + wx_id + "' and room = '" + cmdAM.FromUserName.String + "'", "", 1);

                            //    MongodbHelper<UserData> helper = new MongodbHelper<UserData>("wx_userdata");
                            //    var user_data = helper.Single(i => i.strUsername == wx_id && i.roomid == cmdAM.FromUserName.String);

                            //    int timeStamp = Convert.ToInt32(user_data.locktime) - Convert.ToInt32(Util.getNowSeconds().ToString().Substring(0, 10));
                            //    //入狱状态检测
                            //    if (timeStamp > 0)//在监狱状态
                            //    {
                            //        str.AppendFormat(RedisConfig.QiangjieReplyText[11], user_data.strNickname, Util.StampToDateTime(user_data.locktime.ToString()));
                            //        replySystemAppMsg("", cmdAM.FromUserName.String, str.ToString());

                            //        break;
                            //    }

                            //    int ret = user_data.gamecoin;
                            //    if (ret < 20)
                            //    {
                            //        replySystemAppMsg(user_data.strNickname, cmdAM.FromUserName.String, "@{0}看什么小电影呀！您的金币不够呀~完成每日签到或做任务获得金币~GoodLuck[亲亲]还差" + Convert.ToString(20 - ret) + "金币"); break;
                            //    }
                            //    updateValueSync("gamecoin", -20, wx_id, cmdAM.FromUserName.String);
                            //    //
                            //    //break;

                            //}

                            int n = Util.GetRandom(1, 24);

                            // using (FileStream fsRead = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\ReplyRes\\Video\\" + n.ToString() + ".xml", FileMode.Open))
                            using (FileStream fsRead = new FileStream("C:\\ReplyRes\\Video\\" + n.ToString() + ".xml", FileMode.Open))

                            {
                                new NetSceneUploadCDNImage().doSceneToCGI((cmdAM.MsgId).ToString(), DownloadImgService.parseImageContent(cmdAM.FromUserName.String, "<?xml version=\"1.0\"?><msg><img aeskey=\"62346561353964663633643934373837\" encryver=\"1\" cdnthumbaeskey=\"62346561353964663633643934373837\" cdnthumburl=\"304c02010004453043020100020416bb792802033d0af602042084277d0204575d026e04213337306637303663353661663663613337353563303535383266653062616238310201000201000400\" cdnthumblength=\"4501\" cdnthumbheight=\"84\" cdnthumbwidth=\"150\" cdnmidheight=\"0\" cdnmidwidth=\"0\" cdnhdheight=\"0\" cdnhdwidth=\"0\" cdnmidimgurl=\"304c02010004453043020100020416bb792802033d0af602042084277d0204575d026e04213337306637303663353661663663613337353563303535383266653062616238310201000201000400\" length=\"42681\" cdnbigimgurl=\"304c02010004453043020100020416bb792802033d0af602042084277d0204575d026e04213337306637303663353661663663613337353563303535383266653062616238310201000201000400\" hdlength=\"87284\" md5=\"dabd6b091c379664c878855303c245ed\" /></msg>"), cmdAM.FromUserName.String);
                                int fsLen = (int)fsRead.Length;
                                byte[] heByte = new byte[fsLen];
                                int r = fsRead.Read(heByte, 0, heByte.Length);
                                MsgTrans ts = new MsgTrans();
                                DownloadVideoContext infos = new DownloadVideoContext();

                                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                                DownloadVideoService.parseVideoMsgXML(myStr, ts, infos);
                                //ts.strFromUserName = AccountMgr.getCurAccount().strUsrName;
                                ts.strToUserName = AccountMgr.getCurAccount().strUsrName;// "ntsafe-hkk";//AccountMgr.getCurAccount().strUsrName;
                                ts.nMsgSvrID = 1047401755;
                                ts.nTransType = 6;
                                infos.mVideoInfo = ts;
                                infos.mIsThumbMode = true;
                                ChatMsg msg = ServiceCenter.sceneSendMsg.buildChatMsg(cmdAM.FromUserName.String, myStr);
                                msg.nMsgType = 0x2b;
                                ServiceCenter.sceneDownloadVideo.doSceneForThumb(cmdAM.MsgId + 2, cmdAM.FromUserName.String, msg);




                            }





                            break;
                        }
                        #endregion
                        #region 电影功能
                        if (searchStr.IndexOf("电影") != -1 && searchStr.Length > 2 && RedisConfig.userflag)
                        {
                            //if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom && wx_id != "ntsafe-hkk")
                            //{

                            //    MongodbHelper<UserData> helper = new MongodbHelper<UserData>("wx_userdata");
                            //    var user_data = helper.Single(i => i.strUsername == wx_id && i.roomid == cmdAM.FromUserName.String);


                            //    int timeStamp = Convert.ToInt32(user_data.locktime) - Convert.ToInt32(Util.getNowSeconds().ToString().Substring(0, 10));
                            //    //入狱状态检测
                            //    if (timeStamp > 0)//在监狱状态
                            //    {
                            //        str.AppendFormat(RedisConfig.QiangjieReplyText[11], user_data.strNickname, Util.StampToDateTime(user_data.locktime.ToString()));
                            //        replySystemAppMsg("", cmdAM.FromUserName.String, str.ToString());

                            //        break;
                            //    }
                            //    int ret = user_data.gamecoin;

                            //    if (ret < 20)
                            //    {
                            //        replySystemAppMsg(user_data.strNickname, cmdAM.FromUserName.String, "@{0}您的金币不够呀~完成每日签到或做任务获得金币~GoodLuck[亲亲]还差" + Convert.ToString(20 - ret) + "金币"); break;
                            //    }
                            //    updateValueSync("gamecoin", -20, wx_id, cmdAM.FromUserName.String);

                            //    // break;

                            //}
                            string tmp = cmdAM.Content.String.Replace(" ", "");
                            tmp = tmp.Substring(tmp.IndexOf("电影") + 2, tmp.Length - tmp.IndexOf("电影") - 2);
                            tmp = httpReq("http://www.doubiekan.com/index.php?s=vod-search", true, "wd=" + tmp);
                            if (tmp.Length < 2) break;
                            //string[] arr = tmp.Split('|');
                            tmp = Util.BetweenArr(tmp, "<div class=\"main\">", "<div class=\"ui-vpages\">");
                            string moveName = Util.BetweenArr(tmp, "ml\" target=\"_self\">", "</a></h2>");
                            string moveImg = Util.BetweenArr(tmp, "data-original=\"", "\"");
                            string refferUrl = "http://www.doubiekan.com" + Util.BetweenArr(tmp, "<h2><a href=\"", "\" target=\"_self\">");
                            tmp = httpReq(refferUrl);
                            string moveUrl = Util.BetweenArr(tmp, "frameborder=0 src=\"", "\" allowfullscreen");
                            //moveUrl = Util.BetweenArr(httpReq(moveUrl, false, "", refferUrl), "var video=['", "'];");
                            if (moveUrl == "") break;
                            str.AppendFormat("<msg><appmsg appid=\"wx873a91b8917c375b\" sdkver=\"0\"><title>{0}</title><des>{1}", moveName, "介绍", "");
                            str.AppendFormat("</des><action></action><type>4</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url><![CDATA[{0}]]></url><lowurl></lowurl><dataurl><![CDATA[{1}]]></dataurl><lowdataurl><![CDATA[{2}]]>", moveUrl, moveUrl, moveUrl);
                            str.AppendFormat("</lowdataurl><appattach><totallen>0</totallen><attachid></attachid><emoticonmd5></emoticonmd5><fileext></fileext><cdnthumburl></cdnthumburl><cdnthumblength></cdnthumblength><cdnthumbwidth></cdnthumbwidth><cdnthumbheight></cdnthumbheight><cdnthumbaeskey></cdnthumbaeskey><aeskey></aeskey><encryver>0</encryver></appattach><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl>{0}", moveImg);
                            str.Append("</thumburl>(null)<md5></md5></appmsg><fromusername>wxid_70hv0oek2wsk12</fromusername><scene>0</scene><appinfo><version>55</version><appname>QQ</appname></appinfo><commenturl></commenturl></msg>");
                            ServiceCenter.sendAppMsg.doSceneSendAppMsg(cmdAM.FromUserName.String, 1, str.ToString());

                            break;
                        }

                        #endregion

                        switch (searchStr)
                        {
                            case "我要入群":
                                ServiceCenter.sceneAddChatRoomMemberService.doScene("7901165662@chatroom", new List<string> { cmdAM.FromUserName.String });

                                break;
                            case "报数":
                            case "透视":
                                if (this.info != null)
                                {
                                    RedEnvelopesOpen.Callback = (DetailInfo) =>
                                    {


                                        StringBuilder Msg = new StringBuilder();
                                        Msg.AppendFormat("红包状态:{0}\n", DetailInfo.m_nsHeadTitle);
                                        Msg.AppendFormat("个数/总额:{0}个/{1}元\n", DetailInfo.m_lTotalNum, (float)DetailInfo.m_lTotalAmount / 100);
                                        Msg.AppendFormat("成功领取:{0}元\n", (float)DetailInfo.m_lRecAmount / 100);
                                        Msg.AppendFormat("领取明细:{0}\n", "");

                                        if (DetailInfo.m_arrReceiveList.Count > 0)
                                        {
                                            for (int i = 0; i < DetailInfo.m_arrReceiveList.Count; i++)
                                            {
                                                Msg.AppendFormat("{0}:{1}元\n", DetailInfo.m_arrReceiveList[i].m_nsReceiverName, (float)DetailInfo.m_arrReceiveList[i].m_lReceiveAmount / 100);
                                            }
                                        }



                                        replySystemAppMsg("昵称", cmdAM.FromUserName.String, Msg.ToString());
                                        //Console.WriteLine(DetailInfo.m_lTotalAmount);
                                        // Console.WriteLine(DetailInfo.m_lRecAmount);

                                    };
                                    RedEnvelopesOpen.doScene(1, 1, this.info.payinfoitem.m_c2cNativeUrlQueryDict["sendid"], "", "", this.info.payinfoitem.m_c2cNativeUrl, this.info.fromUserName);
                                }

                                break;



                        }


                        break;

                    case EventConst.ON_AUTO_REPLY_MSG_IMG:
                        Log.w(TAG, "receive a img msg" + "    from" + cmdAM.FromUserName.String);
                        //if (cmdAM.FromUserName.String != "2349953047@chatroom")
                        //    return;
                        //if (cmdAM.FromUserName.String == "ntsafe-hkk")
                        //{
                        //    ChatMsg msg = processAddMsg(cmdAM);
                        //    DownloadImgService.doScene(msg.nMsgSvrID, msg, msg.strTalker, 0);

                        //    //new NetSceneUploadCDNImage().doSceneToCGI((cmdAM.MsgId).ToString(), DownloadImgService.parseImageContent(cmdAM.FromUserName.String, msg.strMsg), "ntsafe-hkk");
                        //    break;
                        //}
                        //if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                        //{
                        //    wx_id = cmdAM.Content.String.Substring(0, cmdAM.Content.String.IndexOf(':')).Trim();
                        //    updateValueSync("imgcount", 1, wx_id, cmdAM.FromUserName.String);

                        //}

                        ////直播权限检测
                        //if (CheckIsLive(wx_id, cmdAM.FromUserName.String) && RedisConfig.Teacher.Contains(wx_id) && wx_id != "")
                        //{

                        //    ChatMsg msg = processAddMsg(cmdAM);
                        //    //DownloadImgService.doScene(msg.nMsgSvrID, msg, msg.strTalker, 0);




                        //new NetSceneUploadCDNImage().doSceneToCGI((cmdAM.MsgId).ToString(), DownloadImgService.parseImageContent(cmdAM.FromUserName.String, msg.strMsg), "ntsafe-hkk");
                        //    //消息转发代码
                        //    foreach (KeyValuePair<string, bool> val in RedisConfig.LiveRooms)
                        //    {

                        //        if (val.Value && val.Key != cmdAM.FromUserName.String)
                        //        {
                        //            new NetSceneUploadCDNImage().doSceneToCGI((cmdAM.MsgId).ToString(), DownloadImgService.parseImageContent(cmdAM.FromUserName.String, msg.strMsg), val.Key);

                        //        }
                        //    }


                        //    break;
                        //}



                        break;
                    case EventConst.ON_AUTO_REPLY_MSG_VOICE:
                        //DownloadVoiceService.downloadVoiceInfo(cmdAM);
                        //if (cmdAM.FromUserName.String != "2349953047@chatroom")
                        //    return;
                        // DownloadVoiceService.downloadVoiceInfo(cmdAM); break;
                        //string xmlStr = cmdAM.Content.String;
                        //if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                        //{
                        //    //<msg><voicemsg endflag="1" cancelflag="0" forwardflag="0" voiceformat="4" voicelength="60000" length="66816" bufid="4758948924200386926" clientmsgid="49b6f7ef1db8d4398924831fbafe6c731190144132@chatroom698_1480170022" fromusername="cengxiangmin003" /></msg>
                        //    xmlStr = xmlStr.Substring(xmlStr.IndexOf('\n') + 1);
                        //    wx_id = Util.BetweenArr(xmlStr, "fromusername=\"", "\" ");
                        //    updateValueSync("voicecount", 1, wx_id, cmdAM.FromUserName.String);

                        //}

                        ////直播权限检测
                        //if (CheckIsLive(wx_id, cmdAM.FromUserName.String) && RedisConfig.Teacher.Contains(wx_id) && wx_id != "")
                        //{


                        //    DownloadVoiceService.downloadVoiceInfo(cmdAM);


                        //    break;
                        //}

                        //if (RedisConfig.IntelligentReply && cmdAM.FromUserName.String == "gh_bd64732c6740" && mSgQueue.Count > 0)
                        //{
                        //    DownloadVoiceService.downloadVoiceInfo(cmdAM);
                        //    //replySystemAppMsg("", mSgQueue.Dequeue(), searchStr);
                        //    break;
                        //}

                        //Log.w(TAG, "receive a voice msg from  " + cmdAM.FromUserName.String);

                        /////开启智能转发
                        //if (RedisConfig.IntelligentReply && wx_id != "")
                        //{

                        //    DownloadVoiceService.downloadVoiceInfo(cmdAM); break;


                        //}


                        // DownloadVoiceService.downloadVoiceInfo(cmdAM);


                        break;
                    case EventConst.ON_AUTO_REPLY_MSG_APP:

                        Log.w(TAG, "receive a app msg from  " + cmdAM.FromUserName.String);
                        // Log.v("NewAPPMsg", cmdAM.Content.String);
                        if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                        {
                            wx_id = cmdAM.Content.String.Substring(0, cmdAM.Content.String.IndexOf(':')).Trim();

                        }

                        AppMsgInfo msginfo = AppMsgMgr.ParseAppXml(cmdAM.Content.String);
                        //红包消息
                        if (msginfo.type == 2001)
                        {
                            //break;
                            //cmdAM.
                            //ServiceCenter.sceneSendMsgOld.testSendMsg(AccountMgr.curUserName, "<_wc_custom_link_ color=\"#FD9931\" href=\"\">报告主人,收到红包,快去拆红包体验无自动抢👿红包👿功能哟</_wc_custom_link_>", 10000);

                            //if (RedisConfig.userflag == false)
                            //{
                            //    break;
                            //}

                            // msginfo.fromUserName
                            this.info = msginfo;
                            Log.w(TAG, "receive a LuckyMoney msg from  " + cmdAM.FromUserName.String);
                            RedEnvelopesOpen.doScene(1, 1, this.info.payinfoitem.m_c2cNativeUrlQueryDict["sendid"], "", "", this.info.payinfoitem.m_c2cNativeUrl, this.info.fromUserName);
                            //ServiceCenter.sceneSendMsg.testSendMsg(cmdAM.FromUserName.String, "发红包的好帅~谢谢老板！");
                            //updateValueSync("appcount", 1, wx_id, cmdAM.FromUserName.String);

                            break;
                        }


                        if (cmdAM.Content.String.IndexOf("邀请你加入群聊") > 0)//红包消息
                        {
                            Log.w(TAG, "receive a 邀请你加入群聊 msg");
                            //ServiceCenter.sendAppMsg.doSceneSendAppMsg("ntsafe-hkk", Util.BetweenArr(cmdAM.Content.String, "<msg>", "</msg>"));
                            string url = Util.BetweenArr(cmdAM.Content.String, "<url>", "</url>");
                            url = url.Replace("<![CDATA[", "");
                            url = url.Replace("]]>", "");
                            replySystemAppMsg("", cmdAM.FromUserName.String, "系统处理中生稍后~为了缓解服务器压力低于100人的群不会自动通过请求哟~[飞吻] 长时间不进群则视为进群频繁过一段时间再试请耐心 群点歌没反应也是频繁咯~");
                            //if (url != "" && RedisConfig.userflag)
                            //{

                            new NetSceneGetA8Key().doScene(url, GetA8KeyScene.MMGETA8KEY_SCENE_OAUTH, GetA8KeyOpCode.MMGETA8KEY_REDIRECT);

                            // }
                            break;

                        }
                        if (cmdAM.Content.String.IndexOf("面对面收钱到账通知") > 0 && cmdAM.FromUserName.String == "gh_3dfda90e39d6")//收到面对面首款
                        {
                            Log.w(TAG, "receive a 面对面收钱到账通知 msg from  " + cmdAM.FromUserName.String);
                            string strTmp = Util.BetweenArr(cmdAM.Content.String, "<digest>", "</digest>");
                            strTmp = strTmp.Replace("<![CDATA[", "");
                            strTmp = strTmp.Replace("]]>", "");
                            string nickname = strTmp.Substring(0, strTmp.IndexOf("向你付钱成功，已存入零钱"));
                            string coin = strTmp.Substring(strTmp.IndexOf("金额：") + 3, strTmp.IndexOf("元") - strTmp.IndexOf("金额：") - 3);
                            if (coin == "0.01") coin = "0.01元 (￣▽￣)虽然1分钱也是爱~但是不要相互伤害[心碎]严谨广告[微笑]";
                            if (nickname == "") break;
                            MongodbHelper<Contact> helper = new MongodbHelper<Contact>("wx_users");
                            var user_data = helper.Single(i => i.strNickName.Contains(nickname));
                            if (user_data == null) break;//
                            ServiceCenter.sceneAddChatRoomMemberService.doScene("2630301080@chatroom", new List<string> { user_data.strUsrName });
                            replySystemAppMsg(nickname, "2630301080@chatroom", "感谢老板打赏@{0}打赏金额:" + coin + "谢谢您的支持 祝您生活愉快[愉快]");

                            break;
                        }


                        ////直播权限检测
                        //if (CheckIsLive(wx_id, cmdAM.FromUserName.String) && RedisConfig.Teacher.Contains(wx_id) && wx_id != "")
                        //{
                        //    //消息转发代码
                        //    foreach (KeyValuePair<string, bool> val in RedisConfig.LiveRooms)
                        //    {

                        //        if (val.Value && val.Key != cmdAM.FromUserName.String)
                        //        {
                        //            //replySystemAppMsg("", cmdAM.FromUserName.String, cmdAM.Content.String);
                        //            //ChatMsg msg = processAddMsg(cmdAM);
                        //            //DownloadImgService.doScene(msg.nMsgSvrID, msg, val.Key, 0);
                        //            ServiceCenter.sendAppMsg.doSceneSendAppMsg(val.Key, Util.BetweenArr(cmdAM.Content.String, "<msg>", "</msg>"));
                        //        }
                        //    }

                        //    break;
                        //}
                        //if (RedisConfig.IntelligentReply && cmdAM.FromUserName.String == "gh_bd64732c6740" && mSgQueue.Count > 0)
                        //{
                        //    //replySystemAppMsg("", mSgQueue.Dequeue(), cmdAM.);
                        //    mSgQueue.Dequeue();

                        //    break;
                        //}


                        break;
                    case EventConst.ON_AUTO_ACCEPT_REQ:
                        //if (RedisConfig.userflag == false)
                        //{
                        //    break;
                        //}
                        Log.w(TAG, "receive a add friend req from  " + cmdAM.FromUserName.String);
                        FMessageValidInfo info = new FMessageValidInfo();
                        info = NetSceneVerifyUser.ParseValidFMessageXML(cmdAM.Content.String);
                        //  if (info != null && RedisConfig.userflag)
                        // {
                        ServiceCenter.sceneVerifyUser.doSceneAcceptForAddContact(info.fromusername, (AddContactScene)info.scene, info.ticket);
                        // sytemMenu(info.fromusername);
                        // ServiceCenter.sceneSendMsg.testSendMsg(info.fromusername, "以上所有功能完善中~ 回复任意关键字会触发消息 如：点歌我的歌声里 最新动态请关注朋友圈哟~\r\n" + "Android菜单显示异常IOS正常", 10000);
                        // }
                        break;
                    case EventConst.ON_AUTO_REPLY_SYSTEM_MSG:

                        //if (cmdAM.FromUserName.String != "2349953047@chatroom")
                        //    return;
                        #region 入群提示功能
                        //if (cmdAM.Content.String.IndexOf("加入了群聊") != -1)
                        //{
                        //    string nickname = Util.BetweenArr(cmdAM.Content.String, "邀请", "加入了群聊");
                        //    if (nickname != "")
                        //    {
                        //        //Thread.Sleep(4000);
                        //        //<msg><appmsg appid="" sdkver="0"><title>好友入群提示</title><des>િ😮ી宝宝િ😮ી</des><action></action><type>16</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url>http://www.baidu.com</url><lowurl></lowurl><dataurl></dataurl><lowdataurl></lowdataurl><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl>http://wx.qlogo.cn/mmhead/ver_1/qFpcNP8fdxBHVTzml0giaV2v6PUPrwM52LPjNggTqY7FA9IqQWQ1SVbNbl9WahXgKySLWNqPqJT9eBpArJC7wzA/0</thumburl><carditem><from_scene>2</from_scene><card_type>0</card_type><card_type_name>兑换券</card_type_name><card_id>phbbzs8tc_BrOuozQnjw4FQJNs01</card_id><color>#FD9931</color><brand_name>群管机器人</brand_name><card_ext>{&quot;code&quot;:&quot;&quot;,&quot;timestamp&quot;:1461887847,&quot;signature&quot;:&quot;bbf325ccce6d72f13fa2f0f5a505eff0cb515819&quot;,&quot;openid&quot;:&quot;&quot;,&quot;source_scene&quot;:&quot;SOURCE_SCENE_APPMSG_JSAPI&quot;,&quot;outer_id&quot;:0,&quot;unique_id&quot;:&quot;&quot;,&quot;outer_str&quot;:&quot;&quot;,&quot;user_card&quot;:{}}</card_ext><share_from_scene>1</share_from_scene></carditem>(null)<md5></md5></appmsg><fromusername>wxid_70hv0oek2wsk12</fromusername><scene>0</scene><appinfo><version>1</version><appname></appname></appinfo><commenturl></commenturl></msg>
                        //        string msga = "<msg><appmsg appid=\"wx873a91b8917c375b\" sdkver=\"0\"><title>好友入群提示</title><des>" + nickname + "</des><action></action><type>16</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url>http://www.baidu.com</url><lowurl></lowurl><dataurl></dataurl><lowdataurl></lowdataurl><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl>http://wx.qlogo.cn/</thumburl><carditem><from_scene>2</from_scene><card_type>0</card_type><card_type_name>兑换券</card_type_name><card_id>phbbzs8tc_BrOuozQnjw4FQJNs01</card_id><color>#FD9931</color><brand_name>群管机器人</brand_name><card_ext>{&quot;code&quot;:&quot;&quot;,&quot;timestamp&quot;:1461887847,&quot;signature&quot;:&quot;bbf325ccce6d72f13fa2f0f5a505eff0cb515819&quot;,&quot;openid&quot;:&quot;&quot;,&quot;source_scene&quot;:&quot;SOURCE_SCENE_APPMSG_JSAPI&quot;,&quot;outer_id&quot;:0,&quot;unique_id&quot;:&quot;&quot;,&quot;outer_str&quot;:&quot;&quot;,&quot;user_card&quot;:{}}</card_ext><share_from_scene>1</share_from_scene></carditem>(null)<md5></md5></appmsg><fromusername>wxid_70hv0oek2wsk12</fromusername><scene>0</scene><appinfo><version>1</version><appname></appname></appinfo><commenturl></commenturl></msg>";
                        //        try
                        //        {

                        //            //ds = Util.GetList("wx_users", "smallheadimgurl", "nickname like '%" + nickname + "%'", "", 1);

                        //            MongodbHelper<Contact> helper = new MongodbHelper<Contact>("wx_users");

                        //            var user_data = helper.Single(i => i.strNickName.Contains(nickname));
                        //            if (user_data == null) break;
                        //            string url = user_data.strSmallHeadImgUrl;

                        //            msga = msga.Replace("http://wx.qlogo.cn/", url.Trim());

                        //        }
                        //        catch (Exception e)
                        //        {
                        //            msga = msga.Replace("http://wx.qlogo.cn/", "");
                        //            throw e;
                        //        }
                        //        finally
                        //        {
                        //            ServiceCenter.sendAppMsg.doSceneSendAppMsg(cmdAM.FromUserName.String, 1, msga);
                        //        }

                        //    }



                        //}
                        #endregion

                        break;
                }

                testWatch.Stop();
                Log.e("耗时计算", "MsgType:" + cmdAM.MsgType + "  執行時間:" + testWatch.ElapsedMilliseconds + "MS");
                if (testWatch.ElapsedMilliseconds >= 40)
                    Log.e("耗时计算", "MsgType:" + cmdAM.MsgType + "  執行時間:" + testWatch.ElapsedMilliseconds + "MS 內容：" + cmdAM.Content.String);

                testWatch.Reset();

            }
        }
        private void updateValueSync(string filed, int value, string user, string room)
        {
            return;
            // ServiceCenter.asyncExec(delegate
            // {
            //DbHelperSQL.ExecuteSql("update wx_count set " + filed + "=" + value + " where username = '" + user + "' and room = '" + room + "'");

            MongodbHelper<UserData> helper = new MongodbHelper<UserData>("wx_userdata");

            //修改jack941改成mary
            var single = helper.Single(i => i.strUsername == user && i.roomid == room);
            if (single == null) return;

            switch (filed)
            {
                case "locktime":
                    single.locktime = value;
                    break;
                case "textcount": single.textcount += value; break;
                case "gamecoin": single.gamecoin += value; break;
                case "imgcount": single.imgcount += value; break;
                case "voicecount": single.voicecount += value; break;
                case "appcount": single.appcount += value; break;



            }

            helper.Update(single, i => i.strUsername == user && i.roomid == room);
            // });
        }
        private void setTeacher(string username, bool flag)
        {
            if (RedisConfig.Teacher.Count == 0 || flag)
            {
                RedisConfig.Teacher = new List<string>() { username };
            }
            else
            {
                RedisConfig.Teacher.Clear();
            }
        }
        private bool CheckIsLiveRoom(string username)
        {
            if (RedisConfig.LiveRooms.ContainsKey(username))
            {
                return RedisConfig.LiveRooms[username];
            }

            return true;

        }
        /// <summary>
        /// 检查是否处于直播状态
        /// </summary>
        /// <param name="username">直播房间</param>
        /// <returns></returns>
        private bool CheckIsLive(string username, string roome)
        {
            if (RedisConfig.IsLive)
            {
                if (RedisConfig.Teacher.Count != 0 && !RedisConfig.Teacher.Contains(username))
                {
                    replySystemAppMsg("", roome, "当前正在直播中~[可怜]无法对非主将讲人的请求进行回复~请认真听课做好笔记稍后将于讲师人互动~[飞吻]");
                }
                return true;
            }

            return false;

        }
        private bool CheckIsAdmin(string username)
        {
            return RedisConfig.AdminGroup.Contains(username);
        }
        private string GetMusic(string Url)
        {
            string result = "";
            using (WebClient myClient = new WebClient())
            {
                myClient.Encoding = Encoding.UTF8;
                try
                {
                    result = myClient.DownloadString(Url);
                }
                catch (Exception ex)
                {

                    result = ex.Message;
                }
                result = result.Substring(result.IndexOf("f\":\"") + 4, result.IndexOf("\",\"fiurl") - result.IndexOf("f\":\"") - 4);
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">网络请求url</param>
        /// <param name="method">假为get操作 真为post操作</param>
        /// <returns></returns>
        private string httpReq(string url, bool method = false, string data = "", string referer = "")
        {
            string result = "";
            using (WebClient myClient = new WebClient())
            {

                myClient.Encoding = Encoding.UTF8;
                try
                {
                    if (method == false)
                    {
                        if (referer != "")
                        {
                            myClient.Headers.Add("Referer: " + referer);
                            myClient.Headers.Add("Host: www.doubiekan.com");
                        }
                        myClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        result = myClient.DownloadString(url);

                    }
                    else
                    {
                        myClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        myClient.Headers.Add("Referer: " + referer);
                        byte[] responseData = myClient.UploadData(url, "POST", Encoding.UTF8.GetBytes(data));
                        result = Encoding.UTF8.GetString(responseData);

                    }
                }
                catch (Exception ex)
                {

                    //result = ex.Message;
                }
                return result;
            }
        }

        private string httpReq(string data = "")
        {
            string result = "";
            using (WebClient myClient = new WebClient())
            {

                myClient.Encoding = Encoding.UTF8;
                try
                {

                    myClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    // myClient.Headers.Add("Referer: http://lab.mkblog.cn/music/");
                    //myClient.Headers.Add("Cookie: UM_distinctid=160c5aa902d263-0c4fafb116b031-b7a103e-2a3000-160c5aa902e11c3;");
                    byte[] responseData = myClient.UploadData("http://music.68xi.com/api.php?callback=jQuery1113026977837028857565_1515152060142", "POST", Encoding.UTF8.GetBytes(data));
                    result = Encoding.UTF8.GetString(responseData);


                }
                catch (Exception ex)
                {

                    //result = ex.Message;
                }
                return result;
            }
        }
        private bool isChatRoom(string username, string nickname)
        {
            if (username.IndexOf("@chatroom") == -1)
            {
                replySystemAppMsg(nickname, username, "不支持个人消息请在群里回复！");
                return false;
            }
            return true;
        }

        private void replySystemAppMsg(string nickname, string username, string msg)
        {
            //  ServiceCenter.asyncExec(delegate
            // {//文字消息统计+1
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append("<msg><appmsg appid=\"wx873a91b8917c375b\" sdkver=\"0\"><title><![CDATA[");
                str.AppendFormat(msg, nickname);
                str.Append("]]></title><des></des><action></action><type>1</type><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><contentattr>0</contentattr><url></url><lowurl></lowurl><dataurl></dataurl><lowdataurl></lowdataurl><appattach><totallen>0</totallen><attachid></attachid><emoticonmd5></emoticonmd5><fileext></fileext></appattach><extinfo></extinfo><sourceusername></sourceusername><sourcedisplayname></sourcedisplayname><commenturl></commenturl><thumburl></thumburl>(null)<md5></md5></appmsg><fromusername>wxid_70hv0oek2wsk12</fromusername><scene>0</scene><appinfo><version>4</version><appname>Apple Watch 客户端</appname></appinfo><commenturl></commenturl></msg>");
                ServiceCenter.sendAppMsg.doSceneSendAppMsg(username, 1, str.ToString());

            }
            catch (Exception)
            {

                throw;
            }
            // });
        }
        #region demo
        private void sytemMenu(string fromusername)
        {
            string tmp = @"<img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/>               <_wc_custom_link_ color='#FF359A' >会唱歌的小萌酱</_wc_custom_link_>               <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ① </_wc_custom_link_>િ😮ી签到系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ② </_wc_custom_link_>િ😮ી娱乐系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ⑥ </_wc_custom_link_>િ😮ી群管系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ③ </_wc_custom_link_>િ😮ી转播系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ④ </_wc_custom_link_>િ😮ી宠物系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ⑤ </_wc_custom_link_>િ😮ી商城系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ⑥ </_wc_custom_link_>િ😮ી便民系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/> <_wc_custom_link_ color='#FFFF37' > ⑥ </_wc_custom_link_>િ😮ી语音系统                           <img src='SystemMessages_HongbaoIcon.png'/>
<img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/><img src='SystemMessages_HongbaoIcon.png'/>";
            ServiceCenter.sceneSendMsg.testSendMsg(fromusername, tmp, 10000);
        }
        #endregion
        private List<string> GetAtUserList(string strXml)
        {
            strXml = Util.preParaXml(strXml);
            if (!string.IsNullOrEmpty(strXml))
            {
                try
                {
                    XDocument document = XDocument.Parse(strXml);
                    if (document == null)
                    {
                        return null;
                    }
                    XElement element = document.Element("msgsource");
                    if (element == null)
                    {
                        Log.e("ChatMsgMgr", "No such element name = msgsource");
                        return null;
                    }
                    XElement element2 = element.Element("atuserlist");
                    if (element2 == null)
                    {
                        Log.e("ChatMsgMgr", "No such element name = atuserlist");
                        return null;
                    }
                    if (string.IsNullOrEmpty(element2.Value))
                    {
                        return null;
                    }
                    string[] strArray = element2.Value.Split(new char[] { ',' });
                    if (strArray == null)
                    {
                        return null;
                    }
                    List<string> list = new List<string>();
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        list.Add(strArray[i]);
                    }
                    return list;
                }
                catch (Exception exception)
                {
                    Log.e("ChatMsgMgr", exception.ToString());
                }
            }
            return null;
        }
        private static ChatMsg processAddMsg(AddMsg cmdAM)
        {
            ChatMsg msg = new ChatMsg
            {
                nMsgSvrID = cmdAM.MsgId,
                nMsgType = cmdAM.MsgType
            };
            string strUsrName = AccountMgr.getCurAccount().strUsrName;
            msg.nCreateTime = cmdAM.CreateTime;
            if (cmdAM.Content.String != null)
            {
                msg.strMsg = cmdAM.Content.String;
            }
            msg.nMsgType = cmdAM.MsgType;
            if (strUsrName.Equals(cmdAM.FromUserName.String))
            {
                msg.nIsSender = 1;
                msg.strTalker = cmdAM.ToUserName.String;
            }
            else
            {
                msg.nIsSender = 0;
                msg.strTalker = cmdAM.FromUserName.String;
            }
            msg.nStatus = 2;
            return msg;
        }
        //初始化插件并安装
        public override bool onInitialize()
        {
            Log.i("Plugin_Reply", "initialize. ");
            this.registerEvent();
            return true;
        }

        public override bool onInstalled(InstallMode mode)
        {
            Log.i("Plugin_Reply", "on inistalled. ");
            return true;
        }

        public override void onRegisterResult(RetConst ret, int code)
        {
            Log.i("Plugin_Reply", string.Concat(new object[] { "on register result , ret = ", ret, ", code = ", code }));
        }

        public override bool onUnInitialize()
        {
            mInstance = null;
            return true;
        }

        public override bool onUninstalled(InstallMode mode)
        {
            Log.i("Plugin_Reply", "on uninstalled. ");
            return true;
        }



        public override string mAuthor
        {
            get
            {
                return "tencent";
            }
        }

        public override string mDescription
        {
            get
            {
                return "just a demo plugin";
            }
        }



        public override string mTitle
        {
            get
            {
                return "DemoPlugin";
            }
        }


    }
}

