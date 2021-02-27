namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="RegisterVoicePrintRequest")]
    public class RegisterVoicePrintRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private VoicePieceCtx _PieceData;
        private uint _ResId;
        private uint _Step;
        private uint _VoiceTicket;
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

        [ProtoMember(4, IsRequired=true, Name="PieceData", DataFormat=DataFormat.Default)]
        public VoicePieceCtx PieceData
        {
            get
            {
                return this._PieceData;
            }
            set
            {
                this._PieceData = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ResId", DataFormat=DataFormat.TwosComplement)]
        public uint ResId
        {
            get
            {
                return this._ResId;
            }
            set
            {
                this._ResId = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Step", DataFormat=DataFormat.TwosComplement)]
        public uint Step
        {
            get
            {
                return this._Step;
            }
            set
            {
                this._Step = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="VoiceTicket", DataFormat=DataFormat.TwosComplement)]
        public uint VoiceTicket
        {
            get
            {
                return this._VoiceTicket;
            }
            set
            {
                this._VoiceTicket = value;
            }
        }
    }
}

