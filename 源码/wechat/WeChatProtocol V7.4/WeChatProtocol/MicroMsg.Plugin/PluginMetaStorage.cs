namespace MicroMsg.Plugin
{
    using MicroMsg.Manager;
    using System;

    public class PluginMetaStorage
    {
        private static PluginMetaMap mPluginMetaMap;
        private const string TAG = "PluginMetaStorage";

        public static void flushMetaToXML()
        {
            //ConfigMgr.write<PluginMetaMap>(mPluginMetaMap);
        }

        public static void loadMetaFromXML()
        {
            mPluginMetaMap = null;//ConfigMgr.read<PluginMetaMap>();
            if (mPluginMetaMap == null)
            {
                mPluginMetaMap = new PluginMetaMap();
            }
        }

        public static bool restoreMetaInfo(PluginBase plug)
        {
            if ((mPluginMetaMap == null) || (plug == null))
            {
                return false;
            }
            string key = plug.mName + plug.mID;
            if (!mPluginMetaMap.mapPluginMetaInfo.ContainsKey(key))
            {
                return false;
            }
            PluginMetaInfo plugMeta = mPluginMetaMap.mapPluginMetaInfo[key];
            plug.mMetaInfo.copyFrom(plugMeta);
            return true;
        }

        public static bool updateMetaInfo(PluginBase plug)
        {
            if ((mPluginMetaMap != null) && PluginBase.isValidPlugin(plug))
            {
                string key = plug.mName + plug.mID;
                if (!mPluginMetaMap.mapPluginMetaInfo.ContainsKey(key))
                {
                    PluginMetaInfo info = new PluginMetaInfo();
                    info.copyFrom(plug.mMetaInfo);
                    mPluginMetaMap.mapPluginMetaInfo.Add(key, info);
                }
                else
                {
                    mPluginMetaMap.mapPluginMetaInfo[key].copyFrom(plug.mMetaInfo);
                }
                flushMetaToXML();
            }
            return true;
        }
    }
}

