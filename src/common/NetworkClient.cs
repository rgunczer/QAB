using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Global;
using System.Drawing;
using System.Drawing.Imaging;


namespace Util
{
    class NetworkClient
    {
        private Timer timer_connection = null;
        
        private const string QABOT_SERVER_WELCOME_MESSAGE = "Master I am here to serve you!";

        private string m_serverIP;
        private int m_port;

        private Socket server = null;
        private bool suppress = false;

        public NetworkClient(string ip, int port)
        {
            m_serverIP = ip;
            m_port = port;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void UploadScreenshot(Bitmap bmp)
        {
            int newWidth = 400;
            int newHeight = (bmp.Height * 400) / bmp.Width;

            Image img = bmp.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 80L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            
            string fileName = Environment.MachineName + ".jpg";

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, jgpEncoder, myEncoderParameters);
                byte[] buffer = ms.ToArray();
                UploadBufferToFile(buffer, @"screenshots\" + fileName);
            }
        }

        public void UploadBufferToFile(byte[] buffer, string pathOnServer)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip);

                byte[] buf = new byte[1024];
                int received = server.Receive(buf);
                string str = Encoding.UTF8.GetString(buf, 0, received);

                if (QABOT_SERVER_WELCOME_MESSAGE == str)
                {
                    byte[] b = Encoding.UTF8.GetBytes("PUT_FILE|" + pathOnServer);
                    server.Send(b, b.Length, SocketFlags.None);
                    received = server.Receive(buf);
                    str = Encoding.UTF8.GetString(buf, 0, received);

                    if (str == "I am ready to receive file")
                    {
                        using (NetworkStream ns = new NetworkStream(server))
                        {
                            if (ns.CanWrite)
                            {
                                ns.Write(buffer, 0, buffer.Length);
                                ns.Flush();
                            }
                        }
                    }
                    server.Close();                                                           
                }
            }
            catch //(Exception ex)
            {                
                //UtilSys.MessageBox(ex.Message);
            }
        }
        

        public void UploadScreenshot1(Bitmap bmp)
        {
            int newWidth = 400;
            int newHeight = (bmp.Height * 400) / bmp.Width;
            
            Image img = bmp.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            
            // Save the bitmap as a JPG file with zero quality level compression.
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 80L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            Directory.SetCurrentDirectory(Application.StartupPath);
            string fileName = Environment.MachineName + ".jpg";
            
            img.Save(fileName, jgpEncoder, myEncoderParameters);

            System.Threading.Thread.Sleep(1000);

            UploadFileBinary(fileName, @"screenshots\" + fileName, true, false);
        }

        public void RunPilot()
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip);

                byte[] buf = new byte[1024];
                //int received = 0; // server.Receive(buf);
                string str = string.Empty; // Encoding.UTF8.GetString(buf, 0, received);

                //if (QABOT_SERVER_WELCOME_MESSAGE == str)
                {
                    //Scripter.ServerOutput(str);

                    server.Send(Encoding.UTF8.GetBytes("RUN_PILOT"));
                    /*
                                        received = server.Receive(buf);

                                        str = Encoding.UTF8.GetString(buf, 0, received);

                                        if ("PONG" == str)
                                            return true;
                                    }
                                    return false;
                    */
                }
            }
            catch (Exception ex)
            {
                UtilSys.MessageBox(ex.Message);                
            }                                    
        }

        public bool CheckForUpdates(string path)
        {            
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ip);

            byte[] buf = new byte[1024];
            int received = server.Receive(buf);
            string str = Encoding.UTF8.GetString(buf, 0, received);

            if (QABOT_SERVER_WELCOME_MESSAGE == str)
            {
                string msg = "GET_CREATION|" + path;

                byte[] b = Encoding.UTF8.GetBytes(msg);
                server.Send(b, b.Length, SocketFlags.None);

                received = server.Receive(buf);
                str = Encoding.UTF8.GetString(buf, 0, received);

                if ("ERROR" == str)
                {
                    server.Close();
                    UtilSys.MessageBox(str);
                    return false;
                }                               


                FileInfo fi = new FileInfo(path);
                DateTime dtLocal = fi.LastWriteTime;
                
                string temp = dtLocal.Year.ToString() + "-" + dtLocal.Month.ToString() + "-" + dtLocal.Day.ToString() + "-" + dtLocal.Hour.ToString() + "-" + dtLocal.Minute.ToString() + "-" + dtLocal.Second.ToString();

                string[] arrServer = str.Split('-');
                
                DateTime dtServer = new DateTime(Convert.ToInt32(arrServer[0]),  // year
                                                 Convert.ToInt32(arrServer[1]),  // month
                                                 Convert.ToInt32(arrServer[2]),  // day
                                                 Convert.ToInt32(arrServer[3]),  // hour
                                                 Convert.ToInt32(arrServer[4]),  // minute
                                                 Convert.ToInt32(arrServer[5])); // second

                if (dtLocal < dtServer)
                {
                    server.Close();
                    return true;
                }                                                
            }
            server.Close();
            return false;
        }

        public byte[] DownloadFileToByteArray(string serverPath)
        {
            List<byte> lstBuffer = new List<byte>();            
            string msg = "GET_FILE|" + serverPath;

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ip);

            byte[] buf = new byte[1024];
            int received = server.Receive(buf);
            string str = Encoding.UTF8.GetString(buf, 0, received);

            if (QABOT_SERVER_WELCOME_MESSAGE == str)
            {
                byte[] b = Encoding.UTF8.GetBytes(msg);
                server.Send(b, b.Length, SocketFlags.None);

                received = server.Receive(buf);
                str = Encoding.UTF8.GetString(buf, 0, received);

                if ("I am ready to send file" == str)
                {                    
                    byte[] b1 = Encoding.UTF8.GetBytes("OK");
                    server.Send(b1, b1.Length, SocketFlags.None);

                    byte[] buffer = new byte[1024];
                    received = server.Receive(buffer);

                    while (received != 0 && received != -1)
                    {
                        for (int i = 0; i < received; ++i)
                            lstBuffer.Add(buffer[i]);
                                              
                        received = server.Receive(buffer);
                    }
                }
                server.Close();                
            }
            return lstBuffer.ToArray();
        }

        public bool DownloadFile(string serverPath, string savePath)
        {
            string msg = "GET_FILE|" + serverPath;

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ip);

            byte[] buf = new byte[1024];
            int received = server.Receive(buf);
            string str = Encoding.UTF8.GetString(buf, 0, received);

            if (QABOT_SERVER_WELCOME_MESSAGE == str)
            {
                byte[] b = Encoding.UTF8.GetBytes(msg);                
                server.Send(b, b.Length, SocketFlags.None);
                
                received = server.Receive(buf);
                str = Encoding.UTF8.GetString(buf, 0, received);

                if ("I am ready to send file" == str)
                {
                    string temp = string.Empty;
                    string[] arr = savePath.Split('\\');

                    arr[arr.Length - 1] = "";

                    foreach (string item in arr)
                    {
                        if (item != "")
                        {
                            temp += item;

                            if (!Directory.Exists(temp))
                                Directory.CreateDirectory(temp);

                            temp += "\\";
                        }
                    }
                    
                    if (File.Exists(savePath))
                        File.Delete(savePath);

                    byte[] b1 = Encoding.UTF8.GetBytes("OK");
                    server.Send(b1, b1.Length, SocketFlags.None);

                    byte[] buffer = new byte[1024];
                    received = server.Receive(buffer);                    

                    while (received != 0 && received != -1)
                    {                        
                        byte[] bw = new byte[received];

                        for (int i = 0; i < received; ++i)
                            bw[i] = buffer[i];                                                

                        using (FileStream stream = new FileStream(savePath , FileMode.Create | FileMode.Append))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(bw);                            
                                writer.Close();
                            }
                        }
                        received = server.Receive(buffer);                       
                    }
                }
                server.Close();                
            }

            return true;
        }

        public List<string> GetSyncFolderStructure()
        {
            List<string> list = new List<string>();
            string buffer = string.Empty;

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ip);
            
            byte[] buf = new byte[1024];
            int received = server.Receive(buf);
            string str = Encoding.UTF8.GetString(buf, 0, received);

            if (QABOT_SERVER_WELCOME_MESSAGE == str)
            {
                byte[] b = Encoding.UTF8.GetBytes("GET_SYNC_FOLDER_STRUCTURE");
                server.Send(b, b.Length, SocketFlags.None);
                                                               
                while(received != 0 && received != -1)
                {
                    received = server.Receive(buf);
                    str = Encoding.UTF8.GetString(buf, 0, received);
                    //Scripter.ServerOutput(str);
                    buffer += str;
                }

                string[] arr = buffer.Split('|');

                foreach (string item in arr)
                {
                    list.Add(item);
                }
                return list;
            }            
            return null;
        }

        public void Test()
        {
            try
            {
                if (null != timer_connection)
                    return;

                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP.Trim()), m_port);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.RemoteEndPoint = ip;
                e.UserToken = server;
                e.Completed += new EventHandler<SocketAsyncEventArgs>(e_Completed);
                
                server.ConnectAsync(e);

                if (timer_connection != null)
                {
                    timer_connection.Dispose();
                }
                else
                {
                    timer_connection = new Timer();
                }
                suppress = false;
                timer_connection.Interval = 2000;
                timer_connection.Tick += new EventHandler(timer_connection_Tick);
                timer_connection.Start();                
            }
            catch (SocketException ex)
            {
                UtilSys.MessageBox(ex.Message);                
            }                                    
        }

        void timer_connection_Tick(object sender, EventArgs e)
        {
            if (suppress)
                return;

            if (!server.Connected)
            {
                if (null != timer_connection)
                    timer_connection.Stop();

                MessageBox.Show("Connection Timeout", "timer");

                if (null != server)
                    server.Close();

                server = null;
            }            
        }

        void e_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (null == server)
                return;

            if (!server.Connected)
            {
                suppress = true;
                timer_connection.Stop();
                timer_connection.Dispose();
                timer_connection = null;

                if (null != server)
                    server.Close();

                server = null;

                UtilSys.MessageBox("Connection Timeout");
                return;
            }            

            //Scripter.ServerOutput("Connection Established");

            byte[] buf = new byte[128];
            int received = server.Receive(buf);
            string msg = Encoding.UTF8.GetString(buf, 0, received);

            if (QABOT_SERVER_WELCOME_MESSAGE == msg)
            {
                //Scripter.ServerOutput(msg);
                byte[] b = Encoding.UTF8.GetBytes("ping");

                server.Send(b, b.Length, SocketFlags.None);
                received = server.Receive(buf);

                msg = Encoding.UTF8.GetString(buf, 0, received);

                if ("PONG" == msg)
                {
                    timer_connection.Stop();
                    timer_connection.Dispose();
                    timer_connection = null;
                    MessageBox.Show("Successful connection to the server at [" + e.RemoteEndPoint + "]", "QABOT-Scripter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    
            }            
        }

        public void UploadFileBinary(string path, string downloadTo, bool silent, bool decoreteServerPath)
        {
            string szPath = downloadTo;
            
            if (decoreteServerPath)
                szPath = path.Replace(downloadTo + "\\", "");       

            if (!File.Exists(path))
            {
                if (!silent)
                    UtilSys.MessageBox("Unable to upload\nFile does not exist: [" + path + "]");
                return;
            }

            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip);

                byte[] buf = new byte[1024];
                int received = server.Receive(buf);
                string str = Encoding.UTF8.GetString(buf, 0, received);

                if (QABOT_SERVER_WELCOME_MESSAGE == str)
                {                    
                    byte[] b = Encoding.UTF8.GetBytes("PUT_FILE|" + szPath);                    
                    server.Send(b, b.Length, SocketFlags.None);
                    received = server.Receive(buf);
                    str = Encoding.UTF8.GetString(buf, 0, received);

                    if (str == "I am ready to receive file")
                    {                        
                        using (NetworkStream ns = new NetworkStream(server))
                        {                            
                            if (ns.CanWrite)
                            {
                                byte[] bbb = File.ReadAllBytes(path);
                                ns.Write(bbb, 0, bbb.Length);
                                ns.Flush();
                            }
                        }                                                
                    }
                    server.Close();

                    if (!silent)
                        UtilSys.MessageBoxInfo("Successfully uploaded file: " + path);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    UtilSys.MessageBox(ex.Message);
            }
        }

        public void UploadFile(string fileName, string fileData)
        {
            const string FILE_UPLOAD = "FILE_UPLOAD";
            const string FILE_UPLOAD_COMPLETED = "FILE_UPLOAD_COMPLETED";

            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(m_serverIP), m_port);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip);

                byte[] buf = new byte[1024];
                int received = server.Receive(buf);
                string str = Encoding.UTF8.GetString(buf, 0, received);

                if (QABOT_SERVER_WELCOME_MESSAGE == str)
                {
                    //Scripter.ServerOutput(str);

                    byte[] b = Encoding.ASCII.GetBytes(FILE_UPLOAD);                    
                    server.Send(b, b.Length, SocketFlags.None);
                    received = server.Receive(buf);
                    str = Encoding.UTF8.GetString(buf, 0, received);

                    if (str == "I am ready to receive file")
                    {
                        //Scripter.ServerOutput(str);

                        using (NetworkStream ns = new NetworkStream(server))
                        {
                            string input = "<File=" + fileName + ">\n" + fileData + "\n<FileEnd>";

                            if (ns.CanWrite)
                            {
                                ns.Write(Encoding.UTF8.GetBytes(input), 0, input.Length);
                                ns.Flush();
                            }
                        }

                        byte[] b1 = Encoding.ASCII.GetBytes(FILE_UPLOAD_COMPLETED);
                        server.Send(b1, b1.Length, SocketFlags.None);
                    }
                    else
                        UtilSys.MessageBox("Server is unable to receive file");

                    server.Close();
                }
            }
            catch (Exception ex)
            {
                UtilSys.MessageBox(ex.Message);
            }
        }

    }
}