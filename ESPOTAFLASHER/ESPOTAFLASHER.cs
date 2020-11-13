using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESPOTAFLASHER
{
    public partial class ESPOTAFLASHER : Form
    {
        public ESPOTAFLASHER()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Firmware location";
                dialog.Filter = "Firmware (*.bin)|*.BIN";
                if (dialog.ShowDialog() == DialogResult.OK)
                    firmwareLocationText.Text = dialog.FileName;
            }
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            // check all the inputs
            if (firmwareLocationText.Text == "")
            {
                MessageBox.Show("Please enter of the firmware!");
                return;
            }
            if (ipaddressText.Text == "")
            {
                MessageBox.Show("Please enter the ipaddress/hostname!");
                return;
            }
            // reset fields
            infoText.Text = "";
            progressBar1.Value = 0;
            // lock temporarily all the controlls
            foreach (Control control in this.Controls)
            {
                if(control.Name != "infoText")
                control.Enabled = false;
            }
            // by default ESPOTA will upload to the remoteport:8266 and localport:12889 for the esp8266
            // to set the port use new ESPOTA("ipaddressOrHostname", remoteport, localport, "Location for the firmware");  
            ESPOTA updater = new ESPOTA(ipaddressText.Text, firmwareLocationText.Text);   
            progressBar1.Maximum = updater.content_size;
            
            // subscribe to the available events 
            updater.progressEvent += (senderx, ex) => progressBar1.Value = ex;
            updater.messageEvent += (senderx, ex) =>
            {
                infoText.Text += ex + "\n";
                infoText.Refresh();
            };

            // Update the firmware to the specified address
            if (!updater.Update()) MessageBox.Show("Failed to upload the firmware!");

            // unlock all the controlls
            foreach (Control control in this.Controls) control.Enabled = Enabled;
        }
    }
}
