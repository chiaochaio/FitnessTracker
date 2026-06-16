using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTracker
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormLogin login = new FormLogin();
            Application.Run(login);
            if (login.IsLoginSuccess)
            {
                // 4. 成功才真正啟動主畫面生命週期
                Application.Run(new fitnesstracker(login.LoggedInAccount));
            }
        }
    }
}
