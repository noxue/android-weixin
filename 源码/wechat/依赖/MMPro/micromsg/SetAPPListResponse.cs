namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SetAPPListResponse")]
    public class SetAPPListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Hash;
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

        [ProtoMember(2, IsRequired=true, Name="Hash", DataFormat=DataFormat.TwosComplement)]
        public uint Hash
        {
            get
            {
                return this._Hash;
            }
            set
            {
                this._Hash = value;
            }
        }
    }
}

