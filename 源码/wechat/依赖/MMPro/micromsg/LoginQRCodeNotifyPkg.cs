namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="LoginQRCodeNotifyPkg")]
    public class LoginQRCodeNotifyPkg : IExtensible
    {
        private SKBuiltinBuffer_t _NotifyData;
        private uint _OPCode;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="NotifyData", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t NotifyData
        {
            get
            {
                return this._NotifyData;
            }
            set
            {
                this._NotifyData = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OPCode", DataFormat=DataFormat.TwosComplement)]
        public uint OPCode
        {
            get
            {
                return this._OPCode;
            }
            set
            {
                this._OPCode = value;
            }
        }
    }
}

