using System.Net.NetworkInformation;
using System.Windows;

namespace ВКанализации
{
    public partial class MainWindow
    {
        //Текущий ip
        private static readonly string? Ip = GetLocalIpAddress();
        private const string EmptyField = "";
        private User? _user;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        //Username, IP_Address - TextBox

        //Создать новый чат
        private void CreateChat(object sender, RoutedEventArgs e)
        {
            if (Username.Text == EmptyField) MessageBox.Show("Заполните имя пользователя!");
            else
            { 
                _user = new User(Username.Text, Ip);
                //user.Name = Username.Text;
                var full = _user.Name + _user.Ip;
                MessageBox.Show(full);
                var server = new Server();
                server.Show();
            }
        }
        //Присоединиться к существующему чату
        private void JoinChat(object sender, RoutedEventArgs e)
        {
            if (Username.Text == EmptyField && IpAddress.Text == EmptyField) MessageBox.Show("Проверьте заполненность имени или введенный IP адрес");
            else
            {
                _user = new User(Username.Text, IpAddress.Text);
                //user.Name = Username.Text;
                //user.IP = IpAddress.Text;
                var full = _user.Name + _user.Ip;
                MessageBox.Show(full);
                var client = new Client();
                client.Show();
            }
        }

        private static string? GetLocalIpAddress()
        {
            // Получаем все сетевые интерфейсы на компьютере
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            // Проходимся по каждому интерфейсу
            return (from networkInterface in networkInterfaces where networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback select networkInterface.GetIPProperties() into properties from unicastAddress in properties.UnicastAddresses where unicastAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork select unicastAddress.Address.ToString()).FirstOrDefault();
        }
    }
}