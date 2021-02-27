namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoicePieceCtx")]
    public class VoicePieceCtx : IExtensible
    {
        private SKBuiltinBuffer_t _PieceData;
        private uint _PieceFlag;
        private uint _PieceLen;
        private uint _StartPos;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="PieceData", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t PieceData
        {
            get
            {
                return this._PieceData;
            }
            set
            {
                this._PieceData = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="PieceFlag", DataFormat=DataFormat.TwosComplement)]
        public uint PieceFlag
        {
            get
            {
                return this._PieceFlag;
            }
            set
            {
                this._PieceFlag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="PieceLen", DataFormat=DataFormat.TwosComplement)]
        public uint PieceLen
        {
            get
            {
                return this._PieceLen;
            }
            set
            {
                this._PieceLen = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
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
    }
}

