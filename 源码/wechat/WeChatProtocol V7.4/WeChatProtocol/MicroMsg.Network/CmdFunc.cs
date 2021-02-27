namespace MicroMsg.Network
{
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;

    public class CmdFunc
    {
        public const string CMDURI_ADDCHATROOMMEMBER_REQ = "/cgi-bin/micromsg-bin/addchatroommember";
        public const string CMDURI_ADDGROUP_URL = "/cgi-bin/micromsg-bin/addgroupcard";
        public const string CMDURI_ADDSAFEDEVICE_URL = "/cgi-bin/micromsg-bin/addsafedevice";
        public const string CMDURI_AUTH_REQ = "/cgi-bin/micromsg-bin/auth";
        public const string CMDURI_BAKCHAT_RECOVER_DATA_URL = "/cgi-bin/micromsg-bin/bakchatrecoverdata";
        public const string CMDURI_BAKCHAT_RECOVER_DELETE_URL = "/cgi-bin/micromsg-bin/bakchatdelete";
        public const string CMDURI_BAKCHAT_RECOVER_GETLIST_URL = "/cgi-bin/micromsg-bin/bakchatrecovergetlist";
        public const string CMDURI_BAKCHAT_RECOVER_HEAD_URL = "/cgi-bin/micromsg-bin/bakchatrecoverhead";
        public const string CMDURI_BAKCHATUPLOADEND_URL = "/cgi-bin/micromsg-bin/bakchatuploadend";
        public const string CMDURI_BAKCHATUPLOADHEAD_URL = "/cgi-bin/micromsg-bin/bakchatuploadhead";
        public const string CMDURI_BAKCHATUPLOADMEDIA_URL = "/cgi-bin/micromsg-bin/bakchatuploadmedia";
        public const string CMDURI_BAKCHATUPLOADMSG_URL = "/cgi-bin/micromsg-bin/bakchatuploadmsg";
        public const string CMDURI_BATCH_GET_SHAKE_TRAN_IMG_REQ = "/cgi-bin/micromsg-bin/batchgetshaketranimg";
        public const string CMDURI_BATCHGETCONTACTPROFILE_REQ = "/cgi-bin/micromsg-bin/batchgetcontactprofile";
        public const string CMDURI_BINDOP_MOBILE = "/cgi-bin/micromsg-bin/bindopmobile";
        public const string CMDURI_BINDOP_MOBILE_FOR_REG = "/cgi-bin/micromsg-bin/bindopmobileforreg";
        public const string CMDURI_BINDQQ_URL = "/cgi-bin/micromsg-bin/bindqq";
        public const string CMDURI_BINDSAFEMOBILE_URL = "/cgi-bin/micromsg-bin/bindsafemobile";
        public const string CMDURI_CREATECHATROOM_REQ = "/cgi-bin/micromsg-bin/createchatroom";
        public const string CMDURI_DEL_BOTTLE = "/cgi-bin/micromsg-bin/deletebottle";
        public const string CMDURI_DEL_CHATROOMMEMBER_URL = "/cgi-bin/micromsg-bin/delchatroommember";
        public const string CMDURI_DELETESAFEDEVICE_URL = "/cgi-bin/micromsg-bin/delsafedevice";
        public const string CMDURI_DOWNLOAD_APP_ATTACH_REQ = "/cgi-bin/micromsg-bin/downloadappattach";
        public const string CMDURI_DOWNLOAD_HEADIMG_URL = "/cgi-bin/micromsg-bin/gethdheadimg";
        public const string CMDURI_DOWNLOAD_PACKAGE_URL = "/cgi-bin/micromsg-bin/downloadpackage";
        public const string CMDURI_DOWNLOADVIDEO_REQ = "/cgi-bin/micromsg-bin/downloadvideo";
        public const string CMDURI_DOWNLOADVOICE_REQ = "/cgi-bin/micromsg-bin/downloadvoice";
        public const string CMDURI_EXPOSE_REQ = "/cgi-bin/micromsg-bin/expose";
        public const string CMDURI_FACEBOOK_AUTH_URL = "/cgi-bin/micromsg-bin/facebookauth";
        public const string CMDURI_FEED_BACK_URL = "/cgi-bin/micromsg-bin/sendfeedback";
        public const string CMDURI_GENERALSET_REQ = "/cgi-bin/micromsg-bin/generalset";
        public const string CMDURI_GET_A8KEY = "/cgi-bin/micromsg-bin/geta8key";
        public const string CMDURI_GET_APP_INFO_REQ = "/cgi-bin/micromsg-bin/getappinfo";
        public const string CMDURI_GET_BOTTLE_COUNT = "/cgi-bin/micromsg-bin/getbottlecount";
        public const string CMDURI_GET_PSM_IMG_URL = "/cgi-bin/micromsg-bin/getpsmimg";
        public const string CMDURI_GET_QRCODE_REQ = "/cgi-bin/micromsg-bin/getqrcode";
        public const string CMDURI_GET_RECOMMEND_APPLIST = "/cgi-bin/micromsg-bin/getrecommendapplist";
        public const string CMDURI_GET_REPORT_STRATEGY_URL = "/cgi-bin/micromsg-bin/reportstrategy";
        public const string CMDURI_GET_WEIBO_URL = "/cgi-bin/micromsg-bin/getwburl";
        public const string CMDURI_GETCDNDNS_URL = "/cgi-bin/micromsg-bin/getcdndns";
        public const string CMDURI_GETCONTACT_REQ = "/cgi-bin/micromsg-bin/getcontact";
        public const string CMDURI_GETINVITEFRIEND_REQ = "/cgi-bin/micromsg-bin/getinvitefriend";
        public const string CMDURI_GETMFRIEND_URL = "/cgi-bin/micromsg-bin/getmfriend";
        public const string CMDURI_GETMSGIMG_REQ = "/cgi-bin/micromsg-bin/getmsgimg";
        public const string CMDURI_GETPACKAGELIST_URL = "/cgi-bin/micromsg-bin/getpackagelist";
        public const string CMDURI_GETPROFILE_URL = "/cgi-bin/micromsg-bin/getprofile";
        public const string CMDURI_GETQQGROUP_REQ = "/cgi-bin/micromsg-bin/getqqgroup";
        public const string CMDURI_GETUPDATEINFO_REQ = "/cgi-bin/micromsg-bin/getupdateinfo";
        public const string CMDURI_GETUPDATEPACK_REQ = "/cgi-bin/micromsg-bin/getupdatepack";
        public const string CMDURI_GETUSERIMG_REQ = "/cgi-bin/micromsg-bin/batchgetheadimg";
        public const string CMDURI_GETUSERNAME_REQ = "/cgi-bin/micromsg-bin/getusername";
        public const string CMDURI_GETVERIFYIMG_REQ = "/cgi-bin/micromsg-bin/getverifyimg";
        public const string CMDURI_GETVUSERINFO_REQ = "/cgi-bin/micromsg-bin/getvuserinfo";
        public const string CMDURI_INIT_REQ = "/cgi-bin/micromsg-bin/init";
        public const string CMDURI_KVREPORT_URL = "/cgi-bin/micromsg-bin/kvreport";
        public const string CMDURI_LBSFIND_URL = "/cgi-bin/micromsg-bin/lbsfind";
        public const string CMDURI_LOGOUT_WEBWX = "/cgi-bin/micromsg-bin/logoutwebwx";
        public const string CMDURI_MASS_SEND_REQ = "/cgi-bin/micromsg-bin/masssend";
        public const string CMDURI_NEWINIT_REQ = "/cgi-bin/micromsg-bin/newinit";
        public const string CMDURI_NEWREG_REQ = "/cgi-bin/micromsg-bin/newreg";
        public const string CMDURI_NEWSYNC_REQ = "/cgi-bin/micromsg-bin/newsync";
        public const string CMDURI_OPEN_BOTTLE = "/cgi-bin/micromsg-bin/openbottle";
        public const string CMDURI_PICK_BOTTLE = "/cgi-bin/micromsg-bin/pickbottle";
        public const string CMDURI_PUSHCHANNEL_REG_URL = "/cgi-bin/micromsg-bin/winphonereg";
        public const string CMDURI_PUSHCHANNEL_UNREG_URL = "/cgi-bin/micromsg-bin/winphoneunreg";
        public const string CMDURI_RECEIVE_EMOJI = "/cgi-bin/micromsg-bin/receiveemoji";
        public const string CMDURI_REPORT_CLIENTPERF = "/cgi-bin/micromsg-bin/clientperfreport";
        public const string CMDURI_RESET_PWD_URL = "/cgi-bin/micromsg-bin/getresetpwdurl";
        public const string CMDURI_SEARCHCONTACT_REQ = "/cgi-bin/micromsg-bin/searchcontact";
        public const string CMDURI_SEARCHFRIEND_REQ = "/cgi-bin/micromsg-bin/searchfriend";
        public const string CMDURI_SEND_APP_MSG_REQ = "/cgi-bin/micromsg-bin/sendappmsg";
        public const string CMDURI_SEND_EMOJI = "/cgi-bin/micromsg-bin/sendemoji";
        public const string CMDURI_SENDCARD_REQ = "/cgi-bin/micromsg-bin/sendcard";
        public const string CMDURI_SENDINVITEMAIL_REQ = "/cgi-bin/micromsg-bin/sendinvitemail";
        public const string CMDURI_SENDMSG_REQ = "/cgi-bin/micromsg-bin/sendmsg";
        public const string CMDURI_SHAKE_IMG_URL = "/cgi-bin/micromsg-bin/shakeimg";
        public const string CMDURI_SHAKE_TRAN_IMG_GET_REQ = "/cgi-bin/micromsg-bin/shaketranimgget";
        public const string CMDURI_SHAKE_TRAN_IMG_REPORT_REQ = "/cgi-bin/micromsg-bin/shaketranimgreport";
        public const string CMDURI_SHAKE_TRAN_IMG_UNBIND_REQ = "/cgi-bin/micromsg-bin/shaketranimgunbind";
        public const string CMDURI_SHAKEGET_REQ = "/cgi-bin/micromsg-bin/shakeget";
        public const string CMDURI_SHAKEREPORT_REQ = "/cgi-bin/micromsg-bin/shakereport";
        public const string CMDURI_SNS_COMMENT_REQ = "/cgi-bin/micromsg-bin/snscomment";
        public const string CMDURI_SNS_OBJECT_OP_REQ = "/cgi-bin/micromsg-bin/snsobjectop";
        public const string CMDURI_SNS_OBJECTDETAIL_REQ = "/cgi-bin/micromsg-bin/snsobjectdetail";
        public const string CMDURI_SNS_POST_REQ = "/cgi-bin/micromsg-bin/snspost";
        public const string CMDURI_SNS_SYNC_REQ = "/cgi-bin/micromsg-bin/snssync";
        public const string CMDURI_SNS_TAG_LIST_REQ = "/cgi-bin/micromsg-bin/snstaglist";
        public const string CMDURI_SNS_TAG_MEMBER_MUTIL_SET_REQ = "/cgi-bin/micromsg-bin/snstagmemmutilset";
        public const string CMDURI_SNS_TAG_MEMBER_OP_REQ = "/cgi-bin/micromsg-bin/snstagmemberoption";
        public const string CMDURI_SNS_TAG_OP_REQ = "/cgi-bin/micromsg-bin/snstagoption";
        public const string CMDURI_SNS_TIME_LINE_REQ = "/cgi-bin/micromsg-bin/snstimeline";
        public const string CMDURI_SNS_UPLOAD_REQ = "/cgi-bin/micromsg-bin/snsupload";
        public const string CMDURI_SNS_USER_PAGE_REQ = "/cgi-bin/micromsg-bin/snsuserpage";
        public const string CMDURI_STATUS_NOTIFY = "/cgi-bin/micromsg-bin/statusnotify";
        public const string CMDURI_SWITCH_PUSHMAIL = "/cgi-bin/micromsg-bin/switchpushmail";
        public const string CMDURI_SYNC_REQ = "/cgi-bin/micromsg-bin/sync";
        public const string CMDURI_THROW_BOTTLE = "/cgi-bin/micromsg-bin/throwbottle";
        public const string CMDURI_UPDATESAFEDEVICE_URL = "/cgi-bin/micromsg-bin/updatesafedevice";
        public const string CMDURI_UPLOAD_APP_ATTACH_REQ = "/cgi-bin/micromsg-bin/uploadappattach";
        public const string CMDURI_UPLOAD_HEADIMG_URL = "/cgi-bin/micromsg-bin/uploadhdheadimg";
        public const string CMDURI_UPLOADMCONTACT_URL = "/cgi-bin/micromsg-bin/uploadmcontact";
        public const string CMDURI_UPLOADMSGIMG_REQ = "/cgi-bin/micromsg-bin/uploadmsgimg";
        public const string CMDURI_UPLOADVIDEO_REQ = "/cgi-bin/micromsg-bin/uploadvideo";
        public const string CMDURI_UPLOADVOICE_REQ = "/cgi-bin/micromsg-bin/uploadvoice";
        public const string CMDURI_VERIFYUSER = "/cgi-bin/micromsg-bin/verifyuser";
        public const string CMDURI_VOIP_ANSWER_REQ = "/cgi-bin/micromsg-bin/voipanswer";
        public const string CMDURI_VOIP_CANCELINVITE_REQ = "/cgi-bin/micromsg-bin/voipcancelinvite";
        public const string CMDURI_VOIP_HEARTBEAT_REQ = "/cgi-bin/micromsg-bin/voipheartbeat";
        public const string CMDURI_VOIP_INVITE_REQ = "/cgi-bin/micromsg-bin/voipinvite";
        public const string CMDURI_VOIP_INVITEREMIND_REQ = "/cgi-bin/micromsg-bin/voipinviteremind";
        public const string CMDURI_VOIP_SHUTDOWN_REQ = "/cgi-bin/micromsg-bin/voipshutdown";
        public const string CMDURI_VOIP_SYNC_REQ = "/cgi-bin/micromsg-bin/voipsync";
        private static Dictionary<int, MMFuncConst> mapMMFunc;

        private static void AddItem(string cgi, MMFuncConst func)
        {
            mapMMFunc.Add(cgi.GetHashCode(), func);
        }

        private static void checkMMFuncMap()
        {
            if (mapMMFunc == null)
            {
                mapMMFunc = new Dictionary<int, MMFuncConst>();
                AddItem("/cgi-bin/micromsg-bin/auth", MMFuncConst.MMFunc_Auth);
                AddItem("/cgi-bin/micromsg-bin/sendmsg", MMFuncConst.MMFunc_SendMsg);
                AddItem("/cgi-bin/micromsg-bin/sync", MMFuncConst.MMFunc_Sync);
                AddItem("/cgi-bin/micromsg-bin/uploadmsgimg", MMFuncConst.MMFunc_UploadMsgImg);
                AddItem("/cgi-bin/micromsg-bin/getmsgimg", MMFuncConst.MMFunc_GetMsgImg);
                AddItem("/cgi-bin/micromsg-bin/init", MMFuncConst.MMFunc_Init);
                AddItem("/cgi-bin/micromsg-bin/getupdatepack", MMFuncConst.MMFunc_GetUpdatePack);
                AddItem("/cgi-bin/micromsg-bin/searchfriend", MMFuncConst.MMFunc_SearchFriend);
                AddItem("/cgi-bin/micromsg-bin/getinvitefriend", MMFuncConst.MMFunc_GetInviteFriend);
                AddItem("/cgi-bin/micromsg-bin/uploadvoice", MMFuncConst.MMFunc_UploadVoice);
                AddItem("/cgi-bin/micromsg-bin/downloadvoice", MMFuncConst.MMFunc_DownloadVoice);
                AddItem("/cgi-bin/micromsg-bin/newinit", MMFuncConst.MMFunc_NewInit);
                AddItem("/cgi-bin/micromsg-bin/newsync", MMFuncConst.MMFunc_NewSync);
                AddItem("/cgi-bin/micromsg-bin/newreg", MMFuncConst.MMFunc_NewReg);
                AddItem("/cgi-bin/micromsg-bin/getusername", MMFuncConst.MMFunc_GetUserName);
                AddItem("/cgi-bin/micromsg-bin/batchgetcontactprofile", MMFuncConst.MMFunc_BatchGetContactProfile);
                AddItem("/cgi-bin/micromsg-bin/batchgetheadimg", MMFuncConst.MMFunc_BatchGetHeadImg);
                AddItem("/cgi-bin/micromsg-bin/getupdateinfo", MMFuncConst.MMFunc_GetUpdateInfo);
                AddItem("/cgi-bin/micromsg-bin/addchatroommember", MMFuncConst.MMFunc_AddChatRoomMember);
                AddItem("/cgi-bin/micromsg-bin/createchatroom", MMFuncConst.MMFunc_CreateChatRoom);
                AddItem("/cgi-bin/micromsg-bin/getverifyimg", MMFuncConst.MMFunc_GetVerifyImg);
                AddItem("/cgi-bin/micromsg-bin/getqqgroup", MMFuncConst.MMFunc_GetQQGroup);
                AddItem("/cgi-bin/micromsg-bin/verifyuser", MMFuncConst.MMFunc_VerifyUser);
                AddItem("/cgi-bin/micromsg-bin/searchcontact", MMFuncConst.MMFunc_SearchContact);
                AddItem("/cgi-bin/micromsg-bin/sendcard", MMFuncConst.MMFunc_SendCard);
                AddItem("/cgi-bin/micromsg-bin/sendinvitemail", MMFuncConst.MMFunc_SendInviteEmail);
                AddItem("/cgi-bin/micromsg-bin/uploadvideo", MMFuncConst.MMFunc_UploadVideo);
                AddItem("/cgi-bin/micromsg-bin/downloadvideo", MMFuncConst.MMFunc_DownloadVideo);
                AddItem("/cgi-bin/micromsg-bin/shakereport", MMFuncConst.MMFunc_ShakeReport);
                AddItem("/cgi-bin/micromsg-bin/shakeget", MMFuncConst.MMFunc_ShakeGet);
                AddItem("/cgi-bin/micromsg-bin/expose", MMFuncConst.MMFunc_Expose);
                AddItem("/cgi-bin/micromsg-bin/generalset", MMFuncConst.MMFunc_GeneralSet);
                AddItem("/cgi-bin/micromsg-bin/masssend", MMFuncConst.MMFunc_MassSend);
                AddItem("/cgi-bin/micromsg-bin/getvuserinfo", MMFuncConst.MMFunc_GetVUserInfo);
                AddItem("/cgi-bin/micromsg-bin/bindopmobileforreg", MMFuncConst.MMFunc_BindMobileForReg);
                AddItem("/cgi-bin/micromsg-bin/bindopmobile", MMFuncConst.MMFunc_BindOpMobile);
                AddItem("/cgi-bin/micromsg-bin/getresetpwdurl", MMFuncConst.MMFunc_GetResetPwdUrl);
                AddItem("/cgi-bin/micromsg-bin/bindqq", MMFuncConst.MMFunc_BindQQ);
                AddItem("/cgi-bin/micromsg-bin/lbsfind", MMFuncConst.MMFunc_LbsFind);
                AddItem("/cgi-bin/micromsg-bin/addgroupcard", MMFuncConst.MMFunc_AddGroupCard);
                AddItem("/cgi-bin/micromsg-bin/uploadhdheadimg", MMFuncConst.MMFunc_UploadHDHeadImg);
                AddItem("/cgi-bin/micromsg-bin/gethdheadimg", MMFuncConst.MMFunc_GetHDHeadImg);
                AddItem("/cgi-bin/micromsg-bin/sendfeedback", MMFuncConst.MMFunc_SendFeedback);
                AddItem("/cgi-bin/micromsg-bin/winphonereg", MMFuncConst.MMFunc_WinphoneReg);
                AddItem("/cgi-bin/micromsg-bin/winphoneunreg", MMFuncConst.MMFunc_WinphoneUnReg);
                AddItem("/cgi-bin/micromsg-bin/shakeimg", MMFuncConst.MMFunc_ShakeImg);
                AddItem("/cgi-bin/micromsg-bin/getbottlecount", MMFuncConst.MMFunc_GetBottleCount);
                AddItem("/cgi-bin/micromsg-bin/openbottle", MMFuncConst.MMFunc_OpenBottle);
                AddItem("/cgi-bin/micromsg-bin/pickbottle", MMFuncConst.MMFunc_PickBottle);
                AddItem("/cgi-bin/micromsg-bin/throwbottle", MMFuncConst.MMFunc_ThrowBottle);
                AddItem("/cgi-bin/micromsg-bin/getpsmimg", MMFuncConst.MMFunc_GetPSMImg);
                AddItem("/cgi-bin/micromsg-bin/getpackagelist", MMFuncConst.MMFunc_GetPackageList);
                AddItem("/cgi-bin/micromsg-bin/downloadpackage", MMFuncConst.MMFunc_DownloadPackage);
                AddItem("/cgi-bin/micromsg-bin/getmfriend", MMFuncConst.MMFunc_GetMFriend);
                AddItem("/cgi-bin/micromsg-bin/uploadmcontact", MMFuncConst.MMFunc_UploadMContact);
                AddItem("/cgi-bin/micromsg-bin/getcontact", MMFuncConst.MMFunc_GetContact);
                AddItem("/cgi-bin/micromsg-bin/facebookauth", MMFuncConst.MMFunc_FaceBookAuth);
                AddItem("/cgi-bin/micromsg-bin/getqrcode", MMFuncConst.MMFunc_GetQRCode);
                AddItem("/cgi-bin/micromsg-bin/snstimeline", MMFuncConst.MMFunc_MMSnsTimeLine);
                AddItem("/cgi-bin/micromsg-bin/snsuserpage", MMFuncConst.MMFunc_MMSnsUserPage);
                AddItem("/cgi-bin/micromsg-bin/snsupload", MMFuncConst.MMFunc_MMSnsUpload);
                AddItem("/cgi-bin/micromsg-bin/snspost", MMFuncConst.MMFunc_MMSnsPost);
                AddItem("/cgi-bin/micromsg-bin/snssync", MMFuncConst.MMFunc_MMSnsSync);
                AddItem("/cgi-bin/micromsg-bin/snsobjectop", MMFuncConst.MMFunc_MMSnsObjectOp);
                AddItem("/cgi-bin/micromsg-bin/snscomment", MMFuncConst.MMFunc_MMSnsComment);
                AddItem("/cgi-bin/micromsg-bin/snsobjectdetail", MMFuncConst.MMFunc_MMSnsObjectDetail);
                AddItem("/cgi-bin/micromsg-bin/geta8key", MMFuncConst.MMFunc_GetA8Key);
                AddItem("/cgi-bin/micromsg-bin/logoutwebwx", MMFuncConst.MMFunc_LogOutWebWx);
                AddItem("/cgi-bin/micromsg-bin/statusnotify", MMFuncConst.MMFunc_StatusNotify);
                AddItem("/cgi-bin/micromsg-bin/sendappmsg", MMFuncConst.MMFunc_SendAppMsg);
                AddItem("/cgi-bin/micromsg-bin/getappinfo", MMFuncConst.MMFunc_GetAppInfo);
                AddItem("/cgi-bin/micromsg-bin/uploadappattach", MMFuncConst.MMFunc_UploadAppAttach);
                AddItem("/cgi-bin/micromsg-bin/downloadappattach", MMFuncConst.MMFunc_DownloadAppAttach);
                AddItem("/cgi-bin/micromsg-bin/snstagoption", MMFuncConst.MMFunc_MMSnsTagOption);
                AddItem("/cgi-bin/micromsg-bin/snstagmemberoption", MMFuncConst.MMFunc_MMSnsTagMemberOption);
                AddItem("/cgi-bin/micromsg-bin/snstaglist", MMFuncConst.MMFunc_MMSnsTagList);
                AddItem("/cgi-bin/micromsg-bin/snstagmemmutilset", MMFuncConst.MMFunc_MMSnsTagMemMutilSet);
                AddItem("/cgi-bin/micromsg-bin/shaketranimgreport", MMFuncConst.MMFunc_ShakeTranImgReport);
                AddItem("/cgi-bin/micromsg-bin/shaketranimgget", MMFuncConst.MMFunc_ShakeTranImgGet);
                AddItem("/cgi-bin/micromsg-bin/batchgetshaketranimg", MMFuncConst.MMFunc_BatchGetShakeTranImg);
                AddItem("/cgi-bin/micromsg-bin/shaketranimgunbind", MMFuncConst.MMFunc_ShakeTranImgUnBind);
                AddItem("/cgi-bin/micromsg-bin/clientperfreport", MMFuncConst.MMFunc_ReportClntPerf);
                AddItem("/cgi-bin/micromsg-bin/bakchatuploadhead", MMFuncConst.MMFunc_BakChatUploadHead);
                AddItem("/cgi-bin/micromsg-bin/bakchatuploadmsg", MMFuncConst.MMFunc_BakChatUploadMsg);
                AddItem("/cgi-bin/micromsg-bin/bakchatuploadmedia", MMFuncConst.MMFunc_BakChatUploadMedia);
                AddItem("/cgi-bin/micromsg-bin/bakchatuploadend", MMFuncConst.MMFunc_BakChatUploadEnd);
                AddItem("/cgi-bin/micromsg-bin/bakchatrecoverdata", MMFuncConst.MMFunc_BakChatRecoverMedia);
                AddItem("/cgi-bin/micromsg-bin/bakchatrecovergetlist", MMFuncConst.MMFunc_BakChatRecoverGetList);
                AddItem("/cgi-bin/micromsg-bin/bakchatrecoverhead", MMFuncConst.MMFunc_BakChatRecoverHead);
                AddItem("/cgi-bin/micromsg-bin/bakchatdelete", MMFuncConst.MMFunc_BakChatDelete);
                AddItem("/cgi-bin/micromsg-bin/kvreport", MMFuncConst.MMFunc_ReportKV);
                AddItem("/cgi-bin/micromsg-bin/reportstrategy", MMFuncConst.MMFunc_ReportStrategyCtrl);
                AddItem("/cgi-bin/micromsg-bin/voipanswer", MMFuncConst.MMFunc_VoipAnswer);
                AddItem("/cgi-bin/micromsg-bin/voipcancelinvite", MMFuncConst.MMFunc_VoipCancelInvite);
                AddItem("/cgi-bin/micromsg-bin/voipheartbeat", MMFuncConst.MMFunc_VoipHeartBeat);
                AddItem("/cgi-bin/micromsg-bin/voipinvite", MMFuncConst.MMFunc_VoipInvite);
                AddItem("/cgi-bin/micromsg-bin/voipinviteremind", MMFuncConst.MMFunc_VoipInviteRemind);
                AddItem("/cgi-bin/micromsg-bin/voipshutdown", MMFuncConst.MMFunc_VoipShutDown);
                AddItem("/cgi-bin/micromsg-bin/voipsync", MMFuncConst.MMFunc_VoipSync);
                AddItem("/cgi-bin/micromsg-bin/getrecommendapplist", MMFuncConst.MMFunc_RecommendAppList);
                AddItem("/cgi-bin/micromsg-bin/getprofile", MMFuncConst.MMFunc_GetProfile);
                AddItem("/cgi-bin/micromsg-bin/updatesafedevice", MMFuncConst.MMFunc_MMUpdateSafeDevice);
                AddItem("/cgi-bin/micromsg-bin/delsafedevice", MMFuncConst.MMFunc_MMDelSafeDevice);
                AddItem("/cgi-bin/micromsg-bin/getcdndns", MMFuncConst.MMFunc_GetCDNDns);
                AddItem("/cgi-bin/micromsg-bin/tenpay", MMFuncConst.MMFunc_TenPay);
                AddItem("/cgi-bin/micromsg-bin/getloginqrcode", MMFuncConst.MMFunc_GetLoginQRCode);
                AddItem("/cgi-bin/micromsg-bin/checkloginqrcode", MMFuncConst.MMFunc_CheckLoginQRCode);
                AddItem("/cgi-bin/micromsg-bin/newauth", MMFuncConst.MMFunc_NewAuth);
                AddItem("/cgi-bin/micromsg-bin/getcrmsg", MMFuncConst.MMFunc_GETCRMSG);
                AddItem("/cgi-bin/micromsg-bin/initcontact", MMFuncConst.MMFunc_IniContact);


                AddItem("/cgi-bin/micromsg-bin/getchatroommemberdetail", MMFuncConst.MMFunc_GetChatRoomMemberDetail);
                //


            }
        }

        public static MMFuncConst getMMFuncByCGI(string cgiName)
        {
            if (!string.IsNullOrEmpty(cgiName))
            {
                checkMMFuncMap();
                int hashCode = cgiName.GetHashCode();
                if (mapMMFunc.ContainsKey(hashCode))
                {
                    return mapMMFunc[hashCode];
                }
            }
            return MMFuncConst.MMFunc_Default;
        }

        public static string getUriByCmdID(int cmdID)
        {
            switch (cmdID)
            {
                case 1:
                    return "/cgi-bin/micromsg-bin/auth";

                case 2:
                    return "/cgi-bin/micromsg-bin/sendmsg";

                case 3:
                    return "/cgi-bin/micromsg-bin/sync";

                case 9:
                    return "/cgi-bin/micromsg-bin/uploadmsgimg";

                case 10:
                    return "/cgi-bin/micromsg-bin/getmsgimg";

                case 14:
                    return "/cgi-bin/micromsg-bin/init";

                case 15:
                    return "/cgi-bin/micromsg-bin/batchgetheadimg";

                case 0x10:
                    return "/cgi-bin/micromsg-bin/getupdatepack";

                case 0x11:
                    return "/cgi-bin/micromsg-bin/searchfriend";

                case 0x12:
                    return "/cgi-bin/micromsg-bin/getinvitefriend";

                case 0x13:
                    return "/cgi-bin/micromsg-bin/uploadvoice";

                case 20:
                    return "/cgi-bin/micromsg-bin/downloadvoice";

                case 0x1a:
                    return "/cgi-bin/micromsg-bin/newsync";

                case 0x1b:
                    return "/cgi-bin/micromsg-bin/newinit";

                case 0x1c:
                    return "/cgi-bin/micromsg-bin/batchgetcontactprofile";

                case 0x20:
                    return "/cgi-bin/micromsg-bin/newreg";

                case 0x21:
                    return "/cgi-bin/micromsg-bin/getusername";

                case 0x22:
                    return "/cgi-bin/micromsg-bin/searchcontact";

                case 0x23:
                    return "/cgi-bin/micromsg-bin/getupdateinfo";

                case 0x24:
                    return "/cgi-bin/micromsg-bin/addchatroommember";

                case 0x25:
                    return "/cgi-bin/micromsg-bin/createchatroom";

                case 0x26:
                    return "/cgi-bin/micromsg-bin/getqqgroup";

                case 0x27:
                    return "/cgi-bin/micromsg-bin/uploadvideo";

                case 40:
                    return "/cgi-bin/micromsg-bin/downloadvideo";

                case 0x29:
                    return "/cgi-bin/micromsg-bin/sendinvitemail";

                case 0x2a:
                    return "/cgi-bin/micromsg-bin/sendcard";

                case 0x2c:
                    return "/cgi-bin/micromsg-bin/verifyuser";

                case 0x30:
                    return "/cgi-bin/micromsg-bin/getverifyimg";

                case 0x38:
                    return "/cgi-bin/micromsg-bin/shakereport";

                case 0x39:
                    return "/cgi-bin/micromsg-bin/shakeget";

                case 0x3b:
                    return "/cgi-bin/micromsg-bin/expose";

                case 60:
                    return "/cgi-bin/micromsg-bin/getvuserinfo";

                case 0x3e:
                    return "/cgi-bin/micromsg-bin/voipsync";

                case 0x3f:
                    return "/cgi-bin/micromsg-bin/voipinvite";

                case 0x40:
                    return "/cgi-bin/micromsg-bin/voipcancelinvite";

                case 0x41:
                    return "/cgi-bin/micromsg-bin/voipanswer";

                case 0x42:
                    return "/cgi-bin/micromsg-bin/voipshutdown";

                case 0x43:
                    return "/cgi-bin/micromsg-bin/getqrcode";

                case 70:
                    return "/cgi-bin/micromsg-bin/generalset";

                case 0x47:
                    return "/cgi-bin/micromsg-bin/getcontact";

                case 0x51:
                    return "/cgi-bin/micromsg-bin/voipheartbeat";

                case 0x54:
                    return "/cgi-bin/micromsg-bin/masssend";

                case 0x5f:
                    return "/cgi-bin/micromsg-bin/snsupload";

                case 0x61:
                    return "/cgi-bin/micromsg-bin/snspost";

                case 0x62:
                    return "/cgi-bin/micromsg-bin/snstimeline";

                case 0x63:
                    return "/cgi-bin/micromsg-bin/snsuserpage";

                case 100:
                    return "/cgi-bin/micromsg-bin/snscomment";

                case 0x65:
                    return "/cgi-bin/micromsg-bin/snsobjectdetail";

                case 0x66:
                    return "/cgi-bin/micromsg-bin/snssync";

                case 0x68:
                    return "/cgi-bin/micromsg-bin/snsobjectop";

                case 0x69:
                    return "/cgi-bin/micromsg-bin/uploadappattach";

                case 0x6a:
                    return "/cgi-bin/micromsg-bin/downloadappattach";

                case 0x6b:
                    return "/cgi-bin/micromsg-bin/sendappmsg";

                case 0x6c:
                    return "/cgi-bin/micromsg-bin/getappinfo";

                case 0x6d:
                    return "/cgi-bin/micromsg-bin/getrecommendapplist";

                case 0x72:
                    return "/cgi-bin/micromsg-bin/snstagoption";

                case 0x73:
                    return "/cgi-bin/micromsg-bin/snstagmemberoption";

                case 0x74:
                    return "/cgi-bin/micromsg-bin/snstaglist";

                case 0x75:
                    return "/cgi-bin/micromsg-bin/snstagmemmutilset";

                case 0x76:
                    return "/cgi-bin/micromsg-bin/getprofile";

                case 0x7d:
                    return "/cgi-bin/micromsg-bin/voipinviteremind";

                case 0x7f:
                    return "/cgi-bin/micromsg-bin/shaketranimgreport";

                case 0x80:
                    return "/cgi-bin/micromsg-bin/shaketranimgget";

                case 0x81:
                    return "/cgi-bin/micromsg-bin/batchgetshaketranimg";

                case 130:
                    return "/cgi-bin/micromsg-bin/shaketranimgunbind";

                case 0x86:
                    return "/cgi-bin/micromsg-bin/bakchatuploadhead";

                case 0x87:
                    return "/cgi-bin/micromsg-bin/bakchatuploadend";

                case 0x88:
                    return "/cgi-bin/micromsg-bin/bakchatuploadmsg";

                case 0x89:
                    return "/cgi-bin/micromsg-bin/bakchatuploadmedia";

                case 0x8a:
                    return "/cgi-bin/micromsg-bin/bakchatrecovergetlist";

                case 0x8b:
                    return "/cgi-bin/micromsg-bin/bakchatrecoverhead";

                case 140:
                    return "/cgi-bin/micromsg-bin/bakchatrecoverdata";

                case 0x8d:
                    return "/cgi-bin/micromsg-bin/bakchatdelete";
                case 0xab:
                    return "/cgi-bin/micromsg-bin/updatesafedevice";
                case 0xac:
                    return "/cgi-bin/micromsg-bin/delsafedevice";
                case 0xb9:
                    return "/cgi-bin/micromsg-bin/tenpay";
                case 0xb2:
                    return "/cgi-bin/micromsg-bin/newauth";
                case 805:
                    return "/cgi-bin/micromsg-bin/getcrmsg";
            }
            return "";
        }
    }
}

