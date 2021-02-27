namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipAddr")]
    public class VoipAddr : IExtensible
    {
        private int _Ip;
        private int _Port;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Ip", DataFormat=DataFormat.TwosComplement)]
        public int Ip
        {
            get
            {
                return this._Ip;
            }
            set
            {
                this._Ip = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Port", DataFormat=DataFormat.TwosComplement)]
        public int Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                this._Port = value;
            }
        }
    }
}

