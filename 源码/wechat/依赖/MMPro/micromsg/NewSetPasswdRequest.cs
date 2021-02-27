namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewSetPasswdRequest")]
    public class NewSetPasswdRequest : IExtensible
    {
        private SKBuiltinBuffer_t _AutoAuthKey = null;
        private micromsg.BaseRequest _BaseRequest;
        private string _Password = "";
        private string _Ticket = "";
        private uint _TicketType = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="AutoAuthKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t AutoAuthKey
        {
            get
            {
                return this._AutoAuthKey;
            }
            set
            {
                this._AutoAuthKey = value;
            }
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

        [ProtoMember(2, IsRequired=false, Name="Password", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Ticket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                this._Ticket = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="TicketType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TicketType
        {
            get
            {
                return this._TicketType;
            }
            set
            {
                this._TicketType = value;
            }
        }
    }
}

