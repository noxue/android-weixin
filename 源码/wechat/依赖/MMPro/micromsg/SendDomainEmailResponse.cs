namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SendDomainEmailResponse")]
    public class SendDomainEmailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private int _DomainEmailRet;
        private readonly List<DomainEmailItem> _List = new List<DomainEmailItem>();
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

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(2, IsRequired=true, Name="DomainEmailRet", DataFormat=DataFormat.TwosComplement)]
        public int DomainEmailRet
        {
            get
            {
                return this._DomainEmailRet;
            }
            set
            {
                this._DomainEmailRet = value;
            }
        }

        [ProtoMember(4, Name="List", DataFormat=DataFormat.Default)]
        public List<DomainEmailItem> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

