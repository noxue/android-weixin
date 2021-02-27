namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;
    using MicroMsg.Common.Algorithm;

    public class NetSceneSendAppMsg : NetSceneBaseEx<SendAppMsgRequest, SendAppMsgResponse, SendAppMsgRequest.Builder>
    {
        private ChatMsg mAppMsg;
        private UploadAppMsgContext mContext;
        private onSendAppMsgFinished mOnSendAppMsgFinished;
        private const string TAG = "NetSceneSendAppMsg";

        public bool doScene(UploadAppMsgContext context, onSendAppMsgFinished OnFinished)
        {
            if (((context == null) || (context.mMsg == null)) || (context.mTrans == null))
            {
                return false;
            }
            this.mContext = context;
            context.mTrans.nStatus = 2;
            //StorageMgr.msgTrans.update(context.mTrans);
            this.mAppMsg = context.mMsg;
            this.mOnSendAppMsgFinished = OnFinished;
            return this.doSceneEx((AppMsgSouce) context.mTrans.nEndFlag);
        }

        public bool doScene(ChatMsg msg, AppMsgSouce source)
        {
            this.mAppMsg = msg;
            return this.doSceneEx(source);
        }

        private bool doSceneEx(AppMsgSouce source)
        {
            AppMsgInfo info = AppMsgMgr.ParseAppXml(this.mAppMsg.strMsg);
            if (info == null)
            {
                Log.e("NetSceneSendAppMsg", "invalid AppMsgInfo, msg.strMsg = " + this.mAppMsg.strMsg);
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            AppMsg.Builder builder = AppMsg.CreateBuilder();
            builder.AppId = info.appid;
            uint result = 0;
            builder.SdkVersion = uint.TryParse(info.sdkVer, out result) ? result : 0;
            builder.ToUserName = this.mAppMsg.strTalker;
            //if (info.fromUserName != AccountMgr.curUserName)
            //{
            //    Log.e("NetSceneSendAppMsg", "invalid from username = " + info.fromUserName);
            //    return false;
            //}
           // builder.FromUserName = info.fromUserName;
            builder.FromUserName = AccountMgr.curUserName;

            builder.Type = (uint) info.type;
            builder.Content = AppMsgMgr.getXmlNodeString(this.mAppMsg.strMsg, "appmsg") ?? "";
            if (info.showtype == 2)
            {
                builder.Content = builder.Content + AppMsgMgr.getXmlNodeString(this.mAppMsg.strMsg, "ShakePageResult");
            }
            if (string.IsNullOrEmpty(builder.Content))
            {
                Log.e("NetSceneSendAppMsg", "invalid appmsgBuilder.Content = " + builder.Content);
                return false;
            }
            builder.CreateTime = (uint) this.mAppMsg.nCreateTime;
            builder.ClientMsgId = this.mAppMsg.strClientMsgId;
            byte[] inBytes = null;//StorageIO.readFromFile(ChatMsgMgr.getMsgThumbnail(this.mAppMsg));
            if (inBytes != null)
            {
                builder.Thumb = Util.toSKBuffer(inBytes);
            }
            builder.Source = (int) source;
            base.mBuilder.Msg = builder.Build();
            base.mSessionPack.mCmdID = 0x6b;
            base.endBuilder();
            return true;
        }
        public  bool doSceneSendAppMsg(string toUsername,int source, string xmlstr)
        {
            AppMsgInfo info = AppMsgMgr.ParseAppXml(xmlstr);
            if (info == null)
            {
                //Log.e("NetSceneSendAppMsg", "invalid AppMsgInfo, msg.strMsg = " + this.mAppMsg.strMsg);
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            AppMsg.Builder builder = AppMsg.CreateBuilder();
            builder.AppId = info.appid;
            uint result = 0;
            builder.SdkVersion = uint.TryParse(info.sdkVer, out result) ? result : 0;
            builder.ToUserName = toUsername;
            info.fromUserName = AccountMgr.curUserName;
            //if (info.fromUserName != AccountMgr.curUserName)
            //{
            //    Log.e("NetSceneSendAppMsg", "invalid from username = " + info.fromUserName);
            //    return false;
            //}
            builder.FromUserName = info.fromUserName;
            builder.Type = (uint)info.type;
            builder.Content = AppMsgMgr.getXmlNodeString(xmlstr, "appmsg") ?? "";
            if (info.showtype == 2)
            {
                //builder.Content = builder.Content + AppMsgMgr.getXmlNodeString(this.mAppMsg.strMsg, "ShakePageResult");
                builder.Content = builder.Content;

            }
            if (string.IsNullOrEmpty(builder.Content))
            {
                Log.e("NetSceneSendAppMsg", "invalid appmsgBuilder.Content = " + builder.Content);
                return false;
            }
            builder.CreateTime =(uint) Util.getNowSeconds();
            builder.ClientMsgId = MD5Core.GetHashString(toUsername + Util.getNowMilliseconds()); ;
            byte[] inBytes = null;//StorageIO.readFromFile(ChatMsgMgr.getMsgThumbnail(this.mAppMsg));
            if (inBytes != null)
            {
                builder.Thumb = Util.toSKBuffer(inBytes);
            }
            builder.Source = (int)source;
            base.mBuilder.Msg = builder.Build();
            base.mSessionPack.mCmdID = 0x6b;
            base.endBuilder();
            return true;
        }
        public bool doSceneSendAppMsg(string toUsername, string xmlstr)
        {
            AppMsgInfo info = AppMsgMgr.ParseAppXml("<msg>" + xmlstr + "</msg>");
            if (info == null)
            {
                //Log.e("NetSceneSendAppMsg", "invalid AppMsgInfo, msg.strMsg = " + this.mAppMsg.strMsg);
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            AppMsg.Builder builder = AppMsg.CreateBuilder();
            builder.AppId = info.appid;
            uint result = 0;
            builder.SdkVersion = uint.TryParse(info.sdkVer, out result) ? result : 0;
            builder.ToUserName = toUsername;
            //if (info.fromUserName != AccountMgr.curUserName)
            //{
           // info.fromUserName = AccountMgr.curUserName;
                Log.e("NetSceneSendAppMsg", "invalid from username = " + info.fromUserName);
                //return false;
           // }
                builder.FromUserName = AccountMgr.curUserName;
            builder.Type = (uint)info.type;
            builder.Content = AppMsgMgr.getXmlNodeString("<msg>" + xmlstr + "</msg>", "appmsg") ?? "";
            if (info.showtype == 2)
            {
                //builder.Content = builder.Content + AppMsgMgr.getXmlNodeString(this.mAppMsg.strMsg, "ShakePageResult");
            }
            if (string.IsNullOrEmpty(builder.Content))
            {
                Log.e("NetSceneSendAppMsg", "invalid appmsgBuilder.Content = " + builder.Content);
                return false;
            }
            builder.CreateTime = (uint)Util.getNowSeconds();
            builder.ClientMsgId = MD5Core.GetHashString(toUsername + Util.getNowMilliseconds()); ;
            byte[] inBytes = null;//StorageIO.readFromFile(ChatMsgMgr.getMsgThumbnail(this.mAppMsg));
            if (inBytes != null)
            {
                builder.Thumb = Util.toSKBuffer(inBytes);
            }
            builder.Source = 1;
            base.mBuilder.Msg = builder.Build();
            base.mSessionPack.mCmdID = 0x6b;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(SendAppMsgRequest request, SendAppMsgResponse response)
        {
            Log.e("NetSceneSendAppMsg", "send request failed");
           // this.mAppMsg.nStatus = 1;
            //StorageMgr.chatMsg.updateMsg(this.mAppMsg);
            //if (this.mOnSendAppMsgFinished != null)
            //{
            //    this.mContext.mTrans.nStatus = 4;
            //    this.mOnSendAppMsgFinished(this.mContext);
            //}
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SEND_APP_MSG_ERR, null, null);
        }

        protected override void onSuccess(SendAppMsgRequest request, SendAppMsgResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneSendAppMsg", "send request failed ret =" + ret);
                //this.mAppMsg.nStatus = 1;
                ////StorageMgr.chatMsg.updateMsg(this.mAppMsg);
                //if (this.mOnSendAppMsgFinished != null)
                //{
                //    this.mContext.mTrans.nStatus = 4;
                //    this.mOnSendAppMsgFinished(this.mContext);
                //}
                //EventCenter.postEvent(EventConst.ON_NETSCENE_SEND_APP_MSG_ERR, null, null);
            }
            else
            {
               // this.mAppMsg.nMsgSvrID = (int) response.MsgId;
               // this.mAppMsg.nCreateTime = response.CreateTime;
               // this.mAppMsg.nStatus = 2;
               //// StorageMgr.chatMsg.updateMsg(this.mAppMsg);
               // if (this.mOnSendAppMsgFinished != null)
               // {
               //     this.mContext.mTrans.nStatus = 3;
               //     this.mOnSendAppMsgFinished(this.mContext);
               // }
                //EventCenter.postEvent(EventConst.ON_NETSCENE_SEND_APP_MSG_SUCCESS, null, null);
            }
        }
    }
}

