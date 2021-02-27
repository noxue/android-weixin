namespace MicroMsg.Network
{
    //using micromsg;
    using MicroMsg.Common.Utils;
    //using MicroMsg.Manager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using micromsg;

    public class HostService
    {
        public static readonly string[] constLongBakHostList;//new string[] { "120.204.201.146", "112.64.200.218", "112.64.237.188", "180.153.82.30", "180.153.218.191", "117.135.130.187" };
        public static readonly string[] constLongDnsList = new string[] { "long.weixin.qq.com" };
        public static readonly string[] constShortBakHostList = new string[] { "112.64.200.240", "112.64.237.186", "180.153.82.27", "117.135.130.177", "180.153.218.192", "120.204.201.143" };
        public static readonly string[] constShortDnsList = new string[] { "short.weixin.qq.com" };
        public const string DEFAULT_LONG_DNS = "long.weixin.qq.com";
        public const string DEFAULT_SHORT_DNS = "short.weixin.qq.com";
        public  HostInfo[] mDebugHost = new HostInfo[] { new HostInfo("180.153.74.12", 0x1f90, 0), new HostInfo("180.153.74.10", 80, 0) };
        public static bool mDebugMode = false;
        public static ConnectHost mLongConnHosts = null;
        public static ConnectHost mShortConnHosts = null;
        private const uint TYPE_LONG_CONN = 0;
        private const uint TYPE_SHORT_CONN = 1;

        public static void init()
        {
            if (mLongConnHosts == null)
            {
                mLongConnHosts = new ConnectHost(constLongDnsList, constLongBakHostList, 0);
            }
            if (mShortConnHosts == null)
            {
                mShortConnHosts = new ConnectHost(constShortDnsList, constShortBakHostList, 1);
            }
            //loadLocalDnsHost();
            //loadLocalBuiltinIP();
        }

        //private static void loadLocalBuiltinIP()
        //{
        //    //ServerHost host = GConfigMgr.read<ServerHost>();
        //    //if ((host != null) && (host.addrList != null))
        //    //{
        //    //    foreach (ServerAddress address in host.addrList)
        //    //    {
        //    //        if (address.nType == 0)
        //    //        {
        //    //            mLongConnHosts.addBuiltInHost(Util.ByteArrayToString(address.bytesIP));
        //    //        }
        //    //        else if (address.nType == 1)
        //    //        {
        //    //            mShortConnHosts.addBuiltInHost(Util.ByteArrayToString(address.bytesIP));
        //    //        }
        //    //    }
        //    //}
        //}

        //private static void loadLocalDnsHost()
        //{
        //    //IDCDnsHost host = GConfigMgr.read<IDCDnsHost>();
        //    //if (((host != null) && (host._keep_idcHostList != null)) && (host._keep_idcHostList.Count > 0))
        //    //{
        //    //    mLongConnHosts.cleanDnsHost();
        //    //    mShortConnHosts.cleanDnsHost();
        //    //    foreach (DNSHostItemInfo info in host._keep_idcHostList)
        //    //    {
        //    //        Log.i("Network", "load local idc host item: origin = " + info._keep_origin + ", substitute = " + info._keep_substitute);
        //    //        if (info._keep_origin == "long.weixin.qq.com")
        //    //        {
        //    //            mLongConnHosts.addDnsHost(info._keep_substitute);
        //    //        }
        //    //        else if (info._keep_origin == "short.weixin.qq.com")
        //    //        {
        //    //            mShortConnHosts.addDnsHost(info._keep_substitute);
        //    //        }
        //    //    }
        //    //}
        //}

        //public static void printInfo()
        //{
        //    if (mLongConnHosts != null)
        //    {
        //        mLongConnHosts.printInfo();
        //    }
        //    if (mShortConnHosts != null)
        //    {
        //        mShortConnHosts.printInfo();
        //    }
        //}

        //public static void setDebugHost(string longHost, string shortHost)
        //{
        //    if (!string.IsNullOrEmpty(longHost))
        //    {
        //        mDebugHost[0].mHost = longHost;
        //    }
        //    if (!string.IsNullOrEmpty(shortHost))
        //    {
        //        mDebugHost[1].mHost = shortHost;
        //    }
        //}

        public static void setDebugMode(bool enable)
        {
            mDebugMode = enable;
            Connector.close();
        }

        public static void setDNSFailed()
        {
            if (mLongConnHosts != null)
            {
                mLongConnHosts.setDNSFailed();
            }
            if (mShortConnHosts != null)
            {
                mShortConnHosts.setDNSFailed();
            }
        }

        public static byte[] trimBytesArray(byte[] buffer)
        {
            int count = 0;
            if (buffer == null)
            {
                return null;
            }
            byte[] buffer3 = buffer;
            for (int i = 0; i < buffer3.Length; i++)
            {
                if (buffer3[i] == 0)
                {
                    break;
                }
                count++;
            }
            if (count == 0)
            {
                return null;
            }
            byte[] dst = new byte[count];
            Buffer.BlockCopy(buffer, 0, dst, 0, count);
            return dst;
        }

        public static void updateAuthBuiltinIP(BuiltinIPList ipList)
        {
            ServerHost data = new ServerHost();
            if (ipList.LongConnectIPListCount > 0)
            {
                mLongConnHosts.cleanBuiltInHost();
                foreach (BuiltinIP nip in ipList.LongConnectIPListList)
                {
                    byte[] buf = trimBytesArray(nip.IP.ToByteArray());
                    if (buf != null)
                    {
                        mLongConnHosts.addBuiltInHost(Util.ByteArrayToString(buf));
                        ServerAddress item = new ServerAddress
                        {
                            nType = 0,
                            bytesIP = buf
                        };
                        data.addrList.Add(item);
                    }
                }
            }
            if (ipList.ShortConnectIPListCount > 0)
            {
                mShortConnHosts.cleanBuiltInHost();
                foreach (BuiltinIP nip2 in ipList.ShortConnectIPListList)
                {
                    byte[] buffer2 = trimBytesArray(nip2.IP.ToByteArray());
                    if (buffer2 != null)
                    {
                        mShortConnHosts.addBuiltInHost(Util.ByteArrayToString(buffer2));
                        ServerAddress address2 = new ServerAddress
                        {
                            nType = 1,
                            bytesIP = buffer2
                        };
                        data.addrList.Add(address2);
                    }
                }
            }
            if ((ipList.LongConnectIPListCount > 0) || (ipList.ShortConnectIPListCount > 0))
            {
               // GConfigMgr.write<ServerHost>(data);
            }
        }

        public static void updateAuthIDCHost(HostList hostList)
        {
            if ((hostList != null) && (hostList.ListCount > 0))
            {
                IDCDnsHost data = new IDCDnsHost
                {
                    _keep_idcHostList = new List<DNSHostItemInfo>()
                };
                mShortConnHosts.cleanDnsHost();
                mLongConnHosts.cleanDnsHost();
                foreach (Host host2 in hostList.ListList)
                {
                    DNSHostItemInfo item = new DNSHostItemInfo
                    {
                        _keep_origin = host2.Origin,
                        _keep_substitute = host2.Substitute
                    };
                    data._keep_idcHostList.Add(item);
                    Log.i("Network", "update idc host item: origin = " + item._keep_origin + ", substitute = " + item._keep_substitute);
                    if (host2.Origin == "long.weixin.qq.com")
                    {
                        mLongConnHosts.addDnsHost(host2.Substitute);
                    }
                    else if (host2.Origin == "short.weixin.qq.com")
                    {
                        mShortConnHosts.addDnsHost(host2.Substitute);
                    }
                }
                if (data._keep_idcHostList.Count > 0)
                {
                    //GConfigMgr.write<IDCDnsHost>(data);
                }
            }
        }

        public class ConnectHost
        {
            private const int BAK_TYPE = 2;
            private const int BUILTIN_TYPE = 1;
            private const int DNS_TYPE = 0;
            private  readonly int[] longConnRetry = new int[] { 2, 1, 1 };
            private  readonly int[] longConntPortMap = new int[] { 80, 0x1bb, 0x1f90 };
            private List<HostService.HostInfo> mBakList = new List<HostService.HostInfo>();
            public List<HostService.HostInfo> mBuiltInList = new List<HostService.HostInfo>();
            private uint mConnectType;
            private HostService.HostInfo mCurrentHost;
            private int mCurrentType;
            public List<HostService.HostInfo> mDnsList = new List<HostService.HostInfo>();
            private  readonly int[] shortConnPortMap = new int[] { 80 };
            private  readonly int[] shortConnRetry = new int[] { 3, 1, 1 };
            private const string TAG = "Network";
            private  readonly string[] tagConnectInfo = new string[] { "longconn", "shortconn" };
            private  readonly string[] tagHostInfo = new string[] { "dns", "builtinip", "backup" };

            public ConnectHost(string[] dnsList, string[] bakList, uint connType)
            {
                this.mConnectType = connType;
                foreach (string str in dnsList)
                {
                    this.addHost(str, this.mDnsList, this.getRetryArray()[0], 0);
                }
            }

            public void addBuiltInHost(string host)
            {
                this.addHost(host, this.mBuiltInList, this.getRetryArray()[1], 1);
            }

            public void addDnsHost(string host)
            {
                this.addHost(host, this.mDnsList, this.getRetryArray()[0], 0);
            }

            private void addHost(string host, List<HostService.HostInfo> listHost, int retry, int type)
            {
                if (listHost.Count >= 0x12)
                {
                    Log.e("Network", "add host too much , ignored host = " + host);
                }
                else if ((from item in listHost
                    where item.mHost == host
                    select item).Count<HostService.HostInfo>() != 0)
                {
                    Log.e("Network", "host exist already , ignored host = " + host);
                }
                else
                {
                    foreach (int num in this.getPortMap())
                    {
                        HostService.HostInfo info = new HostService.HostInfo(host, num, retry);
                        listHost.Add(info);
                    }
                }
            }

            public void cleanBuiltInHost()
            {
                this.mBuiltInList.Clear();
            }

            public void cleanDnsHost()
            {
                this.mDnsList.Clear();
                this.mCurrentHost = null;
            }

            public HostService.HostInfo getAvailableHost()
            {
                //if (HostService.mDebugMode)
                //{
                //    return HostService.mDebugHost[this.mConnectType];
                //}
                if (this.mCurrentHost == null)
                {
                    this.mCurrentHost = this.getFirstAvailableItem(this.mDnsList);
                    this.mCurrentType = 0;
                    if (this.mCurrentHost == null)
                    {
                        Log.i("Network", "dns hosts are not available all, try builtin host...");
                        this.mCurrentHost = this.getFirstAvailableItem(this.mBuiltInList);
                        this.mCurrentType = 1;
                    }
                    if (this.mCurrentHost == null)
                    {
                        Log.i("Network", "builtin hosts are not available all, try backup host...");
                        this.mCurrentHost = this.getFirstAvailableItem(this.mBakList);
                        this.mCurrentType = 2;
                    }
                    if (this.mCurrentHost == null)
                    {
                        Log.i("Network", "all hosts are not available , try dns again...");
                        this.resetAvailableStatus(this.mDnsList, this.getRetryArray()[0]);
                        this.resetAvailableStatus(this.mBuiltInList, this.getRetryArray()[1]);
                        this.resetAvailableStatus(this.mBakList, this.getRetryArray()[2]);
                        this.mCurrentHost = this.mDnsList[0];
                        this.mCurrentType = 0;
                    }
                }
                Log.d("Network", string.Concat(new object[] { "get available host = ", this.mCurrentHost.mHost, ", port = ", this.mCurrentHost.mPort, ", retryLeft = ", this.mCurrentHost.mRetryLeft }));
                return this.mCurrentHost;
            }

            public string getCurrentHostString()
            {
                if (this.mCurrentHost != null)
                {
                    return (this.mCurrentHost.mHost + ":" + this.mCurrentHost.mPort);
                }
                return "";
            }

            private HostService.HostInfo getFirstAvailableItem(List<HostService.HostInfo> listHost)
            {
                foreach (HostService.HostInfo info in listHost)
                {
                    if (info.mRetryLeft > 0)
                    {
                        return info;
                    }
                }
                return null;
            }

            private int[] getPortMap()
            {
                if (this.mConnectType == 0)
                {
                    return longConntPortMap;
                }
                return shortConnPortMap;
            }

            private int[] getRetryArray()
            {
                if (this.mConnectType == 0)
                {
                    return longConnRetry;
                }
                return shortConnRetry;
            }

            public void onCurrentHostFailed()
            {
                if ((this.mCurrentHost != null) && !HostService.mDebugMode)
                {
                    this.mCurrentHost.mRetryLeft--;
                    if (this.mCurrentHost.mRetryLeft <= 0)
                    {
                        Log.e("Network", string.Concat(new object[] { "set current host not available , host = ", this.mCurrentHost.mHost, ", port= ", this.mCurrentHost.mPort, ", type=", this.mCurrentType }));
                        this.mCurrentHost.mRetryLeft = 0;
                        this.mCurrentHost = null;
                    }
                }
            }

            public void onCurrentHostSuccess()
            {
                if ((this.mCurrentHost != null) && !HostService.mDebugMode)
                {
                    this.mCurrentHost.mRetryLeft = this.getRetryArray()[this.mCurrentType];
                }
            }

            public void printInfo()
            {
                if (this.mCurrentHost != null)
                {
                    Log.i("Network", string.Concat(new object[] { "current host ", this.mCurrentHost.mHost, ":", this.mCurrentHost.mPort, ", ", tagConnectInfo[this.mConnectType], ", ", tagHostInfo[this.mCurrentType] }));
                }
            }

            private void resetAvailableStatus(List<HostService.HostInfo> listHost, int retry)
            {
                foreach (HostService.HostInfo info in listHost)
                {
                    info.mRetryLeft = retry;
                }
            }

            public void setDNSFailed()
            {
                this.resetAvailableStatus(this.mDnsList, 0);
            }
        }

        public class HostInfo
        {
            public string mHost;
            public int mPort;
            public int mRetryLeft;

            public HostInfo(string host, int port, int retry)
            {
                this.mHost = host;
                this.mPort = port;
                this.mRetryLeft = retry;
            }
        }
    }
}

