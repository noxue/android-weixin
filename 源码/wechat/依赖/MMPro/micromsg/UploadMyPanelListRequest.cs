namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="UploadMyPanelListRequest")]
    public class UploadMyPanelListRequest : IExtensible
    {
        private uint _OpCode;
        private readonly List<string> _ProductIDList = new List<string>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(2, Name="ProductIDList", DataFormat=DataFormat.Default)]
        public List<string> ProductIDList
        {
            get
            {
                return this._ProductIDList;
            }
        }
    }
}

