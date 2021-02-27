namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="VoipSpeedTestRsp")]
    public class VoipSpeedTestRsp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _NeedTest;
        private readonly List<SpeedTestSvr> _SvrList = new List<SpeedTestSvr>();
        private uint _SvrListCnt;
        private ulong _TestId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="NeedTest", DataFormat=DataFormat.TwosComplement)]
        public uint NeedTest
        {
            get
            {
                return this._NeedTest;
            }
            set
            {
                this._NeedTest = value;
            }
        }

        [ProtoMember(5, Name="SvrList", DataFormat=DataFormat.Default)]
        public List<SpeedTestSvr> SvrList
        {
            get
            {
                return this._SvrList;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="SvrListCnt", DataFormat=DataFormat.TwosComplement)]
        public uint SvrListCnt
        {
            get
            {
                return this._SvrListCnt;
            }
            set
            {
                this._SvrListCnt = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TestId", DataFormat=DataFormat.TwosComplement)]
        public ulong TestId
        {
            get
            {
                return this._TestId;
            }
            set
            {
                this._TestId = value;
            }
        }
    }
}

