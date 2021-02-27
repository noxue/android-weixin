namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipStatReportData")]
    public class VoipStatReportData : IExtensible
    {
        private SKBuiltinString_t _StatReport;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="StatReport", DataFormat=DataFormat.Default)]
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

