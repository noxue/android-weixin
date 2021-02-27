namespace micromsg
{
    using ProtoBuf;
    using System;

    [ProtoContract(Name="ExtDevLoginType")]
    public enum ExtDevLoginType
    {
        [ProtoEnum(Name="EXTDEV_LOGINTYPE_NORMAL", Value=0)]
        EXTDEV_LOGINTYPE_NORMAL = 0,
        [ProtoEnum(Name="EXTDEV_LOGINTYPE_PAIR", Value=2)]
        EXTDEV_LOGINTYPE_PAIR = 2,
        [ProtoEnum(Name="EXTDEV_LOGINTYPE_TMP", Value=1)]
        EXTDEV_LOGINTYPE_TMP = 1
    }
}

