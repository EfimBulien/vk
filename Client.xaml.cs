using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace ВКанализации
{
    public partial class Client
    {
        public Client()
        {
            InitializeComponent();
        }
        private async void SendMessage(string message, Socket s = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            //await socket.SendAsync(bytes, SocketFlags.None);
        }
        private async void ReceieveMessage()
        {
            while (true)
            {
                byte[] bytes = new byte[65536];
                //await socket.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                MessagesLbx.Items.Add(message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SendMessage(TextTbx.Text);
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
