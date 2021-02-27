using System;
using System.Collections.Generic;

using System.Windows.Forms;
using MicroMsg.Scene;
using MicroMsg.Storage;
using MicroMsg.Plugin.Sns.Scene;
using MicroMsg.Network;
using micromsg;
using MicroMsg.Common.Utils;
using MicroMsg.Manager;
using System.Threading;
using MicroMsg.Scene.ChatRoom;
using System.IO;
using MicroMsg.Scene.Video;
using MicroMsg.Plugin.WCPay.Scene;
using MicroMsg.Plugin.WCRedEnvelopes.Scene;
using MicroMsg.Common.Event;
using WeChatProtocol.MicroMsg.Storage;
using System.Drawing;
using System.Net;
using System.Text;
using MicroMsg.Protocol;

namespace WeChatProtocol
{
    public partial class sendForm : Form


    {
        private EventWatcher m_WatcherCheckLoginQrcode;
        public sendForm()
        {
            InitializeComponent();
            m_WatcherCheckLoginQrcode = new EventWatcher(this, null, new EventHandlerDelegate(this.onEventHandlerCheckLoginQrcode));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (rb_VoiceMsg.Checked) {



                using (FileStream fsRead = new FileStream(Directory.GetCurrentDirectory() + "\\ReplyRes\\voice\\1.mp3", FileMode.Open))
                {
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);
                    int type = 4;
                    ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(tb_toUsername.Text, 60, heByte, type);
                    //Log.i("UploadVoiceService", "cmd to scene begin, toUserName = " + cmdAM.FromUserName.String + " , send id = " + n);
                }
            }
            if (rb_QueryRed.Checked)
            {
                //自动抢红包 屏蔽群提示

                RedEnvelopesOpen.ReceiverQueryRedEnvelopes("1", "10000387012016080660772888823", "wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=10000387012016080660772888823&sendusername=wangyi281364&ver=6&sign=1098df3d7cfdd5eaf586d3a9dc8ec87492889e7540edf6a1f7420590ef7b152bed5dfba917383d07cfce01e94de4d94f17bda42daacbf8a849b7e3876095bb3d14dcf2520540bb8cb2092107ea98cf7e8ce9f2f5ab11b378d138a9746c1c3516");

                // RedEnvelopesOpen.doScene(1, 1, "10000387012016080660772888823", "", "", "wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=10000387012016080660772888823&sendusername=wangyi281364&ver=6&sign=1098df3d7cfdd5eaf586d3a9dc8ec87492889e7540edf6a1f7420590ef7b152bed5dfba917383d07cfce01e94de4d94f17bda42daacbf8a849b7e3876095bb3d14dcf2520540bb8cb2092107ea98cf7e8ce9f2f5ab11b378d138a9746c1c3516", "wangyi281364", null);
            }
            if (rb_VerifyPayPassword.Checked)
            {
                //NetSceneTenPay.TenPayCtrlSalt();
                //NetSceneTenPay.QueryBalance();
                NetSceneTenPay.VerifyPayPassword(tb_Contents.Text);
            }
            if (rb_like.Checked)
            {
                // SnsAsyncMgr.setLikeFlag(Convert.ToUInt64(tb_toUsername.Text),false);


            }
            if (rb_delComment.Checked)
            {
                SnsAsyncMgr.delComment(Convert.ToUInt64(tb_toUsername.Text), Convert.ToInt32(tb_Contents.Text));

            }

            if (rb_delsns.Checked)
            {
                SnsAsyncMgr.delete(Convert.ToUInt64(tb_toUsername.Text));
            }

            if (rb_SnsDetail.Checked)
            {

                new NetSceneSnsDetail().doScene(Convert.ToUInt64(tb_toUsername.Text));
                // NetSceneSnsUserPage.Instance.doScene(tb_toUsername.Text, false, Convert.ToUInt64(tb_Contents.Text), "53bb2752accbaf2a");

            }
            if (rb_TextMsg.Checked)
            {
               // ServiceCenter.sceneSendMsgOld.testSendMsg(tb_toUsername.Text, tb_Contents.Text, 10000);
                ServiceCenter.sceneSendMsgOld.SendOneMsg(tb_toUsername.Text, tb_Contents.Text, 1);



            }
            if (rb_AppMsg.Checked)
            {
                ServiceCenter.sendAppMsg.doSceneSendAppMsg(tb_toUsername.Text, 1, tb_Contents.Text);

            }
            if (rb_card.Checked)
            {
                ChatMsg chatMsgInfo = ServiceCenter.sceneSendMsg.buildChatMsg(tb_toUsername.Text, tb_Contents.Text, 0x2a);
                ServiceCenter.sceneSendMsg.doSendMsg(chatMsgInfo, 1);

            }
            if (rb_sns.Checked)
            {

                NetSceneSnsUserPage.Instance.getFirstPage(tb_toUsername.Text, 0);
            }
            if (rb_delContact.Checked)
            {   //删除好友
                OpLogMgr.OpDelContact(tb_toUsername.Text);
            }
            if (rb_loginbyphone.Checked)
            {
                if (tb_Contents.Text == "")
                {
                    ServiceCenter.sceneBindOpMobileForReg.doSceneBindSafeDevice(tb_toUsername.Text, tb_toUsername.Text);
                    //ServiceCenter.sceneBindOpMobileForReg.doScene("18363118008");
                    //ServiceCenter.sceneBindOpMobileForReg.doSceneDialForVerifyCode("478344");
                }
                else
                {
                    ServiceCenter.sceneBindOpMobileForReg.doSceneVerifyForBindSafeDevice(tb_Contents.Text);
                }
            }
            if (rb_GetContact.Checked)
            {
                List<string> userNameList = new List<string> {
                    tb_toUsername.Text
                };
                ServiceCenter.sceneBatchGetContact.doScene(userNameList);

            }
            if (rb_getkey.Checked)
            {

                new NetSceneGetA8Key().doScene(tb_toUsername.Text, GetA8KeyScene.MMGETA8KEY_SCENE_MSG, GetA8KeyOpCode.MMGETA8KEY_REDIRECT);

                //new NetSceneGetA8Key().doScene(tb_toUsername.Text, GetA8KeyScene.MMGETA8KEY_SCENE_OAUTH,GetA8KeyOpCode.MMGETA8KEY_REDIRECT);
                //
            }
            if (rb_addroomuser.Checked)
            {

                //ServiceCenter.sceneAddChatRoomMemberService.doScene(tb_toUsername.Text, new List<string> { tb_Contents.Text });
                new NSInviteChatRoomMember().doScene(tb_toUsername.Text, new List<string> { tb_Contents.Text });
            }

            if (rb_SearchContact.Checked)
            {

                //ServiceCenter.sceneAddChatRoomMemberService.doScene(tb_toUsername.Text, new List<string> { tb_Contents.Text });
                ServiceCenter.sceneSearchContact.doScene(tb_toUsername.Text);
            }

            //ServiceCenter.sceneAuth.doSceneWithVerify("3155852981", "ukebangv5", "ukebangv5", tb_toUsername.Text, tb_Contents.Text);

            if (rb_QuitChatRoom.Checked)
            {
                OpLogMgr.OpQuitChatRoom(tb_toUsername.Text);
            }


            if (rb_video.Checked)
            {
                ServiceCenter.sceneUploadVideo.test();
            }
            if (rb_cndvideo.Checked)
            {

                //ServiceCenter.sceneDownloadVideo.doSceneForThumb(cmdAM.MsgId, cmdAM.FromUserName.String, processAddMsg(cmdAM));

                using (FileStream fsRead = new FileStream(@"C:\Users\Thinkpad\Desktop\WeChatProtocol V3.0Video\WeChatProtocol\bin\Debug\ReplyRes\Video\1047401755.xml", FileMode.Open))
                {
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);
                    MsgTrans ts = new MsgTrans();
                    DownloadVideoContext info = new DownloadVideoContext();

                    string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                    DownloadVideoService.parseVideoMsgXML(myStr, ts, info);
                    //ts.strFromUserName = AccountMgr.getCurAccount().strUsrName;
                    ts.strToUserName = AccountMgr.getCurAccount().strUsrName;// "ntsafe-hkk";//AccountMgr.getCurAccount().strUsrName;
                    ts.nMsgSvrID = 1047401755;
                    ts.nTransType = 6;
                    info.mVideoInfo = ts;
                    info.mIsThumbMode = true;
                    // new NetSceneUploadCdnVideo().doSceneToCGI("ntsafe-hkk", ts, info, 0x3e);

                    //new NetSceneUploadCdnVideo().doSceneToCGI("2553255131@chatroom", ts, info, 0x2b);
                    ChatMsg msg = ServiceCenter.sceneSendMsg.buildChatMsg("ntsafe-hkk", myStr);
                    msg.nMsgType = 0x2b;
                    ServiceCenter.sceneDownloadVideo.doSceneForThumb(1047401755, AccountMgr.getCurAccount().strUsrName, msg);




                }
            }
            if (rb_delAllFriends.Checked)
            {
                ServiceCenter.asyncExec(delegate
            {
                for (int i = 0; i < RedisConfig._users.Count; i++)
                {


                    NetSceneSnsUserPage.Instance.getFirstPage(RedisConfig._users[i], 0);
                    Thread.Sleep(1000);


                }
            });

            }
            if (rb_open.Checked)
            {
                //new NetSceneRedEnvelopes("", "/cgi-bin/micromsg-bin/hongbao").doScene(tb_Contents.Text);

                // wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=10000387012016070870859620692&sendusername=ntsafe-hkk&ver=6&sign=a3defa85b27c730382f91fa4082df167cf5600b3b2da1f20c6787aabc6f87ede87258ff5ad96f54d20b4f09d185db52b02cfca53831bccf4954e9690bbe4be1aade86af810965ba4d52f8345505931887004c6830872d698fb87e4c233ab621a

                RedEnvelopesOpen.doScene(1, 1, "10000388012016080470474621869", "http://wx.qlogo.cn/mmhead/ver_1/sPIibS76d1RFRO96sibCoOBMVyDwVmILVY73tVQzjcettGdq0OptK6nyicKadukozwtogZ2FVo4liaHUK7A7m7ia3MeJMyjiazCGcicOa6MSqRzmZc/0", "test", "wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=10000388012016080470474621869&sendusername=ntsafe-hkk&ver=6&sign=c14c3daef04288cb0731599360b4b3042c3f7def3057ad1015e10b269dfa702588d58a9eeaa5956ea9544ba05c2846dc0b5b207e8104d9d90260b78935ffeb6c6710e3eeb0d9ad3d02db5b200be4577fe20d88a1bf28801f3c7d54de412c4144", "ntsafe-hkk", null);//wxid_bt7kure6c8jh22
            }

            //wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=10000389012016070870754916286&sendusername=ntsafe-hkk&ver=6&sign=80ee9b3971c2f7a20ee3943c2e4c147ea9d34969c9996d6e82181ea9261e4aefb0c466c2fd4fe8aa31a6de1136928534973f7fc56f4901c7a023ed1eed6e399173a16b966a680c1d3e1e327b205823052fb6e6397c7a2041a67797518ea19bbe
            // Log.i("count", Convert.ToString(SessionPackMgr.queueCount()));
        }
        private void onEventHandlerCheckLoginQrcode(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs != null)
            {


                if (evtArgs.mEventID == EventConst.ON_LOGIN_GETQRCODE)
                {
                    GetLoginQrcode qrcode = evtArgs.mObject as GetLoginQrcode;
                    if (qrcode.ImgBuf != null)
                    {

                        pB_ShowQrcode.BeginInvoke(new Action(() => pB_ShowQrcode.Image = Image.FromStream(new MemoryStream(qrcode.ImgBuf))));

                        new NetSceneCheckLoginQRCode().doScene(qrcode.Uuid, qrcode.NotifyKey);
                        qrcode.ImgBuf = null;

                    }
                }

                if (evtArgs.mEventID == EventConst.ON_LOGIN_CHECKQRCODE)
                {
                    CheckQrcode qrcode = evtArgs.mObject as CheckQrcode;

                    // Log.d("event", "event thread id " + Thread.CurrentThread.ManagedThreadId.ToString());
                    switch (qrcode.Status)
                    {
                        case 0:
                            lab_ShowMsg.BeginInvoke(new Action(() => lab_ShowMsg.Text = "未扫描 剩余时间" + qrcode.ExpiredTime.ToString() + "S"));
                            break;
                        case 1:
                            lab_ShowMsg.BeginInvoke(new Action(() => lab_ShowMsg.Text = "已扫描 未确认  剩余时间" + qrcode.ExpiredTime.ToString() + "S"));


                            using (WebClient _client = new WebClient())
                            {

                                pB_ShowQrcode.Image = Image.FromStream(new MemoryStream(_client.DownloadData(qrcode.HeadImgUrl)));
                            }

                            break;

                        case 2:
                            lab_ShowMsg.BeginInvoke(new Action(() => lab_ShowMsg.Text = "已确认 " + qrcode.Nickname));

                            //ServiceCenter.sceneAuth.QrcodeLogin(qrcode.Username, qrcode.Password);


                            EventCenter.removeEventWatcher(EventConst.ON_LOGIN_GETQRCODE, this.m_WatcherCheckLoginQrcode);
                            EventCenter.removeEventWatcher(EventConst.ON_LOGIN_CHECKQRCODE, this.m_WatcherCheckLoginQrcode);

                            new NetSceneNewAuth().doScene(qrcode.Username, qrcode.Password);

                            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\User\\" + qrcode.Username + "\\") == false)//如果不存在就创建file文件夹
                            {
                                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\User\\" + qrcode.Username);
                                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\User\\" + qrcode.Username + "\\" + ConstantsProtocol.ChatRoomPath);

                            }
                            break;
                        case 4:
                            lab_ShowMsg.BeginInvoke(new Action(() => lab_ShowMsg.Text = "已取消扫描"));
                            break;
                        default:
                            lab_ShowMsg.BeginInvoke(new Action(() => lab_ShowMsg.Text = "debug Status" + qrcode.Status.ToString() + "S"));
                            break;
                    }


                }
            }
        }
        private void btn_GetLoginQrcode_Click(object sender, EventArgs e)
        {

            EventCenter.registerEventWatcher(EventConst.ON_LOGIN_GETQRCODE, this.m_WatcherCheckLoginQrcode);
            EventCenter.registerEventWatcher(EventConst.ON_LOGIN_CHECKQRCODE, this.m_WatcherCheckLoginQrcode);
            new NetSceneGetLoginQRCode().doScene();
        }

        private void btn_SendBuf_Click(object sender, EventArgs e)
        {
            ServiceCenter.sceneSendMsgOld.testSendMsg(tb_toUsername.Text, tb_Contents.Text, 49);


        }

        private void btn_AutoAuth_Click(object sender, EventArgs e)
        {
            SessionPackMgr.putToHead(AuthPack.makeAutoAuthPack(2));
            // new NetSceneGetChatRoomMsg().doScene("7582614867@chatroom", 666591975);
            // new NetSceneInitContact().doScene(SessionPackMgr.getAccount().getUsername(),0,0);
        }

        private void btn_SendALL_Click(object sender, EventArgs e)
        {

            //ConstantsProtocol.JMP_URL = tb_JMP_URL.Text;
            //ConstantsProtocol.HB_CONTACT = tb_HB_CONTACT.Text;
            //new NetSceneInitContact().doScene(SessionPackMgr.getAccount().getUsername(), 0, 0);
            NSGetChatroomMemberDetail.doScene("2278561745@chatroom");

        }
    }
}