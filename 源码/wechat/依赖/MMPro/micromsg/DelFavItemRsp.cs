namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="DelFavItemRsp")]
    public class DelFavItemRsp : IExtensible
    {
        private uint _FavId;
        private int _Ret;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="FavId", DataFormat=DataFormat.TwosComplement)]
        public uint FavId
        {
            get
            {
                return this._FavId;
            }
            set
            {
                this._FavId = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
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
    }
}

