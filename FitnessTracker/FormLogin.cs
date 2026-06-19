using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace FitnessTracker
{
    public partial class FormLogin : Form
    {
        // 用來存放帳密對應的 Dictionary(Key: 帳號, Value: 密碼)
        private Dictionary<string, string> userDatabase = new Dictionary<string, string>();
        private string userFilePath = Path.Combine(Application.StartupPath, "users.json");
        public bool IsLoginSuccess { get; private set; } = false;
        public string LoggedInAccount { get; private set; } = "";
        public FormLogin()
        {
            InitializeComponent();
            LoadUsers(); // 視窗開起時，先載入已經註冊過的帳密
        }

        private void LoadUsers()
        {
            if (File.Exists(userFilePath))
            {
                try
                {
                    string json = File.ReadAllText(userFilePath);
                    userDatabase = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }
                catch { userDatabase = new Dictionary<string, string>(); }
            }
            else
            {
                // 預設一組你的學號當作初始帳密，以防一開始沒帳號
                userDatabase["s1131418"] = "123456";
            }
        }

        // 儲存新帳號到檔案
        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(userDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userFilePath, json);
        }

        // TODO: 這裡之後會用來寫「登入」與「建立帳戶」按鈕的 Click 事件
    

       private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = txtAccount.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 💡 修正 1：把條件從 6 改成 7！這樣輸入 7 位數才不會被誤攔截！
            if (account.Length != 7 || !IsAllDigits(account) || !IsAllDigits(password))
            {
                MessageBox.Show("帳號或密碼格式不正確！\n帳號必須為 7 位數字，密碼必須為純數字。", "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 💡 修正 2：徹底拋棄 userDatabase 字典，全面改呼叫 LocalDB 的驗證方法！
            if (DatabaseHelper.VerifyUser(account, password))
            {
                MessageBox.Show("登入成功！", "歡迎", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 標記狀態為成功，並記住帳號
                IsLoginSuccess = true;
                LoggedInAccount = account;

                // 直接關閉登入視窗，此時 Program.cs 就會接到通知並開啟主畫面
                this.Close();
            }
            else
            {
                MessageBox.Show("帳號或密碼錯誤！", "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string account = txtAccount.Text.Trim();
            string password = txtPassword.Text;

            // 💡 【必須放在最前面！】先檢查格式，只要不符合 7 位數字或密碼不是數字，立刻打槍，絕對不准驚動資料庫！
            if (account.Length != 7 || !IsAllDigits(account))
            {
                MessageBox.Show("帳號格式錯誤！必須是剛好 7 位數的純數字（例如學號）。", "註冊失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password) || !IsAllDigits(password))
            {
                MessageBox.Show("密碼格式錯誤！請設定純數字密碼。", "註冊失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 💡 【格式完全正確了，才允許執行寫入！】
            if (DatabaseHelper.RegisterUser(account, password))
            {
                MessageBox.Show("註冊成功！請使用新帳號登入。", "成功");
            }
            else
            {
                MessageBox.Show("註冊失敗，此帳號已被註冊！", "失敗");
            }
        }
        private bool IsAllDigits(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            foreach (char c in str)
            {
                if (c < '0' || c > '9') return false;
            }
            return true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtAccount.Focus(); // 讓使用者連滑鼠都不用碰，開機直接打字！
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            // 當使用者在帳號欄位按下 Enter 鍵
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;         // 攔截事件，防止系統發出「逼」的警告音
                e.SuppressKeyPress = true; // 隱藏按鍵字元
                txtPassword.Focus();       // 自動將焦點跳到密碼輸入框！
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // 當使用者在密碼欄位按下 Enter 鍵
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;         // 攔截事件
                e.SuppressKeyPress = true;
                btnLogin_Click(this, EventArgs.Empty); // 🚀 直接觸發登入按鈕的點擊事件！
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 💡 關鍵防呆：如果是登入成功後的關閉（我們前面寫的 this.Close()），就直接放行，不用再確認！
            if (this.IsLoginSuccess)
            {
                return;
            }

            // 跳出確認對話框
            DialogResult result = MessageBox.Show("確定要退出系統嗎？", "結束程式", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 如果使用者點擊了「否 (No)」
            if (result == DialogResult.No)
            {
                e.Cancel = true; // 核心：這行可以把「關閉視窗」的動作攔截並取消掉！
            }
            else
            {
                // 如果點擊「是」，因為這是登入視窗，直接關閉代表使用者想完全結束程式
                Application.Exit();
            }
        }
    }
 }



