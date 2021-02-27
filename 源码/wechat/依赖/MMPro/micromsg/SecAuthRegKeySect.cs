namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SecAuthRegKeySect")]
    public class SecAuthRegKeySect : IExtensible
    {
        private uint _AuthResultFlag;
        private SKBuiltinBuffer_t _AutoAuthKey;
        private SKBuiltinBuffer_t _SessionKey;
        private ECDHKey _SvrPubECDHKey;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="AuthResultFlag", DataFormat=DataFormat.TwosComplement)]
        public uint AuthResultFlag
        {
            get
            {
                return this._AuthResultFlag;
            }
            set
            {
                this._AuthResultFlag = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="AutoAuthKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t AutoAuthKey
        {
            get
            {
                return this._AutoAuthKey;
            }
            set
            {
                this._AutoAuthKey = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="SessionKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SessionKey
        {
            get
            {
                return this._SessionKey;
            }
            set
            {
                this._SessionKey = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="SvrPubECDHKey", DataFormat=DataFormat.Default)]
        public ECDHKey SvrPubECDHKey
        {
            get
            {
                return this._SvrPubECDHKey;
            }
            set
            {
                this._SvrPubECDHKey = value;
            }
        }
    }
}

