using System;
using System.Collections.Generic;
using System.Data.SqlClient; //  換成微軟正統 SQL 工具箱 (講講義第43頁)
using System.IO;
using System.Windows.Forms;

namespace FitnessTracker
{
    public class DatabaseHelper
    {
        // 完全對齊講義第44、48頁的 DataDirectory 自動化資料庫連接字串
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFilename=|DataDirectory|\db.mdf;Integrated Security=True;";

        // 檢查帳號是否存在，並驗證密碼（登入用）
        public static bool VerifyUser(string account, string password)
        {
            bool isValid = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Password FROM Users WHERE Account = @Account";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result.ToString().Trim() == password)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        // 註冊新帳號，並初始化一筆空白的個人 Profile（註冊用）
        public static bool RegisterUser(string account, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 檢查帳號是否重複
                string checkSql = "SELECT COUNT(*) FROM Users WHERE Account = @Account";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Account", account);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0) return false;
                }

                // 使用交易機制，同時寫入兩張表
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. 寫入 Users
                        string insertUserSql = "INSERT INTO Users (Account, Password) VALUES (@Account, @Password)";
                        using (SqlCommand cmd = new SqlCommand(insertUserSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Account", account);
                            cmd.Parameters.AddWithValue("@Password", password);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. 寫入 UserProfiles
                        string insertProfileSql = "INSERT INTO UserProfiles (Account, Gender, Height, Age, ActivityIndex, LastWeight) VALUES (@Account, N'男', 170, 20, 0, 0)";
                        using (SqlCommand cmd = new SqlCommand(insertProfileSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Account", account);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public static void LoadUserProfile(string account, ref string gender, ref double height, ref int age, ref int activityIndex, ref double lastWeight)
        {
            // 💡 防呆：如果傳進來的帳號根本是空的，立刻警告，因為這就是換日崩潰的主因！
            if (string.IsNullOrEmpty(account)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT TRIM(Gender) as CleanGender, Height, Age, ActivityIndex, LastWeight FROM UserProfiles WHERE TRIM(Account) = @Account";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            gender = rd["CleanGender"].ToString().Trim();
                            height = Convert.ToDouble(rd["Height"]);
                            age = Convert.ToInt32(rd["Age"]);
                            activityIndex = Convert.ToInt32(rd["ActivityIndex"]);
                            lastWeight = Convert.ToDouble(rd["LastWeight"]);

                            // 💡 如果成功讀到資料，跳出提示（測試成功後可以整段刪掉，現在用來抓姦）
                            System.Diagnostics.Debug.WriteLine($"[DatabaseHelper] 成功讀取！{account} -> 性別={gender}, 身高={height}");
                        }
                    }
                }
            }
        }

        // 2. 更新個人狀態與最後體重
        public static void UpdateUserProfile(string account, string gender, double height, int age, int activityIndex, double lastWeight)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string cleanAccount = account.Trim();
                string cleanGender = gender.Trim();

                // 1. 檢查帳號是否存在
                string checkSql = "SELECT COUNT(*) FROM UserProfiles WHERE TRIM(Account) = @Account";
                int hasProfile = 0;

                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Account", cleanAccount);
                    hasProfile = Convert.ToInt32(checkCmd.ExecuteScalar());
                }

                string sql = "";
                string opType = "";
                if (hasProfile > 0)
                {
                    opType = "修改 (UPDATE)";
                    sql = @"UPDATE UserProfiles 
                    SET Gender = @Gender, Height = @Height, Age = @Age, 
                        ActivityIndex = @ActivityIndex, LastWeight = @LastWeight 
                    WHERE TRIM(Account) = @Account";
                }
                else
                {
                    opType = "全新插入 (INSERT)";
                    sql = @"INSERT INTO UserProfiles (Account, Gender, Height, Age, ActivityIndex, LastWeight) 
                    VALUES (@Account, @Gender, @Height, @Age, @ActivityIndex, @LastWeight)";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", cleanAccount);
                    cmd.Parameters.AddWithValue("@Gender", cleanGender);
                    cmd.Parameters.AddWithValue("@Height", height);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@ActivityIndex", activityIndex);
                    cmd.Parameters.AddWithValue("@LastWeight", lastWeight);

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }

        // 1. 讀取指定日期與帳號的飲食清單
        public static List<fitnesstracker.DietItem> GetDietLogs(string account, string dateStr)
        {
            List<fitnesstracker.DietItem> list = new List<fitnesstracker.DietItem>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, FoodName, Calories, Protein FROM DietLogs WHERE TRIM(Account) = @Account AND LogDate = @LogDate";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());
                    cmd.Parameters.AddWithValue("@LogDate", dateStr);
                    using (SqlDataReader rd = cmd.ExecuteReader()) // 講義第49頁標準讀取
                    {
                        while (rd.Read())
                        {
                            list.Add(new fitnesstracker.DietItem
                            {
                                Id = Convert.ToInt32(rd["Id"]), // 💡 存下資料庫自動生成的 Id，方便等一下刪除用！
                                FoodName = rd["FoodName"].ToString().Trim(),
                                Calories = Convert.ToInt32(rd["Calories"]),
                                Protein = Convert.ToDouble(rd["Protein"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // 2. 讀取指定日期與帳號的運動清單
        public static List<fitnesstracker.ExerciseItem> GetExerciseLogs(string account, string dateStr)
        {
            List<fitnesstracker.ExerciseItem> list = new List<fitnesstracker.ExerciseItem>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, ActivityName, DurationMinutes, BurnedCalories FROM ExerciseLogs WHERE TRIM(Account) = @Account AND LogDate = @LogDate";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());
                    cmd.Parameters.AddWithValue("@LogDate", dateStr);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new fitnesstracker.ExerciseItem
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                ActivityName = rd["ActivityName"].ToString().Trim(),
                                DurationMinutes = Convert.ToInt32(rd["DurationMinutes"]),
                                BurnedCalories = Convert.ToInt32(rd["BurnedCalories"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // 3. 新增單筆紀錄 (飲食或運動) -> 對齊講義第34頁 INSERT
        public static void AddLogItem(string tableName, string account, string dateStr, string nameField, string nameValue, int val1, double val2)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "";
                if (tableName == "DietLogs")
                {
                    sql = "INSERT INTO DietLogs (Account, LogDate, FoodName, Calories, Protein) VALUES (@Account, @LogDate, @Name, @Val1, @Val2)";
                }
                else
                {
                    sql = "INSERT INTO ExerciseLogs (Account, LogDate, ActivityName, DurationMinutes, BurnedCalories) VALUES (@Account, @LogDate, @Name, @Val1, @Val2)";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());
                    cmd.Parameters.AddWithValue("@LogDate", dateStr);
                    cmd.Parameters.AddWithValue("@Name", nameValue);
                    cmd.Parameters.AddWithValue("@Val1", val1);
                    cmd.Parameters.AddWithValue("@Val2", val2);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. 刪除單筆紀錄 -> 對齊講義第35頁 DELETE
        public static void DeleteLogItem(string tableName, int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"DELETE FROM {tableName} WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // 1. 檢查並撈取特定日期是否有存過歷史體重指標
        public static bool GetDailyLog(string account, string dateStr, ref double weight, ref string gender, ref double height, ref int age, ref int activityIndex)
        {
            bool hasHistory = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Weight, Gender, Height, Age, ActivityIndex FROM DailyLogs WHERE TRIM(Account) = @Account AND LogDate = @LogDate";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());
                    cmd.Parameters.AddWithValue("@LogDate", dateStr);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            weight = Convert.ToDouble(rd["Weight"]);
                            gender = rd["Gender"].ToString().Trim();
                            height = Convert.ToDouble(rd["Height"]);
                            age = Convert.ToInt32(rd["Age"]);
                            activityIndex = Convert.ToInt32(rd["ActivityIndex"]);
                            hasHistory = true;
                        }
                    }
                }
            }
            return hasHistory;
        }

        // 2. 儲存或更新特定日期的歷史體重指標 (UPSERT 概念)
        public static void SaveDailyLog(string account, string dateStr, double weight, string gender, double height, int age, int activityIndex)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 先檢查這天有沒有歷史紀錄
                string checkSql = "SELECT COUNT(*) FROM DailyLogs WHERE TRIM(Account) = @Account AND LogDate = @LogDate";
                int count = 0;
                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Account", account.Trim());
                    checkCmd.Parameters.AddWithValue("@LogDate", dateStr);
                    count = Convert.ToInt32(checkCmd.ExecuteScalar());
                }

                string sql = "";
                if (count > 0)
                {
                    sql = @"UPDATE DailyLogs 
                    SET Weight = @Weight, Gender = @Gender, Height = @Height, Age = @Age, ActivityIndex = @ActivityIndex 
                    WHERE TRIM(Account) = @Account AND LogDate = @LogDate";
                }
                else
                {
                    sql = @"INSERT INTO DailyLogs (Account, LogDate, Weight, Gender, Height, Age, ActivityIndex) 
                    VALUES (@Account, @LogDate, @Weight, @Gender, @Height, @Age, @ActivityIndex)";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());
                    cmd.Parameters.AddWithValue("@LogDate", dateStr);
                    cmd.Parameters.AddWithValue("@Weight", weight);
                    cmd.Parameters.AddWithValue("@Gender", gender.Trim());
                    cmd.Parameters.AddWithValue("@Height", height);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@ActivityIndex", activityIndex);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // 💡 1. 宣告圖表數據專用的資料結構（消滅 CS0426 錯誤）
        public class ChartDataPoint
        {
            public string Label { get; set; }        // X 軸標籤 (日期或月份)
            public double AvgWeight { get; set; }    // 體重
            public double TotalCalories { get; set; } // 總熱量
            public double TotalProtein { get; set; }  // 總蛋白質
        }

        // 💡 2. 實作圖表數據的 INNER/LEFT JOIN 撈取方法（消滅 CS0117 錯誤）
        public static List<ChartDataPoint> GetChartData(string account, string rangeType)
        {
            List<ChartDataPoint> list = new List<ChartDataPoint>();

            int daysBack = 7;
            if (rangeType == "month") daysBack = 30;
            else if (rangeType == "year") daysBack = 365;

            string startDateStr = DateTime.Today.AddDays(-daysBack).ToString("yyyy-MM-dd");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "";

                if (rangeType == "year")
                {
                    // 年變化的部分維持原樣
                    sql = @"
                SELECT 
                    SUBSTRING(LogDate, 1, 7) AS GroupLabel,
                    0 AS AvgW,
                    SUM(Calories) / COUNT(DISTINCT LogDate) AS AvgCal,
                    SUM(Protein) / COUNT(DISTINCT LogDate) AS AvgPro
                FROM DietLogs
                WHERE TRIM(Account) = @Account AND LogDate >= @StartDate
                GROUP BY SUBSTRING(LogDate, 1, 7)
                ORDER BY GroupLabel";
                }
                else
                {
                    sql = @"
                    SELECT 
                        Diet.LogDate AS GroupLabel,
                        MAX(ISNULL(Daily.W, 0)) AS AvgW, 
                        SUM(Diet.Calories) AS AvgCal,
                        SUM(Diet.Protein) AS AvgPro
                        FROM DietLogs Diet
                        LEFT JOIN (
                        SELECT LogDate, Weight AS W 
                        FROM DailyLogs 
                        WHERE TRIM(Account) = @Account
                        ) Daily ON Diet.LogDate = Daily.LogDate
                         WHERE TRIM(Diet.Account) = @Account AND Diet.LogDate >= @StartDate
                        GROUP BY Diet.LogDate
                        ORDER BY Diet.LogDate";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Account", account.Trim());
                    cmd.Parameters.AddWithValue("@StartDate", startDateStr);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new ChartDataPoint
                            {
                                Label = rd["GroupLabel"].ToString(),
                                AvgWeight = rd["AvgW"] != DBNull.Value ? Convert.ToDouble(rd["AvgW"]) : 0,
                                TotalCalories = rd["AvgCal"] != DBNull.Value ? Convert.ToDouble(rd["AvgCal"]) : 0,
                                TotalProtein = rd["AvgPro"] != DBNull.Value ? Convert.ToDouble(rd["AvgPro"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}