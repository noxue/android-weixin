namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AcceptCardListFromAppRequest")]
    public class AcceptCardListFromAppRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<CardListItem> _card_list = new List<CardListItem>();
        private uint _from_scene = 0;
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

        [ProtoMember(2, Name="card_list", DataFormat=DataFormat.Default)]
        public List<CardListItem> card_list
        {
            get
            {
                return this._card_list;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="from_scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint from_scene
        {
            get
            {
                return this._from_scene;
            }
            set
            {
                this._from_scene = value;
            }
        }
    }
}

