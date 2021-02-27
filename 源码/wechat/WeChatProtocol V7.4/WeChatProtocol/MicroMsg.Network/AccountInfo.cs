namespace MicroMsg.Network
{
    using System;

    public class AccountInfo
    {
        private const int INVALID_UIN = -2;

        private string nickname;
        private string headimg;

        private int mBindUIN;
        private byte[] mCookie;
        private string mNickName;
        private string autoAuthKey;
        private string mPassword;
        private string mPassword2;
        private int mUIN;
        private string mUserName;
        public byte[] SessionKey=new byte[0];

        public string Nickname
        {
            get
            {
                return nickname;
            }

            set
            {
                nickname = value;
            }
        }

        public string Headimg
        {
            get
            {
                return headimg;
            }

            set
            {
                headimg = value;
            }
        }

        public string AutoAuthKey { get => autoAuthKey; set => autoAuthKey = value; }

        public AccountInfo()
        {
            this.reset();
        }

        public int getBindUin()
        {
            return this.mBindUIN;
        }

        public byte[] getCookie()
        {
            return this.mCookie;
        }

        public string getNickname()
        {
            return this.mNickName;
        }

        public string getPassword()
        {
            return this.mPassword;
        }

        public string getPassword2()
        {
            return this.mPassword2;
        }

        public int getUin()
        {
            return this.mUIN;
        }

        public string getUsername()
        {
            return this.mUserName;
        }

        public bool isValid()
        {
            if ((this.mUserName == null) || (this.mUserName.Length == 0))
            {
                return false;
            }
            if (this.mUIN == 0)
            {
                return false;
            }
            return true;
        }

        public void reset()
        {
            this.mUserName = null;
            this.mPassword = null;
            this.mPassword2 = null;
            this.mCookie = new byte[0];
            this.mUIN = 0;
        }

        public void setAuthInfo(string username, string password, string password2)
        {
            this.mUserName = username;
            this.mPassword = password;
            this.mPassword2 = password2;
        }

        public void setBindUin(int uin)
        {
            this.mBindUIN = uin;
        }

        public void setCookie(byte[] cookie)
        {
            this.mCookie = cookie;
        }

        public void setNickname(string name)
        {
            this.mNickName = name;
        }

        public void setPassword(string pwd)
        {
            this.mPassword = pwd;
        }

        public void setPassword2(string pwd)
        {
            this.mPassword2 = pwd;
        }

        public void setUin(int uin)
        {
            this.mUIN = uin;
        }

        public void setUsername(string name)
        {
            this.mUserName = name;
        }
    }
}

