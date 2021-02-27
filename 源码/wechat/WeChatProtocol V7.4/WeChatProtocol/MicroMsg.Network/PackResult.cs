namespace MicroMsg.Network
{
    using System;

    public enum PackResult
    {
        SUCCESS,
        PACK_ERROR,
        UNPACK_ERROR,
        RETRY_LIMIT,
        NET_ERROR,
        PACK_TIMEOUT,
        AUTH_ERROR,
        PARSER_ERROR,
        BEEN_CANCELLED
    }
}

