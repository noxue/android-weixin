namespace MicroMsg.Network
{
    using MicroMsg.Common.Utils;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class SocketClient
    {
        private const int BUFFER_SIZE = 0x20000;
        private byte[] mBufferOutput = new byte[0x40000];
        private byte[] mBufferReceived = new byte[0x20000];
        private ManualResetEvent mClientDoneEvent = new ManualResetEvent(false);
        private int mOutputLength;
        private int mRecvCountTrans;
        private EventHandler<SocketAsyncEventArgs> mRecvEventHandler;
        private int mRecvOffsetTrans;
        private SocketError mRecvResponseResult = ((SocketError) SocketError.TimedOut);
        private SocketAsyncEventArgs mRecvSocketEventArg;
        private int mRecvStatus;
        public Socket mSocket;
        private const int RECV_STATUS_DONE = 2;
        private const int RECV_STATUS_ING = 1;
        private const int RECV_STATUS_READY = 0;

        public void appendData(byte[] buffer, int offset, int count)
        {
            if ((this.mOutputLength + count) > this.mBufferOutput.Length)
            {
                int length = this.mBufferOutput.Length;
                if (length > 0x80000)
                {
                    this.mOutputLength = 0;
                    return;
                }
                byte[] mBufferOutput = this.mBufferOutput;
                this.mBufferOutput = new byte[length * 2];
                Buffer.BlockCopy(mBufferOutput, 0, this.mBufferOutput, 0, this.mOutputLength);
            }
            Buffer.BlockCopy(buffer, offset, this.mBufferOutput, this.mOutputLength, count);
            this.mOutputLength += count;
        }

        private void checkOutput(ref byte[] outBuf, ref int offset, ref int count)
        {
            int totalLen = 0;
            if (this.isRecvCompleted(ref totalLen))
            {
                outBuf = new byte[totalLen];
                count = totalLen;
                offset = 0;
                Buffer.BlockCopy(mBufferOutput, 0, outBuf, 0, totalLen);
                if (this.mOutputLength > totalLen)
                {
                    Buffer.BlockCopy(mBufferOutput, totalLen, mBufferOutput, 0, this.mOutputLength - totalLen);
                    this.mOutputLength -= totalLen;
                }
                else
                {
                    int num2 = 0;
                    Util.writeInt(0, ref mBufferOutput, ref num2);
                    this.mOutputLength = 0;
                }
            }
        }

        private bool checkReady()
        {
            EventHandler<SocketAsyncEventArgs> handler = null;
            if (!this.isConnect())
            {
                return false;
            }
            if (this.mRecvSocketEventArg == null)
            {
                this.mRecvSocketEventArg = new SocketAsyncEventArgs();
            }
            if (this.mRecvEventHandler == null)
            {
                if (handler == null)
                {
                    handler = delegate (object s, SocketAsyncEventArgs e) {
                        this.onHandleBufferReady(e);
                    };
                }
                this.mRecvEventHandler = handler;
                //
                this.mRecvSocketEventArg.Completed+=this.mRecvEventHandler;
            }
            return true;
        }

        public void Close()
        {
            try
            {
                this.mRecvStatus = 0;
                this.mOutputLength = 0;
                if (this.mRecvSocketEventArg != null)
                {
                    this.mRecvSocketEventArg.Dispose();
                    this.mRecvSocketEventArg = null;
                }
                this.mRecvEventHandler = null;
                if (this.mSocket != null)
                {
                    this.mSocket.Dispose();
                    this.mSocket.Close();
                    this.mSocket = null;
                }
            }
            catch (SocketException exception)
            {
                Log.e("Network", "socket close failed: " + exception.Message);
            }
        }

        public SocketError Connect(string hostName, int portNumber)
        {
            EventHandler<SocketAsyncEventArgs> handler = null;
            SocketError result = (SocketError) SocketError.TimedOut;
            Log.i("Network", string.Concat(new object[] { "Try connect :", hostName, ": ", portNumber }));
            try
            {
                DnsEndPoint point = new DnsEndPoint(hostName, portNumber);
                this.mSocket = new Socket((AddressFamily) AddressFamily.InterNetwork, (SocketType) SocketType.Stream, (ProtocolType) ProtocolType.Tcp);
                this.mSocket.ReceiveBufferSize = 0x10000;
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.RemoteEndPoint=((EndPoint) point);
                if (handler == null)
                {
                    handler = delegate (object s, SocketAsyncEventArgs e) {
                        result = e.SocketError;
                        this.mClientDoneEvent.Set();
                    };
                }
                args.Completed+=handler;
                this.mClientDoneEvent.Reset();
                this.mSocket.ConnectAsync(args);
                this.mClientDoneEvent.WaitOne(0x2710);
                return result;
            }
            catch (SocketException exception)
            {
                Log.e("Network", "socket connect failed: " + exception.Message);
                return (SocketError) SocketError.SocketError;
            }
        }

        private SocketError doReceive()
        {
            try
            {
                this.mRecvSocketEventArg.SetBuffer(this.mBufferReceived, 0, this.mBufferReceived.Length);
                this.mSocket.ReceiveAsync(this.mRecvSocketEventArg);
                return (SocketError) SocketError.Success;
            }
            catch (SocketException exception)
            {
                Log.e("Network", "Receive failed: " + exception.Message);
                return (SocketError) SocketError.ConnectionAborted;
            }
        }

        public bool isConnect()
        {
            if (this.mSocket == null)
            {
                return false;
            }
            if (!this.mSocket.Connected)
            {
                return false;
            }
            return true;
        }

        private bool isRecvCompleted(ref int totalLen)
        {
            if (this.mOutputLength < 0x10)
            {
                return false;
            }
            int offset = 0;
            totalLen = Util.readInt(mBufferOutput, ref offset);
            if ((totalLen > 0x20000) || (totalLen < 0x10))
            {
                this.mOutputLength = 0;
                return false;
            }
            return (this.mOutputLength >= totalLen);
        }

        private void onHandleBufferReady(SocketAsyncEventArgs e)
        {
            if (this.mSocket == null)
            {
                Log.e("Network", "recv data , but socket had been closed");
            }
            else if (e.SocketError != ((SocketError) ((int) SocketError.Success)))
            {
                this.mRecvResponseResult = e.SocketError;
                this.mRecvStatus = 2;
            }
            else if (e.BytesTransferred != 0)
            {
                this.mRecvResponseResult = e.SocketError;
                this.mRecvCountTrans = e.BytesTransferred;
                this.mRecvOffsetTrans = e.Offset;
                this.appendData(this.mBufferReceived, this.mRecvOffsetTrans, this.mRecvCountTrans);
                this.mRecvStatus = 2;
                NetHandler.wakeUp();
            }
        }

        public SocketError Receive(ref byte[] outBuf, ref int offset, ref int count)
        {
            if (!this.checkReady())
            {
                return (SocketError) SocketError.ConnectionAborted;
            }
            if (this.mRecvStatus == 1)
            {
                this.checkOutput(ref outBuf, ref offset, ref count);
                return (SocketError) SocketError.Success;
            }
            if (this.mRecvStatus == 2)
            {
                this.checkOutput(ref outBuf, ref offset, ref count);
                this.mRecvStatus = 0;
                return this.mRecvResponseResult;
            }
            if (this.mRecvStatus == 0)
            {
                this.mRecvStatus = 1;
                return this.doReceive();
            }
            return (SocketError) SocketError.Success;
        }

        public SocketError Send(byte[] data)
        {
            EventHandler<SocketAsyncEventArgs> handler2 = null;
            SocketError response = (SocketError) SocketError.Success;
            try
            {
                if ((this.mSocket != null) && this.mSocket.Connected)
                {
                    using (SocketAsyncEventArgs args = new SocketAsyncEventArgs
                    {
                        UserToken = null
                    })
                    {
                        if (handler2 == null)
                        {
                            handler2 = delegate(object s, SocketAsyncEventArgs e)
                            {
                                response = e.SocketError;
                                this.mClientDoneEvent.Set();
                            };
                        }
                        EventHandler<SocketAsyncEventArgs> handler = handler2;
                        args.Completed += handler;
                        args.SetBuffer(data, 0, data.Length);
                        this.mClientDoneEvent.Reset();
                        this.mSocket.SendAsync(args);
                        this.mClientDoneEvent.WaitOne(0x1388);
                        return response;
                    }
                }
                return (SocketError) SocketError.NotInitialized;
            }
            catch (SocketException exception)
            {
                Log.e("Network", "socket send failed: " + exception.Message);
                return (SocketError) SocketError.SocketError;
            }
        }
    }
}

