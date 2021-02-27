namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchDelCardItemResponse")]
    public class BatchDelCardItemResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<string> _succ_card_ids = new List<string>();
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

        [ProtoMember(2, Name="succ_card_ids", DataFormat=DataFormat.Default)]
        public List<string> succ_card_ids
        {
            get
            {
                return this._succ_card_ids;
            }
        }
    }
}

