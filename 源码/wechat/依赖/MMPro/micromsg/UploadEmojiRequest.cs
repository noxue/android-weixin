namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadEmojiRequest")]
    public class UploadEmojiRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<EmojiUploadInfoReq> _EmojiItem = new List<EmojiUploadInfoReq>();
        private int _EmojiItemCount;
        private uint _ReqTime = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(3, Name="EmojiItem", DataFormat=DataFormat.Default)]
        public List<EmojiUploadInfoReq> EmojiItem
        {
            get
            {
                return this._EmojiItem;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="EmojiItemCount", DataFormat=DataFormat.TwosComplement)]
        public int EmojiItemCount
        {
            get
            {
                return this._EmojiItemCount;
            }
            set
            {
                this._EmojiItemCount = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ReqTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ReqTime
        {
            get
            {
                return this._ReqTime;
            }
            set
            {
                this._ReqTime = value;
            }
        }
    }
}

