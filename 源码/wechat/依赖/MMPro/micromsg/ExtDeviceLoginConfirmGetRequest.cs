namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ExtDeviceLoginConfirmGetRequest")]
    public class ExtDeviceLoginConfirmGetRequest : IExtensible
    {
        private string _LoginUrl;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="LoginUrl", DataFormat=DataFormat.Default)]
        public string LoginUrl
        {
            get
            {
                return this._LoginUrl;
            }
            set
            {
                this._LoginUrl = value;
            }
        }
    }
}

