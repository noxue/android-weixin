using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 微信挂机
{
    public class MyIni
    {
        /// <summary>
        /// Win32 API函数，在初始化文件指定小节内设置一个字串。
        /// </summary>
        /// <param name="section">要在其中写入新字串的小节名称。这个字串不区分大小写。</param>
        /// <param name="key">要设置的项名或条目名。这个字串不区分大小写。用Null可删除这个小节的所有设置项。</param>
        /// <param name="val">指定为这个项写入的字串值。用Null表示删除这个项现有的字串。</param>
        /// <param name="filePath">初始化文件的名字。如果没有指定完整路径名，则windows会在windows目录查找文件。如果文件没有找到，则函数会创建它。</param>
        /// <returns>Long，非零表示成功，零表示失败。</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,
            string val,
            string filePath);
        /// <summary>
        /// Win32 API函数，为初始化文件中指定的条目取得字串。
        /// </summary>
        /// <param name="section">欲在其中查找条目的小节名称。这个字串不区分大小写。如设为Null，就在retVal缓冲区内装载这个ini文件所有小节的列表。</param>
        /// <param name="key">欲获取的项名或条目名。这个字串不区分大小写。如设为Null，就在retVal缓冲区内装载指定小节所有项的列表。</param>
        /// <param name="def">指定的条目没有找到时返回的默认值。可设为空（""）。</param>
        /// <param name="retVal">指定一个字串缓冲区，长度至少为Size。</param>
        /// <param name="size">指定装载到retVal缓冲区的最大字符数量。</param>
        /// <param name="filePath">初始化文件的名字。如没有指定一个完整路径名，windows就在Windows目录中查找文件。</param>
        /// <returns>int，复制到retVal缓冲区的字节数量，其中不包括那些NULL中止字符。如retVal缓冲区不够大，不能容下全部信息，就返回Size-1（若section或key为NULL，则返回Size-2）。</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);
        /// <summary>
        /// Win32 API函数，为初始化文件中指定的条目获取一个整数值。
        /// </summary>
        /// <param name="lpApplicationName">指定在其中查找条目的小节。注意这个字串是不区分大小写的。</param>
        /// <param name="lpKeyName">欲获取的设置项或条目。这个支持不区分大小写。</param>
        /// <param name="nDefault">指定条目未找到时返回的默认值。</param>
        /// <param name="lpFileName">初始化文件的名字。如果没有指定完整的路径名，windows就会在Windows目录中搜索文件</param>
        /// <returns>
        /// int，找到的条目的值；如指定的条目未找到，就返回默认值。如找到的数字不是一个合法的整数，
        /// 函数会返回其中合法的一部分。如，对于“xyz=55zz”这个条目，函数返回55。
        ///  这个函数也能理解采用标准C语言格式的十六进制数字：用0x作为一个十六进制数字的前缀——例：0x55ab。
        /// </returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string lpApplicationName,
            string lpKeyName,
            int nDefault,
            string lpFileName
            );
        /// <summary>
        /// 文件路径。
        /// </summary>
        private string FilePath;
        /// <summary>
        /// 标准构造函数。
        /// </summary>
        public MyIni()
        {
            //标准构造函数。
        }
        /// <summary>
        /// 构造函数重载版本。
        /// </summary>
        /// <param name="_FilePath">ini文件地址。</param>
        public MyIni(string _FilePath)
        {
            this.FilePath = _FilePath;
        }
        /// <summary>
        /// 获取或设置ini文件的路径及文件名。
        /// </summary>
        public string IniFilePath
        {
            set
            {
                this.FilePath = value;
            }
            get
            {
                return this.FilePath;
            }
        }
        /// <summary>
        /// 写入ini文件。
        /// </summary>
        /// <param name="Section">要在其中写入新字串的小节名称。这个字串不区分大小写。</param>
        /// <param name="key">要设置的项名或条目名。这个字串不区分大小写。用Null可删除这个小节的所有设置项。</param>
        /// <param name="val">指定为这个项写入的字串值。用Null表示删除这个项现有的字串。</param>
        /// <returns>非零表示写入成功。</returns>
        public int WriteIniString(string Section, string key, string val)
        {
            if (IniFilePath.Length <= 0)
            {
                throw new Exception("没有指定ini文件路径。");
            }
            try
            {
                if (WritePrivateProfileString(Section, key, val, FilePath) == 0)
                {
                    return 0;
                }
                return 1;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 获取指定小节的字符串。
        /// </summary>
        /// <param name="Section">要在其中读取字串的小节名称。这个字串不区分大小写。</param>
        /// <param name="key">欲获取的设置项或条目。这个支持不区分大小写。</param>
        /// <param name="def">指定的条目没有找到时返回的默认值。可设为空（""）。</param>
        /// <param name="size">指定装载到缓冲区的最大字符数量。</param>
        /// <returns></returns>
        public string GetIniString(string Section, string key, string def, int size)
        {
            StringBuilder retVal = new StringBuilder(size);
            if (IniFilePath.Length <= 0)
            {
                throw new Exception("没有指定ini文件路径。");
            }
            try
            {
                GetPrivateProfileString(Section, key, def, retVal, size, FilePath);
                return retVal.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 获取指定小节的数字。
        /// </summary>
        /// <param name="Section">指定在其中查找条目的小节。注意这个字串是不区分大小写的。</param>
        /// <param name="key">>欲获取的设置项或条目。这个支持不区分大小写。</param>
        /// <param name="nDefault">指定条目未找到时返回的默认值。</param>
        /// <returns>找到的条目的值；如指定的条目未找到，就返回默认值。</returns>
        public int GetIniInt(string Section, string key, long nDefault)
        {
            int Re;
            if (IniFilePath.Length <= 0)
            {
                throw new Exception("没有指定ini文件路径。");
            }
            try
            {
                Re = GetPrivateProfileInt(Section, key, 0, FilePath);
                if (Re == 0)
                {
                    Re = GetPrivateProfileInt(Section, key, 1, FilePath);
                    if (Re == 1)
                    {
                        return (int)nDefault;
                    }
                }
                return Re;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
