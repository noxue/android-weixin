namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetRewardMagicListResponse")]
    public class GetRewardMagicListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<RewardMagic> _Magic = new List<RewardMagic>();
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

        [ProtoMember(2, Name="Magic", DataFormat=DataFormat.Default)]
        public List<RewardMagic> Magic
        {
            get
            {
                return this._Magic;
            }
        }
    }
}

