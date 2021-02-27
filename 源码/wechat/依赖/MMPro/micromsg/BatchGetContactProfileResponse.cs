namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchGetContactProfileResponse")]
    public class BatchGetContactProfileResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<SKBuiltinBuffer_t> _ContactProfileBufList = new List<SKBuiltinBuffer_t>();
        private uint _Count;
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

        [ProtoMember(3, Name="ContactProfileBufList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinBuffer_t> ContactProfileBufList
        {
            get
            {
                return this._ContactProfileBufList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }
    }
}

