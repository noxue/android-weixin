namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="MassSendResponse")]
    public class MassSendResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _DataStartPos;
        private uint _MaxSupport;
        private uint _ThumbStartPos;
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

        [ProtoMember(2, IsRequired=true, Name="DataStartPos", DataFormat=DataFormat.TwosComplement)]
        public uint DataStartPos
        {
            get
            {
                return this._DataStartPos;
            }
            set
            {
                this._DataStartPos = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="MaxSupport", DataFormat=DataFormat.TwosComplement)]
        public uint MaxSupport
        {
            get
            {
                return this._MaxSupport;
            }
            set
            {
                this._MaxSupport = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ThumbStartPos", DataFormat=DataFormat.TwosComplement)]
        public uint ThumbStartPos
        {
            get
            {
                return this._ThumbStartPos;
            }
            set
            {
                this._ThumbStartPos = value;
            }
        }
    }
}

