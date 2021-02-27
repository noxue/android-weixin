namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="PacketBackupDataResponse")]
    public class PacketBackupDataResponse : IExtensible
    {
        private string _DataID;
        private int _DataType;
        private int _EndOffset;
        private int _StartOffset;
        private int _Status;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="DataID", DataFormat=DataFormat.Default)]
        public string DataID
        {
            get
            {
                return this._DataID;
            }
            set
            {
                this._DataID = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="DataType", DataFormat=DataFormat.TwosComplement)]
        public int DataType
        {
            get
            {
                return this._DataType;
            }
            set
            {
                this._DataType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="EndOffset", DataFormat=DataFormat.TwosComplement)]
        public int EndOffset
        {
            get
            {
                return this._EndOffset;
            }
            set
            {
                this._EndOffset = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="StartOffset", DataFormat=DataFormat.TwosComplement)]
        public int StartOffset
        {
            get
            {
                return this._StartOffset;
            }
            set
            {
                this._StartOffset = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
    }
}

