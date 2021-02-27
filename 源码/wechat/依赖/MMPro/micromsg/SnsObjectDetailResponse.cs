namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsObjectDetailResponse")]
    public class SnsObjectDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SnsObject _Object;
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

        [ProtoMember(2, IsRequired=true, Name="Object", DataFormat=DataFormat.Default)]
        public SnsObject Object
        {
            get
            {
                return this._Object;
            }
            set
            {
                this._Object = value;
            }
        }
    }
}

