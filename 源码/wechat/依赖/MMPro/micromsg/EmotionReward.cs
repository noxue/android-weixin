namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionReward")]
    public class EmotionReward : IExtensible
    {
        private string _BegPicUrl;
        private string _BegWord;
        private uint _MagicExpire = 0;
        private uint _MagicLimit = 0;
        private string _MagicMd5 = "";
        private string _MagicUrl = "";
        private string _MagicWord = "";
        private string _ThanksPicUrl;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="BegPicUrl", DataFormat=DataFormat.Default)]
        public string BegPicUrl
        {
            get
            {
                return this._BegPicUrl;
            }
            set
            {
                this._BegPicUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BegWord", DataFormat=DataFormat.Default)]
        public string BegWord
        {
            get
            {
                return this._BegWord;
            }
            set
            {
                this._BegWord = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="MagicExpire", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MagicExpire
        {
            get
            {
                return this._MagicExpire;
            }
            set
            {
                this._MagicExpire = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="MagicLimit", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MagicLimit
        {
            get
            {
                return this._MagicLimit;
            }
            set
            {
                this._MagicLimit = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="MagicMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MagicMd5
        {
            get
            {
                return this._MagicMd5;
            }
            set
            {
                this._MagicMd5 = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="MagicUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MagicUrl
        {
            get
            {
                return this._MagicUrl;
            }
            set
            {
                this._MagicUrl = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="MagicWord", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MagicWord
        {
            get
            {
                return this._MagicWord;
            }
            set
            {
                this._MagicWord = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ThanksPicUrl", DataFormat=DataFormat.Default)]
        public string ThanksPicUrl
        {
            get
            {
                return this._ThanksPicUrl;
            }
            set
            {
                this._ThanksPicUrl = value;
            }
        }
    }
}

