namespace CPLAdapter
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbWireLine = new System.Windows.Forms.RadioButton();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBaud);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(75, 62);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(100, 21);
            this.txtBaud.TabIndex = 3;
            this.txtBaud.Text = "460800";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(75, 24);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "COM2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "波特率:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "串口号：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 277);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
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
            this.groupBox2.Location = new System.Drawing.Point(217, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 109);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本机网络设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "CPL命令接收端口号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "井深接收端口号：";
            // 
            // txtCPLCmdRecvPort
            // 
            this.txtCPLCmdRecvPort.Location = new System.Drawing.Point(131, 62);
            this.txtCPLCmdRecvPort.Name = "txtCPLCmdRecvPort";
            this.txtCPLCmdRecvPort.Size = new System.Drawing.Size(53, 21);
            this.txtCPLCmdRecvPort.TabIndex = 3;
            this.txtCPLCmdRecvPort.Text = "8002";
            // 
            // txtDeptRecvPort
            // 
            this.txtDeptRecvPort.Location = new System.Drawing.Point(131, 20);
            this.txtDeptRecvPort.Name = "txtDeptRecvPort";
            this.txtDeptRecvPort.Size = new System.Drawing.Size(53, 21);
            this.txtDeptRecvPort.TabIndex = 3;
            this.txtDeptRecvPort.Text = "8001";
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
            this.groupBox3.Location = new System.Drawing.Point(12, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 135);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "远程网络设置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "CMS网络端口：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "CPL网络端口：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "CMS网络IP：";
            // 
            // txtCMSPort
            // 
            this.txtCMSPort.Location = new System.Drawing.Point(95, 104);
            this.txtCMSPort.Name = "txtCMSPort";
            this.txtCMSPort.Size = new System.Drawing.Size(92, 21);
            this.txtCMSPort.TabIndex = 3;
            this.txtCMSPort.Text = "8004";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "CPL网络IP：";
            // 
            // txtCMSIp
            // 
            this.txtCMSIp.Location = new System.Drawing.Point(95, 77);
            this.txtCMSIp.Name = "txtCMSIp";
            this.txtCMSIp.Size = new System.Drawing.Size(92, 21);
            this.txtCMSIp.TabIndex = 3;
            this.txtCMSIp.Text = "192.168.0.4";
            // 
            // txtCPLPort
            // 
            this.txtCPLPort.Location = new System.Drawing.Point(95, 51);
            this.txtCPLPort.Name = "txtCPLPort";
            this.txtCPLPort.Size = new System.Drawing.Size(92, 21);
            this.txtCPLPort.TabIndex = 3;
            this.txtCPLPort.Text = "8003";
            // 
            // txtCPLIP
            // 
            this.txtCPLIP.Location = new System.Drawing.Point(95, 24);
            this.txtCPLIP.Name = "txtCPLIP";
            this.txtCPLIP.Size = new System.Drawing.Size(92, 21);
            this.txtCPLIP.TabIndex = 3;
            this.txtCPLIP.Text = "192.168.0.5";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbWireLine);
            this.groupBox4.Controls.Add(this.rbLine);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(217, 136);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 126);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "其它设置";
            // 
            // rbWireLine
            // 
            this.rbWireLine.AutoSize = true;
            this.rbWireLine.Location = new System.Drawing.Point(141, 25);
            this.rbWireLine.Name = "rbWireLine";
            this.rbWireLine.Size = new System.Drawing.Size(47, 16);
            this.rbWireLine.TabIndex = 4;
            this.rbWireLine.TabStop = true;
            this.rbWireLine.Text = "无线";
            this.rbWireLine.UseVisualStyleBackColor = true;
            // 
            // rbLine
            // 
            this.rbLine.AutoSize = true;
            this.rbLine.Location = new System.Drawing.Point(91, 25);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(47, 16);
            this.rbLine.TabIndex = 4;
            this.rbLine.TabStop = true;
            this.rbLine.Text = "有线";
            this.rbLine.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "显示连接方式：";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(112, 277);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtSave
            // 
            this.txtSave.Location = new System.Drawing.Point(217, 277);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(108, 23);
            this.txtSave.TabIndex = 2;
            this.txtSave.Text = "保存配置信息";
            this.txtSave.UseVisualStyleBackColor = true;
            this.txtSave.Click += new System.EventHandler(this.txtSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 322);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(329, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "说明：如果配置信息有改变，先保存配置信息后点击开始按钮";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 343);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSave);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "CPL中转适配器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCMSPort;
        private System.Windows.Forms.TextBox txtCMSIp;
        private System.Windows.Forms.RadioButton rbWireLine;
        private System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.Button txtSave;
        private System.Windows.Forms.Label label8;
    }
}

