namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="RemindSyncResponse")]
    public class RemindSyncResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.CmdList _CmdList;
        private uint _ContinueFlag;
        private SKBuiltinBuffer_t _KeyBuff;
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

        [ProtoMember(2, IsRequired=true, Name="CmdList", DataFormat=DataFormat.Default)]
        public micromsg.CmdList CmdList
        {
            get
            {
                return this._CmdList;
            }
            set
            {
                this._CmdList = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ContinueFlag
        {
            get
            {
                return this._ContinueFlag;
            }
            set
            {
                this._ContinueFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="KeyBuff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuff
        {
            get
            {
                return this._KeyBuff;
            }
            set
            {
                this._KeyBuff = value;
            }
        }
    }
}

