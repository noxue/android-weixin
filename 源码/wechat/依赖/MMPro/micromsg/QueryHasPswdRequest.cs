namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="QueryHasPswdRequest")]
    public class QueryHasPswdRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private int _Scene;
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

        [ProtoMember(2, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
        public int Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }
    }
}

