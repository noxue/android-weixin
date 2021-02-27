namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchGetRemindInfoResponse")]
    public class BatchGetRemindInfoResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _RemindInfoCount;
        private readonly List<RemindItem> _RemindInfoList = new List<RemindItem>();
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

        [ProtoMember(2, IsRequired=true, Name="RemindInfoCount", DataFormat=DataFormat.TwosComplement)]
        public uint RemindInfoCount
        {
            get
            {
                return this._RemindInfoCount;
            }
            set
            {
                this._RemindInfoCount = value;
            }
        }

        [ProtoMember(3, Name="RemindInfoList", DataFormat=DataFormat.Default)]
        public List<RemindItem> RemindInfoList
        {
            get
            {
                return this._RemindInfoList;
            }
        }
    }
}

