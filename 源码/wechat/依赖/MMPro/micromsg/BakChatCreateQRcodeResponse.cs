namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BakChatCreateQRcodeResponse")]
    public class BakChatCreateQRcodeResponse : IExtensible
    {
        private uint _EncryFlag;
        private string _Hello = "";
        private string _ID = "";
        private SKBuiltinBuffer_t _Key;
        private string _OK = "";
        private SKBuiltinBuffer_t _QRCodeBuffer;
        private string _QRCodeUrl = "";
        private uint _Ret;
        private string _Tickit = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=true, Name="EncryFlag", DataFormat=DataFormat.TwosComplement)]
        public uint EncryFlag
        {
            get
            {
                return this._EncryFlag;
            }
            set
            {
                this._EncryFlag = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Hello", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Hello
        {
            get
            {
                return this._Hello;
            }
            set
            {
                this._Hello = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="ID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Key", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="OK", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OK
        {
            get
            {
                return this._OK;
            }
            set
            {
                this._OK = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="QRCodeBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t QRCodeBuffer
        {
            get
            {
                return this._QRCodeBuffer;
            }
            set
            {
                this._QRCodeBuffer = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="QRCodeUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QRCodeUrl
        {
            get
            {
                return this._QRCodeUrl;
            }
            set
            {
                this._QRCodeUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public uint Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Tickit", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Tickit
        {
            get
            {
                return this._Tickit;
            }
            set
            {
                this._Tickit = value;
            }
        }
    }
}

