namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="EnemyGameInfo")]
    public class EnemyGameInfo : IExtensible
    {
        private uint _Score;
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Score", DataFormat=DataFormat.TwosComplement)]
        public uint Score
        {
            get
            {
                return this._Score;
            }
            set
            {
                this._Score = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
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

