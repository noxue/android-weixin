namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SwitchOpVoicePrintResponse")]
    public class SwitchOpVoicePrintResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _UserStatus;
        private uint _UserSwitch;
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

        [ProtoMember(3, IsRequired=true, Name="UserStatus", DataFormat=DataFormat.TwosComplement)]
        public uint UserStatus
        {
            get
            {
                return this._UserStatus;
            }
            set
            {
                this._UserStatus = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="UserSwitch", DataFormat=DataFormat.TwosComplement)]
        public uint UserSwitch
        {
            get
            {
                return this._UserSwitch;
            }
            set
            {
                this._UserSwitch = value;
            }
        }
    }
}

