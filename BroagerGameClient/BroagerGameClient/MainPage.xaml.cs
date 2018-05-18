using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using System.Net.Sockets;

namespace BroagerGameClient
{
	public partial class MainPage : ContentPage
	{
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);



		public MainPage()
		{
			InitializeComponent();
		}
        private void joinBtn_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("localhost", 8888);
            serverStream = clientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(nameBox.Text);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }
        private void getMessage()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                if (returndata.Equals("enable"))
                {
                    answer1.IsEnabled = true;
                    answer2.IsEnabled = true;
                    answer3.IsEnabled = true;
                    answer4.IsEnabled = true;
                }
                
            }
        }

        private void lockButtons()
        {
            answer1.IsEnabled = false;
            answer2.IsEnabled = false;
            answer3.IsEnabled = false;
            answer4.IsEnabled = false;
        }
        private void answer1_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("1$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            lockButtons();

        }
        private void answer2_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("2$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            lockButtons();
        }
        private void answer3_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("3$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            lockButtons();
        }
        private void answer4_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("4$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            lockButtons();
        }
    }
}
