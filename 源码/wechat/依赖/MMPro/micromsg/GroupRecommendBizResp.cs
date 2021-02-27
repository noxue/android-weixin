namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GroupRecommendBizResp")]
    public class GroupRecommendBizResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private RecommendGroups _GroupList;
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

        [ProtoMember(2, IsRequired=true, Name="GroupList", DataFormat=DataFormat.Default)]
        public RecommendGroups GroupList
        {
            get
            {
                return this._GroupList;
            }
            set
            {
                this._GroupList = value;
            }
        }
    }
}

