namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="TalkStatReportData")]
    public class TalkStatReportData : IExtensible
    {
        private int _LogID;
        private SKBuiltinString_t _StatReport;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="LogID", DataFormat=DataFormat.TwosComplement)]
        public int LogID
        {
            get
            {
                return this._LogID;
            }
            set
            {
                this._LogID = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="StatReport", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t StatReport
        {
            get
            {
                return this._StatReport;
            }
            set
            {
                this._StatReport = value;
            }
        }
    }
}

