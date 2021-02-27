namespace MicroMsg.Storage
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;


    public class MsgTrans : StorageItem
    {
        private byte[] _bytesXmlData;
        private long _nCreateTime;
        private int _nDuration;
        private int _nEndFlag;
        private uint _nLastModifyTime;
        private int _nMsgLocalID;
        private int _nMsgSvrID;
        private int _nMsgTransID;
        private int _nRecordLength;
        private int _nStatus;
        private int _nTotalDataLen;
        private int _nTransDataLen;
        private int _nTransType;
        private string _strClientMsgId;
        private string _strFromUserName;
        private string _strImagePath;
        private string _strThumbnail;
        private string _strToUserName;
    
        
        private const uint Field_bytesXmlData = 0x20000;
        private const uint Field_nCreateTime = 0x800;
        private const uint Field_nDuration = 0x2000;
        private const uint Field_nEndFlag = 0x8000;
        private const uint Field_nLastModifyTime = 0x1000;
        private const uint Field_nMsgLocalID = 2;
        private const uint Field_nMsgSvrID = 4;
        private const uint Field_nMsgTransID = 1;
        private const uint Field_nRecordLength = 0x4000;
        private const uint Field_nStatus = 0x10;
        private const uint Field_nTotalDataLen = 0x100;
        private const uint Field_nTransDataLen = 0x80;
        private const uint Field_nTransType = 8;
        private const uint Field_strClientMsgId = 0x10000;
        private const uint Field_strFromUserName = 0x20;
        private const uint Field_strImagePath = 0x400;
        private const uint Field_strThumbnail = 0x200;
        private const uint Field_strToUserName = 0x40;

        public override void merge(object o)
        {
            MsgTrans trans = o as MsgTrans;
            if (0L != (trans.modify & 4L))
            {
                this._nMsgSvrID = trans._nMsgSvrID;
            }
            if (0L != (trans.modify & 2L))
            {
                this._nMsgLocalID = trans._nMsgLocalID;
            }
            if (0L != (trans.modify & 8L))
            {
                this._nTransType = trans._nTransType;
            }
            if (0L != (trans.modify & 0x10L))
            {
                this._nStatus = trans._nStatus;
            }
            if (0L != (trans.modify & 0x20L))
            {
                this._strFromUserName = trans._strFromUserName;
            }
            if (0L != (trans.modify & 0x40L))
            {
                this._strToUserName = trans._strToUserName;
            }
            if (0L != (trans.modify & 0x80L))
            {
                this._nTransDataLen = trans._nTransDataLen;
            }
            if (0L != (trans.modify & 0x100L))
            {
                this._nTotalDataLen = trans._nTotalDataLen;
            }
            if (0L != (trans.modify & 0x200L))
            {
                this._strThumbnail = trans._strThumbnail;
            }
            if (0L != (trans.modify & 0x400L))
            {
                this._strImagePath = trans._strImagePath;
            }
            if (0L != (trans.modify & 0x800L))
            {
                this._nCreateTime = trans._nCreateTime;
            }
            if (0L != (trans.modify & 0x1000L))
            {
                this._nLastModifyTime = trans._nLastModifyTime;
            }
            if (0L != (trans.modify & 0x2000L))
            {
                this._nDuration = trans._nDuration;
            }
            if (0L != (trans.modify & 0x4000L))
            {
                this._nRecordLength = trans._nRecordLength;
            }
            if (0L != (trans.modify & 0x8000L))
            {
                this._nEndFlag = trans._nEndFlag;
            }
            if (0L != (trans.modify & 0x10000L))
            {
                this._strClientMsgId = trans._strClientMsgId;
            }
            if (0L != (trans.modify & 0x20000L))
            {
                this._bytesXmlData = trans._bytesXmlData;
            }
        }

        
        public byte[] bytesXmlData
        {
            get
            {
                return this._bytesXmlData;
            }
            set
            {
                this._bytesXmlData = value;
                base.modify |= 0x20000L;
            }
        }

        
        public long nCreateTime
        {
            get
            {
                return this._nCreateTime;
            }
            set
            {
                this._nCreateTime = value;
                base.modify |= 0x800L;
            }
        }

        
        public int nDuration
        {
            get
            {
                return this._nDuration;
            }
            set
            {
                this._nDuration = value;
                base.modify |= 0x2000L;
            }
        }

        
        public int nEndFlag
        {
            get
            {
                return this._nEndFlag;
            }
            set
            {
                this._nEndFlag = value;
                base.modify |= 0x8000L;
            }
        }

        
        public uint nLastModifyTime
        {
            get
            {
                return this._nLastModifyTime;
            }
            set
            {
                this._nLastModifyTime = value;
                base.modify |= 0x1000L;
            }
        }

        
        public int nMsgLocalID
        {
            get
            {
                return this._nMsgLocalID;
            }
            set
            {
                this._nMsgLocalID = value;
                base.modify |= 2L;
            }
        }

        
        public int nMsgSvrID
        {
            get
            {
                return this._nMsgSvrID;
            }
            set
            {
                this._nMsgSvrID = value;
                base.modify |= 4L;
            }
        }

        public int nMsgTransID
        {
            get
            {
                return this._nMsgTransID;
            }
            set
            {
                this._nMsgTransID = value;
                base.modify |= 1L;
            }
        }

        
        public int nRecordLength
        {
            get
            {
                return this._nRecordLength;
            }
            set
            {
                this._nRecordLength = value;
                base.modify |= 0x4000L;
            }
        }

        
        public int nStatus
        {
            get
            {
                return this._nStatus;
            }
            set
            {
                this._nStatus = value;
                base.modify |= 0x10L;
            }
        }

        
        public int nTotalDataLen
        {
            get
            {
                return this._nTotalDataLen;
            }
            set
            {
                this._nTotalDataLen = value;
                base.modify |= 0x100L;
            }
        }

        
        public int nTransDataLen
        {
            get
            {
                return this._nTransDataLen;
            }
            set
            {
                this._nTransDataLen = value;
                base.modify |= 0x80L;
            }
        }

        
        public int nTransType
        {
            get
            {
                return this._nTransType;
            }
            set
            {
                this._nTransType = value;
                base.modify |= 8L;
            }
        }

        
        public string strClientMsgId
        {
            get
            {
                return this._strClientMsgId;
            }
            set
            {
                this._strClientMsgId = value;
                base.modify |= 0x10000L;
            }
        }

        
        public string strFromUserName
        {
            get
            {
                return this._strFromUserName;
            }
            set
            {
                this._strFromUserName = value;
                base.modify |= 0x20L;
            }
        }

        
        public string strImagePath
        {
            get
            {
                return this._strImagePath;
            }
            set
            {
                this._strImagePath = value;
                base.modify |= 0x400L;
            }
        }

        
        public string strThumbnail
        {
            get
            {
                return this._strThumbnail;
            }
            set
            {
                this._strThumbnail = value;
                base.modify |= 0x200L;
            }
        }

        
        public string strToUserName
        {
            get
            {
                return this._strToUserName;
            }
            set
            {
                this._strToUserName = value;
                base.modify |= 0x40L;
            }
        }
    }
}

