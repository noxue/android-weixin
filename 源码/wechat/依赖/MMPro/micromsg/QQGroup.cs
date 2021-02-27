namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="QQGroup")]
    public class QQGroup : IExtensible
    {
        private uint _GroupID;
        private string _GroupName = "";
        private string _MD5 = "";
        private uint _MemberNum;
        private uint _WeixinNum;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="GroupID", DataFormat=DataFormat.TwosComplement)]
        public uint GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                this._GroupID = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="GroupName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                this._GroupName = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="MD5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MD5
        {
            get
            {
                return this._MD5;
            }
            set
            {
                this._MD5 = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="MemberNum", DataFormat=DataFormat.TwosComplement)]
        public uint MemberNum
        {
            get
            {
                return this._MemberNum;
            }
            set
            {
                this._MemberNum = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="WeixinNum", DataFormat=DataFormat.TwosComplement)]
        public uint WeixinNum
        {
            get
            {
                return this._WeixinNum;
            }
            set
            {
                this._WeixinNum = value;
            }
        }
    }
}

