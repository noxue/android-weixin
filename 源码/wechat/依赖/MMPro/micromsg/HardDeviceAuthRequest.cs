namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="HardDeviceAuthRequest")]
    public class HardDeviceAuthRequest : IExtensible
    {
        private SKBuiltinBuffer_t _AuthBuffer;
        private uint _AuthVer;
        private micromsg.BaseRequest _BaseRequest;
        private micromsg.HardDevice _HardDevice;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="AuthBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t AuthBuffer
        {
            get
            {
                return this._AuthBuffer;
            }
            set
            {
                this._AuthBuffer = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="AuthVer", DataFormat=DataFormat.TwosComplement)]
        public uint AuthVer
        {
            get
            {
                return this._AuthVer;
            }
            set
            {
                this._AuthVer = value;
            }
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

        [ProtoMember(2, IsRequired=true, Name="HardDevice", DataFormat=DataFormat.Default)]
        public micromsg.HardDevice HardDevice
        {
            get
            {
                return this._HardDevice;
            }
            set
            {
                this._HardDevice = value;
            }
        }
    }
}

