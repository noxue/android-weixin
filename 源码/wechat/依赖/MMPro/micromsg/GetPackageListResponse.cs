namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetPackageListResponse")]
    public class GetPackageListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ContinueFlag;
        private uint _Count;
        private readonly List<Package> _List = new List<Package>();
        private uint _SvrCount;
        private uint _Type = 0;
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

        [ProtoMember(4, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<Package> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="SvrCount", DataFormat=DataFormat.TwosComplement)]
        public uint SvrCount
        {
            get
            {
                return this._SvrCount;
            }
            set
            {
                this._SvrCount = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

