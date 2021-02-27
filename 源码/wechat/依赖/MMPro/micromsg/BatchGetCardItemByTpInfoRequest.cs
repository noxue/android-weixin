namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchGetCardItemByTpInfoRequest")]
    public class BatchGetCardItemByTpInfoRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<CardTpInfoItem> _items = new List<CardTpInfoItem>();
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

        [ProtoMember(2, Name="items", DataFormat=DataFormat.Default)]
        public List<CardTpInfoItem> items
        {
            get
            {
                return this._items;
            }
        }
    }
}

