namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="EmotionDonor")]
    public class EmotionDonor : IExtensible
    {
        private string _HeadUrl;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="HeadUrl", DataFormat=DataFormat.Default)]
        public string HeadUrl
        {
            get
            {
                return this._HeadUrl;
            }
            set
            {
                this._HeadUrl = value;
            }
        }
    }
}

