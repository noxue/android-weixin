namespace MicroMsg.Storage
{
   // using Microsoft.Phone.Data.Linq.Mapping;
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;

    public class SnsMsg : StorageItem
    {
        private SnsMsgDetail _refer;

        
        
        public byte[] bytesRefer;
        
        public int nCommentId;
        
        public uint nCreateTime;

        public int nMsgID;
        
        public int nReplyCommentId;
        
        public uint nSource;
        
        public uint nType;
        
        public string strContent;
        
        public string strFromNickname;
        
        public string strFromUsername;
        
        public string strObjectID;
        
        public string strParentID;
        
        public string strToNickname;
        
        public string strToUsername;

        public override void merge(object o)
        {
            SnsMsg msg = o as SnsMsg;
            this.strObjectID = msg.strObjectID;
            this.strParentID = msg.strParentID;
            this.strFromUsername = msg.strFromUsername;
            this.strToUsername = msg.strToUsername;
            this.strFromNickname = msg.strFromNickname;
            this.strToNickname = msg.strToNickname;
            this.nType = msg.nType;
            this.nSource = msg.nSource;
            this.nCreateTime = msg.nCreateTime;
            this.strContent = msg.strContent;
            this.nReplyCommentId = msg.nReplyCommentId;
            this.nCommentId = msg.nCommentId;
        }

        public SnsMsgDetail refer
        {
            get
            {
                if (this._refer == null)
                {
                    this._refer = StorageXml.loadFromBuffer<SnsMsgDetail>(this.bytesRefer);
                }
                return this._refer;
            }
            set
            {
                this._refer = value;
                this.bytesRefer = StorageXml.saveToBuffer<SnsMsgDetail>(value);
            }
        }
    }
}

