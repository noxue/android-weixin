namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BackupRequestSession")]
    public class BackupRequestSession : IExtensible
    {
        private readonly List<string> _SessionName = new List<string>();
        private readonly List<long> _TimeInterval = new List<long>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, Name="SessionName", DataFormat=DataFormat.Default)]
        public List<string> SessionName
        {
            get
            {
                return this._SessionName;
            }
        }

        [ProtoMember(2, Name="TimeInterval", DataFormat=DataFormat.TwosComplement)]
        public List<long> TimeInterval
        {
            get
            {
                return this._TimeInterval;
            }
        }
    }
}

