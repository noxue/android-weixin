﻿namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="CheckCDNResponse")]
    public class CheckCDNResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private readonly List<FavCDNItem> _List = new List<FavCDNItem>();
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

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<FavCDNItem> List
        {
            get
            {
                return this._List;
            }
        }
    }
}

