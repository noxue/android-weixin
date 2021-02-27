namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="VoipCompleteStatusList")]
    public class VoipCompleteStatusList : IExtensible
    {
        private readonly List<VoipCompleteStatus> _CompleteStatus = new List<VoipCompleteStatus>();
        private int _Count;
        private uint _Seq;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, Name="CompleteStatus", DataFormat=DataFormat.Default)]
        public List<VoipCompleteStatus> CompleteStatus
        {
            get
            {
                return this._CompleteStatus;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public int Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Seq", DataFormat=DataFormat.TwosComplement)]
        public uint Seq
        {
            get
            {
                return this._Seq;
            }
            set
            {
                this._Seq = value;
            }
        }
    }
}

