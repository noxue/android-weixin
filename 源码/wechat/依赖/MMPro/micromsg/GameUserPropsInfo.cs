namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GameUserPropsInfo")]
    public class GameUserPropsInfo : IExtensible
    {
        private uint _Count;
        private uint _PropsId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
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

