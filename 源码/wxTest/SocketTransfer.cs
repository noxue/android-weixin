using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 微信挂机
{
    public class SocketTransfer
    {
        public byte[] SendPacket(string ip, int port, byte[] sendPacket)
        {
            byte[] bytResult = null;
            do
            {
                try
                {
                    IPEndPoint ipp = new IPEndPoint(IPAddress.Parse(ip), port);
                    Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    soc.Connect(ipp);
                    int ret = soc.Send(sendPacket);//发送

                    byte[] bytLen = new byte[4];
                    ret = soc.Receive(bytLen, 0, 4, SocketFlags.None);
                    int len = bytLen[2] * 256 + bytLen[3];
                    bytResult = new byte[len];
                    int readLen = 4;
                    while (readLen < len)
                    {
                        ret = soc.Receive(bytResult, readLen, len - readLen, SocketFlags.None);
                        readLen += ret;
                    }
                    bytResult = bytResult.Take(len).ToArray();
                }
                catch (Exception ex)
                {
                    //  throw;
                }
                if (bytResult == null || bytResult.Length == 0)
                {
                    Thread.Sleep(1000 * 5);
                }
                else
                {
                    break;
                }
            } while (true);

            return bytResult;
        }

        public byte[] SendLBSPacket(string ip, int port, byte[] sendPacket)
        {
            IPEndPoint ipp = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipp);
            int ret = soc.Send(sendPacket);
            byte[] bytResult = new byte[40000];
            Thread.Sleep(300);
            ret = soc.Receive(bytResult);
            bytResult = bytResult.Take(ret).ToArray();

            return bytResult;
        }
    }
}
