namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="VoipAddrSet")]
    public class VoipAddrSet : IExtensible
    {
        private readonly List<VoipAddr> _Addrs = new List<VoipAddr>();
        private int _Cnt;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, Name="Addrs", DataFormat=DataFormat.Default)]
        public List<VoipAddr> Addrs
        {
            get
            {
                return this._Addrs;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Cnt", DataFormat=DataFormat.TwosComplement)]
        public int Cnt
        {
            get
            {
                return this._Cnt;
            }
            set
            {
                this._Cnt = value;
            }
        }
    }
}

