namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="JSAPIPermissionBitSet")]
    public class JSAPIPermissionBitSet : IExtensible
    {
        private uint _BitValue;
        private uint _BitValue2 = 0;
        private uint _BitValue3 = 0;
        private uint _BitValue4 = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BitValue", DataFormat=DataFormat.TwosComplement)]
        public uint BitValue
        {
            get
            {
                return this._BitValue;
            }
            set
            {
                this._BitValue = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="BitValue2", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BitValue2
        {
            get
            {
                return this._BitValue2;
            }
            set
            {
                this._BitValue2 = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="BitValue3", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BitValue3
        {
            get
            {
                return this._BitValue3;
            }
            set
            {
                this._BitValue3 = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="BitValue4", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BitValue4
        {
            get
            {
                return this._BitValue4;
            }
            set
            {
                this._BitValue4 = value;
            }
        }
    }
}

