namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="ShareCardResponse")]
    public class ShareCardResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _RetInfoCount;
        private readonly List<ShareCardRetInfo> _RetInfoList = new List<ShareCardRetInfo>();
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

        [ProtoMember(2, IsRequired=true, Name="RetInfoCount", DataFormat=DataFormat.TwosComplement)]
        public uint RetInfoCount
        {
            get
            {
                return this._RetInfoCount;
            }
            set
            {
                this._RetInfoCount = value;
            }
        }

        [ProtoMember(3, Name="RetInfoList", DataFormat=DataFormat.Default)]
        public List<ShareCardRetInfo> RetInfoList
        {
            get
            {
                return this._RetInfoList;
            }
        }
    }
}

