namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="LBSLifeActionList")]
    public class LBSLifeActionList : IExtensible
    {
        private LBSLifeAction _LifeAction;
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="LifeAction", DataFormat=DataFormat.Default)]
        public LBSLifeAction LifeAction
        {
            get
            {
                return this._LifeAction;
            }
            set
            {
                this._LifeAction = value;
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

