using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ESPOTAFLASHER
{
    class ESPOTA
    {
        public event EventHandler<string> messageEvent;
        public event EventHandler<int> progressEvent;
        public enum FLASH_Options
        {
            FLASH = 0,
            SPIFFS = 100,
            AUTH = 200,
        }
        public int content_size;
        private TcpListener listener;
        private string _remoteAddr;
        private int _remotePort = 8266;
        private int _localPort = 12889;
        private string _FileName;
        private byte[] message_bytes;
        private byte[] file_data;
        private IPAddress deviceIp;

        public ESPOTA(string remoteAddr, int remotePort, int localPort, string FileName, FLASH_Options command = FLASH_Options.FLASH)
        {
            _remoteAddr = remoteAddr;
            _remotePort = remotePort;
            _localPort = localPort;
            _FileName = FileName;
            readFile();
        }
        public ESPOTA(string remoteAddr, string FileName)
        {
            _remoteAddr = remoteAddr;
            _FileName = FileName;
            readFile();
        }
        private void readFile()
        {
            try
            {
                file_data = File.ReadAllBytes(_FileName);
                content_size = file_data.Length;
                if (file_data == null && content_size == 0)
                {
                    messageEvent?.Invoke(this, "Failed to read the file!");
                }
            }
            catch
            {
                messageEvent?.Invoke(this, "Failed to read the file!");
                throw;
            }
        }
        public bool Update()
        {
            messageEvent?.Invoke(this, $"Uploading: {_FileName}");

            if (_remoteAddr.Contains(".local"))
            {
                try
                {
                    deviceIp = Dns.GetHostEntry(_remoteAddr).AddressList[0];
                    messageEvent?.Invoke(this, $"Detected a hostname with the ip: {deviceIp}");
                }
                catch
                {
                    messageEvent?.Invoke(this, "Failed to parse the hostname!");
                    return false;
                }
            }
            else
            {
                try
                {
                    deviceIp = IPAddress.Parse(_remoteAddr);
                    messageEvent?.Invoke(this, $"Uploading to : {deviceIp}");
                }
                catch (System.FormatException)
                {
                    messageEvent?.Invoke(this, "Failed to parse the ipaddress!");
                    return false;
                }
            }
            message_bytes = Encoding.ASCII.GetBytes(string.Format("0 {1} {2} {3}\n", FLASH_Options.FLASH, _localPort, content_size, CreateMD5(file_data)));

            IPAddress address = IPAddress.Any;

            listener = new TcpListener(address, _localPort);
            listener.Server.NoDelay = true;
            listener.Start();

            messageEvent?.Invoke(this, $"Listening to TCP clients at 0.0.0.0:{_localPort}");
            messageEvent?.Invoke(this, $"Upload size {content_size}");
            messageEvent?.Invoke(this, "Sending invitation to " + _remoteAddr);
            UdpClient sock2 = new UdpClient();
            try
            {
                sock2.Send(message_bytes, message_bytes.Length, new IPEndPoint(deviceIp, _remotePort));
            }
            catch (System.Net.Sockets.SocketException)
            {
                messageEvent?.Invoke(this, "Failed to reach the ipaddress");
                return false;
            }

            var t = sock2.ReceiveAsync();
            t.Wait(10000);
            var res = t.Result;
            if (res == null)
            {
                messageEvent?.Invoke(this, "No Response");
                {
                    listener.Stop();
                    return false;
                }
            }
            var res_text = Encoding.ASCII.GetString(res.Buffer);
            if (res_text != "OK")
            {
                messageEvent?.Invoke(this, "AUTH requeired and not implemented");
                {
                    listener.Stop();
                    return false;
                }
            }
            sock2.Close();
            messageEvent?.Invoke(this, "Waiting for device ...");
            listener.Server.SendTimeout = 10000;
            listener.Server.ReceiveTimeout = 10000;
            DateTime startTime = DateTime.Now;
            TcpClient client = null;
            messageEvent?.Invoke(this, "Awaiting Connection");
            while ((DateTime.Now - startTime).TotalSeconds < 10)
            {
                if (listener.Pending())
                {

                    client = listener.AcceptTcpClient();
                    client.NoDelay = true;
                    break;
                }
                else
                    Thread.Sleep(10);
            }
            if (client == null)
            {
                messageEvent?.Invoke(this, "No response from device");
                {
                    listener.Stop();
                    return false;
                }
            }
            messageEvent?.Invoke(this, "Got Connection");
            using (MemoryStream fs = new MemoryStream(file_data))
            {
                int offset = 0;
                byte[] chunk = new byte[1460];
                int chunk_size = 0;
                int read_count = 0;
                string resp = "";
                messageEvent?.Invoke(this, "Started writing");
                while (content_size > offset)
                {
                    chunk_size = fs.Read(chunk, 0, 1460);
                    offset += chunk_size;
                    if (client.Available > 0)
                    {
                        resp = Encoding.ASCII.GetString(chunk, 0, read_count);
                        Console.Write(resp);
                    }
                    progressEvent?.Invoke(this, offset);
                    client.Client.Send(chunk, 0, chunk_size, SocketFlags.None);
                    client.ReceiveTimeout = 10000;

                }
                client.ReceiveTimeout = 60000;
                read_count = client.Client.Receive(chunk);
                resp = Encoding.ASCII.GetString(chunk, 0, read_count);
                while (!resp.Contains("O"))
                {
                    if (resp.Contains("E"))
                    {
                        client.Close();
                        listener.Stop();
                        return false;
                    }
                    read_count = client.Client.Receive(chunk);
                    resp = Encoding.ASCII.GetString(chunk, 0, read_count);
                }
                messageEvent?.Invoke(this, "Done...");
                client.Close();
                listener.Stop();
                return true;
            }
        }
        private string CreateMD5(byte[] input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(input);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++) sb.Append(hashBytes[i].ToString("x2"));
                return sb.ToString();
            }
        }
    }
}