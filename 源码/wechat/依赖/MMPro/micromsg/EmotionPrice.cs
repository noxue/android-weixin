namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="EmotionPrice")]
    public class EmotionPrice : IExtensible
    {
        private string _Label;
        private string _Number;
        private string _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Label", DataFormat=DataFormat.Default)]
        public string Label
        {
            get
            {
                return this._Label;
            }
            set
            {
                this._Label = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Number", DataFormat=DataFormat.Default)]
        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Type", DataFormat=DataFormat.Default)]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

