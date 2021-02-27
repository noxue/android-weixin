namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsTimeLineWithTypeRequest")]
    public class SnsTimeLineWithTypeRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private ulong _SelectType = 0L;
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

        [ProtoMember(2, IsRequired=false, Name="SelectType", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong SelectType
        {
            get
            {
                return this._SelectType;
            }
            set
            {
                this._SelectType = value;
            }
        }
    }
}

