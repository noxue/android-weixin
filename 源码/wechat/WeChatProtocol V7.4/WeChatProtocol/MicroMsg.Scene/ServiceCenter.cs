namespace MicroMsg.Scene
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    //using MicroMsg.Scene.ChatRoom;
    //using MicroMsg.Scene.Video;
    //using MicroMsg.Scene.Voice;
    using System;
    using System.Collections.Generic;
    using MicroMsg.Plugin;
    using MicroMsg.Scene.Voice;
    using MicroMsg.Scene.ChatRoom;
    using MicroMsg.Scene.Video;
using MicroMsg.Scene.Image;

    public class ServiceCenter
    {
        //private static NetSceneCrashReport _sceneCrashReport;
        //public static BannerManager bannerManager;
        //public static BConversationMgr bconversationMgr;
        public static WorkThread bgWorker;
        //public static ConfigMgr configMgr;
        //public static ConversationMgr conversationMgr;
        public static Dictionary<string, object> mDictAppStageShouldSave;
        //public static MobileContactMgr mobileContactMgr;
        //public static OfficialAccountConversationMgr occonversationMgr;
        //public static ClientPerfReportMgr perfReportMgr;
        //public static PushNotifyMgr pushNotifyMgr;
        //public static QConversationMgr qconversationMgr;
        //public static ReportService reportService;
        public static AddChatRoomMemberService sceneAddChatRoomMemberService;
        public static NetSceneAuth sceneAuth;
        public static NetSceneBatchGetContact sceneBatchGetContact;
        //public static NetSceneBatchGetContactProfile sceneBatchGetContactProfile;
        //public static NetSceneBatchGetHeadImg sceneBatchGetHeadImg;
        //public static NetSceneBindOpMobile sceneBindMobile;
        public static NetSceneBindOpMobileForReg sceneBindOpMobileForReg;
        //public static NetSceneBindQQ sceneBindQQ;
        //public static NetSceneChatTyping sceneChatTyping;
        //public static NetSceneClientPerfReport sceneClientPerfReport;
        //public static CreateChatRoomService sceneCreateChatRoomService;
        //public static DelChatRoomMemberService sceneDelChatRoomMemberService;
        //public static NetSceneDownloadHDHeadImg sceneDownloadHDHeadImg;
        public static DownloadVideoService sceneDownloadVideo;
        //public static DownloadVoiceService sceneDownloadVoiceService;
        //public static NetSceneExpose sceneExpose;
        //public static NetSceneGeneralSet sceneGeneralSet;
        //public static NetSceneGetPSMImg sceneGetPSMImg;
        //public static NetSceneGetQRCode sceneGetQRCode;
        //public static NetSceneGetUpdateInfo sceneGetUpdateInfo;
        public static NetSceneGetUserName sceneGetUserName;
        //public static NetSceneGetVerifyImage sceneGetVerifyImage;
        //public static GroupCardService sceneGroupCardService;
        //public static NetSceneLogout sceneLogout;
        //public static NetSceneGetMFriend sceneNetGetMFriend;
        //public static NetSceneUploadMContact sceneNetUploadMContact;
        public static NetSceneNewInit sceneNewInit;
        public static NetSceneNewReg sceneNewReg;
        public static NetSceneNewSync sceneNewSync;
 
        //public static NetScenePushChannelReg scenePushChannelReg;
        //public static NetScenePushChannelUnReg scenePushChannelUnReg;
        public static NetSceneReg sceneReg;
        //public static NetSceneResetPwd sceneResetPwd;
        public static NetSceneSearchContact sceneSearchContact;
        public static NetSceneSendCard sceneSendCard;
        //public static NetSceneSendFeedback sceneSendFeedback;
        //public static NetSceneSendInviteEmail sceneSendInviteEmail;
        public static NetSceneSendMsg sceneSendMsg;
        public static NetSceneSendMsgOld sceneSendMsgOld;
        //public static NetSceneSendVerifyEmail sceneSendVerifyEmail;
        //public static NetSceneUploadHDHeadImg sceneUploadHDHeadImg;
        public static UploadVideoService sceneUploadVideo;
        public static UploadVoiceService sceneUploadVoice;
        public static NetSceneVerifyUser sceneVerifyUser;
        public static NetSceneSendAppMsg sendAppMsg;
        //public static SessionMgr sessionMgr;
       // public static StatReportMgr statReportMgr;
        private const string TAG = "ServiceCenter";
        //public static VUserInfoMgr vuserInfoMgr;
        //public static WebMMMgr webMMMgr;

        public static bool asyncExec(Action act)
        {
            if (act == null)
            {
                return false;
            }
            return bgWorker.add_job(act);
        }

        public static void asyncExecInit()
        {
            if (bgWorker == null)
            {
                bgWorker = new WorkThread();
            }
        }

        public static void init()
        {
            ExtentCenter.initialize();
            //Profiler.setPoint("ServiceCenterStart");
            //CrashLogMgr.SendCrashInfo();
            //Profiler.setPoint("CrashReport");
            //GConfigMgr.init();
            //ConfigMgr.init();
            //ContactMgr.init();
            //Profiler.setPoint("ConfigMgr");
            //‘› ±≤ª”√
            asyncExecInit();
            //StorageMgr.init();
            AccountMgr.init();
            //Profiler.setPoint("AccountMgrInit");
            //HeadImageMgr.init();
            //FMsgMgr.init();
            //MobileContactMgr.init();
            //SnsMsgMgr.init();
            SnsAsyncMgr.init();
            //GetPackageListMgr.checkInit();
            //Profiler.setPoint("MgrInit");
            //qconversationMgr = new QConversationMgr();
            //bconversationMgr = new BConversationMgr();
            //conversationMgr = new ConversationMgr();
            //bannerManager = new BannerManager();
            //occonversationMgr = new OfficialAccountConversationMgr();
            //webMMMgr = new WebMMMgr();
            //Profiler.setPoint("NewMgr");
            NetSceneSyncCheck.initSyncCheck();
            sceneAuth = new NetSceneAuth();
            sceneNewInit = new NetSceneNewInit();
            sceneNewSync = new NetSceneNewSync();
 
            sceneReg = new NetSceneReg();
            sceneNewReg = new NetSceneNewReg();
            sceneGetUserName = new NetSceneGetUserName();
            sceneBindOpMobileForReg = new NetSceneBindOpMobileForReg();
            //sceneResetPwd = new NetSceneResetPwd();
            sceneSearchContact = new NetSceneSearchContact();
            sceneVerifyUser = new NetSceneVerifyUser();
            //sceneBatchGetHeadImg = new NetSceneBatchGetHeadImg();
            //sceneBindQQ = new NetSceneBindQQ();
            //sceneBindMobile = new NetSceneBindOpMobile();
            sceneUploadVoice = new UploadVoiceService();
            //sceneDownloadVoiceService = new DownloadVoiceService();
            //sceneSendInviteEmail = new NetSceneSendInviteEmail();
            sceneSendMsg = new NetSceneSendMsg();
            sceneSendMsgOld = new NetSceneSendMsgOld();
            //sceneCreateChatRoomService = new CreateChatRoomService();
            sceneAddChatRoomMemberService = new AddChatRoomMemberService();
            //sceneDelChatRoomMemberService = new DelChatRoomMemberService();
            //sceneGroupCardService = new GroupCardService();
            //sceneSendFeedback = new NetSceneSendFeedback();
            //sceneSendVerifyEmail = new NetSceneSendVerifyEmail();
            //sceneGetVerifyImage = new NetSceneGetVerifyImage();
            //sceneChatTyping = new NetSceneChatTyping();
            sceneDownloadVideo = new DownloadVideoService();
            sceneUploadVideo = new UploadVideoService();
            //scenePushChannelReg = new NetScenePushChannelReg();
            //scenePushChannelUnReg = new NetScenePushChannelUnReg();
            //pushNotifyMgr = new PushNotifyMgr();
            //sceneNetUploadMContact = new NetSceneUploadMContact();
            //sceneNetGetMFriend = new NetSceneGetMFriend();
            //sceneLogout = new NetSceneLogout();
            //sceneGetUpdateInfo = new NetSceneGetUpdateInfo();
            //sceneUploadHDHeadImg = new NetSceneUploadHDHeadImg();
            //sceneDownloadHDHeadImg = new NetSceneDownloadHDHeadImg();
            //sceneGeneralSet = new NetSceneGeneralSet();
            //sceneBatchGetContactProfile = new NetSceneBatchGetContactProfile();
            sceneBatchGetContact = new NetSceneBatchGetContact();
            //sceneExpose = new NetSceneExpose();
            //sceneGetPSMImg = new NetSceneGetPSMImg();
            //sceneGetQRCode = new NetSceneGetQRCode();
            sceneSendCard = new NetSceneSendCard();
            //sceneClientPerfReport = new NetSceneClientPerfReport();
            sendAppMsg = new NetSceneSendAppMsg();
            //Profiler.setPoint("NewScene");
            //sessionMgr = new SessionMgr();
            //statReportMgr = new StatReportMgr();
            //vuserInfoMgr = new VUserInfoMgr();
            //mobileContactMgr = new MobileContactMgr();
            //perfReportMgr = new ClientPerfReportMgr();
            //reportService = new ReportService();
            AccountMgr.loginStartupNotify();
            //Profiler.setPoint("ServiceCenterEnd");
        }

        public static void uninit()
        {
            AccountMgr.uninit();
        }


    }
}

