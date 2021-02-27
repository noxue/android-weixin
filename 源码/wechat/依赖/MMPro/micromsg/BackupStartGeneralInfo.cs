namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="BackupStartGeneralInfo")]
    public class BackupStartGeneralInfo : IExtensible
    {
        private ulong _DeviceFreeSpace;
        private string _DeviceID;
        private string _DeviceName;
        private string _Model;
        private string _SystemName;
        private string _SystemVersion;
        private uint _WechatVersion;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=true, Name="DeviceFreeSpace", DataFormat=DataFormat.TwosComplement)]
        public ulong DeviceFreeSpace
        {
            get
            {
                return this._DeviceFreeSpace;
            }
            set
            {
                this._DeviceFreeSpace = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="DeviceID", DataFormat=DataFormat.Default)]
        public string DeviceID
        {
            get
            {
                return this._DeviceID;
            }
            set
            {
                this._DeviceID = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="DeviceName", DataFormat=DataFormat.Default)]
        public string DeviceName
        {
            get
            {
                return this._DeviceName;
            }
            set
            {
                this._DeviceName = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Model", DataFormat=DataFormat.Default)]
        public string Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                this._Model = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="SystemName", DataFormat=DataFormat.Default)]
        public string SystemName
        {
            get
            {
                return this._SystemName;
            }
            set
            {
                this._SystemName = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="SystemVersion", DataFormat=DataFormat.Default)]
        public string SystemVersion
        {
            get
            {
                return this._SystemVersion;
            }
            set
            {
                this._SystemVersion = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="WechatVersion", DataFormat=DataFormat.TwosComplement)]
        public uint WechatVersion
        {
            get
            {
                return this._WechatVersion;
            }
            set
            {
                this._WechatVersion = value;
            }
        }
    }
}

