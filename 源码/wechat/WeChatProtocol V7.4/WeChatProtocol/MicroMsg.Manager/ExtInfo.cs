namespace MicroMsg.Manager
{
    using System;
    
    using System.Runtime.Serialization;


    public class ExtInfo
    {
        public ExtInfo()
        {
        }

        public ExtInfo(string a, string b, string c, int d, uint e, string f, string g, string h)
        {
            this.IsShowHeadImgInMsg = a;
            this.IsHideInputToolbarInMsg = b;
            this.IsAgreeProtocol = c;
            this.RoleId = d;
            this.ConferenceContactExpireTime = e;
            this.IsShowMember = f;
            this.VerifyContactPromptTitle = g;
            this.VerifyMsg2Remark = h;
        }

        
        public uint ConferenceContactExpireTime { get; set; }

        
        public string IsAgreeProtocol { get; set; }

        
        public string IsHideInputToolbarInMsg { get; set; }

        
        public string IsShowHeadImgInMsg { get; set; }

        
        public string IsShowMember { get; set; }

        
        public int RoleId { get; set; }

        
        public string VerifyContactPromptTitle { get; set; }

        
        public string VerifyMsg2Remark { get; set; }
    }
}

