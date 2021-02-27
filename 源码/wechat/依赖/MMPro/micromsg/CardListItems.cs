namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="CardListItems")]
    public class CardListItems : IExtensible
    {
        private readonly List<CardListItem> _card_list = new List<CardListItem>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, Name="card_list", DataFormat=DataFormat.Default)]
        public List<CardListItem> card_list
        {
            get
            {
                return this._card_list;
            }
        }
    }
}

