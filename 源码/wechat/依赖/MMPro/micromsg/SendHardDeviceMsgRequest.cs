namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SendHardDeviceMsgRequest")]
    public class SendHardDeviceMsgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private micromsg.HardDevice _HardDevice;
        private micromsg.HardDeviceMsg _HardDeviceMsg;
        private SKBuiltinBuffer_t _SessionBuffer;
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

        [ProtoMember(3, IsRequired=true, Name="HardDeviceMsg", DataFormat=DataFormat.Default)]
        public micromsg.HardDeviceMsg HardDeviceMsg
        {
            get
            {
                return this._HardDeviceMsg;
            }
            set
            {
                this._HardDeviceMsg = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="SessionBuffer", DataFormat=DataFormat.Default)]
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
    }
}

