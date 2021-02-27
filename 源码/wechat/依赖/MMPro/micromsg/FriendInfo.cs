namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FriendInfo")]
    public class FriendInfo : IExtensible
    {
        private string _Alias = "";
        private string _DisplayNickName = "";
        private string _DisplayRemark = "";
        private string _DisplayUserName = "";
        private uint _MatchField = 0;
        private string _Source = "";
        private uint _Status;
        private uint _Type;
        private string _WXNickName = "";
        private string _WXRemark = "";
        private string _WXUserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(11, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Alias
        {
            get
            {
                return this._Alias;
            }
            set
            {
                this._Alias = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="DisplayNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DisplayNickName
        {
            get
            {
                return this._DisplayNickName;
            }
            set
            {
                this._DisplayNickName = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="DisplayRemark", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DisplayRemark
        {
            get
            {
                return this._DisplayRemark;
            }
            set
            {
                this._DisplayRemark = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="DisplayUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DisplayUserName
        {
            get
            {
                return this._DisplayUserName;
            }
            set
            {
                this._DisplayUserName = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="MatchField", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MatchField
        {
            get
            {
                return this._MatchField;
            }
            set
            {
                this._MatchField = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Source", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public uint Status
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

        [ProtoMember(7, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=false, Name="WXNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WXNickName
        {
            get
            {
                return this._WXNickName;
            }
            set
            {
                this._WXNickName = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="WXRemark", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WXRemark
        {
            get
            {
                return this._WXRemark;
            }
            set
            {
                this._WXRemark = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="WXUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WXUserName
        {
            get
            {
                return this._WXUserName;
            }
            set
            {
                this._WXUserName = value;
            }
        }
    }
}

