namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsPostCtocUploadInfo")]
    public class SnsPostCtocUploadInfo : IExtensible
    {
        private uint _Flag;
        private uint _PhotoCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Flag", DataFormat=DataFormat.TwosComplement)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="PhotoCount", DataFormat=DataFormat.TwosComplement)]
        public uint PhotoCount
        {
            get
            {
                return this._PhotoCount;
            }
            set
            {
                this._PhotoCount = value;
            }
        }
    }
}

