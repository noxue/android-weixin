namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="CloseMicroBlog")]
    public class CloseMicroBlog : IExtensible
    {
        private SKBuiltinString_t _MicroBlogUserName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="MicroBlogUserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t MicroBlogUserName
        {
            get
            {
                return this._MicroBlogUserName;
            }
            set
            {
                this._MicroBlogUserName = value;
            }
        }
    }
}

