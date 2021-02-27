namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="PositionItem")]
    public class PositionItem : IExtensible
    {
        private double _Heading;
        private double _Latitude;
        private double _Longitude;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="Heading", DataFormat=DataFormat.TwosComplement)]
        public double Heading
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

        [ProtoMember(1, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
        public double Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                this._Latitude = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Longitude", DataFormat=DataFormat.TwosComplement)]
        public double Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                this._Longitude = value;
            }
        }
    }
}

