namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GrantBigChatRoomResp")]
    public class GrantBigChatRoomResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Quota;
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

        [ProtoMember(2, IsRequired=true, Name="Quota", DataFormat=DataFormat.TwosComplement)]
        public uint Quota
        {
            get
            {
                return this._Quota;
            }
            set
            {
                this._Quota = value;
            }
        }
    }
}

