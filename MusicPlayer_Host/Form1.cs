using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPlayer_Service_WCF;

namespace MusicPlayer_Host
{
    public partial class Form1 : Form
    {
        ServiceHost host;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;

            Uri tcpAdrs = new Uri("net.tcp://" + textBoxIP.Text.ToString() + ":" +
                textBoxPort.Text.ToString() + "/");

            Uri httpAdrs = new Uri("http://" + textBoxIP.Text.ToString() + ":" +
                (int.Parse(textBoxPort.Text.ToString()) + 1).ToString() + "/");

            Uri[] baseAdresses = { tcpAdrs, httpAdrs };

            host = new ServiceHost(typeof(Service1), baseAdresses);

            
            try
            {
                host.Open();
            }
            catch (Exception ex)
            {
                labelStatus.Text = ex.Message.ToString();
            }
            finally
            {
                if (host.State == CommunicationState.Opened)
                {
                    labelStatus.Text = "Host otwarto!";
                    buttonStop.Enabled = true;
                   
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (host != null)
            {
                try
                {
                    host.Close();
                }
                catch (Exception ex)
                {
                    labelStatus.Text = ex.Message.ToString();
                }
                finally
                {
                    if (host.State == CommunicationState.Closed)
                    {
                        labelStatus.Text = "Zamknięto!";
                        buttonStart.Enabled = true;
                        buttonStop.Enabled = false;
                    }
                }
            }
        }
    }
}
