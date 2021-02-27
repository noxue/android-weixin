namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetVoiceTransResResponse")]
    public class GetVoiceTransResResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private QueryResCtx _QueryCtx;
        private VoiceTransRes _TransRes;
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

        [ProtoMember(3, IsRequired=true, Name="QueryCtx", DataFormat=DataFormat.Default)]
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

        [ProtoMember(2, IsRequired=true, Name="TransRes", DataFormat=DataFormat.Default)]
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
    }
}

