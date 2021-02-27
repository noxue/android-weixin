namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DelContactMsg")]
    public class DelContactMsg : IExtensible
    {
        private int _MaxMsgId;
        private long _NewMsgId = 0L;
        private SKBuiltinString_t _UserName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="MaxMsgId", DataFormat=DataFormat.TwosComplement)]
        public int MaxMsgId
        {
            get
            {
                return this._MaxMsgId;
            }
            set
            {
                this._MaxMsgId = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="NewMsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public long NewMsgId
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

        [ProtoMember(1, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

