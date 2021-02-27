namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DownLoadPackageResponse")]
    public class DownLoadPackageResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _PackageBuf;
        private uint _TotalSize = 0;
        private uint _Type = 0;
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

        [ProtoMember(2, IsRequired=true, Name="PackageBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t PackageBuf
        {
            get
            {
                return this._PackageBuf;
            }
            set
            {
                this._PackageBuf = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="TotalSize", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TotalSize
        {
            get
            {
                return this._TotalSize;
            }
            set
            {
                this._TotalSize = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

