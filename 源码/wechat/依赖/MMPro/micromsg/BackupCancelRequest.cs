namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BackupCancelRequest")]
    public class BackupCancelRequest : IExtensible
    {
        private string _ID;
        private uint _Reason = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ID", DataFormat=DataFormat.Default)]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Reason", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Reason
        {
            get
            {
                return this._Reason;
            }
            set
            {
                this._Reason = value;
            }
        }
    }
}

