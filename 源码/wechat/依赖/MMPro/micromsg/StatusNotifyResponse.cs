namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="StatusNotifyResponse")]
    public class StatusNotifyResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ChatContactCount = 0;
        private readonly List<ChatContact> _ChatContactList = new List<ChatContact>();
        private uint _MsgId;
        private ulong _NewMsgId = 0L;
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

        [ProtoMember(4, IsRequired=false, Name="ChatContactCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChatContactCount
        {
            get
            {
                return this._ChatContactCount;
            }
            set
            {
                this._ChatContactCount = value;
            }
        }

        [ProtoMember(5, Name="ChatContactList", DataFormat=DataFormat.Default)]
        public List<ChatContact> ChatContactList
        {
            get
            {
                return this._ChatContactList;
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

        [ProtoMember(3, IsRequired=false, Name="NewMsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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
    }
}

