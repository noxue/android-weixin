namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="WhatsNewsResponse")]
    public class WhatsNewsResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count = 0;
        private uint _FstSNSTime = 0;
        private readonly List<SKBuiltinString_t> _PicUrlList = new List<SKBuiltinString_t>();
        private uint _RegistTime = 0;
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

        [ProtoMember(4, IsRequired=false, Name="Count", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(3, IsRequired=false, Name="FstSNSTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FstSNSTime
        {
            get
            {
                return this._FstSNSTime;
            }
            set
            {
                this._FstSNSTime = value;
            }
        }

        [ProtoMember(5, Name="PicUrlList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> PicUrlList
        {
            get
            {
                return this._PicUrlList;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="RegistTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RegistTime
        {
            get
            {
                return this._RegistTime;
            }
            set
            {
                this._RegistTime = value;
            }
        }
    }
}

