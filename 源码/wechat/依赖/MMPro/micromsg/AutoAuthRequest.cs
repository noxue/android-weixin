namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="AutoAuthRequest")]
    public class AutoAuthRequest : IExtensible
    {
        private AutoAuthAesReqData _AesReqData;
        private AutoAuthRsaReqData _RsaReqData;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AesReqData", DataFormat=DataFormat.Default)]
        public AutoAuthAesReqData AesReqData
        {
            get
            {
                return this._AesReqData;
            }
            set
            {
                this._AesReqData = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="RsaReqData", DataFormat=DataFormat.Default)]
        public AutoAuthRsaReqData RsaReqData
        {
            get
            {
                return this._RsaReqData;
            }
            set
            {
                this._RsaReqData = value;
            }
        }
    }
}

