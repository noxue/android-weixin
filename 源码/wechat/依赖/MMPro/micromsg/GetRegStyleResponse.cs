namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetRegStyleResponse")]
    public class GetRegStyleResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private ShowStyleKey _RegStyle;
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

        [ProtoMember(2, IsRequired=true, Name="RegStyle", DataFormat=DataFormat.Default)]
        public ShowStyleKey RegStyle
        {
            get
            {
                return this._RegStyle;
            }
            set
            {
                this._RegStyle = value;
            }
        }
    }
}

