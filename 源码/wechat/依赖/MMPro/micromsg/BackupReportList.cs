namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BackupReportList")]
    public class BackupReportList : IExtensible
    {
        private uint _Count;
        private readonly List<BackupReportItem> _List = new List<BackupReportItem>();
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
        public List<BackupReportItem> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

