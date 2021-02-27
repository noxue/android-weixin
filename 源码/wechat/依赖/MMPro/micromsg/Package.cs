namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Package")]
    public class Package : IExtensible
    {
        private SKBuiltinBuffer_t _Ext = null;
        private int _Id;
        private string _Md5 = "";
        private string _Name = "";
        private string _PackName = "";
        private uint _Size = 0;
        private SKBuiltinBuffer_t _Thumb = null;
        private int _Version;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=false, Name="Ext", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Ext
        {
            get
            {
                return this._Ext;
            }
            set
            {
                this._Ext = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Md5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Md5
        {
            get
            {
                return this._Md5;
            }
            set
            {
                this._Md5 = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="PackName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackName
        {
            get
            {
                return this._PackName;
            }
            set
            {
                this._PackName = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Size", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Size
        {
            get
            {
                return this._Size;
            }
            set
            {
                this._Size = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Thumb", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Thumb
        {
            get
            {
                return this._Thumb;
            }
            set
            {
                this._Thumb = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Version", DataFormat=DataFormat.TwosComplement)]
        public int Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }
    }
}

