namespace FitnessTracker
{
    partial class fitnesstracker
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbCalories = new System.Windows.Forms.ProgressBar();
            this.lblCalProgress = new System.Windows.Forms.Label();
            this.lblCalStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveWeight = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(551, 387);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.BurlyWood;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(543, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "今日狀態";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(240)))), ((int)(((byte)(200)))));
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(543, 350);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "";
            this.tabPage2.Text = "飲食紀錄";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightCyan;
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(543, 350);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "運動紀錄";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(6, 6);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(188, 33);
            this.dtpDate.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.lblCalStatus);
            this.groupBox1.Controls.Add(this.lblCalProgress);
            this.groupBox1.Controls.Add(this.pbCalories);
            this.groupBox1.Location = new System.Drawing.Point(6, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "今日熱量統計";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.btnSaveWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtWeight);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 131);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "個人狀態";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // pbCalories
            // 
            this.pbCalories.Location = new System.Drawing.Point(16, 41);
            this.pbCalories.Name = "pbCalories";
            this.pbCalories.Size = new System.Drawing.Size(291, 28);
            this.pbCalories.TabIndex = 0;
            // 
            // lblCalProgress
            // 
            this.lblCalProgress.AutoSize = true;
            this.lblCalProgress.Location = new System.Drawing.Point(12, 85);
            this.lblCalProgress.Name = "lblCalProgress";
            this.lblCalProgress.Size = new System.Drawing.Size(109, 24);
            this.lblCalProgress.TabIndex = 1;
            this.lblCalProgress.Text = "今日已攝取:";
            // 
            // lblCalStatus
            // 
            this.lblCalStatus.AutoSize = true;
            this.lblCalStatus.Location = new System.Drawing.Point(12, 121);
            this.lblCalStatus.Name = "lblCalStatus";
            this.lblCalStatus.Size = new System.Drawing.Size(109, 24);
            this.lblCalStatus.TabIndex = 2;
            this.lblCalStatus.Text = "剩餘可攝取:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "今日體重：";
            // 
            // txtWeight
            // 
            this.txtWeight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtWeight.Location = new System.Drawing.Point(110, 38);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(110, 33);
            this.txtWeight.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "kg";
            // 
            // btnSaveWeight
            // 
            this.btnSaveWeight.Location = new System.Drawing.Point(110, 77);
            this.btnSaveWeight.Name = "btnSaveWeight";
            this.btnSaveWeight.Size = new System.Drawing.Size(97, 36);
            this.btnSaveWeight.TabIndex = 3;
            this.btnSaveWeight.Text = "更新體重";
            this.btnSaveWeight.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(15, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(513, 153);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "新增飲食項目";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "食物名稱：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "熱量 (kcal)：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "蛋白質 (g)：";
            // 
            // fitnesstracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fitnesstracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "運動飲食小助手";
            this.Load += new System.EventHandler(this.fitnesstracker_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ProgressBar pbCalories;
        private System.Windows.Forms.Label lblCalStatus;
        private System.Windows.Forms.Label lblCalProgress;
        private System.Windows.Forms.Button btnSaveWeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

