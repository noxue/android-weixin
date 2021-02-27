namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetLbsLifeDetailResponse")]
    public class GetLbsLifeDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private LbsLifeDetail _LifeDetail;
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

        [ProtoMember(2, IsRequired=true, Name="LifeDetail", DataFormat=DataFormat.Default)]
        public LbsLifeDetail LifeDetail
        {
            get
            {
                return this._LifeDetail;
            }
            set
            {
                this._LifeDetail = value;
            }
        }
    }
}

