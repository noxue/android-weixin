namespace MicroMsg.Storage
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;


    public class BottleContact : StorageItem
    {
     

        
        public byte[] bytesXmlData;
        
        public long nCreateTime;
        
        public int nHasChat;
        
        public int nHDImgFlag;
        
        public int nImgFlag;
        
        public int nSex;
        
        public int nType;
        
        public string strBigHeadImgUrl;
        
        public string strCity;
        
        public string strCountry;
        
        public string strNickName;
        
        public string strProvince;
        
        public string strSignature;
        
        public string strSmallHeadImgUrl;
        public string strUserName;
    }
}

