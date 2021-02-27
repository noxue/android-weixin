namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindHardDeviceResponse")]
    public class BindHardDeviceResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Flag = 0;
        private micromsg.HardDevice _HardDevice;
        private micromsg.HardDeviceAttr _HardDeviceAttr;
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

        [ProtoMember(4, IsRequired=false, Name="Flag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
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

        [ProtoMember(3, IsRequired=true, Name="HardDeviceAttr", DataFormat=DataFormat.Default)]
        public micromsg.HardDeviceAttr HardDeviceAttr
        {
            get
            {
                return this._HardDeviceAttr;
            }
            set
            {
                this._HardDeviceAttr = value;
            }
        }
    }
}

