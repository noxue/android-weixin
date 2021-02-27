namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="NewInitResponse")]
    public class NewInitResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _CmdCount;
        private readonly List<CmdItem> _CmdList = new List<CmdItem>();
        private uint _ContinueFlag;
        private SKBuiltinBuffer_t _CurrentSynckey;
        private SKBuiltinBuffer_t _MaxSynckey;
        private uint _Ratio;
        private uint _SelectBitmap;
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

        [ProtoMember(6, IsRequired=true, Name="CmdCount", DataFormat=DataFormat.TwosComplement)]
        public uint CmdCount
        {
            get
            {
                return this._CmdCount;
            }
            set
            {
                this._CmdCount = value;
            }
        }

        [ProtoMember(7, Name="CmdList", DataFormat=DataFormat.Default)]
        public List<CmdItem> CmdList
        {
            get
            {
                return this._CmdList;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ContinueFlag
        {
            get
            {
                return this._ContinueFlag;
            }
            set
            {
                this._ContinueFlag = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="CurrentSynckey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t CurrentSynckey
        {
            get
            {
                return this._CurrentSynckey;
            }
            set
            {
                this._CurrentSynckey = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="MaxSynckey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t MaxSynckey
        {
            get
            {
                return this._MaxSynckey;
            }
            set
            {
                this._MaxSynckey = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="Ratio", DataFormat=DataFormat.TwosComplement)]
        public uint Ratio
        {
            get
            {
                return this._Ratio;
            }
            set
            {
                this._Ratio = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="SelectBitmap", DataFormat=DataFormat.TwosComplement)]
        public uint SelectBitmap
        {
            get
            {
                return this._SelectBitmap;
            }
            set
            {
                this._SelectBitmap = value;
            }
        }
    }
}

