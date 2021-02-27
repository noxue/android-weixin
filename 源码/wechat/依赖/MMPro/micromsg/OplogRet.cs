namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="OplogRet")]
    public class OplogRet : IExtensible
    {
        private uint _Count;
        private readonly List<int> _Ret = new List<int>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(2, Name="Ret", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<int> Ret
        {
            get
            {
                return this._Ret;
            }
        }
    }
}

