namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="OplogRequest")]
    public class OplogRequest : IExtensible
    {
        private CmdList _Oplog;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Oplog", DataFormat=DataFormat.Default)]
        public CmdList Oplog
        {
            get
            {
                return this._Oplog;
            }
            set
            {
                this._Oplog = value;
            }
        }
    }
}

