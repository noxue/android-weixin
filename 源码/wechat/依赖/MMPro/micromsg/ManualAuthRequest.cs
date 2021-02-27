namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ManualAuthRequest")]
    public class ManualAuthRequest : IExtensible
    {
        private ManualAuthAesReqData _AesReqData;
        private ManualAuthRsaReqData _RsaReqData;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AesReqData", DataFormat=DataFormat.Default)]
        public ManualAuthAesReqData AesReqData
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
        public ManualAuthRsaReqData RsaReqData
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

