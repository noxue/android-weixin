namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GameConsumeProps")]
    public class GameConsumeProps : IExtensible
    {
        private int _ConsumeCount;
        private uint _PropsId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="ConsumeCount", DataFormat=DataFormat.TwosComplement)]
        public int ConsumeCount
        {
            get
            {
                return this._ConsumeCount;
            }
            set
            {
                this._ConsumeCount = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="PropsId", DataFormat=DataFormat.TwosComplement)]
        public uint PropsId
        {
            get
            {
                return this._PropsId;
            }
            set
            {
                this._PropsId = value;
            }
        }
    }
}

