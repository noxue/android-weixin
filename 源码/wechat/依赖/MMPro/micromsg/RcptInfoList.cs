namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="RcptInfoList")]
    public class RcptInfoList : IExtensible
    {
        private uint _count;
        private readonly List<RcptInfoNode> _rcptinfolist = new List<RcptInfoNode>();
        private uint _timestamp;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="count", DataFormat=DataFormat.TwosComplement)]
        public uint count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }

        [ProtoMember(2, Name="rcptinfolist", DataFormat=DataFormat.Default)]
        public List<RcptInfoNode> rcptinfolist
        {
            get
            {
                return this._rcptinfolist;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="timestamp", DataFormat=DataFormat.TwosComplement)]
        public uint timestamp
        {
            get
            {
                return this._timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }
    }
}

