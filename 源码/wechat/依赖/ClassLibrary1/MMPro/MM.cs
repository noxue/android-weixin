namespace MMPro
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public class MM
    {
        [ProtoContract]
        public class _NEWMSG
        {
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public string Alias;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nick;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong serverid;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string t13;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public ulong t14;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public byte[] t15;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public ulong t16;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public ulong t17;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public ulong t19;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public ulong t20;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public ulong t21;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public ulong t22;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public ulong t23;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public ulong t25;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public byte[] t26;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public ulong t29;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public ulong t30;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public ulong t31;
            [ProtoMember(0x21, Options=MemberSerializationOptions.Required)]
            public ulong t33;
            [ProtoMember(0x22, Options=MemberSerializationOptions.Required)]
            public ulong t34;
            [ProtoMember(0x24, Options=MemberSerializationOptions.Required)]
            public ulong t36;
            [ProtoMember(0x26, Options=MemberSerializationOptions.Required)]
            public byte[] t38;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public ulong t4;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public byte[] t5;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public byte[] t6;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public ulong t7;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public ulong t8;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public ulong t9;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString wxid;
        }

        [ProtoContract]
        public class AccountInfo
        {
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string Alias;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string bindMail;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string bindMobile;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int bindUin;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string deviceInfoXml;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string fsUrl;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string nickName;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string officialNamePinyin;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public string officialNameZh;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public int pluginFlag;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public int pushMailStatus;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int registerType;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public int safeDevice;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public int status;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string wxid;
        }

        [ProtoContract]
        public class AccountStorage
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.AesKey Key;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public LocalInfo local;

            [ProtoContract]
            public class LocalInfo
            {
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public T1 data;
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public uint len;

                [ProtoContract]
                public class T1
                {
                    [ProtoMember(3, Options=MemberSerializationOptions.Required)]
                    public uint createtime;
                    [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                    public byte[] data;
                    [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                    public uint len;
                }
            }
        }

        [ProtoContract]
        public class AcctTypeResource
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string Accttypeid;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] aWordings;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] Urls;
        }

        [ProtoContract]
        public class AdClickRequest
        {
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public uint adType;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string bssid;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint clickAction;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int clickpos;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string descxml;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint flipStatus;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint scene;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.AdShareInfo shareInfo;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string snsStatext;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string ssid;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public ulong timestampMs;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string viewid;
        }

        [ProtoContract]
        public class AdClickResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string msg;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int ret;
        }

        [ProtoContract]
        public class AddChatRoomMemberRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString chatRoomName;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.MemberReq[] memberList;
        }

        [ProtoContract]
        public class AddChatRoomMemberResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.MemberResp[] memberList;
        }

        public enum AddContactScene
        {
            MM_ADDSCENE_APPMSG = 0x39,
            MM_ADDSCENE_BBM = 50,
            MM_ADDSCENE_BIZ_CONFERENCE = 40,
            MM_ADDSCENE_BIZ_PAY = 0x33,
            MM_ADDSCENE_BOTTLE = 0x19,
            MM_ADDSCENE_BRAND_QA = 0x22,
            MM_ADDSCENE_CHATROOM = 14,
            MM_ADDSCENE_CORP_EMAIL = 0x10,
            MM_ADDSCENE_FACEBOOK = 0x1f,
            MM_ADDSCENE_FUZZY_SEARCH = 0x23,
            MM_ADDSCENE_INTERESTED_BRAND = 0x38,
            MM_ADDSCENE_LBS = 0x12,
            MM_ADDSCENE_LBSROOM = 0x2c,
            MM_ADDSCENE_LOGO_WALL = 0x24,
            MM_ADDSCENE_PF_CONTACT = 6,
            MM_ADDSCENE_PF_EMAIL = 5,
            MM_ADDSCENE_PF_GROUP = 8,
            MM_ADDSCENE_PF_MOBILE = 10,
            MM_ADDSCENE_PF_MOBILE_EMAIL = 11,
            MM_ADDSCENE_PF_MOBILE_REVERSE = 0x15,
            MM_ADDSCENE_PF_QQ = 4,
            MM_ADDSCENE_PF_SHAKE_PHONE_GROUP = 0x17,
            MM_ADDSCENE_PF_SHAKE_PHONE_OPPSEX = 0x18,
            MM_ADDSCENE_PF_SHAKE_PHONE_PAIR = 0x16,
            MM_ADDSCENE_PF_UNKNOWN = 9,
            MM_ADDSCENE_PF_WEIXIN = 7,
            MM_ADDSCENE_PROMOTE_BIZCARD = 0x29,
            MM_ADDSCENE_PROMOTE_MSG = 0x26,
            MM_ADDSCENE_QRCode = 30,
            MM_ADDSCENE_RADARSEARCH = 0x30,
            MM_ADDSCENE_RECOMMEND_BRAND = 0x37,
            MM_ADDSCENE_REG_ADD_MFRIEND = 0x34,
            MM_ADDSCENE_SCANBARCODE = 0x31,
            MM_ADDSCENE_SCANIMAGE = 0x2d,
            MM_ADDSCENE_SCANIMAGE_BOOK = 0x2f,
            MM_ADDSCENE_SEARCH_BRAND = 0x27,
            MM_ADDSCENE_SEARCH_BRAND_SERICE = 0x35,
            MM_ADDSCENE_SEARCH_BRAND_SUBSCR = 0x36,
            MM_ADDSCENE_SEARCH_EMAIL = 2,
            MM_ADDSCENE_SEARCH_PHONE = 15,
            MM_ADDSCENE_SEARCH_QQ = 1,
            MM_ADDSCENE_SEARCH_WEIXIN = 3,
            MM_ADDSCENE_SEND_CARD = 0x11,
            MM_ADDSCENE_SHAKE_SCENE1 = 0x1a,
            MM_ADDSCENE_SHAKE_SCENE2 = 0x1b,
            MM_ADDSCENE_SHAKE_SCENE3 = 0x1c,
            MM_ADDSCENE_SHAKE_SCENE4 = 0x1d,
            MM_ADDSCENE_SHAKETV = 0x2e,
            MM_ADDSCENE_SNS = 0x20,
            MM_ADDSCENE_TIMELINE_BIZ = 0x25,
            MM_ADDSCENE_UNKNOW = 0,
            MM_ADDSCENE_VIEW_MOBILE = 13,
            MM_ADDSCENE_VIEW_QQ = 12,
            MM_ADDSCENE_WEB = 0x21,
            MM_ADDSCENE_WEB_OP_MENU = 0x2b,
            MM_ADDSCENE_WEB_PROFILE_URL = 0x2a
        }

        [ProtoContract]
        public class AddFavItemRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baserequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string clientId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string @object;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string sourceId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int sourceType;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int type;
        }

        [ProtoContract]
        public sealed class AddFavItemResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baserequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int favId;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int updateSeq;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int usedSize;
        }

        [ProtoContract]
        public class AdditionalContactList
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.LinkedinContactItem linkedinContactItem;
        }

        [ProtoContract]
        public class AddMsg
        {
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString content;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public ulong createtime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString from;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ img;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int imgStatus;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong msgid;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint msgSeq;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string msgSource;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public ulong newMsgId;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string pushcontent;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int status;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString to;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int type;
        }

        [ProtoContract]
        public class AdExposureInfo
        {
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint allEndTime;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public uint allStartTime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint endPositionType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public ulong endTime;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public ulong halfEndTime;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public ulong halfStartTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public float readHeight;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint startPositionType;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public ulong startTime;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint unReadBottomHeight;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public float unReadTopHeight;
        }

        [ProtoContract]
        public class AdExposureRequest
        {
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint adType;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string bssid;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string descxml;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public uint exposureCnt;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint exposureDuration;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.AdExposureInfo exposureInfo;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public ulong feedDuration;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public ulong feedFullDuration;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public uint flipStatus;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint scene;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public string snsStatext;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.AdExposureSocialInfo socialInfo;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string ssid;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public ulong timestampMs;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint type;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string viewid;
        }

        [ProtoContract]
        public class AdExposureResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string msg;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int ret;
        }

        [ProtoContract]
        public class AdExposureSocialInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint commentCount;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint likeCount;
        }

        [ProtoContract]
        public class AdShareInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        [ProtoContract]
        public class AesKey
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] key;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int len;
        }

        public class Agree_Duty
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <agreed_flag>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <button_wording>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <delay_expired_time>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <service_protocol_url>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <service_protocol_wording>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <title>k__BackingField;

            public int agreed_flag { get; set; }

            public string button_wording { get; set; }

            public int delay_expired_time { get; set; }

            public string service_protocol_url { get; set; }

            public string service_protocol_wording { get; set; }

            public string title { get; set; }
        }

        [ProtoContract]
        public class AppIdResource
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint Functionflag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] Urls;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] Wordings;
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true), XmlRoot(Namespace="", IsNullable=false)]
        public class appmsg
        {
            private string actionField;
            private byte androidsourceField;
            private MM.appmsgAppattach appattachField;
            private string appidField;
            private MM.appmsgCanvasPageItem canvasPageItemField;
            private object commenturlField;
            private byte contentattrField;
            private object contentField;
            private object dataurlField;
            private string desField;
            private MM.appmsgDesignershared designersharedField;
            private MM.appmsgEmoticongift emoticongiftField;
            private MM.appmsgEmoticonshared emoticonsharedField;
            private MM.appmsgEmotionpageshared emotionpagesharedField;
            private object extinfoField;
            private object lowdataurlField;
            private object lowurlField;
            private object md5Field;
            private object mediatagnameField;
            private string messageactionField;
            private string messageextField;
            private byte sdkverField;
            private byte showtypeField;
            private string sourcedisplaynameField;
            private string sourceusernameField;
            private object statextstrField;
            private MM.appmsgStreamvideo streamvideoField;
            private object template_idField;
            private object thumburlField;
            private string titleField;
            private byte typeField;
            private string urlField;
            private object usernameField;
            private MM.appmsgWeappinfo weappinfoField;
            private object websearchField;
            private MM.appmsgWebviewshared webviewsharedField;

            public string action
            {
                get
                {
                    return this.actionField;
                }
                set
                {
                    this.actionField = value;
                }
            }

            public byte androidsource
            {
                get
                {
                    return this.androidsourceField;
                }
                set
                {
                    this.androidsourceField = value;
                }
            }

            public MM.appmsgAppattach appattach
            {
                get
                {
                    return this.appattachField;
                }
                set
                {
                    this.appattachField = value;
                }
            }

            [XmlAttribute]
            public string appid
            {
                get
                {
                    return this.appidField;
                }
                set
                {
                    this.appidField = value;
                }
            }

            public MM.appmsgCanvasPageItem canvasPageItem
            {
                get
                {
                    return this.canvasPageItemField;
                }
                set
                {
                    this.canvasPageItemField = value;
                }
            }

            public object commenturl
            {
                get
                {
                    return this.commenturlField;
                }
                set
                {
                    this.commenturlField = value;
                }
            }

            public object content
            {
                get
                {
                    return this.contentField;
                }
                set
                {
                    this.contentField = value;
                }
            }

            public byte contentattr
            {
                get
                {
                    return this.contentattrField;
                }
                set
                {
                    this.contentattrField = value;
                }
            }

            public object dataurl
            {
                get
                {
                    return this.dataurlField;
                }
                set
                {
                    this.dataurlField = value;
                }
            }

            public string des
            {
                get
                {
                    return this.desField;
                }
                set
                {
                    this.desField = value;
                }
            }

            public MM.appmsgDesignershared designershared
            {
                get
                {
                    return this.designersharedField;
                }
                set
                {
                    this.designersharedField = value;
                }
            }

            public MM.appmsgEmoticongift emoticongift
            {
                get
                {
                    return this.emoticongiftField;
                }
                set
                {
                    this.emoticongiftField = value;
                }
            }

            public MM.appmsgEmoticonshared emoticonshared
            {
                get
                {
                    return this.emoticonsharedField;
                }
                set
                {
                    this.emoticonsharedField = value;
                }
            }

            public MM.appmsgEmotionpageshared emotionpageshared
            {
                get
                {
                    return this.emotionpagesharedField;
                }
                set
                {
                    this.emotionpagesharedField = value;
                }
            }

            public object extinfo
            {
                get
                {
                    return this.extinfoField;
                }
                set
                {
                    this.extinfoField = value;
                }
            }

            public object lowdataurl
            {
                get
                {
                    return this.lowdataurlField;
                }
                set
                {
                    this.lowdataurlField = value;
                }
            }

            public object lowurl
            {
                get
                {
                    return this.lowurlField;
                }
                set
                {
                    this.lowurlField = value;
                }
            }

            public object md5
            {
                get
                {
                    return this.md5Field;
                }
                set
                {
                    this.md5Field = value;
                }
            }

            public object mediatagname
            {
                get
                {
                    return this.mediatagnameField;
                }
                set
                {
                    this.mediatagnameField = value;
                }
            }

            public string messageaction
            {
                get
                {
                    return this.messageactionField;
                }
                set
                {
                    this.messageactionField = value;
                }
            }

            public string messageext
            {
                get
                {
                    return this.messageextField;
                }
                set
                {
                    this.messageextField = value;
                }
            }

            [XmlAttribute]
            public byte sdkver
            {
                get
                {
                    return this.sdkverField;
                }
                set
                {
                    this.sdkverField = value;
                }
            }

            public byte showtype
            {
                get
                {
                    return this.showtypeField;
                }
                set
                {
                    this.showtypeField = value;
                }
            }

            public string sourcedisplayname
            {
                get
                {
                    return this.sourcedisplaynameField;
                }
                set
                {
                    this.sourcedisplaynameField = value;
                }
            }

            public string sourceusername
            {
                get
                {
                    return this.sourceusernameField;
                }
                set
                {
                    this.sourceusernameField = value;
                }
            }

            public object statextstr
            {
                get
                {
                    return this.statextstrField;
                }
                set
                {
                    this.statextstrField = value;
                }
            }

            public MM.appmsgStreamvideo streamvideo
            {
                get
                {
                    return this.streamvideoField;
                }
                set
                {
                    this.streamvideoField = value;
                }
            }

            public object template_id
            {
                get
                {
                    return this.template_idField;
                }
                set
                {
                    this.template_idField = value;
                }
            }

            public object thumburl
            {
                get
                {
                    return this.thumburlField;
                }
                set
                {
                    this.thumburlField = value;
                }
            }

            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            public byte type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }

            public object username
            {
                get
                {
                    return this.usernameField;
                }
                set
                {
                    this.usernameField = value;
                }
            }

            public MM.appmsgWeappinfo weappinfo
            {
                get
                {
                    return this.weappinfoField;
                }
                set
                {
                    this.weappinfoField = value;
                }
            }

            public object websearch
            {
                get
                {
                    return this.websearchField;
                }
                set
                {
                    this.websearchField = value;
                }
            }

            public MM.appmsgWebviewshared webviewshared
            {
                get
                {
                    return this.webviewsharedField;
                }
                set
                {
                    this.webviewsharedField = value;
                }
            }
        }

        [ProtoContract]
        public class AppMsg
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string appId;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string content;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string from;
            [ProtoMember(15)]
            public string jsAppId;
            [ProtoMember(12)]
            public string msgSource;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public int remindId;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint sdkVersion;
            [ProtoMember(14)]
            public string shareUrlOpen;
            [ProtoMember(13)]
            public string shareUrlOriginal;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int source;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString thumb;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string to;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint type;
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgAppattach
        {
            private string aeskeyField;
            private object attachidField;
            private string cdnthumbaeskeyField;
            private ushort cdnthumbheightField;
            private uint cdnthumblengthField;
            private string cdnthumbmd5Field;
            private string cdnthumburlField;
            private ushort cdnthumbwidthField;
            private byte encryverField;
            private object fileextField;
            private byte islargefilemsgField;

            public string aeskey
            {
                get
                {
                    return this.aeskeyField;
                }
                set
                {
                    this.aeskeyField = value;
                }
            }

            public object attachid
            {
                get
                {
                    return this.attachidField;
                }
                set
                {
                    this.attachidField = value;
                }
            }

            public string cdnthumbaeskey
            {
                get
                {
                    return this.cdnthumbaeskeyField;
                }
                set
                {
                    this.cdnthumbaeskeyField = value;
                }
            }

            public ushort cdnthumbheight
            {
                get
                {
                    return this.cdnthumbheightField;
                }
                set
                {
                    this.cdnthumbheightField = value;
                }
            }

            public uint cdnthumblength
            {
                get
                {
                    return this.cdnthumblengthField;
                }
                set
                {
                    this.cdnthumblengthField = value;
                }
            }

            public string cdnthumbmd5
            {
                get
                {
                    return this.cdnthumbmd5Field;
                }
                set
                {
                    this.cdnthumbmd5Field = value;
                }
            }

            public string cdnthumburl
            {
                get
                {
                    return this.cdnthumburlField;
                }
                set
                {
                    this.cdnthumburlField = value;
                }
            }

            public ushort cdnthumbwidth
            {
                get
                {
                    return this.cdnthumbwidthField;
                }
                set
                {
                    this.cdnthumbwidthField = value;
                }
            }

            public byte encryver
            {
                get
                {
                    return this.encryverField;
                }
                set
                {
                    this.encryverField = value;
                }
            }

            public object fileext
            {
                get
                {
                    return this.fileextField;
                }
                set
                {
                    this.fileextField = value;
                }
            }

            public byte islargefilemsg
            {
                get
                {
                    return this.islargefilemsgField;
                }
                set
                {
                    this.islargefilemsgField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgCanvasPageItem
        {
            private string canvasPageXmlField;

            public string canvasPageXml
            {
                get
                {
                    return this.canvasPageXmlField;
                }
                set
                {
                    this.canvasPageXmlField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgDesignershared
        {
            private string designernameField;
            private string designerrediretcturlField;
            private byte designeruinField;

            public string designername
            {
                get
                {
                    return this.designernameField;
                }
                set
                {
                    this.designernameField = value;
                }
            }

            public string designerrediretcturl
            {
                get
                {
                    return this.designerrediretcturlField;
                }
                set
                {
                    this.designerrediretcturlField = value;
                }
            }

            public byte designeruin
            {
                get
                {
                    return this.designeruinField;
                }
                set
                {
                    this.designeruinField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgEmoticongift
        {
            private byte packageflagField;
            private object packageidField;

            public byte packageflag
            {
                get
                {
                    return this.packageflagField;
                }
                set
                {
                    this.packageflagField = value;
                }
            }

            public object packageid
            {
                get
                {
                    return this.packageidField;
                }
                set
                {
                    this.packageidField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgEmoticonshared
        {
            private byte packageflagField;
            private object packageidField;

            public byte packageflag
            {
                get
                {
                    return this.packageflagField;
                }
                set
                {
                    this.packageflagField = value;
                }
            }

            public object packageid
            {
                get
                {
                    return this.packageidField;
                }
                set
                {
                    this.packageidField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgEmotionpageshared
        {
            private string descField;
            private string iconUrlField;
            private byte pageTypeField;
            private object secondUrlField;
            private byte tidField;
            private string titleField;

            public string desc
            {
                get
                {
                    return this.descField;
                }
                set
                {
                    this.descField = value;
                }
            }

            public string iconUrl
            {
                get
                {
                    return this.iconUrlField;
                }
                set
                {
                    this.iconUrlField = value;
                }
            }

            public byte pageType
            {
                get
                {
                    return this.pageTypeField;
                }
                set
                {
                    this.pageTypeField = value;
                }
            }

            public object secondUrl
            {
                get
                {
                    return this.secondUrlField;
                }
                set
                {
                    this.secondUrlField = value;
                }
            }

            public byte tid
            {
                get
                {
                    return this.tidField;
                }
                set
                {
                    this.tidField = value;
                }
            }

            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgStreamvideo
        {
            private object streamvideoaduxinfoField;
            private object streamvideopublishidField;
            private object streamvideothumburlField;
            private object streamvideotitleField;
            private byte streamvideototaltimeField;
            private object streamvideourlField;
            private object streamvideoweburlField;
            private object streamvideowordingField;

            public object streamvideoaduxinfo
            {
                get
                {
                    return this.streamvideoaduxinfoField;
                }
                set
                {
                    this.streamvideoaduxinfoField = value;
                }
            }

            public object streamvideopublishid
            {
                get
                {
                    return this.streamvideopublishidField;
                }
                set
                {
                    this.streamvideopublishidField = value;
                }
            }

            public object streamvideothumburl
            {
                get
                {
                    return this.streamvideothumburlField;
                }
                set
                {
                    this.streamvideothumburlField = value;
                }
            }

            public object streamvideotitle
            {
                get
                {
                    return this.streamvideotitleField;
                }
                set
                {
                    this.streamvideotitleField = value;
                }
            }

            public byte streamvideototaltime
            {
                get
                {
                    return this.streamvideototaltimeField;
                }
                set
                {
                    this.streamvideototaltimeField = value;
                }
            }

            public object streamvideourl
            {
                get
                {
                    return this.streamvideourlField;
                }
                set
                {
                    this.streamvideourlField = value;
                }
            }

            public object streamvideoweburl
            {
                get
                {
                    return this.streamvideoweburlField;
                }
                set
                {
                    this.streamvideoweburlField = value;
                }
            }

            public object streamvideowording
            {
                get
                {
                    return this.streamvideowordingField;
                }
                set
                {
                    this.streamvideowordingField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgWeappinfo
        {
            private string appidField;
            private byte appservicetypeField;
            private string pagepathField;
            private string shareIdField;
            private byte typeField;
            private string usernameField;
            private byte versionField;
            private string weappiconurlField;

            public string appid
            {
                get
                {
                    return this.appidField;
                }
                set
                {
                    this.appidField = value;
                }
            }

            public byte appservicetype
            {
                get
                {
                    return this.appservicetypeField;
                }
                set
                {
                    this.appservicetypeField = value;
                }
            }

            public string pagepath
            {
                get
                {
                    return this.pagepathField;
                }
                set
                {
                    this.pagepathField = value;
                }
            }

            public string shareId
            {
                get
                {
                    return this.shareIdField;
                }
                set
                {
                    this.shareIdField = value;
                }
            }

            public byte type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            public string username
            {
                get
                {
                    return this.usernameField;
                }
                set
                {
                    this.usernameField = value;
                }
            }

            public byte version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            public string weappiconurl
            {
                get
                {
                    return this.weappiconurlField;
                }
                set
                {
                    this.weappiconurlField = value;
                }
            }
        }

        [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true)]
        public class appmsgWebviewshared
        {
            private object jsAppIdField;
            private object publisherIdField;
            private object shareUrlOpenField;
            private object shareUrlOriginalField;

            public object jsAppId
            {
                get
                {
                    return this.jsAppIdField;
                }
                set
                {
                    this.jsAppIdField = value;
                }
            }

            public object publisherId
            {
                get
                {
                    return this.publisherIdField;
                }
                set
                {
                    this.publisherIdField = value;
                }
            }

            public object shareUrlOpen
            {
                get
                {
                    return this.shareUrlOpenField;
                }
                set
                {
                    this.shareUrlOpenField = value;
                }
            }

            public object shareUrlOriginal
            {
                get
                {
                    return this.shareUrlOriginalField;
                }
                set
                {
                    this.shareUrlOriginalField = value;
                }
            }
        }

        [ProtoContract]
        public class AuthParam
        {
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ a2Key;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string authKey;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public int authResultFlag;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string authTicket;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ autoAuthKey;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ cliDbencryptInfo;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ cliDbencryptKey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.Ecdh ecdh;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string fsurl;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public int mmtlsControlBitFlag;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public int newVersion;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public int serverTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SessionKey session;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public MM.ShowStyleKey showStyleKey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public long uin;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public int updateFlag;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.WTLoginImgReqInfo wtloginImgRespInfo;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ wtloginRspBuff;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint wtloginRspBuffFlag;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.WxVerifyCodeReqInfo wxVerifyCodeRespInfo;
        }

        [ProtoContract]
        public class AuthResult
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int code;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.ErrMsg err_msg;
        }

        [ProtoContract]
        public class AutoAuthAesReqData
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ autoAuthKey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MMPro.MM.BaseAuthReqInfo BaseAuthReqInfo;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int builtinIpseq;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public int channel;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ clientCheckData;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string clientSeqId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string deviceName;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string deviceType;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string imei;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string language;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string softType;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string timeZone;
        }

        [ProtoContract]
        public class AutoAuthKey
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.AesKey encryptKey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ key;
        }

        [ProtoContract]
        public class AutoAuthRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.AutoAuthAesReqData aesReqData;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.AutoAuthRsaReqData rsaReqData;
        }

        [ProtoContract]
        public class AutoAuthRsaReqData
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.AesKey aesEncryptKey;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.EcdhKey cliPubEcdhKey;
        }

        [ProtoContract]
        public class BaseAuthReqInfo
        {
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint Authreqflag;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string Authticket;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ cliDbencryptInfo;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Clidbencryptke;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.WTLoginImgReqInfo Wtloginimgreqi;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Wtloginreqbuff;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.WxVerifyCodeReqInfo Wxverifycodere;
        }

        [ProtoContract]
        public class BaseRequest
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int clientVersion;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] devicelId;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string osType;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int scene;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public byte[] sessionKey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int uin;
        }

        [ProtoContract]
        public class BaseResponse
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString errMsg;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.RetConst ret;
        }

        [ProtoContract]
        public sealed class BatchGetFavItemRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baserequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int[] favIdList;
        }

        [ProtoContract]
        public class BatchGetFavItemResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.FavObject[] objectList;
        }

        [ProtoContract]
        public class BatchGetHeadImgRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] userNameList;
        }

        [ProtoContract]
        public class BatchGetHeadImgResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.ImgPair[] imgPairList;
        }

        [ProtoContract]
        public class BizApiInfo
        {
            [ProtoMember(1)]
            public string apiName;
        }

        [ProtoContract]
        public class BizScopeInfo
        {
            [ProtoMember(4)]
            public uint apiCount;
            [ProtoMember(5)]
            public MM.BizApiInfo[] apiList;
            [ProtoMember(1)]
            public string scope;
            [ProtoMember(3)]
            public string scopeDesc;
            [ProtoMember(2)]
            public uint scopeStatus;
        }

        [ProtoContract]
        public class BuiltinIP
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string domain;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string ip;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint port;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint type;
        }

        [ProtoContract]
        public class BuiltinIPList
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint longConnectIpcount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.BuiltinIP[] longConnectIplist;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint seq;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint shortconnectIpcount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.BuiltinIP[] shortConnectIplist;
        }

        [ProtoContract]
        public class CanvasInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string dataBuffer;
        }

        [ProtoContract]
        public class CDNAUTHINFO
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public byte[] aesKey;
        }

        [ProtoContract]
        public class CDNClientConfig
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int c2CretryInterval;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int c2Crwtimeout;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int c2CshowErrorDelayMs;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int snsretryInterval;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int snsrwtimeout;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int snsshowErrorDelayMs;
        }

        [ProtoContract]
        public class CDNDnsInfo
        {
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ authKey;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint expireTime;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public uint fakeUin;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] fontIPList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int frontID;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int frontIPCount;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public int frontIPPortCount;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.CDNDnsPortInfo[] frontIPPortList;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ newAuthkey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint uin;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint ver;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string zoneDomain;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public int zoneID;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int zoneIPCount;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] zoneIPList;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public int zoneIPPortCount;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public MM.CDNDnsPortInfo[] zoneIPPortList;
        }

        [ProtoContract]
        public class CDNDnsPortInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint portCount;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint[] portList;
        }

        [XmlRoot("msg")]
        public class CDNMSG
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private MM.msgImg <img>k__BackingField;

            [XmlElement("img")]
            public MM.msgImg img { get; set; }
        }

        [XmlRoot("msg")]
        public class CDNMSG_VIDEO
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private MM.msgVideomsg <videomsg>k__BackingField;

            public MM.msgVideomsg videomsg { get; set; }
        }

        public enum CGI_TYPE
        {
            CGI_TYPE_ADCLICK = 0x4d0,
            CGI_TYPE_ADDCHATROOMMEMBER = 120,
            CGI_TYPE_ADDFAVITEM = 0x191,
            CGI_TYPE_ADEXPOSURE = 0x4cf,
            CGI_TYPE_BATCHGETFAVITEM = 0x192,
            CGI_TYPE_BATCHGETHEADIMG = 0x7b,
            CGI_TYPE_BINDQUERYNEW = 0x5dd,
            CGI_TYPE_CHECKLOGINQRCODE = 0x1f7,
            CGI_TYPE_CREATECHATROOM = 0x77,
            CGI_TYPE_DEFAULT = 0,
            CGI_TYPE_DELCHATROOMMEMBER = 0xb3,
            CGI_TYPE_DOWNLOADVOICE = 0x80,
            CGI_TYPE_EXTDEVICELOGINCONFIRMGET = 0x3cb,
            CGI_TYPE_F2FQRCODE = 0x634,
            CGI_TYPE_FAVSYNC = 400,
            CGI_TYPE_GETA8KEY = 0xe9,
            CGI_TYPE_GETBANNERINFO = 0x68f,
            CGI_TYPE_GETCDNDNS = 0x17b,
            CGI_TYPE_GETCHATROOMINFODETAIL = 0xdf,
            CGI_TYPE_GETCHATROOMMEMBERDETAIL = 0x227,
            CGI_TYPE_GETCONTACT = 0xb6,
            CGI_TYPE_GETCONTACTLABELLIST = 0x27f,
            CGI_TYPE_GETEMOTIONDESC = 0x209,
            CGI_TYPE_GETFAVINFO = 0x1b6,
            CGI_TYPE_GETLOGINQRCODE = 0x1f6,
            CGI_TYPE_GETMSGIMG = 0x6d,
            CGI_TYPE_GETONLINEINFO = 0x20e,
            CGI_TYPE_GETOPENIMRESOURCE = 0x1c5,
            CGI_TYPE_GETPAYFUNCTIONLIST = 0x1ef,
            CGI_TYPE_GETPROFILE = 0x12e,
            CGI_TYPE_GETQRCODE = 0xa8,
            CGI_TYPE_GETTRANSFERWORDINH = 0x7c8,
            CGI_TYPE_HEARTBEAT = 0x206,
            CGI_TYPE_INITCONTACT = 0x353,
            CGI_TYPE_LBSFIND = 0x94,
            CGI_TYPE_MANUALAUTH = 0x2bd,
            CGI_TYPE_MASSSEND = 0xc1,
            CGI_TYPE_MMSNSCOMMENT = 0xd5,
            CGI_TYPE_MMSNSOBJECTDETAIL = 210,
            CGI_TYPE_MMSNSPORT = 0xd1,
            CGI_TYPE_MMSNSSYNC = 0xd6,
            CGI_TYPE_MMSNSTAGLIST = 0x124,
            CGI_TYPE_MMSNSTIMELINE = 0xd3,
            CGI_TYPE_MMSNSUPLOAD = 0xcf,
            CGI_TYPE_MMSNSUSERPAGE = 0xd4,
            CGI_TYPE_NEWGETINVITEFRIEND = 0x87,
            CGI_TYPE_NEWSENDMSG = 0x20a,
            CGI_TYPE_NEWSYNC = 0x8a,
            CGI_TYPE_OPENWXHB = 0x695,
            CGI_TYPE_OPLOG = 0x2a9,
            CGI_TYPE_PUSHLOGINURL = 0x28e,
            CGI_TYPE_QRYDETAILWXHB = 0x631,
            CGI_TYPE_QRYLISTWXHB = 0x5ea,
            CGI_TYPE_RECEIVEWXHB = 0x62d,
            CGI_TYPE_SEARCHCONTACT = 0x6a,
            CGI_TYPE_SENDAPPMSG = 0xde,
            CGI_TYPE_SENDEMOJI = 0xaf,
            CGI_TYPE_SETCHATROOMANNOUNCEMENT = 0x3e1,
            CGI_TYPE_SNSOBJECTOP = 0xda,
            CGI_TYPE_STATUSNOTIFY = 0xfb,
            CGI_TYPE_TENPAY = 0x181,
            CGI_TYPE_TIMESEED = 0x1dd,
            CGI_TYPE_TRANSFEROPERATION = 0x69b,
            CGI_TYPE_TRANSFERQUERY = 0x65c,
            CGI_TYPE_TRANSFERSETF2FFEE = 0x657,
            CGI_TYPE_UPLOADIMAGE = 0x271,
            CGI_TYPE_UPLOADMCONTACT = 0x85,
            CGI_TYPE_UPLOADVIDEO = 0x95,
            CGI_TYPE_UPLOADVOICE = 0x7f,
            CGI_TYPE_VERIFYUSER = 0x89,
            CGI_TYPE_WISHWXHB = 0x692
        }

        [ProtoContract]
        public class ChatInfo
        {
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public ulong clientMsgId;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string content;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string msgSource;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString toid;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int type;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public long utc;
        }

        [ProtoContract]
        public class ChatRoomMemberData
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.ChatRoomMemberInfo[] chatRoomMember;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint infoMask;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
        }

        [ProtoContract]
        public class ChatRoomMemberInfo
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint chatroomMemberFlag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string displayName;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string nickName;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class CheckLoginQRCodeRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.AesKey aes;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint opcode;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint timeStamp;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string uuid;
        }

        [ProtoContract]
        public class CheckLoginQRCodeResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.LoginQRCodeNotifyPkg data;
        }

        [ProtoContract]
        public class CloseMicroBlog
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString microBlogUserName;
        }

        public enum CmdConst
        {
            MM_CMDID_CancelQRPay = 0xc6,
            MM_CMDID_GenPrepay = 0xbd,
            MM_CMDID_GetBizIapDetail = 0xea,
            MM_CMDID_GetBizIapPayResult = 0xeb,
            MM_CMDID_GetLatestPayProductInfo = 0xe5,
            MM_CMDID_GetOrderList = 0xec,
            MM_CMDID_GetPayFunctionList = 0xe3,
            MM_CMDID_GetPayFunctionProductList = 0xe4,
            MM_CMDID_GetProductInfo = 0xdb,
            MM_CMDID_NEW_YEAR_SHAKE_REQ = 0x135,
            MM_CMDID_NEW_YEAR_SHAKE_RESP = 0x3b9acb35,
            MM_CMDID_PayAuthApp = 0xbc,
            MM_CMDID_PayDelUserRoll = 0xbb,
            MM_CMDID_PayQueryUserRoll = 0xba,
            MM_CMDID_PaySubscribe = 0xce,
            MM_CMDID_PreparePurchase = 0xd6,
            MM_CMDID_RcptInfoAdd = 200,
            MM_CMDID_RcptInfoQuery = 0xca,
            MM_CMDID_RcptInfoRemove = 0xc9,
            MM_CMDID_RcptInfoTouch = 0xcc,
            MM_CMDID_RcptInfoUpdate = 0xcb,
            MM_CMDID_SubmitPayProductBuyInfo = 230,
            MM_CMDID_TenPay = 0xb9,
            MM_CMDID_VerifyPurchase = 0xd7,
            MM_PKT_ADD_FAV_ITEM_REQ = 0xc1,
            MM_PKT_ADD_FAV_ITEM_RESP = 0x3b9acac1,
            MM_PKT_ADDCHATROOMMEMBER_REQ = 0x24,
            MM_PKT_ASYNCDOWNLOADVOICE_REQ = 0x16,
            MM_PKT_ASYNCDOWNLOADVOICE_RESP = 0x3b9aca16,
            MM_PKT_AUTH_RESP = 0x3b9aca01,
            MM_PKT_BAKCHAT_RECOVER_DATA_REQ = 140,
            MM_PKT_BAKCHAT_RECOVER_DATA_RESP = 0x3b9aca8c,
            MM_PKT_BAKCHAT_RECOVER_DELETE_REQ = 0x8d,
            MM_PKT_BAKCHAT_RECOVER_DELETE_RESP = 0x3b9aca8d,
            MM_PKT_BAKCHAT_RECOVER_GETLIST_REQ = 0x8a,
            MM_PKT_BAKCHAT_RECOVER_GETLIST_RESP = 0x3b9aca8a,
            MM_PKT_BAKCHAT_RECOVER_HEAD_REQ = 0x8b,
            MM_PKT_BAKCHAT_RECOVER_HEAD_RESP = 0x3b9aca8b,
            MM_PKT_BAKCHAT_UPLOAD_END_REQ = 0x87,
            MM_PKT_BAKCHAT_UPLOAD_END_RESP = 0x3b9aca87,
            MM_PKT_BAKCHAT_UPLOAD_HEAD_REQ = 0x86,
            MM_PKT_BAKCHAT_UPLOAD_HEAD_RESP = 0x3b9aca86,
            MM_PKT_BAKCHAT_UPLOAD_MEDIA_REQ = 0x89,
            MM_PKT_BAKCHAT_UPLOAD_MEDIA_RESP = 0x3b9aca89,
            MM_PKT_BAKCHAT_UPLOAD_MSG_REQ = 0x88,
            MM_PKT_BAKCHAT_UPLOAD_MSG_RESP = 0x3b9aca88,
            MM_PKT_BATCH_DEL_FAV_ITEM_REQ = 0xc2,
            MM_PKT_BATCH_DEL_FAV_ITEM_RESP = 0x3b9acac2,
            MM_PKT_BATCH_GET_SHAKE_TRAN_IMG_REQ = 0x81,
            MM_PKT_BATCH_GET_SHAKE_TRAN_IMG_RESP = 0x3b9aca81,
            MM_PKT_BATCHGETCONTACTPROFILE_REQ = 0x1c,
            MM_PKT_BATCHGETCONTACTPROFILE_RESP = 0x3b9aca1c,
            MM_PKT_BULLETIN_REQ = 0x48,
            MM_PKT_CHECKUNBIND_REQ = 0x83,
            MM_PKT_CHECKUNBIND_RESP = 0x3b9aca83,
            MM_PKT_CLICK_COMMAND_REQ = 0xb0,
            MM_PKT_CLICK_COMMAND_RESP = 0x3b9acab0,
            MM_PKT_CONNCONTROL_REQ = 11,
            MM_PKT_CREATECHATROOM_REQ = 0x25,
            MM_PKT_DEL_SAFEDEVICE_REQ = 0xac,
            MM_PKT_DEL_SAFEDEVICE_RESP = 0x3b9acaac,
            MM_PKT_DIRECTSEND_REQ = 8,
            MM_PKT_DOWNLOAD_APP_ATTACH_REQ = 0x6a,
            MM_PKT_DOWNLOAD_APP_ATTACH_RESP = 0x3b9aca6a,
            MM_PKT_DOWNLOADVIDEO_REQ = 40,
            MM_PKT_DOWNLOADVOICE_REQ = 20,
            MM_PKT_DOWNLOADVOICE_RESP = 0x3b9aca14,
            MM_PKT_EXCHANGE_EMOTION_PACK_REQ = 0xd5,
            MM_PKT_EXCHANGE_EMOTION_PACK_RESP = 0x3b9acad5,
            MM_PKT_EXPOSE_REQ = 0x3b,
            MM_PKT_EXPOSE_RESP = 0x3b9aca3b,
            MM_PKT_FAV_CHECKCDN_REQ = 0xc5,
            MM_PKT_FAV_CHECKCDN_RESP = 0x3b9acac5,
            MM_PKT_FAV_SYNC_REQ = 0xc3,
            MM_PKT_FAV_SYNC_RESP = 0x3b9acac3,
            MM_PKT_FAVNOTIFY_REQ = 0xc0,
            MM_PKT_FIXSYNCCHECK_REQ = 30,
            MM_PKT_FIXSYNCCHECK_RESP = 0x3b9aca1e,
            MM_PKT_GENERALSET_REQ = 70,
            MM_PKT_GENERALSET_RESP = 0x3b9aca46,
            MM_PKT_GET_APP_INFO__RESP = 0x3b9aca6c,
            MM_PKT_GET_APP_INFO_REQ = 0x6c,
            MM_PKT_GET_CERT_REQ = 0xb3,
            MM_PKT_GET_CERT_RESP = 0x3b9acab3,
            MM_PKT_GET_EMOTION_DETAIL_REQ = 0xd3,
            MM_PKT_GET_EMOTION_DETAIL_RESP = 0x3b9acad3,
            MM_PKT_GET_EMOTION_LIST_REQ = 210,
            MM_PKT_GET_EMOTION_LIST_RESP = 0x3b9acad2,
            MM_PKT_GET_FAV_INFO_REQ = 0xd9,
            MM_PKT_GET_FAV_INFO_RESP = 0x3b9acad9,
            MM_PKT_GET_PROFILE_REQ = 0x76,
            MM_PKT_GET_PROFILE_RESP = 0x3b9aca76,
            MM_PKT_GET_QRCODE_REQ = 0x43,
            MM_PKT_GET_QRCODE_RESP = 0x3b9aca43,
            MM_PKT_GET_RECOMMEND_APP_LIST_REQ = 0x6d,
            MM_PKT_GET_RECOMMEND_APP_LIST_RESP = 0x3b9aca6d,
            MM_PKT_GETA8KEY_REQ = 0x9b,
            MM_PKT_GETA8KEY_RESP = 0x3b9aca9b,
            MM_PKT_GETCONTACT_REQ = 0x47,
            MM_PKT_GETCONTACT_RESP = 0x3b9aca47,
            MM_PKT_GETINVITEFRIEND_REQ = 0x12,
            MM_PKT_GETINVITEFRIEND_RESP = 0x3b9aca12,
            MM_PKT_GETMSGIMG_REQ = 10,
            MM_PKT_GETMSGIMG_RESP = 0x3b9aca0a,
            MM_PKT_GETPSMIMG_REQ = 0x1d,
            MM_PKT_GETPSMIMG_RESP = 0x3b9aca1d,
            MM_PKT_GETQQGROUP_REQ = 0x26,
            MM_PKT_GETQQGROUP_RESP = 0x3b9aca26,
            MM_PKT_GETUPDATEINFO_REQ = 0x23,
            MM_PKT_GETUPDATEPACK_REQ = 0x10,
            MM_PKT_GETUPDATEPACK_RESP = 0x3b9aca10,
            MM_PKT_GETUSERIMG_REQ = 15,
            MM_PKT_GETUSERIMG_RESP = 0x3b9aca0f,
            MM_PKT_GETUSERNAME_REQ = 0x21,
            MM_PKT_GETUSERNAME_RESP = 0x3b9aca21,
            MM_PKT_GETVERIFYIMG_REQ = 0x30,
            MM_PKT_GETVUSERINFO_REQ = 60,
            MM_PKT_GETVUSERINFO_RESP = 0x3b9aca3c,
            MM_PKT_INIT_REQ = 14,
            MM_PKT_INIT_RESP = 0x3b9aca0e,
            MM_PKT_INVALID_REQ = 0,
            MM_PKT_KvReportRsa_REQ = 0xda,
            MM_PKT_KvReportRsa_RESP = 0x3b9acada,
            MM_PKT_LBS_ROOM_MEMBER_REQ = 0xb8,
            MM_PKT_LBS_ROOM_MEMBER_RESP = 0x3b9acab8,
            MM_PKT_LBS_ROOM_REQ = 0xb7,
            MM_PKT_LBS_ROOM_RESP = 0x3b9acab7,
            MM_PKT_MASS_SEND_REQ = 0x54,
            MM_PKT_MASS_SEND_RESP = 0x3b9aca54,
            MM_PKT_MOD_EMOTION_PACK_REQ = 0xd4,
            MM_PKT_MOD_EMOTION_PACK_RESP = 0x3b9acad4,
            MM_PKT_MOD_FAV_ITEM_REQ = 0xd8,
            MM_PKT_MOD_FAV_ITEM_RESP = 0x3b9acad8,
            MM_PKT_NEW_AUTH_REQ = 0xb2,
            MM_PKT_NEW_AUTH_RESP = 0x3b9acab2,
            MM_PKT_NEWGETINVITEFRIEND_REQ = 0x17,
            MM_PKT_NEWGETINVITEFRIEND_RESP = 0x3b9aca17,
            MM_PKT_NEWINIT_REQ = 0x1b,
            MM_PKT_NEWINIT_RESP = 0x3b9aca1b,
            MM_PKT_NEWNOTIFY_REQ = 0x18,
            MM_PKT_NEWNOTIFY_RESP = 0x3b9aca18,
            MM_PKT_NEWREG_REQ = 0x20,
            MM_PKT_NEWREG_RESP = 0x3b9aca20,
            MM_PKT_NEWSENDMSG_REQ = 0xed,
            MM_PKT_NEWSENDMSG_RESP = 0x3b9acaed,
            MM_PKT_NEWSYNC_REQ = 0x1a,
            MM_PKT_NEWSYNC_RESP = 0x3b9aca1a,
            MM_PKT_NEWSYNCCHECK_REQ = 0x19,
            MM_PKT_NEWSYNCCHECK_RESP = 0x3b9aca19,
            MM_PKT_NOOP_REQ = 6,
            MM_PKT_NOOP_RESP = 0x3b9aca06,
            MM_PKT_NOTIFY_REQ = 5,
            MM_PKT_PREPARE_PURCHASE_REQ = 0xd6,
            MM_PKT_PREPARE_PURCHASE_RESP = 0x3b9acad6,
            MM_PKT_QUERY_HAS_PASSWD_REQ = 0x84,
            MM_PKT_QUERY_HAS_PASSWD_RESP = 0x3b9aca84,
            MM_PKT_QUIT_REQ = 7,
            MM_PKT_REDIRECT_REQ = 12,
            MM_PKT_REG_REQ = 0x1f,
            MM_PKT_REG_RESP = 0x3b9aca1f,
            MM_PKT_SEARCHCONTACT_REQ = 0x22,
            MM_PKT_SEARCHFRIEND_REQ = 0x11,
            MM_PKT_SEARCHFRIEND_RESP = 0x3b9aca11,
            MM_PKT_SEND_APP_MSG__RESP = 0x3b9aca6b,
            MM_PKT_SEND_APP_MSG_REQ = 0x6b,
            MM_PKT_SENDCARD_REQ = 0x2a,
            MM_PKT_SENDCARD_RESP = 0x3b9aca2a,
            MM_PKT_SENDINVITEMAIL_REQ = 0x29,
            MM_PKT_SENDMSG_RESP = 0x3b9aca02,
            MM_PKT_SENDVERIFYMAIL_REQ = 0x2b,
            MM_PKT_SET_PWD_REQ = 180,
            MM_PKT_SET_PWD_RESP = 0x3b9acab4,
            MM_PKT_SHAKE_MUSIC_REQ = 0xb1,
            MM_PKT_SHAKE_MUSIC_RESP = 0x3b9acab1,
            MM_PKT_SHAKE_TRAN_IMG_GET_REQ = 0x80,
            MM_PKT_SHAKE_TRAN_IMG_GET_RESP = 0x3b9aca80,
            MM_PKT_SHAKE_TRAN_IMG_REPORT_REQ = 0x7f,
            MM_PKT_SHAKE_TRAN_IMG_REPORT_RESP = 0x3b9aca7f,
            MM_PKT_SHAKE_TRAN_IMG_UNBIND_REQ = 130,
            MM_PKT_SHAKE_TRAN_IMG_UNBIND_RESP = 0x3b9aca82,
            MM_PKT_SHAKEGET_REQ = 0x39,
            MM_PKT_SHAKEGET_RESP = 0x3b9aca39,
            MM_PKT_SHAKEREPORT_REQ = 0x38,
            MM_PKT_SHAKEREPORT_RESP = 0x3b9aca38,
            MM_PKT_SNS_OBJECT_OP_REQ = 0x68,
            MM_PKT_SNS_OBJECT_OP_RESP = 0x3b9aca68,
            MM_PKT_SNS_POST_REQ = 0x61,
            MM_PKT_SNS_POST_RESP = 0x3b9aca61,
            MM_PKT_SNS_SYNC_REQ = 0x66,
            MM_PKT_SNS_SYNC_RESP = 0x3b9aca66,
            MM_PKT_SNS_TAG_LIST_REQ = 0x74,
            MM_PKT_SNS_TAG_LIST_RESP = 0x3b9aca74,
            MM_PKT_SNS_TAG_MEMBER_MUTIL_SET_REQ = 0x75,
            MM_PKT_SNS_TAG_MEMBER_MUTIL_SET_RESP = 0x3b9aca75,
            MM_PKT_SNS_TAG_MEMBER_OP_REQ = 0x73,
            MM_PKT_SNS_TAG_MEMBER_OP_RESP = 0x3b9aca73,
            MM_PKT_SNS_TAG_OP_REQ = 0x72,
            MM_PKT_SNS_TAG_OP_RESP = 0x3b9aca72,
            MM_PKT_SNS_TIME_LINE_REQ = 0x62,
            MM_PKT_SNS_TIME_LINE_RESP = 0x3b9aca62,
            MM_PKT_SNS_UPLOAD_REQ = 0x5f,
            MM_PKT_SNS_UPLOAD_RESP = 0x3b9aca5f,
            MM_PKT_SNS_USER_PAGE_REQ = 0x63,
            MM_PKT_SNS_USER_PAGE_RESP = 0x3b9aca63,
            MM_PKT_SNSCOMMENT_REQ = 100,
            MM_PKT_SNSOBJECTDETAIL_REQ = 0x65,
            MM_PKT_SYNC_REQ = 3,
            MM_PKT_SYNC_RESP = 0x3b9aca03,
            MM_PKT_SYNCHECK_REQ = 13,
            MM_PKT_SYNCHECK_RESP = 0x3b9aca0d,
            MM_PKT_TALKROOMADDMEMBER_REQ = 0xa9,
            MM_PKT_TALKROOMADDMEMBER_RESP = 0x3b9acaa9,
            MM_PKT_TALKROOMCREATE_REQ = 0xa8,
            MM_PKT_TALKROOMCREATE_RESP = 0x3b9acaa8,
            MM_PKT_TALKROOMDELMEMBER_REQ = 170,
            MM_PKT_TALKROOMDELMEMBER_RESP = 0x3b9acaaa,
            MM_PKT_TALKROOMENTER_REQ = 0x93,
            MM_PKT_TALKROOMENTER_RESP = 0x3b9aca93,
            MM_PKT_TALKROOMEXIT_REQ = 0x94,
            MM_PKT_TALKROOMEXIT_RESP = 0x3b9aca94,
            MM_PKT_TALKROOMINVITE_REQ = 0xae,
            MM_PKT_TALKROOMINVITE_RESP = 0x3b9acaae,
            MM_PKT_TALKROOMMICACTION_REQ = 0x92,
            MM_PKT_TALKROOMMICACTION_RESP = 0x3b9aca92,
            MM_PKT_TALKROOMNOOP_REQ = 0x95,
            MM_PKT_TALKROOMNOOP_RESP = 0x3b9aca95,
            MM_PKT_TOKEN_REQ = 4,
            MM_PKT_TOKEN_RESP = 0x3b9aca04,
            MM_PKT_UPDATE_SAFEDEVICE_REQ = 0xab,
            MM_PKT_UPDATE_SAFEDEVICE_RESP = 0x3b9acaab,
            MM_PKT_UPLOAD_APP_ATTACH_REQ = 0x69,
            MM_PKT_UPLOAD_APP_ATTACH_RESP = 0x3b9aca69,
            MM_PKT_UPLOADMSGIMG_REQ = 9,
            MM_PKT_UPLOADMSGIMG_RESP = 0x3b9aca09,
            MM_PKT_UPLOADVIDEO_REQ = 0x27,
            MM_PKT_UPLOADVOICE_REQ = 0x13,
            MM_PKT_UPLOADVOICE_RESP = 0x3b9aca13,
            MM_PKT_UPLOADWEIBOIMG_REQ = 0x15,
            MM_PKT_UPLOADWEIBOIMG_RESP = 0x3b9aca15,
            MM_PKT_VERIFY_PURCHASE_REQ = 0xd7,
            MM_PKT_VERIFY_PURCHASE_RESP = 0x3b9acad7,
            MM_PKT_VERIFY_PWD_REQ = 0xb6,
            MM_PKT_VERIFY_PWD_RESP = 0x3b9acab6,
            MM_PKT_VERIFYUSER = 0x2c,
            MM_PKT_VOICETRANS_NOTIFY = 0xf1,
            MM_PKT_VOIP_REQ = 120,
            MM_PKT_VOIPANSWER_REQ = 0x41,
            MM_PKT_VOIPANSWER_RESP = 0x3b9aca41,
            MM_PKT_VOIPCANCELINVITE_REQ = 0x40,
            MM_PKT_VOIPCANCELINVITE_RESP = 0x3b9aca40,
            MM_PKT_VOIPHEARTBEAT_REQ = 0x51,
            MM_PKT_VOIPHEARTBEAT_RESP = 0x3b9aca51,
            MM_PKT_VOIPINVITE_REQ = 0x3f,
            MM_PKT_VOIPINVITE_RESP = 0x3b9aca3f,
            MM_PKT_VOIPINVITEREMIND_REQ = 0x7d,
            MM_PKT_VOIPINVITEREMIND_RESP = 0x3b9aca7d,
            MM_PKT_VOIPNOTIFY_REQ = 0x3d,
            MM_PKT_VOIPNOTIFY_RESP = 0x3b9aca3d,
            MM_PKT_VOIPSHUTDOWN_REQ = 0x42,
            MM_PKT_VOIPSHUTDOWN_RESP = 0x3b9aca42,
            MM_PKT_VOIPSYNC_REQ = 0x3e,
            MM_PKT_VOIPSYNC_RESP = 0x3b9aca3e
        }

        [ProtoContract]
        public class CmdItem
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public DATA cmdBuf;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SyncCmdID cmdid;

            [ProtoContract]
            public class DATA
            {
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public byte[] data;
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public int len;
            }
        }

        [ProtoContract]
        public class CmdList
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int count;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.CmdItem[] list;
        }

        public class CMsgContext
        {
            public string aesKey = "";
            public string bigUrlKey = "";
            public int cdnhdheight;
            public int cdnhdwidth;
            public int cdnmidheight;
            public int cdnmidwidth;
            public string cdnthumbaeskey = "";
            public int cdnthumbheight;
            public int cdnthumblength;
            public string cdnthumburl = "";
            public int cdnthumbwidth;
            public string cdnvideourl = "";
            public byte[] data;
            public int encryVer;
            public string fromusername = "";
            public int hdlength;
            public int isad;
            public int length;
            public string md5 = "";
            public string midUrlKey = "";
            public string newmd5 = "";
            public int playlength;
            public string thumbaesKey = "";
            public int thumblength;
            public string thumbUrlKey = "";

            public bool isCdnImgMsg()
            {
                if (this.aesKey.Length < 0x10)
                {
                    return false;
                }
                return true;
            }

            public bool isCdnImgMsgWithThumb()
            {
                if (!this.isCdnImgMsg())
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(this.thumbUrlKey))
                {
                    return false;
                }
                return true;
            }

            public static MM.CMsgContext parseImageContent(string xmlContent)
            {
                MM.CMsgContext context = new MM.CMsgContext();
                if (string.IsNullOrEmpty(xmlContent))
                {
                    context.length = 0;
                    context.hdlength = 0;
                    return context;
                }
                string text = xmlContent;
                XElement element = null;
                try
                {
                    element = XDocument.Parse(text).Element("msg");
                    string name = "img";
                    XElement element2 = element.Element("img");
                    if (element.Element("img") > null)
                    {
                        name = "imgmsg";
                    }
                    else if (element.Element("videomsg") > null)
                    {
                        name = "videomsg";
                    }
                    XAttribute attribute = element.Element(name).Attribute("length");
                    if (attribute > null)
                    {
                        context.length = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("hdlength");
                    if (attribute > null)
                    {
                        context.hdlength = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnthumblength");
                    if (attribute > null)
                    {
                        context.cdnthumblength = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnthumbheight");
                    if (attribute > null)
                    {
                        context.cdnthumbheight = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnthumbwidth");
                    if (attribute > null)
                    {
                        context.cdnthumbwidth = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnmidheight");
                    if (attribute > null)
                    {
                        context.cdnmidheight = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnmidwidth");
                    if (attribute > null)
                    {
                        context.cdnmidwidth = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnhdheight");
                    if (attribute > null)
                    {
                        context.cdnhdheight = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("md5");
                    if (attribute > null)
                    {
                        context.md5 = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnhdwidth");
                    if (attribute > null)
                    {
                        context.cdnhdwidth = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("cdnthumbaeskey");
                    if (attribute > null)
                    {
                        context.cdnthumbaeskey = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnthumblength");
                    if (attribute > null)
                    {
                        context.thumblength = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("encryver");
                    if (attribute > null)
                    {
                        context.encryVer = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("aeskey");
                    if (attribute > null)
                    {
                        context.aesKey = attribute.Value;
                        context.thumbaesKey = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnthumbaeskey");
                    if (attribute > null)
                    {
                        context.thumbaesKey = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnmidimgurl");
                    if (attribute > null)
                    {
                        context.midUrlKey = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnbigimgurl");
                    if (attribute > null)
                    {
                        context.bigUrlKey = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnthumburl");
                    if (attribute > null)
                    {
                        context.thumbUrlKey = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnvideourl");
                    if (attribute > null)
                    {
                        context.cdnvideourl = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("cdnthumburl");
                    if (attribute > null)
                    {
                        context.cdnthumburl = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("fromusername");
                    if (attribute > null)
                    {
                        context.fromusername = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("newmd5");
                    if (attribute > null)
                    {
                        context.newmd5 = attribute.Value;
                    }
                    attribute = element.Element(name).Attribute("isad");
                    if (attribute > null)
                    {
                        context.isad = int.Parse(attribute.Value);
                    }
                    attribute = element.Element(name).Attribute("playlength");
                    if (attribute > null)
                    {
                        context.playlength = int.Parse(attribute.Value);
                    }
                }
                catch (Exception)
                {
                    context.length = 0;
                    context.hdlength = 0;
                    return context;
                }
                return context;
            }
        }

        [ProtoContract]
        public class CreateChatRoomRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(6)]
            public MM.SKBuiltinString_ extBuffer;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.MemberReq[] memberList;
            [ProtoMember(5)]
            public uint scene;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString topic;
        }

        [ProtoContract]
        public class CreateChatRoomResponese
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(9)]
            public string bigHeadImgUrl;
            [ProtoMember(7)]
            public MM.SKBuiltinString chatRoomName;
            [ProtoMember(8)]
            public MM.SKBuiltinString_ imgBuf;
            [ProtoMember(5)]
            public uint memberCount;
            [ProtoMember(6)]
            public MM.MemberResp[] memberList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString pYInitial;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString quanPin;
            [ProtoMember(10)]
            public string smallHeadImgUrl;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString topic;
        }

        [ProtoContract]
        public class CustomizedInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint brandFlag;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string brandIconURL;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string brandInfo;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string externalInfo;
        }

        [ProtoContract]
        public class DeepLinkBitSet
        {
            [ProtoMember(1)]
            public ulong bitValue;
        }

        [ProtoContract]
        public class DelChatContact
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString UserName;
        }

        [ProtoContract]
        public class DelChatRoomMemberRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string chatRoomName;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.DelMemberReq[] memberList;
        }

        [ProtoContract]
        public class DelChatRoomMemberResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint memberCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.DelMemberResp memberList;
        }

        [ProtoContract]
        public class DelContact
        {
            [ProtoMember(2)]
            public uint deleteContactScene;
            [ProtoMember(1)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class DelContactMsg
        {
            [ProtoMember(2)]
            public int maxMsgId;
            [ProtoMember(1)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class DelMemberReq
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString memberName;
        }

        [ProtoContract]
        public class DelMemberResp
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString memberName;
        }

        [ProtoContract]
        public class DelMsg
        {
            [ProtoMember(2)]
            public uint count;
            [ProtoMember(3)]
            public int[] msgIdList;
            [ProtoMember(1)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class DelUserDomainEmail
        {
            [ProtoMember(2)]
            public MM.SKBuiltinString email;
            [ProtoMember(1)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class DisturbSetting
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint allDaySetting;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.DisturbTimeSpan alldayTime;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint nightSetting;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.DisturbTimeSpan nightTime;
        }

        [ProtoContract]
        public class DisturbTimeSpan
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint beginTime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint endTime;
        }

        [ProtoContract]
        public class DownloadVoiceRequest
        {
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string Chatroomname;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint length;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public ulong MasterbufId;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public ulong Newmsgid;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint offset;
        }

        [ProtoContract]
        public class DownloadVoiceResponse
        {
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public uint Cancelflag;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string Clientmsgid;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Data;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint Endflag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint length;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public ulong Newmsgid;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint offset;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint Voicelength;
        }

        [ProtoContract]
        public class Ecdh
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.EcdhKey ecdhkey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int nid;
        }

        [ProtoContract]
        public class EcdhKey
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] key;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int len;
        }

        [ProtoContract]
        public class EmojiUploadInfo
        {
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ emojiBuffer;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string externXml;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string MD5;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string msgSource;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string report;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int startPos;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public int t11;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string to;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int totalLen;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int type;
        }

        [ProtoContract]
        public sealed class EmotionDesc
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public LangDesc[] list;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string md5;

            [ProtoContract]
            public sealed class LangDesc
            {
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public string desc;
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public string lang;
            }
        }

        public enum enMMScanQrcodeActionCode
        {
            MMSCAN_QRCODE_A8KEY = 0,
            MMSCAN_QRCODE_APP = 3,
            MMSCAN_QRCODE_EMOTICON = 20,
            MMSCAN_QRCODE_JUMP = 9,
            MMSCAN_QRCODE_MMPAY = 10,
            MMSCAN_QRCODE_MMPAY_NATIVE = 11,
            MMSCAN_QRCODE_PLUGIN = 5,
            MMSCAN_QRCODE_PROFILE = 4,
            MMSCAN_QRCODE_SPECIAL_WEBVIEW = 6,
            MMSCAN_QRCODE_TEXT = 1,
            MMSCAN_QRCODE_VCARD = 8,
            MMSCAN_QRCODE_WEBVIEW = 2,
            MMSCAN_QRCODE_WEBVIEW_NO_NOTICE = 7
        }

        public enum enMMTenPayCgiCmd
        {
            MMTENPAY_BIZ_CGICMD_PLATFORM_NOTIFY_CHECK_RESULT = 0x1b,
            MMTENPAY_BIZ_CGICMD_PLATFORM_QUERY_BZJ_INFO = 0x22,
            MMTENPAY_BIZ_CGICMD_PLATFORM_QUERY_CHECK_RECORD = 0x17,
            MMTENPAY_BIZ_CGICMD_PLATFORM_QUERY_CHECK_RESULT = 0x19,
            MMTENPAY_BIZ_CGICMD_QUERY_BIZ_CHECK_RESULT = 0x38,
            MMTENPAY_BIZ_CGICMD_QUERY_NEW_PARTNER_ID = 0x37,
            MMTENPAY_BIZ_CGICMD_WEB_NOTIFY_CHECK_RESULT = 0x1a,
            MMTENPAY_BIZ_CGICMD_WEB_QUERY_CHECK_RECORD = 0x16,
            MMTENPAY_BIZ_CGICMD_WEB_QUERY_CHECK_RESULT = 0x18,
            MMTENPAY_BIZ_CGICMD_WX_QRY_AUTH_INFO = 70,
            MMTENPAY_CGICMD_AUTHEN = 0,
            MMTENPAY_CGICMD_BANK_QUERY = 7,
            MMTENPAY_CGICMD_BANKCARDBIN_QUERY = 15,
            MMTENPAY_CGICMD_BIND_AUTHEN = 12,
            MMTENPAY_CGICMD_BIND_QUERY_NEW = 0x48,
            MMTENPAY_CGICMD_BIND_VERIFY = 13,
            MMTENPAY_CGICMD_BIND_VERIFY_REG = 0x11,
            MMTENPAY_CGICMD_CHANGE_PWD = 9,
            MMTENPAY_CGICMD_CHECK_PWD = 0x12,
            MMTENPAY_CGICMD_CHKPAYACC = 0x4f,
            MMTENPAY_CGICMD_ELEM_QUERY_NEW = 0x49,
            MMTENPAY_CGICMD_GEN_PRE_FETCH = 0x4b,
            MMTENPAY_CGICMD_GEN_PRE_SAVE = 0x4a,
            MMTENPAY_CGICMD_GEN_PRE_TRANSFER = 0x53,
            MMTENPAY_CGICMD_GET_FIXED_AMOUNT_QRCODE = 0x5e,
            MMTENPAY_CGICMD_IMPORT_BIND_QUERY = 0x25,
            MMTENPAY_CGICMD_IMPORT_ENCRYPT_QUERY = 0x26,
            MMTENPAY_CGICMD_MCH_TRADE = 0x1c,
            MMTENPAY_CGICMD_NONPAY = 0x5c,
            MMTENPAY_CGICMD_OFFLINE_CHG_FEE = 50,
            MMTENPAY_CGICMD_OFFLINE_CLOSE = 0x2f,
            MMTENPAY_CGICMD_OFFLINE_CREATE = 0x2e,
            MMTENPAY_CGICMD_OFFLINE_FPAY = 0x30,
            MMTENPAY_CGICMD_OFFLINE_GET_TOKEN = 0x34,
            MMTENPAY_CGICMD_OFFLINE_QUERY_USER = 0x31,
            MMTENPAY_CGICMD_OFFLINE_UNFREEZE = 0x33,
            MMTENPAY_CGICMD_PAYRELAY = 0x57,
            MMTENPAY_CGICMD_PAYUNREG = 0x47,
            MMTENPAY_CGICMD_PUBLIC_API = 0x15,
            MMTENPAY_CGICMD_QRCODE_CREATE = 5,
            MMTENPAY_CGICMD_QRCODE_TO_BARCODE = 0x4e,
            MMTENPAY_CGICMD_QRCODE_USE = 6,
            MMTENPAY_CGICMD_QUERY_REFUND = 80,
            MMTENPAY_CGICMD_QUERY_TRANSFER_STATUS = 0x54,
            MMTENPAY_CGICMD_QUERY_USER_TYPE = 30,
            MMTENPAY_CGICMD_RESET_PWD = 20,
            MMTENPAY_CGICMD_RESET_PWD_AUTHEN = 10,
            MMTENPAY_CGICMD_RESET_PWD_VERIFY = 11,
            MMTENPAY_CGICMD_TIMESEED = 0x13,
            MMTENPAY_CGICMD_TRANSFEAR_SEND_CANCEL_MSG = 0x61,
            MMTENPAY_CGICMD_TRANSFER_CONFIRM = 0x55,
            MMTENPAY_CGICMD_TRANSFER_GET_USERNAME = 0x5f,
            MMTENPAY_CGICMD_TRANSFER_RETRYSENDMESSAGE = 0x56,
            MMTENPAY_CGICMD_UNBIND = 14,
            MMTENPAY_CGICMD_USER_ROLL = 3,
            MMTENPAY_CGICMD_USER_ROLL_BATCH = 4,
            MMTENPAY_CGICMD_USER_ROLL_SAVE_AND_FETCH = 0x4d,
            MMTENPAY_CGICMD_VERIFY = 1,
            MMTENPAY_CGICMD_VERIFY_BIND = 0x4c,
            MMTENPAY_CGICMD_VERIFY_REG = 0x10,
            MMTENPAY_CGICMD_WX_FUND_ACCOUNT_QUERY = 0x2b,
            MMTENPAY_CGICMD_WX_FUND_BINDSP_QUERY = 0x2a,
            MMTENPAY_CGICMD_WX_FUND_BUY = 0x27,
            MMTENPAY_CGICMD_WX_FUND_CHANGE = 0x29,
            MMTENPAY_CGICMD_WX_FUND_PROFIT_QUERY = 0x2c,
            MMTENPAY_CGICMD_WX_FUND_REDEM = 40,
            MMTENPAY_CGICMD_WX_FUND_SUPPORT_BANK = 0x2d,
            MMTENPAY_CGICMD_WX_GET_MERSIGN_ORDER = 0x58,
            MMTENPAY_CGICMD_WX_GET_MERSIGN_SIMPLE = 90,
            MMTENPAY_CGICMD_WX_HB_REDPACKETNOTIFY = 0x35,
            MMTENPAY_CGICMD_WX_HBAA_TRANSFER = 0x51,
            MMTENPAY_CGICMD_WX_OFFLINE_AUTHEN = 0x23,
            MMTENPAY_CGICMD_WX_OFFLINE_PAY = 0x24,
            MMTENPAY_CGICMD_WX_PAY_CONFIRM = 0x52,
            MMTENPAY_CGICMD_WX_QUERY_BANK_ROLL_LIST_BATCH = 0x5d,
            MMTENPAY_CGICMD_WX_QUERY_ORDER = 0x60,
            MMTENPAY_CGICMD_WX_QUERY_SP_BANK = 0x5b,
            MMTENPAY_CGICMD_WX_SP_CLOSE_ORDER = 0x36,
            MMTENPAY_CGICMD_WX_VERIFY_MERSIGN = 0x59,
            MMTENPAY_CGICMD_WXCREDIT_AUTHEN = 0x40,
            MMTENPAY_CGICMD_WXCREDIT_COMMIT_QUESTION = 60,
            MMTENPAY_CGICMD_WXCREDIT_QUERY = 0x39,
            MMTENPAY_CGICMD_WXCREDIT_QUERY_BILL_DETAIL = 0x43,
            MMTENPAY_CGICMD_WXCREDIT_QUERY_CARD_DETAIL = 0x3a,
            MMTENPAY_CGICMD_WXCREDIT_QUERY_PRIVILEGE = 0x44,
            MMTENPAY_CGICMD_WXCREDIT_QUERY_QUESTION = 0x3b,
            MMTENPAY_CGICMD_WXCREDIT_RENEW_IDENTIFY = 0x45,
            MMTENPAY_CGICMD_WXCREDIT_REPAY = 0x3d,
            MMTENPAY_CGICMD_WXCREDIT_SIMPLE_VERIFY = 0x42,
            MMTENPAY_CGICMD_WXCREDIT_UNBIND = 0x3e,
            MMTENPAY_CGICMD_WXCREDIT_VERIFY = 0x41,
            MMTENPAY_CGICMD_WXCREDIT_VERIFY_PASSWD = 0x3f,
            MMTENPAY_GW_CGICMD_NORMAL_ORDER_QUERY = 0x1d,
            MMTENPAY_GW_CGICMD_NORMAL_REFUND_QUERY = 0x21,
            MMTENPAY_GW_CGICMD_VERIFY_NOTIFY_ID = 0x1f
        }

        [ProtoContract]
        public class ErrMsg
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string msg;
        }

        [ProtoContract]
        public class Ext_CommentId
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint commentId;
        }

        public class ExtDeviceLoginConfirmErrorRet
        {
            [ProtoMember(2)]
            public string contentStr;
            [ProtoMember(1)]
            public uint iconType;
        }

        public class ExtDeviceLoginConfirmExpiredRet
        {
            [ProtoMember(3)]
            public string buttonStr;
            [ProtoMember(2)]
            public string contentStr;
            [ProtoMember(1)]
            public uint iconType;
        }

        [ProtoContract]
        public class ExtDeviceLoginConfirmGetRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string deviceName;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string loginUrl;
        }

        [ProtoContract]
        public class ExtDeviceLoginConfirmGetResponse
        {
            [ProtoMember(1)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5)]
            public string deviceNameStr;
            [ProtoMember(3, Options=MemberSerializationOptions.DynamicType)]
            public MM.ExtDeviceLoginConfirmErrorRet errorRet;
            [ProtoMember(4, Options=MemberSerializationOptions.DynamicType)]
            public MM.ExtDeviceLoginConfirmExpiredRet expiredRet;
            [ProtoMember(7)]
            public uint funcCtrl;
            [ProtoMember(6)]
            public uint loginClientVersion;
            [ProtoMember(2)]
            public MM.ExtDeviceLoginConfirmOKRet okret;
        }

        [ProtoContract]
        public class ExtDeviceLoginConfirmOKRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string loginUrl;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string sessionList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public bool syncMsg;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] unReadChatContaceList;
        }

        [ProtoContract]
        public class ExtDeviceLoginConfirmOKResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] msgContextPubKey;
        }

        [ProtoContract]
        public class ExtDeviceLoginConfirmOKRet
        {
            [ProtoMember(4)]
            public string buttonCancelStr;
            [ProtoMember(3)]
            public string buttonOkstr;
            [ProtoMember(6)]
            public uint confirmTimeOut;
            [ProtoMember(2)]
            public string contentStr;
            [ProtoMember(1)]
            public uint icoType;
            [ProtoMember(7)]
            public string loginedDevTip;
            [ProtoMember(5)]
            public uint reqSessionLimit;
            [ProtoMember(8)]
            public string titleStr;
            [ProtoMember(9)]
            public string warningStr;
        }

        [ProtoContract]
        public class F2FQrcodeRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
        }

        [ProtoContract]
        public class F2FQrcodeResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.MenuItem bottomItem;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string bottomleftIconUrl;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public bool bottomRightArrowFlag;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint busiType;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public MM.MiniProgramInfo buyMaterialInfo;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public uint guideMaterialFlag;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string mchName;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string mchPhoto;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string trueName;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] upperRightItems;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string upperWording;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string url;
        }

        [ProtoContract]
        public class FavObject
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint favId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint flag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string @object;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int status;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int updateSeq;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint updateTime;
        }

        [ProtoContract]
        public sealed class FavSyncRequest
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.syncMsgKey keyBuf;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int selector;
        }

        [ProtoContract]
        public sealed class FavSyncResponse
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.CmdList cmdList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint continueFlag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.syncMsgKey key_buf;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int ret;
        }

        [ProtoContract]
        public class FLAG
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int flag;
        }

        [ProtoContract]
        public class FriendGroup
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint groupId;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string groupName;
        }

        [ProtoContract]
        public class FunctionSwitch
        {
            [ProtoMember(1)]
            public uint functionId;
            [ProtoMember(2)]
            public int switchValue;
        }

        [ProtoContract]
        public class GeneralControlBitSet
        {
            [ProtoMember(1)]
            public uint Bitvalue;
        }

        [ProtoContract]
        public class GetA8KeyRequest
        {
            [ProtoMember(3)]
            public MM.SKBuiltinString_ a2key;
            [ProtoMember(13)]
            public MM.SKBuiltinString_ a2KeyNew;
            [ProtoMember(4)]
            public MM.SKBuiltinString appID;
            [ProtoMember(1)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(12)]
            public string bundleID;
            [ProtoMember(0x12)]
            public uint codeType;
            [ProtoMember(0x13)]
            public uint codeVersion;
            [ProtoMember(0x17)]
            public MM.SKBuiltinString_ cookie;
            [ProtoMember(0x10)]
            public uint flag;
            [ProtoMember(15)]
            public uint fontScale;
            [ProtoMember(9)]
            public uint friendQQ;
            [ProtoMember(8)]
            public string friendUserName;
            [ProtoMember(0x15)]
            public string functionId;
            [ProtoMember(0x11)]
            public string netType;
            [ProtoMember(2)]
            public uint opCode;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint reason;
            [ProtoMember(20)]
            public ulong requestId;
            [ProtoMember(7)]
            public MM.SKBuiltinString reqUrl;
            [ProtoMember(10)]
            public uint scene;
            [ProtoMember(5)]
            public MM.SKBuiltinString scope;
            [ProtoMember(6)]
            public MM.SKBuiltinString state;
            [ProtoMember(11)]
            public string userName;
            [ProtoMember(0x16)]
            public uint walletRegion;
        }

        [ProtoContract]
        public class GetA8KeyResponse
        {
            [ProtoMember(3)]
            public string a8key;
            [ProtoMember(4)]
            public MM.enMMScanQrcodeActionCode actionCode;
            [ProtoMember(0x12)]
            public string antispamTicket;
            [ProtoMember(1)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(6)]
            public string content;
            [ProtoMember(0x1c)]
            public MM.SKBuiltinString_ cookie;
            [ProtoMember(0x16)]
            public MM.DeepLinkBitSet deepLinkBitSet;
            [ProtoMember(2)]
            public string fullURL;
            [ProtoMember(8)]
            public MM.GeneralControlBitSet generalControlBitSet;
            [ProtoMember(0x1b)]
            public string headimg;
            [ProtoMember(0x19)]
            public MM.SKBuiltinString_ httpHeader;
            [ProtoMember(0x18)]
            public uint httpHeaderCount;
            [ProtoMember(0x17)]
            public MM.SKBuiltinString_ jSAPIControlBytes;
            [ProtoMember(7)]
            public MM.JSAPIPermissionBitSet jSAPIPermission;
            [ProtoMember(0x1d)]
            public string menuWording;
            [ProtoMember(0x15)]
            public string mID;
            [ProtoMember(0x10)]
            public uint scopeCount;
            [ProtoMember(0x11)]
            public MM.BizScopeInfo[] ScopeList;
            [ProtoMember(15)]
            public string shareURL;
            [ProtoMember(20)]
            public string ssid;
            [ProtoMember(5)]
            public string title;
            [ProtoMember(9)]
            public string userName;
            [ProtoMember(0x1a)]
            public string wording;
        }

        [ProtoContract]
        public class GetCDNDnsRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string clientIP;
        }

        [ProtoContract]
        public class GetCDNDnsResponse
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.CDNDnsInfo appDnsInfo;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public byte[] cdndnsRuleBuf;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.CDNClientConfig defaultConfig;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.CDNClientConfig disasterConfig;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.CDNDnsInfo dnsinfo;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public byte[] fakeCdndnsRulebuf;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.CDNDnsInfo fakeDnsInfo;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public int getCdnDnsIntervalMs;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.CDNDnsInfo snsDnsInfo;

            [ProtoContract]
            public class tag7
            {
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public uint t1;
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public ulong t2;
                [ProtoMember(3, Options=MemberSerializationOptions.Required)]
                public uint t3;
                [ProtoMember(4, Options=MemberSerializationOptions.Required)]
                public uint t4;
                [ProtoMember(5, Options=MemberSerializationOptions.Required)]
                public uint t5;
                [ProtoMember(6, Options=MemberSerializationOptions.Required)]
                public uint t6;
            }
        }

        [ProtoContract]
        public class GetChatRoomInfoDetailRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string chatRoomName;
        }

        [ProtoContract]
        public class GetChatRoomInfoDetailResponse
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string announcement;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string announcementEditor;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint announcementPublishTime;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint chatRoomInfoVersion;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint chatRoomStatus;
        }

        [ProtoContract]
        public class GetChatroomMemberDetailRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string chatroomUserName;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint clientVersion;
        }

        [ProtoContract]
        public class GetChatroomMemberDetailResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string chatroomUserName;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint clientVersion;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.ChatRoomMemberData newChatroomData;
        }

        [ProtoContract]
        public class GetConnectInfoRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string Url;
        }

        [ProtoContract]
        public class GetConnectInfoResponse
        {
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint Addrcount;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public byte[] AddrList;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public ulong Datasize;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public uint EncryFlag;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string Hello;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string Id;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.AesKey Key;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string Ok;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string Pcacctname;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string Pcname;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string Resource;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint Scene;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint Type;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string Wifiname;
        }

        [ProtoContract]
        public class GetContactLabelListRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
        }

        [ProtoContract]
        public class GetContactLabelListResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint labelCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.LabelPair[] labelPairList;
        }

        [ProtoContract]
        public class GetContactRequest
        {
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] antispamTicket;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint antispamTicketCount;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ chatRoomAccessVerifyTicket;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString fromChatRoom;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint fromChatRoomCount;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint getContactScene;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint userCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] userNameList;
        }

        [ProtoContract]
        public class GetContactResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint contactCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.ModContact[] contactList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int[] ret;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.VerifyUserValidTicket[] ticket;
        }

        [ProtoContract]
        public sealed class GetEmotionDescRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string productID;
        }

        [ProtoContract]
        public sealed class GetEmotionDescResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string buttonDesc;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint clickFlag;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint downLoadFlag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.EmotionDesc[] list;
        }

        [ProtoContract]
        public sealed class GetFavInfoRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baserequest;
        }

        [ProtoContract]
        public class GetFavInfoResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint mxAutoDownloadSize;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint mxAutoUploadSize;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint mxFavFileSize;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public ulong totalSize;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public ulong usedSize;
        }

        [ProtoContract]
        public class GetLoginQRCodeRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.AesKey aes;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string deviceName;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint extDevLoginType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string hadrwareExtra;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint opcode;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.RSAPem rsa;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string softType;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class GetLoginQRCodeResponse
        {
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.AesKey AESKey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ blueToothBroadCastContent;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string blueToothBroadCastUuid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint checkTime;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint expiredTime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.QRCode qRCode;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string uuid;
        }

        [ProtoContract]
        public class GetMFriendRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string mD5;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.MEmail[] updateEmailList;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint updateEmailListSize;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.Mobile[] updateMobileList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint updateMobileListSize;
        }

        [ProtoContract]
        public class GetMFriendResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.Mobile friendList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string md5;
        }

        [ProtoContract]
        public class GetMsgImgRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint compressType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint dataLen;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString from;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public ulong newMsgId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint startPos;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString to;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint totalLen;
        }

        [ProtoContract]
        public class GetMsgImgResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ data;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint dataLen;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString from;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint startPos;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString to;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint totalLen;
        }

        [ProtoContract]
        public class GetOnlineInfoRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string language;
        }

        [ProtoContract]
        public class GetOnlineInfoResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint flag;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint iConType;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint onlineCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.OnlineInfo[] onlineList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string summaryXML;
        }

        [ProtoContract]
        public class GetOpenIMResourceRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string Appid;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string Language;
            [ProtoMember(3)]
            public uint Wordingid;
        }

        [ProtoContract]
        public class GetOpenIMResourceResponse
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.AcctTypeResource acctTypeResource;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.AppIdResource appidResource;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint wordingIdResource;
        }

        [ProtoContract]
        public class GetPayFunctionListRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string exInfo;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string telephonyNetIso;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint ticketCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public byte[] ticketList;
        }

        [ProtoContract]
        public class GetPayFunctionListResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint cacheTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint notShowTutorial;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string payFunctionList;
        }

        [ProtoContract]
        public class GetProfileRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class GetProfileResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.ModUserInfo userInfo;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.UserInfoExt userInfoExt;
        }

        [ProtoContract]
        public class GetQRCodeRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint opcode;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint style;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] userName;
        }

        [ProtoContract]
        public class GetQRCodeResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string footerWording;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ qrcode;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string revokeQrcodeId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string revokeQrcodeWording;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint style;
        }

        [ProtoContract]
        public class GetReportStrategyRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string baseRdeviceModel;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string deviceBrand;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string languageVer;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int logid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string osName;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string osVersion;
        }

        [ProtoContract]
        public class GmailInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string gmailAcct;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint gmailErrCode;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint gmailSwitch;
        }

        [ProtoContract]
        public class GmailList
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.GmailInfo[] list;
        }

        [ProtoContract]
        public class HeartBeatRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ blueToothBroadCastContent;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.syncMsgKey Keybuf;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint scene;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public long Timestamp;
        }

        [ProtoContract]
        public class HeartBeatResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Bluetoothbroad;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint Nexttime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint Selector;
        }

        [ProtoContract]
        public class HongBaoRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int cgiCmd;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint outPutType;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S reqText;
        }

        [ProtoContract]
        public class HongBaoResponse
        {
            [ProtoMember(1)]
            public MMPro.MM.BaseResponse BaseResponse;
            [ProtoMember(5)]
            public int cgiCmdid;
            [ProtoMember(7)]
            public string errorMsg;
            [ProtoMember(6)]
            public int errorType;
            [ProtoMember(4)]
            public string platMsg;
            [ProtoMember(3)]
            public int platRet;
            [ProtoMember(2)]
            public MM.SKBuiltinString_S retText;
        }

        public class HongBaoRetText
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private MM.Agree_Duty <agree_duty>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <hbStatus>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <hbType>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <isSender>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <receiveStatus>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <retcode>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <retmsg>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <sendId>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <sendUserName>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <statusMess>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <timingIdentifier>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <watermark>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <wishing>k__BackingField;

            public MM.Agree_Duty agree_duty { get; set; }

            public int hbStatus { get; set; }

            public int hbType { get; set; }

            public int isSender { get; set; }

            public int receiveStatus { get; set; }

            public int retcode { get; set; }

            public string retmsg { get; set; }

            public string sendId { get; set; }

            public string sendUserName { get; set; }

            public string statusMess { get; set; }

            public string timingIdentifier { get; set; }

            public string watermark { get; set; }

            public string wishing { get; set; }
        }

        [ProtoContract]
        public class Host
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string origin;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int priority;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string substitute;
        }

        [ProtoContract]
        public class HostList
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.Host[] list;
        }

        [ProtoContract]
        public class ImgPair
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ imgBuf;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString username;
        }

        [ProtoContract]
        public class InitContactRequest
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int currentChatRoomContactSeq;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int currentWxcontactSeq;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        [ProtoContract]
        public class InitContactResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string[] contactUsernameList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint countinueFlag;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int currentChatRoomContactSeq;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int currentWxcontactSeq;
        }

        [ProtoContract]
        public class InviteFriendOpen
        {
            [ProtoMember(2)]
            public uint friendType;
            [ProtoMember(1)]
            public string userName;
        }

        [ProtoContract]
        public class JSAPIPermissionBitSet
        {
            [ProtoMember(1)]
            public uint Bitvalue1;
            [ProtoMember(2)]
            public uint Bitvalue2;
            [ProtoMember(3)]
            public uint Bitvalue3;
            [ProtoMember(4)]
            public uint Bitvalue4;
        }

        [ProtoContract]
        public class KVSTAT
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string kid;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string value;
        }

        [ProtoContract]
        public class LabelPair
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint labelID;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string labelName;
        }

        [ProtoContract]
        public class LBsContactInfo
        {
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string alias;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public string antispamTocklet;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string city;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public string country;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public MM.CustomizedInfo customizedInfo;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string distance;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public int headImgVersion;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint imgStatus;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public string myBrandList;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string nickName;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string province;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string userName;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string verifyContent;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint verifyFlag;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string verifyInfo;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string weibo;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public uint weiboFlag;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public string weiboNickname;
        }

        [ProtoContract]
        public class LbsRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string cellId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ clientCheckData;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public int gPSSource;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public float latitude;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public float logitude;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string macAddr;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opCode;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int precision;
        }

        [ProtoContract]
        public class LbsResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint contactCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.LBsContactInfo[] contactList;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint flushTime;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint roomMemberCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint state;
        }

        [ProtoContract]
        public class LinkedinContactItem
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string linkedinMemberID;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string linkedinName;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string linkedinPublicUrl;
        }

        [ProtoContract]
        public class LoginInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public byte[] aesKey;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string androidVer;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int clientVer;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string guid;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int uin;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int unknown;
        }

        [ProtoContract]
        public class LoginQRCodeNotify
        {
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string device;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public int EffectiveTime;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string headImgUrl;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string nickName;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int pushLoginUrlexpiredTime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int state;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int t10;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string uuid;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string wxid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string wxnewpass;
        }

        [ProtoContract]
        public class LoginQRCodeNotifyPkg
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ notifyData;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opcode;
        }

        public enum LongLinkCmdId
        {
            LONGLINK_IDENTIFY_REQ = 0xcd,
            LONGLINK_IDENTIFY_RESP = 0x3b9acacd,
            NEWSENDMSG = 0xed,
            PUSH_DATA_CMDID = 0x7a,
            RECV_CHECKLOGINQRCODE_CMDID = 0x3b9acae9,
            RECV_DOWNLOADVOICE = 0x3b9aca14,
            RECV_GETLOGINQRCODE = 0x3b9acae8,
            RECV_GETPROFILE = 0x3b9aca76,
            RECV_MSGIMG_CMDID = 0x3b9aca09,
            RECV_NEWSYNC_CMDID = 0x3b9aca79,
            RECV_NOOP_CMDID = 0x3b9aca06,
            RECV_PUSH_CMDID = 0x18,
            SEND_CHECKLOGINQRCODE_CMDID = 0xe9,
            SEND_DOWNLOADVOICE = 20,
            SEND_GETLOGINQRCODE = 0xe8,
            SEND_GETPROFILE = 0x76,
            SEND_MANUALAUTH_CMDID = 0xfd,
            SEND_MSGIMG_CMDID = 9,
            SEND_NEWSYNC_CMDID = 0x79,
            SEND_NOOP_CMDID = 6,
            SEND_SYNC_SUCCESS = 0x3b9acabe,
            SIGNALKEEP_CMDID = 0xf3
        }

        [ProtoContract]
        public class ManualAuthAccountRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.AesKey aes;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.Ecdh ecdh;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string password1;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string password2;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class ManualAuthDeviceRequest
        {
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string Adsource;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.BaseAuthReqInfo baseReqInfo;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int Builtinipseq;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string Bundleid;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public int Channel;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Clientcheckdat;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string clientSeqID;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string deviceBrand;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string deviceInfoXml;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string deviceModel;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] imei;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public int Inputtype;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public string Iphonever;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string language;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string loginDeviceName;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public string osType;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public string realCountry;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string Signature;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string softInfoXml;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public int Timestamp;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string timeZone;
        }

        [ProtoContract]
        public class ManualAuthResponse
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.AccountInfo accountInfo;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.AuthParam authParam;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.NetworkSectResp dnsInfo;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int unifyFlag;
        }

        [ProtoContract]
        public class MassSendRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint cameraType;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string clientId;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public uint compressType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S dataBuffer;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint dataStartPos;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint dataTotalLen;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public uint isSendAgain;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint mediaTime;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint msgType;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public string thumbAeskey;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ thumbData;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public uint thumbHeight;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint thumbStartPos;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public uint thumbTotalLen;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string thumbUrl;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public uint thumbWith;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string toList;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public uint toListCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string toListMd5;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public string videoAeskey;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint videoSource;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string videoUrl;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public uint voiceFormat;
        }

        [ProtoContract]
        public class MassSendResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint dataStartPos;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint maxSupport;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint thumbStartPos;
        }

        [Serializable]
        public class media
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <CDataContent>k__BackingField;

            [XmlIgnore]
            public string CDataContent { get; set; }

            [XmlElement("id")]
            public XmlNode id
            {
                get
                {
                    XmlDocument document = new XmlDocument();
                    return document.CreateCDataSection(this.CDataContent);
                }
                set
                {
                    if (value == null)
                    {
                        this.CDataContent = null;
                    }
                    else
                    {
                        XmlNode node = value;
                        if (node == null)
                        {
                            throw new InvalidOperationException("Node is null.");
                        }
                        this.CDataContent = node.Value;
                    }
                }
            }

            [XmlElement("title")]
            public XmlNode title
            {
                get
                {
                    XmlDocument document = new XmlDocument();
                    return document.CreateCDataSection(this.CDataContent);
                }
                set
                {
                    if (value != null)
                    {
                        XmlNode node = value;
                        if (node == null)
                        {
                            throw new InvalidOperationException("Node is null.");
                        }
                        this.CDataContent = node.Value;
                    }
                }
            }

            [XmlElement("type")]
            public XmlNode type
            {
                get
                {
                    XmlDocument document = new XmlDocument();
                    return document.CreateCDataSection(this.CDataContent);
                }
                set
                {
                    if (value == null)
                    {
                        this.CDataContent = null;
                    }
                    else
                    {
                        XmlNode node = value;
                        if (node == null)
                        {
                            throw new InvalidOperationException("Node is null.");
                        }
                        this.CDataContent = node.Value;
                    }
                }
            }
        }

        [ProtoContract]
        public class MediaInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SnsMediaType mediaType;
            [ProtoMember(4)]
            public string sessionId;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(5)]
            public uint startTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint videoPlayLength;
        }

        [ProtoContract]
        public class MEmail
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string v;
        }

        [ProtoContract]
        public class MemberReq
        {
            [ProtoMember(1)]
            public MM.SKBuiltinString member;
        }

        [ProtoContract]
        public class MemberResp
        {
            [ProtoMember(14)]
            public string City;
            [ProtoMember(12)]
            public uint contactType;
            [ProtoMember(0x13)]
            public string country;
            [ProtoMember(1)]
            public MM.SKBuiltinString member;
            [ProtoMember(2)]
            public uint memberStatus;
            [ProtoMember(3)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(0x10)]
            public uint personalCard;
            [ProtoMember(13)]
            public string province;
            [ProtoMember(4)]
            public MM.SKBuiltinString pYInitial;
            [ProtoMember(5)]
            public MM.SKBuiltinString quanPin;
            [ProtoMember(9)]
            public MM.SKBuiltinString remark;
            [ProtoMember(10)]
            public MM.SKBuiltinString remarkPYInitial;
            [ProtoMember(11)]
            public MM.SKBuiltinString remarkQuanPin;
            [ProtoMember(6)]
            public int sex;
            [ProtoMember(15)]
            public string signature;
            [ProtoMember(0x11)]
            public uint verifyFlag;
            [ProtoMember(0x12)]
            public string verifyInfo;
        }

        [ProtoContract]
        public class MenuItem
        {
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint isShowRed;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string subwording;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint type;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string url;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string waappPath;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string waappUsername;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string wording;
        }

        [ProtoContract]
        public class MiniProgramInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string pagePath;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        [ProtoContract]
        public class MMSnsUserPageRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public ulong createtime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string t2;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint t4;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint t5;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public ulong t6;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string wxid;
        }

        [ProtoContract]
        public class Mobile
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string v;
        }

        [ProtoContract]
        public class ModChatRoomMember
        {
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public string albumBgimgId;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public int albumFlag;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public int albumStyle;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public string alias;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string city;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint contactType;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public string country;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public MM.CustomizedInfo customizedInfo;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ imgBuf;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint imgFlag;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public string myBrandList;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public int personalCard;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string province;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString pyInitial;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString quanPin;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString remark;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString remarkPYInitial;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString remarkQuanPin;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public string smallheadImgUrl;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string verifyContent;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public int verifyFlag;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public string verifyInfo;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public string weibo;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public int weiboFlag;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string weiboNickname;
        }

        [ProtoContract]
        public class ModChatRoomNotify
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString chatRoomName;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint status;
        }

        [ProtoContract]
        public class ModChatRoomTopic
        {
            [ProtoMember(1)]
            public MM.SKBuiltinString chatRoomName;
            [ProtoMember(2)]
            public MM.SKBuiltinString chatRoomTopic;
        }

        [ProtoContract]
        public class ModContact
        {
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public uint addContactScene;
            [ProtoMember(50, Options=MemberSerializationOptions.Required)]
            public MM.AdditionalContactList additionalContactList;
            [ProtoMember(0x24, Options=MemberSerializationOptions.Required)]
            public string albumBGImgID;
            [ProtoMember(0x23, Options=MemberSerializationOptions.Required)]
            public int albumFlag;
            [ProtoMember(0x22, Options=MemberSerializationOptions.Required)]
            public int albumStyle;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public string alias;
            [ProtoMember(0x27, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint bitMask;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint bitVal;
            [ProtoMember(60, Options=MemberSerializationOptions.Required)]
            public string cardImgUrl;
            [ProtoMember(0x2b, Options=MemberSerializationOptions.Required)]
            public string chatRoomData;
            [ProtoMember(0x40, Options=MemberSerializationOptions.Required)]
            public int ChatroomInfoVersion;
            [ProtoMember(0x37, Options=MemberSerializationOptions.Required)]
            public uint chatroomMaxCount;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public uint chatRoomNotify;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public string chatRoomOwner;
            [ProtoMember(0x42, Options=MemberSerializationOptions.Required)]
            public int ChatroomstatuStatus;
            [ProtoMember(0x38, Options=MemberSerializationOptions.Required)]
            public uint chatroomType;
            [ProtoMember(0x35, Options=MemberSerializationOptions.Required)]
            public uint chatroomVersion;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string city;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint contactType;
            [ProtoMember(0x26, Options=MemberSerializationOptions.Required)]
            public string country;
            [ProtoMember(0x2a, Options=MemberSerializationOptions.Required)]
            public MM.CustomizedInfo customizedInfo;
            [ProtoMember(0x41, Options=MemberSerializationOptions.Required)]
            public int DeletecontactScene;
            [ProtoMember(0x3a, Options=MemberSerializationOptions.Required)]
            public int deleteFlag;
            [ProtoMember(0x3b, Options=MemberSerializationOptions.Required)]
            public string description;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] domainList_;
            [ProtoMember(0x2d, Options=MemberSerializationOptions.Required)]
            public string encryptUserName;
            [ProtoMember(0x43, Options=MemberSerializationOptions.Required)]
            public int Extflag;
            [ProtoMember(0x36, Options=MemberSerializationOptions.Required)]
            public string extInfo;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public uint hasWeiXinHdHeadImg;
            [ProtoMember(0x2c, Options=MemberSerializationOptions.Required)]
            public string headImgMd5;
            [ProtoMember(0x2e, Options=MemberSerializationOptions.Required)]
            public string iDCardNum;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ imgBuf;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint imgFlag;
            [ProtoMember(0x3d, Options=MemberSerializationOptions.Required)]
            public string labelIDList;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public int level;
            [ProtoMember(0x31, Options=MemberSerializationOptions.Required)]
            public string mobileFullHash;
            [ProtoMember(0x30, Options=MemberSerializationOptions.Required)]
            public string mobileHash;
            [ProtoMember(0x29, Options=MemberSerializationOptions.Required)]
            public string myBrandList;
            [ProtoMember(0x39, Options=MemberSerializationOptions.Required)]
            public MM.ChatRoomMemberData newChatroomData;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public uint personalCard;
            [ProtoMember(0x3e, Options=MemberSerializationOptions.Required)]
            public MM.PhoneNumListInfo Phonenumlistinfo;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string province;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString pyInitial;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString quanPin;
            [ProtoMember(0x2f, Options=MemberSerializationOptions.Required)]
            public string realName;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString remark;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString remarkPYInitial;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString remarkQuanPin;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint roomInfoCount;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public MM.RoomInfo[] RoomInfoList;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(40, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
            [ProtoMember(0x25, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public string verifyContent;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public uint verifyFlag;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public string verifyInfo;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public string weibo;
            [ProtoMember(0x21, Options=MemberSerializationOptions.Required)]
            public uint weiboFlag;
            [ProtoMember(0x20, Options=MemberSerializationOptions.Required)]
            public string weiboNickname;
            [ProtoMember(0x3f, Options=MemberSerializationOptions.Required)]
            public string Weidianinfo;
        }

        [ProtoContract]
        public class ModMsgStatus
        {
            [ProtoMember(2)]
            public MM.SKBuiltinString fromUserName;
            [ProtoMember(1)]
            public int msgId;
            [ProtoMember(4)]
            public uint status;
            [ProtoMember(3)]
            public MM.SKBuiltinString toUserName;
        }

        [ProtoContract]
        public class ModNotifyStatus
        {
            [ProtoMember(2)]
            public uint status;
            [ProtoMember(1)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class ModUserDomainEmail
        {
            [ProtoMember(2)]
            public MM.SKBuiltinString email;
            [ProtoMember(1)]
            public uint status;
        }

        [ProtoContract]
        public class MODUSERIMG
        {
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] imgBuf;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint imgLen;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string imgMd5;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint imgType;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
        }

        [ProtoContract]
        public class ModUserInfo
        {
            [ProtoMember(0x23, Options=MemberSerializationOptions.Required)]
            public string albumBGImgID;
            [ProtoMember(0x22, Options=MemberSerializationOptions.Required)]
            public int albumFlag;
            [ProtoMember(0x21, Options=MemberSerializationOptions.Required)]
            public int albumStyle;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public string alias;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString bindEmail;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString bindMobile;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint bindUin;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint bitFlag;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string city;
            [ProtoMember(0x26, Options=MemberSerializationOptions.Required)]
            public string country;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public MM.DisturbSetting disturbSetting;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public int experience;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public uint faceBookFlag;
            [ProtoMember(0x25, Options=MemberSerializationOptions.Required)]
            public string fBToken;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public ulong fBUserID;
            [ProtoMember(0x20, Options=MemberSerializationOptions.Required)]
            public string fBUserName;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public MM.GmailList gmailList;
            [ProtoMember(0x27, Options=MemberSerializationOptions.Required)]
            public uint grayscaleFlag;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public byte[] imgBuf;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint imgLen;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public int level;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public int levelHighExp;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public int levelLowExp;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint personalCard;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public uint pluginFlag;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public uint pluginSwitch;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public int point;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string province;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint status;
            [ProtoMember(0x24, Options=MemberSerializationOptions.Required)]
            public uint tXNewsCategory;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public uint verifyFlag;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public string verifyInfo;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public string weibo;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public uint weiboFlag;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public string weiboNickname;
        }

        public class msgImg
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <aeskey>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnbigimgurl>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnhdheight>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnhdwidth>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnmidheight>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnmidimgurl>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnmidwidth>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnthumbaeskey>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnthumbheight>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private ushort <cdnthumblength>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnthumburl>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnthumbwidth>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <encryver>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private uint <hdlength>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private uint <length>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <md5>k__BackingField;

            [XmlAttribute]
            public string aeskey { get; set; }

            [XmlAttribute]
            public string cdnbigimgurl { get; set; }

            [XmlAttribute]
            public byte cdnhdheight { get; set; }

            [XmlAttribute]
            public byte cdnhdwidth { get; set; }

            [XmlAttribute]
            public byte cdnmidheight { get; set; }

            [XmlAttribute]
            public string cdnmidimgurl { get; set; }

            [XmlAttribute]
            public byte cdnmidwidth { get; set; }

            [XmlAttribute]
            public string cdnthumbaeskey { get; set; }

            [XmlAttribute]
            public byte cdnthumbheight { get; set; }

            [XmlAttribute]
            public ushort cdnthumblength { get; set; }

            [XmlAttribute]
            public string cdnthumburl { get; set; }

            [XmlAttribute]
            public byte cdnthumbwidth { get; set; }

            [XmlAttribute]
            public byte encryver { get; set; }

            [XmlAttribute]
            public uint hdlength { get; set; }

            [XmlAttribute]
            public uint length { get; set; }

            [XmlAttribute]
            public string md5 { get; set; }
        }

        [Serializable]
        public class msgsource
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <CDataContent>k__BackingField;

            [XmlIgnore]
            public string CDataContent { get; set; }

            [XmlElement("atuserlist")]
            public XmlNode[] Nodes
            {
                get
                {
                    XmlDocument document = new XmlDocument();
                    return new XmlNode[] { document.CreateCDataSection(this.CDataContent) };
                }
                set
                {
                    if (value == null)
                    {
                        this.CDataContent = null;
                    }
                    else
                    {
                        if (value.Length != 1)
                        {
                            throw new InvalidOperationException("Invalid array.");
                        }
                        XmlNode node = value[0];
                        if (node == null)
                        {
                            throw new InvalidOperationException("Node is null.");
                        }
                        this.CDataContent = node.Value;
                    }
                }
            }
        }

        public class msgVideomsg
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <aeskey>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnthumbaeskey>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private ushort <cdnthumbheight>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private ushort <cdnthumblength>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnthumburl>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <cdnthumbwidth>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <cdnvideourl>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <fromusername>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <isad>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private uint <length>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <md5>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <newmd5>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private byte <playlength>k__BackingField;

            [XmlAttribute]
            public string aeskey { get; set; }

            [XmlAttribute]
            public string cdnthumbaeskey { get; set; }

            [XmlAttribute]
            public ushort cdnthumbheight { get; set; }

            [XmlAttribute]
            public ushort cdnthumblength { get; set; }

            [XmlAttribute]
            public string cdnthumburl { get; set; }

            [XmlAttribute]
            public byte cdnthumbwidth { get; set; }

            [XmlAttribute]
            public string cdnvideourl { get; set; }

            [XmlAttribute]
            public string fromusername { get; set; }

            [XmlAttribute]
            public byte isad { get; set; }

            [XmlAttribute]
            public uint length { get; set; }

            [XmlAttribute]
            public string md5 { get; set; }

            [XmlAttribute]
            public string newmd5 { get; set; }

            [XmlAttribute]
            public byte playlength { get; set; }
        }

        [ProtoContract]
        public class NetworkControl
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint maxNoopInterval;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint mimNoopInterval;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int noopIntervalTime;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string portList;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string timeoutList;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int typingInterval;
        }

        [ProtoContract]
        public class NetworkSectResp
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.BuiltinIPList builtinIplist;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.NetworkControl networkControl;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.HostList newHostList;
        }

        [ProtoContract]
        public class NewGetInviteFriendRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint friendType;
        }

        [ProtoContract]
        public class NewGetInviteFriendResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint friendCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.NewInviteFriend[] friendList;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint groupCount;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.FriendGroup[] groupList;
        }

        [ProtoContract]
        public class NewInviteFriend
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string email;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint friendType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint groupId;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string imgIdx;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string nickName;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string remark;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint uin;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class NewSendMsgRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int cnt;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.ChatInfo info;
        }

        [ProtoContract]
        public class NewSendMsgRespone
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public BASERESPONSE baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public NewMsgResponeNew[] List;

            [ProtoContract]
            public class BASERESPONSE
            {
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public MM.SKBuiltinString errMsg;
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public int ret;
            }

            [ProtoContract]
            public class NewMsgResponeNew
            {
                [ProtoMember(4, Options=MemberSerializationOptions.Required)]
                public ulong ClientMsgid;
                [ProtoMember(5, Options=MemberSerializationOptions.Required)]
                public uint Createtime;
                [ProtoMember(3, Options=MemberSerializationOptions.Required)]
                public ulong MsgId;
                [ProtoMember(8, Options=MemberSerializationOptions.Required)]
                public ulong newMsgId;
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public uint Ret;
                [ProtoMember(6, Options=MemberSerializationOptions.Required)]
                public uint servertime;
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public MM.SKBuiltinString toUsetName;
                [ProtoMember(7, Options=MemberSerializationOptions.Required)]
                public uint Type;
            }
        }

        [ProtoContract]
        public class NewSyncRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.FLAG continueflag;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string device;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int scene;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int selector;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int syncmsgdigest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.syncMsgKey tagmsgkey;
        }

        [ProtoContract]
        public class NewSyncResponse
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.CmdList cmdList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int Continueflag;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint Onlineversion;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int ret;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint Status;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint Svrtime;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public byte[] sync_key;
        }

        [ProtoContract]
        public class OnlineInfo
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ clientKey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string deviceID;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint deviceType;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string wordingXML;
        }

        [ProtoContract]
        public class Openimcontact
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class OpenQQMicroBlog
        {
            [ProtoMember(1)]
            public MM.SKBuiltinString microBlogUserName;
        }

        [ProtoContract]
        public class OpLog
        {
            [ProtoMember(2)]
            public MM.SKBuiltinString_ buffer;
            [ProtoMember(1)]
            public uint cmdid;
        }

        [ProtoContract]
        public class OpLogRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.CmdList cmd;
        }

        [ProtoContract]
        public class OpLogResponse
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.OpLogRet oplogRet;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int ret;
        }

        [ProtoContract]
        public class OpLogRet
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] errMsg;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] ret;
        }

        [ProtoContract]
        public class PatternLockInfo
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint lockStatus;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint patternVersion;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ sign;
        }

        [ProtoContract]
        public class PhoneNumListInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint Count;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint[] phoneNumList;
        }

        [ProtoContract]
        public class PossibleFriend
        {
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint contactType;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string domainList;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public uint friendScene;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint imgFlag;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string mobile;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string nickName;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string pYInitial;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string quanPin;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string source;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class PreDownloadInfo
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string noPreDownloadRange;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint preDownloadNetType;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint preDownloadPercent;
        }

        [ProtoContract]
        public class PSMStat
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string aType;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint mType;
        }

        [ProtoContract]
        public class PushLoginURLRequest
        {
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Autoauthkey;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string Autoauthticket;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string ClientId;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string Devicename;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint opcode;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.AesKey randomEncryKey;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.RSAPem rsa;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        [ProtoContract]
        public class PushLoginURLResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ blueToothBroadCastContent;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string blueToothBroadCastUuid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint Checktime;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint Expiredtime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.AesKey Notifykey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string uuid;
        }

        [ProtoContract]
        public class QContact
        {
            [ProtoMember(2)]
            public string displayName;
            [ProtoMember(3)]
            public uint extInfoSeq;
            [ProtoMember(4)]
            public uint imgUpdateSeq;
            [ProtoMember(1)]
            public string userName;
        }

        [ProtoContract]
        public class QRCode
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint len;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] src;
        }

        [ProtoContract]
        public class QuitChatRoom
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString chatRoomName;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class ReportKvRequest
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public byte[] datapkg;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public byte[] encryptkey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint Genstgver;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint Specstgver;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint Uinstgver;
        }

        public enum RetConst
        {
            ERR_SERVER_FILE_EXPIRED = -5103059,
            MM_BOTTLE_COUNT_ERR = 0x10,
            MM_BOTTLE_ERR_UNKNOWNTYPE = 15,
            MM_BOTTLE_NOTEXIT = 0x11,
            MM_BOTTLE_PICKCOUNTINVALID = 0x13,
            MM_BOTTLE_UINNOTMATCH = 0x12,
            MM_ERR_ACCESS_DENIED = -5,
            MM_ERR_ACCOUNT_BAN = -200,
            MM_ERR_ALPHA_FORBIDDEN = -75,
            MM_ERR_ANSWER_COUNT = -150,
            MM_ERR_ARG = -2,
            MM_ERR_AUTH_ANOTHERPLACE = -100,
            MM_ERR_BADEMAIL = -28,
            MM_ERR_BATCHGETCONTACTPROFILE_MODE = -45,
            MM_ERR_BIGBIZ_AUTH = -69,
            MM_ERR_BIND_EMAIL_SAME_AS_QMAIL = -86,
            MM_ERR_BINDED_BY_OTHER = -85,
            MM_ERR_BINDUIN_BINDED = -50,
            MM_ERR_BIZ_FANS_LIMITED = -87,
            MM_ERR_BLACKLIST = -22,
            MM_ERR_BLOCK_BY_SPAM = -106,
            MM_ERR_BOTTLEBANBYEXPOSE = -2002,
            MM_ERR_CERT_EXPIRED = -102,
            MM_ERR_CHATROOM_NEED_INVITE = -2012,
            MM_ERR_CHATROOM_PARTIAL_INVITE = -2013,
            MM_ERR_CLIDB_ENCRYPT_KEYINFO_INVALID = -2010,
            MM_ERR_CLIENT = -800000,
            MM_ERR_CONNECT_INFO_URL_INVALID = -2011,
            MM_ERR_COOKIE_KICK = -2008,
            MM_ERR_CRITICALUPDATE = -16,
            MM_ERR_DOMAINDISABLE = -27,
            MM_ERR_DOMAINMAXLIMITED = -26,
            MM_ERR_DOMAINVERIFIED = -25,
            MM_ERR_EMAIL_FORMAT = -111,
            MM_ERR_EMAILEXIST = -8,
            MM_ERR_EMAILNOTVERIFY = -9,
            MM_ERR_FACING_CREATECHATROOM_RETRY = -432,
            MM_ERR_FAV_ALREADY = -400,
            MM_ERR_FILE_EXPIRED = -352,
            MM_ERR_FORCE_QUIT = -999999,
            MM_ERR_FORCE_REDIRECT = -2005,
            MM_ERR_FREQ_LIMITED = -34,
            MM_ERR_GETMFRIEND_NOT_READY = -70,
            MM_ERR_GMAIL_IMAP = -63,
            MM_ERR_GMAIL_ONLINELIMITE = -61,
            MM_ERR_GMAIL_PWD = -60,
            MM_ERR_GMAIL_WEBLOGIN = -62,
            MM_ERR_HAS_BINDED = -84,
            MM_ERR_HAS_NO_HEADIMG = -53,
            MM_ERR_HAS_UNBINDED = -83,
            MM_ERR_HAVE_BIND_FACEBOOK = -67,
            MM_ERR_IDC_REDIRECT = -301,
            MM_ERR_IMG_READ = -1005,
            MM_ERR_INVALID_BIND_OPMODE = -37,
            MM_ERR_INVALID_GROUPCARD_CONTACT = -52,
            MM_ERR_INVALID_HDHEADIMG_REQ_TOTAL_LEN = -54,
            MM_ERR_INVALID_UPLOADMCONTACT_OPMODE = -38,
            MM_ERR_IS_NOT_OWNER = -66,
            MM_ERR_KEYBUF_INVALID = -2006,
            MM_ERR_LBSBANBYEXPOSE = -2001,
            MM_ERR_LBSDATANOTFOUND = -2000,
            MM_ERR_LOGIN_QRCODE_UUID_EXPIRED = -2007,
            MM_ERR_LOGIN_URL_DEVICE_UNSAFE = -2009,
            MM_ERR_MEMBER_TOOMUCH = -23,
            MM_ERR_MOBILE_BINDED = -35,
            MM_ERR_MOBILE_FORMAT = -41,
            MM_ERR_MOBILE_NEEDADJUST = -74,
            MM_ERR_MOBILE_NULL = -39,
            MM_ERR_MOBILE_UNBINDED = -36,
            MM_ERR_NEED_QQPWD = -49,
            MM_ERR_NEED_VERIFY = -6,
            MM_ERR_NEED_VERIFY_USER = -44,
            MM_ERR_NEEDREG = -30,
            MM_ERR_NEEDSECONDPWD = -31,
            MM_ERR_NEW_USER = -79,
            MM_ERR_NICEQQ_EXPIRED = -72,
            MM_ERR_NICKNAMEINVALID = -15,
            MM_ERR_NICKRESERVED = -11,
            MM_ERR_NO_BOTTLECOUNT = -56,
            MM_ERR_NO_HDHEADIMG = -55,
            MM_ERR_NO_QUESTION = -152,
            MM_ERR_NO_RETRY = -101,
            MM_ERR_NODATA = -203,
            MM_ERR_NOTBINDQQ = -81,
            MM_ERR_NOTCHATROOMCONTACT = -21,
            MM_ERR_NOTMICROBLOGCONTACT = -20,
            MM_ERR_NOTOPENPRIVATEMSG = -19,
            MM_ERR_NOTQQCONTACT = -46,
            MM_ERR_NOUPDATEINFO = -18,
            MM_ERR_NOUSER = -4,
            MM_ERR_OIDBTIMEOUT = -29,
            MM_ERR_ONE_BINDTYPE_LEFT = -82,
            MM_ERR_OTHER_MAIN_ACCT = -204,
            MM_ERR_PARSE_MAIL = -64,
            MM_ERR_PASSWORD = -3,
            MM_ERR_PICKBOTTLE_NOBOTTLE = -58,
            MM_ERR_QA_RELATION = -153,
            MM_ERR_QQ_BAN = -201,
            MM_ERR_QQ_OK_NEED_MOBILE = -205,
            MM_ERR_QRCODEVERIFY_BANBYEXPOSE = -2004,
            MM_ERR_QUESTION_COUNT = -151,
            MM_ERR_RADAR_PASSWORD_SIMPLE = -431,
            MM_ERR_RECOMMENDEDUPDATE = -17,
            MM_ERR_REG_BUT_LOGIN = -212,
            MM_ERR_REVOKEMSG_TIMEOUT = -430,
            MM_ERR_SEND_VERIFYCODE = -57,
            MM_ERR_SESSIONTIMEOUT = -13,
            MM_ERR_SHAKE_TRAN_IMG_CANCEL = -90,
            MM_ERR_SHAKE_TRAN_IMG_CONTINUE = -92,
            MM_ERR_SHAKE_TRAN_IMG_NOTFOUND = -91,
            MM_ERR_SHAKE_TRAN_IMG_OTHER = -93,
            MM_ERR_SHAKEBANBYEXPOSE = -2003,
            MM_ERR_SHORTVIDEO_CANCEL = 0xf4240,
            MM_ERR_SPAM = -24,
            MM_ERR_SVR_MOBILE_FORMAT = -78,
            MM_ERR_SYS = -1,
            MM_ERR_TICKET_NOTFOUND = -48,
            MM_ERR_TICKET_UNMATCH = -47,
            MM_ERR_TOLIST_LIMITED = -71,
            MM_ERR_TRYQQPWD = -73,
            MM_ERR_UINEXIST = -12,
            MM_ERR_UNBIND_MAIN_ACCT = -206,
            MM_ERR_UNBIND_MOBILE_NEED_QQPWD = -202,
            MM_ERR_UNBIND_REGBYMOBILE = -65,
            MM_ERR_UNMATCH_MOBILE = -40,
            MM_ERR_UNSUPPORT_COUNTRY = -59,
            MM_ERR_USER_BIND_MOBILE = -43,
            MM_ERR_USER_MOBILE_UNMATCH = -42,
            MM_ERR_USER_NOT_SUPPORT = -94,
            MM_ERR_USER_NOT_VERIFYUSER = -302,
            MM_ERR_USEREXIST = -7,
            MM_ERR_USERNAMEINVALID = -14,
            MM_ERR_USERRESERVED = -10,
            MM_ERR_UUID_BINDED = -76,
            MM_ERR_VERIFYCODE_NOTEXIST = -51,
            MM_ERR_VERIFYCODE_TIMEOUT = -33,
            MM_ERR_VERIFYCODE_UNMATCH = -32,
            MM_ERR_WEIBO_PUSH_TRANS = -80,
            MM_ERR_WRONG_SESSION_KEY = -77,
            MM_FACEBOOK_ACCESSTOKEN_UNVALID = -68,
            MM_OK = 0,
            MMSNS_RET_BAN = 0xca,
            MMSNS_RET_CLIENTID_EXIST = 0xce,
            MMSNS_RET_COMMENT_HAVE_LIKE = 0xcc,
            MMSNS_RET_COMMENT_NOT_ALLOW = 0xcd,
            MMSNS_RET_COMMENT_PRIVACY = 0xd0,
            MMSNS_RET_ISALL = 0xcf,
            MMSNS_RET_PRIVACY = 0xcb,
            MMSNS_RET_SPAM = 0xc9
        }

        [ProtoContract]
        public class RoomInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class RSAPem
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint len;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string pem;
        }

        [ProtoContract]
        public class SafeDevice
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string deviceType;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string name;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string uuid;
        }

        [ProtoContract]
        public class SafeDeviceList
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SafeDevice[] list;
        }

        [ProtoContract]
        public class SearchContactItem
        {
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string albumBigimgId;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public int albumFlag;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public int albumStyle;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public string alias;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string city;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ imgBuf;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int personalCard;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string province;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString pYInitial;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString quanPin;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public int verifyFlag;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string verifyInfo;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string weibo;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public int weiboFlag;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string weiboNickname;
        }

        [ProtoContract]
        public class SearchContactRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint fromScene;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint opCode;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint searchScene;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
        }

        [ProtoContract]
        public class SearchContactResponse
        {
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string albumBgimgId;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public int albumFlag;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public int albumStyle;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public string antispamTicket;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string city;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public uint contactCount;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public MM.SearchContactItem[] contactlist;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public string country;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public MM.CustomizedInfo customizedInfo;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ imgBuf;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public string kfworkerId;
            [ProtoMember(0x20, Options=MemberSerializationOptions.Required)]
            public uint matchType;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public string myBrandList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString nickName;
            [ProtoMember(0x22, Options=MemberSerializationOptions.Required)]
            public uint openImcontactCount;
            [ProtoMember(0x23, Options=MemberSerializationOptions.Required)]
            public MM.Openimcontact[] openImcontactList;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public int personalCard;
            [ProtoMember(0x21, Options=MemberSerializationOptions.Required)]
            public string popupInfoMsg;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string province;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString pYInitial;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString quanPin;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ resBuf;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int sex;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string signature;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString userName;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string verifyContent;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public int verifyFlag;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string verifyInfo;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public string weibo;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public int weiboFlag;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string weiboNickname;
        }

        [ProtoContract]
        public class SendAppMsgRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string commentUrl;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public long crc;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int fileType;
            [ProtoMember(8)]
            public string fromSence;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint hitMd5;
            [ProtoMember(5)]
            public string md5;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.AppMsg msg;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public ulong msgForwardType;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int reqTime;
            [ProtoMember(7)]
            public string signature;
        }

        [ProtoContract]
        public class SendAppMsgResponse
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string appId;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string from;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public ulong t9;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string to;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint type;
        }

        [ProtoContract]
        public class SendEmojiResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int emojiItemCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public byte[] tag3;
        }

        [ProtoContract]
        public class SessionKey
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] key;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int len;
        }

        [ProtoContract]
        public class SetChatRoomAnnouncementRequest
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string announcement;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string chatRoomName;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint setAnnouncementFlag;
        }

        [ProtoContract]
        public class SetChatRoomAnnouncementResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
        }

        [ProtoContract]
        public class ShowStyleKey
        {
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public byte[] key;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint keycount;
        }

        [ProtoContract]
        public class SKBuiltinString
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string @string = "";
        }

        [ProtoContract]
        public class SKBuiltinString_
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] buffer = null;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint iLen = 0;
        }

        [ProtoContract]
        public class SKBuiltinString_Int
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint @int;
        }

        [ProtoContract]
        public class SKBuiltinString_S
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string buffer;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint iLen;
        }

        [ProtoContract]
        public sealed class SnsAction
        {
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int commentId;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string content;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint createtime;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string from;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string fromnickname;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public int replyCommentId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string to;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string tonickname;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SnsObjectType type;
        }

        [ProtoContract]
        public sealed class SnsActionGroup
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SnsAction currentAction;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong id;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public ulong parentId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SnsAction referAction;
        }

        [ProtoContract]
        public class SnsAdExpInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong hateFeedid;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint hateTimestamp;
        }

        [ProtoContract]
        public class SnsBufferUrl
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint type;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string url;
        }

        [ProtoContract]
        public class SnsCommentInfo
        {
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int commentId;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string content;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string nickname;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public int replyCommentId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string replyUsername;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint type;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        [ProtoContract]
        public sealed class SnsCommentRequest
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SnsActionGroup action;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string clientid;
        }

        [ProtoContract]
        public sealed class SnsCommentResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SnsObject snsObject;
        }

        [ProtoContract]
        public class SnsGroup
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong GroupId;
        }

        public enum SnsMediaType
        {
            MMSNS_DATA_MUSIC = 5,
            MMSNS_DATA_PHOTO = 2,
            MMSNS_DATA_SIGHT = 6,
            MMSNS_DATA_TEXT = 1,
            MMSNS_DATA_VIDEO = 4,
            MMSNS_DATA_VOICE = 3
        }

        [ProtoContract]
        public sealed class SnsObject : IComparable
        {
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_[] blackList;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public uint blackListCount;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public uint commentCount;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.SnsCommentInfo[] commentUserList;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint commentUserListCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public uint deleteFlag;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public uint extFlag;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public uint groupCount;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public MM.SnsGroup[] groupList;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_[] groupUser;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public uint groupUserCount;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong id;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public uint isNotRichText;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint likeCount;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint likeFlag;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SnsCommentInfo[] likeUserList;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint likeUserListNum;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string nickname;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public uint noChange;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S objectDesc;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ objectOperations;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public MM.PreDownloadInfo preDownloadInfo;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public ulong referId;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public string referUsername;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public MM.SnsRedEnvelops snsRedEnvelops;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string username;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public MM.SnsWeAppInfo weAppInfo;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint withUserCount;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public MM.SnsCommentInfo[] withUserList;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint withUserListNum;

            int IComparable.CompareTo(object obj)
            {
                return ((MM.SnsObject) obj).id.CompareTo(this.id);
            }
        }

        [ProtoContract]
        public sealed class SnsObjectDetailRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint groupDetail;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public ulong id;
        }

        [ProtoContract]
        public sealed class SnsObjectDetailResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SnsObject @object;
        }

        [ProtoContract]
        public class SnsObjectOp
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ ext;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong id;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SnsObjectOpType opType;
        }

        [ProtoContract]
        public class SnsObjectOpRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SnsObjectOp opList;
        }

        [ProtoContract]
        public class SnsObjectOpResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int[] opRetList;
        }

        public enum SnsObjectOpType
        {
            MMSNS_OBJECTOP_CANCEL_LIKE = 5,
            MMSNS_OBJECTOP_DEL = 1,
            MMSNS_OBJECTOP_DELETE_COMMENT = 4,
            MMSNS_OBJECTOP_SET_OPEN = 3,
            MMSNS_OBJECTOP_SET_PRIVACY = 2
        }

        public enum SnsObjectType
        {
            MMSNS_OBJECT_UNKNOWN,
            MMSNS_OBJECT_PHOTO,
            MMSNS_OBJECT_TEXT,
            MMSNS_OBJECT_FEED,
            MMSNS_OBJECT_MUSIC,
            MMSNS_OBJECT_VIDEO,
            MMSNS_OBJECT_LOCATION,
            MMSNS_OBJECT_BACKGROUND,
            MMSNS_OBJECT_WXSIGN,
            MMSNS_OBJECT_PRODUCT,
            MMSNS_OBJECT_EMOTION,
            MMSNS_OBJECT_OLD_TV,
            MMSNS_OBJECT_GENERAL_PRODUCT,
            MMSNS_OBJECT_GENERAL_CARD,
            MMSNS_OBJECT_TV,
            MMSNS_OBJECT_SIGHT
        }

        [ProtoContract]
        public class SnsPostCtocUploadInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint flag;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint photoCount;
        }

        [ProtoContract]
        public class SnsPostOperationFields
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint contactTagCount;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string jsAppid;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string shareUrlOpen;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string shareUrlOriginal;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint tempUserCount;
        }

        [ProtoContract]
        public class SnsPostRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] blackList;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint blackListNum;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public MM.CanvasInfo canvasInfo;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ clientCheckData;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string clientId;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public MM.SnsPostCtocUploadInfo ctocUploadInfo;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public string fromScene;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SnsGroup[] groupIds;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint groupNum;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] groupUser;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public uint groupUserNum;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public MM.MediaInfo[] mediaInfo;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public uint mediaInfoCount;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S objectDesc;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint objectSource;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ poiInfo;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint postBGImgType;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint privacy;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public ulong referId;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public MM.SnsPostOperationFields snsPostOperationFields;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public MM.SnsRedEnvelops snsRedEnvelops;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint syncFlag;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public MM.TwitterInfo twitterInfo;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public MM.SnsWeAppInfo[] weAppInfo;
            [ProtoMember(4)]
            public MM.SKBuiltinString[] withUserList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint withUserListNum;
        }

        [ProtoContract]
        public class SnsPostResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SnsObject snsObject;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string spamTips;
        }

        [ProtoContract]
        public class SnsRedEnvelops
        {
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint reportId;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint reportKey;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint resourceId;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint rewardCount;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public byte[] rewardUserList;
        }

        [ProtoContract]
        public class SnsServerConfig
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int copyAndPasteWordLimit;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int postMentionLimit;
        }

        [ProtoContract]
        public class SnsSyncRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.syncMsgKey keyBuf;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint selector;
        }

        [ProtoContract]
        public class SnsSyncResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.CmdList cmdList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint continueFlag;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.syncMsgKey keyBuf;
        }

        [ProtoContract]
        public class SnsTag
        {
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString[] list;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public ulong tagId;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string tagName;
        }

        public enum SnsTagDefaultId : ulong
        {
            MM_SNS_TAG_ID_BLACKLIST = 5L,
            MM_SNS_TAG_ID_CLASSMATE = 3L,
            MM_SNS_TAG_ID_COLLEAGUE = 2L,
            MM_SNS_TAG_ID_FRIEND = 1L,
            MM_SNS_TAG_ID_OTHERS = 6L,
            MM_SNS_TAG_ID_OUTSIDERS = 4L,
            MM_SNS_TAG_ID_PRIVATE = 0L
        }

        [ProtoContract]
        public class SnsTagListRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opCode;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string tagListMd5;
        }

        [ProtoContract]
        public class SnsTagListResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint count;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SnsTag[] list;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint opCode;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint t6;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string tagListMd5;
        }

        [ProtoContract]
        public class SnsTimeLineRequest
        {
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SnsAdExpInfo adexpinfo;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public ulong clientLastestId;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string firstPageMd5;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint lastRequestTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public ulong maxId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public ulong minFilterId;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint networkType;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ seesion;
        }

        [ProtoContract]
        public class SnsTimeLineResponse
        {
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint advertiseCount;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public byte[] advertiseList;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint controlFlag;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string fristPageMd5;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint newRequestTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint objectCount;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint objectCountForSameMd5;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SnsObject[] objectList;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public uint recCount;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public byte[] recList;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SnsServerConfig serverConfig;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ session;
        }

        [ProtoContract]
        public class SnsUploadRequest
        {
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public string appId;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ buffer;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string clientId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string descript;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint extFlag;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint filterStype;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string md5;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public int netType;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public MM.SnsObjectType objectType;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public int photoFrom;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint startPos;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint syncFlag;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint totalLen;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.TwitterInfo twitterInfo;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint type;
        }

        [ProtoContract]
        public class SnsUploadResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SnsBufferUrl bufferUrl;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string clientId;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public ulong id;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint startPos;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint thumbUrlCount;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SnsBufferUrl[] thumbUrls;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint totalLen;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint type;
        }

        [ProtoContract]
        public class SnsUserInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string snsBGImgID;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public ulong snsBGObjectID;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public uint snsFlag;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint snsFlagEx;
        }

        [ProtoContract]
        public class SnsUserPageRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string fristPageMd5;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint lastRequestTime;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public ulong maxid;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public ulong minFilterId;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint source;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        [ProtoContract]
        public class SnsUserPageResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string fristPageMd5;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public ulong limitedId;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint newRequestTime;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint objectCount;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint objectCountForSameMd5;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SnsObject[] objectList;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint objectTotalCount;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SnsServerConfig serverConfig;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
        }

        [ProtoContract]
        public class SnsWeAppInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint appId;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string mapPoiId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string redirectUrl;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint score;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint showType;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [Serializable]
        public class softtype
        {
            [XmlElement("k1")]
            public string k1;
            [XmlElement("k14")]
            public string k14;
            [XmlElement("k2")]
            public string k2;
            [XmlElement("k3")]
            public string k3;
            [XmlElement("k4")]
            public string k4;
            [XmlElement("k43")]
            public string k43;
            [XmlElement("k5")]
            public string k5;
        }

        [ProtoContract]
        public class StatusNotifyRequest
        {
            [ProtoMember(1)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(5)]
            public string clientMsgId;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint code;
            [ProtoMember(3)]
            public string fromUserName_;
            [ProtoMember(8)]
            public byte[] function;
            [ProtoMember(4)]
            public string toUserName_;
            [ProtoMember(7)]
            public byte[] unreadChatList;
            [ProtoMember(6)]
            public uint unreadChatListCount;
            [ProtoMember(10)]
            public byte[] unreadFunction;
            [ProtoMember(9)]
            public uint unreadFunctionCount;
        }

        [ProtoContract]
        public class StatusNotifyResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint msgid;
        }

        public enum SyncCmdID
        {
            CmdIdAddMsg = 5,
            CmdIdCloseMicroBlog = 12,
            CmdIdDelChatContact = 7,
            CmdIdDelContact = 4,
            CmdIdDelContactMsg = 8,
            CmdIdDelMsg = 9,
            CmdIdDelUserDomainEmail = 0x13,
            CmdIdFunctionSwitch = 0x17,
            CmdIdInviteFriendOpen = 0x16,
            CmdIdMax = 0xc9,
            CmdIdModChatRoomMember = 15,
            CmdIdModChatRoomNotify = 20,
            CmdIdModChatRoomTopic = 0x1b,
            CmdIdModContact = 2,
            CmdIdModContactDomainEmail = 0x11,
            CmdIdModMicroBlog = 13,
            CmdIdModMsgStatus = 6,
            CmdIdModNotifyStatus = 14,
            CmdIdModQContact = 0x18,
            CmdIdModTContact = 0x19,
            CmdIdModUserDomainEmail = 0x12,
            CmdIdModUserInfo = 1,
            CmdIdOpenQQMicroBlog = 11,
            CmdIdPossibleFriend = 0x15,
            CmdIdPsmStat = 0x1a,
            CmdIdQuitChatRoom = 0x10,
            CmdIdReport = 10,
            CmdInvalid = 0,
            MM_FAV_SYNCCMD_ADDITEM = 200,
            MM_GAME_SYNCCMD_ADDMSG = 0xc9,
            MM_SNS_SYNCCMD_ACTION = 0x2e,
            MM_SNS_SYNCCMD_OBJECT = 0x2d,
            MM_SYNCCMD_BRAND_SETTING = 0x2f,
            MM_SYNCCMD_DELBOTTLECONTACT = 0x22,
            MM_SYNCCMD_DELETE_SNS_OLDGROUP = 0x38,
            MM_SYNCCMD_DELETEBOTTLE = 0x20,
            MM_SYNCCMD_KVCMD = 0x37,
            MM_SYNCCMD_KVSTAT = 0x24,
            MM_SYNCCMD_MODBOTTLECONTACT = 0x21,
            MM_SYNCCMD_MODCHATROOMMEMBERDISPLAYNAME = 0x30,
            MM_SYNCCMD_MODCHATROOMMEMBERFLAG = 0x31,
            MM_SYNCCMD_MODDESCRIPTION = 0x36,
            MM_SYNCCMD_MODDISTURBSETTING = 0x1f,
            MM_SYNCCMD_MODSNSBLACKLIST = 0x34,
            MM_SYNCCMD_MODSNSUSERINFO = 0x33,
            MM_SYNCCMD_MODUSERIMG = 0x23,
            MM_SYNCCMD_NEWDELMSG = 0x35,
            MM_SYNCCMD_UPDATESTAT = 30,
            MM_SYNCCMD_USERINFOEXT = 0x2c,
            MM_SYNCCMD_WEBWXFUNCTIONSWITCH = 50,
            NN_SYNCCMD_THEMESTAT = 0x25
        }

        [ProtoContract]
        public class Synckey
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int size;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public Synckey_[] type;

            [ProtoContract]
            public class Synckey_
            {
                [ProtoMember(2, Options=MemberSerializationOptions.Required)]
                public long synckey;
                [ProtoMember(1, Options=MemberSerializationOptions.Required)]
                public int type;
            }
        }

        [ProtoContract]
        public class syncMsgKey
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public int len;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.Synckey msgkey;
        }

        public enum syncScene
        {
            MM_NEWSYNC_SCENE_AFTERINIT = 5,
            MM_NEWSYNC_SCENE_BG2FG = 3,
            MM_NEWSYNC_SCENE_CONTINUEFLAG = 6,
            MM_NEWSYNC_SCENE_NOTIFY = 1,
            MM_NEWSYNC_SCENE_OTHER = 7,
            MM_NEWSYNC_SCENE_PROCESSSTART = 4,
            MM_NEWSYNC_SCENE_TIMER = 2
        }

        [ProtoContract]
        public class TContact
        {
            [ProtoMember(2)]
            public string displayName;
            [ProtoMember(3)]
            public uint extInfoSeq;
            [ProtoMember(4)]
            public uint imgUpdateSeq;
            [ProtoMember(1)]
            public string userName;
        }

        [ProtoContract]
        public class TenPayRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.enMMTenPayCgiCmd cgiCmd;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint outPutType;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S reqText;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S reqTextWx;
        }

        [ProtoContract]
        public class TenPayResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.enMMTenPayCgiCmd cgiCmdid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string platMsg;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int platRet;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_S reqText;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string tenpayErrMsg;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int tenpayErrType;
        }

        [ProtoContract]
        public class ThrowBottleRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int bottletype;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string clientID;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ content;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int msgtype;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int startPos;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int totalLen;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint voiceLength;
        }

        [ProtoContract]
        public class TwitterInfo
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string oauthToken;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string oauthTokenSecret;
        }

        [ProtoContract]
        public class UploadEmojiRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.EmojiUploadInfo emojiItem;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public int emojiItemCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int tag4;
        }

        [ProtoContract]
        public class UploadImageRequest_CDN
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string from;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string mediaId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int t4;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int t5;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public byte[] t6;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string to;
        }

        [ProtoContract]
        public class UploadMContactRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.MEmail[] emailList;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int emailListSize;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string mobile;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public MM.Mobile[] mobileList;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int mobileListSize;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int opcode;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class UploadMContactResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
        }

        [ProtoContract]
        public class UploadMsgImgRequest
        {
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public string aESKey;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public string appid;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MMPro.MM.BaseRequest BaseRequest;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public int cDNBigImgSize;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string cDNBigImgUrl;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public int cDNMidImgSize;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string cDNMidImgUrl;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public string cDNThumbAESKey;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public int cDNThumbImgHeight;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public int cDNThumbImgSize;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public string cDNThumbImgUrl;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public int cDNThumbImgWidth;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString clientImgId;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint compressType;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public uint Crc32;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ data;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint dataLen;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public int encryVer;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString from;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public uint hitMd5;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public string md5;
            [ProtoMember(0x22, Options=MemberSerializationOptions.Required)]
            public string mediaTagName;
            [ProtoMember(0x20, Options=MemberSerializationOptions.Required)]
            public string messageAction;
            [ProtoMember(0x21, Options=MemberSerializationOptions.Required)]
            public string messageExt;
            [ProtoMember(0x1d)]
            public uint Msgforwardtype;
            [ProtoMember(10)]
            public string msgSource;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint msgType;
            [ProtoMember(12)]
            public uint netType;
            [ProtoMember(13)]
            public int photoFrom;
            [ProtoMember(0x1a)]
            public uint reqTime;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint startPos;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString to;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint totalLen;
            [ProtoMember(14)]
            public uint uICreateTime;
        }

        [ProtoContract]
        public class UploadMsgImgResponse
        {
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public string Aeskey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString clientImgId;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint dataLen;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string Fileid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString from;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public uint Msgid;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public ulong Newmsgid;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint startPos;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString to;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint totalLen;
        }

        [ProtoContract]
        public sealed class UploadVideoRequest
        {
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public string aESKey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MMPro.MM.BaseRequest BaseRequest;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public uint cameraType;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public string cDNThumbAESKey;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public int cDNThumbImgHeight;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public int cDNThumbImgSize;
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public int cDNThumbImgWidth;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string cDNThumbUrl;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string cDNVideoUrl;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(0x26, Options=MemberSerializationOptions.Required)]
            public ulong crc32;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public int encryVer;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string from;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint funcFlag;
            [ProtoMember(0x24, Options=MemberSerializationOptions.Required)]
            public uint hitMd5;
            [ProtoMember(0x27, Options=MemberSerializationOptions.Required)]
            public uint msgForwardType;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public string msgSource;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public uint networkEnv;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint playLength;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public uint reqTime;
            [ProtoMember(40, Options=MemberSerializationOptions.Required)]
            public uint Source;
            [ProtoMember(0x23, Options=MemberSerializationOptions.Required)]
            public uint staExtStr;
            [ProtoMember(0x22, Options=MemberSerializationOptions.Required)]
            public string streamVideoAdUxInfo;
            [ProtoMember(0x21, Options=MemberSerializationOptions.Required)]
            public string streamVideoPublishId;
            [ProtoMember(0x20, Options=MemberSerializationOptions.Required)]
            public string streamVideoThumbUrl;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public string streamVideoTitle;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public uint streamVideoTotalTime;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public string streamVideoUrl;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public string streamVideoWebUrl;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public string streamVideoWording;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ thumbData;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint thumbStartPos;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public int thumbTotalLen;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string to;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ videoData;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public int videoFrom;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public string videoMd5;
            [ProtoMember(0x25, Options=MemberSerializationOptions.Required)]
            public string VideoNewMd5;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint videoStartPos;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public int videoTotalLen;
        }

        [ProtoContract]
        public class UploadVideoResponse
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public string cdnthumbaeskey;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public ulong newMsgId;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint thumbStartPos;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint videoStartPos;
        }

        [ProtoContract]
        public class UploadVoiceRequest
        {
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public int cancelFlag;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ data;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public int endFlag;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public int forwardFlag;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string from;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public int length;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public int msgId;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public string msgsource;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public ulong newMsgId;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public int offset;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public int reqTime;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string to;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public int uicreateTime;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public MM.VoiceFormat voiceFormat;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ voiceId;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public int voiceLen;
        }

        [ProtoContract]
        public class UploadVoiceResponse
        {
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint cancelFlag;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public string clientMsgId;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint createTime;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public uint endFlag;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string fromUserName;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public uint length;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint msgId;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public ulong newMsgId;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint offset;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string toUserName;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint voiceLength;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct URL
        {
            public static string CGI_EXTDEVICELOGINCONFIRMGET;
            public static string CGI_MMSNSTIMELINE;
            public static string CGI_MMSNSTAGLIST;
            public static string CGI_MMSNSPORT;
            public static string CGI_MMSNSOBJECTDETAIL;
            public static string CGI_MMSNSOBJECTOP;
            public static string CGI_OPLOG;
            public static string CGI_MANUALAUTH;
            public static string CGI_NEWSYNC;
            public static string CGI_NEWSENDMSG;
            public static string CGI_STATUSNOTIFY;
            public static string CGI_UPLOADIMAGE;
            public static string CGI_ADDFAVITEM;
            public static string CGI_FAVSYNC;
            public static string CGI_GETFAVINFO;
            public static string CGI_BATCHGETFAVITEM;
            public static string CGI_GETEMOTIONDESC;
            public static string CGI_SENDEMOJI;
            public static string CGI_GETCONTACTLABELLIST;
            public static string CGI_UPLOADVOICE;
            public static string CGI_SENDAPPMSG;
            public static string CGI_UPLOADVIDEO;
            public static string CGI_MMSNSUSERPAGE;
            public static string CGI_MMSNSCOMMENT;
            public static string CGI_GETCONTACT;
            public static string CGI_GETCDNDNS;
            public static string CGI_GETPROFILE;
            public static string CGI_GETMSGIMG;
            public static string CGI_CHECKLOGINQRCODE;
            public static string CGI_GETLOGINQRCODE;
            public static string CGI_GETOPENIMRESOURCE;
            public static string CGI_GETCHATROOMMEMBERDETAIL;
            public static string CGI_GETCHATROOMINFODETAIL;
            public static string CGI_DOWNLOADVOICE;
            public static string CGI_HEARTBEAT;
            public static string CGI_GETONLINEINFO;
            public static string CGI_PUSHLOGINURL;
            public static string CGI_TIMESEED;
            public static string CGI_TENPAY;
            public static string CGI_F2FQRCODE;
            public static string CGI_TRANSFERSETF2FFEE;
            public static string CGI_BINDQUERYNEW;
            public static string CGI_GETBANNERINFO;
            public static string CGI_GETPAYFUNCTIONLIST;
            public static string CGI_TRANSFERQUERY;
            public static string CGI_GETTRANSFERWORDINH;
            public static string CGI_TRANSFEROPERATION;
            public static string CGI_VERIFYUSER;
            public static string CGI_CREATECHATROOM;
            public static string CGI_BATCHGETHEADIMG;
            public static string CGI_ADDCHATROOMMEMBER;
            public static string CGI_DELCHATROOMMEMBER;
            public static string CGI_GETA8KEY;
            public static string CGI_ADCLICK;
            public static string CGI_ADEXPOSURE;
            public static string CGI_MMSNSSYNC;
            public static string CGI_MASSSEND;
            public static string CGI_MMSNSUPLOAD;
            public static string CGI_INITCONTACT;
            public static string CGI_RECEIVEWXHB;
            public static string CGI_OPENWXHB;
            public static string CGI_QRYDETAILWXHB;
            public static string CGI_QRYLISTWXHB;
            public static string CGI_WISHWXHB;
            public static string CGI_NEWGETINVITEFRIEND;
            public static string CGI_UPLOADMCONTACT;
            public static string CGI_LBSFIND;
            public static string CGI_SETCHATROOMANNOUNCEMENT;
            public static string CGI_GETQRCODE;
            public static string CGI_SEARCHCONTACT;
            static URL()
            {
                CGI_EXTDEVICELOGINCONFIRMGET = "/cgi-bin/micromsg-bin/extdeviceloginconfirmget";
                CGI_MMSNSTIMELINE = "/cgi-bin/micromsg-bin/mmsnstimeline";
                CGI_MMSNSTAGLIST = "/cgi-bin/micromsg-bin/mmsnstaglist";
                CGI_MMSNSPORT = "/cgi-bin/micromsg-bin/mmsnspost";
                CGI_MMSNSOBJECTDETAIL = "/cgi-bin/micromsg-bin/mmsnsobjectdetail";
                CGI_MMSNSOBJECTOP = "/cgi-bin/micromsg-bin/mmsnsobjectop";
                CGI_OPLOG = "/cgi-bin/micromsg-bin/oplog";
                CGI_MANUALAUTH = "/cgi-bin/micromsg-bin/manualauth";
                CGI_NEWSYNC = "/cgi-bin/micromsg-bin/newsync";
                CGI_NEWSENDMSG = "/cgi-bin/micromsg-bin/newsendmsg";
                CGI_STATUSNOTIFY = "/cgi-bin/micromsg-bin/statusnotify";
                CGI_UPLOADIMAGE = "/cgi-bin/micromsg-bin/uploadmsgimg";
                CGI_ADDFAVITEM = "/cgi-bin/micromsg-bin/addfavitem";
                CGI_FAVSYNC = "/cgi-bin/micromsg-bin/favsync";
                CGI_GETFAVINFO = "/cgi-bin/micromsg-bin/getfavinfo";
                CGI_BATCHGETFAVITEM = "/cgi-bin/micromsg-bin/batchgetfavitem";
                CGI_GETEMOTIONDESC = "/cgi-bin/micromsg-bin/getemotiondesc";
                CGI_SENDEMOJI = "/cgi-bin/micromsg-bin/sendemoji";
                CGI_GETCONTACTLABELLIST = "/cgi-bin/micromsg-bin/getcontactlabellist";
                CGI_UPLOADVOICE = "/cgi-bin/micromsg-bin/uploadvoice";
                CGI_SENDAPPMSG = "/cgi-bin/micromsg-bin/sendappmsg";
                CGI_UPLOADVIDEO = "/cgi-bin/micromsg-bin/uploadvideo";
                CGI_MMSNSUSERPAGE = "/cgi-bin/micromsg-bin/mmsnsuserpage";
                CGI_MMSNSCOMMENT = "/cgi-bin/micromsg-bin/mmsnscomment";
                CGI_GETCONTACT = "/cgi-bin/micromsg-bin/getcontact";
                CGI_GETCDNDNS = "/cgi-bin/micromsg-bin/getcdndns";
                CGI_GETPROFILE = "/cgi-bin/micromsg-bin/getprofile";
                CGI_GETMSGIMG = "/cgi-bin/micromsg-bin/getmsgimg";
                CGI_CHECKLOGINQRCODE = "/cgi-bin/micromsg-bin/checkloginqrcode";
                CGI_GETLOGINQRCODE = "/cgi-bin/micromsg-bin/getloginqrcode";
                CGI_GETOPENIMRESOURCE = "/cgi-bin/micromsg-bin/getopenimresource";
                CGI_GETCHATROOMMEMBERDETAIL = "/cgi-bin/micromsg-bin/getchatroommemberdetail";
                CGI_GETCHATROOMINFODETAIL = "/cgi-bin/micromsg-bin/getchatroominfodetail";
                CGI_DOWNLOADVOICE = "/cgi-bin/micromsg-bin/downloadvoice";
                CGI_HEARTBEAT = "/cgi-bin/micromsg-bin/heartbeat";
                CGI_GETONLINEINFO = "/cgi-bin/micromsg-bin/getonlineinfo";
                CGI_PUSHLOGINURL = "/cgi-bin/micromsg-bin/pushloginurl";
                CGI_TIMESEED = "/cgi-bin/mmpay-bin/tenpay/timeseed";
                CGI_TENPAY = "/cgi-bin/micromsg-bin/tenpay";
                CGI_F2FQRCODE = "/cgi-bin/mmpay-bin/f2fqrcode";
                CGI_TRANSFERSETF2FFEE = "/cgi-bin/mmpay-bin/transfersetf2ffee";
                CGI_BINDQUERYNEW = "/cgi-bin/mmpay-bin/tenpay/bindquerynew";
                CGI_GETBANNERINFO = "/cgi-bin/mmpay-bin/tenpay/getbannerinfo";
                CGI_GETPAYFUNCTIONLIST = "/cgi-bin/micromsg-bin/getpayfunctionlist";
                CGI_TRANSFERQUERY = "/cgi-bin/mmpay-bin/transferquery";
                CGI_GETTRANSFERWORDINH = "/cgi-bin/mmpay-bin/gettransferwording";
                CGI_TRANSFEROPERATION = "/cgi-bin/mmpay-bin/transferoperation";
                CGI_VERIFYUSER = "/cgi-bin/micromsg-bin/verifyuser";
                CGI_CREATECHATROOM = "/cgi-bin/micromsg-bin/createchatroom";
                CGI_BATCHGETHEADIMG = "/cgi-bin/micromsg-bin/batchgetheadimg";
                CGI_ADDCHATROOMMEMBER = "/cgi-bin/micromsg-bin/addchatroommember";
                CGI_DELCHATROOMMEMBER = "/cgi-bin/micromsg-bin/delchatroommember";
                CGI_GETA8KEY = "/cgi-bin/micromsg-bin/geta8key";
                CGI_ADCLICK = "/cgi-bin/mmoc-bin/ad/click";
                CGI_ADEXPOSURE = "/cgi-bin/mmoc-bin/ad/exposure";
                CGI_MMSNSSYNC = "/cgi-bin/micromsg-bin/mmsnssync";
                CGI_MASSSEND = "/cgi-bin/micromsg-bin/masssend";
                CGI_MMSNSUPLOAD = "/cgi-bin/micromsg-bin/mmsnsupload";
                CGI_INITCONTACT = "/cgi-bin/micromsg-bin/initcontact";
                CGI_RECEIVEWXHB = "/cgi-bin/mmpay-bin/receivewxhb";
                CGI_OPENWXHB = "/cgi-bin/mmpay-bin/openwxhb";
                CGI_QRYDETAILWXHB = "/cgi-bin/mmpay-bin/qrydetailwxhb";
                CGI_QRYLISTWXHB = "/cgi-bin/mmpay-bin/qrylistwxhb";
                CGI_WISHWXHB = "/cgi-bin/mmpay-bin/wishwxhb";
                CGI_NEWGETINVITEFRIEND = "/cgi-bin/micromsg-bin/newgetinvitefriend";
                CGI_UPLOADMCONTACT = "/cgi-bin/micromsg-bin/uploadmcontact";
                CGI_LBSFIND = "/cgi-bin/micromsg-bin/lbsfind";
                CGI_SETCHATROOMANNOUNCEMENT = "/cgi-bin/micromsg-bin/setchatroomannouncement";
                CGI_GETQRCODE = "/cgi-bin/micromsg-bin/getqrcode";
                CGI_SEARCHCONTACT = "/cgi-bin/micromsg-bin/searchcontact";
            }
        }

        [ProtoContract]
        public class UserInfoExt
        {
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint bigChatRoomInvite;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint bigChatRoomQuota;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint bigChatRoomSize;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString extXml;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public uint grayscaleFlag;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint mainAcctType;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string msgPushSound;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string myBrandList;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint safeDevice;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public MM.SafeDeviceList safeDeviceList;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string safeMobile;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string voipPushSound;
        }

        [ProtoContract]
        public class USERINFOEXT
        {
            [ProtoMember(0x16, Options=MemberSerializationOptions.Required)]
            public string bbmnickName;
            [ProtoMember(0x15, Options=MemberSerializationOptions.Required)]
            public string bbpin;
            [ProtoMember(20, Options=MemberSerializationOptions.Required)]
            public string bbppid;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public uint bigChatRoomInvite;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint bigChatRoomQuota;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public uint bigChatRoomSize;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public string bigHeadImgUrl;
            [ProtoMember(30, Options=MemberSerializationOptions.Required)]
            public ulong extStatus;
            [ProtoMember(12, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString extXml;
            [ProtoMember(0x1f, Options=MemberSerializationOptions.Required)]
            public string f2FpushSound;
            [ProtoMember(0x10, Options=MemberSerializationOptions.Required)]
            public string googleContactName;
            [ProtoMember(15, Options=MemberSerializationOptions.Required)]
            public uint grayscaleFlag;
            [ProtoMember(0x11, Options=MemberSerializationOptions.Required)]
            public string idcardNum;
            [ProtoMember(0x18, Options=MemberSerializationOptions.Required)]
            public string kfinfo;
            [ProtoMember(0x17, Options=MemberSerializationOptions.Required)]
            public MM.LinkedinContactItem linkedinContackItem;
            [ProtoMember(11, Options=MemberSerializationOptions.Required)]
            public uint mainAcctType;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string msgPushSound;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string myBrandList;
            [ProtoMember(0x19, Options=MemberSerializationOptions.Required)]
            public MM.PatternLockInfo patternLockInfo;
            [ProtoMember(0x1b, Options=MemberSerializationOptions.Required)]
            public uint payWalletType;
            [ProtoMember(0x12, Options=MemberSerializationOptions.Required)]
            public string realName;
            [ProtoMember(0x13, Options=MemberSerializationOptions.Required)]
            public string regCountry;
            [ProtoMember(14, Options=MemberSerializationOptions.Required)]
            public uint safeDevice;
            [ProtoMember(13, Options=MemberSerializationOptions.Required)]
            public MM.SafeDeviceList safeDeviceList;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public string safeMobile;
            [ProtoMember(0x1a, Options=MemberSerializationOptions.Required)]
            public string securityDeviceId;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public string smallHeadImgUrl;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.SnsUserInfo snsUserInfo;
            [ProtoMember(0x20, Options=MemberSerializationOptions.Required)]
            public int userStatus;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public string voipPushSound;
            [ProtoMember(0x1d, Options=MemberSerializationOptions.Required)]
            public uint walletRegion;
            [ProtoMember(0x1c, Options=MemberSerializationOptions.Required)]
            public string weiDianInfo;
        }

        [ProtoContract]
        public class VerifyUser
        {
            [ProtoMember(3)]
            public string antispamTicket;
            [ProtoMember(5)]
            public string chatRoomUserName;
            [ProtoMember(4)]
            public uint friendFlag;
            [ProtoMember(9)]
            public string reportInfo;
            [ProtoMember(8)]
            public uint scanQrcodeFromScene;
            [ProtoMember(7)]
            public string sourceNickName;
            [ProtoMember(6)]
            public string sourceUserName;
            [ProtoMember(1)]
            public string value;
            [ProtoMember(2)]
            public string verifyUserTicket;
        }

        public enum VerifyUserOpCode
        {
            MM_VERIFYUSER_ADDCONTACT = 1,
            MM_VERIFYUSER_RECVERREPLY = 6,
            MM_VERIFYUSER_SENDERREPLY = 5,
            MM_VERIFYUSER_SENDREQUEST = 2,
            MM_VERIFYUSER_VERIFYOK = 3,
            MM_VERIFYUSER_VERIFYREJECT = 4
        }

        [ProtoContract]
        public class VerifyUserRequest
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ clientCheckData;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.VerifyUserOpCode opcode;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_[] sceneList;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint sceneListCount;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string verifyContent;
            [ProtoMember(9, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_[] verifyInfoList;
            [ProtoMember(8, Options=MemberSerializationOptions.Required)]
            public uint verifyInfoListCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.VerifyUser[] verifyUserList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint verifyUserListSize;
        }

        [ProtoContract]
        public class VerifyUserRequest1
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseRequest baseRequest;
            [ProtoMember(10, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ clientCheckData;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public MM.VerifyUserOpCode opcode;
            [ProtoMember(7, Options=MemberSerializationOptions.Required)]
            public byte[] sceneList;
            [ProtoMember(6, Options=MemberSerializationOptions.Required)]
            public uint SceneListNumFieldNumber;
            [ProtoMember(5, Options=MemberSerializationOptions.Required)]
            public string verifyContent;
            [ProtoMember(9)]
            public byte[] verifyInfoList;
            [ProtoMember(8)]
            public int verifyInfoListCount;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.VerifyUser[] verifyUserList;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public uint verifyUserListSize;
        }

        [ProtoContract]
        public class VerifyUserResponese
        {
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public MM.BaseResponse baseResponse;
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string userName;
        }

        [ProtoContract]
        public class VerifyUserValidTicket
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string antispamticket;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string username;
        }

        public enum VoiceFormat
        {
            MM_VOICE_FORMAT_AMR = 0,
            MM_VOICE_FORMAT_MP3 = 2,
            MM_VOICE_FORMAT_SILK = 4,
            MM_VOICE_FORMAT_SPEEX = 1,
            MM_VOICE_FORMAT_UNKNOWN = -1,
            MM_VOICE_FORMAT_WAVE = 3
        }

        [ProtoContract]
        public class WTLoginImgReqInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string Imgcode;
            [ProtoMember(3, Options=MemberSerializationOptions.Required)]
            public string Imgencrtptkey;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string Imgsid;
            [ProtoMember(4, Options=MemberSerializationOptions.Required)]
            public MM.SKBuiltinString_ Ksid;
        }

        [ProtoContract]
        public class WxVerifyCodeReqInfo
        {
            [ProtoMember(2, Options=MemberSerializationOptions.Required)]
            public string Verifycontent;
            [ProtoMember(1, Options=MemberSerializationOptions.Required)]
            public string Verifysignatur;
        }
    }
}

