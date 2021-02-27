namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetQQGroupRequest")]
    public class GetQQGroupRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _GroupID;
        private uint _OpType;
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

        [ProtoMember(3, IsRequired=true, Name="GroupID", DataFormat=DataFormat.TwosComplement)]
        public uint GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                this._GroupID = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpType", DataFormat=DataFormat.TwosComplement)]
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

