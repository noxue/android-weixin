namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="PropertySurveyResponse")]
    public class PropertySurveyResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _NextReportTime;
        private uint _ReportFlag;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="NextReportTime", DataFormat=DataFormat.TwosComplement)]
        public uint NextReportTime
        {
            get
            {
                return this._NextReportTime;
            }
            set
            {
                this._NextReportTime = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ReportFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ReportFlag
        {
            get
            {
                return this._ReportFlag;
            }
            set
            {
                this._ReportFlag = value;
            }
        }
    }
}

