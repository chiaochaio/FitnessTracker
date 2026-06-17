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
            if (string.IsNullOrEmpty(account))
            {
                MessageBox.Show("【警告】資料庫讀取失敗：傳入的 currentUser 帳號是空的(null或空字串)！\n這會導致程式查不到資料而直接退回預設值(男/170)！",
                                "讀取帳號落空警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                        else
                        {
                            // 💡 如果資料庫連線正常，但裡面找不到這個學號的資料
                            MessageBox.Show($"【資料庫讀取報告】\n連線成功，但在 UserProfiles 資料表內【找不到】帳號為 '{account.Trim()}' 的生理資料紀錄！\n因此系統將暫時使用畫面的預設值。",
                                            "讀取結果：無資料", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                    // 💡【物理診斷爆彈】：強迫跳出視窗，告訴我們底層資料庫的真實慘狀！
                    MessageBox.Show($"【資料庫連線診斷報告】\n" +
                                    $"目前登入帳號: '{cleanAccount}'\n" +
                                    $"資料庫內原先是否存在: {(hasProfile > 0 ? "是" : "否")}\n" +
                                    $"本次執行的 SQL 動作: {opType}\n" +
                                    $"本次真正成功影響的資料庫列數: {rowsAffected} 筆\n" +
                                    $"預期寫入資料: 性別={cleanGender}, 身高={height}, 體重={lastWeight}",
                                    "後台即時診斷", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}