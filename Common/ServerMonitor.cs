//using Microsoft.VisualBasic;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Common
//{

//    public class ServerMonitor
//    {
//        private TcpListener _Listener;
//        private List<ConnectionInfo> _Connections = new List<ConnectionInfo>();

//        private Task _ConnectionMontior;
//        private void StartStopButton_CheckedChanged(System.Object sender, System.EventArgs e)
//        {
//            if (StartStopButton.Checked)
//            {
//                StartStopButton.Text = "Stop";
//                StartStopButton.Image = My.Resources.Resources.StopServer;
//                _Listener = new TcpListener(IPAddress.Any, Convert.ToInt32(PortTextBox.Text));
//                _Listener.Start();
//                MonitorInfo monitor = new MonitorInfo(_Listener, _Connections);
//                ListenForClient(monitor);
//                _ConnectionMontior = Task.Factory.StartNew(DoMonitorConnections, monitor, TaskCreationOptions.LongRunning);
//            }
//            else
//            {
//                StartStopButton.Text = "Start";
//                StartStopButton.Image = My.Resources.Resources.StartServer;
//                ((MonitorInfo)_ConnectionMontior.AsyncState).Cancel = true;
//                _Listener.Stop();
//                _Listener = null;
//            }
//        }

//        private void PortTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
//        {
//            int deltaPort = 0;
//            if (!int.TryParse(PortTextBox.Text, deltaPort) || deltaPort < 1 || deltaPort > 65535)
//            {
//                MessageBox.Show("Port number must be an integer between 1 and 65535.", "Invalid Port Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                PortTextBox.SelectAll();
//                e.Cancel = true;
//            }
//        }

//        private void ListenForClient(MonitorInfo monitor)
//        {
//            ConnectionInfo info = new ConnectionInfo(monitor);
//            _Listener.BeginAcceptTcpClient(DoAcceptClient, info);
//        }

//        private void DoAcceptClient(IAsyncResult result)
//        {
//            MonitorInfo monitorInfo = (MonitorInfo)_ConnectionMontior.AsyncState;
//            if (monitorInfo.Listener != null && !monitorInfo.Cancel)
//            {
//                ConnectionInfo info = (ConnectionInfo)result.AsyncState;
//                monitorInfo.Connections.Add(info);
//                info.AcceptClient(result);
//                ListenForClient(monitorInfo);
//                info.AwaitData();
//                Action doUpdateConnectionCountLabel = new Action(UpdateConnectionCountLabel);
//                Invoke(doUpdateConnectionCountLabel);
//            }
//        }

//        private void DoMonitorConnections()
//        {
//            //Create delegate for updating output display
//            Action<string> doAppendOutput = new Action<string>(AppendOutput);
//            //Create delegate for updating connection count label
//            Action doUpdateConnectionCountLabel = new Action(UpdateConnectionCountLabel);

//            //Get MonitorInfo instance from thread-save Task instance
//            MonitorInfo monitorInfo = (MonitorInfo)_ConnectionMontior.AsyncState;

//            //Report progress
//            this.Invoke(doAppendOutput, "Monitor Started.");

//            //Implement client connection processing loop
//            do
//            {
//                //Create temporary list for recording closed connections
//                int lostCount = 0;
//                //Examine each connection for processing
//                for (int index = monitorInfo.Connections.Count - 1; index >= 0; index += -1)
//                {
//                    ConnectionInfo info = monitorInfo.Connections(index);
//                    if (info.Client.Connected)
//                    {
//                        //Process connected client
//                        if (info.DataQueue.Count > 0)
//                        {
//                            //The code in this If-Block should be modified to build 'message' objects
//                            //according to the protocol you defined for your data transmissions.
//                            //This example simply sends all pending message bytes to the output textbox.
//                            //Without a protocol we cannot know what constitutes a complete message, so
//                            //with multiple active clients we could see part of client1's first message,
//                            //then part of a message from client2, followed by the rest of client1's
//                            //first message (assuming client1 sent more than 64 bytes).
//                            List<byte> messageBytes = new List<byte>();
//                            while (info.DataQueue.Count > 0)
//                            {
//                                byte value = 0;
//                                if (info.DataQueue.TryDequeue(value))
//                                {
//                                    messageBytes.Add(value);
//                                }
//                            }
//                            this.Invoke(doAppendOutput, System.Text.Encoding.ASCII.GetString(messageBytes.ToArray));
//                        }
//                    }
//                    else
//                    {
//                        //Clean-up any closed client connections
//                        monitorInfo.Connections.Remove(info);
//                        lostCount += 1;
//                    }
//                }
//                if (lostCount > 0)
//                {
//                    Invoke(doUpdateConnectionCountLabel);
//                }

//                //Throttle loop to avoid wasting CPU time
//                _ConnectionMontior.Wait(1);
//            } while (!monitorInfo.Cancel);

//            //Close all connections before exiting monitor
//            foreach (ConnectionInfo info in monitorInfo.Connections)
//            {
//                info.Client.Close();
//            }
//            monitorInfo.Connections.Clear();

//            //Update the connection count label and report status
//            Invoke(doUpdateConnectionCountLabel);
//            this.Invoke(doAppendOutput, "Monitor Stopped.");
//        }

//        private void AppendOutput(string message)
//        {
//            if (RichTextBox1.TextLength > 0)
//            {
//                RichTextBox1.AppendText(ControlChars.NewLine);
//            }
//            RichTextBox1.AppendText(message);
//            RichTextBox1.ScrollToCaret();
//        }

//        private void UpdateConnectionCountLabel()
//        {
//            ConnectionCountLabel.Text = string.Format("{0} Connections", _Connections.Count);
//        }
//    }

//    //Provides a simple container object to be used for the state object passed to the connection monitoring thread
//    public class MonitorInfo
//    {
//        public bool Cancel { get; set; }

//        private List<ConnectionInfo> _Connections;
//        public List<ConnectionInfo> Connections
//        {
//            get { return _Connections; }
//        }

//        private TcpListener _Listener;
//        public TcpListener Listener
//        {
//            get { return _Listener; }
//        }

//        public MonitorInfo(TcpListener tcpListener, List<ConnectionInfo> connectionInfoList)
//        {
//            _Listener = tcpListener;
//            _Connections = connectionInfoList;
//        }
//    }

//    //Provides a container object to serve as the state object for async client and stream operations
//    public class ConnectionInfo
//    {
//        //hold a reference to entire monitor instead of just the listener
//        private MonitorInfo _Monitor;
//        public MonitorInfo Monitor
//        {
//            get { return _Monitor; }
//        }

//        private TcpClient _Client;
//        public TcpClient Client
//        {
//            get { return _Client; }
//        }

//        private NetworkStream _Stream;
//        public NetworkStream Stream
//        {
//            get { return _Stream; }
//        }

//        private System.Collections.Concurrent.ConcurrentQueue<byte> _DataQueue;
//        public System.Collections.Concurrent.ConcurrentQueue<byte> DataQueue
//        {
//            get { return _DataQueue; }
//        }

//        private int _LastReadLength;
//        public int LastReadLength
//        {
//            get { return _LastReadLength; }
//        }

//        //The buffer size is an arbitrary value which should be selected based on the
//        //amount of data you need to transmit, the rate of transmissions, and the
//        //anticipalted number of clients. These are the considerations for designing
//        //the communicaition protocol for data transmissions, and the size of the read
//        //buffer must be based upon the needs of the protocol.

//        private byte[] _Buffer = new byte[64];
//        public ConnectionInfo(MonitorInfo monitor)
//        {
//            _Monitor = monitor;
//            _DataQueue = new System.Collections.Concurrent.ConcurrentQueue<byte>();
//        }

//        public void AcceptClient(IAsyncResult result)
//        {
//            _Client = _Monitor.Listener.EndAcceptTcpClient(result);
//            if (_Client != null && _Client.Connected)
//            {
//                _Stream = _Client.GetStream;
//            }
//        }

//        public void AwaitData()
//        {
//            _Stream.BeginRead(_Buffer, 0, _Buffer.Length, DoReadData, this);
//        }

//        private void DoReadData(IAsyncResult result)
//        {
//            ConnectionInfo info = (ConnectionInfo)result.AsyncState;
//            try
//            {
//                //If the stream is valid for reading, get the current data and then
//                //begin another async read
//                if (info.Stream != null && info.Stream.CanRead)
//                {
//                    info._LastReadLength = info.Stream.EndRead(result);
//                    for (int index = 0; index <= _LastReadLength - 1; index++)
//                    {
//                        info._DataQueue.Enqueue(info._Buffer(index));
//                    }

//                    //The example responds to all data reception with the number of bytes received;
//                    //you would likely change this behavior when implementing your own protocol.
//                    info.SendMessage("Received " + info._LastReadLength + " Bytes");

//                    foreach (ConnectionInfo otherInfo in info.Monitor.Connections)
//                    {
//                        if ((!object.ReferenceEquals(otherInfo, info)))
//                        {
//                            otherInfo.SendMessage(System.Text.Encoding.ASCII.GetString(info._Buffer));
//                        }
//                    }

//                    info.AwaitData();
//                }
//                else
//                {
//                    //If we cannot read from the stream, the example assumes the connection is
//                    //invalid and closes the client connection. You might modify this behavior
//                    //when implementing your own protocol.
//                    info.Client.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                info._LastReadLength = -1;
//            }
//        }

//        private void SendMessage(string message)
//        {
//            if (_Stream != null)
//            {
//                byte[] messageData = System.Text.Encoding.ASCII.GetBytes(message);
//                Stream.Write(messageData, 0, messageData.Length);
//            }
//        }
//    }
//}