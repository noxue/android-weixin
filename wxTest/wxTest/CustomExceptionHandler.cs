using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 微信挂机
{
    class CustomExceptionHandler
    {
        // Handles the exception event.
        public void OnThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = this.ShowThreadExceptionDialog(t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("致命错误", "致命错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
            Log(t.Exception.Message);
            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Creates the error message and displays it.
        private DialogResult ShowThreadExceptionDialog(Exception e)
        {
            string errorMsg = "发生错误，请与我们联系以下信息给管理员:\r\n";
            errorMsg = errorMsg + e.Message + "堆栈跟踪:\r\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, "应用程序错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "发生错误，请与我们联系。将以下信息给管理员：\r\n";//" + DateTime.Now.ToString() + "
            if (error != null)
            {
                str = string.Format(strDateInfo + "{0};\r\n堆栈信息:\r\n{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("{0}", e);
            }
            Log(str);
            MessageBox.Show(str, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //记录日志
        public void Log(string log)
        {
            StreamWriter fileA = null;
            try
            {
                if (log == "") return;
                fileA = new StreamWriter(Application.StartupPath + "\\log_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".txt", true, Encoding.Default);//写入
                fileA.Write(log, false);

            }
            catch //(Exception)
            {

                //throw;
            }
            finally
            {
                if (fileA != null)
                {
                    fileA.Close();
                    fileA.Dispose();
                }
            }

        }
    }
}
