using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GLAS_Adapter
{
    public enum SppType { OR,ND,FT};
    public enum DepType { CMS,CPL};
    public partial class Form1 : Form
    {
        public bool adpRun { set; get; }
        public  Adapter adp{get;set;}
        private DepType depType { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        public void ChangeToCPL()
        {
            depType = DepType.CPL;
        }
        public void ChangeToCMS()
        {
            depType = DepType.CMS;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            adpRun = false;
            txtPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            depType = DepType.CPL;
            //rbDEP_BOX.Checked = true;
            //rbSpp_OR.Checked = true;
            LoadConfig();
        }
        private void LoadConfig()//配置文件  Xml
        {
            Config.GetConfig();
            txtPort.Text = Config.CfgInfo.ComPortNum;
            txtBaud.Text = Config.CfgInfo.BaudRate.ToString();

            txtDeptRecvPort.Text = Config.CfgInfo.LocalDeptPort.ToString();
            txtCPLCmdRecvPort.Text = Config.CfgInfo.LocalCmdRecvPort.ToString();

            txtCPLIP.Text = Config.CfgInfo.CPLWitsIP;
            txtCPLPort.Text = Config.CfgInfo.CPLWitsPort.ToString();
            txtCMSIp.Text = Config.CfgInfo.CMSWitsRecvIP;
            txtCMSPort.Text = Config.CfgInfo.CMSWitsRecvPort.ToString();

            //rbLine.Checked = (Config.CfgInfo.DisplayMode == 1);
            //rbWireLine.Checked = (Config.CfgInfo.DisplayMode == 0);
        }

        private void txtSave_Click(object sender, EventArgs e)
        {

            try
            {
                Config.CfgInfo.ComPortNum = txtPort.Text.Trim();
                Config.CfgInfo.BaudRate = int.Parse(txtBaud.Text.Trim());

                Config.CfgInfo.LocalDeptPort = int.Parse(txtDeptRecvPort.Text.Trim());
                Config.CfgInfo.LocalCmdRecvPort = int.Parse(txtCPLCmdRecvPort.Text.Trim());

                Config.CfgInfo.CPLWitsIP = txtCPLIP.Text.Trim();
                Config.CfgInfo.CPLWitsPort = int.Parse(txtCPLPort.Text.Trim());
                Config.CfgInfo.CMSWitsRecvIP = txtCMSIp.Text.Trim();
                Config.CfgInfo.CMSWitsRecvPort = int.Parse(txtCMSPort.Text.Trim());

                //Config.CfgInfo.DisplayMode = rbLine.Checked ? 1 : 0;
                Config.SaveConfig();
            }
            catch
            {
                MessageBox.Show("保存出错，请检查数据格式是否正确!");
            }
           
        }


        public void btnStart_Click(object sender, EventArgs e)//开始   调用ADAPTER
        {
            try
            {
                adp = new Adapter(txtPort.Text,depType);
                adp.Owner = this;//Adapter's Owner-"Form1 Object"
                string str = adp.Start();
                adpRun = true;
                adp.BeginWork();
                adp.gBox.ClearQueue();
                adp.tcpServer.Owner = adp;
                if (str != null)
                {
                    MessageBox.Show(str);
                }
                else
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    groupBox5.Enabled = true;
                    groupBox6.Enabled = false;
                }
                //Notify error
            }
            catch(Exception ex)
            {
                adpRun = false;
                MessageBox.Show(ex.Message);
            }
        }

        public void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (adp != null)
                {
                    adp.Stop();
                    adpRun = false;
                    adp.Dispose();
                    MessageBox.Show("停止后请关闭深度跟踪!");
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    groupBox5.Enabled = false;
                    groupBox6.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (adp != null)
            {
                adp.Stop();
                notifyIcon1.Visible = false;
            }
        }

        private void rbDEP_BOX_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = false;
            groupBox1.Enabled = true;
            ChangeToCPL();
        }

        private void rbDEP_PORT_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox3.Enabled = true;
            ChangeToCMS();
        }

        private void rbSpp_OR_CheckedChanged(object sender, EventArgs e)
        {
            adp.gBox.ChangeToOR();
        }

        private void rbSpp_ND_CheckedChanged(object sender, EventArgs e)
        {
            adp.gBox.ChangeToND();
        }

        private void rbSPP_FT_CheckedChanged(object sender, EventArgs e)
        {
            adp.gBox.ChangeToFT();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)    //最小化到系统托盘
            {
                notifyIcon1.Visible = true;    //显示托盘图标
                this.Hide();    //隐藏窗口
                this.ShowInTaskbar = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (adpRun)
            {
                //注意判断关闭事件Reason来源于窗体按钮，否则用菜单退出时无法退出!
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;    //取消"关闭窗口"事件
                    this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
                    notifyIcon1.Visible = true;
                    this.Hide();
                    return;
                }
            }
            else if (!adpRun)
            {
                this.Dispose();
                this.Close();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(2000);
        }

    }
}
