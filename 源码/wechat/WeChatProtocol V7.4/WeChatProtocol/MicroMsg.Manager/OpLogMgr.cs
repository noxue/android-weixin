namespace MicroMsg.Manager
{
    using Google.ProtocolBuffers;
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class OpLogMgr
    {
        private const string TAG = "OpLogMgr";
        private static List<OpLog> _opList = new List<OpLog>();
        private static List<List<OpLog>> _opListList =new List<List<OpLog>>();

        public static bool add(OpLog op)
        {
            if (op == null)
            {
                return false;
            }
            if ((op.nCmdId < 0) || (op.nCmdId >= 0x30))
            {
                return false;
            }
           // Log.e("OpLogMgr", "************ add************");
           // return false;
            //return (((op.bytesCmdBuf != null) && (op.bytesCmdBuf.Length > 0)) && StorageMgr.oplog.add(op));
            //return (((op.bytesCmdBuf != null) && (op.bytesCmdBuf.Length > 0)) && _opList.Add(op)));
            if(op.bytesCmdBuf != null && op.bytesCmdBuf.Length > 0)
            {
                _opList.Add(op);
                return true;
            }
            return false;
        }

        public static bool addList(List<OpLog> opList)
        {
            if (opList == null)
            {
                return false;
            }
            //return StorageMgr.oplog.addList(opList);
            _opListList.Add(opList);
            Log.e("OpLogMgr", "************ addList************111111" );
            return true;
        }

        public static void clear(int nMaxOplogID)
        {
            //StorageMgr.oplog.clear(nMaxOplogID);
            //_opList.Remove();
            for (int i = _opList.Count - 1; i >= 0; i--)
            {
                if (_opList[i].nID == nMaxOplogID)
                {
                    _opList.RemoveAt(i);
                }
            }
        }

        public static int count()
        {
            //return StorageMgr.oplog.count();
            //Log.e("OpLogMgr", "************ addList************");
            return _opList.Count;
        }

        public static void dumpData()
        {
            //List<OpLog> list = StorageMgr.oplog.getList();
            //if (list != null)
            //{
            //    Log.d("OpLogMgr", "************ oplist  count = " + list.Count);
            //    foreach (OpLog log in list)
            //    {
            //        object o = CmdItemHelper.toCmdObject(log);
            //        Log.d("OpLogMgr", o.debugString());
            //    }
            //    Log.d("OpLogMgr", "************ ************");
            //}
            Log.e("OpLogMgr", "************ ************");
        }

        public static CmdList getCmdItemList()
        {
            List<OpLog> list = _opList;
            CmdList.Builder builder = new CmdList.Builder();
            if ((list == null) || (list.Count <= 0))
            {
               // Log.e("OpLogMgr", "************ getCmdItemList************");
                return builder.SetCount(0).Build();
            }
            foreach (OpLog log in list)
            {
                SKBuiltinBuffer_t _t = Util.toSKBuffer(log.bytesCmdBuf);
                CmdItem item = CmdItem.CreateBuilder().SetCmdId(log.nCmdId).SetCmdBuf(_t).Build();
                if (item != null)
                {
                    builder.ListList.Add(item);
                }
            }
            return builder.SetCount((uint)builder.ListList.Count).Build();
        }

        public static int getMaxOplogID()
        {
            List<OpLog> source = _opList;
            if ((source == null) || (source.Count <= 0))
            {
                return 0;
            }
            return source.Max<OpLog>(((Func<OpLog, int>)(op => op.nID)));
        }

        public static bool OpDelChatContact(string userName)
        {
            DelChatContact.Builder builder = DelChatContact.CreateBuilder();
            builder.UserName = Util.toSKString(userName);
            return add(new OpLog(7, builder.Build().ToByteArray()));
        }

        public static bool OpDelContact(string userName)
        {
            DelContact.Builder builder = DelContact.CreateBuilder();
            builder.UserName = Util.toSKString(userName);
            return add(new OpLog(4, builder.Build().ToByteArray()));
        }

        //public static bool OpDeleteBottle(string id, int throwBackType = 0)
        //{
        //    DeleteBottle.Builder builder = DeleteBottle.CreateBuilder();
        //    builder.BottleId = id;
        //    builder.ThrowBackType = throwBackType;
        //    return add(new OpLog(0x20, builder.Build().ToByteArray()));
        //}

        public static bool opFunctionSwitch(FunctionID f, bool enable)
        {
            FunctionSwitch.Builder builder = FunctionSwitch.CreateBuilder();
            builder.FunctionId = (uint) f;
            builder.SwitchValue = enable ? (uint)1 : (uint)2;
            return add(new OpLog(0x17, builder.Build().ToByteArray()));
        }

        public static bool OplogAddKVStat(List<KVStat> items)
        {
            if ((items == null) || (items.Count <= 0))
            {
                return false;
            }
            KVStatOpLog.Builder builder = KVStatOpLog.CreateBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                KVStatItem.Builder builder2 = KVStatItem.CreateBuilder();
                builder2.Key = (uint)items[i].key;
                builder2.Value = items[i].value;
                builder.AddList(builder2.Build());
            }
            builder.Count = (uint)builder.ListCount;
            
            Log.e("OpLogMgr", "************ OplogAddKVStat items************");
            return add(new OpLog(0x24, builder.Build().ToByteArray()));
        }

        public static bool OplogAddKVStat(int key, string value)
        {
            KVStatOpLog.Builder builder = KVStatOpLog.CreateBuilder();
            KVStatItem.Builder builder2 = KVStatItem.CreateBuilder();
            builder2.Key = (uint)key;
            builder2.Value = value;
            builder.AddList(builder2.Build());
            builder.Count = (uint)builder.ListCount;
           
            Log.e("OpLogMgr", "************ OplogAddKVStat ************");
            return add(new OpLog(0x24, builder.Build().ToByteArray()));
        }

        public static bool OpModBrandSetting(string username, uint BrandFlag)
        {
            ModBrandSetting.Builder builder = ModBrandSetting.CreateBuilder();
            builder.UserName = username;
            builder.BrandFlag = BrandFlag;
            return add(new OpLog(0x2f, builder.Build().ToByteArray()));
        }

        public static bool OpModChatRoomNotify(string chatRoomName, uint status)
        {
            ModChatRoomNotify.Builder builder = ModChatRoomNotify.CreateBuilder();
            builder.ChatRoomName = Util.toSKString(chatRoomName);
            builder.Status = status;
            return add(new OpLog(20, builder.Build().ToByteArray()));
        }

        public static bool OpModChatRoomTopic(string chatRoomName, string chatRoomTopic)
        {
            ModChatRoomTopic.Builder builder = ModChatRoomTopic.CreateBuilder();
            builder.ChatRoomName = Util.toSKString(chatRoomName);
            builder.ChatRoomTopic = Util.toSKString(chatRoomTopic);
            return add(new OpLog(0x1b, builder.Build().ToByteArray()));
        }

        public static bool OpModContact(Contact ct, uint bitMask)
        {
            ModContact.Builder builder = ModContact.CreateBuilder();
            builder.UserName = Util.toSKString(ct.strUsrName);
            builder.NickName = Util.toSKString(ct.strNickName);
            builder.PYInitial = Util.toSKString(ct.strPYInitial);
            builder.QuanPin = Util.toSKString(ct.strQuanPin);
            builder.Sex = ct.nSex;
            builder.ImgBuf = Util.toSKBuffer("");
            builder.BitMask = bitMask;
            builder.BitVal = ct.nFlags;
            builder.ImgFlag = ct.nImgFlag;
            builder.Remark = Util.toSKString(ct.strRemark);
            builder.RemarkPYInitial = Util.toSKString(ct.strRemarkPYInitial);
            builder.RemarkQuanPin = Util.toSKString(ct.strRemarkQuanPin);
            builder.ContactType = ct.nContactType;
            builder.RoomInfoCount = 0;
            builder.DomainList = Util.toSKString(ct.strDomainList);
            builder.ChatRoomNotify = ct.nChatRoomNotify;
            builder.AddContactScene = ct.nAddContactScene;
            builder.Province = Util.NullAsNil(ct.strProvince);
            builder.City = Util.NullAsNil(ct.strCity);
            builder.Signature = Util.NullAsNil(ct.strSignature);
            builder.PersonalCard = ct.nPersonalCard;
            builder.HasWeiXinHdHeadImg = ct.nHasWeiXinHdHeadImg;
            builder.VerifyFlag = ct.nVerifyFlag;
            builder.VerifyInfo = Util.NullAsNil(ct.strVerifyInfo);
            return add(new OpLog(2, builder.Build().ToByteArray()));
        }

        public static bool OpModMsgStatus(int msgSvrId, string fromUserName, string toUserName, uint status)
        {
            ModMsgStatus.Builder builder = ModMsgStatus.CreateBuilder();
            builder.MsgId = msgSvrId;
            builder.FromUserName = Util.toSKString(fromUserName);
            builder.ToUserName = Util.toSKString(toUserName);
            builder.Status = status;
            return add(new OpLog(6, builder.Build().ToByteArray()));
        }

        public static bool opModUserInfo(uint BitFlag, Account acc)
        {
            if ((BitFlag == 0) || (acc == null))
            {
                return false;
            }
            ModUserInfo.Builder builder = ModUserInfo.CreateBuilder();
            builder.BitFlag = BitFlag;
            builder.UserName = Util.toSKString(acc.strUsrName);
            builder.NickName = Util.toSKString("");
            builder.BindUin = 0;
            builder.BindEmail = Util.toSKString("");
            builder.BindMobile = Util.toSKString("");
            builder.Status = 0;
            builder.ImgLen = 0;
            if ((BitFlag & 1) != 0)
            {
                builder.UserName = Util.toSKString(acc.strUsrName);
            }
            if ((BitFlag & 2) != 0)
            {
                builder.NickName = Util.toSKString(acc.strNickName);
            }
            if ((BitFlag & 4) != 0)
            {
                builder.BindUin = acc.nUin;
            }
            if ((BitFlag & 8) != 0)
            {
                builder.BindEmail = Util.toSKString(acc.strBindEmail);
            }
            if ((BitFlag & 0x10) != 0)
            {
                builder.BindMobile = Util.toSKString(acc.strBindMobile);
            }
            if ((BitFlag & 0x20) != 0)
            {
                builder.Status = acc.nStatus;
            }
            long num1 = BitFlag & 0x40;
            if ((BitFlag & 0x80) != 0)
            {
                builder.PersonalCard = acc.nPersonalCard;
                builder.Sex = acc.nSex;
                builder.Signature = (acc.strSignature == null) ? "" : acc.strSignature;
                builder.Province = (acc.strProvince == null) ? "" : acc.strProvince;
                builder.City = (acc.strCity == null) ? "" : acc.strCity;
                builder.Country = (acc.strCountry == null) ? "" : acc.strCountry;
            }
            long num2 = BitFlag & 0x100;
            long num3 = BitFlag & 0x200;
            long num4 = BitFlag & 0x400;
            if ((BitFlag & 0x800) != 0)
            {
                builder.PluginFlag = acc.nPluginFlag;
            }
            return add(new OpLog(1, builder.Build().ToByteArray()));
        }

        public static bool OpQuitChatRoom(string chatRoomName)
        {
            QuitChatRoom.Builder builder = QuitChatRoom.CreateBuilder();
            builder.UserName = Util.toSKString(AccountMgr.getCurAccount().strUsrName);
            builder.ChatRoomName = Util.toSKString(chatRoomName);
            return add(new OpLog(0x10, builder.Build().ToByteArray()));
        }

        //public static bool OpSendPsmStat(PSMSTAT_KEY key, string tipID)
        //{
        //    PSMStat.Builder builder = PSMStat.CreateBuilder();
        //    builder.MType = (uint) key;
        //    builder.AType = tipID;
        //    return add(new OpLog(0x1a, builder.Build().ToByteArray()));
        //}

        //public static bool opUploadSelfHeadimg(uint BitFlag, Account acc, byte[] imgBuf)
        //{
        //    if ((BitFlag == 0) || (acc == null))
        //    {
        //        return false;
        //    }
        //    ModUserInfo.Builder builder = ModUserInfo.CreateBuilder();
        //    builder.BitFlag = BitFlag;
        //    builder.UserName = Util.toSKString(acc.strUsrName);
        //    builder.NickName = Util.toSKString("");
        //    builder.BindUin = 0;
        //    builder.BindEmail = Util.toSKString("");
        //    builder.BindMobile = Util.toSKString("");
        //    builder.Status = 0;
        //    builder.ImgLen = (uint) imgBuf.Length;
        //    builder.ImgBuf = ByteString.CopyFrom(imgBuf);
        //    return add(new OpLog(1, builder.Build().ToByteArray()));
        //}

        public enum FunctionID : uint
        {
            BottleInChat = 14,
            BottleMsgNotify = 11,
            DisableAutoAddFriendWhenReply = 9,
            DisableFindMeByMobile = 8,
            DisableFindMeByQQ = 2,
            DisablePushMsgDetail = 10,
            DisableRecommendMeToQQFriends = 3,
            DisableRecommendMobileFirendsToMe = 7,
            DisableRecommendQQFriendsToMe = 6,
            DisableUploadMContact = 0x11,
            DisplayWeixinOnLine = 12,
            GMail = 15,
            MediaNote = 13,
            MM_FUNCTIONSWITCH_ALBUM_NOT_FOR_STRANGER = 0x18,
            MM_FUNCTIONSWITCH_MEISHI_CARD_OPEN = 0x17,
            MM_FUNCTIONSWITCH_NEWSAPP_TXNEWS_CLOSE = 0x1a,
            MM_FUNCTIONSWITCH_SAFEDEVICE_OPEN = 0x1c,
            MM_FUNCTIONSWITCH_USERNAME_SEARCH_CLOSE = 0x19,
            MM_FUNCTIONSWITCH_WEBONLINE_PUSH_OPEN = 0x1b,
            NeedVerify = 4,
            QQMailPushNotify = 1,
            Reader = 0x13,
            ReaderTXNews = 20,
            ReaderTXWeibo = 0x15,
            RECFBFriend = 0x12,
            RecvOfflineQQMsg = 5,
            TXWeibo = 0x10,
            TXWeiboIcon = 0x16
        }

        public enum FunctionValue : uint
        {
            disable = 2,
            enable = 1
        }
    }
}

