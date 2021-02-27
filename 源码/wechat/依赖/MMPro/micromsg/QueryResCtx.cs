namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="QueryResCtx")]
    public class QueryResCtx : IExtensible
    {
        private uint _Interval;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Interval", DataFormat=DataFormat.TwosComplement)]
        public uint Interval
        {
            get
            {
                return this._Interval;
            }
            set
            {
                this._Interval = value;
            }
        }
    }
}

