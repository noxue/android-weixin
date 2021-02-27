namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="DownloadEmojiRequest")]
    public class DownloadEmojiRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<EmojiDownloadInfoReq> _EmojiItem = new List<EmojiDownloadInfoReq>();
        private int _EmojiItemCount;
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
        public List<EmojiDownloadInfoReq> EmojiItem
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
    }
}

