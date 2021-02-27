namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewSyncResponse")]
    public class NewSyncResponse : IExtensible
    {
        private micromsg.CmdList _CmdList;
        private uint _ContinueFlag;
        private SKBuiltinBuffer_t _KeyBuf;
        private uint _OnlineVersion = 0;
        private int _Ret;
        private uint _Status = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(3, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(4, IsRequired=true, Name="KeyBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuf
        {
            get
            {
                return this._KeyBuf;
            }
            set
            {
                this._KeyBuf = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="OnlineVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint OnlineVersion
        {
            get
            {
                return this._OnlineVersion;
            }
            set
            {
                this._OnlineVersion = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Status", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
    }
}

