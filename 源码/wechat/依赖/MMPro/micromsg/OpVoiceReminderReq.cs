namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="OpVoiceReminderReq")]
    public class OpVoiceReminderReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private int _OpCode;
        private readonly List<VoiceRemindInfo> _RemindInfoList = new List<VoiceRemindInfo>();
        private uint _RemindInfoNum;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public int OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(4, Name="RemindInfoList", DataFormat=DataFormat.Default)]
        public List<VoiceRemindInfo> RemindInfoList
        {
            get
            {
                return this._RemindInfoList;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="RemindInfoNum", DataFormat=DataFormat.TwosComplement)]
        public uint RemindInfoNum
        {
            get
            {
                return this._RemindInfoNum;
            }
            set
            {
                this._RemindInfoNum = value;
            }
        }
    }
}

