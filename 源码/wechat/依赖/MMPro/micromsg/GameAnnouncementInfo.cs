namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GameAnnouncementInfo")]
    public class GameAnnouncementInfo : IExtensible
    {
        private string _Desc = "";
        private uint _Style;
        private string _Title = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Desc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Desc
        {
            get
            {
                return this._Desc;
            }
            set
            {
                this._Desc = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Style", DataFormat=DataFormat.TwosComplement)]
        public uint Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                this._Style = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Title", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

