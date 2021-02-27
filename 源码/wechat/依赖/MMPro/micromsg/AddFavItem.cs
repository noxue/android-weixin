namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="AddFavItem")]
    public class AddFavItem : IExtensible
    {
        private int _FavId;
        private uint _Flag;
        private int _Type;
        private uint _UpdateSeq;
        private uint _UpdateTime;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="FavId", DataFormat=DataFormat.TwosComplement)]
        public int FavId
        {
            get
            {
                return this._FavId;
            }
            set
            {
                this._FavId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Flag", DataFormat=DataFormat.TwosComplement)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=true, Name="UpdateSeq", DataFormat=DataFormat.TwosComplement)]
        public uint UpdateSeq
        {
            get
            {
                return this._UpdateSeq;
            }
            set
            {
                this._UpdateSeq = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="UpdateTime", DataFormat=DataFormat.TwosComplement)]
        public uint UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this._UpdateTime = value;
            }
        }
    }
}

