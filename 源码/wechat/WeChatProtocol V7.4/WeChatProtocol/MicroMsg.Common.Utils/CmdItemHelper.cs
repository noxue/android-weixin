namespace MicroMsg.Common.Utils
{
    using micromsg;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CmdItemHelper
    {
        public static Dictionary<Type, SyncCmdID> mapCmdId;
        public static Dictionary<SyncCmdID, Type> mapCmdObject;
        private const string TAG = "CmdItemHelper";

        static CmdItemHelper()
        {
            Dictionary<Type, SyncCmdID> dictionary = new Dictionary<Type, SyncCmdID>();
            dictionary.Add(typeof(ModUserInfo), SyncCmdID.CmdIdModUserInfo);
            dictionary.Add(typeof(ModContact), SyncCmdID.CmdIdModContact);
            dictionary.Add(typeof(DelContact), SyncCmdID.CmdIdDelContact);
            dictionary.Add(typeof(AddMsg), SyncCmdID.CmdIdAddMsg);
            dictionary.Add(typeof(ModMsgStatus), SyncCmdID.CmdIdModMsgStatus);
            dictionary.Add(typeof(DelChatContact), SyncCmdID.CmdIdDelChatContact);
            dictionary.Add(typeof(DelContactMsg), SyncCmdID.CmdIdDelContactMsg);
            dictionary.Add(typeof(DelMsg), SyncCmdID.CmdIdDelMsg);
            dictionary.Add(typeof(Report), SyncCmdID.CmdIdReport);
            dictionary.Add(typeof(OpenQQMicroBlog), SyncCmdID.CmdIdOpenQQMicroBlog);
            dictionary.Add(typeof(CloseMicroBlog), SyncCmdID.CmdIdCloseMicroBlog);
            dictionary.Add(typeof(ModNotifyStatus), SyncCmdID.CmdIdModNotifyStatus);
            dictionary.Add(typeof(ModChatRoomMember), SyncCmdID.CmdIdModChatRoomMember);
            dictionary.Add(typeof(ModChatRoomTopic), SyncCmdID.CmdIdModChatRoomTopic);
            dictionary.Add(typeof(ModChatRoomNotify), SyncCmdID.CmdIdModChatRoomNotify);
            dictionary.Add(typeof(QuitChatRoom), SyncCmdID.CmdIdQuitChatRoom);
            dictionary.Add(typeof(DelUserDomainEmail), SyncCmdID.CmdIdDelUserDomainEmail);
            dictionary.Add(typeof(InviteFriendOpen), SyncCmdID.CmdIdInviteFriendOpen);
            dictionary.Add(typeof(FunctionSwitch), SyncCmdID.CmdIdFunctionSwitch);
            dictionary.Add(typeof(QContact), SyncCmdID.CmdIdModQContact);
            dictionary.Add(typeof(TContact), SyncCmdID.CmdIdModTContact);
            dictionary.Add(typeof(PSMStat), SyncCmdID.CmdIdPsmStat);
            dictionary.Add(typeof(UpdateStatOpLog), SyncCmdID.MM_SYNCCMD_UPDATESTAT);
            dictionary.Add(typeof(ModDisturbSetting), SyncCmdID.MM_SYNCCMD_MODDISTURBSETTING);
            dictionary.Add(typeof(DeleteBottle), SyncCmdID.MM_SYNCCMD_DELETEBOTTLE);
            dictionary.Add(typeof(ModBottleContact), SyncCmdID.MM_SYNCCMD_MODBOTTLECONTACT);
            dictionary.Add(typeof(ModUserImg), SyncCmdID.MM_SYNCCMD_MODUSERIMG);
            dictionary.Add(typeof(KVStatOpLog), SyncCmdID.MM_SYNCCMD_KVSTAT);
            dictionary.Add(typeof(ThemeOpLog), SyncCmdID.NN_SYNCCMD_THEMESTAT);
            dictionary.Add(typeof(UserInfoExt), SyncCmdID.MM_SYNCCMD_USERINFOEXT);
            dictionary.Add(typeof(SnsObject), SyncCmdID.MM_SNS_SYNCCMD_OBJECT);
            dictionary.Add(typeof(SnsActionGroup), SyncCmdID.MM_SNS_SYNCCMD_ACTION);
            dictionary.Add(typeof(ModBrandSetting), SyncCmdID.MM_SYNCCMD_BRAND_SETTING);
            mapCmdId = dictionary;
            Dictionary<SyncCmdID, Type> dictionary2 = new Dictionary<SyncCmdID, Type>();
            dictionary2.Add(SyncCmdID.CmdIdModUserInfo, typeof(ModUserInfo));
            dictionary2.Add(SyncCmdID.CmdIdModContact, typeof(ModContact));
            dictionary2.Add(SyncCmdID.CmdIdDelContact, typeof(DelContact));
            dictionary2.Add(SyncCmdID.CmdIdAddMsg, typeof(AddMsg));
            dictionary2.Add(SyncCmdID.CmdIdModMsgStatus, typeof(ModMsgStatus));
            dictionary2.Add(SyncCmdID.CmdIdDelChatContact, typeof(DelChatContact));
            dictionary2.Add(SyncCmdID.CmdIdDelContactMsg, typeof(DelContactMsg));
            dictionary2.Add(SyncCmdID.CmdIdDelMsg, typeof(DelMsg));
            dictionary2.Add(SyncCmdID.CmdIdReport, typeof(Report));
            dictionary2.Add(SyncCmdID.CmdIdOpenQQMicroBlog, typeof(OpenQQMicroBlog));
            dictionary2.Add(SyncCmdID.CmdIdCloseMicroBlog, typeof(CloseMicroBlog));
            dictionary2.Add(SyncCmdID.CmdIdModNotifyStatus, typeof(ModNotifyStatus));
            dictionary2.Add(SyncCmdID.CmdIdModChatRoomMember, typeof(ModChatRoomMember));
            dictionary2.Add(SyncCmdID.CmdIdQuitChatRoom, typeof(QuitChatRoom));
            dictionary2.Add(SyncCmdID.CmdIdDelUserDomainEmail, typeof(DelUserDomainEmail));
            dictionary2.Add(SyncCmdID.CmdIdModChatRoomNotify, typeof(ModChatRoomNotify));
            dictionary2.Add(SyncCmdID.CmdIdInviteFriendOpen, typeof(InviteFriendOpen));
            dictionary2.Add(SyncCmdID.CmdIdFunctionSwitch, typeof(FunctionSwitch));
            dictionary2.Add(SyncCmdID.CmdIdModQContact, typeof(QContact));
            dictionary2.Add(SyncCmdID.CmdIdModTContact, typeof(TContact));
            dictionary2.Add(SyncCmdID.CmdIdPsmStat, typeof(PSMStat));
            dictionary2.Add(SyncCmdID.CmdIdModChatRoomTopic, typeof(ModChatRoomTopic));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_UPDATESTAT, typeof(UpdateStatOpLog));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_MODDISTURBSETTING, typeof(ModDisturbSetting));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_DELETEBOTTLE, typeof(DeleteBottle));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_MODBOTTLECONTACT, typeof(ModBottleContact));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_MODUSERIMG, typeof(ModUserImg));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_KVSTAT, typeof(KVStatOpLog));
            dictionary2.Add(SyncCmdID.NN_SYNCCMD_THEMESTAT, typeof(ThemeOpLog));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_USERINFOEXT, typeof(UserInfoExt));
            dictionary2.Add(SyncCmdID.MM_SNS_SYNCCMD_OBJECT, typeof(SnsObject));
            dictionary2.Add(SyncCmdID.MM_SNS_SYNCCMD_ACTION, typeof(SnsActionGroup));
            dictionary2.Add(SyncCmdID.MM_SYNCCMD_BRAND_SETTING, typeof(ModBrandSetting));
            mapCmdObject = dictionary2;
        }

        public static bool isOplog(object o)
        {
            return mapCmdId.ContainsKey(o.GetType());
        }

        public static CmdItem toCmdItem(object o)
        {
            try
            {
                if (!mapCmdId.ContainsKey(o.GetType()))
                {
                    Log.e("CmdItemHelper", "can not find object in mapCmdId, o.GetType() = " + o.GetType());
                    //DebugEx.debugBreak();
                    return null;
                }
                SyncCmdID did = mapCmdId[o.GetType()];
                SKBuiltinBuffer_t _t = Util.toSKBuffer(o.GetType().InvokeMember("ToByteArray", BindingFlags.InvokeMethod, null, o, null) as byte[]);
                return CmdItem.CreateBuilder().SetCmdId((int) did).SetCmdBuf(_t).Build();
            }
            catch (Exception exception)
            {
                Log.d("CmdItemHelper", "CmdItemHelper  ToByteArray error" + exception);
            }
            return null;
        }

        public static object toCmdObject(CmdItem cmdItem)
        {
            try
            {
                if (!mapCmdObject.ContainsKey((SyncCmdID) cmdItem.CmdId))
                {
                    Log.e("CmdItemHelper", "can not find CmdId in mapCmdObject, cmdItem.CmdId = " + cmdItem.CmdId);
                    if (cmdItem.CmdId != 13)
                    {
                       // DebugEx.debugBreak();
                    }
                    return null;
                }
                Type type = mapCmdObject[(SyncCmdID) cmdItem.CmdId];
                object[] args = new object[] { cmdItem.CmdBuf.Buffer.ToByteArray() };
                return type.InvokeMember("ParseFrom", BindingFlags.InvokeMethod, null, null, args);
            }
            catch (Exception exception)
            {
                Log.d("util", "CmdItemHelper  parse cmdItem error" + exception);
            }
            return null;
        }

        public static object toCmdObject(OpLog op)
        {
            if (op != null)
            {
                if (!mapCmdObject.ContainsKey((SyncCmdID)op.nCmdId))
                {
                    return null;
                }
                try
                {
                    Type type = mapCmdObject[(SyncCmdID)op.nCmdId];
                    object[] args = new object[] { op.bytesCmdBuf };
                    return type.InvokeMember("ParseFrom", BindingFlags.InvokeMethod, null, null, args);
                }
                catch (Exception exception)
                {
                    Log.d("util", "CmdItemHelper  parse cmdItem error" + exception);
                }
            }
            return null;
        }
    }
}

