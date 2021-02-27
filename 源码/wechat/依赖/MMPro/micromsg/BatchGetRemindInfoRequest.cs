namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchGetRemindInfoRequest")]
    public class BatchGetRemindInfoRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _RemindIDCount;
        private readonly List<uint> _RemindIDList = new List<uint>();
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

        [ProtoMember(2, IsRequired=true, Name="RemindIDCount", DataFormat=DataFormat.TwosComplement)]
        public uint RemindIDCount
        {
            get
            {
                return this._RemindIDCount;
            }
            set
            {
                this._RemindIDCount = value;
            }
        }

        [ProtoMember(3, Name="RemindIDList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> RemindIDList
        {
            get
            {
                return this._RemindIDList;
            }
        }
    }
}

