namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="HardDeviceAuthResponse")]
    public class HardDeviceAuthResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _BlockTimeout;
        private uint _CacheTimeout;
        private uint _CryptMethod;
        private SKBuiltinBuffer_t _KeyBuffer;
        private SKBuiltinBuffer_t _SessionBuffer;
        private SKBuiltinBuffer_t _SessionKey;
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

        [ProtoMember(6, IsRequired=true, Name="BlockTimeout", DataFormat=DataFormat.TwosComplement)]
        public uint BlockTimeout
        {
            get
            {
                return this._BlockTimeout;
            }
            set
            {
                this._BlockTimeout = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="CacheTimeout", DataFormat=DataFormat.TwosComplement)]
        public uint CacheTimeout
        {
            get
            {
                return this._CacheTimeout;
            }
            set
            {
                this._CacheTimeout = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="CryptMethod", DataFormat=DataFormat.TwosComplement)]
        public uint CryptMethod
        {
            get
            {
                return this._CryptMethod;
            }
            set
            {
                this._CryptMethod = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="KeyBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuffer
        {
            get
            {
                return this._KeyBuffer;
            }
            set
            {
                this._KeyBuffer = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="SessionBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SessionBuffer
        {
            get
            {
                return this._SessionBuffer;
            }
            set
            {
                this._SessionBuffer = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="SessionKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SessionKey
        {
            get
            {
                return this._SessionKey;
            }
            set
            {
                this._SessionKey = value;
            }
        }
    }
}

