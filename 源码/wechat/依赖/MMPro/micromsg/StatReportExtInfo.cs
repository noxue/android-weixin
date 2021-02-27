namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="StatReportExtInfo")]
    public class StatReportExtInfo : IExtensible
    {
        private uint _BeginTime;
        private uint _CGICount;
        private uint _CGIFailCount;
        private uint _ClientReportTime;
        private uint _EndTime;
        private uint _NotifyCount;
        private uint _PushSyncCount = 0;
        private uint _SyncCount;
        private uint _SyncFailCount;
        private uint _TotalDownloadSize;
        private uint _TotalUploadSize;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BeginTime", DataFormat=DataFormat.TwosComplement)]
        public uint BeginTime
        {
            get
            {
                return this._BeginTime;
            }
            set
            {
                this._BeginTime = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="CGICount", DataFormat=DataFormat.TwosComplement)]
        public uint CGICount
        {
            get
            {
                return this._CGICount;
            }
            set
            {
                this._CGICount = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="CGIFailCount", DataFormat=DataFormat.TwosComplement)]
        public uint CGIFailCount
        {
            get
            {
                return this._CGIFailCount;
            }
            set
            {
                this._CGIFailCount = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ClientReportTime", DataFormat=DataFormat.TwosComplement)]
        public uint ClientReportTime
        {
            get
            {
                return this._ClientReportTime;
            }
            set
            {
                this._ClientReportTime = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="EndTime", DataFormat=DataFormat.TwosComplement)]
        public uint EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                this._EndTime = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="NotifyCount", DataFormat=DataFormat.TwosComplement)]
        public uint NotifyCount
        {
            get
            {
                return this._NotifyCount;
            }
            set
            {
                this._NotifyCount = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="PushSyncCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(6, IsRequired=true, Name="SyncCount", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(7, IsRequired=true, Name="SyncFailCount", DataFormat=DataFormat.TwosComplement)]
        public uint SyncFailCount
        {
            get
            {
                return this._SyncFailCount;
            }
            set
            {
                this._SyncFailCount = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="TotalDownloadSize", DataFormat=DataFormat.TwosComplement)]
        public uint TotalDownloadSize
        {
            get
            {
                return this._TotalDownloadSize;
            }
            set
            {
                this._TotalDownloadSize = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="TotalUploadSize", DataFormat=DataFormat.TwosComplement)]
        public uint TotalUploadSize
        {
            get
            {
                return this._TotalUploadSize;
            }
            set
            {
                this._TotalUploadSize = value;
            }
        }
    }
}

