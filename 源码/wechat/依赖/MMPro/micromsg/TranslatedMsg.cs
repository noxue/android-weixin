namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="TranslatedMsg")]
    public class TranslatedMsg : IExtensible
    {
        private string _BrandWording = "";
        private uint _ClientMsgID;
        private int _Ret;
        private string _TranslatedText = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="BrandWording", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BrandWording
        {
            get
            {
                return this._BrandWording;
            }
            set
            {
                this._BrandWording = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ClientMsgID", DataFormat=DataFormat.TwosComplement)]
        public uint ClientMsgID
        {
            get
            {
                return this._ClientMsgID;
            }
            set
            {
                this._ClientMsgID = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
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

        [ProtoMember(2, IsRequired=false, Name="TranslatedText", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TranslatedText
        {
            get
            {
                return this._TranslatedText;
            }
            set
            {
                this._TranslatedText = value;
            }
        }
    }
}

