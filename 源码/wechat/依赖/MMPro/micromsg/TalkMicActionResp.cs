namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="TalkMicActionResp")]
    public class TalkMicActionResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ChannelId = 0;
        private int _MicSeq;
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

        [ProtoMember(3, IsRequired=false, Name="ChannelId", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChannelId
        {
            get
            {
                return this._ChannelId;
            }
            set
            {
                this._ChannelId = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="MicSeq", DataFormat=DataFormat.TwosComplement)]
        public int MicSeq
        {
            get
            {
                return this._MicSeq;
            }
            set
            {
                this._MicSeq = value;
            }
        }
    }
}

