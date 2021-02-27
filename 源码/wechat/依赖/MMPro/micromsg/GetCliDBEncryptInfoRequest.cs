namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetCliDBEncryptInfoRequest")]
    public class GetCliDBEncryptInfoRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _CertVersion;
        private SKBuiltinBuffer_t _CliDBEncryptInfo;
        private SKBuiltinBuffer_t _CliDBEncryptKey;
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

        [ProtoMember(2, IsRequired=true, Name="CertVersion", DataFormat=DataFormat.TwosComplement)]
        public uint CertVersion
        {
            get
            {
                return this._CertVersion;
            }
            set
            {
                this._CertVersion = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="CliDBEncryptInfo", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t CliDBEncryptInfo
        {
            get
            {
                return this._CliDBEncryptInfo;
            }
            set
            {
                this._CliDBEncryptInfo = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="CliDBEncryptKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t CliDBEncryptKey
        {
            get
            {
                return this._CliDBEncryptKey;
            }
            set
            {
                this._CliDBEncryptKey = value;
            }
        }
    }
}

