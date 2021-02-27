using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MicroMsg.Manager;
using DBUtility;
using MicroMsg.Storage;

namespace MicroMsg.Common.Utils
{
    public enum RelyMsgType
    {
        Text,
        Voice,
        Img,
        Xml
    }
    public class RedisConfig
    {
        public static List<string> _users = new List<string>();
        private static List<string> _adminGroup = new List<string>();
        private static bool _isReplyTextMsg = true;
        private static bool _isReplyVoiceMsg;
        private static bool _isReplyImgMsg;
        private static bool _isLive;
        private static Dictionary<string, bool> _liveRooms = new Dictionary<string, bool>();
        private static bool _isCacheFiles;
        private static string _CacheServerUrl;
        private static string _acceptFriendTextReq;
        private static bool _isRelpyAcceptReq;
        private static bool _isSuperRobot;
        private static string _joinRoomRelpyTextMsg;
        private static string _joinRoomRelpyAppMsg;
        private static string _acceptFriendXmlReq;
        private static bool _mainMethod = true;
        private static bool _isCheckFriends;
        private static List<string> _teacher = new List<string>();
        private static Dictionary<string, string> _outoReply = new Dictionary<string, string>();
        private static Dictionary<string, bool> _pluginControl = new Dictionary<string, bool>();
        private static Dictionary<int, string> _signReplyText = new Dictionary<int, string>();
        private static Dictionary<int, string> _qiangjieReplyText = new Dictionary<int, string>();
        //调试flag
        public static bool flag = false;
        //用户flag
        public static bool userflag = false;
        public static string headimg;
        private static string _snsText;

        public static string SnsText
        {
            get
            {
                // _snsText = "机器猫第一时间自动为您点赞！机器猫若不回复则被系统屏蔽请稍后再试。如果有打扰请给您带来不便请删除本好友~❤~好友都快满啦~有需要批量清理好友清理朋友圈或克隆朋友圈的微信大咖们请联系微信p0ny1213非外挂清理。操作须知:清理或删除的微信需要绑定手机接收验证码给您清理(不放心的不要加,担心账号被盗的不要加,没时间跟小人墨迹)。非外挂。非诚勿扰"; 
                _snsText = "本账号已成功实现多终端登陆,完美实现人机接管。无聊的话就给我发指令:唱歌、";

                return _snsText; }
            set { _snsText = value; }
        }
        private static bool _intelligentReply;
        /// <summary>
        /// 智能问答所有对话转发到微软小兵识别 gh_bd64732c6740
        /// </summary>
        public static bool IntelligentReply
        {
            get { return _intelligentReply; }
            set { _intelligentReply = value; }
        }
        /// <summary>
        /// 抢劫概率
        /// </summary>
        public static int QjPrcent = 15;
        /// <summary>
        /// 入狱概率
        /// </summary>
        public static int RyPrcent = 60;
        /// <summary>
        /// 劫狱概率
        /// </summary>
        public static int JyPrcent = 10;
        /// <summary>
        /// 抢劫系统提示
        /// </summary>
        public static Dictionary<int, string> QiangjieReplyText
        {
            get
            {
                if (_qiangjieReplyText.Count == 0)
                {
                    _qiangjieReplyText.Add(1, "@{0}真是人品爆发,在抢劫过程中成功的击晕了对方。并从他身上搜出{1}个金币~[呲牙]高兴的走了~");
                    _qiangjieReplyText.Add(2, "@{0}拿出[刀]冲对方飞奔了过去，他吓得裤子都湿了[发抖]{{{(>_<)}}}乖乖上缴了{1}个金币");
                    _qiangjieReplyText.Add(3, "@{0}经过多次失败多次被抓，这次预谋了好久。头戴丝袜手拿[菜刀]向对方大喊：打劫！打劫...打劫[回头]简直碉堡了！抢光了他仅剩的{1}金币变成了一个身无分文的穷光蛋！");
                    //抢劫未成功
                    _qiangjieReplyText.Add(4, "@{0}被巡逻的警察逮个正着[足球]罚款{1}个金币");
                    _qiangjieReplyText.Add(5, "你就是屡战屡败江湖号称'神偷小王子'碰巧警察正在泡妹纸,{0}躲过了一劫");
                    _qiangjieReplyText.Add(6, "@{0}你连一把水果刀都买不起抢什么劫,还是洗洗睡吧[心碎]@{1}一脸满满的鄙视(╬▔皿▔)凸");
                    _qiangjieReplyText.Add(7, "抢劫的过程中由于警察正在巡逻[尴尬]@{0}心一慌一不小心踩到香蕉皮滑进了下水道里[衰]");
                    _qiangjieReplyText.Add(8, "@{0}头戴丝袜就冲过去了,一摸兜[菜刀]忘记带了,对方顺势回手来了一大耳光[晕]");
                    _qiangjieReplyText.Add(9, "@{0}你就算打劫他也什么都得不到,他已经穷的在大街上裸奔了[跳跳]");
                    //抢劫逆转
                    _qiangjieReplyText.Add(10, "@{0}今天运气不是很好,碰巧@{1}也在打劫中.可谓狭路相逢勇者胜啊。拿起[刀]就冲了过去,一脚踩到狗屎[便便]想走个狗屎运都这么难！哎[心碎]");
                    _qiangjieReplyText.Add(11, "@{0}已经入狱不得参与任何娱乐项目！好好做人，洗心革面.不要在干都寂寞狗的事了.出狱时间：{1:yyyy年MM月dd日HH时mm分ss秒}达到指定时间回复出狱即可[愉快]");
                    _qiangjieReplyText.Add(12, "@{0}对方已入狱,你也想进监狱了嘛？真是泥菩萨过江[乒乓]冒险可回复劫狱@对方[坏笑][勾引]");
                    _qiangjieReplyText.Add(13, "@{0}毛都没抢到就被警察逮住了~送进了监狱，禁闭30分钟后才可出狱！[再见]出狱时间{1:yyyy年MM月dd日HH时mm分ss秒}达到指定时间回复出狱即可[愉快]");
                    //不在监狱
                    _qiangjieReplyText.Add(14, "@{0}搞错了吧~{1}人家在KTV呢 小心我关了你！[抠鼻]");
                    //劫狱成功
                    _qiangjieReplyText.Add(15, "@{0}在监狱旁挖了个洞，居然把@{1}给拽了出来！[呲牙]");
                    _qiangjieReplyText.Add(16, "@{0}光天化日下，居然学电影劫狱，自己也被关了吧！[阴险]出狱时间{1:yyyy年MM月dd日HH时mm分ss秒}");
                    _qiangjieReplyText.Add(17, "@{0}真是铤而走险，不但没有劫狱成功！[憨笑]还没有被逮住真是高啊~连宝宝都很佩服[抱拳]");


                }
                return _qiangjieReplyText;
            }
            set { _qiangjieReplyText = value; }
        }

        /// <summary>
        /// 主讲人
        /// </summary>
        public static List<string> Teacher
        {
            get
            {
                return _teacher;
            }
            set
            {
                foreach (var item in value)
                {
                    if (!_teacher.Contains(item))
                        _teacher.Add(item);
                }
            }
        }
        /// <summary>
        /// 设置该属性必须设置IsLive = true 群直播 转播房间id ,号分割
        /// </summary>
        public static Dictionary<string, bool> LiveRooms
        {
            get
            {
                if (_liveRooms.Count != 0)
                {
                    return _liveRooms;
                }
                else
                {
                    int cont;

                    var list = new MongodbHelper<ChatRoomInfo>("wx_room").List(1, 100, i => i.strChatRoomid.Contains("@"), out cont);
                    for (int i = 0; i < list.Count; i++)
                    {
                        _liveRooms.Add(list[i].strChatRoomid, Convert.ToBoolean(list[i].isLive));
                    }

                    return _liveRooms;

                }
            }
            set
            {
                //单字典
                foreach (KeyValuePair<string, bool> val in value)
                {
                    if (_liveRooms.ContainsKey(val.Key))
                    {
                        _liveRooms[val.Key] = val.Value;
                    }
                    else
                    {

                        _liveRooms.Add(val.Key, val.Value);
                    }
                    //DbHelperSQL.ExecuteSql("update wx_room set islive=" +Convert.ToInt32(val.Value).ToString() + " where room='" + val.Key + "'");

                }

            }
        }
        public static Dictionary<int, string> SignReplyText
        {
            get
            {
                if (_signReplyText.Count == 0)
                {
                    _signReplyText.Add(0, "");
                    _signReplyText.Add(1, "");
                }
                return _signReplyText;
            }
            set { _signReplyText = value; }
        }
        private static bool _isReplyLuckyMoney;
        /// <summary>
        /// 是否回复红包消息并转发红包
        /// </summary>
        public static bool IsReplyLuckyMoney
        {
            get
            {

                return _isReplyLuckyMoney;
            }
            set
            {

                _isReplyLuckyMoney = value;
            }
        }
        /// <summary>
        /// 插件控制 插件名 是否启用
        /// </summary>
        public static Dictionary<string, bool> PluginControl
        {
            get
            {
                if (!_pluginControl.ContainsKey("Plugin_Reply"))
                {
                    _pluginControl.Add("Plugin_Reply", true);
                }
                return _pluginControl;
            }
            set
            {
                //单字典
                foreach (KeyValuePair<string, bool> val in value)
                {
                    if (!_pluginControl.ContainsKey(val.Key))
                    {
                        _pluginControl.Add(val.Key, val.Value);
                    }
                    else
                    {

                        _pluginControl[val.Key] = val.Value;
                    }
                }


                //_pluginControl = value;
            }
        }
        /// <summary>
        /// 是否检测好友并删除 规则 朋友圈被屏蔽 或朋友圈状态少于10条 视为不活跃用户 小号并删除
        /// </summary>
        public static bool IsCheckFriends
        {
            get { return _isCheckFriends; }
            set { _isCheckFriends = value; }
        }
        /// <summary>
        /// 机器人总体回复开关 默认启用
        /// </summary>
        public static bool MainMethod
        {
            get { return _mainMethod; }
            set { _mainMethod = value; }
        }
        /// <summary>
        /// 通过好友请求后回复xml信息,系统信息,单图文信息,多图文信息
        /// </summary>
        public static string AcceptFriendXmlReq
        {
            get { return _acceptFriendXmlReq; }
            set { if (_isRelpyAcceptReq)_acceptFriendXmlReq = value; }
        }
        /// <summary>
        /// 入群后回复卡片消息,多图文,单图文
        /// </summary>
        public static string JoinRoomRelpyAppMsg
        {
            get { return _joinRoomRelpyAppMsg; }
            set
            {
                if (_isSuperRobot)
                    _joinRoomRelpyAppMsg = value;
            }
        }
        /// <summary>
        /// 优先回复 自定义问答列表 问 答
        /// </summary>
        public static Dictionary<string, string> OutoReplyList
        {
            get { return _outoReply; }
            set
            {
                _outoReply.Clear();
                _outoReply = value;
            }
        }
        /// <summary>
        ///必须启用群管后调用 入群提醒回复文本消息
        /// </summary>
        public static string JoinRoomRelpyTextMsg
        {
            get { return _joinRoomRelpyTextMsg; }
            set { if (_isSuperRobot)_joinRoomRelpyTextMsg = value; }
        }

        /// <summary>
        /// 是否启用群管理
        /// </summary>
        public static bool IsSuperRobot
        {
            get { return _isSuperRobot; }
            set { _isSuperRobot = value; }
        }

        /// <summary>
        /// 是否启用自动通过好友请求
        /// </summary>
        public static bool IsRelpyAcceptReq
        {
            get { return _isRelpyAcceptReq; }
            set { _isRelpyAcceptReq = value; }
        }

        /// <summary>
        /// 通过好友请求后回复text内容
        /// </summary>
        public static string AcceptFriendTextReq
        {
            get { return _acceptFriendTextReq; }

            set
            {
                if (_isRelpyAcceptReq)
                    _acceptFriendTextReq = value;
            }
        }

        /// <summary>
        /// 缓存服务器网址
        /// </summary>
        public static string CacheServerUrl
        {
            get { return _CacheServerUrl; }
            set { _CacheServerUrl = value; }
        }
        /// <summary>
        /// 是否缓存非文字消息的文件 如缓存语音,图片等信息
        /// </summary>
        public static bool IsCacheFiles
        {
            get { return _isCacheFiles; }
            set { _isCacheFiles = value; }
        }

        /// <summary>
        /// 是否启用群直播 群转播服务
        /// </summary>
        public static bool IsLive
        {
            get { return _isLive; }
            set { _isLive = value; }
        }

        private static int _relpyTimes = 30;
        /// <summary>
        /// 回复概率 默认回复概率30%
        /// </summary>
        public static int RelpyTimes
        {
            get { return _relpyTimes; }
            set
            {
                if (value >= 0 && value <= 100)
                { _relpyTimes = value; }
            }
        }
        /// <summary>
        /// 是否回复文字消息 默认为真
        /// </summary>
        public static bool IsReplyTextMsg
        {
            get { return _isReplyTextMsg; }
            set { _isReplyTextMsg = value; }
        }

        /// <summary>
        /// 是否回复图片消息 默认为假
        /// </summary>
        public static bool IsReplyImgMsg
        {
            get { return _isReplyImgMsg; }
            set { _isReplyImgMsg = value; }
        }

        /// <summary>
        /// 是否回复语音消息 默认为假
        /// </summary>
        public static bool IsReplyVoiceMsg
        {
            get { return _isReplyVoiceMsg; }
            set { _isReplyVoiceMsg = value; }
        }
        /// <summary>
        /// 管理员组,号分割 空为不启用非空为启用 禁止@room作为管理员
        /// 
        /// </summary>
        public static List<string> AdminGroup
        {
            get
            {
                if (_adminGroup.Count == 0)
                {
                    _adminGroup.Add("ntsafe-hkk");

                }
                return _adminGroup;
            }
            set
            {
                foreach (var item in value)
                {
                    if (!_adminGroup.Contains(item))
                        _adminGroup.Add(item);
                }
            }
        }

    }


}
