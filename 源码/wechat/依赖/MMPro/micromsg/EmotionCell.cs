namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionCell")]
    public class EmotionCell : IExtensible
    {
        private string _CellTitle = "";
        private string _IconUrl = "";
        private uint _Position;
        private uint _ReqType;
        private string _TagUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="CellTitle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CellTitle
        {
            get
            {
                return this._CellTitle;
            }
            set
            {
                this._CellTitle = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="IconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=true, Name="Position", DataFormat=DataFormat.TwosComplement)]
        public uint Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                this._Position = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ReqType", DataFormat=DataFormat.TwosComplement)]
        public uint ReqType
        {
            get
            {
                return this._ReqType;
            }
            set
            {
                this._ReqType = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="TagUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagUrl
        {
            get
            {
                return this._TagUrl;
            }
            set
            {
                this._TagUrl = value;
            }
        }
    }
}

