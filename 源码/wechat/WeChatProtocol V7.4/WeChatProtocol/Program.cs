using System;
using System.Collections.Generic;
using MicroMsg.Common.Event;
using MicroMsg.Common.Utils;
using MicroMsg.Scene;
using micromsg;
using MicroMsg.Protocol;
using MicroMsg.Manager;
using MicroMsg.Network;
using System.Threading;
using System.Windows.Forms;
using MicroMsg.Common.Timer;
using System.Text;
using System.IO;
using DBUtility;
using MicroMsg.Storage;
using WeChat.MicroMsg.Common.Utils;
using System.Net;

namespace WeChatProtocol
{


    class Program
    {

        private EventWatcher m_WatcherLoginByPhone;
        private EventWatcher m_WatcherLoginErr;
        private EventWatcher m_WatcherLoginNeedReg;
        private EventWatcher m_WatcherLoginNeedVCode;
        private EventWatcher m_WatcherLoginSuccess;
        private EventWatcher mAuthChangeWatcher;

        static void Main(string[] args)
        {

            //TimerObject mTimerObject = TimerService.addTimer(1, new EventHandler(ConstantsProtocol.onTimerDispatcher), 0, -1);
            //mTimerObject.start();

            // TimerSource.checkSystemTimer();

            //  List<FileInfo> infos = new List<FileInfo>();

            byte[] aesKey = Util.HexStringToByte("7E314A6C5372735546637C2540264374");
         
            //byte[] data = Util.HexStringToByte("7D5F1605082075FB71046442080F494B5A053B26BC14856200A7046B610001624B38C109472425D555B470E6CEE5B522E777151DE81DC245B6D73769271C8A3ECB3FAA2D7439E8E8705E92DDDB0CA4D07789DF30930340A1CF1C285B67E786F8FA7FA56EFFA406089F26F3EA84FEB1150C03C262DFF598BEA8F99825DD4A3B3ECE3211302223DB09EC97FFB4F77A4B");


            //using (WebClient _client = new WebClient())
            //{
            //    data= _client.UploadData("http://short.weixin.qq.com/cgi-bin/micromsg-bin/getchatroommemberdetail", "POST", data);
            //}
            //// GetChatroomMemberDetailResponse response = GetChatroomMemberDetailResponse.ParseFrom(data);


            //string a = Util.byteToHexStr(data);

            //  data = Util.ReadProtoRawData(data, 3);
            //// data = Util.ReadProtoRawData(data, 3);

            // string a = Util.byteToHexStr(data);
            // uint continueFlag = Util.ReadProtoInt(data, 2);
            // List<object> list = new List<object>();

            // List<object> msgList = new List<object>();
            // Util.ReadProtoRawDataS(list, data, 3);
            // foreach (var item in list)


            // {
            //     AddMsg msg = AddMsg.ParseFrom((byte[])item);
            //     msgList.Add(msg);
            // }


            ///   




            // string aesa = Util.byteToHexStr(data);
            //   string aesa = Util.byteToHexStr(data);
            //   new NetSceneBatchGetContact().doScene(new List<string> { "a","b" });
            // string str2 = Util.WCPaySignDES3Encode("1", "6BA3DAAA443A2BBB6311D7932B25F626");
            Program init = new Program();
            //UserData ud = new UserData { strUsername = "123123", strNickname = "aaaaa", roomid = "test1", roomname = "testsss" };
            //new MongodbHelper<UserData>("wx_userdata").Update(ud, j => j.strNickname == "abc" && j.roomid == "test1");
            // 7c0fffffff9a00000000000000000000000000000000000000bd0500000000
            //  MMTLVHeader head = new MMTLVHeader();

            // init.init(args[0],args[1],args[2],args[3]);
            init.init();
            //UserData item = new UserData { strUsername = "wxid_c8dv2ais6kj72", roomid = "2553255131@chatroom", signtime = 1462407000 };
            //MongodbHelper<UserData> helper = new MongodbHelper<UserData>("wx_users");
            ////helper.Update(item, i => i.strUsrName == item.strUsrName);
            //MongodbHelper<UserData> helper = new MongodbHelper<UserData>("wx_userdata");
            //int pagecount;
            //helper.Update(item, i => i.strUsername == item.strUsername && i.roomid == item.roomid);
            //Console.ReadKey();
            //int pagecount;

            ////获取名字里面带9的人数
            //var list = helper.List(10, i => i.signtime < 1462407000, i => i.signtime);

            ////var user_data = helper.Single(i => i.strUsername == "ntsafe-hkk" && i.roomid == "25532551311@chatroom");
            //UserData user = list[0];

        }
        // void init(string user,string pass,string deviceid,string headimg)
        void init()
        {
            if (m_WatcherLoginErr == null)
            {
                this.m_WatcherLoginErr = new EventWatcher(this, null, new EventHandlerDelegate(this.onEventHandlerLoginErr));
                this.m_WatcherLoginSuccess = new EventWatcher(this, null, new EventHandlerDelegate(this.onEventHandlerLoginSuccess));
                this.m_WatcherLoginNeedReg = new EventWatcher(this, null, new EventHandlerDelegate(this.onEventHandlerLoginNeedReg));
                this.m_WatcherLoginNeedVCode = new EventWatcher(this, null, new EventHandlerDelegate(this.onEventHandlerLoginNeedVCode));
                this.m_WatcherLoginByPhone = new EventWatcher(this, null, new EventHandlerDelegate(this.onPhoneAuthRegHandler));
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_ERR, this.m_WatcherLoginErr);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_SUCCESS, this.m_WatcherLoginSuccess);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDREG, this.m_WatcherLoginNeedReg);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDVERIFY, this.m_WatcherLoginNeedVCode);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS, this.m_WatcherLoginByPhone);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_ERR, this.m_WatcherLoginByPhone);


                this.mAuthChangeWatcher = new EventWatcher(this, null, new EventHandlerDelegate(this.on_AuthChange_EventHandler));
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTOAUTH_ERR, this.mAuthChangeWatcher);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDVERIFY, this.mAuthChangeWatcher);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTOAUTH_SUCCESS, this.mAuthChangeWatcher);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_SUCCESS, this.mAuthChangeWatcher);
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_ERR, this.mAuthChangeWatcher);
            }


            Log.dShowMsgLog = new Log.ShowConsoleMsg(showmsg);
            //AccountMgr.init();
            SessionPackMgr.setSessionKey(null);
            ServiceCenter.init();
            #region MyRegion
            RedisConfig.flag = false;

            //string result = httpReq("types=search&count=1&source=netease&pages=1&name=我的歌声里");

            //if (user == "wxid_iq8pekho1lgw52" || user == "ntsafe-hkk")
            //{
            //    RedisConfig.userflag = true;
            //}
            //else
            //{
            //    RedisConfig.userflag = false;
            //}

            //RedisConfig.headimg = headimg;
            //Util.gDeviceID = Util.HexStringToByte(deviceid);
            //Util.gDeviceID = Encoding.Default.GetBytes(Util.getDeviceUniqueId());
            //ServiceCenter.sceneAuth.doScene("192801941", "fu4ku.6588");
            //ServiceCenter.sceneAuth.doScene("15309960972", "cw19730207");
            //ServiceCenter.sceneAuth.doSceneWithVerify("13035361423", "lhj19900302", "lhj19900302", "h017e5d3a62e63f70092f9e384772eda6f09b6adba17169fba8d00501152c4fa9453052d5814af90dc8", "wnxm");
            //ServiceCenter.sceneAuth.doScene("+541124098345", "m1raculous");

            // ServiceCenter.sceneAuth.doScene("q10519", "fu4ku.");
            //ServiceCenter.sceneAuth.doScene("98319868", "98319868");
            //ServiceCenter.sceneAuth.doScene("p0ny1213", "199312130220.c");


            //Console.WriteLine((long)12/100);

            //  ServiceCenter.sceneAuth.QrcodeLogin(user, pass);
            //ServiceCenter.sceneAuth.QrcodeLogin("wxid_iq8pekho1lgw52", "extdevnewpwd_CiNBZERCdllicGhfeVJsY28tcmNVeGtvN0JAcXJ0aWNrZXRfMBJAQWFwX3J5akRCZjZyaXNKQnJsSTVReXVFcDJGUnRMYllGcmZocWU4MGpCZS1EdFBoeDZZRWthNm1FcU9NUGJrSBoYZ1NkanVjOUFSMWdUc2NYNE1waVBBeUVk");
            #endregion
            //TimerSource.checkSystemTimer();
            Thread thread2 = new Thread(threadPro);//创建新线程  
            thread2.Start();

        }
        private void showmsg(string inde, string msg)
        {

            switch (inde)
            {
                case " -i ":
                    if (RedisConfig.flag == false)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(msg);
                    }
                    break;

                case " -d ":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(msg);
                    break;

                case " -e ":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    break;
                case " -w ":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    break;
                case " -a ":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    break;
                case " -v ":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    break;
            }
        }


        private void onEventHandlerLoginErr(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            int mObject = (int)evtArgs.mObject;
            if (-6 != mObject)
            {
                if (-205 == mObject)
                {
                    if (evtArgs == null)
                    {
                        return;
                    }
                    string strNum = evtArgs.mObject1 as string;
                    //NewDeviceVerifyPage.GotoThisPage(strNum, new MMUIEvent(this.verifySafeDeviceCb), false, false);
                }
                else if (-100 == mObject)
                {
                    // MMMessageBox.Show(AuthPack.mKickMessage, true, "确定");
                }
                else
                {
                    // InputVCodeControl.DismissInputVcodeBox();
                    //this.processLoginErr(evtArgs);
                }
            }
            // this.enableAllControls();
        }
        private void onEventHandlerLoginSuccess(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            // InputVCodeControl.DismissInputVcodeBox();
            // PageManager.getInstance().PopAllPages(true);
            AuthResponse mObject = null;
            if ((evtArgs.mObject != null) && (evtArgs.mObject is AuthResponse))
            {
                mObject = (AuthResponse)evtArgs.mObject;
            }
            //GConfigMgr.settings.strLastLoginName = this.textBoxAccount.get_Text();
            //GConfigMgr.saveSetting();
            Log.i("onEventHandlerLoginSuccess", "login success");

        }
        private void onEventHandlerLoginNeedReg(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            Log.i("onEventHandlerLoginNeedReg", "onEventHandlerLoginNeedReg");
        }
        private void onEventHandlerLoginNeedVCode(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            Log.i("onEventHandlerLoginNeedVCode", "onEventHandlerLoginNeedVCode");
        }
        private void onPhoneAuthRegHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs != null)
            {
                //his.enableAllControls();
                if (evtArgs.mEventID == EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS)
                {
                    //RegByPhoneStepInputNum.sStrNameToLogin = sStrNameToLogin;
                    //base.GoToPage("/Source/UI/LoginViews/RegByPhoneStepInputVCode.xaml", new object[] { sStrNameToLogin }, false);
                    Log.i("ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS", "ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS");
                }
                else if (evtArgs.mEventID == EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_ERR)
                {
                    RetConst mObject = RetConst.MM_OK;
                    if (evtArgs.mObject != null)
                    {
                        mObject = (RetConst)evtArgs.mObject;
                        Log.d("LoginMainView", "onPhoneAuthRegHandler:evtArgs.mObject is" + mObject);
                        Log.i("ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS", "ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS");
                        switch (mObject)
                        {
                            case RetConst.MM_ERR_CLIENT:
                                //MMMessageBox.Show(strings.Reg_PhoneNum_MsgBadNet, true, "确定");
                                return;

                            case RetConst.MM_ERR_MOBILE_NEEDADJUST:
                                //MMMessageBox.Show(strings.PCon_Bind_Msg_BindPhone_NeedAdjust, true, "确定");
                                return;

                            case RetConst.MM_ERR_USER_BIND_MOBILE:
                            case RetConst.MM_ERR_MOBILE_BINDED:
                                // base.GoToPage("/Source/UI/LoginViews/LogByPhonePassword.xaml", null, false);
                                return;

                            case RetConst.MM_ERR_MOBILE_FORMAT:
                                // MMMessageBox.Show(strings.PCon_Bind_Msg_BindPhone_FormatTip, true, "确定");
                                return;

                            case RetConst.MM_ERR_FREQ_LIMITED:
                                //MMMessageBox.Show(strings.Reg_PhoneNum_MsgTooFreq, true, "确定");
                                return;
                        }
                        //MMMessageBox.Show(strings.Reg_PhoneNum_MsgBadNum, true, "确定");
                    }
                    else
                    {
                        Log.d("LoginMainView", "onPhoneAuthRegHandler:evtArgs.mObject is null");
                    }
                }
            }
        }

        public void on_AuthChange_EventHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs != null)
            {
                if ((EventConst.ON_NETSCENE_AUTOAUTH_ERR == evtArgs.mEventID) || (EventConst.ON_NETSCENE_AUTH_ERR == evtArgs.mEventID))
                {
                    //InputVCodeControl.DismissInputVcodeBox();
                    int mObject = (int)evtArgs.mObject;
                    switch (mObject)
                    {
                        case -100:
                            Log.i(base.GetType().Name, string.Concat(new object[] { "kickout  errorId:", mObject, ",kickMsg = ", AuthPack.mKickMessage }));
                            //MMMessageBox.Show(AuthPack.mKickMessage, true, "确定").ReturnEvent = new DlgReturnEvent(this.AuthErroMsgBoxHandler);
                            return;

                        case -999999:
                            //App.ExitProgram();
                            return;

                        case -205:
                            // if (!PageManager.getInstance().ExsitPage<NewDeviceVerifyPage>())
                            // {
                            //     AccountMgr.SetLoginStatus(false);
                            //     LoginMainView.sStrUserInputUserName = AccountMgr.strUsrName;
                            //     NewDeviceVerifyPage.GotoThisPage(AccountMgr.strBindMobile, new MMUIEvent(this.verifySafeDeviceCb), false, true);
                            //     return;
                            // }
                            return;
                    }
                    //MMMessageBox.Show(strings.MainPage_MsgReLogin, true, "确定").ReturnEvent = new DlgReturnEvent(this.AuthErroMsgBoxHandler);
                }
                else if ((EventConst.ON_NETSCENE_AUTOAUTH_SUCCESS == evtArgs.mEventID) || (EventConst.ON_NETSCENE_AUTH_SUCCESS == evtArgs.mEventID))
                {
                    NetSceneNewSync.isEnable = true;
                    // EventCenter.postEvent(EventConst.ON_APP_ACTIVE, null, null);
                    ServiceCenter.sceneNewSync.doScene(7, syncScene.MM_NEWSYNC_SCENE_OTHER);

                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_ERR, this.m_WatcherLoginErr);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_SUCCESS, this.m_WatcherLoginSuccess);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDREG, this.m_WatcherLoginNeedReg);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDVERIFY, this.m_WatcherLoginNeedVCode);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS, this.m_WatcherLoginByPhone);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_ERR, this.m_WatcherLoginByPhone);

                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTOAUTH_ERR, this.mAuthChangeWatcher);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDVERIFY, this.mAuthChangeWatcher);
                    //EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTOAUTH_SUCCESS, this.mAuthChangeWatcher);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_SUCCESS, this.mAuthChangeWatcher);
                    EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_AUTH_ERR, this.mAuthChangeWatcher);





                    //InputVCodeControl.DismissInputVcodeBox();
                }
                else if ((EventConst.ON_NETSCENE_AUTH_NEEDVERIFY == evtArgs.mEventID) && (evtArgs != null))
                {
                    // if (InputVCodeControl.IsShown())
                    // {
                    //InputVCodeControl.SetInputVCodeBoxWrongStage(evtArgs);
                    VerifyCodeArgs mObject = evtArgs.mObject as VerifyCodeArgs;
                    using (FileStream fs = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + mObject.mImageSid + ".png", FileMode.Create))
                    {


                        fs.Write(mObject.mImageBuf, 0, mObject.mImageBuf.Length);
                        fs.Close();
                    }
                    // }
                    // else
                    // {
                    //     InputVCodeControl.Show(true, evtArgs, null, null, null, null);
                    // }
                    return;
                }
            }
        }



        private void threadPro()
        {
            // MethodInvoker MethInvo = new MethodInvoker(SHOw);
            //BeginInvoke(MethInvo);
            //MethInvo.BeginInvoke(null,null);
            sendForm from = new sendForm();
            from.ShowDialog();

        }
    }
}
