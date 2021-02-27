namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="EmojiUploadResponse")]
    public class EmojiUploadResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<UploadEmojiInfoResp> _EmojiItem = new List<UploadEmojiInfoResp>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, Name="EmojiItem", DataFormat=DataFormat.Default)]
        public List<UploadEmojiInfoResp> EmojiItem
        {
            get
            {
                return this._EmojiItem;
            }
        }
    }
}

