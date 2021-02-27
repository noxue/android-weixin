namespace MicroMsg.Scene
{
    using Google.ProtocolBuffers;
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using System;
    using System.Reflection;

    public abstract class NetSceneBaseEx<TRequest, TResponse, TBuilder> : NetSceneBase where TRequest: AbstractMessageLite<TRequest, TBuilder> where TBuilder: AbstractBuilderLite<TRequest, TBuilder>
    {
        protected TBuilder mBuilder;
        protected PackResult mErrorCode;
        private const string TAG = "NetSceneBaseEx";

        protected NetSceneBaseEx()
        {
        }

        protected bool beginBuilder()
        {
            try
            {
                Type type = typeof(TRequest);
                this.mBuilder = (TBuilder) type.InvokeMember("CreateBuilder", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly, null, null, null);
                if (this.mBuilder != null)
                {
                    base.mSessionPack = new SessionPack();
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.e("NetSceneBaseEx", "initBuilder exception" + exception.ToString());
            }
            return false;
        }

        public bool check(string args)
        {
            return ((args != null) && (args.Length > 0));
        }

        protected bool endBuilder()
        {
            base.mSessionPack.mRequestObject = this.mBuilder.Build();
            base.mSessionPack.mProcRequestToByteArray += new RequestToByteArrayDelegate(this.requestToByteArray);
            base.mSessionPack.mResponseParser += new OnResponseParserDelegate(this.onParserResponse);
            base.mSessionPack.mCompleted += new SessionPackCompletedDelegate(this.onCompleted);
            bool flag = Sender.getInstance().sendPack(base.mSessionPack);
            this.mBuilder = default(TBuilder);
            return flag;
        }

        protected PackResult getLastError()
        {
            return this.mErrorCode;
        }

        private void onCompleted(object sender, PackEventArgs e)
        {
            SessionPack pack = sender as SessionPack;
            TResponse mResponseObject = (TResponse) pack.mResponseObject;
            TRequest mRequestObject = (TRequest) pack.mRequestObject;
            if (e.isSuccess())
            {
                if (base.mSessionPack == null)
                {
                    Log.e("NetSceneBaseEx", "response on success, but cancelled, seq = " + pack.mSeqID);
                    this.mErrorCode = PackResult.BEEN_CANCELLED;
                    this.onFailed(mRequestObject, mResponseObject);
                }
                else
                {
                    this.onSampleSpeed((int) (base.mSessionPack.timeInRecvEnd - base.mSessionPack.timeInSent));
                    base.mSessionPack = null;
                    this.onSuccess(mRequestObject, mResponseObject);
                }
            }
            else
            {
                Log.e("NetSceneBaseEx", "send pack error, result = " + e.result);
                this.mErrorCode = e.result;
                base.mSessionPack = null;
                this.onFailed(mRequestObject, mResponseObject);
            }
        }

        protected abstract void onFailed(TRequest request, TResponse response);
        private object onParserResponse(SessionPack sessionPack)
        {
            try
            {
                Type type = typeof(TResponse);
                object[] args = new object[] { sessionPack.mResponseBuffer };
                return type.InvokeMember("ParseFrom", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly, null, null, args);
            }
            catch (TargetInvocationException exception)
            {
                Log.e("NetSceneBaseEx", "onParserResponse exception" + exception.Message);
            }
            catch (Exception exception2)
            {
                Log.e("NetSceneBaseEx", "onParserResponse exception" + exception2.ToString());
            }
            return null;
        }

        protected virtual void onSampleSpeed(int RTT)
        {
        }

        protected abstract void onSuccess(TRequest request, TResponse response);
        private byte[] requestToByteArray(object obj)
        {
            return ((AbstractMessageLite<TRequest, TBuilder>) obj).ToByteArray();
        }
    }
}

