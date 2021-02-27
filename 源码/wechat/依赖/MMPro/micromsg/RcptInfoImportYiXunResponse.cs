namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="RcptInfoImportYiXunResponse")]
    public class RcptInfoImportYiXunResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private int _rcptinfoimportstatus;
        private RcptInfoList _rcptinfolist;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
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

        [ProtoMember(2, IsRequired=true, Name="rcptinfoimportstatus", DataFormat=DataFormat.TwosComplement)]
        public int rcptinfoimportstatus
        {
            get
            {
                return this._rcptinfoimportstatus;
            }
            set
            {
                this._rcptinfoimportstatus = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="rcptinfolist", DataFormat=DataFormat.Default)]
        public RcptInfoList rcptinfolist
        {
            get
            {
                return this._rcptinfolist;
            }
            set
            {
                this._rcptinfolist = value;
            }
        }
    }
}

