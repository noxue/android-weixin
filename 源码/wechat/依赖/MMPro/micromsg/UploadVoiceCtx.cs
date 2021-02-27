namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="UploadVoiceCtx")]
    public class UploadVoiceCtx : IExtensible
    {
        private uint _DataLen;
        private uint _StartPos;
        private uint _TotalLen;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="DataLen", DataFormat=DataFormat.TwosComplement)]
        public uint DataLen
        {
            get
            {
                return this._DataLen;
            }
            set
            {
                this._DataLen = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public uint StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }
    }
}

