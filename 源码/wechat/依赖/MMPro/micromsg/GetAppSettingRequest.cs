namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetAppSettingRequest")]
    public class GetAppSettingRequest : IExtensible
    {
        private uint _AppCount;
        private readonly List<AppSettingReq> _AppSettingReqList = new List<AppSettingReq>();
        private micromsg.BaseRequest _BaseRequest;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AppCount", DataFormat=DataFormat.TwosComplement)]
        public uint AppCount
        {
            get
            {
                return this._AppCount;
            }
            set
            {
                this._AppCount = value;
            }
        }

        [ProtoMember(3, Name="AppSettingReqList", DataFormat=DataFormat.Default)]
        public List<AppSettingReq> AppSettingReqList
        {
            get
            {
                return this._AppSettingReqList;
            }
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
    }
}

