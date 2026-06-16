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
            this.cmbQuickFood = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFoodName = new System.Windows.Forms.TextBox();
            this.txtFoodCal = new System.Windows.Forms.TextBox();
            this.txtFoodProtein = new System.Windows.Forms.TextBox();
            this.btnAddDiet = new System.Windows.Forms.Button();
            this.dgvDiet = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAddExercise = new System.Windows.Forms.Button();
            this.txtExerciseBurn = new System.Windows.Forms.TextBox();
            this.txtExerciseDuration = new System.Windows.Forms.TextBox();
            this.txtExerciseName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvExercise = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiet)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExercise)).BeginInit();
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
            this.tabPage2.Controls.Add(this.dgvDiet);
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
            this.tabPage3.Controls.Add(this.dgvExercise);
            this.tabPage3.Controls.Add(this.groupBox4);
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
            this.groupBox3.Controls.Add(this.btnAddDiet);
            this.groupBox3.Controls.Add(this.txtFoodProtein);
            this.groupBox3.Controls.Add(this.txtFoodCal);
            this.groupBox3.Controls.Add(this.txtFoodName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbQuickFood);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(15, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(513, 199);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "新增飲食項目";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "食物名稱    ：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "熱量 (kcal) ：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "蛋白質 (g)  ：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // cmbQuickFood
            // 
            this.cmbQuickFood.FormattingEnabled = true;
            this.cmbQuickFood.Location = new System.Drawing.Point(152, 157);
            this.cmbQuickFood.Name = "cmbQuickFood";
            this.cmbQuickFood.Size = new System.Drawing.Size(121, 32);
            this.cmbQuickFood.TabIndex = 3;
            this.cmbQuickFood.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "快速選取    ：";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtFoodName
            // 
            this.txtFoodName.Location = new System.Drawing.Point(153, 37);
            this.txtFoodName.Name = "txtFoodName";
            this.txtFoodName.Size = new System.Drawing.Size(179, 33);
            this.txtFoodName.TabIndex = 5;
            // 
            // txtFoodCal
            // 
            this.txtFoodCal.Location = new System.Drawing.Point(153, 77);
            this.txtFoodCal.Name = "txtFoodCal";
            this.txtFoodCal.Size = new System.Drawing.Size(179, 33);
            this.txtFoodCal.TabIndex = 6;
            // 
            // txtFoodProtein
            // 
            this.txtFoodProtein.Location = new System.Drawing.Point(152, 116);
            this.txtFoodProtein.Name = "txtFoodProtein";
            this.txtFoodProtein.Size = new System.Drawing.Size(179, 33);
            this.txtFoodProtein.TabIndex = 7;
            // 
            // btnAddDiet
            // 
            this.btnAddDiet.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnAddDiet.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddDiet.Location = new System.Drawing.Point(406, 157);
            this.btnAddDiet.Name = "btnAddDiet";
            this.btnAddDiet.Size = new System.Drawing.Size(101, 32);
            this.btnAddDiet.TabIndex = 8;
            this.btnAddDiet.Text = "新增這餐";
            this.btnAddDiet.UseVisualStyleBackColor = false;
            // 
            // dgvDiet
            // 
            this.dgvDiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiet.Location = new System.Drawing.Point(15, 221);
            this.dgvDiet.Name = "dgvDiet";
            this.dgvDiet.RowTemplate.Height = 27;
            this.dgvDiet.Size = new System.Drawing.Size(513, 123);
            this.dgvDiet.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox4.Controls.Add(this.btnAddExercise);
            this.groupBox4.Controls.Add(this.txtExerciseBurn);
            this.groupBox4.Controls.Add(this.txtExerciseDuration);
            this.groupBox4.Controls.Add(this.txtExerciseName);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(15, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(513, 167);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "新增運動項目";
            // 
            // btnAddExercise
            // 
            this.btnAddExercise.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnAddExercise.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddExercise.Location = new System.Drawing.Point(406, 119);
            this.btnAddExercise.Name = "btnAddExercise";
            this.btnAddExercise.Size = new System.Drawing.Size(101, 32);
            this.btnAddExercise.TabIndex = 8;
            this.btnAddExercise.Text = "新增運動";
            this.btnAddExercise.UseVisualStyleBackColor = false;
            // 
            // txtExerciseBurn
            // 
            this.txtExerciseBurn.Location = new System.Drawing.Point(182, 116);
            this.txtExerciseBurn.Name = "txtExerciseBurn";
            this.txtExerciseBurn.Size = new System.Drawing.Size(179, 33);
            this.txtExerciseBurn.TabIndex = 7;
            // 
            // txtExerciseDuration
            // 
            this.txtExerciseDuration.Location = new System.Drawing.Point(182, 75);
            this.txtExerciseDuration.Name = "txtExerciseDuration";
            this.txtExerciseDuration.Size = new System.Drawing.Size(179, 33);
            this.txtExerciseDuration.TabIndex = 6;
            // 
            // txtExerciseName
            // 
            this.txtExerciseName.Location = new System.Drawing.Point(182, 36);
            this.txtExerciseName.Name = "txtExerciseName";
            this.txtExerciseName.Size = new System.Drawing.Size(179, 33);
            this.txtExerciseName.TabIndex = 5;
            this.txtExerciseName.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 24);
            this.label8.TabIndex = 2;
            this.label8.Text = "消耗熱量 (kcal)  ：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(166, 24);
            this.label9.TabIndex = 1;
            this.label9.Text = "運動時間 (mins)：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 24);
            this.label10.TabIndex = 0;
            this.label10.Text = "運動名稱            ：";
            // 
            // dgvExercise
            // 
            this.dgvExercise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExercise.Location = new System.Drawing.Point(15, 191);
            this.dgvExercise.Name = "dgvExercise";
            this.dgvExercise.RowTemplate.Height = 27;
            this.dgvExercise.Size = new System.Drawing.Size(513, 150);
            this.dgvExercise.TabIndex = 2;
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
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiet)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExercise)).EndInit();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbQuickFood;
        private System.Windows.Forms.TextBox txtFoodProtein;
        private System.Windows.Forms.TextBox txtFoodCal;
        private System.Windows.Forms.TextBox txtFoodName;
        private System.Windows.Forms.Button btnAddDiet;
        private System.Windows.Forms.DataGridView dgvDiet;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAddExercise;
        private System.Windows.Forms.TextBox txtExerciseBurn;
        private System.Windows.Forms.TextBox txtExerciseDuration;
        private System.Windows.Forms.TextBox txtExerciseName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvExercise;
    }
}

