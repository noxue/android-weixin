namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="BatchEmojiDownLoadRequest")]
    public class BatchEmojiDownLoadRequest : IExtensible
    {
        private uint _Index;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Index", DataFormat=DataFormat.TwosComplement)]
        public uint Index
        {
            get
            {
                return this._Index;
            }
            set
            {
                this._Index = value;
            }
        }
    }
}

