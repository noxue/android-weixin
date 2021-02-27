namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="AskForRewardRequest")]
    public class AskForRewardRequest : IExtensible
    {
        private EmotionPrice _Price;
        private string _ProductID;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Price", DataFormat=DataFormat.Default)]
        public EmotionPrice Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ProductID", DataFormat=DataFormat.Default)]
        public string ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this._ProductID = value;
            }
        }
    }
}

