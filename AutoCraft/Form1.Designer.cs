namespace AutoCraft
{
    partial class Form1
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
            this.nudTime = new System.Windows.Forms.NumericUpDown();
            this.lblActiveStatus = new System.Windows.Forms.Label();
            this.lblDoc = new System.Windows.Forms.Label();
            this.btnSetAltPos = new System.Windows.Forms.Button();
            this.lblAltPos = new System.Windows.Forms.Label();
            this.lblAugPos = new System.Windows.Forms.Label();
            this.btnSetAugPos = new System.Windows.Forms.Button();
            this.lblRegalPos = new System.Windows.Forms.Label();
            this.btnSetRegalPos = new System.Windows.Forms.Button();
            this.lblChaosPos = new System.Windows.Forms.Label();
            this.btnSetChaosPos = new System.Windows.Forms.Button();
            this.lblChancePos = new System.Windows.Forms.Label();
            this.btnSetChancePos = new System.Windows.Forms.Button();
            this.lblScourPos = new System.Windows.Forms.Label();
            this.btnSetScourPos = new System.Windows.Forms.Button();
            this.lblCraftAreaPos = new System.Windows.Forms.Label();
            this.btnSetCraftAreaPos = new System.Windows.Forms.Button();
            this.pnlSetBTNs = new System.Windows.Forms.Panel();
            this.btnSetAlchPos = new System.Windows.Forms.Button();
            this.btnSetTransPos = new System.Windows.Forms.Button();
            this.lbl_PlayTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbScript = new System.Windows.Forms.GroupBox();
            this.cb_AugBeforeRegal = new System.Windows.Forms.CheckBox();
            this.rb_UseChaos = new System.Windows.Forms.RadioButton();
            this.cb_Regal = new System.Windows.Forms.CheckBox();
            this.cb_Augment = new System.Windows.Forms.CheckBox();
            this.rb_UseAlt = new System.Windows.Forms.RadioButton();
            this.rb_UseChance = new System.Windows.Forms.RadioButton();
            this.rb_UseAlch = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudRegal = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_Regal = new System.Windows.Forms.DataGridView();
            this.dgvRegalIsSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvRegalAffixName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRegalAffixMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRegalAffixMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_Aug = new System.Windows.Forms.DataGridView();
            this.dgvAugIsSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAugAffixName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAugAffixMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAugAffixMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_ReloadAffix = new System.Windows.Forms.Button();
            this.btn_SaveAffix = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv_Stop = new System.Windows.Forms.DataGridView();
            this.dgvStopIsSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvStopAffixName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStopAffixMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStopAffixMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbStopCondition = new System.Windows.Forms.GroupBox();
            this.lblStopPre = new System.Windows.Forms.Label();
            this.nudStopPre = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTransPos = new System.Windows.Forms.Label();
            this.btnSavePos = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.nudIndex = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGetClipboard = new System.Windows.Forms.Button();
            this.lblAlchPos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
            this.pnlSetBTNs.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbScript.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegal)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Regal)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Aug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Stop)).BeginInit();
            this.gbStopCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopPre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // nudTime
            // 
            this.nudTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTime.Location = new System.Drawing.Point(94, 327);
            this.nudTime.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudTime.Name = "nudTime";
            this.nudTime.Size = new System.Drawing.Size(82, 22);
            this.nudTime.TabIndex = 0;
            this.nudTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblActiveStatus
            // 
            this.lblActiveStatus.AutoSize = true;
            this.lblActiveStatus.Location = new System.Drawing.Point(70, 296);
            this.lblActiveStatus.Name = "lblActiveStatus";
            this.lblActiveStatus.Size = new System.Drawing.Size(41, 12);
            this.lblActiveStatus.TabIndex = 3;
            this.lblActiveStatus.Text = "停止中";
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(23, 242);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(80, 12);
            this.lblDoc.TabIndex = 4;
            this.lblDoc.Text = "F2開始,F3停止";
            // 
            // btnSetAltPos
            // 
            this.btnSetAltPos.Location = new System.Drawing.Point(21, 17);
            this.btnSetAltPos.Name = "btnSetAltPos";
            this.btnSetAltPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetAltPos.TabIndex = 5;
            this.btnSetAltPos.Text = "設定改造石座標";
            this.btnSetAltPos.UseVisualStyleBackColor = true;
            this.btnSetAltPos.Click += new System.EventHandler(this.btnSetAltPos_Click);
            // 
            // lblAltPos
            // 
            this.lblAltPos.AutoSize = true;
            this.lblAltPos.Location = new System.Drawing.Point(178, 53);
            this.lblAltPos.Name = "lblAltPos";
            this.lblAltPos.Size = new System.Drawing.Size(33, 12);
            this.lblAltPos.TabIndex = 6;
            this.lblAltPos.Text = "label2";
            // 
            // lblAugPos
            // 
            this.lblAugPos.AutoSize = true;
            this.lblAugPos.Location = new System.Drawing.Point(178, 101);
            this.lblAugPos.Name = "lblAugPos";
            this.lblAugPos.Size = new System.Drawing.Size(33, 12);
            this.lblAugPos.TabIndex = 8;
            this.lblAugPos.Text = "label2";
            // 
            // btnSetAugPos
            // 
            this.btnSetAugPos.Location = new System.Drawing.Point(21, 65);
            this.btnSetAugPos.Name = "btnSetAugPos";
            this.btnSetAugPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetAugPos.TabIndex = 7;
            this.btnSetAugPos.Text = "設定增幅石座標";
            this.btnSetAugPos.UseVisualStyleBackColor = true;
            this.btnSetAugPos.Click += new System.EventHandler(this.btnSetAugPos_Click);
            // 
            // lblRegalPos
            // 
            this.lblRegalPos.AutoSize = true;
            this.lblRegalPos.Location = new System.Drawing.Point(178, 151);
            this.lblRegalPos.Name = "lblRegalPos";
            this.lblRegalPos.Size = new System.Drawing.Size(33, 12);
            this.lblRegalPos.TabIndex = 10;
            this.lblRegalPos.Text = "label2";
            // 
            // btnSetRegalPos
            // 
            this.btnSetRegalPos.Location = new System.Drawing.Point(21, 115);
            this.btnSetRegalPos.Name = "btnSetRegalPos";
            this.btnSetRegalPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetRegalPos.TabIndex = 9;
            this.btnSetRegalPos.Text = "設定富豪石座標";
            this.btnSetRegalPos.UseVisualStyleBackColor = true;
            this.btnSetRegalPos.Click += new System.EventHandler(this.btnSetRegalPos_Click);
            // 
            // lblChaosPos
            // 
            this.lblChaosPos.AutoSize = true;
            this.lblChaosPos.Location = new System.Drawing.Point(178, 203);
            this.lblChaosPos.Name = "lblChaosPos";
            this.lblChaosPos.Size = new System.Drawing.Size(33, 12);
            this.lblChaosPos.TabIndex = 12;
            this.lblChaosPos.Text = "label2";
            // 
            // btnSetChaosPos
            // 
            this.btnSetChaosPos.Location = new System.Drawing.Point(21, 167);
            this.btnSetChaosPos.Name = "btnSetChaosPos";
            this.btnSetChaosPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetChaosPos.TabIndex = 11;
            this.btnSetChaosPos.Text = "設定混沌石座標";
            this.btnSetChaosPos.UseVisualStyleBackColor = true;
            this.btnSetChaosPos.Click += new System.EventHandler(this.btnSetChaosPos_Click);
            // 
            // lblChancePos
            // 
            this.lblChancePos.AutoSize = true;
            this.lblChancePos.Location = new System.Drawing.Point(178, 249);
            this.lblChancePos.Name = "lblChancePos";
            this.lblChancePos.Size = new System.Drawing.Size(33, 12);
            this.lblChancePos.TabIndex = 14;
            this.lblChancePos.Text = "label2";
            // 
            // btnSetChancePos
            // 
            this.btnSetChancePos.Location = new System.Drawing.Point(21, 213);
            this.btnSetChancePos.Name = "btnSetChancePos";
            this.btnSetChancePos.Size = new System.Drawing.Size(106, 23);
            this.btnSetChancePos.TabIndex = 13;
            this.btnSetChancePos.Text = "設定機會石座標";
            this.btnSetChancePos.UseVisualStyleBackColor = true;
            this.btnSetChancePos.Click += new System.EventHandler(this.btnSetChancePos_Click);
            // 
            // lblScourPos
            // 
            this.lblScourPos.AutoSize = true;
            this.lblScourPos.Location = new System.Drawing.Point(178, 293);
            this.lblScourPos.Name = "lblScourPos";
            this.lblScourPos.Size = new System.Drawing.Size(33, 12);
            this.lblScourPos.TabIndex = 16;
            this.lblScourPos.Text = "label2";
            // 
            // btnSetScourPos
            // 
            this.btnSetScourPos.Location = new System.Drawing.Point(21, 257);
            this.btnSetScourPos.Name = "btnSetScourPos";
            this.btnSetScourPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetScourPos.TabIndex = 15;
            this.btnSetScourPos.Text = "設定重鑄石座標";
            this.btnSetScourPos.UseVisualStyleBackColor = true;
            this.btnSetScourPos.Click += new System.EventHandler(this.btnSetScourPos_Click);
            // 
            // lblCraftAreaPos
            // 
            this.lblCraftAreaPos.AutoSize = true;
            this.lblCraftAreaPos.Location = new System.Drawing.Point(178, 440);
            this.lblCraftAreaPos.Name = "lblCraftAreaPos";
            this.lblCraftAreaPos.Size = new System.Drawing.Size(33, 12);
            this.lblCraftAreaPos.TabIndex = 18;
            this.lblCraftAreaPos.Text = "label2";
            // 
            // btnSetCraftAreaPos
            // 
            this.btnSetCraftAreaPos.Location = new System.Drawing.Point(21, 401);
            this.btnSetCraftAreaPos.Name = "btnSetCraftAreaPos";
            this.btnSetCraftAreaPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetCraftAreaPos.TabIndex = 17;
            this.btnSetCraftAreaPos.Text = "設定做裝區座標";
            this.btnSetCraftAreaPos.UseVisualStyleBackColor = true;
            this.btnSetCraftAreaPos.Click += new System.EventHandler(this.btnSetCraftAreaPos_Click);
            // 
            // pnlSetBTNs
            // 
            this.pnlSetBTNs.Controls.Add(this.btnSetAlchPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetCraftAreaPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetTransPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetScourPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetChancePos);
            this.pnlSetBTNs.Controls.Add(this.btnSetChaosPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetRegalPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetAugPos);
            this.pnlSetBTNs.Controls.Add(this.btnSetAltPos);
            this.pnlSetBTNs.Location = new System.Drawing.Point(27, 34);
            this.pnlSetBTNs.Name = "pnlSetBTNs";
            this.pnlSetBTNs.Size = new System.Drawing.Size(145, 438);
            this.pnlSetBTNs.TabIndex = 19;
            // 
            // btnSetAlchPos
            // 
            this.btnSetAlchPos.Location = new System.Drawing.Point(21, 301);
            this.btnSetAlchPos.Name = "btnSetAlchPos";
            this.btnSetAlchPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetAlchPos.TabIndex = 20;
            this.btnSetAlchPos.Text = "設定點金石座標";
            this.btnSetAlchPos.UseVisualStyleBackColor = true;
            this.btnSetAlchPos.Click += new System.EventHandler(this.btnSetAlchPos_Click);
            // 
            // btnSetTransPos
            // 
            this.btnSetTransPos.Location = new System.Drawing.Point(21, 354);
            this.btnSetTransPos.Name = "btnSetTransPos";
            this.btnSetTransPos.Size = new System.Drawing.Size(106, 23);
            this.btnSetTransPos.TabIndex = 21;
            this.btnSetTransPos.Text = "設定蛻變石座標";
            this.btnSetTransPos.UseVisualStyleBackColor = true;
            this.btnSetTransPos.Click += new System.EventHandler(this.btnSetTransPos_Click);
            // 
            // lbl_PlayTime
            // 
            this.lbl_PlayTime.AutoSize = true;
            this.lbl_PlayTime.Location = new System.Drawing.Point(23, 333);
            this.lbl_PlayTime.Name = "lbl_PlayTime";
            this.lbl_PlayTime.Size = new System.Drawing.Size(65, 12);
            this.lbl_PlayTime.TabIndex = 6;
            this.lbl_PlayTime.Text = "執行次數：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "狀態：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(846, 669);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbScript);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.btn_ReloadAffix);
            this.tabPage1.Controls.Add(this.btn_SaveAffix);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dgv_Stop);
            this.tabPage1.Controls.Add(this.gbStopCondition);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.nudDelay);
            this.tabPage1.Controls.Add(this.lbl_PlayTime);
            this.tabPage1.Controls.Add(this.nudTime);
            this.tabPage1.Controls.Add(this.lblDoc);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lblActiveStatus);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(838, 643);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "功能";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.Defocus);
            // 
            // gbScript
            // 
            this.gbScript.Controls.Add(this.cb_AugBeforeRegal);
            this.gbScript.Controls.Add(this.rb_UseChaos);
            this.gbScript.Controls.Add(this.cb_Regal);
            this.gbScript.Controls.Add(this.cb_Augment);
            this.gbScript.Controls.Add(this.rb_UseAlt);
            this.gbScript.Controls.Add(this.rb_UseChance);
            this.gbScript.Controls.Add(this.rb_UseAlch);
            this.gbScript.Location = new System.Drawing.Point(18, 46);
            this.gbScript.Name = "gbScript";
            this.gbScript.Size = new System.Drawing.Size(156, 193);
            this.gbScript.TabIndex = 41;
            this.gbScript.TabStop = false;
            this.gbScript.Text = "選擇腳本";
            // 
            // cb_AugBeforeRegal
            // 
            this.cb_AugBeforeRegal.AutoSize = true;
            this.cb_AugBeforeRegal.Enabled = false;
            this.cb_AugBeforeRegal.Location = new System.Drawing.Point(48, 132);
            this.cb_AugBeforeRegal.Name = "cb_AugBeforeRegal";
            this.cb_AugBeforeRegal.Size = new System.Drawing.Size(84, 16);
            this.cb_AugBeforeRegal.TabIndex = 22;
            this.cb_AugBeforeRegal.Text = "富豪前增幅";
            this.cb_AugBeforeRegal.UseVisualStyleBackColor = true;
            // 
            // rb_UseChaos
            // 
            this.rb_UseChaos.AutoSize = true;
            this.rb_UseChaos.Location = new System.Drawing.Point(26, 43);
            this.rb_UseChaos.Name = "rb_UseChaos";
            this.rb_UseChaos.Size = new System.Drawing.Size(59, 16);
            this.rb_UseChaos.TabIndex = 3;
            this.rb_UseChaos.Tag = "2";
            this.rb_UseChaos.Text = "用混沌";
            this.rb_UseChaos.UseVisualStyleBackColor = true;
            this.rb_UseChaos.CheckedChanged += new System.EventHandler(this.ChangeActivatingAction);
            // 
            // cb_Regal
            // 
            this.cb_Regal.AutoSize = true;
            this.cb_Regal.Enabled = false;
            this.cb_Regal.Location = new System.Drawing.Point(48, 110);
            this.cb_Regal.Name = "cb_Regal";
            this.cb_Regal.Size = new System.Drawing.Size(84, 16);
            this.cb_Regal.TabIndex = 21;
            this.cb_Regal.Text = "使用富豪石";
            this.cb_Regal.UseVisualStyleBackColor = true;
            this.cb_Regal.CheckedChanged += new System.EventHandler(this.cb_Regal_CheckedChanged);
            // 
            // cb_Augment
            // 
            this.cb_Augment.AutoSize = true;
            this.cb_Augment.Enabled = false;
            this.cb_Augment.Location = new System.Drawing.Point(48, 88);
            this.cb_Augment.Name = "cb_Augment";
            this.cb_Augment.Size = new System.Drawing.Size(84, 16);
            this.cb_Augment.TabIndex = 21;
            this.cb_Augment.Text = "使用增幅石";
            this.cb_Augment.UseVisualStyleBackColor = true;
            this.cb_Augment.CheckedChanged += new System.EventHandler(this.cb_Augment_CheckedChanged);
            // 
            // rb_UseAlt
            // 
            this.rb_UseAlt.AutoSize = true;
            this.rb_UseAlt.Location = new System.Drawing.Point(26, 66);
            this.rb_UseAlt.Name = "rb_UseAlt";
            this.rb_UseAlt.Size = new System.Drawing.Size(59, 16);
            this.rb_UseAlt.TabIndex = 2;
            this.rb_UseAlt.Tag = "3";
            this.rb_UseAlt.Text = "用改造";
            this.rb_UseAlt.UseVisualStyleBackColor = true;
            this.rb_UseAlt.CheckedChanged += new System.EventHandler(this.ChangeActivatingAction);
            // 
            // rb_UseChance
            // 
            this.rb_UseChance.AutoSize = true;
            this.rb_UseChance.Location = new System.Drawing.Point(24, 158);
            this.rb_UseChance.Name = "rb_UseChance";
            this.rb_UseChance.Size = new System.Drawing.Size(83, 16);
            this.rb_UseChance.TabIndex = 1;
            this.rb_UseChance.Tag = "4";
            this.rb_UseChance.Text = "用重鑄機會";
            this.rb_UseChance.UseVisualStyleBackColor = true;
            this.rb_UseChance.CheckedChanged += new System.EventHandler(this.ChangeActivatingAction);
            // 
            // rb_UseAlch
            // 
            this.rb_UseAlch.AutoSize = true;
            this.rb_UseAlch.Checked = true;
            this.rb_UseAlch.Location = new System.Drawing.Point(26, 21);
            this.rb_UseAlch.Name = "rb_UseAlch";
            this.rb_UseAlch.Size = new System.Drawing.Size(83, 16);
            this.rb_UseAlch.TabIndex = 0;
            this.rb_UseAlch.TabStop = true;
            this.rb_UseAlch.Tag = "1";
            this.rb_UseAlch.Text = "用重鑄點金";
            this.rb_UseAlch.UseVisualStyleBackColor = true;
            this.rb_UseAlch.CheckedChanged += new System.EventHandler(this.ChangeActivatingAction);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nudRegal);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(23, 471);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 57);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "富豪條件";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "數量：";
            // 
            // nudRegal
            // 
            this.nudRegal.Location = new System.Drawing.Point(71, 22);
            this.nudRegal.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudRegal.Name = "nudRegal";
            this.nudRegal.Size = new System.Drawing.Size(66, 22);
            this.nudRegal.TabIndex = 31;
            this.nudRegal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dgv_Regal);
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(211, 424);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(605, 209);
            this.panel3.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "富豪條件：";
            // 
            // dgv_Regal
            // 
            this.dgv_Regal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Regal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRegalIsSelected,
            this.dgvRegalAffixName,
            this.dgvRegalAffixMin,
            this.dgvRegalAffixMax});
            this.dgv_Regal.Location = new System.Drawing.Point(8, 25);
            this.dgv_Regal.Name = "dgv_Regal";
            this.dgv_Regal.RowTemplate.Height = 24;
            this.dgv_Regal.Size = new System.Drawing.Size(592, 179);
            this.dgv_Regal.TabIndex = 33;
            this.dgv_Regal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // dgvRegalIsSelected
            // 
            this.dgvRegalIsSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgvRegalIsSelected.HeaderText = "選取";
            this.dgvRegalIsSelected.Name = "dgvRegalIsSelected";
            this.dgvRegalIsSelected.Width = 35;
            // 
            // dgvRegalAffixName
            // 
            this.dgvRegalAffixName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvRegalAffixName.HeaderText = "詞綴";
            this.dgvRegalAffixName.Name = "dgvRegalAffixName";
            this.dgvRegalAffixName.Width = 54;
            // 
            // dgvRegalAffixMin
            // 
            this.dgvRegalAffixMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvRegalAffixMin.HeaderText = "下限";
            this.dgvRegalAffixMin.Name = "dgvRegalAffixMin";
            this.dgvRegalAffixMin.Width = 54;
            // 
            // dgvRegalAffixMax
            // 
            this.dgvRegalAffixMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvRegalAffixMax.HeaderText = "上限";
            this.dgvRegalAffixMax.Name = "dgvRegalAffixMax";
            this.dgvRegalAffixMax.Width = 54;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dgv_Aug);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(211, 214);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(605, 209);
            this.panel2.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "增幅條件：";
            // 
            // dgv_Aug
            // 
            this.dgv_Aug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Aug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAugIsSelected,
            this.dgvAugAffixName,
            this.dgvAugAffixMin,
            this.dgvAugAffixMax});
            this.dgv_Aug.Location = new System.Drawing.Point(8, 25);
            this.dgv_Aug.Name = "dgv_Aug";
            this.dgv_Aug.RowTemplate.Height = 24;
            this.dgv_Aug.Size = new System.Drawing.Size(592, 179);
            this.dgv_Aug.TabIndex = 33;
            this.dgv_Aug.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // dgvAugIsSelected
            // 
            this.dgvAugIsSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgvAugIsSelected.HeaderText = "選取";
            this.dgvAugIsSelected.Name = "dgvAugIsSelected";
            this.dgvAugIsSelected.Width = 35;
            // 
            // dgvAugAffixName
            // 
            this.dgvAugAffixName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvAugAffixName.HeaderText = "詞綴";
            this.dgvAugAffixName.Name = "dgvAugAffixName";
            this.dgvAugAffixName.Width = 54;
            // 
            // dgvAugAffixMin
            // 
            this.dgvAugAffixMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvAugAffixMin.HeaderText = "下限";
            this.dgvAugAffixMin.Name = "dgvAugAffixMin";
            this.dgvAugAffixMin.Width = 54;
            // 
            // dgvAugAffixMax
            // 
            this.dgvAugAffixMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvAugAffixMax.HeaderText = "上限";
            this.dgvAugAffixMax.Name = "dgvAugAffixMax";
            this.dgvAugAffixMax.Width = 54;
            // 
            // btn_ReloadAffix
            // 
            this.btn_ReloadAffix.Location = new System.Drawing.Point(46, 581);
            this.btn_ReloadAffix.Name = "btn_ReloadAffix";
            this.btn_ReloadAffix.Size = new System.Drawing.Size(103, 23);
            this.btn_ReloadAffix.TabIndex = 37;
            this.btn_ReloadAffix.Text = "重新載入詞綴";
            this.btn_ReloadAffix.UseVisualStyleBackColor = true;
            this.btn_ReloadAffix.Click += new System.EventHandler(this.btn_ReloadAffix_Click);
            // 
            // btn_SaveAffix
            // 
            this.btn_SaveAffix.Location = new System.Drawing.Point(46, 552);
            this.btn_SaveAffix.Name = "btn_SaveAffix";
            this.btn_SaveAffix.Size = new System.Drawing.Size(102, 23);
            this.btn_SaveAffix.TabIndex = 36;
            this.btn_SaveAffix.Text = "儲存詞綴設定";
            this.btn_SaveAffix.UseVisualStyleBackColor = true;
            this.btn_SaveAffix.Click += new System.EventHandler(this.btn_SaveAffix_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(219, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "停止條件：";
            // 
            // dgv_Stop
            // 
            this.dgv_Stop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Stop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStopIsSelected,
            this.dgvStopAffixName,
            this.dgvStopAffixMin,
            this.dgvStopAffixMax});
            this.dgv_Stop.Location = new System.Drawing.Point(219, 29);
            this.dgv_Stop.Name = "dgv_Stop";
            this.dgv_Stop.RowTemplate.Height = 24;
            this.dgv_Stop.Size = new System.Drawing.Size(592, 179);
            this.dgv_Stop.TabIndex = 32;
            this.dgv_Stop.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // dgvStopIsSelected
            // 
            this.dgvStopIsSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgvStopIsSelected.HeaderText = "選取";
            this.dgvStopIsSelected.Name = "dgvStopIsSelected";
            this.dgvStopIsSelected.Width = 35;
            // 
            // dgvStopAffixName
            // 
            this.dgvStopAffixName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvStopAffixName.HeaderText = "詞綴";
            this.dgvStopAffixName.Name = "dgvStopAffixName";
            this.dgvStopAffixName.Width = 54;
            // 
            // dgvStopAffixMin
            // 
            this.dgvStopAffixMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvStopAffixMin.HeaderText = "下限";
            this.dgvStopAffixMin.Name = "dgvStopAffixMin";
            this.dgvStopAffixMin.Width = 54;
            // 
            // dgvStopAffixMax
            // 
            this.dgvStopAffixMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvStopAffixMax.HeaderText = "上限";
            this.dgvStopAffixMax.Name = "dgvStopAffixMax";
            this.dgvStopAffixMax.Width = 54;
            // 
            // gbStopCondition
            // 
            this.gbStopCondition.Controls.Add(this.lblStopPre);
            this.gbStopCondition.Controls.Add(this.nudStopPre);
            this.gbStopCondition.Location = new System.Drawing.Point(23, 402);
            this.gbStopCondition.Name = "gbStopCondition";
            this.gbStopCondition.Size = new System.Drawing.Size(152, 57);
            this.gbStopCondition.TabIndex = 30;
            this.gbStopCondition.TabStop = false;
            this.gbStopCondition.Text = "停止條件";
            // 
            // lblStopPre
            // 
            this.lblStopPre.AutoSize = true;
            this.lblStopPre.Location = new System.Drawing.Point(21, 26);
            this.lblStopPre.Name = "lblStopPre";
            this.lblStopPre.Size = new System.Drawing.Size(41, 12);
            this.lblStopPre.TabIndex = 32;
            this.lblStopPre.Text = "數量：";
            // 
            // nudStopPre
            // 
            this.nudStopPre.Location = new System.Drawing.Point(71, 22);
            this.nudStopPre.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudStopPre.Name = "nudStopPre";
            this.nudStopPre.Size = new System.Drawing.Size(66, 22);
            this.nudStopPre.TabIndex = 31;
            this.nudStopPre.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "延遲(ms)：";
            // 
            // nudDelay
            // 
            this.nudDelay.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDelay.Location = new System.Drawing.Point(94, 366);
            this.nudDelay.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(82, 22);
            this.nudDelay.TabIndex = 0;
            this.nudDelay.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblTransPos);
            this.tabPage2.Controls.Add(this.btnSavePos);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.lblIndex);
            this.tabPage2.Controls.Add(this.nudIndex);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.btnGetClipboard);
            this.tabPage2.Controls.Add(this.lblAlchPos);
            this.tabPage2.Controls.Add(this.pnlSetBTNs);
            this.tabPage2.Controls.Add(this.lblCraftAreaPos);
            this.tabPage2.Controls.Add(this.lblRegalPos);
            this.tabPage2.Controls.Add(this.lblAltPos);
            this.tabPage2.Controls.Add(this.lblScourPos);
            this.tabPage2.Controls.Add(this.lblAugPos);
            this.tabPage2.Controls.Add(this.lblChaosPos);
            this.tabPage2.Controls.Add(this.lblChancePos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(838, 643);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "設定";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.Defocus);
            // 
            // lblTransPos
            // 
            this.lblTransPos.AutoSize = true;
            this.lblTransPos.Location = new System.Drawing.Point(178, 393);
            this.lblTransPos.Name = "lblTransPos";
            this.lblTransPos.Size = new System.Drawing.Size(33, 12);
            this.lblTransPos.TabIndex = 22;
            this.lblTransPos.Text = "label2";
            // 
            // btnSavePos
            // 
            this.btnSavePos.Location = new System.Drawing.Point(27, 497);
            this.btnSavePos.Name = "btnSavePos";
            this.btnSavePos.Size = new System.Drawing.Size(145, 23);
            this.btnSavePos.TabIndex = 40;
            this.btnSavePos.Text = "儲存座標及位置設定";
            this.btnSavePos.UseVisualStyleBackColor = true;
            this.btnSavePos.Click += new System.EventHandler(this.btnSavePos_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(303, 106);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(310, 280);
            this.listBox1.TabIndex = 34;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(488, 399);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(53, 12);
            this.lblIndex.TabIndex = 33;
            this.lblIndex.Text = "詞綴位置";
            // 
            // nudIndex
            // 
            this.nudIndex.Location = new System.Drawing.Point(547, 396);
            this.nudIndex.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudIndex.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nudIndex.Name = "nudIndex";
            this.nudIndex.Size = new System.Drawing.Size(66, 22);
            this.nudIndex.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "選取詞綴";
            // 
            // btnGetClipboard
            // 
            this.btnGetClipboard.Location = new System.Drawing.Point(303, 393);
            this.btnGetClipboard.Name = "btnGetClipboard";
            this.btnGetClipboard.Size = new System.Drawing.Size(106, 23);
            this.btnGetClipboard.TabIndex = 21;
            this.btnGetClipboard.Text = "取得剪貼簿";
            this.btnGetClipboard.UseVisualStyleBackColor = true;
            this.btnGetClipboard.Click += new System.EventHandler(this.btnGetClipboard_Click);
            // 
            // lblAlchPos
            // 
            this.lblAlchPos.AutoSize = true;
            this.lblAlchPos.Location = new System.Drawing.Point(178, 340);
            this.lblAlchPos.Name = "lblAlchPos";
            this.lblAlchPos.Size = new System.Drawing.Size(33, 12);
            this.lblAlchPos.TabIndex = 21;
            this.lblAlchPos.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 669);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "POE Auto Crafter by Saya";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
            this.pnlSetBTNs.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbScript.ResumeLayout(false);
            this.gbScript.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegal)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Regal)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Aug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Stop)).EndInit();
            this.gbStopCondition.ResumeLayout(false);
            this.gbStopCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopPre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudTime;
        private System.Windows.Forms.Label lblActiveStatus;
        private System.Windows.Forms.Label lblDoc;
        private System.Windows.Forms.Button btnSetAltPos;
        private System.Windows.Forms.Label lblAltPos;
        private System.Windows.Forms.Label lblAugPos;
        private System.Windows.Forms.Button btnSetAugPos;
        private System.Windows.Forms.Label lblRegalPos;
        private System.Windows.Forms.Button btnSetRegalPos;
        private System.Windows.Forms.Label lblChaosPos;
        private System.Windows.Forms.Button btnSetChaosPos;
        private System.Windows.Forms.Label lblChancePos;
        private System.Windows.Forms.Button btnSetChancePos;
        private System.Windows.Forms.Label lblScourPos;
        private System.Windows.Forms.Button btnSetScourPos;
        private System.Windows.Forms.Label lblCraftAreaPos;
        private System.Windows.Forms.Button btnSetCraftAreaPos;
        private System.Windows.Forms.Panel pnlSetBTNs;
        private System.Windows.Forms.Label lbl_PlayTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox cb_Augment;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton rb_UseChaos;
        private System.Windows.Forms.RadioButton rb_UseAlt;
        private System.Windows.Forms.RadioButton rb_UseChance;
        private System.Windows.Forms.RadioButton rb_UseAlch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.GroupBox gbStopCondition;
        private System.Windows.Forms.Label lblStopPre;
        private System.Windows.Forms.NumericUpDown nudStopPre;
        private System.Windows.Forms.Button btnSetAlchPos;
        private System.Windows.Forms.Label lblAlchPos;
        private System.Windows.Forms.DataGridView dgv_Stop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv_Aug;
        private System.Windows.Forms.Button btn_SaveAffix;
        private System.Windows.Forms.Button btn_ReloadAffix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGetClipboard;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.NumericUpDown nudIndex;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnSavePos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_Regal;
        private System.Windows.Forms.CheckBox cb_Regal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudRegal;
        private System.Windows.Forms.CheckBox cb_AugBeforeRegal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvRegalIsSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRegalAffixName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRegalAffixMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRegalAffixMax;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvAugIsSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAugAffixName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAugAffixMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAugAffixMax;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvStopIsSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStopAffixName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStopAffixMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStopAffixMax;
        private System.Windows.Forms.Button btnSetTransPos;
        private System.Windows.Forms.Label lblTransPos;
        private System.Windows.Forms.GroupBox gbScript;
    }
}

