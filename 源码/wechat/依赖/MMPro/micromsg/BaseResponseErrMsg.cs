namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BaseResponseErrMsg")]
    public class BaseResponseErrMsg : IExtensible
    {
        private int _Action;
        private string _Cancel = "";
        private string _Content = "";
        private uint _Countdown = 0;
        private int _DelayConnSec = 0;
        private int _DispSec;
        private string _Ok = "";
        private int _ShowType;
        private string _Title = "";
        private string _Url = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=true, Name="Action", DataFormat=DataFormat.TwosComplement)]
        public int Action
        {
            get
            {
                return this._Action;
            }
            set
            {
                this._Action = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Cancel", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Cancel
        {
            get
            {
                return this._Cancel;
            }
            set
            {
                this._Cancel = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=false, Name="Countdown", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Countdown
        {
            get
            {
                return this._Countdown;
            }
            set
            {
                this._Countdown = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="DelayConnSec", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int DelayConnSec
        {
            get
            {
                return this._DelayConnSec;
            }
            set
            {
                this._DelayConnSec = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="DispSec", DataFormat=DataFormat.TwosComplement)]
        public int DispSec
        {
            get
            {
                return this._DispSec;
            }
            set
            {
                this._DispSec = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Ok", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Ok
        {
            get
            {
                return this._Ok;
            }
            set
            {
                this._Ok = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ShowType", DataFormat=DataFormat.TwosComplement)]
        public int ShowType
        {
            get
            {
                return this._ShowType;
            }
            set
            {
                this._ShowType = value;
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

        [ProtoMember(3, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

