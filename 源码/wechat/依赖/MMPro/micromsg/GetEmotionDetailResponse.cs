namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetEmotionDetailResponse")]
    public class GetEmotionDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.EmotionDetail _EmotionDetail;
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

        [ProtoMember(2, IsRequired=true, Name="EmotionDetail", DataFormat=DataFormat.Default)]
        public micromsg.EmotionDetail EmotionDetail
        {
            get
            {
                return this._EmotionDetail;
            }
            set
            {
                this._EmotionDetail = value;
            }
        }
    }
}

