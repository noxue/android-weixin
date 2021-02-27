namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetPersonalDesignerRequest")]
    public class GetPersonalDesignerRequest : IExtensible
    {
        private uint _DesignerUin;
        private SKBuiltinBuffer_t _ReqBuf;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="DesignerUin", DataFormat=DataFormat.TwosComplement)]
        public uint DesignerUin
        {
            get
            {
                return this._DesignerUin;
            }
            set
            {
                this._DesignerUin = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ReqBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ReqBuf
        {
            get
            {
                return this._ReqBuf;
            }
            set
            {
                this._ReqBuf = value;
            }
        }
    }
}

