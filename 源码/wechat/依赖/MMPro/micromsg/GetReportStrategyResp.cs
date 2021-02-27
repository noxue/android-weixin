namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetReportStrategyResp")]
    public class GetReportStrategyResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Cnt;
        private readonly List<StrategyItem> _List = new List<StrategyItem>();
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

        [ProtoMember(2, IsRequired=true, Name="Cnt", DataFormat=DataFormat.TwosComplement)]
        public uint Cnt
        {
            get
            {
                return this._Cnt;
            }
            set
            {
                this._Cnt = value;
            }
        }

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<StrategyItem> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

