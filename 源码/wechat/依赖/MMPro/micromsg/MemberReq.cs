namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="MemberReq")]
    public class MemberReq : IExtensible
    {
        private SKBuiltinString_t _MemberName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="MemberName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t MemberName
        {
            get
            {
                return this._MemberName;
            }
            set
            {
                this._MemberName = value;
            }
        }
    }
}

