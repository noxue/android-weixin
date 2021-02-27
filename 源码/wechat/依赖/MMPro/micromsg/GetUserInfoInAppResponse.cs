namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetUserInfoInAppResponse")]
    public class GetUserInfoInAppResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _UserCount;
        private readonly List<UserInfoInApp> _UserInfoList = new List<UserInfoInApp>();
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

        [ProtoMember(2, IsRequired=true, Name="UserCount", DataFormat=DataFormat.TwosComplement)]
        public uint UserCount
        {
            get
            {
                return this._UserCount;
            }
            set
            {
                this._UserCount = value;
            }
        }

        [ProtoMember(3, Name="UserInfoList", DataFormat=DataFormat.Default)]
        public List<UserInfoInApp> UserInfoList
        {
            get
            {
                return this._UserInfoList;
            }
        }
    }
}

