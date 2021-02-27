namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="VerifyEmotionPresentationResponse")]
    public class VerifyEmotionPresentationResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _MemberCount;
        private readonly List<MemberStatus> _StatusList = new List<MemberStatus>();
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

        [ProtoMember(2, IsRequired=true, Name="MemberCount", DataFormat=DataFormat.TwosComplement)]
        public uint MemberCount
        {
            get
            {
                return this._MemberCount;
            }
            set
            {
                this._MemberCount = value;
            }
        }

        [ProtoMember(3, Name="StatusList", DataFormat=DataFormat.Default)]
        public List<MemberStatus> StatusList
        {
            get
            {
                return this._StatusList;
            }
        }
    }
}

