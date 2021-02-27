namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsPostResponse")]
    public class SnsPostResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.SnsObject _SnsObject;
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

        [ProtoMember(2, IsRequired=true, Name="SnsObject", DataFormat=DataFormat.Default)]
        public micromsg.SnsObject SnsObject
        {
            get
            {
                return this._SnsObject;
            }
            set
            {
                this._SnsObject = value;
            }
        }
    }
}

