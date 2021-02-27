namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsObjectDetailRequest")]
    public class SnsObjectDetailRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _GroupDetail = 0;
        private ulong _Id;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="GroupDetail", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GroupDetail
        {
            get
            {
                return this._GroupDetail;
            }
            set
            {
                this._GroupDetail = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public ulong Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
    }
}

