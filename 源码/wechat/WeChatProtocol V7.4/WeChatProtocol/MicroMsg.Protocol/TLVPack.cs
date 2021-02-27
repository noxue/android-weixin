namespace MicroMsg.Protocol
{
    using System;
    using System.Runtime.InteropServices;

    public class TLVPack
    {
        public const int c_iBufGrowUnit = 0x400;
        public const int c_iInitializeSize = 0x400;
        public const int c_iSizeofLength = 4;
        public const int c_iSizeofType = 4;
        public int m_eDefaultMode;
        private int m_iAllocSize;
        private int m_iUsedSize;
        private byte[] m_pcBuf;
        private TLVPackHeader m_ptHeader;
        public const int TLVMAGIC = 0x81;

        public TLVPack(int aiInitBufSize, int mode)
        {
            this.m_eDefaultMode = mode;
            this.m_pcBuf = null;
            this.m_iAllocSize = 0;
            this.m_iUsedSize = 0;
            this.m_ptHeader = new TLVPackHeader();
        }

        public int AddBool(int aiType, bool abVal)
        {
            if (!abVal)
            {
                return this.AddInt(aiType, 0);
            }
            return this.AddInt(aiType, 1);
        }

        public int addByte(int aiType, byte acVal)
        {
            return this.addByteArray(aiType, new byte[] { acVal }, 1);
        }

        public int addByteArray(int aiType, byte[] aoVal, int size)
        {
            if (0 > this.ensureSpace(aiType, size))
            {
                return -2;
            }
            if (this.m_eDefaultMode == 0)
            {
                Buffer.BlockCopy(TLVUtil.int2byte(aiType, 4, true), 0, this.m_pcBuf, this.m_iUsedSize, 4);
                this.m_iUsedSize += 4;
                Buffer.BlockCopy(TLVUtil.int2byte(size, 4, true), 0, this.m_pcBuf, this.m_iUsedSize, 4);
                this.m_iUsedSize += 4;
            }
            else if (this.m_eDefaultMode == 1)
            {
                int num = TLVUtil.EncodeVByte32(aiType, this.m_pcBuf, this.m_iUsedSize);
                this.m_iUsedSize += num;
                int num2 = TLVUtil.EncodeVByte32(size, this.m_pcBuf, this.m_iUsedSize);
                this.m_iUsedSize += num2;
            }
            else
            {
                return -8;
            }
            Buffer.BlockCopy(aoVal, 0, this.m_pcBuf, this.m_iUsedSize, size);
            this.m_iUsedSize += size;
            return 0;
        }

        public int addChar(int aiType, sbyte acVal)
        {
            return this.addByteArray(aiType, new byte[] { (byte) acVal }, 1);
        }

        public int addDWord(int aiType, long awVal)
        {
            byte[] dst = new byte[4];
            Buffer.BlockCopy(TLVTypeTransform.longToByteArrayLH(awVal), 0, dst, 0, 4);
            return this.addByteArray(aiType, dst, 4);
        }

        public int AddInt(int aiType, int aiVal)
        {
            return this.addByteArray(aiType, TLVUtil.int2byte(aiVal, 4, this.m_eDefaultMode == 0), 4);
        }

        public int AddLong(int aiType, long allVal, bool abNetOrder = true)
        {
            byte[] bytes = BitConverter.GetBytes(allVal);
            if (this.m_eDefaultMode != 0)
            {
                int num = bytes.Length / 2;
                for (int i = 0; i < num; i++)
                {
                    byte num2 = bytes[i];
                    bytes[i] = bytes[bytes.Length - i];
                    bytes[bytes.Length - i] = num2;
                }
            }
            return this.addByteArray(aiType, bytes, bytes.Length);
        }

        public int addNestedTLV(int aiType, TLVPack apoVal)
        {
            if (0 > this.ensureSpace(aiType, apoVal.m_iUsedSize))
            {
                return -2;
            }
            int iUsedSize = this.m_iUsedSize;
            if (this.m_eDefaultMode == 0)
            {
                Buffer.BlockCopy(TLVUtil.int2byte(aiType, 4, true), 0, this.m_pcBuf, this.m_iUsedSize, 4);
                this.m_iUsedSize += 4;
                Buffer.BlockCopy(TLVUtil.int2byte(apoVal.m_iUsedSize, 4, true), 0, this.m_pcBuf, this.m_iUsedSize, 4);
                this.m_iUsedSize += 4;
            }
            else if (this.m_eDefaultMode == 1)
            {
                int num2 = TLVUtil.EncodeVByte32(aiType, this.m_pcBuf, this.m_iUsedSize);
                this.m_iUsedSize += num2;
                int num3 = TLVUtil.EncodeVByte32(apoVal.m_iUsedSize, this.m_pcBuf, this.m_iUsedSize);
                this.m_iUsedSize += num3;
            }
            else
            {
                return -8;
            }
            if (apoVal.m_iUsedSize > 0)
            {
                int apiSize = 0;
                apiSize = this.m_iAllocSize - this.m_iUsedSize;
                int num5 = apoVal.CopyTo(this.m_pcBuf, this.m_iUsedSize, ref apiSize);
                if (num5 != 0)
                {
                    this.m_iUsedSize = iUsedSize;
                    return num5;
                }
            }
            this.m_iUsedSize += apoVal.m_iUsedSize;
            return 0;
        }

        public int AddWord(int aiType, int ahVal)
        {
            return this.addByteArray(aiType, new byte[] { (byte) (ahVal & 0xff), (byte) ((ahVal >> 8) & 0xff) }, 2);
        }

        public int Attach(byte[] apcBuf, int aiUsedSize, int aiAllocSize)
        {
            if ((apcBuf == null) || (aiUsedSize == 0))
            {
                return -4;
            }
            if ((aiAllocSize != 0) && (aiAllocSize < aiUsedSize))
            {
                return -4;
            }
            if (!this.isValidTLVPack(apcBuf, aiUsedSize, 0))
            {
                return -5;
            }
            if (this.m_pcBuf != null)
            {
                this.m_pcBuf = null;
            }
            this.m_pcBuf = apcBuf;
            this.m_ptHeader.h_pcBuf = this.m_pcBuf;
            this.m_iAllocSize = (aiAllocSize == 0) ? aiUsedSize : aiAllocSize;
            this.m_iUsedSize = aiUsedSize;
            this.m_eDefaultMode = this.m_ptHeader.getMode();
            return 0;
        }

        private void clear()
        {
            if (this.m_pcBuf == null)
            {
                this.m_iAllocSize = 0;
                this.m_iUsedSize = 0;
                this.m_ptHeader = null;
            }
            else if (this.m_iAllocSize < this.m_ptHeader.getSize())
            {
                this.m_ptHeader = null;
                this.m_iUsedSize = 0;
            }
            else
            {
                TLVUtil.fillByteArray(this.m_pcBuf, 0);
                this.m_iUsedSize = this.m_ptHeader.getSize();
                this.m_ptHeader.h_pcBuf = this.m_pcBuf;
                this.m_ptHeader.offset = 0;
                this.m_ptHeader.setMagic(0x81).setCheckSum(new byte[2]).setUsedSize(0).setMode((byte) this.m_eDefaultMode);
            }
        }

        public int CopyFrom(byte[] apcBuf, int aiUsedSize, int offset)
        {
            return this.CopyFrom(apcBuf, aiUsedSize, offset, 0);
        }

        public int CopyFrom(byte[] apcBuf, int aiUsedSize, int offset, int aiAllocSize)
        {
            if ((((apcBuf == null) && (aiUsedSize < this.m_ptHeader.getSize())) || ((aiAllocSize != 0) && (aiAllocSize < aiUsedSize))) || (aiUsedSize < 0))
            {
                return -4;
            }
            if (!this.isValidTLVPack(apcBuf, aiUsedSize, offset))
            {
                return -5;
            }
            if ((this.m_pcBuf == null) || (this.m_pcBuf.Length < aiUsedSize))
            {
                byte[] buffer = new byte[aiUsedSize];
                if (buffer == null)
                {
                    return -1;
                }
                this.m_pcBuf = buffer;
            }
            Buffer.BlockCopy(apcBuf, offset, this.m_pcBuf, 0, aiUsedSize);
            this.m_ptHeader.h_pcBuf = this.m_pcBuf;
            this.m_iAllocSize = (aiAllocSize == 0) ? aiUsedSize : aiAllocSize;
            this.m_iUsedSize = aiUsedSize;
            this.m_eDefaultMode = this.m_ptHeader.getMode();
            return 0;
        }

        public int CopyTo(byte[] apcBuf, int offset, ref int apiSize)
        {
            if ((apcBuf == null) || (apiSize == 0))
            {
                return -4;
            }
            if (apiSize < this.m_iUsedSize)
            {
                apiSize = this.m_iUsedSize;
                return -7;
            }
            byte[] x = TLVUtil.getCheckSum(this.m_pcBuf, this.m_ptHeader.getSize(), this.m_iUsedSize - this.m_ptHeader.getSize());
            this.m_ptHeader.setCheckSum(x);
            int num2 = this.m_iUsedSize - this.m_ptHeader.getSize();
            this.m_ptHeader.setUsedSize(num2);
            if (this.m_iUsedSize > 0)
            {
                Buffer.BlockCopy(this.m_pcBuf, 0, apcBuf, offset, this.m_iUsedSize);
            }
            apiSize = this.m_iUsedSize;
            return 0;
        }

        public int Detach(ref byte[] appcBufFather, ref int apiUsedSizeFather, ref int apiAllocSizeFather)
        {
            if (((appcBufFather == null) && (apiUsedSizeFather == 0)) && (apiAllocSizeFather == 0))
            {
                this.m_pcBuf = null;
                this.clear();
                return 0;
            }
            if (appcBufFather == null)
            {
                return -4;
            }
            appcBufFather = null;
            apiUsedSizeFather = 0;
            apiAllocSizeFather = 0;
            appcBufFather = this.m_pcBuf;
            apiUsedSizeFather = this.m_iUsedSize;
            apiAllocSizeFather = this.m_iAllocSize;
            byte[] x = TLVUtil.getCheckSum(this.m_pcBuf, this.m_ptHeader.getSize(), this.m_iUsedSize - this.m_ptHeader.getSize());
            this.m_ptHeader.setCheckSum(x);
            int num = this.m_iUsedSize - this.m_ptHeader.getSize();
            this.m_ptHeader.setUsedSize(num);
            this.m_pcBuf = null;
            this.clear();
            return 0;
        }

        private int ensureSpace(int aiType, int aiReqSize)
        {
            int num = 0;
            if (this.m_eDefaultMode == 0)
            {
                num = (aiReqSize + 4) + 4;
            }
            else if (this.m_eDefaultMode == 1)
            {
                int apuValue = 0;
                byte[] apcBuffer = new byte[6];
                int num3 = TLVUtil.DecodeVByte32(ref apuValue, apcBuffer, 0);
                aiType = apuValue;
                int num4 = TLVUtil.DecodeVByte32(ref apuValue, apcBuffer, 0);
                num = (num3 + num4) + aiReqSize;
            }
            else
            {
                return -8;
            }
            if ((this.m_pcBuf == null) || (this.m_iUsedSize <= 0))
            {
                num += this.m_ptHeader.getSize();
            }
            if ((num < 0) || ((num + this.m_iUsedSize) < 0))
            {
                return -3;
            }
            if (num > (this.m_iAllocSize - this.m_iUsedSize))
            {
                int num5 = this.m_iAllocSize + 0x400;
                while (num5 < (num + this.m_iUsedSize))
                {
                    num5 *= 2;
                }
                if (num5 < 0)
                {
                    num5 = num + this.m_iUsedSize;
                }
                byte[] dst = new byte[num5];
                bool flag = false;
                if ((this.m_pcBuf != null) || (this.m_iUsedSize > 0))
                {
                    Buffer.BlockCopy(this.m_pcBuf, 0, dst, 0, this.m_iUsedSize);
                }
                else
                {
                    flag = true;
                }
                this.m_iAllocSize = num5;
                this.m_pcBuf = dst;
                this.m_ptHeader.h_pcBuf = this.m_pcBuf;
                if (flag)
                {
                    this.clear();
                }
            }
            return 0;
        }

        public int getAllocSize()
        {
            return this.m_iAllocSize;
        }

        public int GetBool(int aiType, ref bool apbVal)
        {
            byte[] apoVal = null;
            int num = this.getByteArray(aiType, ref apoVal);
            if (num < 0)
            {
                return num;
            }
            if (TLVUtil.byte2int(apoVal, 0, 4, this.m_eDefaultMode == 0) == 0)
            {
                apbVal = false;
            }
            else
            {
                apbVal = true;
            }
            return 0;
        }

        public byte[] getBuf()
        {
            return this.m_pcBuf;
        }

        public int GetByte(int aiType, ref byte apcVal)
        {
            byte[] apoVal = null;
            int num = this.getByteArray(aiType, ref apoVal);
            apcVal = apoVal[0];
            return num;
        }

        public int getByteArray(int aiType, ref byte[] apoVal)
        {
            if (this.m_iUsedSize == this.m_ptHeader.getSize())
            {
                return -6;
            }
            FixedSizeTLVBody body = new FixedSizeTLVBody();
            tVariableSizeTLVItem item = new tVariableSizeTLVItem();
            TLVBody body2 = null;
            if (this.m_eDefaultMode == 0)
            {
                body2 = body;
            }
            else if (this.m_eDefaultMode == 1)
            {
                body2 = item;
            }
            else
            {
                return -8;
            }
            if (body2.MapTo(this.m_pcBuf, this.m_iUsedSize, this.m_ptHeader.getSize()))
            {
                while ((body2.iType != aiType) && (body2.iNextOffset != 0))
                {
                    if (!body2.MapTo(this.m_pcBuf, this.m_iUsedSize, body2.iNextOffset))
                    {
                        return -5;
                    }
                }
                if (body2.iType != aiType)
                {
                    return -6;
                }
                apoVal = new byte[body2.iLength];
                Buffer.BlockCopy(body2.pcValPtr, body2.pcValPtrOffset, apoVal, 0, body2.iLength);
                return 0;
            }
            return -5;
        }

        public int GetDWord(int aiType, ref uint apuVal)
        {
            return this.GetUInt(aiType, ref apuVal);
        }

        public int GetInt(int aiType, ref int apiVal)
        {
            byte[] apoVal = null;
            int num = this.getByteArray(aiType, ref apoVal);
            if (num < 0)
            {
                return num;
            }
            apiVal = TLVUtil.byte2int(apoVal, 0, 4, this.m_eDefaultMode == 0);
            return 0;
        }

        public int GetLong(int aiType, ref int apuVal)
        {
            return this.GetInt(aiType, ref apuVal);
        }

        public int getNestedTLVBuf(int aiType, TLVPack appoVal)
        {
            if (appoVal == null)
            {
                return -4;
            }
            if (this.m_iUsedSize == this.m_ptHeader.getSize())
            {
                return -6;
            }
            FixedSizeTLVBody body = new FixedSizeTLVBody();
            tVariableSizeTLVItem item = new tVariableSizeTLVItem();
            TLVBody body2 = null;
            if (this.m_eDefaultMode == 0)
            {
                body2 = body;
            }
            else if (this.m_eDefaultMode == 1)
            {
                body2 = item;
            }
            else
            {
                return -8;
            }
            if (body2.MapTo(this.m_pcBuf, this.m_iUsedSize, this.m_ptHeader.getSize()))
            {
                while ((body2.iType != aiType) && (body2.iNextOffset != 0))
                {
                    if (!body2.MapTo(this.m_pcBuf, this.m_iUsedSize, body2.iNextOffset))
                    {
                        return -5;
                    }
                }
                if (body2.iType != aiType)
                {
                    return -6;
                }
                if ((body2.iLength > 0) && (0 > appoVal.CopyFrom(body2.pcValPtr, body2.iLength, body2.pcValPtrOffset)))
                {
                    appoVal.m_pcBuf = null;
                    appoVal.m_ptHeader.h_pcBuf = null;
                    return -5;
                }
                return 0;
            }
            return -5;
        }

        public int GetShort(int aiType, ref short aphVal)
        {
            byte[] apoVal = null;
            int num = this.getByteArray(aiType, ref apoVal);
            if (num < 0)
            {
                return num;
            }
            if (this.m_eDefaultMode == 0)
            {
                aphVal = (short) ((((short) (apoVal[0] & 0xff)) << 8) | (apoVal[1] & 0xff));
            }
            else
            {
                aphVal = (short) ((((short) (apoVal[1] & 0xff)) << 8) | (apoVal[0] & 0xff));
            }
            return 0;
        }

        public int GetUInt(int aiType, ref uint apuVal)
        {
            byte[] apoVal = null;
            int num = this.getByteArray(aiType, ref apoVal);
            if (num < 0)
            {
                return num;
            }
            if (this.m_eDefaultMode == 0)
            {
                apuVal = (uint) (((((apoVal[0] & 0xff) << 0x18) | ((apoVal[1] & 0xff) << 0x10)) | ((apoVal[2] & 0xff) << 8)) | (apoVal[3] & 0xff));
            }
            else
            {
                apuVal = (uint) (((((apoVal[3] & 0xff) << 0x18) | ((apoVal[2] & 0xff) << 0x10)) | ((apoVal[1] & 0xff) << 8)) | (apoVal[0] & 0xff));
            }
            return 0;
        }

        public int getUsedSize()
        {
            return this.m_iUsedSize;
        }

        public int GetWord(int aiType, ref int apwVal)
        {
            byte[] apoVal = null;
            int num = this.getByteArray(aiType, ref apoVal);
            if (num < 0)
            {
                return num;
            }
            apwVal = TLVUtil.byte2int(apoVal, 0, 4, this.m_eDefaultMode == 0);
            return 0;
        }

        public bool isValidTLVPack(byte[] apcBuf, int aiUsedSize, int offset)
        {
            if (apcBuf != null)
            {
                if (aiUsedSize < this.m_ptHeader.getSize())
                {
                    return false;
                }
                TLVPackHeader header = new TLVPackHeader {
                    h_pcBuf = apcBuf,
                    offset = offset
                };
                if (header.getMagic() != 0x81)
                {
                    return false;
                }
                int aiSize = aiUsedSize - header.getSize();
                if (header.getUsedSize() != aiSize)
                {
                    return false;
                }
                byte[] buffer = TLVUtil.getCheckSum(apcBuf, offset + header.getSize(), aiSize);
                byte[] buffer2 = header.getCheckSum();
                if ((buffer[0] != buffer2[0]) || (buffer[1] != buffer2[1]))
                {
                    return false;
                }
                if (aiSize == 0)
                {
                    return true;
                }
                FixedSizeTLVBody body = new FixedSizeTLVBody();
                tVariableSizeTLVItem item = new tVariableSizeTLVItem();
                TLVBody body2 = null;
                if (header.getMode() == 0)
                {
                    body2 = body;
                }
                else if (header.getMode() == 1)
                {
                    body2 = item;
                }
                else
                {
                    return false;
                }
                if (body2.MapTo(apcBuf, offset + aiUsedSize, offset + header.getSize()))
                {
                    while (body2.iNextOffset != 0)
                    {
                        if (!body2.MapTo(apcBuf, offset + aiUsedSize, body2.iNextOffset))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public int izeInt(int aiType)
        {
            return this.SizeNumber(aiType, 4, 0);
        }

        public int izeInt(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 4, eTLVMode);
        }

        public int SizeBool(int aiType)
        {
            return this.SizeNumber(aiType, 4, 0);
        }

        public int SizeBool(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 4, eTLVMode);
        }

        public int SizeBuf(int aiType, int aiLen)
        {
            return this.SizeNumber(aiType, aiLen, 0);
        }

        public int SizeBuf(int aiType, int aiLen, int eTLVMode)
        {
            return this.SizeNumber(aiType, aiLen, eTLVMode);
        }

        public int SizeByte(int aiType)
        {
            return this.SizeNumber(aiType, 4, 0);
        }

        public int SizeByte(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 4, eTLVMode);
        }

        public int SizeChar(int aiType)
        {
            return this.SizeNumber(aiType, 2, 0);
        }

        public int SizeChar(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 2, eTLVMode);
        }

        public int SizeDWord(int aiType)
        {
            return this.SizeNumber(aiType, 4, 0);
        }

        public int SizeDWord(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 4, eTLVMode);
        }

        public int SizeHeader()
        {
            return this.m_ptHeader.getSize();
        }

        public int SizeLength(int aiLen, int eTLVMode)
        {
            if (eTLVMode == 0)
            {
                return 4;
            }
            if (eTLVMode != 1)
            {
                return 0x7fffffff;
            }
            if (aiLen == -1)
            {
                return 0x7fffffff;
            }
            byte[] apcBuffer = new byte[6];
            return TLVUtil.EncodeVByte32(aiLen, apcBuffer, 0);
        }

        public int SizeLong(int aiType)
        {
            return this.SizeNumber(aiType, 4, 0);
        }

        public int SizeLong(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 4, eTLVMode);
        }

        public int SizeLongLong(int aiType)
        {
            return this.SizeNumber(aiType, 8, 0);
        }

        public int SizeLongLong(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 8, eTLVMode);
        }

        public int SizeNumber(int aiType, int aiSizeOfType, int eTLVMode)
        {
            if (eTLVMode == 0)
            {
                return (8 + aiSizeOfType);
            }
            if (eTLVMode == 1)
            {
                byte[] apcBuffer = new byte[6];
                int num = TLVUtil.EncodeVByte32(aiType, apcBuffer, 0);
                int num2 = TLVUtil.EncodeVByte32(aiSizeOfType, apcBuffer, 0);
                return ((num + num2) + aiSizeOfType);
            }
            return 0x7fffffff;
        }

        public int SizeShort(int aiType)
        {
            return this.SizeNumber(aiType, 2, 0);
        }

        public int SizeShort(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 2, eTLVMode);
        }

        public int SizeTLV(int aiType, TLVPack apoVal)
        {
            return this.SizeNumber(aiType, apoVal.m_iUsedSize, 0);
        }

        public int SizeTLV(int aiType, TLVPack apoVal, int eTLVMode)
        {
            return this.SizeNumber(aiType, apoVal.m_iUsedSize, eTLVMode);
        }

        public int SizeType(int aiType, int eTLVMode)
        {
            if (eTLVMode == 0)
            {
                return 4;
            }
            if (eTLVMode != 1)
            {
                return 0x7fffffff;
            }
            if (aiType == 0)
            {
                return 0x7fffffff;
            }
            byte[] apcBuffer = new byte[6];
            return TLVUtil.EncodeVByte32(aiType, apcBuffer, 0);
        }

        public int SizeUInt(int aiType)
        {
            return this.SizeNumber(aiType, 4, 0);
        }

        public int SizeUInt(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 4, eTLVMode);
        }

        public int SizeULongLong(int aiType)
        {
            return this.SizeNumber(aiType, 8, 0);
        }

        public int SizeULongLong(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 8, eTLVMode);
        }

        public int SizeWord(int aiType)
        {
            return this.SizeNumber(aiType, 2, 0);
        }

        public int SizeWord(int aiType, int eTLVMode)
        {
            return this.SizeNumber(aiType, 2, eTLVMode);
        }

        public class FixedSizeTLVBody : TLVPack.TLVBody
        {
            public override bool MapTo(byte[] apcSrc, int aiSrcSize, int aiOffset)
            {
                if (((apcSrc == null) || (aiSrcSize <= 0)) || ((aiOffset < 0) || (aiOffset >= aiSrcSize)))
                {
                    return false;
                }
                int num = TLVUtil.byte2int(apcSrc, aiOffset, 4, true);
                int num2 = TLVUtil.byte2int(apcSrc, aiOffset + 4, 4, true);
                base.pcValPtr = apcSrc;
                base.pcValPtrOffset = (aiOffset + 4) + 4;
                int num3 = ((aiOffset + 4) + 4) + num2;
                if (num2 < 0)
                {
                    return false;
                }
                if (base.pcValPtrOffset > aiSrcSize)
                {
                    return false;
                }
                if (num3 > aiSrcSize)
                {
                    return false;
                }
                if (num3 == aiSrcSize)
                {
                    base.iNextOffset = 0;
                }
                else
                {
                    base.iNextOffset = num3;
                }
                base.iType = num;
                base.iLength = num2;
                base.pcValPtr = apcSrc;
                return true;
            }
        }

        public abstract class TLVBody
        {
            public int iLength = 0;
            public int iNextOffset = 0;
            public int iType = 0;
            public byte[] pcValPtr = null;
            public int pcValPtrOffset = 0;

            public abstract bool MapTo(byte[] apcSrc, int aiSrcSize, int aiOffset);
        }

        public class TLVPackHeader
        {
            public byte[] h_pcBuf;
            public int offset;

            public byte[] getBuf()
            {
                return this.h_pcBuf;
            }

            public byte[] getCheckSum()
            {
                return new byte[] { this.h_pcBuf[this.offset + 2], this.h_pcBuf[this.offset + 3] };
            }

            public byte getMagic()
            {
                return this.h_pcBuf[this.offset];
            }

            public byte getMode()
            {
                return this.h_pcBuf[this.offset + 1];
            }

            public int getSize()
            {
                return 12;
            }

            public int getUsedSize()
            {
                return ((((this.h_pcBuf[this.offset + 4] & 0xff) | ((this.h_pcBuf[this.offset + 5] & 0xff) << 8)) | ((this.h_pcBuf[this.offset + 6] & 0xff) << 0x10)) | ((this.h_pcBuf[this.offset + 7] & 0xff) << 0x18));
            }

            public void setBuf(byte[] buf)
            {
                this.h_pcBuf = buf;
            }

            public TLVPack.TLVPackHeader setCheckSum(byte[] x)
            {
                this.h_pcBuf[this.offset + 2] = x[0];
                this.h_pcBuf[this.offset + 3] = x[1];
                return this;
            }

            public TLVPack.TLVPackHeader setMagic(byte x)
            {
                this.h_pcBuf[this.offset] = x;
                return this;
            }

            public TLVPack.TLVPackHeader setMode(byte x)
            {
                this.h_pcBuf[this.offset + 1] = x;
                return this;
            }

            public TLVPack.TLVPackHeader setUsedSize(int x)
            {
                this.h_pcBuf[this.offset + 4] = (byte) (x & 0xff);
                this.h_pcBuf[this.offset + 5] = (byte) ((x >> 8) & 0xff);
                this.h_pcBuf[this.offset + 6] = (byte) ((x >> 0x10) & 0xff);
                this.h_pcBuf[this.offset + 7] = (byte) ((x >> 0x18) & 0xff);
                return this;
            }
        }

        public class tVariableSizeTLVItem : TLVPack.TLVBody
        {
            public override bool MapTo(byte[] apcSrc, int aiSrcSize, int aiOffset)
            {
                if (((apcSrc == null) || (aiSrcSize <= 0)) || ((aiOffset < 0) || (aiOffset >= aiSrcSize)))
                {
                    return false;
                }
                int apuValue = 0;
                int num2 = TLVUtil.DecodeVByte32(ref apuValue, apcSrc, aiOffset);
                int num3 = apuValue;
                int num4 = TLVUtil.DecodeVByte32(ref apuValue, apcSrc, aiOffset + num2);
                int num5 = apuValue;
                base.pcValPtrOffset = (aiOffset + num2) + num4;
                int num6 = ((aiOffset + num2) + num4) + num5;
                if (num5 < 0)
                {
                    return false;
                }
                if (base.pcValPtrOffset > aiSrcSize)
                {
                    return false;
                }
                if (num6 > aiSrcSize)
                {
                    return false;
                }
                if (num6 == aiSrcSize)
                {
                    base.iNextOffset = 0;
                }
                else
                {
                    base.iNextOffset = num6;
                }
                base.iType = num3;
                base.iLength = num5;
                base.pcValPtr = apcSrc;
                return true;
            }
        }
    }
}

