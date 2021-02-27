namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="RcptInfoUpdateRequest")]
    public class RcptInfoUpdateRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private RcptInfoNode _rcptinfo;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="rcptinfo", DataFormat=DataFormat.Default)]
        public RcptInfoNode rcptinfo
        {
            get
            {
                return this._rcptinfo;
            }
            set
            {
                this._rcptinfo = value;
            }
        }
    }
}

