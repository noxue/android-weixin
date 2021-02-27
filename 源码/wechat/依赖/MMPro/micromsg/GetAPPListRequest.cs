namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetAPPListRequest")]
    public class GetAPPListRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Hash;
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

