namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetTVTopicCommentResponse")]
    public class GetTVTopicCommentResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private uint _LastCommentId;
        private uint _LeftCommentCount;
        private readonly List<TVTopicCommentItem> _List = new List<TVTopicCommentItem>();
        private uint _TotalCommentCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="LastCommentId", DataFormat=DataFormat.TwosComplement)]
        public uint LastCommentId
        {
            get
            {
                return this._LastCommentId;
            }
            set
            {
                this._LastCommentId = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="LeftCommentCount", DataFormat=DataFormat.TwosComplement)]
        public uint LeftCommentCount
        {
            get
            {
                return this._LeftCommentCount;
            }
            set
            {
                this._LeftCommentCount = value;
            }
        }

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<TVTopicCommentItem> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="TotalCommentCount", DataFormat=DataFormat.TwosComplement)]
        public uint TotalCommentCount
        {
            get
            {
                return this._TotalCommentCount;
            }
            set
            {
                this._TotalCommentCount = value;
            }
        }
    }
}

