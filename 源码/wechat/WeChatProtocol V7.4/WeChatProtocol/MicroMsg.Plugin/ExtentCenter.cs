namespace MicroMsg.Plugin
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Plugin.Sns.Scene;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class ExtentCenter
    {
        private const string TAG = "ExtentCenter";

        public static T AsObject<T>(object srcObject)
        {
            if (srcObject == null)
            {
                return default(T);
            }
            T dstObject = Activator.CreateInstance<T>();
            if (!AsObject(srcObject, dstObject))
            {
                return default(T);
            }
            return dstObject;
        }

        public static bool AsObject(object srcObject, object dstObject)
        {
            if ((srcObject == null) || (dstObject == null))
            {
                return false;
            }
            Type type = srcObject.GetType();
            if (type == null)
            {
                return false;
            }
            foreach (MemberInfo info3 in dstObject.GetType().GetMembers())
            {
                if (info3.MemberType == MemberTypes.Field)
                {
                    FieldInfo info = info3 as FieldInfo;
                    if (info != null)
                    {
                        FieldInfo field = type.GetField(info.Name);
                        if (field != null)
                        {
                            object obj2 = field.GetValue(srcObject);
                            info.SetValue(dstObject, obj2);
                        }
                    }
                }
            }
            return true;
        }

        public static object executePlugin(PluginBase plugin, int entryType, object args)
        {
            if (!PluginBase.isInstalledPlugin(plugin))
            {
                return null;
            }
            Log.i("ExtentCenter", string.Concat(new object[] { "execute plugin... ", plugin.mName, ", entry type = ", entryType }));
            return plugin.execute(entryType, args);
        }

        public static PluginBase findPluginByID(long id)
        {
            return PluginManager.findPluginByID(id);
        }

        public static PluginBase findPluginByName(string name)
        {
            return PluginManager.findPluginByName(name);
        }

        public static PluginBase findPluginByUserName(string name)
        {
            return PluginManager.findPluginByUserName(name);
        }

        public static List<PluginBase> getAllList()
        {
            return PluginManager.getAllList();
        }

        public static List<PluginBase> getAllListInTab(MainTabIndex index)
        {
            return PluginManager.getAllListInTab(index);
        }

        public static List<PluginBase> getInstalledList()
        {
            return PluginManager.getInstalledList();
        }

        public static List<PluginBase> getListInTab(MainTabIndex index)
        {
            return PluginManager.getListInTab(index);
        }

        public static PluginBase getPluginByChatMsg(ChatMsg chatMsg)
        {
            if (chatMsg == null)
            {
                return null;
            }
            PluginBase ret = null;
            PluginManager.searchOnePlugin(delegate (PluginBase plug) {
                if (plug.onParseChatMsg(chatMsg))
                {
                    ret = plug;
                    return true;
                }
                return false;
            });
            return ret;
        }

        public static PluginBase getPluginByChatTalker(string talker)
        {
            if (string.IsNullOrEmpty(talker))
            {
                return null;
            }
            PluginBase ret = null;
            PluginManager.searchOnePlugin(delegate (PluginBase plug) {
                if (plug.onParseChatTalker(talker))
                {
                    ret = plug;
                    return true;
                }
                return false;
            });
            return ret;
        }

        public static string getPluginNameByChatMsg(ChatMsg chatMsg)
        {
            PluginBase base2 = getPluginByChatMsg(chatMsg);
            if (base2 == null)
            {
                return null;
            }
            return base2.mName;
        }

        public static string getPluginNameByChatTalker(string talker)
        {
            PluginBase base2 = getPluginByChatTalker(talker);
            if (base2 == null)
            {
                return null;
            }
            return base2.mName;
        }

        public static List<PluginBase> getUninstalledList()
        {
            return PluginManager.getUninstalledList();
        }

        public static void initialize()
        {
            EventCenter.registerEventHandler(EventConst.ON_ACCOUNT_LOGIN, new EventHandlerDelegate(ExtentCenter.onAccountLoginProc));
            //≈Û”—»¶
           // NetSceneSnsSync.Init();
        }

        public static bool initializePlugin(PluginBase plugin)
        {
            if (!PluginBase.isInstalledPlugin(plugin))
            {
                return false;
            }
            Log.i("ExtentCenter", "initialize plugin... " + plugin.mName);
            plugin.onInitialize();
            return true;
        }

        public static bool installPlugin(PluginBase plugin, InstallMode mode = 0)
        {
            if (!PluginBase.isValidPlugin(plugin))
            {
                return false;
            }
            if (PluginBase.isInstalledPlugin(plugin))
            {
                return false;
            }
            Log.i("ExtentCenter", string.Concat(new object[] { "install plugin... ", plugin.mName, ", install mode =", mode }));
            plugin.onInstalled(mode);
            plugin.mMetaInfo.isInstalled = true;
            PluginMetaStorage.updateMetaInfo(plugin);
            if ((mode == InstallMode.UserInstall) && (plugin.mProtocolPluginFlag != ((EPluginFlag) 0)))
            {
                Account acc = AccountMgr.getCurAccount();
                acc.nPluginFlag = (uint) (((EPluginFlag) acc.nPluginFlag) & ~plugin.mProtocolPluginFlag);
                AccountMgr.updateAccount();
                //OpLogMgr.opModUserInfo(0x800, acc);
                ServiceCenter.sceneNewSync.doScene(7, syncScene.MM_NEWSYNC_SCENE_OTHER);
            }
            initializePlugin(plugin);
            return true;
        }

        private static void onAccountLoginProc(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            PluginMetaStorage.loadMetaFromXML();
            PluginLoader.scanAvailablePluginList();
        }

        public static void onAppActivated()
        {
            PluginManager.searchAllPlugin(delegate (PluginBase plug) {
                plug.onAppActivated();
                return true;
            });
        }

        public static void onAppDeactivated()
        {
            PluginManager.searchAllPlugin(delegate (PluginBase plug) {
                plug.onAppDeactivated();
                return true;
            });
        }

        public static void onPluginFlagChanged(uint newFlag, uint oldFlag)
        {
            PluginManager.searchAllPlugin(delegate (PluginBase plug) {
                syncPluginInstall(plug, newFlag);
                return true;
            });
        }

        public static bool registerPlugin(PluginBase plugin)
        {
            if (plugin == null)
            {
                return false;
            }
            if (!PluginBase.isValidPlugin(plugin))
            {
                Log.i("ExtentCenter", "register plugin... " + plugin.mName);
                plugin.onRegisterResult(RetConst.MM_OK, 0);
                plugin.mMetaInfo.isRegistered = true;
                PluginMetaStorage.updateMetaInfo(plugin);
            }
            return true;
        }

        public static void syncPluginInstall(PluginBase plug, uint newFlag)
        {
            if (PluginBase.isValidPlugin(plug) && (plug.mProtocolPluginFlag != ((EPluginFlag) 0)))
            {
                uint num = (uint) (((EPluginFlag) newFlag) & plug.mProtocolPluginFlag);
                if ((num != 0) && plug.isInstalledPlugin())
                {
                    uninstallPlugin(plug, InstallMode.SyncInstall);
                }
                else if ((num == 0) && !plug.isInstalledPlugin())
                {
                    installPlugin(plug, InstallMode.SyncInstall);
                }
            }
        }

        public static void uninitialize()
        {
            PluginManager.searchAllPlugin(delegate (PluginBase plug) {
                plug.onUnInitialize();
                return true;
            });
        }

        public static bool uninstallPlugin(PluginBase plugin, InstallMode mode = 0)
        {
            if (!PluginBase.isValidPlugin(plugin))
            {
                return false;
            }
            if (!PluginBase.isInstalledPlugin(plugin))
            {
                return false;
            }
            Log.i("ExtentCenter", string.Concat(new object[] { "uninstall plugin... ", plugin.mName, ", uninstall mode =", mode }));
            plugin.onUninstalled(mode);
            plugin.mMetaInfo.isInstalled = false;
            PluginMetaStorage.updateMetaInfo(plugin);
            if ((mode == InstallMode.UserInstall) && (plugin.mProtocolPluginFlag != ((EPluginFlag) 0)))
            {
                Account acc = AccountMgr.getCurAccount();
                acc.nPluginFlag = (uint) (((EPluginFlag) acc.nPluginFlag) | plugin.mProtocolPluginFlag);
                AccountMgr.updateAccount();
               // OpLogMgr.opModUserInfo(0x800, acc);
                ServiceCenter.sceneNewSync.doScene(7, syncScene.MM_NEWSYNC_SCENE_OTHER);
            }
            return true;
        }

        public static bool unregisterPlugin(PluginBase plugin)
        {
            if (plugin == null)
            {
                return false;
            }
            if (PluginBase.isValidPlugin(plugin))
            {
                Log.i("ExtentCenter", "unregister plugin... " + plugin.mName);
                if (plugin.isInstalledPlugin())
                {
                    plugin.onUninstalled(InstallMode.DefaultInstall);
                    plugin.mMetaInfo.isInstalled = false;
                }
                plugin.onUnRegisterResult(RetConst.MM_OK, 0);
                plugin.mMetaInfo.isRegistered = false;
                PluginMetaStorage.updateMetaInfo(plugin);
            }
            return true;
        }
    }
}

