namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="CheckVoiceTransResponse")]
    public class CheckVoiceTransResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _NotifyId;
        private QueryResCtx _QueryCtx;
        private int _Status;
        private VoiceTransRes _TransRes;
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

        [ProtoMember(6, IsRequired=true, Name="NotifyId", DataFormat=DataFormat.TwosComplement)]
        public uint NotifyId
        {
            get
            {
                return this._NotifyId;
            }
            set
            {
                this._NotifyId = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="QueryCtx", DataFormat=DataFormat.Default)]
        public QueryResCtx QueryCtx
        {
            get
            {
                return this._QueryCtx;
            }
            set
            {
                this._QueryCtx = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TransRes", DataFormat=DataFormat.Default)]
        public VoiceTransRes TransRes
        {
            get
            {
                return this._TransRes;
            }
            set
            {
                this._TransRes = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="UploadCtx", DataFormat=DataFormat.Default)]
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

