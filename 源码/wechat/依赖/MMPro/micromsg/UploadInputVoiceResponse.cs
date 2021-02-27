namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="UploadInputVoiceResponse")]
    public class UploadInputVoiceResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _EndFlag;
        private SKBuiltinBuffer_t _Text;
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

        [ProtoMember(2, IsRequired=true, Name="EndFlag", DataFormat=DataFormat.TwosComplement)]
        public uint EndFlag
        {
            get
            {
                return this._EndFlag;
            }
            set
            {
                this._EndFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Text", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }
        }
    }
}

