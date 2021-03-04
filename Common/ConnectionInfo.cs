//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Net.Sockets;
//using System.Net;
//using System.Windows.Forms;
//using 
//namespace Common
//{
//    public class ConnectionInfo
//    {
//        private Action<string> _AppendMethod;
//        private TcpClient _Client;
//        private NetworkStream _Stream;
//        private int _LastReadLength;
//        private byte[] _Buffer = new byte[64];

//        public Action<string> AppendMethod
//        {
//            get { return _AppendMethod; }
//        }
        
//        public TcpClient Client
//        {
//            get { return _Client; }
//        }
        
//        public NetworkStream Stream
//        {
//            get { return _Stream; }
//        }
        
//        public int LastReadLength
//        {
//            get { return _LastReadLength; }
//        }
        
//        public ConnectionInfo(IPAddress address, int port, Action<string> append)
//        {
//            _AppendMethod = append;
//            _Client = new TcpClient();
//            _Client.Connect(address, port);
//            _Stream =_Client.GetStream();
//        }

//        public void AwaitData()
//        {
//            _Stream.BeginRead(_Buffer, 0, _Buffer.Length, DoReadData, this);
//        }

//        public void Close()
//        {
//            if (_Client != null)
//                _Client.Close();
//            _Client = null;
//            _Stream = null;
//        }

//        private void DoReadData(IAsyncResult result)
//        {
//            ConnectionInfo info = (ConnectionInfo)result.AsyncState;
//            try
//            {
//                if (info._Stream != null && info._Stream.CanRead)
//                {
//                    info._LastReadLength = info._Stream.EndRead(result);
//                    if (info._LastReadLength > 0)
//                    {
//                        string message = System.Text.Encoding.ASCII.GetString(info._Buffer);
//                        info._AppendMethod(message);
//                    }
//                    info.AwaitData();
//                }
//            }
//            catch (Exception ex)
//            {
//                info._LastReadLength = -1;
//                info._AppendMethod(ex.Message);
//            }
//        }
//    }

//    public class ClientSide
//    {
//        private ConnectionInfo _Connection;
//        private IPAddress _ServerAddress;

//        public ClientSide()
//        { 
//        }

//        public void sendCom(String txt)
//        {
//            if (_Connection != null && _Connection.Client.Connected && _Connection.Stream != null)
//            {
//                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(txt);
//                _Connection.Stream.Write(buffer, 0, buffer.Length);
//            }
//        }

//        public void serverAddrValidate(String ServerAddrTxt)
//        {
//            _ServerAddress = null;
//            IPHostEntry remoteHost = Dns.GetHostEntry(ServerAddrTxt);
//            if (remoteHost != null && remoteHost.AddressList.Length > 0)
//            {
//                foreach (IPAddress deltaAddress in remoteHost.AddressList)
//                {
//                    if (deltaAddress.AddressFamily == AddressFamily.InterNetwork)
//                    {
//                        _ServerAddress = deltaAddress;
//                        break; // TODO: might not be correct. Was : Exit For
//                    }
//                }
//            }

//            if (_ServerAddress == null)
//            {
//                MessageBox.Show("Cannot resolve server address.", "Invalid Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                //ServerTextBox.SelectAll();
//                //e.Cancel = true;
//            }
//        }

//        public void PortValidate(string _port)
//        {
//            int deltaPort = 0;
            
//            if (!int.TryParse(_port,out deltaPort) || deltaPort < 1 || deltaPort > 65535)
//            {
//                MessageBox.Show("Port number must be an integer between 1 and 65535.", "Invalid Port Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                //PortTextBox.SelectAll();
//                //e.Cancel = true;
//            }
//        }

//        private void InvokeAppendOutput(string message)
//        {
//            Action<string> doAppendOutput = new Action<string>(AppendOutput);
//            //this.Invoke(doAppendOutput, message);
//        }

//        private void AppendOutput(string message)
//        {
//            //if (RichTextBox1.TextLength > 0)
//            //{
//            //    RichTextBox1.AppendText(ControlChars.NewLine);
//            //}
//            //RichTextBox1.AppendText(message);
//            //RichTextBox1.ScrollToCaret();
//        }

//        private void ConnectButton_CheckedChanged(object sender, System.EventArgs e, string port)
//        {
//            ToolStripButton ConnectButton = (ToolStripButton)sender;

//            if (ConnectButton.Checked)
//            {
//                if (_ServerAddress != null)
//                {
//                    ConnectButton.Text = "Disconnect";
//                    //ConnectButton.Image = My.Resources.Disconnect;
//                    try
//                    {
//                        _Connection = new ConnectionInfo(_ServerAddress, Convert.ToInt32(port), InvokeAppendOutput);
//                        _Connection.AwaitData();
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show(ex.Message, "Error Connecting to Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                        ConnectButton.Checked = false;
//                    }
//                }
//                else
//                {
//                    MessageBox.Show("Invalid server name or address.", "Cannot Connect to Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                    ConnectButton.Checked = false;
//                }
//            }
//            else
//            {
//                ConnectButton.Text = "Connect";
//                ConnectButton.Image = "";
//                if (_Connection != null)
//                    _Connection.Close();
//                _Connection = null;
//            }
//        }
//    }

//    public class ServerSide
//    {
//        public ServerSide()
//        {
//        }


//    }
//}