namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetAppPersonalInfoListResponse")]
    public class GetAppPersonalInfoListResponse : IExtensible
    {
        private readonly List<AppPersonalInfo> _AppPersonalInfoList = new List<AppPersonalInfo>();
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, Name="AppPersonalInfoList", DataFormat=DataFormat.Default)]
        public List<AppPersonalInfo> AppPersonalInfoList
        {
            get
            {
                return this._AppPersonalInfoList;
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

