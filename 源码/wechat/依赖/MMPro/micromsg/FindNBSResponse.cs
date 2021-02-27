namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="FindNBSResponse")]
    public class FindNBSResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _NBSConfigCount;
        private readonly List<NBSConfigInfo> _NBSConfigList = new List<NBSConfigInfo>();
        private uint _NBSCount;
        private readonly List<NBSInfo> _NBSList = new List<NBSInfo>();
        private SKBuiltinBuffer_t _PageBuff;
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

        [ProtoMember(4, IsRequired=true, Name="NBSConfigCount", DataFormat=DataFormat.TwosComplement)]
        public uint NBSConfigCount
        {
            get
            {
                return this._NBSConfigCount;
            }
            set
            {
                this._NBSConfigCount = value;
            }
        }

        [ProtoMember(5, Name="NBSConfigList", DataFormat=DataFormat.Default)]
        public List<NBSConfigInfo> NBSConfigList
        {
            get
            {
                return this._NBSConfigList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="NBSCount", DataFormat=DataFormat.TwosComplement)]
        public uint NBSCount
        {
            get
            {
                return this._NBSCount;
            }
            set
            {
                this._NBSCount = value;
            }
        }

        [ProtoMember(3, Name="NBSList", DataFormat=DataFormat.Default)]
        public List<NBSInfo> NBSList
        {
            get
            {
                return this._NBSList;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="PageBuff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t PageBuff
        {
            get
            {
                return this._PageBuff;
            }
            set
            {
                this._PageBuff = value;
            }
        }
    }
}

