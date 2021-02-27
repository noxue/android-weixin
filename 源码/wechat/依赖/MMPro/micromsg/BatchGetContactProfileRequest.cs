namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchGetContactProfileRequest")]
    public class BatchGetContactProfileRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Count;
        private uint _Mode;
        private readonly List<SKBuiltinString_t> _UserNameList = new List<SKBuiltinString_t>();
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

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(2, IsRequired=true, Name="Mode", DataFormat=DataFormat.TwosComplement)]
        public uint Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                this._Mode = value;
            }
        }

        [ProtoMember(4, Name="UserNameList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> UserNameList
        {
            get
            {
                return this._UserNameList;
            }
        }
    }
}

