namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SendMsgRequestNew")]
    public class SendMsgRequestNew : IExtensible
    {
        private uint _Count;
        private readonly List<MicroMsgRequestNew> _List = new List<MicroMsgRequestNew>();
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
        public List<MicroMsgRequestNew> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

