namespace MicroMsg.Storage
{
    using MicroMsg.Manager;
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using MicroMsg.Common.Utils;
    using MongoDB.Attributes;

    
    public class Contact 
    {
        private byte[] _bytesXmlBuf;
        private uint _nAddContactScene;
        private uint _nChatRoomNotify;
        private uint _nContactType;
        private int _nExtInfoSeq;
        private uint _nFlags;
        private uint _nHasWeiXinHdHeadImg;
        private uint _nImgFlag;
        private int _nImgUpdateSeq;
        private uint _nPersonalCard;
        private int _nRoomInfoCount;
        private int _nSex;
        private uint? _nSource;
        private uint _nVerifyFlag;
        private uint? _pQQ;
        private string _strAlias;
        private string _strBigHeadImgUrl;
        private string _strCity;
        private string _strCountry;
        private string _strDomainList;
        private string _strEmail;
        private string _strExtInfo;
        private string _strHeadImg;
        private string _strMobile;
        private string _strNickName;
        private string _strProvince;
        private string _strPYInitial;
        private string _strQuanPin;
        private string _strRemark;
        private string _strRemarkPYInitial;
        private string _strRemarkQuanPin;
        private string _strSignature;
        private string _strSmallHeadImgUrl;
        private string _strUsrName;
        private string _strVerifyInfo;
        private uint _Uin;
        
        private ContactXmlData _xmlData;
        private const int Field_bytesXmlBuf = 0x40000000;
        private const int Field_nAddContactScene = 0x80000;
        private const int Field_nChatRoomNotify = 0x40000;
        private const int Field_nContactType = 0x8000;
        private const int Field_nExtInfoSeq = 0x100000;
        private const int Field_nFlags = 0x400;
        private const int Field_nHasWeiXinHdHeadImg = 0x8000000;
        private const int Field_nImgFlag = 0x4000;
        private const int Field_nImgUpdateSeq = 0x200000;
        private const int Field_nPersonalCard = 0x4000000;
        private const int Field_nRoomInfoCount = 0x10000;
        private const int Field_nSex = 0x40;
        private const long Field_nSource = 1L;
        private const int Field_nVerifyFlag = 0x10000000;
        private const long Field_pQQ = 2L;
        private const long Field_strAlias = -2147483648L;
        private const long Field_strBigHeadImgUrl = 8L;
        private const int Field_strCity = 0x1000000;
        private const long Field_strCountry = 4L;
        private const int Field_strDomainList = 0x20000;
        private const int Field_strEmail = 0x10;
        private const int Field_strExtInfo = 0x400000;
        private const int Field_strHeadImg = 0x200;
        private const int Field_strMobile = 0x20;
        private const int Field_strNickName = 4;
        private const int Field_strProvince = 0x800000;
        private const int Field_strPYInitial = 0x80;
        private const int Field_strQuanPin = 0x100;
        private const int Field_strRemark = 0x800;
        private const int Field_strRemarkPYInitial = 0x1000;
        private const int Field_strRemarkQuanPin = 0x2000;
        private const int Field_strSignature = 0x2000000;
        private const long Field_strSmallHeadImgUrl = 0x10L;
        private const int Field_strUsrName = 1;
        private const int Field_strVerifyInfo = 0x20000000;
        private const int Field_Uin = 8;
        private MicroMsg.Manager.BrandInfo mBrandInfo;
        private MicroMsg.Manager.ExtInfo mExtInfo;
        private ContactXmlDataSerial serial;

      
        private void load()
        {
            if (this._xmlData == null)
            {
                this.serial = StorageXml.loadFromBuffer<ContactXmlDataSerial>(this.bytesXmlBuf);
                if (this.serial == null)
                {
                    this.serial = new ContactXmlDataSerial();
                    this.serial.data = new ContactXmlData();
                }
                this._xmlData = this.serial.data;
            }
        }
       
        //public override void merge(object o)
        //{
        //    Contact contact = o as Contact;
        //    if (0L != (contact.modify & 1L))
        //    {
        //        this._strUsrName = contact._strUsrName;
        //    }
        //    if (0L != (contact.modify & 4L))
        //    {
        //        this._strNickName = contact._strNickName;
        //    }
        //    if (0L != (contact.modify & 8L))
        //    {
        //        this._Uin = contact._Uin;
        //    }
        //    if (0L != (contact.modify & 0x10L))
        //    {
        //        this._strEmail = contact._strEmail;
        //    }
        //    if (0L != (contact.modify & 0x20L))
        //    {
        //        this._strMobile = contact._strMobile;
        //    }
        //    if (0L != (contact.modify & 0x40L))
        //    {
        //        this._nSex = contact._nSex;
        //    }
        //    if (0L != (contact.modify & 0x80L))
        //    {
        //        this._strPYInitial = contact._strPYInitial;
        //    }
        //    if (0L != (contact.modify & 0x100L))
        //    {
        //        this._strQuanPin = contact._strQuanPin;
        //    }
        //    if (0L != (contact.modify & 0x200L))
        //    {
        //        this._strHeadImg = contact._strHeadImg;
        //    }
        //    if (0L != (contact.modify & 0x400L))
        //    {
        //        this._nFlags = contact._nFlags;
        //    }
        //    if (0L != (contact.modify & 0x2000000L))
        //    {
        //        this._strSignature = contact._strSignature;
        //    }
        //    if (0L != (contact.modify & 0x800L))
        //    {
        //        this._strRemark = contact._strRemark;
        //    }
        //    if (0L != (contact.modify & 0x1000L))
        //    {
        //        this._strRemarkPYInitial = contact._strRemarkPYInitial;
        //    }
        //    if (0L != (contact.modify & 0x2000L))
        //    {
        //        this._strRemarkQuanPin = contact._strRemarkQuanPin;
        //    }
        //    if (0L != (contact.modify & 0x4000L))
        //    {
        //        this._nImgFlag = contact._nImgFlag;
        //    }
        //    if (0L != (contact.modify & 0x8000L))
        //    {
        //        this._nContactType = contact._nContactType;
        //    }
        //    if (0L != (contact.modify & 0x10000L))
        //    {
        //        this._nRoomInfoCount = contact._nRoomInfoCount;
        //    }
        //    if (0L != (contact.modify & 0x20000L))
        //    {
        //        this._strDomainList = contact._strDomainList;
        //    }
        //    if (0L != (contact.modify & 0x40000L))
        //    {
        //        this._nChatRoomNotify = contact._nChatRoomNotify;
        //    }
        //    if (0L != (contact.modify & 0x80000L))
        //    {
        //        this._nAddContactScene = contact._nAddContactScene;
        //    }
        //    if (0L != (contact.modify & 0x100000L))
        //    {
        //        this._nExtInfoSeq = contact._nExtInfoSeq;
        //    }
        //    if (0L != (contact.modify & 0x200000L))
        //    {
        //        this._nImgUpdateSeq = contact._nImgUpdateSeq;
        //    }
        //    if (0L != (contact.modify & 0x400000L))
        //    {
        //        this._strExtInfo = contact._strExtInfo;
        //    }
        //    if (0L != (contact.modify & 0x800000L))
        //    {
        //        this._strProvince = contact.strProvince;
        //    }
        //    if (0L != (contact.modify & 0x1000000L))
        //    {
        //        this._strCity = contact.strCity;
        //    }
        //    if (0L != (contact.modify & 0x2000000L))
        //    {
        //        this._strSignature = contact.strSignature;
        //    }
        //    if (0L != (contact.modify & 0x4000000L))
        //    {
        //        this._nPersonalCard = contact.nPersonalCard;
        //    }
        //    if (0L != (contact.modify & 0x8000000L))
        //    {
        //        this._nHasWeiXinHdHeadImg = contact.nHasWeiXinHdHeadImg;
        //    }
        //    if (0L != (contact.modify & 0x10000000L))
        //    {
        //        this._nVerifyFlag = contact.nVerifyFlag;
        //    }
        //    if (0L != (contact.modify & 0x20000000L))
        //    {
        //        this._strVerifyInfo = contact.strVerifyInfo;
        //    }
        //    if (0L != (contact.modify & -2147483648L))
        //    {
        //        this._strAlias = contact.strAlias;
        //    }
        //    if (0L != (contact.modify & 1L))
        //    {
        //        this._nSource = contact._nSource;
        //    }
        //    if (0L != (contact.modify & 2L))
        //    {
        //        this._pQQ = contact._pQQ;
        //    }
        //    if (0L != (contact.modify & 4L))
        //    {
        //        this._strCountry = contact.strCountry;
        //    }
        //    if (0L != (contact.modify & 8L))
        //    {
        //        this._strBigHeadImgUrl = contact._strBigHeadImgUrl;
        //    }
        //    if (0L != (contact.modify & 0x10L))
        //    {
        //        this._strSmallHeadImgUrl = contact._strSmallHeadImgUrl;
        //    }
        //    if (0L != (contact.modify & 0x40000000L))
        //    {
        //        this._bytesXmlBuf = contact._bytesXmlBuf;
        //        this._xmlData = null;
        //        this.serial = null;
        //    }
        //}
   
        private void unload()
        {
            if (this.serial != null)
            {
                this.bytesXmlBuf = StorageXml.saveToBuffer<ContactXmlDataSerial>(this.serial);
            }
        }
        [MongoIgnore]
        public MicroMsg.Manager.BrandInfo BrandInfo
        {
            get
            {
                this.load();
                if (this.mBrandInfo == null)
                {
                    this.mBrandInfo = ContactHelper.parseBrandInfo(this._xmlData.strBrandInfo);
                    Log.e("Contact", "need parseBrandInfo");
                }
                return this.mBrandInfo;
            }
            set
            {
                this.mBrandInfo = value;
            }
        }

        [MongoIgnore]
        public byte[] bytesXmlBuf
        {
            get
            {
                return this._bytesXmlBuf;
            }
            set
            {
                this._bytesXmlBuf = value;
                //base.modify |= 0x40000000L;
            }
        }
        [MongoIgnore]
        public MicroMsg.Manager.ExtInfo ExtInfo
        {
            get
            {
                this.load();
                if (this.mExtInfo == null)
                {
                    //this.mExtInfo = ContactHelper.parseExtInfo(this._xmlData.strBrandExternalInfo);
                    Log.e("Contact", "need parseExtInfo");
                }
                return this.mExtInfo;
            }
            set
            {
                this.mExtInfo = value;
            }
        }

       [MongoIgnore]
        public uint nAddContactScene
        {
            get
            {
                return this._nAddContactScene;
            }
            set
            {
                this._nAddContactScene = value;
                //base.modify |= 0x80000L;
            }
        }
        [MongoIgnore]
        public uint nBrandFlag
        {
            get
            {
                this.load();
                return this._xmlData.nBrandFlag;
            }
            set
            {
                this.load();
                this._xmlData.nBrandFlag = value;
                this.unload();
            }
        }

       [MongoIgnore]
        public uint nChatRoomNotify
        {
            get
            {
                return this._nChatRoomNotify;
            }
            set
            {
                this._nChatRoomNotify = value;
                //base.modify |= 0x40000L;
            }
        }

       [MongoIgnore]
        public uint nContactType
        {
            get
            {
                return this._nContactType;
            }
            set
            {
                this._nContactType = value;
                //base.modify |= 0x8000L;
            }
        }

       [MongoIgnore]
        public int nExtInfoSeq
        {
            get
            {
                return this._nExtInfoSeq;
            }
            set
            {
                this._nExtInfoSeq = value;
                //base.modify |= 0x100000L;
            }
        }

       [MongoIgnore]
        public uint nFlags
        {
            get
            {
                return this._nFlags;
            }
            set
            {
                this._nFlags = value;
                //base.modify |= 0x400L;
            }
        }
        [MongoIgnore]
        public uint nHasWeiXinHdHeadImg
        {
            get
            {
                return this._nHasWeiXinHdHeadImg;
            }
            set
            {
                this._nHasWeiXinHdHeadImg = value;
                //base.modify |= 0x8000000L;
            }
        }

       [MongoIgnore]
        public uint nImgFlag
        {
            get
            {
                return this._nImgFlag;
            }
            set
            {
                this._nImgFlag = value;
                //base.modify |= 0x4000L;
            }
        }

       [MongoIgnore]
        public int nImgUpdateSeq
        {
            get
            {
                return this._nImgUpdateSeq;
            }
            set
            {
                this._nImgUpdateSeq = value;
                //base.modify |= 0x200000L;
            }
        }
        [MongoIgnore]
        public int nInputType
        {
            get
            {
                this.load();
                return this._xmlData.nInputType;
            }
            set
            {
                this.load();
                this._xmlData.nInputType = value;
                this.unload();
            }
        }

       [MongoIgnore]
        public uint nPersonalCard
        {
            get
            {
                return this._nPersonalCard;
            }
            set
            {
                this._nPersonalCard = value;
                //base.modify |= 0x4000000L;
            }
        }
        [MongoIgnore]
        public uint nQQ
        {
            get
            {
                uint? pQQ = this.pQQ;
                if (!pQQ.HasValue)
                {
                    return 0;
                }
                return pQQ.GetValueOrDefault();
            }
            set
            {
                this.pQQ = new uint?(value);
            }
        }

       
        public int nRoomInfoCount
        {
            get
            {
                return this._nRoomInfoCount;
            }
            set
            {
                this._nRoomInfoCount = value;
                //base.modify |= 0x10000L;
            }
        }

       
        public int nSex
        {
            get
            {
                return this._nSex;
            }
            set
            {
                this._nSex = value;
                //base.modify |= 0x40L;
            }
        }
        [MongoIgnore]
        public ulong nSnsBGObjectID
        {
            get
            {
                this.load();
                return this._xmlData.nSnsBGObjectID;
            }
            set
            {
                this.load();
                this._xmlData.nSnsBGObjectID = value;
                this.unload();
            }
        }
        [MongoIgnore]
        public uint nSnsFlag
        {
            get
            {
                this.load();
                return this._xmlData.nSnsFlag;
            }
            set
            {
                this.load();
                this._xmlData.nSnsFlag = value;
                this.unload();
            }
        }

        [MongoIgnore]
        public uint? nSource
        {
            get
            {
                return this._nSource;
            }
            set
            {
                this._nSource = value;
                //base.modify |= 1L;
            }
        }

        [MongoIgnore]
        public uint nSrc
        {
            get
            {
                uint? nSource = this.nSource;
                if (!nSource.HasValue)
                {
                    return 0;
                }
                return nSource.GetValueOrDefault();
            }
            set
            {
                this.nSource = new uint?(value);
            }
        }
        [MongoIgnore]
        public int nTooMuchMsgNotifyNumber
        {
            get
            {
                this.load();
                return this._xmlData.nTooMuchMsgNotifyNumber;
            }
            set
            {
                this.load();
                this._xmlData.nTooMuchMsgNotifyNumber = value;
                this.unload();
            }
        }
        [MongoIgnore]
        public int nUpdateDay
        {
            get
            {
                this.load();
                return this._xmlData.nUpdateDay;
            }
            set
            {
                this.load();
                this._xmlData.nUpdateDay = value;
                this.unload();
            }
        }

     [MongoIgnore]  
        public uint nVerifyFlag
        {
            get
            {
                return this._nVerifyFlag;
            }
            set
            {
                this._nVerifyFlag = value;
                //base.modify |= 0x10000000L;
            }
        }
        [MongoIgnore]
        public uint nWeiboFlag
        {
            get
            {
                this.load();
                return this._xmlData.nWeiboFlag;
            }
            set
            {
                this.load();
                this._xmlData.nWeiboFlag = value;
                this.unload();
            }
        }

        [MongoIgnore]
        public uint? pQQ
        {
            get
            {
                return this._pQQ;
            }
            set
            {
                this._pQQ = value;
                //base.modify |= 2L;
            }
        }


        public string strAlias
        {
            get
            {
                return this._strAlias;
            }
            set
            {
                this._strAlias = value;
                //base.modify |= -2147483648L;
            }
        }

       
        public string strBigHeadImgUrl
        {
            get
            {
                return this._strBigHeadImgUrl;
            }
            set
            {
                this._strBigHeadImgUrl = value;
                //base.modify |= 8L;
            }
        }
        [MongoIgnore]
        public string strBrandExternalInfo
        {
            get
            {
                this.load();
                return this._xmlData.strBrandExternalInfo;
            }
            set
            {
                this.load();
                this._xmlData.strBrandExternalInfo = value;
                this.unload();
            }
        }
        [MongoIgnore]
        public string strBrandIconURL
        {
            get
            {
                this.load();
                return this._xmlData.strBrandIconURL;
            }
            set
            {
                this.load();
                this._xmlData.strBrandIconURL = value;
                this.unload();
            }
        }
        [MongoIgnore]
        public string strBrandInfo
        {
            get
            {
                this.load();
                return this._xmlData.strBrandInfo;
            }
            set
            {
                this.load();
                this._xmlData.strBrandInfo = value;
                this.unload();
            }
        }

        public string strChatRoomOwner
        {
            get
            {
                this.load();
                return this._xmlData.strChatRoomOwner;
            }
            set
            {
                this.load();
                this._xmlData.strChatRoomOwner = value;
                this.unload();
            }
        }

       
        public string strCity
        {
            get
            {
                return this._strCity;
            }
            set
            {
                this._strCity = value;
                //base.modify |= 0x1000000L;
            }
        }

       
        public string strCountry
        {
            get
            {
                return this._strCountry;
            }
            set
            {
                this._strCountry = value;
                //base.modify |= 4L;
            }
        }

       [MongoIgnore]
        public string strDomainList
        {
            get
            {
                return this._strDomainList;
            }
            set
            {
                this._strDomainList = value;
                //base.modify |= 0x20000L;
            }
        }

       [MongoIgnore]
        public string strEmail
        {
            get
            {
                return this._strEmail;
            }
            set
            {
                this._strEmail = value;
                //base.modify |= 0x10L;
            }
        }

       [MongoIgnore]
        public string strExtInfo
        {
            get
            {
                return this._strExtInfo;
            }
            set
            {
                this._strExtInfo = value;
                //base.modify |= 0x400000L;
            }
        }

       
        public string strHeadImg
        {
            get
            {
                return this._strHeadImg;
            }
            set
            {
                this._strHeadImg = value;
                //base.modify |= 0x200L;
            }
        }
        [MongoIgnore]
        public string strInputText
        {
            get
            {
                this.load();
                return this._xmlData.strInputText;
            }
            set
            {
                this.load();
                this._xmlData.strInputText = value;
                this.unload();
            }
        }

        [MongoIgnore]
        public string strMobile
        {
            get
            {
                return this._strMobile;
            }
            set
            {
                this._strMobile = value;
                //base.modify |= 0x20L;
            }
        }
        [MongoIgnore]
        public string strMyBrandList
        {
            get
            {
                this.load();
                return this._xmlData.strMyBrandList;
            }
            set
            {
                this.load();
                this._xmlData.strMyBrandList = value;
                this.unload();
            }
        }

       
        public string strNickName
        {
            get
            {
                return this._strNickName;
            }
            set
            {
                this._strNickName = value;
                
                //base.modify |= 4L;
            }
        }

        public string strProvince
        {
            get
            {
                return this._strProvince;
            }
            set
            {
                this._strProvince = value;
                //base.modify |= 0x800000L;
            }
        }

       [MongoIgnore]
        public string strPYInitial
        {
            get
            {
                return this._strPYInitial;
            }
            set
            {
                this._strPYInitial = value;
                //base.modify |= 0x80L;
            }
        }

       [MongoIgnore]
        public string strQuanPin
        {
            get
            {
                return this._strQuanPin;
            }
            set
            {
                this._strQuanPin = value;
                //base.modify |= 0x100L;
            }
        }

       [MongoIgnore]
        public string strRemark
        {
            get
            {
                return this._strRemark;
            }
            set
            {
                this._strRemark = value;
                //base.modify |= 0x800L;
            }
        }

       [MongoIgnore]
        public string strRemarkPYInitial
        {
            get
            {
                return this._strRemarkPYInitial;
            }
            set
            {
                this._strRemarkPYInitial = value;
                //base.modify |= 0x1000L;
            }
        }

       [MongoIgnore]
        public string strRemarkQuanPin
        {
            get
            {
                return this._strRemarkQuanPin;
            }
            set
            {
                this._strRemarkQuanPin = value;
                //base.modify |= 0x2000L;
            }
        }

       
        public string strSignature
        {
            get
            {
                return this._strSignature;
            }
            set
            {
                this._strSignature = value;
                //base.modify |= 0x2000000L;
            }
        }

       
        public string strSmallHeadImgUrl
        {
            get
            {
                return this._strSmallHeadImgUrl;
            }
            set
            {
                this._strSmallHeadImgUrl = value;
                //base.modify |= 0x10L;
            }
        }

        public string strSnsBGImgID
        {
            get
            {
                this.load();
                return this._xmlData.strSnsBGImgID;
            }
            set
            {
                this.load();
                this._xmlData.strSnsBGImgID = value;
                this.unload();
            }
        }

        [MongoAlias("_id")]
        public string strUsrName
        {
            get
            {
                return this._strUsrName;
            }
            set
            {
                this._strUsrName = value;
                //base.modify |= 1L;
            }
        }

       [MongoIgnore]
        public string strVerifyInfo
        {
            get
            {
                return this._strVerifyInfo;
            }
            set
            {
                this._strVerifyInfo = value;
                //base.modify |= 0x20000000L;
            }
        }
        [MongoIgnore]
        public string strWeibo
        {
            get
            {
                this.load();
                return this._xmlData.strWeibo;
            }
            set
            {
                this.load();
                this._xmlData.strWeibo = value;
                this.unload();
            }
        }
        [MongoIgnore]
        public string strWeiboNickname
        {
            get
            {
                this.load();
                return this._xmlData.strWeiboNickname;
            }
            set
            {
                this.load();
                this._xmlData.strWeiboNickname = value;
                this.unload();
            }
        }

       [MongoIgnore]
        public uint Uin
        {
            get
            {
                return this._Uin;
            }
            set
            {
                this._Uin = value;
                //base.modify |= 8L;
            }
        }
    }
}

