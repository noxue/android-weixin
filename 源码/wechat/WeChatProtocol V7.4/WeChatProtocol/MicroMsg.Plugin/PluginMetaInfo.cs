namespace MicroMsg.Plugin
{
    using MicroMsg.Storage;
    using System;


    public class PluginMetaInfo
    {
        public bool isInstalled;
        public bool isRegistered;

        public void copyFrom(PluginMetaInfo plugMeta)
        {
            this.isRegistered = plugMeta.isRegistered;
            this.isInstalled = plugMeta.isInstalled;
        }
    }
}

