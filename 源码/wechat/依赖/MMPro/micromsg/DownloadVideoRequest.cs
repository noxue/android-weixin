namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DownloadVideoRequest")]
    public class DownloadVideoRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _MsgId;
        private uint _MxPackSize = 0;
        private uint _NetworkEnv = 0;
        private ulong _NewMsgId = 0L;
        private uint _StartPos;
        private uint _TotalLen;
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

        [ProtoMember(2, IsRequired=true, Name="MsgId", DataFormat=DataFormat.TwosComplement)]
        public uint MsgId
        {
            get
            {
                return this._MsgId;
            }
            set
            {
                this._MsgId = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="MxPackSize", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MxPackSize
        {
            get
            {
                return this._MxPackSize;
            }
            set
            {
                this._MxPackSize = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="NetworkEnv", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NetworkEnv
        {
            get
            {
                return this._NetworkEnv;
            }
            set
            {
                this._NetworkEnv = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="NewMsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong NewMsgId
        {
            get
            {
                return this._NewMsgId;
            }
            set
            {
                this._NewMsgId = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public uint StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }
    }
}

