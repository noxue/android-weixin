namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="PluginInstall")]
    public class PluginInstall : IExtensible
    {
        private uint _IsUnInstall;
        private uint _PluginFlag;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="IsUnInstall", DataFormat=DataFormat.TwosComplement)]
        public uint IsUnInstall
        {
            get
            {
                return this._IsUnInstall;
            }
            set
            {
                this._IsUnInstall = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="PluginFlag", DataFormat=DataFormat.TwosComplement)]
        public uint PluginFlag
        {
            get
            {
                return this._PluginFlag;
            }
            set
            {
                this._PluginFlag = value;
            }
        }
    }
}

