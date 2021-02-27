using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMPro;
using Newtonsoft.Json;
 
namespace Xcode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NewXcode xcode = new NewXcode();
        private void Form1_Load(object sender, EventArgs e)
        {
            




        }

        private void btn_getLoginQrcode_Click(object sender, EventArgs e)
        {
            Action action = () =>
            {

                var GetLoignQrcode = xcode.GetLoginQRcode();

                //put(JsonConvert.SerializeObject(GetLoignQrcode));
                if (GetLoignQrcode.BaseResponse.Ret == (int)MMPro.MM.RetConst.MM_OK)
                {
                   
                    if (GetLoignQrcode.QRCode.iLen > 0)
                        pB_Qrcode.Image = Image.FromStream(new MemoryStream(GetLoignQrcode.QRCode.Buffer));
                    CheckLogin();
                    //Console.WriteLine(uuid);
                }
                else
                {
                    rtb_Msg.AppendText("获取二维码失败！");
                }



            };

            action.BeginInvoke(new AsyncCallback((IAsyncResult result) =>
            {

                rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText("已确认 等待登陆")));

                // 
            }), null);

            Device_ s = xcode.DeviceS;

        }

        private void CheckLogin() {
           var Check = xcode.CheckLoginQRCode();

            if (Check.HeadImgURL!="") {
               // pB_Qrcode.Load(Check.HeadImgURL);
            }

            if (Check.Status == 2)
            {
                var ManualAuth = xcode.ManualAuth(Check.UserName, Check.Pwd);

                if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
                {
                    //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                    //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                    //int len = (int)ManualAuth.dnsInfo.builtinIplist.shortconnectIpcount;
                    //Util.shortUrl = "http://" + ManualAuth.dnsInfo.newHostList.list[1].substitute;
                    //put(Util.shortUrl);

                    //继续检查状态
                    //chek.Start();
                    CheckLogin();
                }
                else if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
                {
                    put(JsonConvert.SerializeObject(ManualAuth));
                    put(JsonConvert.SerializeObject(xcode.DeviceS));
                }
                else {
                    put(JsonConvert.SerializeObject(ManualAuth));
                }
            }
            else
            {
                put(string.Format("剩余扫码时间：{0} Status：{1}", Check.ExpiredTime, Check.Status));

                System.Threading.Thread.Sleep(500);
                CheckLogin();
            }
        }

        

        public void put(string s)
        {
            rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText(s + "\n")));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Device_ s = JsonConvert.DeserializeObject<Device_>(rtb_Msg.Text);

            xcode = new NewXcode(new Device_() {
                devicelId =s.devicelId,
                AesKey =s.AesKey,
                autoAuthKey_buff =s.autoAuthKey_buff,
                cookie =s.cookie,
                m_Uin =s.m_Uin,
                shortUrl = s.shortUrl,
                DevicelType = s.DevicelType,
                DeviceName =s.DeviceName,
                IMEI =s.IMEI,
                pri_key_buf = s.pri_key_buf,
                pub_key_buf = s.pub_key_buf,
                UUid = s.UUid,
                SyncKey = s.SyncKey
            });

           //var geta8key = newXcode.GetA8Key("weixin", "http://mp.weixin.qq.com/s?__biz=MzA5MDAwOTExMw==&mid=200126214&idx=1&sn=a1e7410ec56de5b6c4810dd7f7db8a47&chksm=1e0b3470297cbd666198666278421aed0a131d775561c08f52db0c82ce0e6a9546aac072a20e&mpshare=1&scene=1&srcid=0408bN3ACxqAH6jyq4vCBP9e#rd");
           // put(JsonConvert.SerializeObject(geta8key));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var geta8key = xcode.GetA8Key("weixin", "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx3827070276e49e30&redirect_uri=https%3A%2F%2Fwx.17u.cn%2Fhome%2Findex.html%3Fifhttps%3Dtrue%26type%3D0%26refid%3D480110332&response_type=code&scope=snsapi_base&state=123#wechat_redirect");
            put(JsonConvert.SerializeObject(geta8key));
        }

        private void pB_Qrcode_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            put(xcode.GenerateWxDat());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var snspost = xcode.SnsPost(rtb_Msg.Text, 1);
            put(JsonConvert.SerializeObject(snspost));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            put(JsonConvert.SerializeObject(xcode.DeviceS));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var ManualAuth = xcode.UserLogin("misty1798168048", "qwertyuiop83", "62706c6973743030d4010203040506090a582476657273696f6e58246f626a65637473592461726368697665725424746f7012000186a0a2070855246e756c6c5f102034306536633038323865316439373933363863643733373832626635623537635f100f4e534b657965644172636869766572d10b0c54726f6f74800108111a232d32373a406375787d0000000000000101000000000000000d0000000000000000000000000000007f");

            if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            {
                //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                //int len = (int)ManualAuth.dnsInfo.builtinIplist.shortconnectIpcount;
                //Util.shortUrl = "http://" + ManualAuth.dnsInfo.newHostList.list[1].substitute;
                //put(Util.shortUrl);

                //继续检查状态
                //chek.Start();
                button6_Click(sender,e);
            }
            else if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                put(JsonConvert.SerializeObject(ManualAuth));
                put(JsonConvert.SerializeObject(xcode.DeviceS));
            }
            else
            {
                put(JsonConvert.SerializeObject(ManualAuth));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //btn_Sync.Enabled = false;

            Action action = () =>
            {

                int ret = 1;
                //var NewInit = new mm.command.NewInitResponse();
                while (ret == 1)
                {
                    var NewSync = xcode.NewSyncEcode();
                    if (NewSync == null) { continue; }
                    if (NewSync.cmdList.count <= 0) { continue; }
                    foreach (var r in NewSync.cmdList.list)
                    {
                        //put("cmdId:"+r.cmdid.ToString());
                        if (r.cmdid == MM.SyncCmdID.CmdIdAddMsg)
                        {
                            micromsg.AddMsg msg = Util.Deserialize<micromsg.AddMsg>(r.cmdBuf.data);
                            put(JsonConvert.SerializeObject(msg));
                        }
                        else if (r.cmdid == MM.SyncCmdID.CmdIdModContact)
                        {
                            micromsg.ModContact contact = Util.Deserialize<micromsg.ModContact>(r.cmdBuf.data);
                            //put(JsonConvert.SerializeObject(contact));
                        }


                    }

                    //Sync = NewSync.sync_key;
                }


            };

            action.BeginInvoke(new AsyncCallback((IAsyncResult result) =>
            {

                rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText("同步信息结束")));

                // 
            }), null);


        }

        private void button8_Click(object sender, EventArgs e)
        {
            var NewVerifyPasswd = xcode.NewVerifyPasswd("qwertyuiop83");
            put(JsonConvert.SerializeObject(NewVerifyPasswd));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var NewVerifyPasswd = xcode.NewSetPasswd("qwertyuiop83a",rtb_Msg.Text);
            put(JsonConvert.SerializeObject(NewVerifyPasswd));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
           var ManualAuth = xcode.UserLogin(tb_Username.Text, tb_passwd.Text, tb_data62.Text);



            if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            {
                //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                //int len = (int)ManualAuth.dnsInfo.builtinIplist.shortconnectIpcount;
                //Util.shortUrl = "http://" + ManualAuth.dnsInfo.newHostList.list[1].substitute;
                //put(Util.shortUrl);

                //继续检查状态
                //chek.Start();
                button6_Click_1(sender, e);
            }
            else if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                put(JsonConvert.SerializeObject(ManualAuth));
                put(JsonConvert.SerializeObject(xcode.DeviceS));
            }
            else
            {
                put(JsonConvert.SerializeObject(ManualAuth));
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            put(JsonConvert.SerializeObject(xcode.AutoAuthRequest()));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Device_ device = new Device_();

            string data = tb_data62.Text;
                
            string[] a16 = data.Split('|');
            //4086207604 tttt8557 Ae0fb5820273737

            device.devicelId = a16[2];//a16 主要这个参数
            device.IMEI = a16[3]; // imei
            device.DeviceName = a16[8]; //随便
            // 建议使用a16 数据对里面的数据进行替换
            //device.DevicelType = string.Format("<softtype><lctmoc>0</lctmoc><level>1</level><k1>ARMv7 processor rev 1 (v7l) </k1><k2></k2><k3>{0}</k3><k4>{1}</k4><k5>460007337766541</k5><k6>89860012221746527381</k6><k7>{2}</k7><k8>unknown</k8><k9>{3}</k9><k10>2</k10><k11>placeholder</k11><k12>0001</k12><k13>0000000000000001</k13><k14>{4}</k14><k15></k15><k16>neon vfp swp half thumb fastmult edsp vfpv3 idiva idivt</k16><k18>e89b158e4bcf922ebd09eb83f5378e11</k18><k21>\"wireless\"</k21><k22></k22><k24>{5}</k24><k26>0</k26><k30>\"wireless\"</k30><k33>com.tencent.mm</k33><k34>Android-x86/android_x86/x86:5.1.1/LMY48Z/denglibo08021647:userdebug/test-keys</k34><k35>vivo v3</k35><k36>unknown</k36><k37></k37><k38>x86</k38><k39>android_x86</k39><k40>taurus</k40><k41>1</k41><k42></k42><k43>null</k43><k44>0</k44><k45></k45><k46></k46><k47>wifi</k47><k48>{6}</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>1080</k57><k58></k58><k59>0</k59></softtype>", a16[10], a16[3], a16[4], a16[8], a16[5], a16[5], a16[10]);

            device.DevicelType = "<softtype><lctmoc>0</lctmoc><level>1</level><k1>ARMv7 processor rev 1 (v7l) </k1><k2></k2><k3>5.1.1</k3><k4>865166024671219</k4><k5>460007337766541</k5><k6>89860012221746527381</k6><k7>d3151233cfbb4fd4</k7><k8>unknown</k8><k9>iPhon 100</k9><k10>2</k10><k11>placeholder</k11><k12>0001</k12><k13>0000000000000001</k13><k14>01:61:19:58:78:d2</k14><k15></k15><k16>neon vfp swp half thumb fastmult edsp vfpv3 idiva idivt</k16><k18>e89b158e4bcf922ebd09eb83f5378e11</k18><k21>\"wireless\"</k21><k22></k22><k24>41:27:91:12:5e:14</k24><k26>0</k26><k30>\"wireless\"</k30><k33>com.tencent.mm</k33><k34>Android-x86/android_x86/x86:5.1.1/LMY48Z/denglibo08021647:userdebug/test-keys</k34><k35>vivo v3</k35><k36>unknown</k36><k37>iPhon</k37><k38>x86</k38><k39>android_x86</k39><k40>taurus</k40><k41>1</k41><k42>100</k42><k43>null</k43><k44>0</k44><k45></k45><k46></k46><k47>wifi</k47><k48>865166024671219</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>1080</k57><k58></k58><k59>0</k59></softtype>";
            var ManualAuth = xcode.Android_Login(a16[0], a16[1], device );



            if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            {
                //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                //int len = (int)ManualAuth.dnsInfo.builtinIplist.shortconnectIpcount;
                //Util.shortUrl = "http://" + ManualAuth.dnsInfo.newHostList.list[1].substitute;
                //put(Util.shortUrl);

                //继续检查状态
                //chek.Start();
                button11_Click(sender, e);
            }
            else if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                put(JsonConvert.SerializeObject(ManualAuth));
                put(JsonConvert.SerializeObject(xcode.DeviceS));
            }
            else
            {
                put(JsonConvert.SerializeObject(ManualAuth));
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var geta8key = xcode.GetA8Key("", "https://weixin.qq.com/g/ARNrXaucHjW_GL87");
            put(JsonConvert.SerializeObject(geta8key));
        }

        private void button13_Click(object sender, EventArgs e)
        {
           var ExtDeviceLoginConfirmGet = xcode.ExtDeviceLoginConfirmGet(textBox1.Text);
            
            put(JsonConvert.SerializeObject(ExtDeviceLoginConfirmGet));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var ExtDeviceLoginConfirmOK = xcode.ExtDeviceLoginConfirmOK(textBox1.Text);

            put(JsonConvert.SerializeObject(ExtDeviceLoginConfirmOK));

        }

        private void button15_Click(object sender, EventArgs e)
        {
            var PushLoginURL = xcode.PushLoginURL();
            put(JsonConvert.SerializeObject(PushLoginURL));
        }
    }
}
