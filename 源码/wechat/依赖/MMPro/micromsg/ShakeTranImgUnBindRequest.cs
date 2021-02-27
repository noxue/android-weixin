namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeTranImgUnBindRequest")]
    public class ShakeTranImgUnBindRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _OpType = 0;
        private uint _Scene;
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

        [ProtoMember(3, IsRequired=false, Name="OpType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint OpType
        {
            get
            {
                return this._OpType;
            }
            set
            {
                this._OpType = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }
    }
}

