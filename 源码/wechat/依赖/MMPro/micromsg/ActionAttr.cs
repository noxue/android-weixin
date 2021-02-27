namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ActionAttr")]
    public class ActionAttr : IExtensible
    {
        private string _Content = "";
        private string _IconUrl = "";
        private string _Name = "";
        private string _Tips = "";
        private uint _Type = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=false, Name="IconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrl
        {
            get
            {
                return this._IconUrl;
            }
            set
            {
                this._IconUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="Tips", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Tips
        {
            get
            {
                return this._Tips;
            }
            set
            {
                this._Tips = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

