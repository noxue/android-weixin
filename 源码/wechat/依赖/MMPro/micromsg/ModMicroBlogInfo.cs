namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ModMicroBlogInfo")]
    public class ModMicroBlogInfo : IExtensible
    {
        private uint _DeleteFlag;
        private uint _MicroBlogType;
        private uint _NotifyStatus;
        private SKBuiltinString_t _UserName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="DeleteFlag", DataFormat=DataFormat.TwosComplement)]
        public uint DeleteFlag
        {
            get
            {
                return this._DeleteFlag;
            }
            set
            {
                this._DeleteFlag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="MicroBlogType", DataFormat=DataFormat.TwosComplement)]
        public uint MicroBlogType
        {
            get
            {
                return this._MicroBlogType;
            }
            set
            {
                this._MicroBlogType = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="NotifyStatus", DataFormat=DataFormat.TwosComplement)]
        public uint NotifyStatus
        {
            get
            {
                return this._NotifyStatus;
            }
            set
            {
                this._NotifyStatus = value;
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

