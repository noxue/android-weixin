namespace MicroMsg.Protocol
{
    using System;

    public class PackMini
    {
        private static int _MMPKG_BIT_MAX(int bits)
        {
            return ((((int) 1) << bits) - 1);
        }

        private static int _mmpkg_validate(MMPKG_mini_header head)
        {
            if (head == null)
            {
                return -1;
            }
            if (head.compress_algo > _MMPKG_BIT_MAX(2))
            {
                return -3;
            }
            if (head.encrypt_algo > _MMPKG_BIT_MAX(4))
            {
                return -3;
            }
            if (head.server_id_len > _MMPKG_BIT_MAX(4))
            {
                return -3;
            }
            return 0;
        }

        public static void appendIntTobuf(byte[] dstBuf, ref int dstOffset, int srcInt)
        {
            byte[] apcBuffer = new byte[4];
            int count = TLVUtil.EncodeVByte32(srcInt, apcBuffer, 0);
            Buffer.BlockCopy(apcBuffer, 0, dstBuf, dstOffset, count);
            dstOffset += count;
        }

        public static void getIntFrombuf(byte[] srcBuf, ref int srcOffset, ref int dstInt)
        {
            if (srcOffset < srcBuf.Length)
            {
                int num = TLVUtil.DecodeVByte32(ref dstInt, srcBuf, srcOffset);
                srcOffset += num;
            }
        }

        public static int mmpkg_mini_head_hton(MMPKG_mini_header header, out byte[] outbuff)
        {
            outbuff = null;
            if (header == null)
            {
                return -1;
            }
            int num = _mmpkg_validate(header);
            if (num != 0)
            {
                return num;
            }
            byte[] dst = new byte[header.getMaxSize()];
            int dstOffset = 0;
            byte[] src = header.getHeaderBits();
            Buffer.BlockCopy(src, 0, dst, dstOffset, src.Length);
            dstOffset += src.Length;
            byte[] buffer3 = TLVUtil.int2byte(header.ret, 4, true);
            Buffer.BlockCopy(buffer3, 0, dst, dstOffset, buffer3.Length);
            dstOffset += buffer3.Length;
            buffer3 = TLVUtil.int2byte((int) header.uin, 4, true);
            Buffer.BlockCopy(buffer3, 0, dst, dstOffset, buffer3.Length);
            dstOffset += buffer3.Length;
            Buffer.BlockCopy(header.server_id, 0, dst, dstOffset, header.server_id.Length);
            dstOffset += header.server_id.Length;
            appendIntTobuf(dst, ref dstOffset, header.cmd_id);
            appendIntTobuf(dst, ref dstOffset, (int) header.compress_len);
            appendIntTobuf(dst, ref dstOffset, (int) header.compressed_len);
            appendIntTobuf(dst, ref dstOffset, header.cert_version);
            appendIntTobuf(dst, ref dstOffset, header.device_type);
            int count = dstOffset;
            outbuff = new byte[count];
            Buffer.BlockCopy(dst, 0, outbuff, 0, count);
            header.len = (byte) count;
            Buffer.BlockCopy(header.getHeaderBits(), 0, outbuff, 0, 1);
            return 0;
        }

        public static int mmpkg_mini_head_ntoh(byte[] packBuf, out MMPKG_mini_header header)
        {
            header = null;
            if ((packBuf == null) || (packBuf.Length <= 2))
            {
                return -1;
            }
            int offSet = 0;
            header = new MMPKG_mini_header();
            header.setHHeaderBits(packBuf);
            if ((header.len == 0) || (packBuf.Length < header.len))
            {
                return -4;
            }
            offSet += 2;
            header.ret = TLVUtil.byte2int(packBuf, offSet, 4, true);
            offSet += 4;
            header.uin = (uint)TLVUtil.byte2int(packBuf, offSet, 4, true);
            offSet += 4;
            if (header.server_id_len > 0)
            {
                header.server_id = new byte[header.server_id_len];
                Buffer.BlockCopy(packBuf, offSet, header.server_id, 0, header.server_id_len);
                offSet += header.server_id_len;
            }
            int dstInt = 0;
            getIntFrombuf(packBuf, ref offSet, ref dstInt);
            header.cmd_id = (ushort) dstInt;
            getIntFrombuf(packBuf, ref offSet, ref dstInt);
            header.compress_len = (uint) dstInt;
            getIntFrombuf(packBuf, ref offSet, ref dstInt);
            header.compressed_len = (uint) dstInt;
            getIntFrombuf(packBuf, ref offSet, ref dstInt);
            header.cert_version = (ushort) dstInt;
            dstInt = 0;
            getIntFrombuf(packBuf, ref offSet, ref dstInt);
            header.device_type = (ushort) dstInt;
            return 0;
        }
    }
}
