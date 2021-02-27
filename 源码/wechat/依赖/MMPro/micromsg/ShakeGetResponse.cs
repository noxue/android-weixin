namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeGetResponse")]
    public class ShakeGetResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private int _Ret;
        private uint _Scence;
        private readonly List<ShakeGetItem> _ShakeGetList = new List<ShakeGetItem>();
        private string _Tips = "";
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

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Scence", DataFormat=DataFormat.TwosComplement)]
        public uint Scence
        {
            get
            {
                return this._Scence;
            }
            set
            {
                this._Scence = value;
            }
        }

        [ProtoMember(3, Name="ShakeGetList", DataFormat=DataFormat.Default)]
        public List<ShakeGetItem> ShakeGetList
        {
            get
            {
                return this._ShakeGetList;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Tips", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

