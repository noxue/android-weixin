namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetBoundHardDevicesResponse")]
    public class GetBoundHardDevicesResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ContinueFlag;
        private uint _Count;
        private readonly List<ModHardDevice> _DeviceList = new List<ModHardDevice>();
        private uint _Version;
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

        [ProtoMember(7, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ContinueFlag
        {
            get
            {
                return this._ContinueFlag;
            }
            set
            {
                this._ContinueFlag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
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

        [ProtoMember(5, Name="DeviceList", DataFormat=DataFormat.Default)]
        public List<ModHardDevice> DeviceList
        {
            get
            {
                return this._DeviceList;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Version", DataFormat=DataFormat.TwosComplement)]
        public uint Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }
    }
}

