namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SnsTimeLineWithTypeResponse")]
    public class SnsTimeLineWithTypeResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ObjectCount;
        private readonly List<SnsObject> _ObjectList = new List<SnsObject>();
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

        [ProtoMember(2, IsRequired=true, Name="ObjectCount", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, Name="ObjectList", DataFormat=DataFormat.Default)]
        public List<SnsObject> ObjectList
        {
            get
            {
                return this._ObjectList;
            }
        }
    }
}

