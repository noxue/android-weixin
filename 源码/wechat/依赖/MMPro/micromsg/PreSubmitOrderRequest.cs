namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="PreSubmitOrderRequest")]
    public class PreSubmitOrderRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<SampleProduct> _Product = new List<SampleProduct>();
        private uint _ProductCount;
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

        [ProtoMember(3, Name="Product", DataFormat=DataFormat.Default)]
        public List<SampleProduct> Product
        {
            get
            {
                return this._Product;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ProductCount", DataFormat=DataFormat.TwosComplement)]
        public uint ProductCount
        {
            get
            {
                return this._ProductCount;
            }
            set
            {
                this._ProductCount = value;
            }
        }
    }
}

