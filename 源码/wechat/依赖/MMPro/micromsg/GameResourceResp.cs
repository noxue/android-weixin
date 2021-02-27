namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GameResourceResp")]
    public class GameResourceResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private int _PropsCount;
        private readonly List<GamePropsInfo> _PropsList = new List<GamePropsInfo>();
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

        [ProtoMember(2, IsRequired=true, Name="PropsCount", DataFormat=DataFormat.TwosComplement)]
        public int PropsCount
        {
            get
            {
                return this._PropsCount;
            }
            set
            {
                this._PropsCount = value;
            }
        }

        [ProtoMember(3, Name="PropsList", DataFormat=DataFormat.Default)]
        public List<GamePropsInfo> PropsList
        {
            get
            {
                return this._PropsList;
            }
        }
    }
}

