namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="MediaNoteOpLog")]
    public class MediaNoteOpLog : IExtensible
    {
        private int _NoteType;
        private uint _WriteCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="NoteType", DataFormat=DataFormat.TwosComplement)]
        public int NoteType
        {
            get
            {
                return this._NoteType;
            }
            set
            {
                this._NoteType = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="WriteCount", DataFormat=DataFormat.TwosComplement)]
        public uint WriteCount
        {
            get
            {
                return this._WriteCount;
            }
            set
            {
                this._WriteCount = value;
            }
        }
    }
}

