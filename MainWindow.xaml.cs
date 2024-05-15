using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;

namespace ВКанализации
{
    public partial class MainWindow
    {
        private const string EmptyField = "";
        public static User? user;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateChat(object sender, RoutedEventArgs e)
        {
            if (Username.Text == EmptyField)
            {
                MessageBox.Show("Заполните имя пользователя!");
                return;
            }
            else if (Username.Text == "sys")
            {
                MessageBox.Show("Имя пользователя не может быть 'sys'");
                return;
            }
            else if (!Regex.IsMatch(Username.Text, @"^[a-zA-Z0-9]+$")) 
            {
                MessageBox.Show("Имя пользователя может содержать только буквы и цифры");
                return;
            }
            else
            {
                user = new User(Username.Text);
                var server = new Server();
                server.Show();
                Close();
            }
        }
        private void JoinChat(object sender, RoutedEventArgs e)
        {
            if (Username.Text == EmptyField || IpAddress.Text == "")
            {
                MessageBox.Show("Проверьте заполненность имени или введенный IP адрес");
                return;
            }
            else if (Username.Text == "sys")
            {
                MessageBox.Show("Имя пользователя не может быть 'sys'");
                return;
            }
            else if (!Regex.IsMatch(Username.Text, @"^[a-zA-Z0-9]+$")) 
            {
                MessageBox.Show("Имя пользователя может содержать только буквы и цифры");
                return;
            }
            else if (!IPAddress.TryParse(IpAddress.Text, out _))
            {
                MessageBox.Show("Неверный адрес");
                return;
            }
            else
            {
                user = new User(Username.Text, IpAddress.Text);
                Client.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    Client.socket.Connect(IpAddress.Text, 7000);
                    var client = new Client();
                    client.Show();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось подключиться к серверу");
                }
            }
        }
    }
}