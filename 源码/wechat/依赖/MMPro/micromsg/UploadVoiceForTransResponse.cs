namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="UploadVoiceForTransResponse")]
    public class UploadVoiceForTransResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private UploadVoiceCtx _UploadCtx;
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

        [ProtoMember(2, IsRequired=true, Name="UploadCtx", DataFormat=DataFormat.Default)]
        public UploadVoiceCtx UploadCtx
        {
            get
            {
                return this._UploadCtx;
            }
            set
            {
                this._UploadCtx = value;
            }
        }
    }
}

