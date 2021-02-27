namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="UnBindLinkedinContactRequest")]
    public class UnBindLinkedinContactRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Opcode;
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

        [ProtoMember(2, IsRequired=true, Name="Opcode", DataFormat=DataFormat.TwosComplement)]
        public uint Opcode
        {
            get
            {
                return this._Opcode;
            }
            set
            {
                this._Opcode = value;
            }
        }
    }
}

