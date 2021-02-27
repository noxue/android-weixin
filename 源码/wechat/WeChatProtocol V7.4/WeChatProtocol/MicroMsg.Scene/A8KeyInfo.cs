namespace MicroMsg.Scene
{
    using System;

    public class A8KeyInfo
    {
        public string A8Key;
        public uint ActionCode;
        public string Content;
        public string FullURL;
        public uint nFlagGeneralControlSet;
        public uint nFlagPermissionSet;
        private static readonly bool[] sArrDefaultPermissionSet = new bool[] { 
            false, true, true, false, false, false, false, false, true, true, true, true, true, false, false, true, 
            true, true, false, true, true, true, true, true, true, true, false
         };
        public string Title;

        public static enMMScanQrcodeActionCode GetActionCode(A8KeyInfo info)
        {
            if (info == null)
            {
                return enMMScanQrcodeActionCode.MMSCAN_QRCODE_WEBVIEW_NO_NOTICE;
            }
            return (enMMScanQrcodeActionCode) info.ActionCode;
        }

        //public static bool HasPermission(A8KeyInfo info, enJSAPIPermissionBitSet flag)
        //{
        //    uint num = (uint) flag;
        //    if (info != null)
        //    {
        //        return ((num & info.nFlagPermissionSet) != 0);
        //    }
        //    int index = 0;
        //    while (num != 0)
        //    {
        //        num = num >> 1;
        //        index++;
        //    }
        //    return (((index < sArrDefaultPermissionSet.Length) && (index >= 0)) && sArrDefaultPermissionSet[index]);
        //}
    }
}

