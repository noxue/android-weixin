namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipCompleteStatus")]
    public class VoipCompleteStatus : IExtensible
    {
        private SKBuiltinBuffer_t _Buffer;
        private int _CmdId;
        private uint _Key;
        private uint _Uin;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="Buffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="CmdId", DataFormat=DataFormat.TwosComplement)]
        public int CmdId
        {
            get
            {
                return this._CmdId;
            }
            set
            {
                this._CmdId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Key", DataFormat=DataFormat.TwosComplement)]
        public uint Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Uin", DataFormat=DataFormat.TwosComplement)]
        public uint Uin
        {
            get
            {
                return this._Uin;
            }
            set
            {
                this._Uin = value;
            }
        }
    }
}

