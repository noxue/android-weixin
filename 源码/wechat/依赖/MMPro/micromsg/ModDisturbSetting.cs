namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ModDisturbSetting")]
    public class ModDisturbSetting : IExtensible
    {
        private micromsg.DisturbSetting _DisturbSetting;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="DisturbSetting", DataFormat=DataFormat.Default)]
        public micromsg.DisturbSetting DisturbSetting
        {
            get
            {
                return this._DisturbSetting;
            }
            set
            {
                this._DisturbSetting = value;
            }
        }
    }
}

