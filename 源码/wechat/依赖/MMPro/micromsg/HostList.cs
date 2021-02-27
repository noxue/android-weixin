namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="HostList")]
    public class HostList : IExtensible
    {
        private uint _Count;
        private readonly List<Host> _List = new List<Host>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(2, Name="List", DataFormat=DataFormat.Default)]
        public List<Host> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

