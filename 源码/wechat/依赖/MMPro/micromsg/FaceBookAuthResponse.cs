namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FaceBookAuthResponse")]
    public class FaceBookAuthResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private ulong _FBUserID;
        private string _FBUserName = "";
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

        [ProtoMember(2, IsRequired=true, Name="FBUserID", DataFormat=DataFormat.TwosComplement)]
        public ulong FBUserID
        {
            get
            {
                return this._FBUserID;
            }
            set
            {
                this._FBUserID = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="FBUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FBUserName
        {
            get
            {
                return this._FBUserName;
            }
            set
            {
                this._FBUserName = value;
            }
        }
    }
}

