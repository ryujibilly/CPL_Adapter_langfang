namespace GLAS_Adapter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.ComboBox();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCPLCmdRecvPort = new System.Windows.Forms.TextBox();
            this.txtDeptRecvPort = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCMSPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCMSIp = new System.Windows.Forms.TextBox();
            this.txtCPLPort = new System.Windows.Forms.TextBox();
            this.txtCPLIP = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbSPP_FT = new System.Windows.Forms.RadioButton();
            this.rbSpp_ND = new System.Windows.Forms.RadioButton();
            this.rbSpp_OR = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.rbDEP_BOX = new System.Windows.Forms.RadioButton();
            this.rbDEP_PORT = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtBaud);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(265, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "采集箱串口设置";
            // 
            // txtPort
            // 
            this.txtPort.FormattingEnabled = true;
            this.txtPort.Location = new System.Drawing.Point(99, 31);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(132, 23);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "COM3";
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(100, 78);
            this.txtBaud.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(132, 25);
            this.txtBaud.TabIndex = 3;
            this.txtBaud.Text = "115200";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "波特率:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "串口号：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(55, 373);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 29);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCPLCmdRecvPort);
            this.groupBox2.Controls.Add(this.txtDeptRecvPort);
            this.groupBox2.Location = new System.Drawing.Point(289, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(283, 136);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本机网络设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "CPL命令接收端口号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "井深接收端口号：";
            // 
            // txtCPLCmdRecvPort
            // 
            this.txtCPLCmdRecvPort.Location = new System.Drawing.Point(175, 78);
            this.txtCPLCmdRecvPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtCPLCmdRecvPort.Name = "txtCPLCmdRecvPort";
            this.txtCPLCmdRecvPort.Size = new System.Drawing.Size(69, 25);
            this.txtCPLCmdRecvPort.TabIndex = 3;
            this.txtCPLCmdRecvPort.Text = "9002";
            // 
            // txtDeptRecvPort
            // 
            this.txtDeptRecvPort.Location = new System.Drawing.Point(175, 25);
            this.txtDeptRecvPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeptRecvPort.Name = "txtDeptRecvPort";
            this.txtDeptRecvPort.Size = new System.Drawing.Size(69, 25);
            this.txtDeptRecvPort.TabIndex = 3;
            this.txtDeptRecvPort.Text = "9001";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtCMSPort);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtCMSIp);
            this.groupBox3.Controls.Add(this.txtCPLPort);
            this.groupBox3.Controls.Add(this.txtCPLIP);
            this.groupBox3.Location = new System.Drawing.Point(307, 159);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(265, 170);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "远程网络设置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 134);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "CMS网络端口：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "CPL网络端口：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "CMS网络IP：";
            // 
            // txtCMSPort
            // 
            this.txtCMSPort.Location = new System.Drawing.Point(127, 130);
            this.txtCMSPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtCMSPort.Name = "txtCMSPort";
            this.txtCMSPort.Size = new System.Drawing.Size(121, 25);
            this.txtCMSPort.TabIndex = 3;
            this.txtCMSPort.Text = "9003";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "CPL网络IP：";
            // 
            // txtCMSIp
            // 
            this.txtCMSIp.Location = new System.Drawing.Point(127, 96);
            this.txtCMSIp.Margin = new System.Windows.Forms.Padding(4);
            this.txtCMSIp.Name = "txtCMSIp";
            this.txtCMSIp.Size = new System.Drawing.Size(121, 25);
            this.txtCMSIp.TabIndex = 3;
            this.txtCMSIp.Text = "99.0.0.20";
            // 
            // txtCPLPort
            // 
            this.txtCPLPort.Location = new System.Drawing.Point(127, 64);
            this.txtCPLPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtCPLPort.Name = "txtCPLPort";
            this.txtCPLPort.Size = new System.Drawing.Size(121, 25);
            this.txtCPLPort.TabIndex = 3;
            this.txtCPLPort.Text = "9004";
            // 
            // txtCPLIP
            // 
            this.txtCPLIP.Location = new System.Drawing.Point(127, 30);
            this.txtCPLIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtCPLIP.Name = "txtCPLIP";
            this.txtCPLIP.Size = new System.Drawing.Size(121, 25);
            this.txtCPLIP.TabIndex = 3;
            this.txtCPLIP.Text = "99.0.0.61";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(188, 373);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 29);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtSave
            // 
            this.txtSave.Location = new System.Drawing.Point(328, 373);
            this.txtSave.Margin = new System.Windows.Forms.Padding(4);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(144, 29);
            this.txtSave.TabIndex = 2;
            this.txtSave.Text = "保存配置信息";
            this.txtSave.UseVisualStyleBackColor = true;
            this.txtSave.Click += new System.EventHandler(this.txtSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 342);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(412, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "说明：如果配置信息有改变，先保存配置信息后点击开始按钮";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbSPP_FT);
            this.groupBox5.Controls.Add(this.rbSpp_ND);
            this.groupBox5.Controls.Add(this.rbSpp_OR);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(16, 159);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(283, 83);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "立压类型：";
            // 
            // rbSPP_FT
            // 
            this.rbSPP_FT.AutoSize = true;
            this.rbSPP_FT.Location = new System.Drawing.Point(188, 39);
            this.rbSPP_FT.Name = "rbSPP_FT";
            this.rbSPP_FT.Size = new System.Drawing.Size(88, 19);
            this.rbSPP_FT.TabIndex = 3;
            this.rbSPP_FT.Text = "滤波信号";
            this.rbSPP_FT.UseVisualStyleBackColor = true;
            this.rbSPP_FT.CheckedChanged += new System.EventHandler(this.rbSPP_FT_CheckedChanged);
            // 
            // rbSpp_ND
            // 
            this.rbSpp_ND.AutoSize = true;
            this.rbSpp_ND.Location = new System.Drawing.Point(99, 39);
            this.rbSpp_ND.Name = "rbSpp_ND";
            this.rbSpp_ND.Size = new System.Drawing.Size(88, 19);
            this.rbSpp_ND.TabIndex = 2;
            this.rbSpp_ND.Text = "隔直信号";
            this.rbSpp_ND.UseVisualStyleBackColor = true;
            this.rbSpp_ND.CheckedChanged += new System.EventHandler(this.rbSpp_ND_CheckedChanged);
            // 
            // rbSpp_OR
            // 
            this.rbSpp_OR.AutoSize = true;
            this.rbSpp_OR.Checked = true;
            this.rbSpp_OR.Location = new System.Drawing.Point(7, 39);
            this.rbSpp_OR.Name = "rbSpp_OR";
            this.rbSpp_OR.Size = new System.Drawing.Size(88, 19);
            this.rbSpp_OR.TabIndex = 1;
            this.rbSpp_OR.TabStop = true;
            this.rbSpp_OR.Text = "原始信号";
            this.rbSpp_OR.UseVisualStyleBackColor = true;
            this.rbSpp_OR.CheckedChanged += new System.EventHandler(this.rbSpp_OR_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 15);
            this.label11.TabIndex = 0;
            // 
            // rbDEP_BOX
            // 
            this.rbDEP_BOX.AutoSize = true;
            this.rbDEP_BOX.Checked = true;
            this.rbDEP_BOX.Location = new System.Drawing.Point(39, 33);
            this.rbDEP_BOX.Name = "rbDEP_BOX";
            this.rbDEP_BOX.Size = new System.Drawing.Size(88, 19);
            this.rbDEP_BOX.TabIndex = 5;
            this.rbDEP_BOX.TabStop = true;
            this.rbDEP_BOX.Text = "采集箱体";
            this.rbDEP_BOX.UseVisualStyleBackColor = true;
            this.rbDEP_BOX.CheckedChanged += new System.EventHandler(this.rbDEP_BOX_CheckedChanged);
            // 
            // rbDEP_PORT
            // 
            this.rbDEP_PORT.AutoSize = true;
            this.rbDEP_PORT.Location = new System.Drawing.Point(137, 33);
            this.rbDEP_PORT.Name = "rbDEP_PORT";
            this.rbDEP_PORT.Size = new System.Drawing.Size(82, 19);
            this.rbDEP_PORT.TabIndex = 6;
            this.rbDEP_PORT.Text = "CMS网络";
            this.rbDEP_PORT.UseVisualStyleBackColor = true;
            this.rbDEP_PORT.CheckedChanged += new System.EventHandler(this.rbDEP_PORT_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbDEP_PORT);
            this.groupBox6.Controls.Add(this.rbDEP_BOX);
            this.groupBox6.Location = new System.Drawing.Point(16, 249);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(283, 80);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "深度来源：";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "GLAS适配器最小化到系统托盘，后台运行！";
            this.notifyIcon1.BalloonTipTitle = "GLAS适配器";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "GLAS适配器";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipShown += new System.EventHandler(this.notifyIcon1_BalloonTipShown);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 423);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSave);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "GLAS地面软件适配器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDeptRecvPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCPLCmdRecvPort;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCPLPort;
        private System.Windows.Forms.TextBox txtCPLIP;
        public System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCMSPort;
        private System.Windows.Forms.TextBox txtCMSIp;
        private System.Windows.Forms.Button txtSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbSPP_FT;
        private System.Windows.Forms.RadioButton rbSpp_ND;
        private System.Windows.Forms.RadioButton rbSpp_OR;
        private System.Windows.Forms.RadioButton rbDEP_PORT;
        private System.Windows.Forms.RadioButton rbDEP_BOX;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ComboBox txtPort;
    }
}

