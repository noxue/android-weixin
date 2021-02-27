namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="RegisterVoicePrintResponse")]
    public class RegisterVoicePrintResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private VoicePieceCtx _NextPiece;
        private uint _NextStep;
        private int _ResgisterRet;
        private uint _VoiceTicket;
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

        [ProtoMember(4, IsRequired=true, Name="NextPiece", DataFormat=DataFormat.Default)]
        public VoicePieceCtx NextPiece
        {
            get
            {
                return this._NextPiece;
            }
            set
            {
                this._NextPiece = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="NextStep", DataFormat=DataFormat.TwosComplement)]
        public uint NextStep
        {
            get
            {
                return this._NextStep;
            }
            set
            {
                this._NextStep = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ResgisterRet", DataFormat=DataFormat.TwosComplement)]
        public int ResgisterRet
        {
            get
            {
                return this._ResgisterRet;
            }
            set
            {
                this._ResgisterRet = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="VoiceTicket", DataFormat=DataFormat.TwosComplement)]
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

