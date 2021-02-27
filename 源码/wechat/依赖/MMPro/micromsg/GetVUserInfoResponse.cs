namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetVUserInfoResponse")]
    public class GetVUserInfoResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Version;
        private uint _VUserCount;
        private readonly List<VUserResponseItem> _VUserList = new List<VUserResponseItem>();
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

        [ProtoMember(2, IsRequired=true, Name="Version", DataFormat=DataFormat.TwosComplement)]
        public uint Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="VUserCount", DataFormat=DataFormat.TwosComplement)]
        public uint VUserCount
        {
            get
            {
                return this._VUserCount;
            }
            set
            {
                this._VUserCount = value;
            }
        }

        [ProtoMember(4, Name="VUserList", DataFormat=DataFormat.Default)]
        public List<VUserResponseItem> VUserList
        {
            get
            {
                return this._VUserList;
            }
        }
    }
}

