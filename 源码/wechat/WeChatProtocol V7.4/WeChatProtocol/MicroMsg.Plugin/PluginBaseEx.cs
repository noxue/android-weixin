namespace MicroMsg.Plugin
{
    using MicroMsg.Common.Utils;
 
    using MicroMsg.Protocol;
 
    using MicroMsg.Storage;
 
 
 

    public class PluginBaseEx : PluginBase
    {
        public const long PluginID = 0x8005L;
        public const string PluginKey = "PluginBaseEx";
        public const string PluginName = "PluginBaseEx";
        public const int PluginVersion = 0x100;
        //public LbsService sceneLbsService;
        private const string TAG = "PluginBaseEx";

        public override object execute(int entryType, object args)
        {
            return null;
        }


 

        public override bool onInitialize()
        {
            Log.i("PluginBaseEx", "initialize. ");
            //this.sceneLbsService = new LbsService();
            return true;
        }

        public override bool onInstalled(InstallMode mode)
        {
            Log.i("PluginBaseEx", "on inistalled. ");
            return true;
        }

        public override bool onParseChatMsg(ChatMsg chatMsg)
        {
            return false;
        }

        public override void onRegisterResult(RetConst ret, int code)
        {
            Log.i("PluginBaseEx", string.Concat(new object[] { "on register result , ret = ", ret, ", code = ", code }));
        }

        public override bool onUninstalled(InstallMode mode)
        {
            Log.i("PluginBaseEx", "on uninstalled. ");
           // this.sceneLbsService.doCleanPosition();
            return true;
        }

        public override string mAuthor
        {
            get
            {
                return null;
            }
        }

        public override string mDescription
        {
            get
            {
                return null;
            }
        }

        //public override BitmapSource mIcon
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        //public override BitmapSource mIconHD
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        //public override BitmapSource mIconInList
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        public override string mTitle
        {
            get
            {
                return null;
            }
        }
    }
}

