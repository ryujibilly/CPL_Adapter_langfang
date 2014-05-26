using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CPLAdapter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            LoadConfig();
        }
        private void LoadConfig()
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

            rbLine.Checked = (Config.CfgInfo.DisplayMode == 1);
            rbWireLine.Checked = (Config.CfgInfo.DisplayMode == 0);
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

                Config.CfgInfo.DisplayMode = rbLine.Checked ? 1 : 0;
                Config.SaveConfig();
            }
            catch
            {
                MessageBox.Show("保存出错，请检查数据格式是否正确!");
            }
           
        }

        Adapter adp = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            adp = new Adapter();
            string str=adp.Start();
            if (str != null)
            {
                MessageBox.Show(str);
            }
            else
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (adp != null)
            {
                adp.Stop();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (adp != null)
            {
                adp.Stop();
            }
        }

    }
}
