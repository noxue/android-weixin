namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetAuthAppListResponse")]
    public class GetAuthAppListResponse : IExtensible
    {
        private uint _AppCount;
        private readonly List<AuthAppBaseInfo> _AuthAppList = new List<AuthAppBaseInfo>();
        private micromsg.BaseResponse _BaseResponse;
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

        [ProtoMember(3, Name="AuthAppList", DataFormat=DataFormat.Default)]
        public List<AuthAppBaseInfo> AuthAppList
        {
            get
            {
                return this._AuthAppList;
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
    }
}

