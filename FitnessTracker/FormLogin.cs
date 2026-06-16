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

            if (userDatabase.ContainsKey(account) && userDatabase[account] == password)
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
            string password = txtPassword.Text.Trim();

            // 基本檢查
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("請輸入完整的帳號與密碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 檢查帳號是否重複
            if (userDatabase.ContainsKey(account))
            {
                MessageBox.Show("此帳號已被註冊！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // 加入資料庫並存檔
                userDatabase[account] = password;
                SaveUsers();
                MessageBox.Show("帳戶建立成功！可以使用此帳號登入了。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
 }



