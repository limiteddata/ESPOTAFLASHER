**ESPOTAFLASHER**

This flasher is a implementation of the espota.py(https://github.com/esp8266/Arduino/blob/master/tools/espota.py) in C# and inspired from https://github.com/shehrozeee/espotacs.
This works with both esp32 and esp8266.

how to use :

```.cpp
using ESPOTAFLASHER;

// by default ESPOTA will upload to the remoteport:8266 and localport:12889 for the esp8266
// to set the port use new ESPOTA("ipaddressOrHostname", remoteport, localport, "Location for the firmware");  
ESPOTA updater = new ESPOTA(ipaddressText.Text, firmwareLocationText.Text);   
            
// subscribe to the available events 
updater.progressEvent += (senderx, ex) => progressBar1.Value = ex;
updater.messageEvent += (senderx, ex) =>
{
	infoText.Text += ex + "\n";
	infoText.Refresh();
};

// Update the firmware to the specified address
if (!updater.Update()) MessageBox.Show("Failed to upload the firmware!");
```
