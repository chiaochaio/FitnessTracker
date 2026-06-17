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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbActivity = new System.Windows.Forms.ComboBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSaveWeight = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCalStatus = new System.Windows.Forms.Label();
            this.lblCalProgress = new System.Windows.Forms.Label();
            this.pbCalories = new System.Windows.Forms.ProgressBar();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteDiet = new System.Windows.Forms.Button();
            this.dgvDiet = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAddDiet = new System.Windows.Forms.Button();
            this.txtFoodProtein = new System.Windows.Forms.TextBox();
            this.txtFoodCal = new System.Windows.Forms.TextBox();
            this.txtFoodName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbQuickFood = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDeleteExercise = new System.Windows.Forms.Button();
            this.dgvExercise = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAddExercise = new System.Windows.Forms.Button();
            this.txtExerciseBurn = new System.Windows.Forms.TextBox();
            this.txtExerciseDuration = new System.Windows.Forms.TextBox();
            this.txtExerciseName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panelChartContainer = new System.Windows.Forms.Panel();
            this.panelChartContainer2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiet)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExercise)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 525);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fitnesstracker_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.BurlyWood;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(780, 482);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "今日狀態(H)";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.cmbGender);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cmbActivity);
            this.groupBox2.Controls.Add(this.txtAge);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtHeight);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnSaveWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtWeight);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 241);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "個人狀態";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cmbGender
            // 
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cmbGender.Location = new System.Drawing.Point(521, 51);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(51, 38);
            this.cmbGender.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(430, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 30);
            this.label14.TabIndex = 11;
            this.label14.Text = "性別：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(406, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 30);
            this.label13.TabIndex = 10;
            this.label13.Text = "活動量：";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // cmbActivity
            // 
            this.cmbActivity.FormattingEnabled = true;
            this.cmbActivity.Items.AddRange(new object[] {
            "靜態生活 (幾乎不運動)",
            "輕度活動 (每週運動1-3次)",
            "中度活動 (每週運動3-5次)",
            "高度活動 (每週運動6-7次)",
            "非常活躍 (激烈運動/勞動工作)"});
            this.cmbActivity.Location = new System.Drawing.Point(521, 96);
            this.cmbActivity.Name = "cmbActivity";
            this.cmbActivity.Size = new System.Drawing.Size(193, 38);
            this.cmbActivity.TabIndex = 9;
            this.cmbActivity.SelectedIndexChanged += new System.EventHandler(this.cmbActivity_SelectedIndexChanged);
            // 
            // txtAge
            // 
            this.txtAge.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtAge.Location = new System.Drawing.Point(106, 144);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(110, 39);
            this.txtAge.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 30);
            this.label12.TabIndex = 7;
            this.label12.Text = "年齡：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(222, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 30);
            this.label11.TabIndex = 6;
            this.label11.Text = "cm";
            // 
            // txtHeight
            // 
            this.txtHeight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtHeight.Location = new System.Drawing.Point(106, 90);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(110, 39);
            this.txtHeight.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 30);
            this.label7.TabIndex = 4;
            this.label7.Text = "身高：";
            // 
            // btnSaveWeight
            // 
            this.btnSaveWeight.BackColor = System.Drawing.Color.LightSalmon;
            this.btnSaveWeight.Location = new System.Drawing.Point(521, 175);
            this.btnSaveWeight.Name = "btnSaveWeight";
            this.btnSaveWeight.Size = new System.Drawing.Size(193, 45);
            this.btnSaveWeight.TabIndex = 3;
            this.btnSaveWeight.Text = "更新個人狀態";
            this.btnSaveWeight.UseVisualStyleBackColor = false;
            this.btnSaveWeight.Click += new System.EventHandler(this.btnSaveWeight_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "kg";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtWeight
            // 
            this.txtWeight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtWeight.Location = new System.Drawing.Point(106, 41);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(110, 39);
            this.txtWeight.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "體重：";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.lblCalStatus);
            this.groupBox1.Controls.Add(this.lblCalProgress);
            this.groupBox1.Controls.Add(this.pbCalories);
            this.groupBox1.Location = new System.Drawing.Point(6, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 182);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "今日熱量統計";
            // 
            // lblCalStatus
            // 
            this.lblCalStatus.AutoSize = true;
            this.lblCalStatus.Location = new System.Drawing.Point(12, 94);
            this.lblCalStatus.Name = "lblCalStatus";
            this.lblCalStatus.Size = new System.Drawing.Size(139, 30);
            this.lblCalStatus.TabIndex = 2;
            this.lblCalStatus.Text = "剩餘可攝取:";
            // 
            // lblCalProgress
            // 
            this.lblCalProgress.AutoSize = true;
            this.lblCalProgress.Location = new System.Drawing.Point(12, 49);
            this.lblCalProgress.Name = "lblCalProgress";
            this.lblCalProgress.Size = new System.Drawing.Size(139, 30);
            this.lblCalProgress.TabIndex = 1;
            this.lblCalProgress.Text = "今日已攝取:";
            this.lblCalProgress.Click += new System.EventHandler(this.lblCalProgress_Click);
            // 
            // pbCalories
            // 
            this.pbCalories.Location = new System.Drawing.Point(202, 10);
            this.pbCalories.Name = "pbCalories";
            this.pbCalories.Size = new System.Drawing.Size(397, 27);
            this.pbCalories.TabIndex = 0;
            this.pbCalories.Click += new System.EventHandler(this.pbCalories_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(6, 6);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(188, 39);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(240)))), ((int)(((byte)(200)))));
            this.tabPage2.Controls.Add(this.btnDeleteDiet);
            this.tabPage2.Controls.Add(this.dgvDiet);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 482);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "";
            this.tabPage2.Text = "飲食紀錄(E)";
            // 
            // btnDeleteDiet
            // 
            this.btnDeleteDiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDeleteDiet.Location = new System.Drawing.Point(699, 436);
            this.btnDeleteDiet.Name = "btnDeleteDiet";
            this.btnDeleteDiet.Size = new System.Drawing.Size(75, 38);
            this.btnDeleteDiet.TabIndex = 2;
            this.btnDeleteDiet.Text = "刪除";
            this.btnDeleteDiet.UseVisualStyleBackColor = false;
            this.btnDeleteDiet.Click += new System.EventHandler(this.btnDeleteDiet_Click);
            // 
            // dgvDiet
            // 
            this.dgvDiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiet.Location = new System.Drawing.Point(19, 256);
            this.dgvDiet.Name = "dgvDiet";
            this.dgvDiet.RowHeadersWidth = 51;
            this.dgvDiet.RowTemplate.Height = 27;
            this.dgvDiet.Size = new System.Drawing.Size(749, 218);
            this.dgvDiet.TabIndex = 1;
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
            this.groupBox3.Size = new System.Drawing.Size(753, 229);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "新增飲食項目";
            // 
            // btnAddDiet
            // 
            this.btnAddDiet.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnAddDiet.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddDiet.Location = new System.Drawing.Point(619, 175);
            this.btnAddDiet.Name = "btnAddDiet";
            this.btnAddDiet.Size = new System.Drawing.Size(128, 48);
            this.btnAddDiet.TabIndex = 8;
            this.btnAddDiet.Text = "新增這餐";
            this.btnAddDiet.UseVisualStyleBackColor = false;
            this.btnAddDiet.Click += new System.EventHandler(this.btnAddDiet_Click);
            // 
            // txtFoodProtein
            // 
            this.txtFoodProtein.Location = new System.Drawing.Point(175, 166);
            this.txtFoodProtein.Name = "txtFoodProtein";
            this.txtFoodProtein.Size = new System.Drawing.Size(119, 39);
            this.txtFoodProtein.TabIndex = 7;
            // 
            // txtFoodCal
            // 
            this.txtFoodCal.Location = new System.Drawing.Point(175, 106);
            this.txtFoodCal.Name = "txtFoodCal";
            this.txtFoodCal.Size = new System.Drawing.Size(119, 39);
            this.txtFoodCal.TabIndex = 6;
            this.txtFoodCal.TextChanged += new System.EventHandler(this.txtFoodCal_TextChanged);
            // 
            // txtFoodName
            // 
            this.txtFoodName.Location = new System.Drawing.Point(175, 45);
            this.txtFoodName.Name = "txtFoodName";
            this.txtFoodName.Size = new System.Drawing.Size(179, 39);
            this.txtFoodName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 30);
            this.label6.TabIndex = 4;
            this.label6.Text = "快速選取：";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // cmbQuickFood
            // 
            this.cmbQuickFood.FormattingEnabled = true;
            this.cmbQuickFood.Location = new System.Drawing.Point(415, 89);
            this.cmbQuickFood.Name = "cmbQuickFood";
            this.cmbQuickFood.Size = new System.Drawing.Size(315, 38);
            this.cmbQuickFood.TabIndex = 3;
            this.cmbQuickFood.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 30);
            this.label5.TabIndex = 2;
            this.label5.Text = "蛋白質 (g) ：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "熱量 (kcal)：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "食物名稱：";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightCyan;
            this.tabPage3.Controls.Add(this.btnDeleteExercise);
            this.tabPage3.Controls.Add(this.dgvExercise);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(780, 482);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "運動紀錄(S)";
            // 
            // btnDeleteExercise
            // 
            this.btnDeleteExercise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDeleteExercise.Location = new System.Drawing.Point(693, 436);
            this.btnDeleteExercise.Name = "btnDeleteExercise";
            this.btnDeleteExercise.Size = new System.Drawing.Size(81, 40);
            this.btnDeleteExercise.TabIndex = 3;
            this.btnDeleteExercise.Text = "刪除";
            this.btnDeleteExercise.UseVisualStyleBackColor = false;
            this.btnDeleteExercise.Click += new System.EventHandler(this.btnDeleteExercise_Click);
            // 
            // dgvExercise
            // 
            this.dgvExercise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExercise.Location = new System.Drawing.Point(18, 280);
            this.dgvExercise.Name = "dgvExercise";
            this.dgvExercise.RowHeadersWidth = 51;
            this.dgvExercise.RowTemplate.Height = 27;
            this.dgvExercise.Size = new System.Drawing.Size(741, 196);
            this.dgvExercise.TabIndex = 2;
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
            this.groupBox4.Location = new System.Drawing.Point(18, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(748, 245);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "新增運動項目";
            // 
            // btnAddExercise
            // 
            this.btnAddExercise.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnAddExercise.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddExercise.Location = new System.Drawing.Point(610, 187);
            this.btnAddExercise.Name = "btnAddExercise";
            this.btnAddExercise.Size = new System.Drawing.Size(131, 49);
            this.btnAddExercise.TabIndex = 8;
            this.btnAddExercise.Text = "新增運動";
            this.btnAddExercise.UseVisualStyleBackColor = false;
            this.btnAddExercise.Click += new System.EventHandler(this.btnAddExercise_Click);
            // 
            // txtExerciseBurn
            // 
            this.txtExerciseBurn.Location = new System.Drawing.Point(238, 184);
            this.txtExerciseBurn.Name = "txtExerciseBurn";
            this.txtExerciseBurn.Size = new System.Drawing.Size(280, 39);
            this.txtExerciseBurn.TabIndex = 7;
            // 
            // txtExerciseDuration
            // 
            this.txtExerciseDuration.Location = new System.Drawing.Point(238, 132);
            this.txtExerciseDuration.Name = "txtExerciseDuration";
            this.txtExerciseDuration.Size = new System.Drawing.Size(280, 39);
            this.txtExerciseDuration.TabIndex = 6;
            // 
            // txtExerciseName
            // 
            this.txtExerciseName.Location = new System.Drawing.Point(238, 75);
            this.txtExerciseName.Name = "txtExerciseName";
            this.txtExerciseName.Size = new System.Drawing.Size(280, 39);
            this.txtExerciseName.TabIndex = 5;
            this.txtExerciseName.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 30);
            this.label8.TabIndex = 2;
            this.label8.Text = "消耗熱量 (kcal)  ：";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 30);
            this.label9.TabIndex = 1;
            this.label9.Text = "運動時間 (mins)：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(205, 30);
            this.label10.TabIndex = 0;
            this.label10.Text = "運動名稱            ：";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelChartContainer2);
            this.tabPage4.Controls.Add(this.comboBox1);
            this.tabPage4.Controls.Add(this.panelChartContainer);
            this.tabPage4.Location = new System.Drawing.Point(4, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(780, 482);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "統計圖表(C)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(19, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(108, 38);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // panelChartContainer
            // 
            this.panelChartContainer.Location = new System.Drawing.Point(3, 6);
            this.panelChartContainer.Name = "panelChartContainer";
            this.panelChartContainer.Size = new System.Drawing.Size(376, 470);
            this.panelChartContainer.TabIndex = 0;
            // 
            // panelChartContainer2
            // 
            this.panelChartContainer2.Location = new System.Drawing.Point(388, 6);
            this.panelChartContainer2.Name = "panelChartContainer2";
            this.panelChartContainer2.Size = new System.Drawing.Size(389, 470);
            this.panelChartContainer2.TabIndex = 1;
            // 
            // fitnesstracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(812, 548);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fitnesstracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "運動飲食小助手";
            this.Load += new System.EventHandler(this.fitnesstracker_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiet)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExercise)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage4.ResumeLayout(false);
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbActivity;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Button btnDeleteDiet;
        private System.Windows.Forms.Button btnDeleteExercise;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panelChartContainer;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panelChartContainer2;
    }
}

