namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsADObjectOpDeleteComment")]
    public class SnsADObjectOpDeleteComment : IExtensible
    {
        private ulong _CommentId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="CommentId", DataFormat=DataFormat.TwosComplement)]
        public ulong CommentId
        {
            get
            {
                return this._CommentId;
            }
            set
            {
                this._CommentId = value;
            }
        }
    }
}

