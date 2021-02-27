namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetCertRequest")]
    public class GetCertRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _CurrentCertVersion;
        private SKBuiltinBuffer_t _RandomEncryKey;
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

        [ProtoMember(3, IsRequired=true, Name="CurrentCertVersion", DataFormat=DataFormat.TwosComplement)]
        public uint CurrentCertVersion
        {
            get
            {
                return this._CurrentCertVersion;
            }
            set
            {
                this._CurrentCertVersion = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t RandomEncryKey
        {
            get
            {
                return this._RandomEncryKey;
            }
            set
            {
                this._RandomEncryKey = value;
            }
        }
    }
}

