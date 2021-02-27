namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetPersonalDesignerResponse")]
    public class GetPersonalDesignerResponse : IExtensible
    {
        private string _BannerUrl;
        private micromsg.BaseResponse _BaseResponse;
        private string _BizName;
        private string _Desc;
        private readonly List<EmotionSummary> _EmotionList = new List<EmotionSummary>();
        private string _HeadUrl;
        private string _Name;
        private SKBuiltinBuffer_t _ReqBuf;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="BannerUrl", DataFormat=DataFormat.Default)]
        public string BannerUrl
        {
            get
            {
                return this._BannerUrl;
            }
            set
            {
                this._BannerUrl = value;
            }
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

        [ProtoMember(6, IsRequired=true, Name="BizName", DataFormat=DataFormat.Default)]
        public string BizName
        {
            get
            {
                return this._BizName;
            }
            set
            {
                this._BizName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Desc", DataFormat=DataFormat.Default)]
        public string Desc
        {
            get
            {
                return this._Desc;
            }
            set
            {
                this._Desc = value;
            }
        }

        [ProtoMember(7, Name="EmotionList", DataFormat=DataFormat.Default)]
        public List<EmotionSummary> EmotionList
        {
            get
            {
                return this._EmotionList;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="HeadUrl", DataFormat=DataFormat.Default)]
        public string HeadUrl
        {
            get
            {
                return this._HeadUrl;
            }
            set
            {
                this._HeadUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Name", DataFormat=DataFormat.Default)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="ReqBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ReqBuf
        {
            get
            {
                return this._ReqBuf;
            }
            set
            {
                this._ReqBuf = value;
            }
        }
    }
}

