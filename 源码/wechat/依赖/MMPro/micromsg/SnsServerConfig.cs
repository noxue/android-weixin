namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsServerConfig")]
    public class SnsServerConfig : IExtensible
    {
        private int _CopyAndPasteWordLimit = 0;
        private int _PostMentionLimit = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="CopyAndPasteWordLimit", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int CopyAndPasteWordLimit
        {
            get
            {
                return this._CopyAndPasteWordLimit;
            }
            set
            {
                this._CopyAndPasteWordLimit = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="PostMentionLimit", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int PostMentionLimit
        {
            get
            {
                return this._PostMentionLimit;
            }
            set
            {
                this._PostMentionLimit = value;
            }
        }
    }
}

