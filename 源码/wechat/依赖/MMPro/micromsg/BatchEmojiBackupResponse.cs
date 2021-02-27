namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BatchEmojiBackupResponse")]
    public class BatchEmojiBackupResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<string> _NeedUploadMd5List = new List<string>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, Name="NeedUploadMd5List", DataFormat=DataFormat.Default)]
        public List<string> NeedUploadMd5List
        {
            get
            {
                return this._NeedUploadMd5List;
            }
        }
    }
}

