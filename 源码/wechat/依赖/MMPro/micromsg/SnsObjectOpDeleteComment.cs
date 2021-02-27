namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsObjectOpDeleteComment")]
    public class SnsObjectOpDeleteComment : IExtensible
    {
        private int _CommentId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="CommentId", DataFormat=DataFormat.TwosComplement)]
        public int CommentId
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

