namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ECDHKey")]
    public class ECDHKey : IExtensible
    {
        private SKBuiltinBuffer_t _Key;
        private int _Nid;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Key", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Nid", DataFormat=DataFormat.TwosComplement)]
        public int Nid
        {
            get
            {
                return this._Nid;
            }
            set
            {
                this._Nid = value;
            }
        }
    }
}

