namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SetOAuthScopeResponse")]
    public class SetOAuthScopeResponse : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _ScopeCount;
        private readonly List<BizScopeInfo> _ScopeList = new List<BizScopeInfo>();
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

        [ProtoMember(2, IsRequired=true, Name="ScopeCount", DataFormat=DataFormat.TwosComplement)]
        public uint ScopeCount
        {
            get
            {
                return this._ScopeCount;
            }
            set
            {
                this._ScopeCount = value;
            }
        }

        [ProtoMember(3, Name="ScopeList", DataFormat=DataFormat.Default)]
        public List<BizScopeInfo> ScopeList
        {
            get
            {
                return this._ScopeList;
            }
        }
    }
}

