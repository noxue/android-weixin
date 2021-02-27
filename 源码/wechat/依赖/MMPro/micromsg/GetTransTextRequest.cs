namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetTransTextRequest")]
    public class GetTransTextRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _MsgCount;
        private readonly List<TranslateOrgMsg> _MsgList = new List<TranslateOrgMsg>();
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

        [ProtoMember(2, IsRequired=true, Name="MsgCount", DataFormat=DataFormat.TwosComplement)]
        public uint MsgCount
        {
            get
            {
                return this._MsgCount;
            }
            set
            {
                this._MsgCount = value;
            }
        }

        [ProtoMember(3, Name="MsgList", DataFormat=DataFormat.Default)]
        public List<TranslateOrgMsg> MsgList
        {
            get
            {
                return this._MsgList;
            }
        }
    }
}

