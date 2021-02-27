namespace MicroMsg.Network
{
    using MicroMsg.Common.Utils;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;

    public class HttpClient
    {
        public const int BUFFER_SIZE = 0x2000;
        public HttpRequestContext mHttpContext = new HttpRequestContext();
        private Action<byte[]> rspCallback;
        private const int STATUS_GET_DOING = 1;
        private const int STATUS_GET_DONE = 2;
        private const int STATUS_GET_NULL = 0;
        private int httpseq;

        public event DefineHttpHeaderDelegate mDefineHeader;

        public HttpClient(string cmdUri, int seq, string host)
        {
            this.mHttpContext.mCmdUri = cmdUri;
            this.mHttpContext.mSeqID = seq;
            this.mHttpContext.mHost = host;
        }

        public void close()
        {
            if (this.mHttpContext != null)
            {
                Log.d("Network", "httpclient close, uri=" + this.mHttpContext.mCmdUri);
                this.mHttpContext.release();
                this.mHttpContext = null;
            }
        }

        public int getReceivedLength()
        {
            if (this.mHttpContext != null)
            {
                return this.mHttpContext.mOutputLength;
            }
            return 0;
        }

        private void onGetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            Action action = null;
            HttpRequestContext asyncState = (HttpRequestContext)asynchronousResult.AsyncState;
            try
            {
                asyncState.mStreamRequest = asyncState.mRequest.EndGetRequestStream(asynchronousResult);
                asyncState.mStreamRequest.Write(asyncState.mBufferInput, 0, asyncState.mBufferInput.Length);
                asyncState.mStreamRequest.Close();
                asyncState.mStreamRequest.Dispose();
                asyncState.mAsyncResponseResult = asyncState.mRequest.BeginGetResponse(new AsyncCallback(this.onRecvHttpCallback), asyncState);
            }
            catch (Exception exception)
            {
                Log.e("Network", "http.onGetRequestStreamCallback failed: " + exception.Message);
                asyncState.mStatus = 2;
                asyncState.release();
                if (this.rspCallback != null)
                {
                    if (action == null)
                    {
                        // action = () => 
                        this.rspCallback(null);
                    }
                    //Deployment.get_Current().get_Dispatcher().BeginInvoke(action);
                }
            }
        }

        private void onRecvHttpCallback(IAsyncResult asynchronousResult)
        {
            Action action = null;
            Action action2 = null;
            HttpRequestContext context = (HttpRequestContext)asynchronousResult.AsyncState;
            try
            {
                HttpWebRequest mRequest = context.mRequest;
                context.mResponse = (HttpWebResponse)mRequest.EndGetResponse(asynchronousResult);

                ServerBulletin.onRecvBulletin(context.mResponse.Headers["WXBT"]);
               // Stream responseStream = context.mResponse.GetResponseStream();

                Log.w("Network", "http return head ,statuscode =  " + context.mResponse.StatusCode.ToString() + "http seq "+ httpseq);//get_StatusCode());
                Stream ms = context.mResponse.GetResponseStream();

                //    using (var ms = context.mResponse.GetResponseStream())
                //      {
                //  using (var responseStream = new MemoryStream())
                //  {
                //responseStream.(ms);
                var responseStream = new MemoryStream();
                const int bufferLength = 1024 * 10;
                int actual;
                byte[] buffers = new byte[bufferLength];
                while ((actual = ms.Read(buffers, 0, bufferLength)) > 0)
                {
                    responseStream.Write(buffers, 0, actual);
                }
                responseStream.Position = 0;
                //...
                // }
                //   }


                int newSize = ((int)responseStream.Length) + 0x2000;
                byte[] buffer = new byte[newSize];
                int len = responseStream.Read(buffer, 0, (int)responseStream.Length);
                int num3 = 0;
                context.setProgress(len);
                do
                {
                    num3 = responseStream.Read(buffer, len, newSize - len);
                    if (num3 > 0)
                    {
                        len += num3;
                        if ((newSize - len) < 0x2000)
                        {
                            newSize += 0x2000;
                            Array.Resize<byte>(ref buffer, newSize);
                        }
                        context.setProgress(len);
                    }
                }
                while (num3 > 0);
                context.mOutputLength = len;
                context.mBufferOutput = buffer;
                context.mStatus = 2;
                if (this.rspCallback != null)
                {
                    if (action == null)
                    {
                        // action = () => 
                        this.rspCallback(context.mBufferOutput);
                    }
                    //Deployment.get_Current().get_Dispatcher().BeginInvoke(action);
                }
                //context.mResponse.Close();
                responseStream.Dispose();
            }
            catch (Exception exception)
            {
                Log.e("Network", "http.onRecvHttpCallback failed: " + exception.Message);
                context.mStatus = 2;
                context.release();
                if (this.rspCallback != null)
                {
                    if (action2 == null)
                    {
                        //action2 = () => 
                        this.rspCallback(null);
                    }
                    //Deployment.get_Current().get_Dispatcher().BeginInvoke(action2);
                }
            }
        }

        public SocketError receive(ref byte[] outBuf, ref int seq, ref string cmdUri)
        {
            if (this.mHttpContext.mStatus != 2)
            {
                return (SocketError)SocketError.NoData;
            }
            seq = this.mHttpContext.mSeqID;
            cmdUri = this.mHttpContext.mCmdUri;
            if (this.mHttpContext.mOutputLength <= 0)
            {
                return (SocketError)SocketError.SocketError;
            }
            int mOutputLength = this.mHttpContext.mOutputLength;
            outBuf = new byte[mOutputLength];
            Buffer.BlockCopy(this.mHttpContext.mBufferOutput, 0, outBuf, 0, mOutputLength);
            this.mHttpContext.mStatus = 0;
            this.mHttpContext.mOutputLength = 0;
            return (SocketError)SocketError.Success;
        }

        public bool send(byte[] data = null, int count = 0, Action<byte[]> cb = null, int seq=0)
        {
            Action action = null;
            try
            {
                this.rspCallback = cb;
                Uri uri = new Uri(this.mHttpContext.mCmdUri);
                Log.i("Network", string.Concat(new object[] { "http send request, len =", count, " to uri=", uri }));
                this.mHttpContext.mRequest = (HttpWebRequest)WebRequest.Create(uri);
                if (this.mDefineHeader == null)
                {
                    this.mHttpContext.mRequest.Method = "POST";
                    this.mHttpContext.mRequest.ContentType = "application/octet-stream";
                    this.mHttpContext.mRequest.UserAgent = "wp7 HTTP Client";
                    this.mHttpContext.mRequest.Accept = "*/*";
                    this.mHttpContext.mRequest.Host = this.mHttpContext.mHost;
                    this.mHttpContext.mRequest.Headers.Add("X-Online-Host", this.mHttpContext.mHost);
                    this.mHttpContext.mRequest.Headers.Add("Cache-Control", "no-cache");

                    //this.mHttpContext.mRequest.Headers["X-Online-Host"] = this.mHttpContext.mHost;
                    //this.mHttpContext.mRequest.Headers["Cache-Control"] = "no-cache";
                    //this.mHttpContext.mRequest.Connection = "Keep-Alive";
                }
                else
                {
                    this.mDefineHeader(this.mHttpContext.mRequest);
                }
                this.mHttpContext.mStatus = 1;
                if ((data != null) && (count != 0))
                {
                    httpseq = seq;
                    this.mHttpContext.mBufferInput = new byte[count];
                    Buffer.BlockCopy(data, 0, this.mHttpContext.mBufferInput, 0, count);
                    this.mHttpContext.mAsyncRequestStreamResult = this.mHttpContext.mRequest.BeginGetRequestStream(new AsyncCallback(this.onGetRequestStreamCallback), this.mHttpContext);
                }
                else
                {
                    this.mHttpContext.mAsyncResponseResult = this.mHttpContext.mRequest.BeginGetResponse(new AsyncCallback(this.onRecvHttpCallback), this.mHttpContext);
                }
                return true;
            }
            catch (Exception exception)
            {
                Log.e("Network", "http.send failed: " + exception.Message);
                this.mHttpContext.mStatus = 2;
                this.mHttpContext.release();
                if (this.rspCallback != null)
                {
                    if (action == null)
                    {
                        //action = delegate {
                        this.rspCallback(null);
                        // };
                    }
                    // Deployment.get_Current().get_Dispatcher().BeginInvoke(action);
                }
            }
            return false;
        }

        public static void testHttpClient()
        {
            new Thread(new ThreadStart(HttpClient.testThreadMain)).Start();
        }

        public static void testThreadMain()
        {
            //byte[] data = new byte[0x400];
            //int count = 0;
            //count = Util.loadDataFromFile(ref data, "authBodyTestDat");
            //HttpClient client = new HttpClient("/cgi-bin/micromsg-bin/auth", 1, null);
            //client.send(data, count, null);
            //byte[] outBuf = null;
            //int seq = 0;
            //string cmdUri = null;
            //do
            //{
            //    Thread.Sleep(100);
            //}
            //while (client.receive(ref outBuf, ref seq, ref cmdUri) == ((SocketError) ((int) SocketError.NoData)));
        }

        public class HttpRequestContext
        {
            public IAsyncResult mAsyncReadResult;
            public IAsyncResult mAsyncRequestStreamResult;
            public IAsyncResult mAsyncResponseResult;
            public byte[] mBufferInput;
            public byte[] mBufferOutput;
            public string mCmdUri;
            public string mHost;
            public int mOutputLength;
            public HttpWebRequest mRequest;
            public HttpWebResponse mResponse;
            public int mSeqID;
            public int mStatus;
            public Stream mStreamRequest;
            public Stream mStreamResponse;

            public event OnHttpReceivedProgressDelegate mProgress;

            public void release()
            {
                try
                {
                    if (this.mStreamResponse != null)
                    {
                        this.mStreamResponse.Close();
                        this.mStreamResponse.Dispose();
                        this.mStreamResponse = null;
                    }
                    if (this.mResponse != null)
                    {
                        this.mResponse.Close();
                        this.mResponse = null;
                    }
                }
                catch (Exception exception)
                {
                    Log.e("Network", string.Concat(new object[] { "failed to release httpclient context, seq =", this.mSeqID, ", err: ", exception.Message }));
                }
            }

            public void setProgress(int len)
            {
                if (this.mProgress != null)
                {
                    this.mProgress(len);
                }
            }
        }
    }
}

