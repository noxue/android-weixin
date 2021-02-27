namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetAppSettingResponse")]
    public class GetAppSettingResponse : IExtensible
    {
        private uint _AppCount;
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<AppSetting> _SettingList = new List<AppSetting>();
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

        [ProtoMember(3, Name="SettingList", DataFormat=DataFormat.Default)]
        public List<AppSetting> SettingList
        {
            get
            {
                return this._SettingList;
            }
        }
    }
}

