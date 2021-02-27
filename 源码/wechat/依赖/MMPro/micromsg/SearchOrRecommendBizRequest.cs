namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SearchOrRecommendBizRequest")]
    public class SearchOrRecommendBizRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _EntryFlag;
        private SKBuiltinString_t _NickName;
        private uint _OpCode;
        private SKBuiltinBuffer_t _ReqBuf;
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

        [ProtoMember(4, IsRequired=true, Name="EntryFlag", DataFormat=DataFormat.TwosComplement)]
        public uint EntryFlag
        {
            get
            {
                return this._EntryFlag;
            }
            set
            {
                this._EntryFlag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="NickName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ReqBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ReqBuf
        {
            get
            {
                return this._ReqBuf;
            }
            set
            {
                this._ReqBuf = value;
            }
        }
    }
}

