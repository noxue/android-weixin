namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsADCommentResponse")]
    public class SnsADCommentResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.SnsADObject _SnsADObject;
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

        [ProtoMember(2, IsRequired=true, Name="SnsADObject", DataFormat=DataFormat.Default)]
        public micromsg.SnsADObject SnsADObject
        {
            get
            {
                return this._SnsADObject;
            }
            set
            {
                this._SnsADObject = value;
            }
        }
    }
}

