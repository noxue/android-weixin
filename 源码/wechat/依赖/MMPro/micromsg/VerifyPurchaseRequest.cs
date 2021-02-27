namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VerifyPurchaseRequest")]
    public class VerifyPurchaseRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _BillNo = "";
        private string _CurrencyType = "";
        private uint _PayTime;
        private uint _PayType;
        private string _Price = "";
        private string _ProductID = "";
        private uint _Quantity = 0;
        private SKBuiltinBuffer_t _Receipt;
        private string _ReceiptSig = "";
        private uint _VerifyType;
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

        [ProtoMember(8, IsRequired=false, Name="BillNo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                this._BillNo = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="CurrencyType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CurrencyType
        {
            get
            {
                return this._CurrencyType;
            }
            set
            {
                this._CurrencyType = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="PayTime", DataFormat=DataFormat.TwosComplement)]
        public uint PayTime
        {
            get
            {
                return this._PayTime;
            }
            set
            {
                this._PayTime = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="PayType", DataFormat=DataFormat.TwosComplement)]
        public uint PayType
        {
            get
            {
                return this._PayType;
            }
            set
            {
                this._PayType = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Price", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ProductID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this._ProductID = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="Quantity", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Receipt", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Receipt
        {
            get
            {
                return this._Receipt;
            }
            set
            {
                this._Receipt = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="ReceiptSig", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ReceiptSig
        {
            get
            {
                return this._ReceiptSig;
            }
            set
            {
                this._ReceiptSig = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="VerifyType", DataFormat=DataFormat.TwosComplement)]
        public uint VerifyType
        {
            get
            {
                return this._VerifyType;
            }
            set
            {
                this._VerifyType = value;
            }
        }
    }
}

