namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetVoicePrintResourceResponse")]
    public class GetVoicePrintResourceResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private ResourceCtx _ResourceData;
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

        [ProtoMember(2, IsRequired=true, Name="ResourceData", DataFormat=DataFormat.Default)]
        public ResourceCtx ResourceData
        {
            get
            {
                return this._ResourceData;
            }
            set
            {
                this._ResourceData = value;
            }
        }
    }
}

