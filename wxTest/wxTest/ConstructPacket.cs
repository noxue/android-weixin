using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace 微信挂机
{
    public class ConstructPacket
    {
        /// <summary>
        /// 构造数据包
        /// </summary>
        /// <param name="lengthBeforeZip">压缩前长度</param>
        /// <param name="lengthAfterZip">压缩后长度</param>
        /// <param name="rsaDataPacket">加密的数据</param>
        /// <param name="deviceID">硬件id</param>
        /// <returns></returns>
        public static byte[] AuthRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] rsaDataPacket, string deviceID, ushort cmd)
        {
            byte[] frontPacket = {
                                     0x4E, 0x70, 0x26, 0x05, 0x10, 0x34, 0x00, 0x00, 0x00, 0x00
                                 };
            byte[] endTag = { 0xae, 0x01, 0x02 };

            byte[] packet = frontPacket.Concat(toVariant(cmd)).Concat(toVariant(lengthBeforeZip)).Concat(toVariant(lengthAfterZip)).Concat(endTag).ToArray();
            int HeadLen = packet.Length;

            packet[0] = (byte)((HeadLen * 4) + 2);

            packet = packet.Concat(rsaDataPacket).ToArray();

            return packet;
        }

        public static byte[] QRCodeRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] rsaDataPacket, string deviceID, ushort cmd)
        {
            byte[] frontPacket = {
                                     0x4E, 0x70, 0x26, 0x05, 0x10, 0x34, 0x00, 0x00, 0x00, 0x00
                                 };
            byte[] endTag = { 0x89, 0x01, 0x02 };

            byte[] packet = frontPacket.Concat(toVariant(cmd)).Concat(toVariant(lengthBeforeZip)).Concat(toVariant(lengthAfterZip)).Concat(endTag).ToArray();
            int HeadLen = packet.Length;

            packet[0] = (byte)((HeadLen * 4) + 1);

            packet = packet.Concat(rsaDataPacket).ToArray();

            return packet;
        }

        public static byte[] RegRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] rsaDataPacket, string deviceID, int cmd)
        {
            byte[] frontPacket = {
                                     0xBF, 0x85, 0x10, 0x26, 0x06, 0x06, 0x36, 0x00, 0x00, 0x00, 0x00
                                 };
            byte[] endTag = { 0xae, 0x01, 0x02, 0x00, 0x01, 0x23, 0x32, 0x20 };

            byte[] packet = frontPacket.Concat(toVariant(cmd)).Concat(toVariant(lengthBeforeZip)).Concat(toVariant(lengthAfterZip)).Concat(endTag).ToArray();
            int HeadLen = packet.Length;

            packet[1] = (byte)((HeadLen * 4) + 1);

            packet = packet.Concat(rsaDataPacket).ToArray();

            return packet;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthBeforeZip"></param>
        /// <param name="lengthAfterZip"></param>
        /// <param name="aesDataPacket"></param>
        /// <param name="uin"></param>
        /// <param name="deviceID"></param>
        /// <param name="_byteVar"></param>
        /// <returns></returns>
        public static byte[] CommonRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] aesDataPacket, uint uin, string deviceID, 
            short cmd, short cmd2, byte[] cookie, uint check)
        {
            byte[] frontPacket = {
                                     0xBF, 0x62, 0x50, 0x26, 0x06, 0x06, 0x36
                                 };
            byte[] endTag = {0x02 };
            byte[] byteUin = new byte[4];
            byteUin[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteUin[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteUin[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteUin[3] = (byte)((uin & 0x000000ff) & 0xff);            

            byte[] packet = frontPacket.Concat(byteUin).Concat(cookie).Concat(toVariant(cmd2)).Concat(toVariant(lengthBeforeZip)).Concat(toVariant(lengthAfterZip)).Concat(toVariant(10000)).Concat(endTag).Concat(toVariant((int)check)).Concat(toVariant(0x01004567)).ToArray();
            int HeadLen = packet.Length;

            packet[1] = (byte)((HeadLen * 4) + 1);
            packet[2] = (byte)(0x50 + cookie.Length);
            packet = packet.Concat(aesDataPacket).ToArray();

            return packet;
        }

        static byte[] toVariant(int toValue)
        {
            uint va = (uint)toValue;
            List<byte> result = new List<byte>();

            while (va >= 0x80)
            {
                result.Add((byte)(0x80 + va % 0x80));
                va /= 0x80;
            }
            result.Add((byte)(va % 0x80));

            return result.ToArray();
        }
    }
}
