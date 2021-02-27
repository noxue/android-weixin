namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ScanStreetViewRequest")]
    public class ScanStreetViewRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private float _Heading = 0f;
        private float _Pitch = 0f;
        private uint _Scene = 0;
        private PositionInfo _UserPos;
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

        [ProtoMember(4, IsRequired=false, Name="Heading", DataFormat=DataFormat.FixedSize), DefaultValue((float) 0f)]
        public float Heading
        {
            get
            {
                return this._Heading;
            }
            set
            {
                this._Heading = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Pitch", DataFormat=DataFormat.FixedSize), DefaultValue((float) 0f)]
        public float Pitch
        {
            get
            {
                return this._Pitch;
            }
            set
            {
                this._Pitch = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(2, IsRequired=true, Name="UserPos", DataFormat=DataFormat.Default)]
        public PositionInfo UserPos
        {
            get
            {
                return this._UserPos;
            }
            set
            {
                this._UserPos = value;
            }
        }
    }
}

