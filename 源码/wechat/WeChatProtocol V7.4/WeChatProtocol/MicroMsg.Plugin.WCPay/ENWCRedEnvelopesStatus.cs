namespace MicroMsg.Plugin.WCPay
{
    using System;

    public enum ENWCRedEnvelopesStatus
    {
        ENWCRedEnvelopesAllReceivedStatus = 4,
        ENWCRedEnvelopesNotAllReceivedStatus = 3,
        ENWCRedEnvelopesPaidStatus = 2,
        ENWCRedEnvelopesTimeOutStatus = 5,
        ENWCRedEnvelopesWaitPayStatus = 1
    }
}

