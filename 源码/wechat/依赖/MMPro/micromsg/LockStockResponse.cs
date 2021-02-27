namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LockStockResponse")]
    public class LockStockResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _LockId = 0;
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

        [ProtoMember(2, IsRequired=false, Name="LockId", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint LockId
        {
            get
            {
                return this._LockId;
            }
            set
            {
                this._LockId = value;
            }
        }
    }
}

