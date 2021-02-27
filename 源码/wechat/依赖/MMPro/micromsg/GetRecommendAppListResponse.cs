namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetRecommendAppListResponse")]
    public class GetRecommendAppListResponse : IExtensible
    {
        private readonly List<OpenAppInfo> _AppList = new List<OpenAppInfo>();
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private uint _Total;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, Name="AppList", DataFormat=DataFormat.Default)]
        public List<OpenAppInfo> AppList
        {
            get
            {
                return this._AppList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
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

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(1, IsRequired=true, Name="Total", DataFormat=DataFormat.TwosComplement)]
        public uint Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                this._Total = value;
            }
        }
    }
}

