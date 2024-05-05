using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace ВКанализации
{
    public partial class Server
    {
        Socket socket;
        List<Socket> users = [];
        public Server()
        {
            InitializeComponent();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Thread listen = new(() =>
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, 7000));
                socket.Listen(100);
                ListenToUsers();
            });
            listen.Start();
            ReceieveMessage();
        }
        private async void ListenToUsers()
        {
            while (true)
            {
                var client = await socket.AcceptAsync();
                users.Add(client);
                ReceieveMessage(client);
            }
        }
        private void ReceieveMessage()
        {
            
        }
        private async void ReceieveMessage(Socket s)
        {
            while (true)
            {
                byte[] bytes = new byte[65536];
                await s.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                MessagesLbx.Items.Add(message);
                foreach (var item in users)
                {
                    SendMessage(message, item);
                }
            }
        }
        private static async void SendMessage(string message, Socket c)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await c.SendAsync(bytes, SocketFlags.None);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
