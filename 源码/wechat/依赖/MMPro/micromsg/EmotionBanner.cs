namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="EmotionBanner")]
    public class EmotionBanner : IExtensible
    {
        private EmotionBannerImg _BannerImg;
        private EmotionSummary _BannerSummary;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="BannerImg", DataFormat=DataFormat.Default)]
        public EmotionBannerImg BannerImg
        {
            get
            {
                return this._BannerImg;
            }
            set
            {
                this._BannerImg = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BannerSummary", DataFormat=DataFormat.Default)]
        public EmotionSummary BannerSummary
        {
            get
            {
                return this._BannerSummary;
            }
            set
            {
                this._BannerSummary = value;
            }
        }
    }
}

