namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DownLoadPackageRequest")]
    public class DownLoadPackageRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Len;
        private uint _Offset;
        private micromsg.Package _Package;
        private uint _Type = 0;
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

        [ProtoMember(4, IsRequired=true, Name="Len", DataFormat=DataFormat.TwosComplement)]
        public uint Len
        {
            get
            {
                return this._Len;
            }
            set
            {
                this._Len = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Offset", DataFormat=DataFormat.TwosComplement)]
        public uint Offset
        {
            get
            {
                return this._Offset;
            }
            set
            {
                this._Offset = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Package", DataFormat=DataFormat.Default)]
        public micromsg.Package Package
        {
            get
            {
                return this._Package;
            }
            set
            {
                this._Package = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

