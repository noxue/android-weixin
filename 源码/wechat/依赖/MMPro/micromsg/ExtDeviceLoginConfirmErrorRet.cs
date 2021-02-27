namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExtDeviceLoginConfirmErrorRet")]
    public class ExtDeviceLoginConfirmErrorRet : IExtensible
    {
        private string _ButtonStr = "";
        private string _ContentStr = "";
        private uint _IconType = 0;
        private string _TitleStr = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="ButtonStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ButtonStr
        {
            get
            {
                return this._ButtonStr;
            }
            set
            {
                this._ButtonStr = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ContentStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ContentStr
        {
            get
            {
                return this._ContentStr;
            }
            set
            {
                this._ContentStr = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="IconType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(3, IsRequired=false, Name="TitleStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TitleStr
        {
            get
            {
                return this._TitleStr;
            }
            set
            {
                this._TitleStr = value;
            }
        }
    }
}

