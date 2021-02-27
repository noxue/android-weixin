namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ImportFriendsRequest")]
    public class ImportFriendsRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _OpCode;
        private uint _Source;
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

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Source", DataFormat=DataFormat.TwosComplement)]
        public uint Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = value;
            }
        }
    }
}

