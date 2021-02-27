namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="ShareFavResponse")]
    public class ShareFavResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _UrlCount;
        private readonly List<SKBuiltinString_t> _UrlList = new List<SKBuiltinString_t>();
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

        [ProtoMember(2, IsRequired=true, Name="UrlCount", DataFormat=DataFormat.TwosComplement)]
        public uint UrlCount
        {
            get
            {
                return this._UrlCount;
            }
            set
            {
                this._UrlCount = value;
            }
        }

        [ProtoMember(3, Name="UrlList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> UrlList
        {
            get
            {
                return this._UrlList;
            }
        }
    }
}

