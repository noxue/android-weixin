﻿namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="UploadEmojiResponse")]
    public class UploadEmojiResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<EmojiUploadInfoResp> _EmojiItem = new List<EmojiUploadInfoResp>();
        private int _EmojiItemCount;
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

        [ProtoMember(3, Name="EmojiItem", DataFormat=DataFormat.Default)]
        public List<EmojiUploadInfoResp> EmojiItem
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

