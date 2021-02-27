namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsUserInfo")]
    public class SnsUserInfo : IExtensible
    {
        private string _SnsBGImgID = "";
        private ulong _SnsBGObjectID = 0L;
        private uint _SnsFlag;
        private uint _SnsFlagEx = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="SnsBGImgID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SnsBGImgID
        {
            get
            {
                return this._SnsBGImgID;
            }
            set
            {
                this._SnsBGImgID = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="SnsBGObjectID", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong SnsBGObjectID
        {
            get
            {
                return this._SnsBGObjectID;
            }
            set
            {
                this._SnsBGObjectID = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="SnsFlag", DataFormat=DataFormat.TwosComplement)]
        public uint SnsFlag
        {
            get
            {
                return this._SnsFlag;
            }
            set
            {
                this._SnsFlag = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="SnsFlagEx", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SnsFlagEx
        {
            get
            {
                return this._SnsFlagEx;
            }
            set
            {
                this._SnsFlagEx = value;
            }
        }
    }
}

