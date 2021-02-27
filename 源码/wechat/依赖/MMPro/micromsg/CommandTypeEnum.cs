namespace micromsg
{
    using ProtoBuf;
    using System;

    [ProtoContract(Name="CommandTypeEnum")]
    public enum CommandTypeEnum
    {
        [ProtoEnum(Name="COMMAND_CONFIRM_BACKUP", Value=2)]
        COMMAND_CONFIRM_BACKUP = 2,
        [ProtoEnum(Name="COMMAND_CONFIRM_CONTINUE_BACKUP", Value=6)]
        COMMAND_CONFIRM_CONTINUE_BACKUP = 6,
        [ProtoEnum(Name="COMMAND_CONFIRM_CONTINUE_RECOVER", Value=8)]
        COMMAND_CONFIRM_CONTINUE_RECOVER = 8,
        [ProtoEnum(Name="COMMAND_CONFIRM_RECOVER", Value=4)]
        COMMAND_CONFIRM_RECOVER = 4,
        [ProtoEnum(Name="COMMAND_REQUEST_TO_BACKUP", Value=1)]
        COMMAND_REQUEST_TO_BACKUP = 1,
        [ProtoEnum(Name="COMMAND_REQUEST_TO_CONTINUE_BACKUP", Value=5)]
        COMMAND_REQUEST_TO_CONTINUE_BACKUP = 5,
        [ProtoEnum(Name="COMMAND_REQUEST_TO_CONTINUE_RECOVER", Value=7)]
        COMMAND_REQUEST_TO_CONTINUE_RECOVER = 7,
        [ProtoEnum(Name="COMMAND_REQUEST_TO_RECOVER", Value=3)]
        COMMAND_REQUEST_TO_RECOVER = 3
    }
}

