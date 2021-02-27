namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExtDeviceControlRequest")]
    public class ExtDeviceControlRequest : IExtensible
    {
        private uint _LockDevice = 0;
        private uint _OpType = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="LockDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint LockDevice
        {
            get
            {
                return this._LockDevice;
            }
            set
            {
                this._LockDevice = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="OpType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint OpType
        {
            get
            {
                return this._OpType;
            }
            set
            {
                this._OpType = value;
            }
        }
    }
}

