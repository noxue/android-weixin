namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckPCWechatFileResponse")]
    public class CheckPCWechatFileResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private int _BlockStatus = 0;
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

        [ProtoMember(2, IsRequired=false, Name="BlockStatus", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int BlockStatus
        {
            get
            {
                return this._BlockStatus;
            }
            set
            {
                this._BlockStatus = value;
            }
        }
    }
}

