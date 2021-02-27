namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetEmotionDescResponse")]
    public class GetEmotionDescResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _ButtonDesc = "";
        private uint _ClickFlag;
        private uint _Count;
        private uint _DownLoadFlag = 0;
        private readonly List<EmotionDesc> _List = new List<EmotionDesc>();
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

        [ProtoMember(5, IsRequired=false, Name="ButtonDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ButtonDesc
        {
            get
            {
                return this._ButtonDesc;
            }
            set
            {
                this._ButtonDesc = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ClickFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ClickFlag
        {
            get
            {
                return this._ClickFlag;
            }
            set
            {
                this._ClickFlag = value;
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

        [ProtoMember(6, IsRequired=false, Name="DownLoadFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DownLoadFlag
        {
            get
            {
                return this._DownLoadFlag;
            }
            set
            {
                this._DownLoadFlag = value;
            }
        }

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<EmotionDesc> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

