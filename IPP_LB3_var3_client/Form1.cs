using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPP_LB3_var3_client
{
    public partial class Form1 : Form
    {
        static public string str = "";
        public Form1()
        {
            InitializeComponent();
        }
        
        Task clientSend = new Task(async () =>
        {
            IPAddress ipAddress = new IPAddress(new byte[] { 192, 168, 0, 157 });
            IPEndPoint ipEndPoint = new(ipAddress, 443); 
            using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(ipEndPoint); 
            var message = str;
            var messageBytes = Encoding.UTF8.GetBytes(message); 
            await client.SendAsync(messageBytes, SocketFlags.None);
            client.Close();
        });

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            str = vScrollBar1.Value.ToString() + "," + vScrollBar2.Value.ToString() + "," + vScrollBar3.Value.ToString();
            clientSend.Start();

        }
    }
}
