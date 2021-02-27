namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsTagOptionResponse")]
    public class SnsTagOptionResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.SnsTag _SnsTag;
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

        [ProtoMember(2, IsRequired=true, Name="SnsTag", DataFormat=DataFormat.Default)]
        public micromsg.SnsTag SnsTag
        {
            get
            {
                return this._SnsTag;
            }
            set
            {
                this._SnsTag = value;
            }
        }
    }
}

