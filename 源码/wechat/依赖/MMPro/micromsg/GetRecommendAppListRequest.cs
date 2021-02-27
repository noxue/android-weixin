namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetRecommendAppListRequest")]
    public class GetRecommendAppListRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Count;
        private uint _IconType;
        private string _InstalledList = "";
        private uint _Start;
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

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=true, Name="IconType", DataFormat=DataFormat.TwosComplement)]
        public uint IconType
        {
            get
            {
                return this._IconType;
            }
            set
            {
                this._IconType = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="InstalledList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string InstalledList
        {
            get
            {
                return this._InstalledList;
            }
            set
            {
                this._InstalledList = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Start", DataFormat=DataFormat.TwosComplement)]
        public uint Start
        {
            get
            {
                return this._Start;
            }
            set
            {
                this._Start = value;
            }
        }
    }
}

