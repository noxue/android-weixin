namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetAppInfoResponse")]
    public class GetAppInfoResponse : IExtensible
    {
        private OpenAppInfo _AppInfo;
        private string _AppType = "";
        private micromsg.BaseResponse _BaseResponse;
        private string _DevInfo = "";
        private uint _NoUse = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AppInfo", DataFormat=DataFormat.Default)]
        public OpenAppInfo AppInfo
        {
            get
            {
                return this._AppInfo;
            }
            set
            {
                this._AppInfo = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="AppType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppType
        {
            get
            {
                return this._AppType;
            }
            set
            {
                this._AppType = value;
            }
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

        [ProtoMember(4, IsRequired=false, Name="DevInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DevInfo
        {
            get
            {
                return this._DevInfo;
            }
            set
            {
                this._DevInfo = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="NoUse", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NoUse
        {
            get
            {
                return this._NoUse;
            }
            set
            {
                this._NoUse = value;
            }
        }
    }
}

