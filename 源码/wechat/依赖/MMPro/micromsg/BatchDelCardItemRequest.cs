namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchDelCardItemRequest")]
    public class BatchDelCardItemRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<string> _card_ids = new List<string>();
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

        [ProtoMember(2, Name="card_ids", DataFormat=DataFormat.Default)]
        public List<string> card_ids
        {
            get
            {
                return this._card_ids;
            }
        }
    }
}

