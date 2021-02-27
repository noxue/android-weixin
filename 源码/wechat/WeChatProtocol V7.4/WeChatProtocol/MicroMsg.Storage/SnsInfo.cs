namespace MicroMsg.Storage
{
    using MicroMsg.Common.Utils;

    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;


    public class SnsInfo : StorageItem
    {
        private SnsUserNameList _blackList;
        private SnsCommentList _commentList;
        private SnsGroupList _groupList;
        private SnsUserNameList _groupUserList;
        private SnsCommentList _likeList;

  
        private SnsCommentList _withList;

        public byte[] bytesObjectDesc;

        public byte[] bytesXmlBlackList;

        public byte[] bytesXmlCommnetList;

        public byte[] bytesXmlGroupList;

        public byte[] bytesXmlGroupUserList;

        public byte[] bytesXmlLikeList;

        public byte[] bytesXmlWithList;

        public int nCheckSum;

        public int nCommentCount;

        public uint nCreateTime;

        public uint nExtFlag;

        public int nLikeCount;

        public int nLikeFlag;
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int nLocalID;

        public int nObjectStyle;

        public int nStatus;

        public int nWithUserCount;

        public int? pBlackListCount;

        public int? pGroupUserCount;
        [Column(CanBeNull = true)]
        public int? pIsRichText;

        public string strNickName;

        public string strObjectID;

        public string strUserName;
        public TimeLineObject timeLine;

        public void blackListFlush()
        {
            if ((this._blackList == null) || (this._blackList.list.Count <= 0))
            {
                this.bytesXmlBlackList = null;
            }
            else
            {
                this.bytesXmlBlackList = StorageXml.saveToBuffer<SnsUserNameList>(this._blackList);
            }
        }

        public void commentListFlush()
        {
            if ((this._commentList == null) || (this._commentList.list.Count <= 0))
            {
                this.bytesXmlCommnetList = null;
            }
            else
            {
                this.bytesXmlCommnetList = StorageXml.saveToBuffer<SnsCommentList>(this._commentList);
            }
        }

        public bool deleteSnsComment(int commentID)
        {
            foreach (SnsComment comment in this.commentList.list)
            {
                if (comment.nCommentId == commentID)
                {
                    this.commentList.list.Remove(comment);
                    this.commentListFlush();
                    return true;
                }
            }
            return false;
        }

        public void dumpXml()
        {
        }

        public TimeLineObject getTimeLine()
        {
            if (this.timeLine == null)
            {
                this.timeLine = TimeLineObject.ParseFrom(this.bytesObjectDesc);
                if (this.timeLine == null)
                {
                    Log.e("SnsInfo", "parse xml error sns obj id=" + this.nObjectID);
                    TimeLineObject.dumpXmlData(this.bytesObjectDesc);
                }
            }
            return this.timeLine;
        }

        public void groupListFlush()
        {
            if ((this._groupList == null) || (this._groupList.list.Count <= 0))
            {
                this.bytesXmlGroupList = null;
            }
            else
            {
                this.bytesXmlGroupList = StorageXml.saveToBuffer<SnsGroupList>(this._groupList);
            }
        }

        public void groupUserListFlush()
        {
            if ((this._groupUserList == null) || (this._groupUserList.list.Count <= 0))
            {
                this.bytesXmlGroupUserList = null;
            }
            else
            {
                this.bytesXmlGroupUserList = StorageXml.saveToBuffer<SnsUserNameList>(this._groupUserList);
            }
        }

        public bool isPrivate()
        {
            if (this.getTimeLine() == null)
            {
                return false;
            }
            return (this.timeLine.nPrivate == 1);
        }

        public void likeListFlush()
        {
            if ((this._likeList == null) || (this._likeList.list.Count <= 0))
            {
                this.bytesXmlLikeList = null;
            }
            else
            {
                this.bytesXmlLikeList = StorageXml.saveToBuffer<SnsCommentList>(this._likeList);
            }
        }

        public override void merge(object o)
        {
            SnsInfo info = o as SnsInfo;
            if (info != null)
            {
                this.nCheckSum = info.nCheckSum;
                this.nStatus = info.nStatus;
                this.nObjectStyle = info.nObjectStyle;
                this.strObjectID = info.strObjectID;
                this.strUserName = info.strUserName;
                this.strNickName = info.strNickName;
                this.nCreateTime = info.nCreateTime;
                this.nLikeFlag = info.nLikeFlag;
                this.nExtFlag = info.nExtFlag;
                this.nLikeCount = info.nLikeCount;
                this.nCommentCount = info.nCommentCount;
                this.nWithUserCount = info.nWithUserCount;
                this.bytesXmlLikeList = info.bytesXmlLikeList;
                this.bytesXmlCommnetList = info.bytesXmlCommnetList;
                this.bytesXmlWithList = info.bytesXmlWithList;
                this.bytesObjectDesc = info.bytesObjectDesc;
                this.bytesXmlGroupList = info.bytesXmlGroupList;
                this.bytesXmlGroupUserList = info.bytesXmlGroupUserList;
                this.bytesXmlBlackList = info.bytesXmlBlackList;
                this._likeList = null;
                this._commentList = null;
                this._withList = null;
                this._groupList = null;
                this.timeLine = null;
            }
        }

        public bool setLikeFlag(bool bLike, string name = null)
        {
            if (bLike && (this.nLikeFlag == 1))
            {
                return false;
            }
            if (!bLike && (this.nLikeFlag == 0))
            {
                return false;
            }
            this.nLikeFlag = bLike ? 1 : 0;
            if (!string.IsNullOrEmpty(name))
            {
                if (bLike)
                {
                    SnsComment item = new SnsComment
                    {
                        strUserName = name
                    };
                    this.likeList.list.Add(item);
                    this.likeListFlush();
                }
                else
                {
                    foreach (SnsComment comment2 in this.likeList.list)
                    {
                        if (comment2.strUserName == name)
                        {
                            this.likeList.list.Remove(comment2);
                            this.likeListFlush();
                            break;
                        }
                    }
                }
            }
            return true;
        }

        public bool setPrivacy(bool bPrivacy)
        {
            if (this.getTimeLine() == null)
            {
                return false;
            }
            if (bPrivacy && (this.timeLine.nPrivate == 1))
            {
                return false;
            }
            if (!bPrivacy && (this.timeLine.nPrivate == 0))
            {
                return false;
            }
            this.timeLine.nPrivate = bPrivacy ? 1 : 0;
            this.bytesObjectDesc = this.timeLine.toXmlData();
            return true;
        }

        public void setTimeLine(TimeLineObject tl)
        {
            this.bytesObjectDesc = tl.toXmlData();
            this.timeLine = tl;
        }

        public static ulong toID(string strID)
        {
            return Convert.ToUInt64(strID, 0x10);
        }

        public static string toStrID(ulong nID)
        {
            return nID.ToString("X16");
        }

        public void withListFlush()
        {
            if ((this._withList == null) || (this._withList.list.Count <= 0))
            {
                this.bytesXmlWithList = null;
            }
            else
            {
                this.bytesXmlWithList = StorageXml.saveToBuffer<SnsCommentList>(this._withList);
            }
        }

        public SnsUserNameList BlackList
        {
            get
            {
                if (this._blackList == null)
                {
                    if (this.bytesXmlBlackList != null)
                    {
                        this._blackList = StorageXml.loadFromBuffer<SnsUserNameList>(this.bytesXmlBlackList);
                    }
                    if (this._blackList == null)
                    {
                        this._blackList = new SnsUserNameList();
                    }
                }
                return this._blackList;
            }
            set
            {
                this._blackList = value;
                this.blackListFlush();
            }
        }

        public SnsCommentList commentList
        {
            get
            {
                if (this._commentList == null)
                {
                    if (this.bytesXmlCommnetList != null)
                    {
                        this._commentList = StorageXml.loadFromBuffer<SnsCommentList>(this.bytesXmlCommnetList);
                    }
                    if (this._commentList == null)
                    {
                        this._commentList = new SnsCommentList();
                    }
                }
                return this._commentList;
            }
            set
            {
                this._commentList = value;
                this.commentListFlush();
            }
        }

        public SnsGroupList groupList
        {
            get
            {
                if (this._groupList == null)
                {
                    if (this.bytesXmlGroupList != null)
                    {
                        this._groupList = StorageXml.loadFromBuffer<SnsGroupList>(this.bytesXmlGroupList);
                    }
                    if (this._groupList == null)
                    {
                        this._groupList = new SnsGroupList();
                    }
                }
                return this._groupList;
            }
            set
            {
                this._groupList = value;
                this.groupListFlush();
            }
        }

        public SnsUserNameList GroupUserList
        {
            get
            {
                if (this._groupUserList == null)
                {
                    if (this.bytesXmlGroupUserList != null)
                    {
                        this._groupUserList = StorageXml.loadFromBuffer<SnsUserNameList>(this.bytesXmlGroupUserList);
                    }
                    if (this._groupUserList == null)
                    {
                        this._groupUserList = new SnsUserNameList();
                    }
                }
                return this._groupUserList;
            }
            set
            {
                this._groupUserList = value;
                this.groupUserListFlush();
            }
        }

        public SnsCommentList likeList
        {
            get
            {
                if (this._likeList == null)
                {
                    if (this.bytesXmlLikeList != null)
                    {
                        this._likeList = StorageXml.loadFromBuffer<SnsCommentList>(this.bytesXmlLikeList);
                    }
                    if (this._likeList == null)
                    {
                        this._likeList = new SnsCommentList();
                    }
                }
                return this._likeList;
            }
            set
            {
                this._likeList = value;
                this.likeListFlush();
            }
        }

        public int nBlackListCount
        {
            get
            {
                int? pBlackListCount = this.pBlackListCount;
                if (!pBlackListCount.HasValue)
                {
                    return 0;
                }
                return pBlackListCount.GetValueOrDefault();
            }
            set
            {
                this.pBlackListCount = new int?(value);
            }
        }

        public int nGroupUserCount
        {
            get
            {
                int? pGroupUserCount = this.pGroupUserCount;
                if (!pGroupUserCount.HasValue)
                {
                    return 0;
                }
                return pGroupUserCount.GetValueOrDefault();
            }
            set
            {
                this.pGroupUserCount = new int?(value);
            }
        }

        public int nIsRichText
        {
            get
            {
                int? pIsRichText = this.pIsRichText;
                if (!pIsRichText.HasValue)
                {
                    return 0;
                }
                return pIsRichText.GetValueOrDefault();
            }
            set
            {
                this.pIsRichText = new int?(value);
            }
        }

        public ulong nObjectID
        {
            get
            {
                if (this.strObjectID == null)
                {
                    return 0L;
                }
                return toID(this.strObjectID);
            }
            set
            {
                this.strObjectID = toStrID(value);
            }
        }

        public SnsCommentList withList
        {
            get
            {
                if (this._withList == null)
                {
                    if (this.bytesXmlWithList != null)
                    {
                        this._withList = StorageXml.loadFromBuffer<SnsCommentList>(this.bytesXmlWithList);
                    }
                    if (this._withList == null)
                    {
                        this._withList = new SnsCommentList();
                    }
                }
                return this._withList;
            }
            set
            {
                this._withList = value;
                this.withListFlush();
            }
        }
    }


    public class SnsUserNameList
    {

        public List<string> list = new List<string>();

        public int nCount;
    }


}
