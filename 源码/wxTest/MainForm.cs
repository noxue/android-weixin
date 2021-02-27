using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using mm.command;
using System.IO;
using System.Net;
using 微信挂机;
using System.Threading;
using System.IO.Compression;

/*
*1、登陆
*2、修改资料（名字、性别、地区、头像、个性号、个性签名）
*3、附近人
4、摇一摇
5、漂流瓶
*6、通讯录上传
*7、搜索对方账号，申请加好友！ 
*8、发送消息 
*9、绑定QQ 绑定邮箱 解绑手机
*10、手机号注册
*11、朋友圈发相册和文字
 */

namespace wxTest
{
    public partial class MainForm : Form
    {
        Dictionary<string, string> DeviceMap = new Dictionary<string, string>();
        Core core = null;
        userInfo user = new userInfo();

        public MainForm()
        {
            InitializeComponent();
            ServicePointManager.Expect100Continue = false;

            LoadDevices();
            LoadHardInfo();
        }

        private void LoadDevices()
        {
            string[] all = File.ReadAllLines("devicelist.txt");

            foreach (string item in all)
            {
                if (item.Contains("----"))
                {
                    string[] items = item.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length != 2)
                        continue;
                    DeviceMap[items[0]] = items[1];
                }
            }
        }

        static List<string> hardinfolist = new List<string>();
        internal static void LoadHardInfo()
        {
            string[] all = File.ReadAllLines("hardinfo.txt", Encoding.Default);
            foreach (string item in all)
            {
                if (!string.IsNullOrEmpty(item) && item.Length > 10)
                    hardinfolist.Add(item);
            }
        }
        private static Random rnd = new Random();
        string[] wifiname = { "TP-LINK_", "360WiFi-", "Tenda_" };

        private void btnLogin_Click_b(object sender, EventArgs e)
        {
            string hardinfo = hardinfolist[rnd.Next(hardinfolist.Count)];
            string[] harditems = hardinfo.Split('|');
            string androidAPIVersion = harditems[28];
            string manufacture = harditems[25];
            string model = harditems[7];
            string version = harditems[2];
            string display = harditems[27];
            string version_cremental = harditems[26];
            string abi = harditems[29];
            string imei = Fun.GenIEMI(harditems[3]);


            string blmac = Fun.GenMac(harditems[13]);
            string wifimac = Fun.GenMac(harditems[16]);
            string mac = Fun.GenMac(harditems[12]);
            string wifi = Fun.GenSerial(wifiname[rnd.Next(wifiname.Length)], 6);
            string androidid = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(GetCurTime().ToString(), "md5").ToLower().Substring(0, 16);
            string mobilemd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("+86" + txtUser.Text, "md5").ToLower();

            string fingerprint = string.Format("<softtype><lctmoc>0</lctmoc><level>1</level><k26>0</k26><k1>{0}</k1><k2>{1}</k2><k3>{2}</k3><k4>{3}</k4><k5>{4}</k5><k6>{5}</k6><k7>{6}</k7><k8>{7}</k8><k9>{8}</k9><k10>{9}</k10>" +
                "<k11>{10}</k11><k12>{11}</k12><k13>{12}</k13><k14>{13}</k14><k15>{14}</k15><k16>{15}</k16><k18>{16}</k18><k21>\"{17}\"</k21><k22>{18}</k22><k24>{19}</k24><k30>\"{20}\"</k30>" +
                "<k33>com.tencent.mm</k33><k34>{21}</k34><k35>{22}</k35><k36>{23}</k36><k37>{24}</k37><k38>{25}</k38><k39>{26}</k39><k40>{27}</k40><k41>0</k41><k42>{28}</k42><k43>null</k43><k44>0</k44><k45>{29}</k45><k46></k46><k47>wifi</k47><k48>{3}</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>980</k57><k58></k58><k59>2</k59></softtype>",
                            harditems[0], harditems[1], harditems[2], imei, Fun.GenSerial(harditems[4], 15 - harditems[4].Length),
                            Fun.GenSerial(harditems[5], 20 - harditems[5].Length), androidid, Fun.GenSerial("", harditems[6].Length),
                            harditems[7], harditems[8], harditems[9], harditems[10], harditems[11], mac, blmac.ToUpper(),
                            harditems[14], "18c867f0717aa67b2ab7347505ba07ed", wifi,
                            harditems[15], wifimac, wifi, harditems[17], harditems[18], harditems[19], harditems[20], harditems[21], harditems[22], harditems[23],
                            harditems[24], mobilemd5);
            //fingerprint= "<softtype><lctmoc>0</lctmoc><level>1</level><k1>ARMv7 processor rev 1 (v7l) </k1><k2></k2><k3>5.1.1</k3><k4>865166024671219</k4><k5>460007337766541</k5><k6>89860012221746527381</k6><k7>d3151233cfbb4fd4</k7><k8>unknown</k8><k9>iPhon 100</k9><k10>2</k10><k11>placeholder</k11><k12>0001</k12><k13>0000000000000001</k13><k14>01:61:19:58:78:d2</k14><k15></k15><k16>neon vfp swp half thumb fastmult edsp vfpv3 idiva idivt</k16><k18>e89b158e4bcf922ebd09eb83f5378e11</k18><k21>\"wireless\"</k21><k22></k22><k24>41:27:91:12:5e:14</k24><k26>0</k26><k30>\"wireless\"</k30><k33>com.tencent.mm</k33><k34>Android-x86/android_x86/x86:5.1.1/LMY48Z/denglibo08021647:userdebug/test-keys</k34><k35>vivo v3</k35><k36>unknown</k36><k37>iPhon</k37><k38>x86</k38><k39>android_x86</k39><k40>taurus</k40><k41>1</k41><k42>100</k42><k43>null</k43><k44>0</k44><k45></k45><k46></k46><k47>wifi</k47><k48>865166024671219</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>1080</k57><k58></k58><k59>0</k59></softtype>";
            string devicetype = string.Format("<deviceinfo><MANUFACTURER name=\"{0}\"><MODEL name=\"{1}\"><VERSION_RELEASE name=\"{2}\"><VERSION_INCREMENTAL name=\"{3}\"><DISPLAY name=\"{4}\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>",
                            manufacture, model, version, version_cremental, display);

            user.imei = imei;
            user.deviceID = Fun.GenDeviceID();
            user.ostype = androidAPIVersion;
            user.manufacturer = manufacture;
            user.Model = model;
            user.release = version;
            user.display = display;
            user.incremental = version_cremental;
            user.fingerprint = fingerprint;
            user.abi = abi;
            user.devicetype = devicetype;

            if (DeviceMap.ContainsKey(txtUser.Text))
            {
                string[] items = DeviceMap[txtUser.Text].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                user.imei = items[0];
                user.deviceID = items[1];
                user.ostype = items[2];
                user.manufacturer = manufacture;
                user.Model = model;
                user.release = version;
                user.display = display;
                user.incremental = version_cremental;
                user.abi = items[8];
                user.fingerprint = items[9];

                manufacture = items[3];
                model = items[4];
                version = items[5];
                display = items[6];
                version_cremental = items[7];

                devicetype = string.Format("<deviceinfo><MANUFACTURER name=\"{0}\"><MODEL name=\"{1}\"><VERSION_RELEASE name=\"{2}\"><VERSION_INCREMENTAL name=\"{3}\"><DISPLAY name=\"{4}\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>",
                            manufacture, model, version, version_cremental, display);
                user.devicetype = devicetype;
            }
            else
            {
                SaveFile("DeviceList.txt", string.Format("{0}||{1}----{2}----{3}----{4}----{5}----{6}----{7}----{8}----{9}----{10}",
                                                        txtUser.Text, user.imei, user.deviceID, androidAPIVersion, manufacture, model, version,
                                                                            display, version_cremental, abi, fingerprint));
            }

            user.user = txtUser.Text;
            user.pwd = txtPass.Text;

            user.deviceID = txtData.Text;

            core = new Core(user);
            NewAuthResponse resp = core.ManualAuth(fingerprint, devicetype, abi);
            if (resp == null)
            {
                //代理出错，更换
            }
            sss = resp.Auth.AutoAuthKey;
            //resp = core.AutoAuth(sss);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            Debug.Print(resp.Base.ErrMsg.String);
            if (resp.Base.Ret == 0 || resp.Base.Ret == -17)
            {
                string username = resp.User.UserName;
                authkey = resp.Auth.AutoAuthKey.Buffer.ToBase64();
                Debug.Print(resp.User.ToString());
                MessageBox.Show("登录成功");
            }
            else
            {
                MessageBox.Show(string.Format("登录失败，错误代码：{0}-{1}", resp.Base.Ret, resp.Base.ErrMsg));
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string hardinfo = hardinfolist[rnd.Next(hardinfolist.Count)];
            string[] harditems = hardinfo.Split('|');
            string androidAPIVersion = harditems[28];
            string manufacture = harditems[25];
            string model = harditems[7];
            string version = harditems[2];
            string display = harditems[27];
            string version_cremental = harditems[26];
            string abi = harditems[29];
            string imei = Fun.GenIEMI(harditems[3]);


            string blmac = Fun.GenMac(harditems[13]);
            string wifimac = Fun.GenMac(harditems[16]);
            string mac = Fun.GenMac(harditems[12]);
            string wifi = Fun.GenSerial(wifiname[rnd.Next(wifiname.Length)], 6);
            string androidid = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(GetCurTime().ToString(), "md5").ToLower().Substring(0, 16);
            string mobilemd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("+86" + txtUser.Text, "md5").ToLower();

            string fingerprint = string.Format("<softtype><lctmoc>0</lctmoc><level>1</level><k26>0</k26><k1>{0}</k1><k2>{1}</k2><k3>{2}</k3><k4>{3}</k4><k5>{4}</k5><k6>{5}</k6><k7>{6}</k7><k8>{7}</k8><k9>{8}</k9><k10>{9}</k10>" +
                "<k11>{10}</k11><k12>{11}</k12><k13>{12}</k13><k14>{13}</k14><k15>{14}</k15><k16>{15}</k16><k18>{16}</k18><k21>\"{17}\"</k21><k22>{18}</k22><k24>{19}</k24><k30>\"{20}\"</k30>" +
                "<k33>com.tencent.mm</k33><k34>{21}</k34><k35>{22}</k35><k36>{23}</k36><k37>{24}</k37><k38>{25}</k38><k39>{26}</k39><k40>{27}</k40><k41>0</k41><k42>{28}</k42><k43>null</k43><k44>0</k44><k45>{29}</k45><k46></k46><k47>wifi</k47><k48>{3}</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>980</k57><k58></k58><k59>2</k59></softtype>",
                            harditems[0], harditems[1], harditems[2], imei, Fun.GenSerial(harditems[4], 15 - harditems[4].Length),
                            Fun.GenSerial(harditems[5], 20 - harditems[5].Length), androidid, Fun.GenSerial("", harditems[6].Length),
                            harditems[7], harditems[8], harditems[9], harditems[10], harditems[11], mac, blmac.ToUpper(),
                            harditems[14], "18c867f0717aa67b2ab7347505ba07ed", wifi,
                            harditems[15], wifimac, wifi, harditems[17], harditems[18], harditems[19], harditems[20], harditems[21], harditems[22], harditems[23],
                            harditems[24], mobilemd5);
            string devicetype = string.Format("<deviceinfo><MANUFACTURER name=\"{0}\"><MODEL name=\"{1}\"><VERSION_RELEASE name=\"{2}\"><VERSION_INCREMENTAL name=\"{3}\"><DISPLAY name=\"{4}\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>",
                            manufacture, model, version, version_cremental, display);

            user.imei = imei;
            user.ostype = androidAPIVersion;
            user.manufacturer = manufacture;
            user.Model = model;
            user.release = version;
            user.display = display;
            user.incremental = version_cremental;
            user.fingerprint = fingerprint;
            user.abi = abi;
            user.devicetype = devicetype;

            user.user = txtUser.Text;
            user.pwd = txtPass.Text;
            string a16 = txtA16.Text;
            if (a16.Length == 16)
            {
                a16 = a16.Remove(15);
            }
            user.deviceID = a16;

            core = new Core(user);
            NewAuthResponse resp = core.ManualAuth(user.fingerprint, user.devicetype, user.abi);
            if (resp == null)
            {
                //代理出错，更换
            }
            sss = resp.Auth.AutoAuthKey;
            //resp = core.AutoAuth(sss);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            Debug.Print(resp.Base.ErrMsg.String);
            if (resp.Base.Ret == 0 || resp.Base.Ret == -17)
            {
                string username = resp.User.UserName;
                authkey = resp.Auth.AutoAuthKey.Buffer.ToBase64();
                Debug.Print(resp.User.ToString());
                MessageBox.Show("登录成功");
            }
            else
            {
                MessageBox.Show(string.Format("登录失败，错误代码：{0}-{1}", resp.Base.Ret, resp.Base.ErrMsg));
            }
        }
        private void SaveFile(string fileName, string writeInfo)
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(writeInfo);
            }
        }
        SKBuiltinBuffer_t sss;
        SearchContactResponse cardinfo;
        string antiSpamTicket;
        private static uint ClientMsgID = 20;
        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long GetCurTime()
        {
            return (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds);
        }
        private string zone = "";
        private string devicetype;

        string tick;

        byte[] notifykey = null;

        private string ticket;
        private string authkey;

        private void bnGetMContact()
        {
            for (int i = 0; i < 10; i++)
            {
                //Thread.Sleep(8888);
                GetMFriendResponse resp = core.GetMFriend();
                //Debug.Print(resp.ToString());
                //txtTip.Text = resp.ToString().Replace("\n", "\r\n");

                for (int n = 0; n < resp.FriendCount; n++)
                {
                    FriendObj one = resp.FriendList[n];

                    //Debug.Print(one.ToString());
                    //MobileMD5是手机号的md5，tx为了安全，并没记录手机号，而是用了md5，如果想知道是哪个手机号，就得把上传的手机进行md5对比了
                    //Debug.Print(string.Format("{0}:{1}", one.Nickname, one.MobileMD5));
                    string strfile = File.ReadAllText("通讯录.txt");
                    if (strfile.Contains(one.Nickname + "|"))
                    {
                        SaveFile("111.txt", one.Nickname + "|" + one.Signature + "|" + one.Sex + "|" + one.Country + "|" + one.City + "|" + one.MobileMD5 + "|" + one.Username + "|" + one.Str22 + "|" + one.Str20);
                    }
                    else if (strfile.Contains(one.Username))
                    {
                        SaveFile("222.txt", one.Nickname + "|" + one.Signature + "|" + one.Sex + "|" + one.Country + "|" + one.City + "|" + one.MobileMD5 + "|" + one.Username + "|" + one.Str22 + "|" + one.Str20);
                    }
                    else
                    {
                        SaveFile("通讯录.txt", one.Nickname + "|" + one.Signature + "|" + one.Sex + "|" + one.Country + "|" + one.City + "|" + one.MobileMD5 + "|" + one.Username + "|" + one.Str22 + "|" + one.Str20);
                    }
                }
            }
        }
        private string GetRandomPassword2(int passwordLen)
        {
            string randomChars2 = "7534098621";
            string password = string.Empty;
            int randomNum;
            Random random = new Random();
            for (int i = 0; i < passwordLen; i++)
            {
                randomNum = random.Next(randomChars2.Length);
                password += randomChars2[randomNum];
            }
            return password;
        }
        private string GetRandomPassword4()
        {
            Random ran = new Random();
            int r1 = ran.Next(18, 20);
            int r2 = ran.Next(000, 999);
            int r3 = ran.Next(108, 110);
            int r4 = ran.Next(222, 888);

            return r1 + "." + r2 + "," + r3 + "." + r4;
        }

        private ulong maxid = 0;

        private void btnInit_Click(object sender, EventArgs e)
        {
            NewInitResponse resp = core.NewInit();
            foreach (CmdItem ci in resp.CmdListList)
            {
                if (ci.CmdId == 1)
                {
                    UserProfile profile;
                    profile = UserProfile.ParseFrom(ci.CmdBuf.Buffer);
                    Debug.Print(profile.ToString());
                }
                else if (ci.CmdId == 2)
                {
                    ContactProfile profile = ContactProfile.ParseFrom(ci.CmdBuf.Buffer);
                    //if (profile.PersonalCard == 1)
                    {
                        Debug.Print(string.Format("{2}:{0}--{1},{3}", profile.UserName.String, profile.NickName.String, profile.PersonalCard, profile.BigHeadImgUrl));

                    }
                    CustomInfo cii = profile.CustomizedInfo;
                    string men = cii.ExternalInfo;
                }
                else if (ci.CmdId == 5)
                {
                    //我通过了你的朋友验证请求，现在我们可以开始聊天了
                    Msg m = Msg.ParseFrom(ci.CmdBuf.Buffer);
                    if (m.Content.String.Contains("验证"))
                    {
                        string sss = "ss";
                    }
                    Debug.Print(string.Format("{0}->{1}:{2}", m.FromUserName.String, m.ToUserName.String, m.Content.String));
                }
                else
                { }
            }

            //Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SearchContactResponse resp = core.SearchOne(txtMobile.Text);
            cardinfo = resp;
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            txtData.Text = resp.UserName.String;
            antiSpamTicket = resp.AntispamTicket;
            Debug.Print(resp.Base.ErrMsg.String);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*\x01 查找添加
\x10 通讯录
\x1E 二维码
\x12 附近人
\x1D 摇一摇
\x0C q好友
\x11 名片
\x03 群方式*/
            //antiSpamTicket = txtMsg.Text;
            //1---不需要验证   2---需要验证
            VerifyUserResponse resp = core.VerifyUser(txtData.Text, antiSpamTicket, 2, "\xf", "hi");
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnUploadMContact_Click(object sender, EventArgs e)
        {
            List<string> lists = new List<string>();
            //最多14个
            lists.Add("+8615536641105");
            UploadMContactResponse resp = core.UploadMContact(lists);
            //Debug.Print(resp.ToString());
            //txtTip.Text = resp.ToString().Replace("\n", "\r\n");

            //Thread.Sleep(8888);
        }

        private void btnGetMContact_Click(object sender, EventArgs e)
        {
            GetMFriendResponse resp = core.GetMFriend();
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");

            for (int n = 0; n < resp.FriendCount; n++)
            {
                FriendObj one = resp.FriendList[n];
                //Debug.Print(one.ToString());
                //MobileMD5是手机号的md5，tx为了安全，并没记录手机号，而是用了md5，如果想知道是哪个手机号，就得把上传的手机进行md5对比了
                Debug.Print(string.Format("{0}:{1}", one.Nickname, one.MobileMD5));
                SaveFile("通讯录.txt", one.Nickname + "|" + one.Signature + "|" + one.Sex + "|" + one.Country + "|" + one.City + "|" + one.MobileMD5 + "|" + one.Username + "|" + one.Str22 + "|" + one.Str20);
            }
        }

        string peer;
        string friendTicket;
        string scene;
        private void btnNewSync_Click(object sender, EventArgs e)
        {
            NewSyncResponse resp = core.NewSync();
            CmdList list = resp.CmdList;

            for (int i = 0; i < list.Count; i++)
            {
                CmdItem item = list.GetList(i);
                int cmdid = item.CmdId;
                if (cmdid != 5)
                    continue;
                SKBuiltinBuffer_t buffer = item.CmdBuf;
                Msg msg = Msg.ParseFrom(buffer.Buffer);
                int t = msg.MsgType;//1系统消息或者好友消息  37是陌生人加好友  //10000 提示好友添加成功
                string msgName = msg.FromUserName.String;
                string s = msg.MsgSource;
                // if (msgName == "fmessage")
                if (t == 37)
                {
                    string tip = msg.Content.String;
                    string tag = "encryptusername=\"";
                    int start = tip.IndexOf(tag);
                    int end = tip.IndexOf("\"", start + tag.Length);
                    //TRACE("%s\n", UTF8ToMBCS(msg).c_str());
                    peer = tip.Substring(start + tag.Length, end - start - tag.Length);

                    tag = "ticket=\"";
                    start = tip.IndexOf(tag);
                    end = tip.IndexOf("\"", start + tag.Length);
                    //TRACE("%s\n", UTF8ToMBCS(msg).c_str());
                    friendTicket = tip.Substring(start + tag.Length, end - start - tag.Length);

                    tag = "scene=\"";
                    start = tip.IndexOf(tag);
                    end = tip.IndexOf("\"", start + tag.Length);
                    //TRACE("%s\n", UTF8ToMBCS(msg).c_str());
                    string sc = tip.Substring(start + tag.Length, end - start - tag.Length);
                    char c = (char)int.Parse(sc);
                    scene = new string(c, 1);
                }
                else if (t == 1)
                {
                    string content = msg.Content.String;
                    string nbName = msg.FromUserName.String;
                }
                Debug.Print(string.Format("{0}:{1}", msg.FromUserName.String, msg.Content.String));
            }
            //Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hardinfo = hardinfolist[rnd.Next(hardinfolist.Count)];
            string[] harditems = hardinfo.Split('|');
            string androidAPIVersion = harditems[28];
            string manufacture = harditems[25];
            string model = harditems[7];
            string version = harditems[2];
            string display = harditems[27];
            string version_cremental = harditems[26];
            string abi = harditems[29];
            string imei = Fun.GenIEMI(harditems[3]);


            string blmac = Fun.GenMac(harditems[13]);
            string wifimac = Fun.GenMac(harditems[16]);
            string mac = Fun.GenMac(harditems[12]);
            string wifi = Fun.GenSerial(wifiname[rnd.Next(wifiname.Length)], 6);
            string androidid = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(GetCurTime().ToString(), "md5").ToLower().Substring(0, 16);
            string mobilemd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("+86" + txtUser.Text, "md5").ToLower();

            string fingerprint = string.Format("<softtype><lctmoc>0</lctmoc><level>1</level><k26>0</k26><k1>{0}</k1><k2>{1}</k2><k3>{2}</k3><k4>{3}</k4><k5>{4}</k5><k6>{5}</k6><k7>{6}</k7><k8>{7}</k8><k9>{8}</k9><k10>{9}</k10>" +
                "<k11>{10}</k11><k12>{11}</k12><k13>{12}</k13><k14>{13}</k14><k15>{14}</k15><k16>{15}</k16><k18>{16}</k18><k21>\"{17}\"</k21><k22>{18}</k22><k24>{19}</k24><k30>\"{20}\"</k30>" +
                "<k33>com.tencent.mm</k33><k34>{21}</k34><k35>{22}</k35><k36>{23}</k36><k37>{24}</k37><k38>{25}</k38><k39>{26}</k39><k40>{27}</k40><k41>0</k41><k42>{28}</k42><k43>null</k43><k44>0</k44><k45>{29}</k45><k46></k46><k47>wifi</k47><k48>{3}</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>980</k57><k58></k58><k59>2</k59></softtype>",
                            harditems[0], harditems[1], harditems[2], imei, Fun.GenSerial(harditems[4], 15 - harditems[4].Length),
                            Fun.GenSerial(harditems[5], 20 - harditems[5].Length), androidid, Fun.GenSerial("", harditems[6].Length),
                            harditems[7], harditems[8], harditems[9], harditems[10], harditems[11], mac, blmac.ToUpper(),
                            harditems[14], "18c867f0717aa67b2ab7347505ba07ed", wifi,
                            harditems[15], wifimac, wifi, harditems[17], harditems[18], harditems[19], harditems[20], harditems[21], harditems[22], harditems[23],
                            harditems[24], mobilemd5);
            string devicetype = string.Format("<deviceinfo><MANUFACTURER name=\"{0}\"><MODEL name=\"{1}\"><VERSION_RELEASE name=\"{2}\"><VERSION_INCREMENTAL name=\"{3}\"><DISPLAY name=\"{4}\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>",
                            manufacture, model, version, version_cremental, display);

            user.imei = imei;
            user.deviceID = Fun.GenDeviceID();
            user.ostype = androidAPIVersion;
            user.manufacturer = manufacture;
            user.Model = model;
            user.release = version;
            user.display = display;
            user.incremental = version_cremental;
            user.fingerprint = fingerprint;
            user.abi = abi;
            user.devicetype = devicetype;

            if (DeviceMap.ContainsKey(txtUser.Text))
            {
                string[] items = DeviceMap[txtUser.Text].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                user.imei = items[0];
                user.deviceID = items[1];
                user.ostype = items[2];
                user.manufacturer = manufacture;
                user.Model = model;
                user.release = version;
                user.display = display;
                user.incremental = version_cremental;
                user.abi = items[8];
                user.fingerprint = items[9];

                manufacture = items[3];
                model = items[4];
                version = items[5];
                display = items[6];
                version_cremental = items[7];

                devicetype = string.Format("<deviceinfo><MANUFACTURER name=\"{0}\"><MODEL name=\"{1}\"><VERSION_RELEASE name=\"{2}\"><VERSION_INCREMENTAL name=\"{3}\"><DISPLAY name=\"{4}\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>",
                            manufacture, model, version, version_cremental, display);
                user.devicetype = devicetype;
            }
            else
            {
                SaveFile("DeviceList.txt", string.Format("{0}||{1}----{2}----{3}----{4}----{5}----{6}----{7}----{8}----{9}----{10}",
                                                        txtUser.Text, user.imei, user.deviceID, androidAPIVersion, manufacture, model, version,
                                                                            display, version_cremental, abi, fingerprint));
            }

            user.user = txtUser.Text;
            user.pwd = txtPass.Text;

            user.deviceID = txtData.Text;

            core = new Core(user);
            NewAuthResponse resp = core.ManualAuth短(fingerprint, devicetype, abi);
            if (resp == null)
            {
                //代理出错，更换
            }
            sss = resp.Auth.AutoAuthKey;
            //resp = core.AutoAuth(sss);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            Debug.Print(resp.Base.ErrMsg.String);
            if (resp.Base.Ret == 0 || resp.Base.Ret == -17)
            {
                string username = resp.User.UserName;
                authkey = resp.Auth.AutoAuthKey.Buffer.ToBase64();
                Debug.Print(resp.User.ToString());
                //MessageBox.Show("登录成功");
            }
            else
            {
                MessageBox.Show(string.Format("登录失败，错误代码：{0}-{1}", resp.Base.Ret, resp.Base.ErrMsg));
            }
        }

        private void btnHeartBeat_Click(object sender, EventArgs e)
        {
            core.HeartBeat();
        }

        private void btnSaveSession_Click(object sender, EventArgs e)
        {
            core.SaveSession("d:\\me.txt");
        }

        private void btnLoadSession_Click(object sender, EventArgs e)
        {
            user.imei = Fun.GenIEMI("86380201");
            user.deviceID = Fun.GenDeviceID();
            user.ostype = "android-16";
            user.manufacturer = "Xiaomi";
            user.Model = "MI 2Sarmeabi-v7a";
            user.release = "4.1.1";
            user.display = "JRO03L";
            user.incremental = "JLB17.0";

            user.user = "";
            user.pwd = "";

            core = new Core(user);
            core.LoadSession("d:\\me.txt");

            SearchContactResponse resp = core.SearchOne(txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnScanLogin_Click(object sender, EventArgs e)
        {
            Geta8keyResponse resp = core.GetOAuth(txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            string url = resp.FullURL;
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 4.2.1; 2013022 Build/HM2013022; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043520 Safari/537.36 MicroMessenger/6.5.16.1120 NetType/WIFI Language/zh_CN");
            string html = wc.DownloadString(url);
            if (html.Contains("二维码已失效"))
            {
                MessageBox.Show("二维码已失效");
                return;
            }
            string tag = "action=\"";
            int start = html.IndexOf(tag);
            int end = html.IndexOf("\">", start);
            url = html.Substring(start + tag.Length, end - start - tag.Length);
            url = "https://login.weixin.qq.com" + url;
            html = wc.UploadString(url, "");
        }

        private void btnCreateChatroom_Click(object sender, EventArgs e)
        {
            List<string> memList = new List<string>();
            memList.Add("wxid_0qqokxqrasvp22");
            memList.Add("wxid_os57n9qdaqp622");
            //2-15个成员
            CreateChatRoomResponse resp = core.CreateChatroom(memList);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnChatrommChat_Click(object sender, EventArgs e)
        {
            NewSendMsgResponse resp = core.SendFriendMsg(++ClientMsgID, txtMsg.Text, (uint)(GetCurTime() / 1000), "4837969555@chatroom", 1);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnAddMem_Click(object sender, EventArgs e)
        {
            List<string> memList = new List<string>();
            memList.Add("wxid_tlmy991e2xx444");
            //不超过15个
            AddChatRoomMemberResponse resp = core.AddChatroomMember("4837969555@chatroom", memList);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnMemList_Click(object sender, EventArgs e)
        {
            GetChatRoomMemberDetailResponse resp = core.GetChatroomMemberList("4837969555@chatroom");
            Debug.Print(resp.ToString());
            foreach (ChatroomMemberInfo item in resp.NewChatroomData.ChatroomMemberList)
            {
                Debug.Print(item.UserName);
            }
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnExitChatroom_Click(object sender, EventArgs e)
        {
            OplogResponse resp = core.OpExitChatroom(16, "4837969555@chatroom", core.userName);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnScanIn_Click(object sender, EventArgs e)
        {
            Geta8keyResponse resp = core.GetOAuth(txtData.Text);

            //weixin://jump/mainframe/1260905555@chatroom----这个说明已经在群里了
            if (!resp.FullURL.Contains("@chatroom"))
            {
                string referer = "";
                string result2 = "";
                string link = "";

                link = resp.FullURL;
                request1(link, ref result2, referer);


                referer = resp.FullURL;
                link = "https://support.weixin.qq.com/cgi-bin/mmsupport-bin/reportforweb?rid=63637&rkey=89&rvalue=1";
                request1(link, ref result2, referer);

                result2 = "";
                byte[] bytes2 = Encoding.UTF8.GetBytes("forBlackberry=forceToUsePost");
                string link4 = resp.FullURL;
                referer = resp.FullURL;

                request2(bytes2, referer, link4, ref result2);
            }
        }

        private void request1(string url, ref string result, string referer = "")
        {
            try
            {
                var web = WebRequest.Create(url) as HttpWebRequest;
                web.AllowAutoRedirect = false;
                web.Headers.Add("Keep-Alive", "115");

                web.Headers.Add("Upgrade-Insecure-Requests", "1");
                web.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,image/wxpic,image/sharpp,image/apng,*/*;q=0.8";

                web.KeepAlive = true;

                web.UserAgent = "Mozilla/5.0 (Linux; Android 4.4.4; hw-CL10 Build/hw-CL10) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/33.0.0.0 Mobile Safari/537.36 MicroMessenger/6.3.16.49_r03ae324.780 NetType/WIFI Language/en_US";

                if (referer != "")
                {
                    web.Referer = referer;
                }

                web.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                web.Headers["Accept-Language"] = "zh-CN,en-US;q=0.8";

                web.Method = "GET";
                var rps = web.GetResponse() as HttpWebResponse;

                Stream responseStream = responseStream = rps.GetResponseStream();

                if (rps.ContentEncoding.ToLower().Contains("gzip"))
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                else if (rps.ContentEncoding.ToLower().Contains("deflate"))
                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                StreamReader Reader = new StreamReader(responseStream, Encoding.UTF8);
                result = Reader.ReadToEnd();

                Reader.Close();
                responseStream.Close();
                web.Abort();
                rps.Close();

            }
            catch (Exception ex)
            {
            }
        }

        private void request2(byte[] bytes, string referer, string link, ref string result)
        {
            try
            {
                var web = WebRequest.Create(link) as HttpWebRequest;
                web.AllowAutoRedirect = true;
                ;
                web.Headers.Add("Origin", "https://mp.weixin.qq.com");

                web.Headers["Accept-Language"] = "zh-CN,en-US;q=0.8";

                web.UserAgent = "Mozilla/5.0 (Linux; Android 4.4.4; hw-CL10 Build/hw-CL10) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/33.0.0.0 Mobile Safari/537.36 MicroMessenger/6.3.16.49_r03ae324.780 NetType/WIFI Language/en_US";

                web.Accept = "*/*";
                web.Referer = referer;
                web.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

                web.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

                web.Method = "POST";
                web.GetRequestStream().Write(bytes, 0, bytes.Length);

                var rps = web.GetResponse() as HttpWebResponse;

                Stream responseStream = responseStream = rps.GetResponseStream();
                if (rps.ContentEncoding.ToLower().Contains("gzip"))
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                else if (rps.ContentEncoding.ToLower().Contains("deflate"))
                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                StreamReader Reader = new StreamReader(responseStream, Encoding.UTF8);

                result = Reader.ReadToEnd();

                Reader.Close();
                responseStream.Close();
                web.Abort();
                rps.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            NewSendMsgResponse resp = core.SendFriendMsg(++ClientMsgID, txtMsg.Text, (uint)(GetCurTime() / 1000), txtData.Text, 1);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnCheckPwd_Click(object sender, EventArgs e)
        {
            NewVerifyPasswdResponse resp = core.NewVerifyPass(txtPass.Text);
            ticket = resp.Ticket;
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnSetPwd_Click(object sender, EventArgs e)
        {
            NewSetPasswdResponse resp = core.NewSetPass(txtData.Text, ticket, authkey);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnNick_Click(object sender, EventArgs e)
        {
            NewSyncResponse resp = core.ModifyNickName(txtData.Text);

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            NewSyncResponse resp = core.ModifyProfile(1, "河南", "郑州", "是 china");

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnAvatar_Click(object sender, EventArgs e)
        {
            UploadhdheadimgResponse resp = core.UploadHeadImg("img\\tt.jpg");
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnSetID_Click(object sender, EventArgs e)
        {
            //-7,id存在
            //-14，id非法

            GeneralSetResponse resp = core.SetWXID(txtData.Text);

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnCheckMobile_Click(object sender, EventArgs e)
        {
            //12 14, 15 注册
            //13, 16, 17，短信登录
            string hardinfo = hardinfolist[rnd.Next(hardinfolist.Count)];
            string[] harditems = hardinfo.Split('|');
            string androidAPIVersion = harditems[28];
            string manufacture = harditems[25];
            string model = harditems[7];
            string version = harditems[2];
            string display = harditems[27];
            string version_cremental = harditems[26];
            string abi = harditems[29];
            string imei = Fun.GenIEMI(harditems[3]);
            string brand = harditems[20];


            string blmac = Fun.GenMac(harditems[13]);
            string wifimac = Fun.GenMac(harditems[16]);
            string mac = Fun.GenMac(harditems[12]);
            string wifi = Fun.GenSerial(wifiname[rnd.Next(wifiname.Length)], 6);
            string androidid = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(GetCurTime().ToString(), "md5").ToLower().Substring(0, 16);
            string mobilemd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("+86" + txtUser.Text, "md5").ToLower();

            string fingerprint = string.Format("<softtype><lctmoc>0</lctmoc><level>1</level><k26>0</k26><k1>{0}</k1><k2>{1}</k2><k3>{2}</k3><k4>{3}</k4><k5>{4}</k5><k6>{5}</k6><k7>{6}</k7><k8>{7}</k8><k9>{8}</k9><k10>{9}</k10>" +
                "<k11>{10}</k11><k12>{11}</k12><k13>{12}</k13><k14>{13}</k14><k15>{14}</k15><k16>{15}</k16><k18>{16}</k18><k21>\"{17}\"</k21><k22>{18}</k22><k24>{19}</k24><k30>\"{20}\"</k30>" +
                "<k33>com.tencent.mm</k33><k34>{21}</k34><k35>{22}</k35><k36>{23}</k36><k37>{24}</k37><k38>{25}</k38><k39>{26}</k39><k40>{27}</k40><k41>0</k41><k42>{28}</k42><k43>null</k43><k44>0</k44><k45>{29}</k45><k46></k46><k47>wifi</k47><k48>{3}</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>980</k57><k58></k58><k59>2</k59></softtype>",
                            harditems[0], harditems[1], harditems[2], imei, Fun.GenSerial(harditems[4], 15 - harditems[4].Length),
                            Fun.GenSerial(harditems[5], 20 - harditems[5].Length), androidid, Fun.GenSerial("", harditems[6].Length),
                            harditems[7], harditems[8], harditems[9], harditems[10], harditems[11], mac, blmac.ToUpper(),
                            harditems[14], "18c867f0717aa67b2ab7347505ba07ed", wifi,
                            harditems[15], wifimac, wifi, harditems[17], harditems[18], harditems[19], harditems[20], harditems[21], harditems[22], harditems[23],
                            harditems[24], mobilemd5);
            devicetype = brand + "-" + model;

            user.imei = imei;
            user.deviceID = Fun.GenDeviceID();
            user.ostype = androidAPIVersion;
            user.manufacturer = manufacture;
            user.Model = model;
            user.release = version;
            user.display = display;
            user.incremental = version_cremental;
            user.fingerprint = fingerprint;
            user.abi = abi;
            user.devicetype = devicetype;
            user.mac = mac;
            user.androidid = androidid;
            user.clientid = user.deviceID + "0_" + GetCurTime().ToString();

            SaveFile("DeviceList.txt", string.Format("{0}||{1}----{2}----{3}----{4}----{5}----{6}----{7}----{8}----{9}----{10}",
                                                        txtUser.Text, user.imei, user.deviceID, androidAPIVersion, manufacture, model, version,
                                                                            display, version_cremental, abi, fingerprint));
            user.user = txtUser.Text;
            user.pwd = txtPass.Text;

            core = new Core(user);

            zone = "+86";
            BindopMobileForRegResponse resp = core.MobileReg(12, zone + txtUser.Text, "");
            Debug.Print(resp.ToString());
            Debug.Print(resp.Base.ErrMsg.String);
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            if (resp.Base.Ret == -301)
            {
                MessageBox.Show("需要重定向");
            }
            Debug.Print(resp.Base.ErrMsg.String);
        }

        private void btnRequestSMS_Click(object sender, EventArgs e)
        {
            BindopMobileForRegResponse resp = core.MobileReg(14, zone + txtUser.Text, "");
            Debug.Print(resp.ToString());
            //resp.SmsUpCode;
            //resp.SmsUpMobile;
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            //Debug.Print(resp.ShowStyle[8].
            Debug.Print(resp.ShowStyle.KeyList[8].Str2);
        }

        private void btnCheckSMS_Click(object sender, EventArgs e)
        {
            BindopMobileForRegResponse resp = core.MobileReg(15, zone + txtUser.Text, txtData.Text);
            Debug.Print(resp.Base.ErrMsg.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
            if (resp.Base.Ret == 0 || resp.Base.Ret == -212)
            {
                tick = resp.Ticket;
            }
            Debug.Print(resp.ToString());
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            NewRegResponse resp = core.NewReg("abc", zone + txtUser.Text, tick);
            Debug.Print(resp.Base.ErrMsg.String);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnMMSUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnMMSPost_Click(object sender, EventArgs e)
        {
            string timeMd5 = Fun.TimeMd5();
            int imgLen = 0;
            MmsnsuploadResponse mlr = core.MMSUploadImg("img\\1.jpg", timeMd5, txtMMSPost.Text, ref imgLen);
            BufferUrlObj buo = mlr.BufferUrl;
            string imgUrl0 = buo.Url;
            string imgUrl150 = mlr.ThumbUrlsList[0].Url;
            timeMd5 = Fun.TimeMd5();
            ImgSX imgsx = Fun.imgsx("img\\1.jpg");//获取图片高宽
            core.MMSPost(timeMd5, txtMMSPost.Text, imgUrl0, imgUrl150, imgLen, imgsx.Height, imgsx.Width);
        }

        private void btnBindQQ_Click(object sender, EventArgs e)
        {
            // -3 q账号错误
            // -5 qq账号有问题，拒绝访问，应该q号被封
            //-123 已结绑定
            // -34 操作频繁
            BindQQResponse resp = core.BindQQ(txtData.Text, txtMsg.Text, "Android设备", "Xiaomi-MI-ONE Plus");

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnBindEMail_Click(object sender, EventArgs e)
        {
            //-85 邮箱已结绑定过
            BindEmailResponse resp = core.BingEmail(txtData.Text);

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnUnbindMobile_Click(object sender, EventArgs e)
        {
            //-124 限制解绑
            BindOpMobileResponse resp = core.BindMobile(txtData.Text, "Android设备", "Xiaomi-MI-ONE Plus");

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnGetOAuth_Click(object sender, EventArgs e)
        {
            Geta8keyResponse resp = core.GetOAuth(txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnExtDevReq_Click(object sender, EventArgs e)
        {
            ExtDeviceLoginConfirmGetResponse resp = core.ExtDevLogin(txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnExtDevConfirm_Click(object sender, EventArgs e)
        {
            ExtDeviceLoginConfirmOKResponse resp = core.ExtDevLoginOK(txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnLogoutWebWX_Click(object sender, EventArgs e)
        {
            LogOutWebWxResponse resp = core.LogoutWeb();
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnSendImg_Click(object sender, EventArgs e)
        {
            UploadMsgImgResponse resp = core.UploadMsgImg("img\\tt.jpg", txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnSendVoice_Click(object sender, EventArgs e)
        {
            UploadvoiceResponse resp = core.SendVoiceMsg(txtData.Text, "img//test.amr");
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnSendCard_Click(object sender, EventArgs e)
        {
            /*
           * pToUserName：要发送的目标wxid
      pRecommendUserName：要推荐的微信ID，可以是wxid_xxx或gh_xxx
      pRecommendNickName：要推荐的微信ID的昵称，UTF-8格式
      pCertInfo：要推荐的微信ID的认证信息，UTF-8格式
      pProvince：要推荐的微信ID的省份，UTF-8格式
      pCity：要推荐的微信ID的城市，UTF-8格式
      bGirl：要推荐的微信ID的性别，是否为女性
      pHeadIconUrl：要推荐的微信ID的图像图标URL
      pSign：要推荐的微信ID的个性签名，UTF-8格式
           */
            string cardMsg;
            if (true)//个人号
            {
                cardMsg = string.Format("<msg username=\"{0}\" nickname=\"{1}\" fullpy=\"{2}\" shortpy=\"{3}\" alias=\"{4}\" imagestatus=\"0\" scene=\"17\" province=\"{5}\" city=\"{6}\" sign=\"{7}\" sex=\"{8}\" certflag=\"\" certinfo=\"\" brandIconUrl=\"\" brandHomeUrl=\"\" brandSubscriptConfigUrl=\"\" brandFlags=\"0\" regionCode=\"\" ></msg>",
                    cardinfo.UserName.String, cardinfo.NickName.String, cardinfo.QuanPin.String, cardinfo.PYInitial.String, cardinfo.Alias, cardinfo.Province, cardinfo.City, cardinfo.Signature, cardinfo.Sex);
            }
            else
            {
                cardMsg = string.Format("<msg username=\"{0}\" nickname=\"{1}\" fullpy=\"\" shortpy=\"\" alias=\"\" imagestatus=\"3\" scene=\"17\" province=\"{2}\" city=\"{3}\" sign=\"{4}\" sex=\"0\" certflag=\"{5}\" certinfo=\"{6}\" brandIconUrl=\"{7}\" brandHomeUrl=\"\" brandSubscriptConfigUrl=\"\" brandFlags=\"0\" regionCode=\"\" ></msg>",
                    cardinfo.UserName.String, cardinfo.NickName.String, cardinfo.Province, cardinfo.City, cardinfo.Signature, cardinfo.VerifyFlag, cardinfo.VerifyInfo, cardinfo.CustomizedInfo.BrandIconURL);
            }
            NewSendMsgResponse resp = core.SendFriendMsg(++ClientMsgID, cardMsg, (uint)(GetCurTime() / 1000), txtData.Text, 42);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnOAuth_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("网页地址不能为空");
                return;
            }
            Geta8keyResponse resp = core.GetOAuth(txtAddress.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnLBS_Click(object sender, EventArgs e)
        {
            string[] items = txtData.Text.Split(',');
            LBSFindResponse resp = core.LBSFind(float.Parse(items[0]), float.Parse(items[1]), 1);
            for (int n = 0; n < resp.ContactCount; n++)
            {
                ContactObject one = resp.ContactListList[n];
                Debug.Print(string.Format("name:{0},sex:{1},distance:{2}", one.NickName, one.Sex, one.Distance));
            }
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnShake_Click(object sender, EventArgs e)
        {
            string[] items = txtData.Text.Split(',');
            ShakereportResponse resp = core.ShakeReprot(float.Parse(items[0]), float.Parse(items[1]));

            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnShakeResult_Click(object sender, EventArgs e)
        {
            ShakegetResponse resp = core.ShakeGet();
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnBottle_Click(object sender, EventArgs e)
        {
            ThrowBottleResponse resp = core.ThrowBottle(txtMsg.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnRemoveFriend_Click(object sender, EventArgs e)
        {
            OplogResponse resp = core.OpLog(4, txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void btnRemoveGH_Click(object sender, EventArgs e)
        {
            OplogResponse resp = core.OpLog(7, txtData.Text);
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*\x01 查找添加
\x10 通讯录
\x1E 二维码
\x12 附近人
\x1D 摇一摇
\x0C q好友
\x11 名片
\x03 群方式*/
            //antiSpamTicket = txtMsg.Text;
            //1---不需要验证   2---需要验证
            VerifyUserResponse resp = core.VerifyUser(txtData.Text, antiSpamTicket, 2, "\x01", "hi");
            Debug.Print(resp.ToString());
            txtTip.Text = resp.ToString().Replace("\n", "\r\n");
        }
    }
}