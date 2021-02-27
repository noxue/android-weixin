namespace MicroMsg.Plugin
{
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
 
 


    public abstract class PluginBase
    {
        public bool mDefaultInstalled = true;
        public bool mEnabled = true;
        //protected StandardUITaskDlg mFFEntryCallBack;
        public long mID;
        public int mIndexInGroup;
        public int mIndexInPosition;
        public MainTabIndex mIndexInTab;
        public string mKey;
        public PluginMetaInfo mMetaInfo = new PluginMetaInfo();
        public string mName;
        public EPluginFlag mProtocolPluginFlag;
        public string mUserName;
        public int mVersion;
        private const string TAG = "PluginBase";

        protected PluginBase()
        {
        }

        public virtual void ClearFFEntryCallBack()
        {
            //this.mFFEntryCallBack = null;
        }

        public abstract object execute(int entryType, object args);
        //public abstract PluginSettingBaseCtrl GetPluginSettingCtrl();
        public bool isInstalledPlugin()
        {
            return (this.mMetaInfo.isRegistered && this.mMetaInfo.isInstalled);
        }

        public static bool isInstalledPlugin(PluginBase plugin)
        {
            if (plugin == null)
            {
                return false;
            }
            return plugin.isInstalledPlugin();
        }

        public bool isInTab(MainTabIndex index)
        {
            return ((this.mIndexInTab & index) != ((MainTabIndex) 0));
        }

        public virtual bool isShowFlagImage()
        {
            return false;
        }

        public bool isValidPlugin()
        {
            if (!this.mMetaInfo.isRegistered)
            {
                return false;
            }
            return true;
        }

        public static bool isValidPlugin(PluginBase plugin)
        {
            if (plugin == null)
            {
                return false;
            }
            return plugin.isValidPlugin();
        }

        public virtual void onAppActivated()
        {
        }

        public virtual void onAppDeactivated()
        {
        }

        public virtual bool onInitialize()
        {
            return true;
        }

        public virtual bool onInstalled(InstallMode mode)
        {
            return true;
        }

        public virtual bool onParseChatMsg(ChatMsg chatMsg)
        {
            return false;
        }

        public virtual bool onParseChatTalker(string talker)
        {
            return false;
        }

        public virtual void onRegisterResult(RetConst ret, int code)
        {
        }

        public virtual void OnShowInFFEntry()
        {
        }

        public virtual bool onUnInitialize()
        {
            //this.mFFEntryCallBack = null;
            return true;
        }

        public virtual bool onUninstalled(InstallMode mode)
        {
            //this.mFFEntryCallBack = null;
            return true;
        }

        public virtual void onUnRegisterResult(RetConst ret, int code)
        {
        }



        public abstract string mAuthor { get; }

        public abstract string mDescription { get; }

       // public virtual string mFFEntryTitle { get; set; }

       // public abstract BitmapSource mIcon { get; }

       // public BitmapSource mIconFlagInFFEntry { get; set; }

      //  public abstract BitmapSource mIconHD { get; }

      //  public BitmapSource mIconInFFEntry { get; set; }

       // public abstract BitmapSource mIconInList { get; }

        public abstract string mTitle { get; }
    }
}

