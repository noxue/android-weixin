namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UnifyAuthResponse")]
    public class UnifyAuthResponse : IExtensible
    {
        private micromsg.AcctSectResp _AcctSectResp = null;
        private micromsg.AuthSectResp _AuthSectResp = null;
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.NetworkSectResp _NetworkSectResp = null;
        private uint _UnifyAuthSectFlag = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="AcctSectResp", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.AcctSectResp AcctSectResp
        {
            get
            {
                return this._AcctSectResp;
            }
            set
            {
                this._AcctSectResp = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="AuthSectResp", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.AuthSectResp AuthSectResp
        {
            get
            {
                return this._AuthSectResp;
            }
            set
            {
                this._AuthSectResp = value;
            }
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

        [ProtoMember(5, IsRequired=false, Name="NetworkSectResp", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.NetworkSectResp NetworkSectResp
        {
            get
            {
                return this._NetworkSectResp;
            }
            set
            {
                this._NetworkSectResp = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UnifyAuthSectFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint UnifyAuthSectFlag
        {
            get
            {
                return this._UnifyAuthSectFlag;
            }
            set
            {
                this._UnifyAuthSectFlag = value;
            }
        }
    }
}

