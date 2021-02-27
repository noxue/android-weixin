namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="UpdateContactLabelRequest")]
    public class UpdateContactLabelRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private micromsg.LabelPair _LabelPair;
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

        [ProtoMember(2, IsRequired=true, Name="LabelPair", DataFormat=DataFormat.Default)]
        public micromsg.LabelPair LabelPair
        {
            get
            {
                return this._LabelPair;
            }
            set
            {
                this._LabelPair = value;
            }
        }
    }
}

