namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLastestExpressInfoResponse")]
    public class GetLastestExpressInfoResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ExpressCount = 0;
        private readonly List<Express> _ExpressList = new List<Express>();
        private int _RetCode = 0;
        private string _RetMsg = "";
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

        [ProtoMember(3, IsRequired=false, Name="ExpressCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ExpressCount
        {
            get
            {
                return this._ExpressCount;
            }
            set
            {
                this._ExpressCount = value;
            }
        }

        [ProtoMember(2, Name="ExpressList", DataFormat=DataFormat.Default)]
        public List<Express> ExpressList
        {
            get
            {
                return this._ExpressList;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="RetCode", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int RetCode
        {
            get
            {
                return this._RetCode;
            }
            set
            {
                this._RetCode = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="RetMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RetMsg
        {
            get
            {
                return this._RetMsg;
            }
            set
            {
                this._RetMsg = value;
            }
        }
    }
}

