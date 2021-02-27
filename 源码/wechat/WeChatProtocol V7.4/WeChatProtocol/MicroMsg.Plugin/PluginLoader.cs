namespace MicroMsg.Plugin
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Storage;
    using System;

    public class PluginLoader
    {
        private const string TAG = "PluginLoader";

        public static bool preLoadPlugin(string className)
        {
            string typeName = "MicroMsg.Plugin." + className + "." + className;
            Type type = Type.GetType(typeName);
            if (type == null)
            {
                Log.e("PluginLoader", "Not found class, name =  " + typeName);
                return false;
            }
            Type c = typeof(PluginBase);
            if (!type.IsSubclassOf(c))
            {
                Log.e("PluginLoader", "Not a plugin class, name = " + typeName);
                return false;
            }
            PluginBase plug = Activator.CreateInstance(type) as PluginBase;
            if (plug == null)
            {
                Log.e("PluginLoader", "Failed to create instance, name = " + typeName);
                return false;
            }
            if (!plug.mEnabled)
            {
                Log.e("PluginLoader", "Plugin disabled, name = " + typeName);
                return false;
            }
            Account account = AccountMgr.getCurAccount();
            if (!PluginMetaStorage.restoreMetaInfo(plug))
            {
                Log.i("PluginLoader", "First load plugin, name = " + typeName);
                if (!ExtentCenter.registerPlugin(plug))
                {
                    Log.e("PluginLoader", "Not pass to register plugin, name = " + typeName);
                    return false;
                }
                if (plug.mProtocolPluginFlag != ((EPluginFlag)0))
                {
                    if ((((EPluginFlag)account.nPluginFlag) & plug.mProtocolPluginFlag) == ((EPluginFlag)0))
                    {
                        ExtentCenter.installPlugin(plug, InstallMode.DefaultInstall);
                    }
                }
                else if (plug.mDefaultInstalled)
                {
                    ExtentCenter.installPlugin(plug, InstallMode.DefaultInstall);
                }
            }
            else
            {
                Log.i("PluginLoader", "Restore plugin completed, name = " + typeName);
                if (!PluginBase.isValidPlugin(plug))
                {
                    Log.e("PluginLoader", "Ignored invalid plugin , name = " + typeName);
                    return false;
                }
                ExtentCenter.initializePlugin(plug);
                ExtentCenter.syncPluginInstall(plug, account.nPluginFlag);
            }
            PluginManager.addPlugin(plug);
            return true;
        }

        public static void scanAvailablePluginList()
        {
            PluginManager.reset();
            //    ExternPluginList list = StorageXml.loadResObject<ExternPluginList>(new Uri("Source/Plugin/PluginManifest.xml", UriKind.Relative));
            //    if (list != null)
            //    {
            //        foreach (string str in list.pluginClassName)
            //        {//‘ÿ»Î≤Âº˛
            //            preLoadPlugin(str);
            //        }
            //    }
            //}
            preLoadPlugin("Plugin_Reply");
        }
    }
}

