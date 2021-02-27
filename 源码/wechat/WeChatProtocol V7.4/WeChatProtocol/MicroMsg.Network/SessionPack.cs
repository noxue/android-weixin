namespace MicroMsg.Network
{
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System;
    using System.Threading;
    using System.Windows;

    public class SessionPack
    {
        private static int gSeq = 1;
        public byte[] mCacheBodyBuffer;
        public bool mCanceled;
        private Connector.ConnectorContext mConnContext = new Connector.ConnectorContext();
        public short mEncrypt = 5;
        public bool mNeedCompress = true;
        private PackProgress mPackProgress = new PackProgress();
        public double mPackTimeoutExpired = (Util.getNowMilliseconds() + 100000.0);
        public object mRequestObject;
        public byte[] mResponseBuffer;
        public object mResponseObject;
        public int mRetryLeft = 3;
        public int mSendStatus;
        public double mSendTimeoutExpired;
        public object mUserArgs;
        public const int STATUE_NOTSEND = 0;
        public const int STATUE_READY_SEND = 1;
        public const int STATUE_RECEIVED = 4;
        public const int STATUE_SEND_ERR = 6;
        public const int STATUE_SENDED = 3;
        public const int STATUE_SENDING = 2;
        public const int STATUE_SUCCESSED = 5;
        public double timeInCmd;
        public double timeInNetCompleted;
        public double timeInRecvBegin;
        public double timeInRecvEnd;
        public double timeInSceneCompleted;
        public double timeInSent;
        public byte[] RandomKey = Util.HexStringToByte("68547dc78ee230da53c30354037bd0a2");
        public byte[] NotifyKey = null;
        public bool mNeedAutoAuth = false;


        public event SessionPackCompletedDelegate mCompleted;

        public event OnHttpReceivedProgressDelegate mHttpRecvProgress;

        public event RequestToByteArrayDelegate mProcRequestToByteArray;

        public event OnResponseParserDelegate mResponseParser;

        public SessionPack()
        {
            this.mConnContext.mSeq = getSeqID();
        }

        public void cancelTimeoutPoint()
        {
            this.mSendTimeoutExpired = 0.0;
        }

        public void checkPackTimeout(double now)
        {
            if (now >= this.mPackTimeoutExpired)
            {
                this.onError(PackResult.PACK_TIMEOUT);
                this.mSendStatus = 6;
            }
        }

        public int getMMFuncID()
        {
            return (int)CmdFunc.getMMFuncByCGI(this.mCmdUri);
        }

        public static double getNow()
        {
            DateTime time = new DateTime(0x7b2, 1, 1);
            return DateTime.Now.Subtract(time).TotalMilliseconds;
        }

        public PackProgress getPackProgress()
        {
            this.mPackProgress.lengthSent = 0;
            this.mPackProgress.lengthReceived = 0;
            if (this.mCacheBodyBuffer != null)
            {
                this.mPackProgress.lengthSent = this.mCacheBodyBuffer.Length;
            }
            if (this.mResponseBuffer != null)
            {
                this.mPackProgress.lengthReceived = this.mResponseBuffer.Length;
            }
            else if (this.mHttpClient != null)
            {
                this.mPackProgress.lengthReceived = this.mHttpClient.getReceivedLength();
            }
            return this.mPackProgress;
        }

        public static int getSeqID()
        {
            return gSeq++;
        }

        public byte[] getSessionKey(bool encode)
        {
            switch (this.mCmdID)
            {
                case 0:
                    return this.getSessionKeyForInvalidCmd(encode);

                case 1:
                    if (!encode)
                    {
                        return Util.StringToByteArray(SessionPackMgr.getAccount().getPassword());
                    }
                    return null;

                case 0x1f:
                case 0x20:
                    if (!encode)
                    {
                        return Util.StringToByteArray(SessionPackMgr.getAccount().getPassword());
                    }
                    return null;

                case 0x21:
                    //if (!encode)
                    //{
                    //    return Util.StringToByteArray(((GetUserNameRequest) this.mRequestObject).Pwd);
                    //}
                    return null;

                case 0x30:
                    if (encode)
                    {
                        return null;
                    }
                    return null;
                //return Util.StringToByteArray(((GetVerifyImgRequest) this.mRequestObject).Pwd.String);
                case 232:
                case 233:
                case 0xb2:
                    if (encode)
                    {
                        return null;
                    }
                    return RandomKey;
            }
            return SessionPackMgr.getSessionKey();
        }

        private byte[] getSessionKeyForInvalidCmd(bool encode)
        {
            switch (this.mCmdUri)
            {
                //case "/cgi-bin/micromsg-bin/getresetpwdurl":
                //    if (encode)
                //    {
                //        return null;
                //    }
                //    return Util.StringToByteArray(((ResetPwdRequest)this.mRequestObject).Pwd);

                case "/cgi-bin/micromsg-bin/bindopmobileforreg":
                    if (encode)
                    {
                        return null;
                    }
                    return Util.StringToByteArray(((BindOpMobileRequest)this.mRequestObject).Mobile);
            }
            return SessionPackMgr.getSessionKey();
        }

        private bool isAutoAuthPack()
        {
            if (this.mCmdID != 1 || this.mCmdID != 0xb2)
            {
                return false;
            }
            AuthRequest mRequestObject = this.mRequestObject as AuthRequest;
            if (mRequestObject == null)
            {
                return false;
            }
            if (mRequestObject.BaseRequest.Scene != 2)
            {
                return false;
            }
            return true;
        }

        public bool isSendTimeout()
        {
            if (this.mSendTimeoutExpired == 0.0)
            {
                return false;
            }
            return (getNow() >= this.mSendTimeoutExpired);
        }

        public void onError(PackResult ret)
        {
            Log.e("Network", string.Concat(new object[] { "pack.onError, cmd = ", (CmdConst)this.mCmdID, " seq = ", this.mSeqID, ", ret = ", ret }));
            if (this.mCompleted != null)
            {
                PackEventArgs args = new PackEventArgs
                {
                    result = ret
                };
                if (this.isAutoAuthPack())
                {
                    SessionPackMgr.setAuthStatus(0);
                }
                else
                {
                    if (((this.mCmdID == 1) || (this.mCmdID == 0x20)) || (this.mCmdID == 0x1f) || (this.mCmdID == 0xb2))
                    {
                        SessionPackMgr.setSessionKey(null);
                        SessionPackMgr.getAccount().reset();
                        SessionPackMgr.setAuthStatus(0);
                    }
                    //Deployment.get_Current().get_Dispatcher().BeginInvoke(new SessionPackCompletedDelegate(this.mCompleted.Invoke), new object[] { this, args });
                    SessionPackCompletedDelegate spd = new SessionPackCompletedDelegate(this.mCompleted.Invoke);
                    spd.BeginInvoke(this, args, null, null);
                }
            }
        }

        public void OnHttpReceivedProgress(int length)
        {
            if (this.mHttpRecvProgress != null)
            {
                this.mHttpRecvProgress(length);
            }
        }

        public object onResponseParser()
        {
            if (this.mResponseParser != null)
            {
                return this.mResponseParser(this);
            }
            return null;
        }

        public void onSuccess()
        {
            Log.i("Network", string.Concat(new object[] { "pack.onSuccess, cmd = ", (CmdConst)this.mCmdID, " seq = ", this.mSeqID }));
            if (this.mCompleted != null)
            {
                PackEventArgs args = new PackEventArgs
                {
                    result = PackResult.SUCCESS
                };
                //Deployment.get_Current().get_Dispatcher().BeginInvoke(
                //new SessionPackCompletedDelegate(this.mCompleted.Invoke), new object[] { this, args };
                SessionPackCompletedDelegate dd = new SessionPackCompletedDelegate(this.mCompleted.Invoke);

                dd.BeginInvoke(this, args, null, null);
                //AsyncCallback bak = new SessionPackCompletedDelegate(this.mCompleted.Invoke);
                //System.Runtime.Remoting.Messaging.AsyncResult result = dd.BeginInvoke(null,this.mCompleted.Invoke,null, new object[] { this, args });
            }
        }

        public void printProgress()
        {
            this.getPackProgress();
        }

        public void printTimeProfile()
        {
        }

        public byte[] requestToByteArray()
        {
            if ((this.mRequestObject != null) && (this.mProcRequestToByteArray != null))
            {
                return this.mProcRequestToByteArray(this.mRequestObject);
            }
            return null;
        }

        public void setTimeoutPoint()
        {
            double num = getNow();
            this.mSendTimeoutExpired = num + 30000.0;
        }

        public int mCmdID
        {
            get
            {
                return this.mConnContext.mCmd;
            }
            set
            {
                this.mConnContext.mCmd = value;
                this.mConnContext.mCmdUri = CmdFunc.getUriByCmdID(value);
            }
        }

        public string mCmdUri
        {
            get
            {
                return this.mConnContext.mCmdUri;
            }
            set
            {
                this.mConnContext.mCmdUri = value;
            }
        }

        public int mConnectMode
        {
            get
            {
                return this.mConnContext.mConnectMode;
            }
            set
            {
                this.mConnContext.mConnectMode = value;
            }
        }

        public HttpClient mHttpClient
        {
            get
            {
                return this.mConnContext.mHttpClient;
            }
            set
            {
                if ((value != null) && (this.mConnContext.mHttpClient != null))
                {
                    this.mConnContext.mHttpClient.close();
                }
                this.mConnContext.mHttpClient = value;
                if (value != null)
                {
                    this.mConnContext.mHttpClient.mHttpContext.mProgress += new OnHttpReceivedProgressDelegate(this.OnHttpReceivedProgress);
                }
            }
        }

        public int mSeqID
        {
            get
            {
                return this.mConnContext.mSeq;
            }
            set
            {
                this.mConnContext.mSeq = value;
            }
        }
    }
}

