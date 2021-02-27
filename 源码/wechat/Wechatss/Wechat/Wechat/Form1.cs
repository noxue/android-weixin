using CRYPT;
using MMPro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wchat;
using Newtonsoft.Json;
namespace Wechat
{
    public partial class Form1 : Form
    {

        public Xcode wechat = new Xcode();

        public string uuid = "";

        public byte[] Key = new byte[0];

        public string MyWxid = "";

        public byte[] Sync = new byte[0];

        public int currentWxcontactSeq = 0;

        public byte[] authKey = new byte[0];
        public Form1()
        {
            InitializeComponent();


        }

        private void btn_GetQrcode_Click(object sender, EventArgs e)
        {
            Action action = () =>
            {

                var GetLoignQrcode = wechat.GetLoginQRcode();
                
                //put(JsonConvert.SerializeObject(GetLoignQrcode));
                if (GetLoignQrcode.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
                {
                    uuid = GetLoignQrcode.uuid;

                    Key = GetLoignQrcode.AESKey.key;
                    if (GetLoignQrcode.qRCode.len > 0)
                        pB_Qrcode.Image = Image.FromStream(new MemoryStream(GetLoignQrcode.qRCode.src));
                    checkLogin(uuid);
                    Console.WriteLine(uuid);
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
        }

        private void checkLogin(string uuid)
        {
            var CheckLogin = wechat.CheckLoginQRCode(uuid);
            // Console.WriteLine(CheckLogin.baseResponse.ret);
            if (CheckLogin == null) { return; }
            if (CheckLogin.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                var asd = CheckLogin.data.notifyData.buffer.ToString(16, 2);
                var __ = Util.nouncompress_aes(CheckLogin.data.notifyData.buffer, Key);
                var r = Util.Deserialize<MMPro.MM.LoginQRCodeNotify>(__);

                rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText("剩下扫码时间：" + r.EffectiveTime.ToString() + " state : " + r.state + "\n")));
                if (r.headImgUrl != null)
                {
                    //pB_Qrcode.Load(r.headImgUrl);
                }

                if (r.wxid != null && r.wxnewpass != "")
                {
                    //发送登录包
                    checkManualAuth(r.wxnewpass, r.wxid);
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    checkLogin(uuid);

                }

                //System.Threading.Thread.Sleep(1000);
            }
        }

        private void checkManualAuth(string wxnewpass, string wxid)
        {

            var ManualAuth = wechat.ManualAuth(wxnewpass, wxid);
            //-301重定向
            Console.WriteLine(ManualAuth.baseResponse.ret);
            if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            {
                //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                int len = (int)ManualAuth.dnsInfo.builtinIplist.shortconnectIpcount;
                Util.shortUrl = "http://" + ManualAuth.dnsInfo.newHostList.list[1].substitute;
                put(Util.shortUrl);

                //继续检查状态
                //chek.Start();
                checkLogin(uuid);
            }
            else if (ManualAuth.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                MyWxid = ManualAuth.accountInfo.wxid;

                put(ManualAuth.accountInfo.wxid);
                put(ObjToJson2<MM.AccountInfo>(ManualAuth.accountInfo));
                put(ObjToJson2<MM.BaseResponse>(ManualAuth.baseResponse));
                put(JsonConvert.SerializeObject(ManualAuth.authParam));

                byte[] strECServrPubKey = ManualAuth.authParam.ecdh.ecdhkey.key;
                byte[] aesKey = new byte[16];
                Xcode.ComputerECCKeyMD5(strECServrPubKey, 57, wechat.pri_key_buf, 328, aesKey);
                //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                //wechat.CheckEcdh = aesKey.ToString(16, 2);
                wechat.AESKey = AES.AESDecrypt(ManualAuth.authParam.session.key, aesKey).ToString(16, 2);

                wechat.baseRequest = wechat.GetBaseRequest("49aa7db2f4a3ffe0e96218f6b92cde32", wechat.GetAESkey(), (uint)wechat.m_uid, "iPad iPhone OS9.3.3");

                authKey = ManualAuth.authParam.autoAuthKey.buffer;

            }
            else if ((int)ManualAuth.baseResponse.ret == 2)
            {
                MyWxid = ManualAuth.accountInfo.wxid;

                put(ManualAuth.accountInfo.wxid);
                put(ObjToJson2<MM.AccountInfo>(ManualAuth.accountInfo));
                put(ObjToJson2<MM.BaseResponse>(ManualAuth.baseResponse));


                //byte[] strECServrPubKey = ManualAuth.authParam.ecdh.ecdhkey.key;
                //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(713, strECServrPubKey, wechat.pri_key_buf);
                //wechat.CheckEcdh = aesKey.ToString(16, 2);
                //wechat.AESKey = AES.AESDecrypt(ManualAuth.authParam.session.key, aesKey).ToString(16, 2);

                //wechat.baseRequest = wechat.GetBaseRequest("49aa7db2f4a3ffe0e96218f6b92cde32", wechat.GetAESkey(), (uint)wechat.m_uid, "iPad iPhone OS9.3.3");

            }
            else
            {
                put(JsonConvert.SerializeObject(ManualAuth));
            }
        }

        public void put(string s)
        {
            rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText(s + "\n")));

        }

        public string ObjToJson2<T>(T obj)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    byte[] byteArr = ms.ToArray();
                    return Encoding.UTF8.GetString(byteArr);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wechat.GetAESkey();
            //put(MMPro.MM.CGI_TYPE.cgi_type_v)
            Int32 ver = 369493792;
            Console.WriteLine(Util.Get62Key("62706c6973743030d4010203040506090a582476657273696f6e58246f626a65637473592461726368697665725424746f7012000186a0a2070855246e756c6c5f102066366664336361353933353631393664663964313935313032393661333332315f100f4e534b657965644172636869766572d10b0c54726f6f74800108111a232d32373a406375787d0000000000000101000000000000000d0000000000000000000000000000007f"));

            //Debug.Print(MM.SyncCmdID.)
        }

        private void btn_lbsfind_Click(object sender, EventArgs e)
        {
            //获取附近人
            var lbs = wechat.LbsLBSFind(23.1351666766f, 113.2708136740f);

            put(ObjToJson2(lbs));
        }

        private void btn_SnsUserPage_Click(object sender, EventArgs e)
        {
            var SnsUserPage = wechat.SnsUserPage("", "wxid_20nv6h3jc37522");

            put(JsonConvert.SerializeObject(SnsUserPage));
        }

        private void btn_SnsTimeLine_Click(object sender, EventArgs e)
        {
            var SnsTimeLine = wechat.SnsTimeLine();
            put(ObjToJson2(SnsTimeLine));
            put(JsonConvert.SerializeObject(SnsTimeLine));
        }

        private void btn_SnsObjectOp_Click(object sender, EventArgs e)
        {
            var SnsObjectOp = wechat.GetSnsObjectOp(12876091418808422504, MM.SnsObjectOpType.MMSNS_OBJECTOP_CANCEL_LIKE);

            put(JsonConvert.SerializeObject(SnsObjectOp));
        }

        private void btn_GetLabelList_Click(object sender, EventArgs e)
        {
            var getcontactlabel = wechat.GetContactLabelList();

            put(JsonConvert.SerializeObject(getcontactlabel));
        }

        private void btn_SnsPost_Click(object sender, EventArgs e)
        {
            var sendsns = wechat.SnsPost(tb_Content.Text, int.Parse(tb_AtUserlist.Text));
            //put(ObjToJson2(sendsns));
            put(JsonConvert.SerializeObject(sendsns));
        }

        private void btn_CreateChatRoom_Click(object sender, EventArgs e)
        {
            // 第一个必须是群主的wxid也就是本号的wxid
            MM.MemberReq[] reqs = new MM.MemberReq[3];
            reqs[0] = new MM.MemberReq();
            reqs[0].member = new MM.SKBuiltinString();
            reqs[0].member.@string = "wxid_20nv6h3jc37522";

            reqs[1] = new MM.MemberReq();
            reqs[1].member = new MM.SKBuiltinString();
            reqs[1].member.@string = "wxid_8egcw2xj1vkq22";

            reqs[2] = new MM.MemberReq();
            reqs[2].member = new MM.SKBuiltinString();
            reqs[2].member.@string = "wxid_l63xil1re74722";

            var CeratchatRoom = wechat.CreateChatRoom(reqs, "xxxx");
            put(ObjToJson2(CeratchatRoom));
            if (CeratchatRoom.baseResponse.ret == MM.RetConst.MM_OK)
            {
                wechat.SendNewMsg(CeratchatRoom.chatRoomName.@string, "测试哦");
            }
        }

        private void btn_AddChatRoomMember_Click(object sender, EventArgs e)
        {
            string list = "[\"xxxxxxssss\",\"xxxsssxxssss\",\"xxxsssxxssss\"]";
            var AddChatRoom = wechat.AddChatRoomMember(tb_ToUsername.Text, list);

            put(ObjToJson2(AddChatRoom));

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Xcode wc = new Xcode();

            wc.devicelId = "49aa7db2f4a3ffe0e96216f6b92cde22";

            wc.GetAESkey();

            put(JsonConvert.SerializeObject(wc.GetLoginQRcode()));
            
            //var BindOpMobile = wechat.BindMobile("+8617607567005", "", 1);

            //put(JsonConvert.SerializeObject(BindOpMobile));
            //wechat.UploadMsgImg();
            //string list = "[\"xxxxxxssss\",\"xxxsssxxssss\",\"xxxsssxxssss\"]";
            //var json = Util.JsonToObject(list);
            //object[] vs = json;
            //Console.WriteLine(vs.Length);

            // var s =  wechat.NewReg(tb_ToUsername.Text,tb_Content.Text);
            //var s = wechat.PushLoginURL(authKey, MyWxid);
            //var s = wechat.UserLogin("0094726042133", "jp5531836", "");
            //put(JsonConvert.SerializeObject(s));

            //if (s.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            //{
            //    //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
            //    //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
            //    int len = (int)s.dnsInfo.builtinIplist.shortconnectIpcount;
            //    Util.shortUrl = "http://" + s.dnsInfo.newHostList.list[1].substitute;
            //}

            
        }
        private void btn_DelChatRoomUser_Click(object sender, EventArgs e)
        {
            var DelChatRoom = wechat.DelChatRoomMember(tb_ToUsername.Text, "[\"wxid_xhdqvshqkor822\"]");

            put(JsonConvert.SerializeObject(DelChatRoom));
        }

        private void btn_GetRoomDetail_Click(object sender, EventArgs e)
        {
            var GetChatroomMenberDettail = wechat.GetChatroomMemberDetail(tb_ToUsername.Text);
            put(ObjToJson2(GetChatroomMenberDettail));
        }

        private void btn_GetA8Key_Click(object sender, EventArgs e)
        {
            var GetA8Key = wechat.GetA8Key("weixin", "http://mp.weixin.qq.com/s?__biz=MzA5MDAwOTExMw==&mid=200126214&idx=1&sn=a1e7410ec56de5b6c4810dd7f7db8a47&chksm=1e0b3470297cbd666198666278421aed0a131d775561c08f52db0c82ce0e6a9546aac072a20e&mpshare=1&scene=1&srcid=0408bN3ACxqAH6jyq4vCBP9e#rd");
            //JsonFormat.printToString(GetA8Key)
            put(JsonConvert.SerializeObject(GetA8Key));

            put(GetA8Key.FullURL);
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            var GetQRCode = wechat.GetQRCode(tb_ToUsername.Text);

            put(JsonConvert.SerializeObject(GetQRCode));
            if (GetQRCode.QRCode.iLen > 0)
            {

                pB_Qrcode.Image = Image.FromStream(new MemoryStream(GetQRCode.QRCode.Buffer));

            }
            else
            {
                MessageBox.Show("获取二维码失败！");
            }

        }

        private void btn_initcontact_Click(object sender, EventArgs e)
        {
            var InitContact = wechat.InitContact(tb_ToUsername.Text, currentWxcontactSeq);

            currentWxcontactSeq = InitContact.currentWxcontactSeq;
            put(JsonConvert.SerializeObject(InitContact));
        }

        private void btn_SearchContact_Click(object sender, EventArgs e)
        {
            var SearchContact = wechat.SearchContact(tb_ToUsername.Text);

            put(JsonConvert.SerializeObject(SearchContact));


        }

        private void btn_SendMsgimg_Click(object sender, EventArgs e)
        {
            var SendMsgImgRet = wechat.UploadMsgImg(tb_ToUsername.Text, MyWxid, System.Environment.CurrentDirectory + "\\1.jpg");
            put(JsonConvert.SerializeObject(SendMsgImgRet));
        }

        private void btn_SendVoice_Click(object sender, EventArgs e)
        {
            var UploadVoice = wechat.UploadVoice(tb_ToUsername.Text, MyWxid, System.Environment.CurrentDirectory + "\\19.amr");
            put(JsonConvert.SerializeObject(UploadVoice));
        }

        private void btn_Fetchauthen_Click(object sender, EventArgs e)
        {

        }

        private void btn_GetBalance_Click(object sender, EventArgs e)
        {

        }

        private void btn_Genprefetch_Click(object sender, EventArgs e)
        {

        }

        private void btn_GetBlankID_Click(object sender, EventArgs e)
        {

        }

        private void btn_F2ffee_Click(object sender, EventArgs e)
        {
            //string payloadJson = "{\"CgiCmd\":0,\"ReqKey\":\"" + ReqKey + "\",\"PassWord\":\"123456\"}";
            string tenpayUrl = "delay_confirm_flag=0&desc=转账测试&fee=10&fee_type=1&pay_scene=31&receiver_name=" + tb_ToUsername.Text + "&scene=31&transfer_scene=2&WCPaySign=29E00C5AD811B14B177311A2A6B7B8C8062FF1179FD9A83AB6A6223F6F3479DE";
            var Tenpay = wechat.TenPay(MM.enMMTenPayCgiCmd.MMTENPAY_CGICMD_GET_FIXED_AMOUNT_QRCODE, tenpayUrl);
            put(JsonConvert.SerializeObject(Tenpay));
        }

        private void btn_GetBalance_Click_1(object sender, EventArgs e)
        {
            var Tenpay = wechat.TenPay(MM.enMMTenPayCgiCmd.MMTENPAY_CGICMD_BIND_QUERY_NEW);
            put(JsonConvert.SerializeObject(Tenpay));
        }

        private void btn_GenPreTransferReq_Click(object sender, EventArgs e)
        {
            string tenpayUrl = "delay_confirm_flag=0&desc=转账测试&fee=10&fee_type=1&pay_scene=31&receiver_name=" + tb_ToUsername.Text + "&scene=31&transfer_scene=2&WCPaySign=29E00C5AD811B14B177311A2A6B7B8C8062FF1179FD9A83AB6A6223F6F3479DE";
            var Tenpay = wechat.TenPay(MM.enMMTenPayCgiCmd.MMTENPAY_CGICMD_GEN_PRE_TRANSFER, tenpayUrl);
            put(JsonConvert.SerializeObject(Tenpay));
        }

        private void btn_TransferReq_Click(object sender, EventArgs e)
        {
            string tenpayUrl = "auto_deduct_flag=0&bank_type=CFT&bind_serial=CFT&busi_sms_flag=0&flag=3&passwd=81AED920CBF05F4E2CE88836B72A37349F53A1610EB3C608E7947EA100DBDD7851ED97096C62EC0B32893A5583AFE09F25873A267A66ECA503E2985823798E8B0DF62CF8201D8415E361297B355F27EB07DBE846920E26B76251F856BEB68C912116E6F7B504F9C685346449C458A4B13685C3161687F6BDFE9C03E7B270D88D&pay_scene=37&req_key" + tb_Content.Text + "&use_touch=0&WCPaySign=CA9D8CD3446D25EC097CDF1B07017C6A76F465A489299E67FBC29F0B06CC08CA";
            var Tenpay = wechat.TenPay(0, tenpayUrl);
            put(JsonConvert.SerializeObject(Tenpay));
        }

        private void btn_TenPay_TransferConfirm_Click(object sender, EventArgs e)
        {

        }

        private void btn_SayHello_Click(object sender, EventArgs e)
        {
            //打招呼的opcode是2
            var VerifyUser = wechat.VerifyUser(MM.VerifyUserOpCode.MM_VERIFYUSER_SENDREQUEST, tb_Content.Text, tb_ToUsername.Text, tb_AtUserlist.Text);
            put(JsonConvert.SerializeObject(VerifyUser));
        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            //最后一个参数是来源啊
            //1来源QQ2来源邮箱3来源微信号14群聊15手机号18附近的人25漂流瓶29摇一摇30二维码13来源通讯录
            var VerifyUser = wechat.VerifyUser(MM.VerifyUserOpCode.MM_VERIFYUSER_SENDREQUEST, tb_Content.Text, tb_ToUsername.Text, tb_AtUserlist.Text, 0x0e);
            put(JsonConvert.SerializeObject(VerifyUser));
        }

        private void btn_AcceptUser_Click(object sender, EventArgs e)
        {
            //最后一个参数是来源啊
            //1来源QQ2来源邮箱3来源微信号14群聊15手机号18附近的人25漂流瓶29摇一摇30二维码13来源通讯录
            var VerifyUser = wechat.VerifyUser(MM.VerifyUserOpCode.MM_VERIFYUSER_VERIFYOK, tb_Content.Text, tb_ToUsername.Text, tb_AtUserlist.Text);
            put(JsonConvert.SerializeObject(VerifyUser));
        }

        private void btn_Add_Gh_Click(object sender, EventArgs e)
        {
            //最后一个参数是来源啊
            //1来源QQ2来源邮箱3来源微信号14群聊15手机号18附近的人25漂流瓶29摇一摇30二维码13来源通讯录
            //非原始id 或扫码后的id 识别二维码后得到http://weixin.qq.com/r/RjndxWXExji5rSGm92xU 账号为RjndxWXExji5rSGm92xU@qr 直接关注即可
            var VerifyUser = wechat.VerifyUser(MM.VerifyUserOpCode.MM_VERIFYUSER_ADDCONTACT, tb_Content.Text, tb_ToUsername.Text, tb_AtUserlist.Text, 0x03);
            put(JsonConvert.SerializeObject(VerifyUser));
        }

        private void btn_ClickCommand_Click(object sender, EventArgs e)
        {
            var ClicCommand = wechat.ClickCommand("gh_d1113d3eaf68", "<info><id>447061836</id><key>rselfmenu_2_0</key><status>menu_click</status><content></content></info>");
            put(JsonConvert.SerializeObject(ClicCommand));
        }

        private void btn_delUser_Click(object sender, EventArgs e)
        {
            var DelUser = wechat.OpLogDelUser(tb_ToUsername.Text);
            put(JsonConvert.SerializeObject(DelUser));
        }

        private void btn_webSearch_Click(object sender, EventArgs e)
        {

        }

        private void btn_shakeGet_Click(object sender, EventArgs e)
        {
            var ShakeReport = wechat.ShakeReport(22.54733f, 110.94f);

            put(JsonConvert.SerializeObject(ShakeReport));
        }

        private void btn_NewInit_Click(object sender, EventArgs e)
        {
            Action action = () =>
            {
                int ret = 1;
                //var NewInit = new mm.command.NewInitResponse();
                var NewInit = wechat.NewInit(MyWxid); ;

                //put(JsonConvert.SerializeObject(NewInit));

                while (ret == 1)
                {
                    NewInit = wechat.NewInit(MyWxid);
                    ret = NewInit.ContinueFlag;

                    Console.WriteLine(NewInit.Base.Ret);
                    foreach (var r in NewInit.CmdListList)
                    {
                        Console.WriteLine("CmdId : {0}", r.CmdId);
                        put("CmdId : " + r.CmdId);
                        if (r.CmdId == 2)
                        {
                            MM.ModContact accountInfo = Util.Deserialize<MM.ModContact>(r.CmdBuf.Buffer.ToByteArray());
                            //mm.command.Msg ss = mm.command.Msg.ParseFrom(r.CmdBuf.Buffer.ToByteArray());
                            //put(JsonConvert.SerializeObject(accountInfo));
                            put(accountInfo.userName.@string);
                            put(accountInfo.alias);
                            put("----------------------------");


                        }

                        //Console.WriteLine();
                    }
                    //

                }

                Sync = NewInit.CurrentSynckey.ToByteArray();

            };

            action.BeginInvoke(new AsyncCallback((IAsyncResult result) =>
            {

                rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText("初始化结束")));

                // 
            }), null);




        }

        private void btn_SnsSync_Click(object sender, EventArgs e)
        {
            var snssyncp = wechat.SnsSync("49aa7db2f4a3ffe0e96218f6b92cde32", "iPad iPhone OS9.3.3");

            foreach (var s in snssyncp.cmdList.list)
            {

                MM.SnsObject chat = Util.Deserialize<MM.SnsObject>(s.cmdBuf.data);
                put(JsonConvert.SerializeObject(chat));
            }
        }

        private void btn_GetContact_Click(object sender, EventArgs e)
        {
            var getcotact = wechat.GetContact_b(tb_ToUsername.Text, tb_AtUserlist.Text);
            put(JsonConvert.SerializeObject(getcotact));
        }

        private void btn_BindMobile_Click(object sender, EventArgs e)
        {

        }

        private void btn_FavSync_Click(object sender, EventArgs e)
        {
            var FavSyns = wechat.FavSync();
            foreach (var s in FavSyns.CmdList.List)
            {
                Debug.Print(s.CmdBuf.Buffer.ToString(16, 2));
                micromsg.AddFavItem shareFav = Util.Deserialize<micromsg.AddFavItem>(s.CmdBuf.Buffer);
                put(JsonConvert.SerializeObject(shareFav));
            }


            //FavSyns = wechat.FavSync(FavSyns.KeyBuf.Buffer);
        }

        private void btn_GetFavItem_Click(object sender, EventArgs e)
        {
            var GetFav = wechat.GetFavItem(Convert.ToInt32(tb_Content.Text));
            foreach (var obj in GetFav.ObjectList)
            {
                put(JsonConvert.SerializeObject(obj));
            }
        }

        private void btn_DelFavItem_Click(object sender, EventArgs e)
        {
            uint[] id = new uint[] { 1, 2, 3 };//欲删除id
            var DelFav = wechat.DelFavItem(id);

            put(JsonConvert.SerializeObject(DelFav));
        }

        private void btn_AddFavItem_Click(object sender, EventArgs e)
        {
            var addFavItem = wechat.addFavItem(tb_Content.Text);
            put(JsonConvert.SerializeObject(addFavItem));
        }

        private void btn_Sync_Click(object sender, EventArgs e)
        {
            btn_Sync.Enabled = false;

            Action action = () =>
            {

                int ret = 1;
                //var NewInit = new mm.command.NewInitResponse();
                while (ret == 1)
                {
                    var NewSync = wechat.NewSyncEcode(4, Sync);
                    if (NewSync.cmdList == null) { continue; }
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

                    Sync = NewSync.sync_key;
                }


            };

            action.BeginInvoke(new AsyncCallback((IAsyncResult result) =>
            {

                rtb_Msg.BeginInvoke(new Action(() => rtb_Msg.AppendText("同步信息结束")));

                // 
            }), null);

        }

        private void btn_SendMsg_Click(object sender, EventArgs e)
        {
            var sendmsg = wechat.SendNewMsg(tb_ToUsername.Text, tb_Content.Text);
            put(JsonConvert.SerializeObject(sendmsg));
        }

        private void btn_AutoAuth_Click(object sender, EventArgs e)
        {
            var AutoAuthRequest =  wechat.AutoAuthRequest(authKey);
            //put(JsonConvert.SerializeObject(AutoAuthRequest));
            if (AutoAuthRequest.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            {
                //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                int len = (int)AutoAuthRequest.dnsInfo.builtinIplist.shortconnectIpcount;
                Util.shortUrl = "http://" + AutoAuthRequest.dnsInfo.newHostList.list[1].substitute;
                Btn_DataLogin_Click(Btn_DataLogin, new EventArgs());
            }
            else if (AutoAuthRequest.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                MyWxid = AutoAuthRequest.accountInfo.wxid;

                put(AutoAuthRequest.accountInfo.wxid);
                put(ObjToJson2<MM.AccountInfo>(AutoAuthRequest.accountInfo));
                put(ObjToJson2<MM.BaseResponse>(AutoAuthRequest.baseResponse));


                byte[] strECServrPubKey = AutoAuthRequest.authParam.ecdh.ecdhkey.key;
                byte[] aesKey = new byte[16];
                Xcode.ComputerECCKeyMD5(strECServrPubKey, 57, wechat.pri_key_buf, 328, aesKey);
                //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                //wechat.CheckEcdh = aesKey.ToString(16, 2);
                wechat.AESKey = AES.AESDecrypt(AutoAuthRequest.authParam.session.key, aesKey).ToString(16, 2);

                wechat.baseRequest = wechat.GetBaseRequest(wechat.devicelId, wechat.AESKey.ToByteArray(16, 2), (uint)wechat.m_uid, "iPad iPhone OS9.3.3");

                //authKey = AutoAuthRequest.authParam.autoAuthKey.buffer;
                put(JsonConvert.SerializeObject(AutoAuthRequest.authParam));
            }
            else {
                put(JsonConvert.SerializeObject(AutoAuthRequest));
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            var logout = wechat.logOut();
            put(JsonConvert.SerializeObject(logout));
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddLabel_Click(object sender, EventArgs e)
        {
            var AddLabel = wechat.AddContactLabel(tb_Content.Text);
            put(JsonConvert.SerializeObject(AddLabel));
        }

        private void btn_ModifyLabelList_Click(object sender, EventArgs e)
        {
            micromsg.UserLabelInfo[] userLabels = new micromsg.UserLabelInfo[1];
            userLabels[0] = new micromsg.UserLabelInfo();
            userLabels[0].LabelIDList = tb_Content.Text;
            userLabels[0].UserName = "wxid_xhdqvshqkor822";

            var Modify = wechat.ModifyContactLabelList(userLabels);

            put(JsonConvert.SerializeObject(Modify));
        }

        private void btn_DelContactLabel_Click(object sender, EventArgs e)
        {
            var DelLabel = wechat.DelContactLabel(tb_Content.Text);
            put(JsonConvert.SerializeObject(DelLabel));
        }

        private void btn_SnsUpload_Click(object sender, EventArgs e)
        {
            var SnsUpload = wechat.SnsUpload(System.Environment.CurrentDirectory + "\\1.jpg");
            put(JsonConvert.SerializeObject(SnsUpload));
        }

        private void btn_SendCardMsg_Click(object sender, EventArgs e)
        {
            // 这里可以发送名片
            var sendmsg = wechat.SendNewMsg(tb_ToUsername.Text, tb_Content.Text, 42);
            put(JsonConvert.SerializeObject(sendmsg));
        }

        private void btn_SendAppMsg_Click(object sender, EventArgs e)
        {
            //<appmsg appid="wx873a91b8917c375b" sdkver="0"><title>歌曲名字</title><des>歌手名字</des><type>3</type><showtype>0</showtype><soundtype>0</soundtype><contentattr>0</contentattr><url>http://i.y.qq.com/v8/playsong.html?songid=歌曲ID</url><lowurl>http://i.y.qq.com/v8/playsong.html?songid=歌曲ID</lowurl><dataurl>音乐地址</dataurl><lowdataurl>音乐地址</lowdataurl> <thumburl>歌曲图片</thumburl></appmsg>

            var sendappmsg = wechat.SendAppMsg(tb_Content.Text, tb_ToUsername.Text, MyWxid);
            put(JsonConvert.SerializeObject(sendappmsg));
        }

        private void btn_SetChatRoomAnnouncement_Click(object sender, EventArgs e)
        {
            var setChatRoomAnnouncement = wechat.setChatRoomAnnouncement(tb_ToUsername.Text, tb_Content.Text);
            put(JsonConvert.SerializeObject(setChatRoomAnnouncement));
        }

        private void btn_downloadMsgimg_Click(object sender, EventArgs e)
        {
            byte[] pic = wechat.GetMsgImg("", 149058, 1684679638, "wxid_20nv6h3jc37522", "wxid_20nv6h3jc37522", 0);
            pB_Qrcode.Image = Image.FromStream(new MemoryStream(pic));
            //保存图片
            using (FileStream fs = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\1684679638" + ".jpg", FileMode.Create))
            {
                fs.Write(pic, 0, pic.Length);
                fs.Close();

            }
        }

        private void btn_BindEail_Click(object sender, EventArgs e)
        {
            var bindemail = wechat.BindEmail("1798168048@qq.com");
            put(JsonConvert.SerializeObject(bindemail));
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var BinOpMobile = wechat.MobileLogin("+8617607567005", tb_Content.Text, 17);
            put(JsonConvert.SerializeObject(BinOpMobile));
        }

        private void btn_loginBySms_Click(object sender, EventArgs e)
        {
            checkManualAuth(tb_ToUsername.Text, tb_AtUserlist.Text);
        }

        private void btn_GetLoginSmsCodes_Click(object sender, EventArgs e)
        {
            var BindOpMibile = wechat.MobileLogin("+"+tb_Content.Text, tb_ToUsername.Text);
            put(JsonConvert.SerializeObject(BindOpMibile));
        }

        private void btn_GetSms_Click(object sender, EventArgs e)
        {
            var BindOpMibile = wechat.BindOpMobileRegFor("+"+tb_Content.Text, tb_ToUsername.Text, 12);
            put(JsonConvert.SerializeObject(BindOpMibile));

        }

        private void btn_ZC_Click(object sender, EventArgs e)
        {
            var BindOpMibile = wechat.BindOpMobileRegFor("+" + tb_Content.Text, tb_ToUsername.Text, 14, tb_Content.Text);
            put(JsonConvert.SerializeObject(BindOpMibile));
        }

        private void btn_ChackSmsCode_Click(object sender, EventArgs e)
        {
            var BindOpMibile = wechat.BindOpMobileRegFor("+" + tb_Content.Text, tb_ToUsername.Text, 15, "",tb_AtUserlist.Text);
            put(JsonConvert.SerializeObject(BindOpMibile));
        }

        private void btn_PushUrlLogin_Click(object sender, EventArgs e)
        {

        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }

        private void btn_logoutweb_Click(object sender, EventArgs e)
        {

        }

        private void btn_NewReg_Click(object sender, EventArgs e)
        {
            var newreg2 = wechat.NewReg(tb_ToUsername.Text, tb_AtUserlist.Text,"+"+ tb_Content.Text);
            put(JsonConvert.SerializeObject(newreg2));
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void btn_SendCDnimg_Click(object sender, EventArgs e)
        {

            var UploadMsgImgCDN = wechat.UploadMsgImgCDN("9e5899241a1d4267ae6a7bb8bcd2deb1", 52005, 4359, "304e020100044730450201000204fc0b7e1802032f59190204929a697102045b5b9576042036363362626332303436353534643733393233663934623831383231353631640204010800010201000400", tb_ToUsername.Text, MyWxid);
            put(JsonConvert.SerializeObject(UploadMsgImgCDN));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var UploadVoice = wechat.UploadVideo(System.Environment.CurrentDirectory + "\\19.amr", MyWxid, tb_ToUsername.Text, 0);
            put(JsonConvert.SerializeObject(UploadVoice));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var GetA8Key = wechat.GetA8Key("", "https://weixin.qq.com/g/A-pc6muNJDi4KiPK");
            //JsonFormat.printToString(GetA8Key)
            put(JsonConvert.SerializeObject(GetA8Key));
        }

        private void Btn_DataLogin_Click(object sender, EventArgs e)
        {
            var UserLoign = wechat.UserLogin(tb_ToUsername.Text, tb_AtUserlist.Text, tb_Content.Text);


            if (UserLoign.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
            {
                //Console.WriteLine(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist[0].ip);
                //byte[] s = Util.Serialize<MM.BuiltinIP>(ManualAuth.dnsInfo.builtinIplist.shortConnectIplist.shortConnectIplist[1]);
                int len = (int)UserLoign.dnsInfo.builtinIplist.shortconnectIpcount;
                Util.shortUrl = "http://" + UserLoign.dnsInfo.newHostList.list[1].substitute;
                Btn_DataLogin_Click(Btn_DataLogin, new EventArgs());
            }
            else if (UserLoign.baseResponse.ret == MMPro.MM.RetConst.MM_OK)
            {
                MyWxid = UserLoign.accountInfo.wxid;

                put(UserLoign.accountInfo.wxid);
                put(ObjToJson2<MM.AccountInfo>(UserLoign.accountInfo));
                put(ObjToJson2<MM.BaseResponse>(UserLoign.baseResponse));


                byte[] strECServrPubKey = UserLoign.authParam.ecdh.ecdhkey.key;
                byte[] aesKey = new byte[16];
                Xcode.ComputerECCKeyMD5(strECServrPubKey, 57, wechat.pri_key_buf, 328, aesKey);
                //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                //wechat.CheckEcdh = aesKey.ToString(16, 2);
                wechat.AESKey = AES.AESDecrypt(UserLoign.authParam.session.key, aesKey).ToString(16, 2);

                //wechat.baseRequest = wechat.GetBaseRequest(wechat.devicelId, wechat.AESKey.ToByteArray(16, 2), (uint)wechat.m_uid, "iMac iPhone OS9.3.3");

                authKey = UserLoign.authParam.autoAuthKey.buffer;
                put(JsonConvert.SerializeObject(UserLoign.authParam));
            }
            else {
                put(JsonConvert.SerializeObject(UserLoign.baseResponse));
            }
        }

        private void btn_verifyPwd_Click(object sender, EventArgs e)
        {
            var VerifyPasswd = wechat.NewVerifyPasswd(tb_AtUserlist.Text);
            put(JsonConvert.SerializeObject(VerifyPasswd)); 
        }

        private void btn_setPwd_Click(object sender, EventArgs e)
        {
           var newsetpasswd = wechat.NewSetPasswd(tb_AtUserlist.Text, tb_ToUsername.Text, authKey);
            put(JsonConvert.SerializeObject(newsetpasswd));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var BindOpMibile = wechat.BindOpMobileRegFor("+97693643876", tb_ToUsername.Text, 17, tb_Content.Text);
            put(JsonConvert.SerializeObject(BindOpMibile));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var GetContact = wechat.GetContact(tb_ToUsername.Text, 1);

            put(JsonConvert.SerializeObject(GetContact));
        }

        private void btn_deviceLogins_Click(object sender, EventArgs e)
        {

        }

        private void btn_BindMobile_getcode_Click(object sender, EventArgs e)
        {
            var BindOpMobile = wechat.BindMobile("+8617607567005", "", 1);

            put(JsonConvert.SerializeObject(BindOpMobile));
        }

        private void btn_Bindmolie_yzcode_Click(object sender, EventArgs e)
        {
            var BindOpMobile = wechat.BindMobile("+8617607567005", tb_Content.Text, 2);

            put(JsonConvert.SerializeObject(BindOpMobile));

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var BindOpMobile = wechat.BindMobile("+8617607567005", tb_Content.Text, 3);

            put(JsonConvert.SerializeObject(BindOpMobile));
        }

        private void btn_UpMobile_Click(object sender, EventArgs e)
        {
            micromsg.Mobile[] mobiles = new micromsg.Mobile[2];

            mobiles[0] = new micromsg.Mobile() { v = "15816648281" };
            mobiles[1] = new micromsg.Mobile() { v = "13692062050" };
            var UpMobile = wechat.UploadMContact("0094729345849", mobiles, MyWxid);
            put(JsonConvert.SerializeObject(UpMobile));
        }

        private void groupBox20_Enter(object sender, EventArgs e)
        {

        }

        private void btn_SyncCheck_Click(object sender, EventArgs e)
        {

        }
    }
}
