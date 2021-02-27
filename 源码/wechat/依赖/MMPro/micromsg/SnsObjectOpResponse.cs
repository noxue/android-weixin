namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SnsObjectOpResponse")]
    public class SnsObjectOpResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _OpCount;
        private readonly List<int> _OpRetList = new List<int>();
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

        [ProtoMember(2, IsRequired=true, Name="OpCount", DataFormat=DataFormat.TwosComplement)]
        public uint OpCount
        {
            get
            {
                return this._OpCount;
            }
            set
            {
                this._OpCount = value;
            }
        }

        [ProtoMember(3, Name="OpRetList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<int> OpRetList
        {
            get
            {
                return this._OpRetList;
            }
        }
    }
}

