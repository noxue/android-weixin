namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipNetQuality")]
    public class VoipNetQuality : IExtensible
    {
        private int _Begin;
        private int _End;
        private int _HitCnt;
        private int _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Begin", DataFormat=DataFormat.TwosComplement)]
        public int Begin
        {
            get
            {
                return this._Begin;
            }
            set
            {
                this._Begin = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="End", DataFormat=DataFormat.TwosComplement)]
        public int End
        {
            get
            {
                return this._End;
            }
            set
            {
                this._End = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="HitCnt", DataFormat=DataFormat.TwosComplement)]
        public int HitCnt
        {
            get
            {
                return this._HitCnt;
            }
            set
            {
                this._HitCnt = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

