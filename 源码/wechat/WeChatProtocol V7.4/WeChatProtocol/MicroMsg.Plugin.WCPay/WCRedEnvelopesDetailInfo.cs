namespace MicroMsg.Plugin.WCPay
{
    using Google.ProtocolBuffers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class WCRedEnvelopesDetailInfo
    {
        public List<WCRedEnvelopesOperationInfo> m_arrOperationInfoList;
        public List<WCRedEnvelopesReceiverInfo> m_arrReceiveList;
        public bool m_bAllowJumpToBalance;
        public bool m_bCanAtomic;
        public bool m_bCanShareHB;
        public bool m_bCheckFlag;
        public bool m_bHasFocused;
        public bool m_bHasMoreReceiver;
        public bool m_bHasWriteThanks = true;
        public bool m_bIsSender;
        public bool m_bReceived;
        public bool m_bShownOpHeadInfo;
        public ENWCRedEnvelopesKind m_enWCRedEnvelopesKind;
        public ENWCRedEnvelopesStatus m_enWCRedEnvelopesStatus;
        public ENWCRedEnvelopesType m_enWCRedEnvelopesType;
        public ENWCRedEnvelopesUserReceiveStatus m_enWCRedEnvelopesUserReceiveStatus;
        public long m_lAmount;
        public int m_localResId;
        public long m_lRecAmount;
        public long m_lRecNum;
        public long m_lTotalAmount;
        public long m_lTotalNum;
        public string m_nsAdMessage;
        public string m_nsAdUrl;
        public string m_nsAppUsrName = "";
        public string m_nsContextUrl;
        public string m_nsExternMessage = "";
        public string m_nsFocusedMsg = "";
        public string m_nsGameMessage;
        public string m_nsHeaderMaskImageUrl = "";
        public string m_nsHeadTitle;
        public string m_nsJumpBalanceDesc;
        public string m_nsOpenTicket;
        public string m_nsReceiveId;
        public string m_nsSendHeadImg;
        public string m_nsSendId;
        public string m_nsSendNickName;
        public string m_nsSendUserName;
        public string m_nsStatusMessage;
        public string m_nsWaterMarkUrl;
        public string m_nsWishing;
        public WCRedEnvelopesAtomicInfo m_oWCRedEnvelopesAtomicInfo;
        public WCRedEnvelopesOperationInfo m_oWCRedEnvelopesOpTail;
        public ByteString m_sendKeyData;
        public string m_shareHintMess = "";
    }
}

