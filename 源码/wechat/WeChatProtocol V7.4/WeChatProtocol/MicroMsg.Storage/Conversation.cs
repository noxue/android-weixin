namespace MicroMsg.Storage
{
    //using MicroMsg.UI.UserContrl;
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;

    
    public class Conversation : StorageItem
    {
        private byte[] _bytesXmlData;
        private int _nIsSend;
        private int _nMsgLocalID;
        private int _nMsgStatus;
        private int _nStatus;
        private long _nTime;
        private int _nUnReadCount;
        private long? _pOrder;
        private string _strContent;
        private string _strMsgType;
        private string _strNickName;
        private string _strUsrName;
 
        private const uint Field_bytesXmlData = 0x400;
        private const uint Field_nIsSend = 0x10;
        private const uint Field_nMsgLocalID = 0x100;
        private const uint Field_nMsgStatus = 0x200;
        private const uint Field_nStatus = 8;
        private const uint Field_nTime = 0x20;
        private const uint Field_nUnReadCount = 4;
        private const uint Field_pOrder = 0x800;
        private const uint Field_strContent = 0x40;
        private const uint Field_strMsgType = 0x80;
        private const uint Field_strNickName = 2;
        private const uint Field_strUsrName = 1;

        public void FillData(object item)
        {
            Conversation conversation = item as Conversation;
            if (conversation != null)
            {
                conversation.strUsrName = this.strUsrName;
                conversation.strNickName = this.strNickName;
                conversation.nUnReadCount = this.nUnReadCount;
                conversation.nStatus = this.nStatus;
                conversation.nIsSend = this.nIsSend;
                conversation.nTime = this.nTime;
                conversation.strContent = this.strContent;
                conversation.strMsgType = this.strMsgType;
                conversation.nMsgLocalID = this.nMsgLocalID;
            }
        }

        public override void merge(object o)
        {
            Conversation conversation = o as Conversation;
            if (0L != (conversation.modify & 1L))
            {
                this._strUsrName = conversation.strUsrName;
            }
            if (0L != (conversation.modify & 2L))
            {
                this._strNickName = conversation.strNickName;
            }
            if (0L != (conversation.modify & 4L))
            {
                this._nUnReadCount = conversation.nUnReadCount;
            }
            if (0L != (conversation.modify & 8L))
            {
                this._nStatus = conversation.nStatus;
            }
            if (0L != (conversation.modify & 0x10L))
            {
                this._nIsSend = conversation.nIsSend;
            }
            if (0L != (conversation.modify & 0x20L))
            {
                this._nTime = conversation.nTime;
            }
            if (0L != (conversation.modify & 0x40L))
            {
                this._strContent = conversation.strContent;
            }
            if (0L != (conversation.modify & 0x80L))
            {
                this._strMsgType = conversation.strMsgType;
            }
            if (0L != (conversation.modify & 0x100L))
            {
                this._nMsgLocalID = conversation.nMsgLocalID;
            }
            if (0L != (conversation.modify & 0x200L))
            {
                this._nMsgStatus = conversation.nMsgStatus;
            }
            if (0L != (conversation.modify & 0x400L))
            {
                this._bytesXmlData = conversation.bytesXmlData;
            }
            if (0L != (conversation.modify & 0x800L))
            {
                this._pOrder = conversation._pOrder;
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
                base.modify |= 0x400L;
            }
        }

        
        public int nIsSend
        {
            get
            {
                return this._nIsSend;
            }
            set
            {
                this._nIsSend = value;
                base.modify |= 0x10L;
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
                base.modify |= 0x100L;
            }
        }

        
        public int nMsgStatus
        {
            get
            {
                return this._nMsgStatus;
            }
            set
            {
                this._nMsgStatus = value;
                base.modify |= 0x200L;
            }
        }

        public long nOrder
        {
            get
            {
                long? pOrder = this.pOrder;
                if (!pOrder.HasValue)
                {
                    return 0L;
                }
                return pOrder.GetValueOrDefault();
            }
            set
            {
                this.pOrder = new long?(value);
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
                base.modify |= 8L;
            }
        }

        
        public long nTime
        {
            get
            {
                return this._nTime;
            }
            set
            {
                this._nTime = value;
                base.modify |= 0x20L;
            }
        }

        
        public int nUnReadCount
        {
            get
            {
                return this._nUnReadCount;
            }
            set
            {
                this._nUnReadCount = value;
                base.modify |= 4L;
            }
        }

        [Column(CanBeNull=true)]
        public long? pOrder
        {
            get
            {
                return this._pOrder;
            }
            set
            {
                this._pOrder = value;
                base.modify |= 0x800L;
            }
        }

        
        public string strContent
        {
            get
            {
                return this._strContent;
            }
            set
            {
                this._strContent = value;
                base.modify |= 0x40L;
            }
        }

        
        public string strMsgType
        {
            get
            {
                return this._strMsgType;
            }
            set
            {
                this._strMsgType = value;
                base.modify |= 0x80L;
            }
        }

        
        public string strNickName
        {
            get
            {
                return this._strNickName;
            }
            set
            {
                this._strNickName = value;
                base.modify |= 2L;
            }
        }

        [Column(IsPrimaryKey=true)]
        public string strUsrName
        {
            get
            {
                return this._strUsrName;
            }
            set
            {
                this._strUsrName = value;
                base.modify |= 1L;
            }
        }
    }
}

