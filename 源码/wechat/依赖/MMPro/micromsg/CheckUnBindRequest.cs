namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="CheckUnBindRequest")]
    public class CheckUnBindRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private int _BindType;
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

        [ProtoMember(2, IsRequired=true, Name="BindType", DataFormat=DataFormat.TwosComplement)]
        public int BindType
        {
            get
            {
                return this._BindType;
            }
            set
            {
                this._BindType = value;
            }
        }
    }
}

