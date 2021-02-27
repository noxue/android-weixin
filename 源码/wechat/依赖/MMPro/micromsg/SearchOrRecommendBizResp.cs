namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SearchOrRecommendBizResp")]
    public class SearchOrRecommendBizResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _BizCount;
        private readonly List<SearchOrRecommendItem> _BizList = new List<SearchOrRecommendItem>();
        private uint _IsEnd;
        private SKBuiltinBuffer_t _ResBuf;
        private uint _ShowFlag;
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

        [ProtoMember(2, IsRequired=true, Name="BizCount", DataFormat=DataFormat.TwosComplement)]
        public uint BizCount
        {
            get
            {
                return this._BizCount;
            }
            set
            {
                this._BizCount = value;
            }
        }

        [ProtoMember(3, Name="BizList", DataFormat=DataFormat.Default)]
        public List<SearchOrRecommendItem> BizList
        {
            get
            {
                return this._BizList;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="IsEnd", DataFormat=DataFormat.TwosComplement)]
        public uint IsEnd
        {
            get
            {
                return this._IsEnd;
            }
            set
            {
                this._IsEnd = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="ResBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ResBuf
        {
            get
            {
                return this._ResBuf;
            }
            set
            {
                this._ResBuf = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ShowFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ShowFlag
        {
            get
            {
                return this._ShowFlag;
            }
            set
            {
                this._ShowFlag = value;
            }
        }
    }
}

