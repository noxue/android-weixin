namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsTagMemberOptionRequest")]
    public class SnsTagMemberOptionRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Count;
        private readonly List<SKBuiltinString_t> _List = new List<SKBuiltinString_t>();
        private uint _OpCode;
        private uint _Scene = 0;
        private ulong _TagId;
        private string _TagName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, Name="List", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TagId", DataFormat=DataFormat.TwosComplement)]
        public ulong TagId
        {
            get
            {
                return this._TagId;
            }
            set
            {
                this._TagId = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="TagName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagName
        {
            get
            {
                return this._TagName;
            }
            set
            {
                this._TagName = value;
            }
        }
    }
}

