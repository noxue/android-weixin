namespace MicroMsg.Storage
{
   // using MicroMsg.UI.UserContrl;
   // using Microsoft.Phone.Data.Linq.Mapping;
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Text;

   public class ChatMsg : StorageItem
    {
        private byte[] _bytesContent;
        private byte[] _bytesXmlData;
        private long _nCreateTime;
        private int _nIsSender;
        private int _nMsgLocalID;
        private int _nMsgSvrID;
        private int _nMsgType;
        private int _nStatus;
        private string _strClientMsgId;
        private string _strContent;
        private string _strTalker;

        
        private MsgXmlData _xmlData;
        public const uint Field_bytesContent = 0x800;
        public const uint Field_bytesXmlData = 0x200;
        public const uint Field_nCreateTime = 0x20;
        public const uint Field_nIsSender = 0x10;
        public const uint Field_nMsgLocalID = 1;
        public const uint Field_nMsgSvrID = 2;
        public const uint Field_nMsgType = 4;
        public const uint Field_nStatus = 8;
        public const uint Field_strClientMsgId = 0x100;
        public const uint Field_strContent = 0x80;
        public const uint Field_strTalker = 0x40;

        public void FillData(object item)
        {
            ChatMsg msg = item as ChatMsg;
            if (msg != null)
            {
                msg.nMsgLocalID = this.nMsgLocalID;
                msg.nMsgSvrID = this.nMsgSvrID;
                msg.nMsgType = this.nMsgType;
                msg.nStatus = this.nStatus;
                msg.nIsSender = this.nIsSender;
                msg.nCreateTime = this.nCreateTime;
                msg.strTalker = this.strTalker;
                msg.strContent = this.strContent;
                msg.strClientMsgId = this.strClientMsgId;
                msg.bytesXmlData = this.bytesXmlData;
                msg.bytesContent = this.bytesContent;
            }
        }

        public string msgSource
        {
            get
            {
                this.load();
                return this._xmlData.msgSource;
            }
            set
            {
                this.load();
                this._xmlData.msgSource = value;
                this.unload();
            }
        }
        private void load()
        {
            if (this._xmlData == null)
            {
                this._xmlData = StorageXml.loadFromBuffer<MsgXmlData>(this.bytesXmlData);
                if (this._xmlData == null)
                {
                    this._xmlData = new MsgXmlData();
                }
            }
        }

        public override void merge(object o)
        {
            ChatMsg msg = o as ChatMsg;
            if (0L != (msg.modify & 2L))
            {
                this._nMsgSvrID = msg._nMsgSvrID;
            }
            if (0L != (msg.modify & 4L))
            {
                this._nMsgType = msg._nMsgType;
            }
            if (0L != (msg.modify & 8L))
            {
                this._nStatus = msg._nStatus;
            }
            if (0L != (msg.modify & 0x10L))
            {
                this._nIsSender = msg._nIsSender;
            }
            if (0L != (msg.modify & 0x20L))
            {
                this._nCreateTime = msg._nCreateTime;
            }
            if (0L != (msg.modify & 0x40L))
            {
                this._strTalker = msg._strTalker;
            }
            if (0L != (msg.modify & 0x80L))
            {
                this._strContent = msg._strContent;
            }
            if (0L != (msg.modify & 0x100L))
            {
                this._strClientMsgId = msg._strClientMsgId;
            }
            if (0L != (msg.modify & 0x800L))
            {
                this._bytesContent = msg._bytesContent;
            }
            if (0L != (msg.modify & 0x200L))
            {
                this._bytesXmlData = msg._bytesXmlData;
                this._xmlData = null;
            }
        }

        private void unload()
        {
            if (this._xmlData != null)
            {
                this.bytesXmlData = StorageXml.saveToBuffer<MsgXmlData>(this._xmlData);
            }
        }

        
        public byte[] bytesContent
        {
            get
            {
                return this._bytesContent;
            }
            set
            {
                this._bytesContent = value;
                base.modify |= 0x800L;
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
                base.modify |= 0x200L;
            }
        }

        public bool isGifRead
        {
            get
            {
                this.load();
                return this._xmlData.isGifRead;
            }
            set
            {
                this.load();
                this._xmlData.isGifRead = value;
                this.unload();
            }
        }

        public bool isRead
        {
            get
            {
                return this.isVoiceRead;
            }
            set
            {
                this.isVoiceRead = value;
            }
        }

        public bool isVoiceRead
        {
            get
            {
                this.load();
                return this._xmlData.isVoiceRead;
            }
            set
            {
                this.load();
                this._xmlData.isVoiceRead = value;
                this.unload();
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
                base.modify |= 0x20L;
            }
        }

        
        public int nIsSender
        {
            get
            {
                return this._nIsSender;
            }
            set
            {
                this._nIsSender = value;
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
                base.modify |= 1L;
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
                base.modify |= 2L;
            }
        }

        
        public int nMsgType
        {
            get
            {
                return this._nMsgType;
            }
            set
            {
                this._nMsgType = value;
                base.modify |= 4L;
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

        
        public string strClientMsgId
        {
            get
            {
                return this._strClientMsgId;
            }
            set
            {
                this._strClientMsgId = value;
                base.modify |= 0x100L;
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
                base.modify |= 0x80L;
            }
        }

        public string strMsg
        {
            get
            {
                if (this.bytesContent != null)
                {
                    return Encoding.UTF8.GetString(this.bytesContent, 0, this.bytesContent.Length);
                }
                return this.strContent;
            }
            set
            {
                if (value == null)
                {
                    this.strContent = null;
                    this.bytesContent = null;
                }
                else if (value.Length <= 0xbb8)
                {
                    this.strContent = value;
                    this.bytesContent = null;
                }
                else
                {
                    this.strContent = null;
                    this.bytesContent = Encoding.UTF8.GetBytes(value);
                }
            }
        }

        public string strPath
        {
            get
            {
                this.load();
                return this._xmlData.strImagePath;
            }
            set
            {
                this.load();
                this._xmlData.strImagePath = value;
                this.unload();
            }
        }

        
        public string strTalker
        {
            get
            {
                return this._strTalker;
            }
            set
            {
                this._strTalker = value;
                base.modify |= 0x40L;
            }
        }

        public string strThumbnail
        {
            get
            {
                this.load();
                return this._xmlData.strThumbnail;
            }
            set
            {
                this.load();
                this._xmlData.strThumbnail = value;
                this.unload();
            }
        }
    }
}

