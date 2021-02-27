namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsActionGroup")]
    public class SnsActionGroup : IExtensible
    {
        private string _ClientId = "";
        private SnsAction _CurrentAction;
        private ulong _Id;
        private ulong _ParentId = 0L;
        private SnsAction _ReferAction = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="ClientId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="CurrentAction", DataFormat=DataFormat.Default)]
        public SnsAction CurrentAction
        {
            get
            {
                return this._CurrentAction;
            }
            set
            {
                this._CurrentAction = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public ulong Id
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

        [ProtoMember(2, IsRequired=false, Name="ParentId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this._ParentId = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ReferAction", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SnsAction ReferAction
        {
            get
            {
                return this._ReferAction;
            }
            set
            {
                this._ReferAction = value;
            }
        }
    }
}

