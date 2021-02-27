namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="StatReportRequest")]
    public class StatReportRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private StatReportExtInfo _ExtInfo;
        private StatReportInfo _Info;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ExtInfo", DataFormat=DataFormat.Default)]
        public StatReportExtInfo ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Info", DataFormat=DataFormat.Default)]
        public StatReportInfo Info
        {
            get
            {
                return this._Info;
            }
            set
            {
                this._Info = value;
            }
        }
    }
}

