namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetA8KeyResp")]
    public class GetA8KeyResp : IExtensible
    {
        private string _A8Key = "";
        private uint _ActionCode = 0;
        private string _AntispamTicket = "";
        private micromsg.BaseResponse _BaseResponse;
        private string _Content = "";
        private micromsg.DeepLinkBitSet _DeepLinkBitSet = null;
        private string _FullURL = "";
        private micromsg.GeneralControlBitSet _GeneralControlBitSet = null;
        private readonly List<HTTPHeader> _HttpHeader = new List<HTTPHeader>();
        private int _HttpHeaderNumb = 0;
        private SKBuiltinBuffer_t _JSAPIControlBytes = null;
        private JSAPIPermissionBitSet _JSAPIPermission = null;
        private string _MID = "";
        private uint _ScopeCount = 0;
        private readonly List<BizScopeInfo> _ScopeList = new List<BizScopeInfo>();
        private string _ShareURL = "";
        private string _SSID = "";
        private string _Title = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="A8Key", DataFormat=DataFormat.Default), DefaultValue("")]
        public string A8Key
        {
            get
            {
                return this._A8Key;
            }
            set
            {
                this._A8Key = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ActionCode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ActionCode
        {
            get
            {
                return this._ActionCode;
            }
            set
            {
                this._ActionCode = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="AntispamTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AntispamTicket
        {
            get
            {
                return this._AntispamTicket;
            }
            set
            {
                this._AntispamTicket = value;
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

        [ProtoMember(6, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="DeepLinkBitSet", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.DeepLinkBitSet DeepLinkBitSet
        {
            get
            {
                return this._DeepLinkBitSet;
            }
            set
            {
                this._DeepLinkBitSet = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FullURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FullURL
        {
            get
            {
                return this._FullURL;
            }
            set
            {
                this._FullURL = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="GeneralControlBitSet", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.GeneralControlBitSet GeneralControlBitSet
        {
            get
            {
                return this._GeneralControlBitSet;
            }
            set
            {
                this._GeneralControlBitSet = value;
            }
        }

        [ProtoMember(0x19, Name="HttpHeader", DataFormat=DataFormat.Default)]
        public List<HTTPHeader> HttpHeader
        {
            get
            {
                return this._HttpHeader;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="HttpHeaderNumb", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int HttpHeaderNumb
        {
            get
            {
                return this._HttpHeaderNumb;
            }
            set
            {
                this._HttpHeaderNumb = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="JSAPIControlBytes", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t JSAPIControlBytes
        {
            get
            {
                return this._JSAPIControlBytes;
            }
            set
            {
                this._JSAPIControlBytes = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="JSAPIPermission", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public JSAPIPermissionBitSet JSAPIPermission
        {
            get
            {
                return this._JSAPIPermission;
            }
            set
            {
                this._JSAPIPermission = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="MID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MID
        {
            get
            {
                return this._MID;
            }
            set
            {
                this._MID = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="ScopeCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ScopeCount
        {
            get
            {
                return this._ScopeCount;
            }
            set
            {
                this._ScopeCount = value;
            }
        }

        [ProtoMember(0x11, Name="ScopeList", DataFormat=DataFormat.Default)]
        public List<BizScopeInfo> ScopeList
        {
            get
            {
                return this._ScopeList;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="ShareURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ShareURL
        {
            get
            {
                return this._ShareURL;
            }
            set
            {
                this._ShareURL = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="SSID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SSID
        {
            get
            {
                return this._SSID;
            }
            set
            {
                this._SSID = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Title", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

