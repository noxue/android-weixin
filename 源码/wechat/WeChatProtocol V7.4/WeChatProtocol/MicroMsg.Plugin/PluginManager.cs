namespace MicroMsg.Plugin
{
    using MicroMsg.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PluginManager
    {
        public static List<PluginBase> mListPlugin = new List<PluginBase>();
        private const string TAG = "PluginManager";

        public static bool addPlugin(PluginBase plugin)
        {
            if (plugin == null)
            {
                return false;
            }
            if (mListPlugin.Contains(plugin))
            {
                return false;
            }
            PluginBase item = findPluginByName(plugin.mName);
            if (item != null)
            {
                mListPlugin.Remove(item);
            }
            mListPlugin.Add(plugin);
            return true;
        }

        public static PluginBase findPluginByID(long id)
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where plugin.mID == id
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            if (source.Count<PluginBase>() > 1)
            {
                Log.e("PluginManager", " more than one with same plugin , id = " + id);
            }
            return source.FirstOrDefault<PluginBase>();
        }

        public static PluginBase findPluginByName(string name)
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where plugin.mName == name
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            if (source.Count<PluginBase>() > 1)
            {
                Log.e("PluginManager", " more than one with same plugin , name = " + name);
            }
            return source.FirstOrDefault<PluginBase>();
        }

        public static PluginBase findPluginByUserName(string name)
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where plugin.mUserName == name
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            if (source.Count<PluginBase>() > 1)
            {
                Log.e("PluginManager", " more than one with mUserName plugin , name = " + name);
            }
            return source.FirstOrDefault<PluginBase>();
        }

        public static List<PluginBase> getAllList()
        {
            return mListPlugin;
        }

        public static List<PluginBase> getAllListInTab(MainTabIndex index)
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where plugin.isInTab(index)
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            return source.ToList<PluginBase>();
        }

        public static List<PluginBase> getInstalledList()
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where PluginBase.isInstalledPlugin(plugin)
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            return source.ToList<PluginBase>();
        }

        public static List<PluginBase> getListInTab(MainTabIndex index)
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where PluginBase.isInstalledPlugin(plugin) && plugin.isInTab(index)
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            return source.ToList<PluginBase>();
        }

        public static List<PluginBase> getUninstalledList()
        {
            IEnumerable<PluginBase> source = from plugin in mListPlugin
                where !PluginBase.isInstalledPlugin(plugin)
                select plugin;
            if (source.Count<PluginBase>() <= 0)
            {
                return null;
            }
            return source.ToList<PluginBase>();
        }

        public static bool removePlugin(PluginBase plugin)
        {
            if (plugin == null)
            {
                return false;
            }
            return mListPlugin.Remove(plugin);
        }

        public static void reset()
        {
            mListPlugin.Clear();
        }

        public static void searchAllPlugin(Func<PluginBase, bool> predicate)
        {
            foreach (PluginBase base2 in mListPlugin)
            {
                if (PluginBase.isInstalledPlugin(base2))
                {
                    predicate(base2);
                }
            }
        }

        public static void searchOnePlugin(Func<PluginBase, bool> predicate)
        {
            foreach (PluginBase base2 in mListPlugin)
            {
                if (PluginBase.isInstalledPlugin(base2) && predicate(base2))
                {
                    break;
                }
            }
        }
    }
}

