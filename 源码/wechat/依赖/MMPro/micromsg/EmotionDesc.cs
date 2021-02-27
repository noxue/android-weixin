namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionDesc")]
    public class EmotionDesc : IExtensible
    {
        private uint _Count;
        private readonly List<LangDesc> _List = new List<LangDesc>();
        private string _Md5 = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<LangDesc> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Md5", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

