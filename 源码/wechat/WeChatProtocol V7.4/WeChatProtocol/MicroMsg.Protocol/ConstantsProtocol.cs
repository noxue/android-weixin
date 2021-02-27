namespace MicroMsg.Protocol
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class ConstantsProtocol
    {
        public const short AES_DECRYPT_WITH_PRIVATEKEY = 5;
        public const short DES_DECRYPT_WITH_PRIVATEKEY = 4;
        public const short DES_ENCRYPT_WITH_PUBLICKEY = 3;

        public static readonly string DEVICE_TYPE_CONST = string.Concat(new object[] { "wp7", "74E64BCD974503", "_", Environment.OSVersion.Version });
        public static string LBS_INFO = "wp7 RM-943_apac_prc_201_8.10.12393.0";
        //public static string DEVICE_TYPE = "wp7 RM-943_apac_prc_201_8.10.12393.0";//(DEVICE_TYPE_CONST + LBS_INFO);
        public static string DEVICE_TYPE = "iPad iPhone OS8.8";
        //public static string DEVICE_TYPE = "Windows 10";
        //public static string DEVICE_TYPE = "iPhone iPhone OS7.1.2";
        //public static string DEVICE_TYPE = "iMac MacBookPro9,1 OSX OSX 10.11.2 build(15C50)";
        public static string JMP_URL = "";
        public static string HB_CONTACT = "";
        public static string CurrentDirectory = Directory.GetCurrentDirectory();
        public static string ChatRoomPath = "ChatRoomData";
        private static int nowHour = DateTime.Now.Hour;

        public static void onTimerDispatcher(object sender, EventArgs e)

        {
            if (DateTime.Now.Hour - nowHour == 1 && AccountMgr.curUserName !="")
            {
                nowHour = DateTime.Now.Hour;
                using (FileStream fsRead = new FileStream(ConstantsProtocol.CurrentDirectory + "\\Config\\Time\\" + nowHour.ToString() + "_00.silk", FileMode.Open))
                {
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);

                    DirectoryInfo TheFolder = new DirectoryInfo(ConstantsProtocol.CurrentDirectory + "\\User\\" + AccountMgr.curUserName + "\\" + ConstantsProtocol.ChatRoomPath);

                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                        XDocument xdocument = XDocument.Load(NextFile.FullName);
                        XElement xmlMemberCount = xdocument.Root.Element("ChatRoomMemberCount");
                        int MemberCount = int.Parse(xmlMemberCount.Value);
                        if (MemberCount >= 50)
                        {
                            ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(NextFile.Name.Replace(".xml", ""), 60, heByte, 4);
                        }
                        Log.i("TimeService", " begin send, toUserName = " + NextFile.Name);
                    }

                }

                Console.WriteLine("触发事件了");
            }
        }
        public static uint CLIENT_MIN_VERSION
        {
            get
            {
                //return 0x64000031;//VersionHelper.getProtocolVersion();
                return 1644430665;//wp 1711278097 
                //return 369298705;//ipad  1644430665
            }
        }

        public static uint CLIENT_MAX_VERSION
        {
            get
            {
                //return 0x64000031;//VersionHelper.getProtocolVersion();
                return 369428512;//wp 1711278097
                //return 369298705;//ipad
            }
        }
    }
}

