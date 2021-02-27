namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="ModFavItemRequest")]
    public class ModFavItemRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _FavId;
        private uint _IndexCount;
        private readonly List<ModFavIndex> _IndexList = new List<ModFavIndex>();
        private uint _ObjectCount;
        private readonly List<ModFavObject> _ObjectList = new List<ModFavObject>();
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

        [ProtoMember(3, IsRequired=true, Name="IndexCount", DataFormat=DataFormat.TwosComplement)]
        public uint IndexCount
        {
            get
            {
                return this._IndexCount;
            }
            set
            {
                this._IndexCount = value;
            }
        }

        [ProtoMember(4, Name="IndexList", DataFormat=DataFormat.Default)]
        public List<ModFavIndex> IndexList
        {
            get
            {
                return this._IndexList;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ObjectCount", DataFormat=DataFormat.TwosComplement)]
        public uint ObjectCount
        {
            get
            {
                return this._ObjectCount;
            }
            set
            {
                this._ObjectCount = value;
            }
        }

        [ProtoMember(6, Name="ObjectList", DataFormat=DataFormat.Default)]
        public List<ModFavObject> ObjectList
        {
            get
            {
                return this._ObjectList;
            }
        }
    }
}

