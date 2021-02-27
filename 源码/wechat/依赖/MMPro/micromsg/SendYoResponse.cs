namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SendYoResponse")]
    public class SendYoResponse : IExtensible
    {
        private ulong _MsgId;
        private int _Ret;
        private uint _ServerTime;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="MsgId", DataFormat=DataFormat.TwosComplement)]
        public ulong MsgId
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

        [ProtoMember(2, IsRequired=true, Name="ServerTime", DataFormat=DataFormat.TwosComplement)]
        public uint ServerTime
        {
            get
            {
                return this._ServerTime;
            }
            set
            {
                this._ServerTime = value;
            }
        }
    }
}

