namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ModHardDevice")]
    public class ModHardDevice : IExtensible
    {
        private uint _BindFlag;
        private micromsg.HardDevice _HardDevice;
        private micromsg.HardDeviceAttr _HardDeviceAttr;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="BindFlag", DataFormat=DataFormat.TwosComplement)]
        public uint BindFlag
        {
            get
            {
                return this._BindFlag;
            }
            set
            {
                this._BindFlag = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="HardDevice", DataFormat=DataFormat.Default)]
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

        [ProtoMember(2, IsRequired=true, Name="HardDeviceAttr", DataFormat=DataFormat.Default)]
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

