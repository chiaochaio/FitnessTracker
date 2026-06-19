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
        private ComboBox cmbTimeRange;
        private OxyPlot.WindowsForms.PlotView chartTrends;
        private OxyPlot.WindowsForms.PlotView chartCalories;
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
            this.KeyPreview = true;
            currentUser = account;
            this.Text = $"運動飲食小助手 - 目前使用者: {currentUser}";
            cmbQuickFood.Items.Clear();
            cmbQuickFood.Items.Add("--請選擇常用食物--"); // 放一個提示當第一項
            foreach (var foodName in quickFoodPreset.Keys)
            {
                cmbQuickFood.Items.Add(foodName);
            }
            cmbTimeRange= comboBox1;
            chartTrends = new OxyPlot.WindowsForms.PlotView();
            chartTrends.Location = new Point(10, 45); // 讓開上方的 ComboBox 位置
            chartTrends.Size = new Size(panelChartContainer.Width - 20, panelChartContainer.Height - 60);
            chartTrends.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; // 自動伸縮
            panelChartContainer.Controls.Add(chartTrends); // 把圖表塞進箱子

            chartCalories = new OxyPlot.WindowsForms.PlotView();
            chartCalories.Location = new Point(10, 45); // 位置跟第一個對齊
            chartCalories.Size = new Size(panelChartContainer2.Width - 20, panelChartContainer2.Height - 60);
            chartCalories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChartContainer2.Controls.Add(chartCalories); // 🚀 塞進第二個新箱子！

            cmbTimeRange.Items.Clear();
            cmbTimeRange.Items.AddRange(new string[] { "最近一週 (7天趨勢)", "最近一個月 (30天趨勢)", "最近一年 (按月平均)" });
            cmbTimeRange.SelectedIndex = 0;
            cmbQuickFood.SelectedIndex = 0;
            
            LoadDataByDate();
        }

        public class DietItem
        {
            public int Id { get; set; }
            public string FoodName { get; set; }
            public int Calories { get; set; }
            public double Protein { get; set; }
        }

        public class ExerciseItem
        {
            public int Id { get; set; }
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
        public struct FoodInfo
        {
            public int Calories;
            public double Protein;
            public FoodInfo(int cal, double pro) { Calories = cal; Protein = pro; }
        }

        // 建立常用食物字典
        private Dictionary<string, FoodInfo> quickFoodPreset = new Dictionary<string, FoodInfo>()
{
          { "白飯 (一碗)", new FoodInfo(280, 6.0) },
          { "水煮雞胸肉 (100g)", new FoodInfo(165, 31.0) },
          { "茶葉蛋 (一顆)", new FoodInfo(75, 7.0) },
          { "無糖豆漿 (400ml)", new FoodInfo(130, 13.0) },
          { "香蕉 (一根)", new FoodInfo(90, 1.1) },
          { "香煎鮭魚 (100g)", new FoodInfo(200, 20.0) },
          { "地瓜 (中一條)", new FoodInfo(140, 1.5) }
};

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

            // 準備好用來承接的局部變數
            userGender = "男";
            userHeight = 170;
            userAge = 20;
            userActivityIndex = 0;
            double todayWeight = 50;

            // 💡【新核心邏輯】：先去 DailyLogs 查這天有沒有儲存過歷史體重指標
            bool hasDayHistory = DatabaseHelper.GetDailyLog(currentUser, dateStr, ref todayWeight, ref userGender, ref userHeight, ref userAge, ref userActivityIndex);

            if (hasDayHistory)
            {
                // 💾【歷史情境】：這一天以前按過更新！直接採用當天寫死在 DailyLogs 的數值
                txtWeight.Text = todayWeight.ToString();
            }
            else
            {
                // 🌟【全新一天情境】：這天沒點過！去 UserProfiles 拿最新帳號總檔來繼承
                DatabaseHelper.LoadUserProfile(currentUser, ref userGender, ref userHeight, ref userAge, ref userActivityIndex, ref todayWeight);
                txtWeight.Text = todayWeight.ToString();
            }

            // 建立本日核心物件，飲食運動清單一樣百分之百走資料庫 Get Logs！
            currentDailyLog = new DailyLog
            {
                DateString = dateStr,
                CurrentWeight = todayWeight,
                Gender = userGender,
                Height = userHeight,
                Age = userAge,
                ActivityIndex = userActivityIndex,
                Diets = DatabaseHelper.GetDietLogs(currentUser, dateStr),
                Exercises = DatabaseHelper.GetExerciseLogs(currentUser, dateStr)
            };

            // 把指標倒回畫面控制項
            txtHeight.Text = userHeight.ToString();
            txtAge.Text = userAge.ToString();

            userGender = userGender.Trim();
            if (userGender == "女") cmbGender.SelectedIndex = 1;
            else cmbGender.SelectedIndex = 0;

            if (userActivityIndex >= 0 && userActivityIndex < 5) cmbActivity.SelectedIndex = userActivityIndex;
            else cmbActivity.SelectedIndex = 0;

            isLoadingData = false;
            RefreshGrids();
            UpdateChartVisuals();
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

        private void LoadExerciseLogs()
        {
            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");
            // 重新去資料庫撈當天最新的運動清單
            currentDailyLog.Exercises = DatabaseHelper.GetExerciseLogs(currentUser, dateStr);

            // 重新綁定 DataSource
            dgvExercise.DataSource = null;
            dgvExercise.DataSource = currentDailyLog.Exercises;
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
            // 如果使用者正在讀取資料（isLoadingData 擋箭牌為 true），或者選到預設提示字，就先別動
            if (isLoadingData || cmbQuickFood.SelectedIndex <= 0) return;

            string selectedFood = cmbQuickFood.SelectedItem.ToString();

            // 從字典裡精準撈出該食物的營養標示，直接填入對應的文字框中！
            if (quickFoodPreset.ContainsKey(selectedFood))
            {
                txtFoodName.Text = selectedFood; // 填入食物名稱
                txtFoodCal.Text = quickFoodPreset[selectedFood].Calories.ToString(); // 填入熱量
                txtFoodProtein.Text = quickFoodPreset[selectedFood].Protein.ToString(); // 填入蛋白質
            }
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
            if (string.IsNullOrEmpty(txtFoodName.Text) || !int.TryParse(txtFoodCal.Text, out int cal))
            {
                MessageBox.Show("請輸入正確的食物名稱與熱量！", "提示");
                return;
            }
            double.TryParse(txtFoodProtein.Text, out double protein);
            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");

            // 💡 1. 寫入 DietLogs 資料表 (講義第34頁)
            DatabaseHelper.AddLogItem("DietLogs", currentUser, dateStr, "FoodName", txtFoodName.Text.Trim(), cal, protein);

            // 2. 重新加載並刷畫面
            LoadDataByDate();

            txtFoodName.Clear();
            txtFoodCal.Clear();
            txtFoodProtein.Clear();
            cmbQuickFood.SelectedIndex = 0;
        }

        private void btnAddExercise_Click(object sender, EventArgs e)
        {
            // 💡 1. 檢查運動名稱（請對齊你畫面上的 TextBox 名稱，假設是 txtSportName）
            if (string.IsNullOrEmpty(txtExerciseName.Text.Trim()))
            {
                MessageBox.Show("請輸入運動項目名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 💡 2. 檢查運動時間與消耗卡路里（請對齊你的 TextBox 名稱，例如 txtSportTime, txtSportCalories）
            // 💥 關鍵錯誤：檢查時，不要用到 txtFoodCalories 或是飲食的錯誤提示！
            if (!int.TryParse(txtExerciseDuration.Text, out int minutes) || minutes <= 0)
            {
                MessageBox.Show("請輸入正確的運動時間（分鐘）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtExerciseBurn.Text, out int calories) || calories <= 0)
            {
                // 🚀 【就是這裡錯了！】請確保你的提示訊息是寫「消耗卡路里」，而不是「食物熱量」
                MessageBox.Show("請輸入正確的運動消耗卡路里！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 💡 3. 呼叫資料庫新增運動紀錄（TableName 記得要傳對）
            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");

            // 呼叫你的 AddLogItem 方法，寫入 ExerciseLogs 表
            DatabaseHelper.AddLogItem("ExerciseLogs", currentUser, dateStr, "ActivityName", txtExerciseName.Text.Trim(), minutes, calories);

            MessageBox.Show("運動紀錄新增成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 💡 4. 清空輸入框並重新整理運動清單
            txtExerciseName.Clear();
            txtExerciseDuration.Clear();
            txtExerciseBurn.Clear();

            LoadExerciseLogs();     // 重新整理 dgvExercise 表格
            RefreshGrids();        // 重新整理今日總卡路里跟進度條
            UpdateChartVisuals();  // 同步更新統計圖表
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
            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");

            // 2. 💡【鎖定當天歷史 -> 改寫入 DailyLogs 資料表！】
            // 這樣做就能把這一天的歷史數據用 SQL 永久封印，再也不需要 SaveDataToFile(); 
            DatabaseHelper.SaveDailyLog(currentUser, dateStr, w, gender, h, age, activityIndex);

            // 同步把數據填進當前記憶體物件，確保當天飲食運動運算不會斷軌
            currentDailyLog.CurrentWeight = w;
            currentDailyLog.Gender = gender;
            currentDailyLog.Height = h;
            currentDailyLog.Age = age;
            currentDailyLog.ActivityIndex = activityIndex;

            // 3. 🚀【更新未來繼承】：維持原樣寫入大師資料庫，讓未來完全空白的新日子去承接
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
            if (dgvDiet.CurrentRow != null && dgvDiet.CurrentRow.Index >= 0)
            {
                int selectedIndex = dgvDiet.CurrentRow.Index;
                if (selectedIndex < currentDailyLog.Diets.Count)
                {
                    DialogResult result = MessageBox.Show("確定要刪除這筆飲食紀錄嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // 💡 拿著這筆紀錄在資料庫裡的真正主鍵 Id 去執行 SQL 刪除！(講義第35頁)
                        int dbId = currentDailyLog.Diets[selectedIndex].Id;
                        DatabaseHelper.DeleteLogItem("DietLogs", dbId);

                        LoadDataByDate();
                        UpdateChartVisuals();
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
            if (dgvExercise.CurrentRow != null && dgvExercise.CurrentRow.Index >= 0)
            {
                int selectedIndex = dgvExercise.CurrentRow.Index;
                if (selectedIndex < currentDailyLog.Exercises.Count)
                {
                    DialogResult result = MessageBox.Show("確定要刪除這筆運動紀錄嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // 💡 同理，拿著 Id 刪除運動
                        int dbId = currentDailyLog.Exercises[selectedIndex].Id;
                        DatabaseHelper.DeleteLogItem("ExerciseLogs", dbId);

                        LoadDataByDate();
                        MessageBox.Show("運動紀錄已刪除！", "成功");
                    }
                }
            }
            else
            {
                MessageBox.Show("請先在表格中點選一筆想要刪除的運動紀錄！", "提示");
            }
        }

        private void UpdateChartVisuals()
        {
            if (chartTrends == null || chartCalories == null || cmbTimeRange == null) return;

            string rangeType = "week";
            if (cmbTimeRange.SelectedIndex == 1) rangeType = "month";
            else if (cmbTimeRange.SelectedIndex == 2) rangeType = "year";

            // 1. 從資料庫撈取聯查數據
            List<DatabaseHelper.ChartDataPoint> dataPoints = DatabaseHelper.GetChartData(currentUser, rangeType);

            // ----------------------------------------------------
            // 📊 【圖表一：消耗卡路里趨勢圖】 (原本的 chartTrends 箱子)
            // ----------------------------------------------------
            var modelBurn = new OxyPlot.PlotModel { Title = "每日運動消耗卡路里 (kcal)" };

            var seriesBurn = new OxyPlot.Series.LineSeries
            {
                Title = "消耗熱量 (kcal)",
                Color = OxyPlot.OxyColors.DodgerBlue, // 用藍色/青色線條代表運動流汗消耗
                StrokeThickness = 3,
                MarkerType = OxyPlot.MarkerType.Circle,
                MarkerSize = 5,
                MarkerFill = OxyPlot.OxyColors.White
            };

            var yAxisBurn = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = "消耗熱量 (kcal)", Minimum = 0 };
            var xAxisBurn = new OxyPlot.Axes.CategoryAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Title = "日期" };
            modelBurn.Axes.Add(yAxisBurn);
            modelBurn.Axes.Add(xAxisBurn);

            // ----------------------------------------------------
            // 📊 【圖表二：攝取熱量趨勢圖】 (新 panelChartContainer2 箱子)
            // ----------------------------------------------------
            var modelCalories = new OxyPlot.PlotModel { Title = "每日飲食攝取熱量 (kcal)" };

            var seriesCal = new OxyPlot.Series.LineSeries
            {
                Title = "攝取熱量 (kcal)",
                Color = OxyPlot.OxyColors.Red, // 用紅色線條代表飲食熱量進帳
                StrokeThickness = 3,
                MarkerType = OxyPlot.MarkerType.Circle,
                MarkerSize = 5,
                MarkerFill = OxyPlot.OxyColors.White
            };

            var yAxisCal = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = "攝取熱量 (kcal)", Minimum = 0 };
            var xAxisCal = new OxyPlot.Axes.CategoryAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Title = "日期" };
            modelCalories.Axes.Add(yAxisCal);
            modelCalories.Axes.Add(xAxisCal);

            // ----------------------------------------------------
            // 🚀 3. 將數據分別灌入兩條卡路里折線中
            // ----------------------------------------------------
            for (int i = 0; i < dataPoints.Count; i++)
            {
                string dateLabel = dataPoints[i].Label;

                // 灌入圖表一：運動消耗
                xAxisBurn.Labels.Add(dateLabel);
                seriesBurn.Points.Add(new OxyPlot.DataPoint(i, dataPoints[i].TotalBurnCalories));

                // 灌入圖表二：飲食攝取
                xAxisCal.Labels.Add(dateLabel);
                seriesCal.Points.Add(new OxyPlot.DataPoint(i, dataPoints[i].TotalCalories));
            }

            // 4. 把折線裝上各自的畫布
            modelBurn.Series.Add(seriesBurn);
            modelCalories.Series.Add(seriesCal);

            // 5. 交付給兩個 Panel 控制項渲染
            chartTrends.Model = modelBurn;      // 藍色：運動消耗
            chartCalories.Model = modelCalories; // 紅色：飲食攝取
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateChartVisuals();
        }
        private void fitnesstracker_KeyDown(object sender, KeyEventArgs e)
        {
            // 💡 防呆機制：如果使用者正在文字框裡「輸入食物名稱」或「打字」，就不要觸發快捷鍵切換分頁
            if (txtFoodName.Focused || txtFoodCal.Focused || txtFoodProtein.Focused ||
                txtWeight.Focused || txtHeight.Focused || txtAge.Focused)
            {
                return;
            }

            // 🚀 當使用者按下 Alt + 指定英文字母時，切換對應的分頁索引 (SelectedIndex)
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.H: // Alt + H -> 今日狀態 (第 0 頁)
                        tabControl1.SelectedIndex = 0;
                        e.Handled = true; // 告訴系統這個按鍵我們處理完了，不用再往外傳
                        break;

                    case Keys.E: // Alt + E -> 飲食紀錄 (第 1 頁)
                        tabControl1.SelectedIndex = 1;
                        e.Handled = true;
                        break;

                    case Keys.S: // Alt + S -> 運動紀錄 (第 2 頁)
                        tabControl1.SelectedIndex = 2;
                        e.Handled = true;
                        break;

                    case Keys.C: // Alt + C -> 統計圖表 (第 3 頁)
                        tabControl1.SelectedIndex = 3;
                        e.Handled = true;
                        break;
                }
            }
        }

        private void pbCalories_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void fitnesstracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 跳出確認對話框
            DialogResult result = MessageBox.Show("確定要關閉運動飲食小助手嗎？", "關閉程式", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 如果使用者點擊了「否 (No)」
            if (result == DialogResult.No)
            {
                e.Cancel = true; // 攔截並取消關閉視窗的動作，讓使用者留在主畫面
            }
            else
            {
                // 如果點擊「是」，確保整支程式（包含背景所有資源）完全關閉乾淨
                Application.Exit();
            }
        }
    }
}
