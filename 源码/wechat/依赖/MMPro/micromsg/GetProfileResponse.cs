namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetProfileResponse")]
    public class GetProfileResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private ModUserInfo _UserInfo;
        private micromsg.UserInfoExt _UserInfoExt;
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

        [ProtoMember(2, IsRequired=true, Name="UserInfo", DataFormat=DataFormat.Default)]
        public ModUserInfo UserInfo
        {
            get
            {
                return this._UserInfo;
            }
            set
            {
                this._UserInfo = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="UserInfoExt", DataFormat=DataFormat.Default)]
        public micromsg.UserInfoExt UserInfoExt
        {
            get
            {
                return this._UserInfoExt;
            }
            set
            {
                this._UserInfoExt = value;
            }
        }
    }
}

