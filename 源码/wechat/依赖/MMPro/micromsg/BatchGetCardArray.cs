namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchGetCardArray")]
    public class BatchGetCardArray : IExtensible
    {
        private readonly List<BatchGetCardItem> _card_array = new List<BatchGetCardItem>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, Name="card_array", DataFormat=DataFormat.Default)]
        public List<BatchGetCardItem> card_array
        {
            get
            {
                return this._card_array;
            }
        }
    }
}

