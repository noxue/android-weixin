namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetUpdatePackRequest")]
    public class GetUpdatePackRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Channel = 0;
        private uint _StartPos;
        private uint _TotalLen;
        private uint _UpdateType;
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

        [ProtoMember(5, IsRequired=false, Name="Channel", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Channel
        {
            get
            {
                return this._Channel;
            }
            set
            {
                this._Channel = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public uint StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="UpdateType", DataFormat=DataFormat.TwosComplement)]
        public uint UpdateType
        {
            get
            {
                return this._UpdateType;
            }
            set
            {
                this._UpdateType = value;
            }
        }
    }
}

