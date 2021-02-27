namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetAppInfoListResponse")]
    public class GetAppInfoListResponse : IExtensible
    {
        private readonly List<BizAppInfo> _AppInfoList = new List<BizAppInfo>();
        private micromsg.BaseResponse _BaseResponse;
        private int _Count;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, Name="AppInfoList", DataFormat=DataFormat.Default)]
        public List<BizAppInfo> AppInfoList
        {
            get
            {
                return this._AppInfoList;
            }
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

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public int Count
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

