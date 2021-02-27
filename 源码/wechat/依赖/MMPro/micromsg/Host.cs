namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Host")]
    public class Host : IExtensible
    {
        private string _Origin = "";
        private int _Priority = 0;
        private string _Substitute = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="Origin", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Origin
        {
            get
            {
                return this._Origin;
            }
            set
            {
                this._Origin = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Priority", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Priority
        {
            get
            {
                return this._Priority;
            }
            set
            {
                this._Priority = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Substitute", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Substitute
        {
            get
            {
                return this._Substitute;
            }
            set
            {
                this._Substitute = value;
            }
        }
    }
}

