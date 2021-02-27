namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ResetPawInfo")]
    public class ResetPawInfo : IExtensible
    {
        private uint _CreateTime;
        private uint _ID;
        private uint _IsReset;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
        public uint CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ID", DataFormat=DataFormat.TwosComplement)]
        public uint ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="IsReset", DataFormat=DataFormat.TwosComplement)]
        public uint IsReset
        {
            get
            {
                return this._IsReset;
            }
            set
            {
                this._IsReset = value;
            }
        }
    }
}

