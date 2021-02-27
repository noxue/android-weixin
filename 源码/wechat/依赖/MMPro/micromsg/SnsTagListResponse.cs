namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsTagListResponse")]
    public class SnsTagListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private readonly List<SnsTag> _List = new List<SnsTag>();
        private uint _OpCode;
        private string _TagListMd5 = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, Name="List", DataFormat=DataFormat.Default)]
        public List<SnsTag> List
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

        [ProtoMember(3, IsRequired=false, Name="TagListMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagListMd5
        {
            get
            {
                return this._TagListMd5;
            }
            set
            {
                this._TagListMd5 = value;
            }
        }
    }
}

