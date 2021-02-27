namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="BaseResponse")]
    public class BaseResponse : IExtensible
    {
        private SKBuiltinString_t _ErrMsg;
        private int _Ret;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="ErrMsg", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ErrMsg
        {
            get
            {
                return this._ErrMsg;
            }
            set
            {
                this._ErrMsg = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }
    }
}

