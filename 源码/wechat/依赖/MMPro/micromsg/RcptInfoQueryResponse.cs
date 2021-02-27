namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RcptInfoQueryResponse")]
    public class RcptInfoQueryResponse : IExtensible
    {
        private string _appnickname = "";
        private string _appusername = "";
        private micromsg.BaseResponse _BaseResponse;
        private uint _isauthority = 0;
        private uint _islatest;
        private RcptInfoList _rcptinfolist;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=false, Name="appnickname", DataFormat=DataFormat.Default), DefaultValue("")]
        public string appnickname
        {
            get
            {
                return this._appnickname;
            }
            set
            {
                this._appnickname = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="appusername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string appusername
        {
            get
            {
                return this._appusername;
            }
            set
            {
                this._appusername = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
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

        [ProtoMember(5, IsRequired=false, Name="isauthority", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint isauthority
        {
            get
            {
                return this._isauthority;
            }
            set
            {
                this._isauthority = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="islatest", DataFormat=DataFormat.TwosComplement)]
        public uint islatest
        {
            get
            {
                return this._islatest;
            }
            set
            {
                this._islatest = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="rcptinfolist", DataFormat=DataFormat.Default)]
        public RcptInfoList rcptinfolist
        {
            get
            {
                return this._rcptinfolist;
            }
            set
            {
                this._rcptinfolist = value;
            }
        }
    }
}

