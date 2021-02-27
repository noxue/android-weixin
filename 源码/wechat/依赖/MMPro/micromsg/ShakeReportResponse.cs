namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeReportResponse")]
    public class ShakeReportResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _Buffer;
        private uint _ImgId;
        private uint _ImgTotoalLen = 0;
        private int _Ret;
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

        [ProtoMember(2, IsRequired=true, Name="Buffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ImgId", DataFormat=DataFormat.TwosComplement)]
        public uint ImgId
        {
            get
            {
                return this._ImgId;
            }
            set
            {
                this._ImgId = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ImgTotoalLen", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ImgTotoalLen
        {
            get
            {
                return this._ImgTotoalLen;
            }
            set
            {
                this._ImgTotoalLen = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }
    }
}

