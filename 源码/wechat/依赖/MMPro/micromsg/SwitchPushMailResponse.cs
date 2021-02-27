namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SwitchPushMailResponse")]
    public class SwitchPushMailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _SwitchValue;
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

        [ProtoMember(2, IsRequired=true, Name="SwitchValue", DataFormat=DataFormat.TwosComplement)]
        public uint SwitchValue
        {
            get
            {
                return this._SwitchValue;
            }
            set
            {
                this._SwitchValue = value;
            }
        }
    }
}

