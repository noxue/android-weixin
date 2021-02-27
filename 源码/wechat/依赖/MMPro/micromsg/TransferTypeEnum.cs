namespace micromsg
{
    using ProtoBuf;
    using System;

    [ProtoContract(Name="TransferTypeEnum")]
    public enum TransferTypeEnum
    {
        [ProtoEnum(Name="TRANSFER_ADDON", Value=1)]
        TRANSFER_ADDON = 1,
        [ProtoEnum(Name="TRANSFER_NEW", Value=0)]
        TRANSFER_NEW = 0
    }
}

