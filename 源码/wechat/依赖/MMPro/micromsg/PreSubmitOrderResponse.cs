namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PreSubmitOrderResponse")]
    public class PreSubmitOrderResponse : IExtensible
    {
        private uint _ActionAttrCount = 0;
        private readonly List<ActionAttr> _ActionAttrs = new List<ActionAttr>();
        private micromsg.BaseResponse _BaseResponse = null;
        private readonly List<micromsg.Express> _Express = new List<micromsg.Express>();
        private uint _ExpressCount = 0;
        private string _LockId = "";
        private int _RetCode = 0;
        private string _RetMsg = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(8, IsRequired=false, Name="ActionAttrCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ActionAttrCount
        {
            get
            {
                return this._ActionAttrCount;
            }
            set
            {
                this._ActionAttrCount = value;
            }
        }

        [ProtoMember(7, Name="ActionAttrs", DataFormat=DataFormat.Default)]
        public List<ActionAttr> ActionAttrs
        {
            get
            {
                return this._ActionAttrs;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="BaseResponse", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(3, Name="Express", DataFormat=DataFormat.Default)]
        public List<micromsg.Express> Express
        {
            get
            {
                return this._Express;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ExpressCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(4, IsRequired=false, Name="LockId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LockId
        {
            get
            {
                return this._LockId;
            }
            set
            {
                this._LockId = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="RetCode", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(6, IsRequired=false, Name="RetMsg", DataFormat=DataFormat.Default), DefaultValue("")]
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

