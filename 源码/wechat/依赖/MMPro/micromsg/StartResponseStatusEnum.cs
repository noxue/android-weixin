namespace micromsg
{
    using ProtoBuf;
    using System;

    [ProtoContract(Name="StartResponseStatusEnum")]
    public enum StartResponseStatusEnum
    {
        [ProtoEnum(Name="START_RESPONSE_ID_WRONG", Value=1)]
        START_RESPONSE_ID_WRONG = 1,
        [ProtoEnum(Name="START_RESPONSE_SIZE_WRONG", Value=2)]
        START_RESPONSE_SIZE_WRONG = 2,
        [ProtoEnum(Name="START_RESPONSE_SUCCESS", Value=0)]
        START_RESPONSE_SUCCESS = 0
    }
}

