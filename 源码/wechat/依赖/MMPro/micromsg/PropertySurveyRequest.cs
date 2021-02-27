namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="PropertySurveyRequest")]
    public class PropertySurveyRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private PropertySurveyInfo _Info;
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

        [ProtoMember(2, IsRequired=true, Name="Info", DataFormat=DataFormat.Default)]
        public PropertySurveyInfo Info
        {
            get
            {
                return this._Info;
            }
            set
            {
                this._Info = value;
            }
        }
    }
}

