namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GmailList")]
    public class GmailList : IExtensible
    {
        private uint _Count;
        private readonly List<GmailInfo> _List = new List<GmailInfo>();
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
        public List<GmailInfo> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

