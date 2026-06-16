using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTracker
{
    public partial class fitnesstracker : Form
    {
        private string currentUser;
        private DailyLog currentDailyLog; // 記錄當天所有的飲食與運動資料物件
        private UserProfile currentUserProfile; // 存放最新個人基本資料
        public fitnesstracker(string account)
        {
            InitializeComponent();
            currentUser = account;
            this.Text = $"運動飲食小助手 - 目前使用者: {currentUser}";
        }

        public class DietItem
        {
            public string FoodName { get; set; }
            public int Calories { get; set; }
            public double Protein { get; set; }
        }

        public class ExerciseItem
        {
            public string ActivityName { get; set; }
            public int DurationMinutes { get; set; }
            public int BurnedCalories { get; set; }
        }

        public class DailyLog
        {
            public string DateString { get; set; }
            public double CurrentWeight { get; set; }
            public List<DietItem> Diets { get; set; } = new List<DietItem>();
            public List<ExerciseItem> Exercises { get; set; } = new List<ExerciseItem>();
        }
        public class UserProfile
        {
            public string Gender { get; set; } = "男";
            public double Height { get; set; } = 170; // 預設值
            public int Age { get; set; } = 20;
            public int ActivityIndex { get; set; } = 0; // 預設選第一個靜態生活
            public double LastWeight { get; set; } = 0;
        }

        private void fitnesstracker_Load(object sender, EventArgs e)
        {
            // 1. 初始化 DataGridView 的外觀（讓牠自動伸縮填滿）
            dgvDiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExercise.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. 預設載入「今天」的資料
            LoadDataByDate();
        }

        // 核心讀檔功能：依據 dtpDate 選擇的日期讀取 JSON
        private void LoadDataByDate()
        {
            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");
            string userFolder = Path.Combine(Application.StartupPath, "Data", currentUser);
            if (!Directory.Exists(userFolder)) Directory.CreateDirectory(userFolder);

            // 【1. 讀取個人基本設定檔（延續最新數據，不用天天輸入）】
            string profilePath = Path.Combine(userFolder, "user_profile.json");
            if (File.Exists(profilePath))
            {
                string profileJson = File.ReadAllText(profilePath);
                currentUserProfile = JsonSerializer.Deserialize<UserProfile>(profileJson);
            }
            else
            {
                currentUserProfile = new UserProfile(); // 第一次登入給空白預設
            }

            // 將基本資料填入 UI 畫面
            cmbGender.SelectedItem = currentUserProfile.Gender ?? "男";
            txtHeight.Text = currentUserProfile.Height.ToString();
            txtAge.Text = currentUserProfile.Age.ToString();
            cmbActivity.SelectedIndex = currentUserProfile.ActivityIndex;

            // 【2. 讀取當天飲食運動紀錄】
            string filePath = Path.Combine(userFolder, $"{dateStr}.json");
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                currentDailyLog = JsonSerializer.Deserialize<DailyLog>(jsonString);
            }
            else
            {
                // 如果這天還沒有紀錄，體重自動延續昨天的，或者顯示 0 讓使用者填
                currentDailyLog = new DailyLog
                {
                    DateString = dateStr,
                    CurrentWeight = currentUserProfile.LastWeight // 讓新的一天自動繼承舊體重
                };
            }

            // 將當天體重填入輸入框
            txtWeight.Text = currentDailyLog.CurrentWeight.ToString();

            RefreshGrids();
        }

        // 核心存檔功能：將目前的 currentDailyLog 寫入該帳號資料夾
        private void SaveDataToFile()
        {
            string userFolder = Path.Combine(Application.StartupPath, "Data", currentUser);
            string filePath = Path.Combine(userFolder, $"{currentDailyLog.DateString}.json");

            // 【存檔】轉成漂亮的 JSON 格式寫入
            string jsonString = JsonSerializer.Serialize(currentDailyLog, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        // 重新整理畫面的表格與統計數據
        private void RefreshGrids()
        {
            dgvDiet.DataSource = null;
            dgvExercise.DataSource = null;
            dgvDiet.DataSource = currentDailyLog.Diets;
            dgvExercise.DataSource = currentDailyLog.Exercises;

            // 1. 計算今日攝取與消耗
            int totalEat = 0;
            foreach (var item in currentDailyLog.Diets) totalEat += item.Calories;

            int totalBurn = 0;
            foreach (var item in currentDailyLog.Exercises) totalBurn += item.BurnedCalories;

            // 2. 依據公式計算 BMR (以男生公式為範例， s1131418 )
            // 公式：66 + (13.7 x 體重) + (5 x 身高) - (6.8 x 年齡)
            double weight = currentDailyLog.CurrentWeight;
            double height = currentUserProfile.Height;
            int age = currentUserProfile.Age;
            string gender = cmbGender.SelectedItem?.ToString() ?? "男";

            double bmr = 0;
            double tdee = 0;

            if (weight > 0 && height > 0 && age > 0)
            {
                if (gender == "男")
                {
                    // 男生公式：66 + (13.7 x 體重) + (5 x 身高) - (6.8 x 年齡)
                    bmr = 66 + (13.7 * weight) + (5 * height) - (6.8 * age);
                }
                else
                {
                    // 女生公式：655 + (9.6 x 體重) + (1.8 x 身高) - (4.7 x 年齡)
                    bmr = 655 + (9.6 * weight) + (1.8 * height) - (4.7 * age);
                }

                // 3. 根據活動量等級乘以係數
                double[] activityFactors = { 1.2, 1.375, 1.55, 1.725, 1.9 };
                int factorIndex = cmbActivity.SelectedIndex >= 0 ? cmbActivity.SelectedIndex : 0;
                tdee = bmr * activityFactors[factorIndex];
            }
            int targetCalories = tdee > 0 ? (int)(tdee * 0.85) : 2000; // 如果沒資料，預設 2000

            int remainingCalories = targetCalories - totalEat + totalBurn;

            // 5. 更新 UI 控制項
            lblCalProgress.Text = $"今日已攝取: {totalEat} kcal / 已消耗: {totalBurn} kcal";
            lblCalStatus.Text = $"今日目標建議攝取: {targetCalories} kcal\n剩餘可攝取熱量: {remainingCalories} kcal";

            // 6. 與 ProgressBar 進度條連動 (防呆：避免超過 100% 崩潰)
            pbCalories.Minimum = 0;
            pbCalories.Maximum = targetCalories;
            if (totalEat <= targetCalories)
            {
                pbCalories.Value = totalEat;
            }
            else
            {
                pbCalories.Value = pbCalories.Maximum; // 爆卡了就停在滿條
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadDataByDate(); // 使用者切換日期，就自動重新讀檔！
        }

        private void btnAddDiet_Click(object sender, EventArgs e)
        {
            // 檢查防呆
            if (string.IsNullOrEmpty(txtFoodName.Text) || !int.TryParse(txtFoodCal.Text, out int cal))
            {
                MessageBox.Show("請輸入正確的食物名稱與熱量！", "提示");
                return;
            }

            double.TryParse(txtFoodProtein.Text, out double protein);

            // 1. 新增到當前的資料清單中
            currentDailyLog.Diets.Add(new DietItem
            {
                FoodName = txtFoodName.Text.Trim(),
                Calories = cal,
                Protein = protein
            });

            // 2. 自動存檔
            SaveDataToFile();

            // 3. 重新整理表格
            RefreshGrids();

            // 4. 清空輸入欄位方便下一筆輸入
            txtFoodName.Clear();
            txtFoodCal.Clear();
            txtFoodProtein.Clear();
        }

        private void btnAddExercise_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExerciseName.Text) ||
        !int.TryParse(txtExerciseDuration.Text, out int duration) ||
        !int.TryParse(txtExerciseBurn.Text, out int burn))
            {
                MessageBox.Show("請輸入正確的運動名稱、時間與消耗熱量！", "提示");
                return;
            }

            // 1. 新增到當前的運動清單中
            currentDailyLog.Exercises.Add(new ExerciseItem
            {
                ActivityName = txtExerciseName.Text.Trim(),
                DurationMinutes = duration,
                BurnedCalories = burn
            });

            // 2. 自動存檔
            SaveDataToFile();

            // 3. 重新整理表格
            RefreshGrids();

            // 4. 清空輸入欄位
            txtExerciseName.Clear();
            txtExerciseDuration.Clear();
            txtExerciseBurn.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveWeight_Click(object sender, EventArgs e)
        {
            // 驗證並撈取輸入
            if (!double.TryParse(txtWeight.Text, out double w) ||
                !double.TryParse(txtHeight.Text, out double h) ||
                !int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("請輸入正確的身高、體重與年齡數字！", "提示");
                return;
            }

            // 1. 更新並儲存當天的體重
            currentDailyLog.CurrentWeight = w;
            SaveDataToFile(); // 儲存 yyyy-MM-dd.json

            // 2. 更新並儲存長期的個人基本資料（身高、年齡、活動量）
            currentUserProfile.Gender = cmbGender.SelectedItem?.ToString() ?? "男";
            currentUserProfile.Height = h;
            currentUserProfile.Age = age;
            currentUserProfile.ActivityIndex = cmbActivity.SelectedIndex;
            currentUserProfile.LastWeight = w;

            string userFolder = Path.Combine(Application.StartupPath, "Data", currentUser);
            string profilePath = Path.Combine(userFolder, "user_profile.json");
            string profileJson = JsonSerializer.Serialize(currentUserProfile, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, profileJson);

            // 3. 重新計算並刷畫面
            RefreshGrids();
            MessageBox.Show("個人狀態與今日熱量目標已更新！", "成功");
        }

        private void lblCalProgress_Click(object sender, EventArgs e)
        {

        }

        private void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
