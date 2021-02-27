namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="StatReportItem")]
    public class StatReportItem : IExtensible
    {
        private uint _ActionID;
        private uint _AliveTime;
        private ulong _BeginTimeMS = 0L;
        private uint _ClientIP = 0;
        private uint _ConnCost = 0;
        private uint _Conncosttime = 0;
        private uint _ConnCount = 0;
        private uint _Cost;
        private uint _Count = 0;
        private uint _DnsCost = 0;
        private uint _Dnscosttime = 0;
        private uint _DnsCount = 0;
        private uint _Dnserrtype = 0;
        private uint _DownloadSize;
        private uint _EndFlag = 0;
        private uint _Endstep = 0;
        private ulong _EndTimeMS = 0L;
        private int _ErrCode = 0;
        private uint _FirstPkgTime = 0;
        private uint _FunID;
        private string _Host = "";
        private uint _IfSuc;
        private int _InnerIp = 0;
        private uint _IP;
        private uint _IPCnt = 0;
        private int _IpIndex = 0;
        private uint _IPType;
        private uint _IsDNS = 0;
        private uint _ISPCode = 0;
        private string _ISPName = "";
        private uint _IsSocket = 0;
        private uint _IsWifiFirstConnect = 0;
        private string _NetExtraInfo = "";
        private uint _NetType;
        private uint _NetworkCost = 0;
        private uint _NewDnsCostTime = 0;
        private uint _NewDnsErrCode = 0;
        private uint _NewDnsErrType = 0;
        private uint _NewDnsSvrIp = 0;
        private int _NewNetType = 0;
        private uint _NotifySyncCount = 0;
        private uint _Port;
        private uint _PushSyncCount = 0;
        private ulong _reserved1 = 0L;
        private ulong _reserved2 = 0L;
        private ulong _reserved3 = 0L;
        private uint _RetryCount = 0;
        private uint _SignalStrength = 0;
        private string _StatReportExtraInfo = "";
        private int _SubNetType = 0;
        private uint _SyncCount = 0;
        private uint _Time;
        private int _TotalConnCost = 0;
        private uint _TotalTime = 0;
        private uint _UploadSize;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ActionID", DataFormat=DataFormat.TwosComplement)]
        public uint ActionID
        {
            get
            {
                return this._ActionID;
            }
            set
            {
                this._ActionID = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="AliveTime", DataFormat=DataFormat.TwosComplement)]
        public uint AliveTime
        {
            get
            {
                return this._AliveTime;
            }
            set
            {
                this._AliveTime = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="BeginTimeMS", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong BeginTimeMS
        {
            get
            {
                return this._BeginTimeMS;
            }
            set
            {
                this._BeginTimeMS = value;
            }
        }

        [ProtoMember(0x20, IsRequired=false, Name="ClientIP", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ClientIP
        {
            get
            {
                return this._ClientIP;
            }
            set
            {
                this._ClientIP = value;
            }
        }

        [ProtoMember(0x25, IsRequired=false, Name="ConnCost", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ConnCost
        {
            get
            {
                return this._ConnCost;
            }
            set
            {
                this._ConnCost = value;
            }
        }

        [ProtoMember(0x2d, IsRequired=false, Name="Conncosttime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Conncosttime
        {
            get
            {
                return this._Conncosttime;
            }
            set
            {
                this._Conncosttime = value;
            }
        }

        [ProtoMember(0x24, IsRequired=false, Name="ConnCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ConnCount
        {
            get
            {
                return this._ConnCount;
            }
            set
            {
                this._ConnCount = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="Cost", DataFormat=DataFormat.TwosComplement)]
        public uint Cost
        {
            get
            {
                return this._Cost;
            }
            set
            {
                this._Cost = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="Count", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(0x23, IsRequired=false, Name="DnsCost", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DnsCost
        {
            get
            {
                return this._DnsCost;
            }
            set
            {
                this._DnsCost = value;
            }
        }

        [ProtoMember(0x2f, IsRequired=false, Name="Dnscosttime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Dnscosttime
        {
            get
            {
                return this._Dnscosttime;
            }
            set
            {
                this._Dnscosttime = value;
            }
        }

        [ProtoMember(0x22, IsRequired=false, Name="DnsCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DnsCount
        {
            get
            {
                return this._DnsCount;
            }
            set
            {
                this._DnsCount = value;
            }
        }

        [ProtoMember(0x30, IsRequired=false, Name="Dnserrtype", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Dnserrtype
        {
            get
            {
                return this._Dnserrtype;
            }
            set
            {
                this._Dnserrtype = value;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="DownloadSize", DataFormat=DataFormat.TwosComplement)]
        public uint DownloadSize
        {
            get
            {
                return this._DownloadSize;
            }
            set
            {
                this._DownloadSize = value;
            }
        }

        [ProtoMember(0x2b, IsRequired=false, Name="EndFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint EndFlag
        {
            get
            {
                return this._EndFlag;
            }
            set
            {
                this._EndFlag = value;
            }
        }

        [ProtoMember(0x2e, IsRequired=false, Name="Endstep", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Endstep
        {
            get
            {
                return this._Endstep;
            }
            set
            {
                this._Endstep = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="EndTimeMS", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong EndTimeMS
        {
            get
            {
                return this._EndTimeMS;
            }
            set
            {
                this._EndTimeMS = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="ErrCode", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ErrCode
        {
            get
            {
                return this._ErrCode;
            }
            set
            {
                this._ErrCode = value;
            }
        }

        [ProtoMember(0x2a, IsRequired=false, Name="FirstPkgTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FirstPkgTime
        {
            get
            {
                return this._FirstPkgTime;
            }
            set
            {
                this._FirstPkgTime = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="FunID", DataFormat=DataFormat.TwosComplement)]
        public uint FunID
        {
            get
            {
                return this._FunID;
            }
            set
            {
                this._FunID = value;
            }
        }

        [ProtoMember(0x1b, IsRequired=false, Name="Host", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Host
        {
            get
            {
                return this._Host;
            }
            set
            {
                this._Host = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="IfSuc", DataFormat=DataFormat.TwosComplement)]
        public uint IfSuc
        {
            get
            {
                return this._IfSuc;
            }
            set
            {
                this._IfSuc = value;
            }
        }

        [ProtoMember(0x37, IsRequired=false, Name="InnerIp", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int InnerIp
        {
            get
            {
                return this._InnerIp;
            }
            set
            {
                this._InnerIp = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="IP", DataFormat=DataFormat.TwosComplement)]
        public uint IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                this._IP = value;
            }
        }

        [ProtoMember(0x1c, IsRequired=false, Name="IPCnt", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IPCnt
        {
            get
            {
                return this._IPCnt;
            }
            set
            {
                this._IPCnt = value;
            }
        }

        [ProtoMember(0x36, IsRequired=false, Name="IpIndex", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int IpIndex
        {
            get
            {
                return this._IpIndex;
            }
            set
            {
                this._IpIndex = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="IPType", DataFormat=DataFormat.TwosComplement)]
        public uint IPType
        {
            get
            {
                return this._IPType;
            }
            set
            {
                this._IPType = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="IsDNS", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IsDNS
        {
            get
            {
                return this._IsDNS;
            }
            set
            {
                this._IsDNS = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="ISPCode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ISPCode
        {
            get
            {
                return this._ISPCode;
            }
            set
            {
                this._ISPCode = value;
            }
        }

        [ProtoMember(0x19, IsRequired=false, Name="ISPName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ISPName
        {
            get
            {
                return this._ISPName;
            }
            set
            {
                this._ISPName = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="IsSocket", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IsSocket
        {
            get
            {
                return this._IsSocket;
            }
            set
            {
                this._IsSocket = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="IsWifiFirstConnect", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IsWifiFirstConnect
        {
            get
            {
                return this._IsWifiFirstConnect;
            }
            set
            {
                this._IsWifiFirstConnect = value;
            }
        }

        [ProtoMember(0x33, IsRequired=false, Name="NetExtraInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NetExtraInfo
        {
            get
            {
                return this._NetExtraInfo;
            }
            set
            {
                this._NetExtraInfo = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="NetType", DataFormat=DataFormat.TwosComplement)]
        public uint NetType
        {
            get
            {
                return this._NetType;
            }
            set
            {
                this._NetType = value;
            }
        }

        [ProtoMember(0x21, IsRequired=false, Name="NetworkCost", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NetworkCost
        {
            get
            {
                return this._NetworkCost;
            }
            set
            {
                this._NetworkCost = value;
            }
        }

        [ProtoMember(0x26, IsRequired=false, Name="NewDnsCostTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NewDnsCostTime
        {
            get
            {
                return this._NewDnsCostTime;
            }
            set
            {
                this._NewDnsCostTime = value;
            }
        }

        [ProtoMember(40, IsRequired=false, Name="NewDnsErrCode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NewDnsErrCode
        {
            get
            {
                return this._NewDnsErrCode;
            }
            set
            {
                this._NewDnsErrCode = value;
            }
        }

        [ProtoMember(0x27, IsRequired=false, Name="NewDnsErrType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NewDnsErrType
        {
            get
            {
                return this._NewDnsErrType;
            }
            set
            {
                this._NewDnsErrType = value;
            }
        }

        [ProtoMember(0x29, IsRequired=false, Name="NewDnsSvrIp", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NewDnsSvrIp
        {
            get
            {
                return this._NewDnsSvrIp;
            }
            set
            {
                this._NewDnsSvrIp = value;
            }
        }

        [ProtoMember(0x31, IsRequired=false, Name="NewNetType", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NewNetType
        {
            get
            {
                return this._NewNetType;
            }
            set
            {
                this._NewNetType = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="NotifySyncCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NotifySyncCount
        {
            get
            {
                return this._NotifySyncCount;
            }
            set
            {
                this._NotifySyncCount = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Port", DataFormat=DataFormat.TwosComplement)]
        public uint Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                this._Port = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="PushSyncCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PushSyncCount
        {
            get
            {
                return this._PushSyncCount;
            }
            set
            {
                this._PushSyncCount = value;
            }
        }

        [ProtoMember(0x1d, IsRequired=false, Name="reserved1", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong reserved1
        {
            get
            {
                return this._reserved1;
            }
            set
            {
                this._reserved1 = value;
            }
        }

        [ProtoMember(30, IsRequired=false, Name="reserved2", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong reserved2
        {
            get
            {
                return this._reserved2;
            }
            set
            {
                this._reserved2 = value;
            }
        }

        [ProtoMember(0x1f, IsRequired=false, Name="reserved3", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong reserved3
        {
            get
            {
                return this._reserved3;
            }
            set
            {
                this._reserved3 = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="RetryCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RetryCount
        {
            get
            {
                return this._RetryCount;
            }
            set
            {
                this._RetryCount = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="SignalStrength", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SignalStrength
        {
            get
            {
                return this._SignalStrength;
            }
            set
            {
                this._SignalStrength = value;
            }
        }

        [ProtoMember(0x34, IsRequired=false, Name="StatReportExtraInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StatReportExtraInfo
        {
            get
            {
                return this._StatReportExtraInfo;
            }
            set
            {
                this._StatReportExtraInfo = value;
            }
        }

        [ProtoMember(50, IsRequired=false, Name="SubNetType", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int SubNetType
        {
            get
            {
                return this._SubNetType;
            }
            set
            {
                this._SubNetType = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="SyncCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SyncCount
        {
            get
            {
                return this._SyncCount;
            }
            set
            {
                this._SyncCount = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Time", DataFormat=DataFormat.TwosComplement)]
        public uint Time
        {
            get
            {
                return this._Time;
            }
            set
            {
                this._Time = value;
            }
        }

        [ProtoMember(0x35, IsRequired=false, Name="TotalConnCost", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int TotalConnCost
        {
            get
            {
                return this._TotalConnCost;
            }
            set
            {
                this._TotalConnCost = value;
            }
        }

        [ProtoMember(0x2c, IsRequired=false, Name="TotalTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TotalTime
        {
            get
            {
                return this._TotalTime;
            }
            set
            {
                this._TotalTime = value;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="UploadSize", DataFormat=DataFormat.TwosComplement)]
        public uint UploadSize
        {
            get
            {
                return this._UploadSize;
            }
            set
            {
                this._UploadSize = value;
            }
        }
    }
}

