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
        private bool isLoadingData = false;

        private string userGender = "男";
        private double userHeight = 170;
        private int userAge = 20;
        private int userActivityIndex = 0;
        public fitnesstracker(string account)
        {
            InitializeComponent();
            currentUser = account;
            this.Text = $"運動飲食小助手 - 目前使用者: {currentUser}";
            LoadDataByDate();
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
            public string Gender { get; set; } = "男";
            public double Height { get; set; } = 170;
            public int Age { get; set; } = 20;
            public int ActivityIndex { get; set; } = 0;
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
        }

        // 核心讀檔功能：依據 dtpDate 選擇的日期讀取 JSON
        private void LoadDataByDate()
        {
            isLoadingData = true;

            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");

            // 1. 先確認這一天以前有沒有存過 JSON 檔案
            string userFolder = Path.Combine(Application.StartupPath, "Data", currentUser);
            string filePath = Path.Combine(userFolder, $"{dateStr}.json");

            if (File.Exists(filePath))
            {
                // 💾【歷史情境】：這一天以前存過資料！
                string jsonString = File.ReadAllText(filePath);
                currentDailyLog = JsonSerializer.Deserialize<DailyLog>(jsonString);

                // 💡 歷史資料最高優先！直接從這天的檔案裡把當時的數據抓出來填畫面，絕對不被最新資料庫干擾！
                userGender = currentDailyLog.Gender ?? "男";
                userHeight = currentDailyLog.Height > 0 ? currentDailyLog.Height : 170;
                userAge = currentDailyLog.Age > 0 ? currentDailyLog.Age : 20;
                userActivityIndex = currentDailyLog.ActivityIndex;
                txtWeight.Text = currentDailyLog.CurrentWeight.ToString();
            }
            else
            {
                // 🌟【全新一天情境】：這一天完全沒點過、沒資料！
                // 💡 這時候才去資料庫拿最新的一般數據來給新的一天「繼承」！
                userGender = "男";
                userHeight = 170;
                userAge = 20;
                userActivityIndex = 0;
                double lastWeight = 50;

                // 從 LocalDB 撈出最新的那筆大師級檔案
                DatabaseHelper.LoadUserProfile(currentUser, ref userGender, ref userHeight, ref userAge, ref userActivityIndex, ref lastWeight);

                // 建立新一天的空白紀錄物件，並把繼承過來的精華數據灌進去
                currentDailyLog = new DailyLog
                {
                    DateString = dateStr,
                    CurrentWeight = lastWeight,
                    Gender = userGender,
                    Height = userHeight,
                    Age = userAge,
                    ActivityIndex = userActivityIndex
                };

                txtWeight.Text = lastWeight.ToString();
            }

            // 2. 統一將撈出來的變數（不論是歷史還是資料庫繼承來的）倒回畫面上顯示
            txtHeight.Text = userHeight.ToString();
            txtAge.Text = userAge.ToString();

            userGender = userGender.Trim();
            if (userGender == "女") cmbGender.SelectedIndex = 1;
            else cmbGender.SelectedIndex = 0;

            if (userActivityIndex >= 0 && userActivityIndex < 5) cmbActivity.SelectedIndex = userActivityIndex;
            else cmbActivity.SelectedIndex = 0;

            isLoadingData = false;

            // 3. 重新計算今日進度條與熱量
            RefreshGrids();
        }

        private void SaveDataToFile()
        {
            string userFolder = Path.Combine(Application.StartupPath, "Data", currentUser);
            string filePath = Path.Combine(userFolder, $"{currentDailyLog.DateString}.json");

            // 轉成漂亮的 JSON 格式寫入
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

            double.TryParse(txtWeight.Text, out double weight);
            double height = userHeight; // 永遠使用資料庫吐出來的真實身高！
            int age = userAge;         // 永遠使用資料庫吐出來的真實年齡！
            string gender = userGender;// 永遠使用資料庫吐出來的真實性別！

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
            // 1. 驗證並撈取輸入
            if (!double.TryParse(txtWeight.Text, out double w) ||
                !double.TryParse(txtHeight.Text, out double h) ||
                !int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("請輸入正確的身高、體重與年齡數字！", "提示");
                return;
            }

            string gender = cmbGender.SelectedItem?.ToString() ?? "男";
            int activityIndex = cmbActivity.SelectedIndex >= 0 ? cmbActivity.SelectedIndex : 0;

            // 2. 🔒【鎖定當天歷史】：把這筆設定寫入當天的物件中
            currentDailyLog.CurrentWeight = w;
            currentDailyLog.Gender = gender;
            currentDailyLog.Height = h;
            currentDailyLog.Age = age;
            currentDailyLog.ActivityIndex = activityIndex;

            // 儲存到當天的 JSON 歷史檔案中，以後點回這天，它就永遠長這樣！
            SaveDataToFile();

            // 3. 🚀【更新未來繼承】：同時把最新狀況寫入 LocalDB，給未來的全新日子使用
            DatabaseHelper.UpdateUserProfile(currentUser, gender, h, age, activityIndex, w);

            // 4. 同步更新目前主畫面的全域計算變數
            userGender = gender;
            userHeight = h;
            userAge = age;
            userActivityIndex = activityIndex;

            // 5. 刷畫面計算
            RefreshGrids();

            MessageBox.Show("個人狀態與今日資料庫熱量目標已更新！", "成功");
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

        private void txtFoodCal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDeleteDiet_Click(object sender, EventArgs e)
        {
            // 檢查使用者是否有選中 DataGridView 的任何一列
            if (dgvDiet.CurrentRow != null && dgvDiet.CurrentRow.Index >= 0)
            {
                // 取得目前選中那一列的索引值
                int selectedIndex = dgvDiet.CurrentRow.Index;

                // 防呆：確保索引值在 List 的合理範圍內
                if (selectedIndex < currentDailyLog.Diets.Count)
                {
                    // 彈出確認視窗，避免使用者不小心手殘點錯
                    DialogResult result = MessageBox.Show("確定要刪除這筆飲食紀錄嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // 1. 從核心資料清單中移除該筆項目
                        currentDailyLog.Diets.RemoveAt(selectedIndex);

                        // 2. 重新儲存到 JSON 檔案
                        SaveDataToFile();

                        // 3. 重新整理畫面表格與熱量進度條
                        RefreshGrids();

                        MessageBox.Show("飲食紀錄已刪除！", "成功");
                    }
                }
            }
            else
            {
                MessageBox.Show("請先在表格中點選一筆想要刪除的飲食紀錄！", "提示");
            }
        }

        private void btnDeleteExercise_Click(object sender, EventArgs e)
        {
            // 檢查使用者是否有選中 DataGridView 的任何一列
            if (dgvExercise.CurrentRow != null && dgvExercise.CurrentRow.Index >= 0)
            {
                int selectedIndex = dgvExercise.CurrentRow.Index;

                if (selectedIndex < currentDailyLog.Exercises.Count)
                {
                    DialogResult result = MessageBox.Show("確定要刪除這筆運動紀錄嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // 1. 從運動清單中移除
                        currentDailyLog.Exercises.RemoveAt(selectedIndex);

                        // 2. 存檔
                        SaveDataToFile();

                        // 3. 刷畫面
                        RefreshGrids();

                        MessageBox.Show("運動紀錄已刪除！", "成功");
                    }
                }
            }
            else
            {
                MessageBox.Show("請先在表格中點選一筆想要刪除的運動紀錄！", "提示");
            }
        }
    }
}
