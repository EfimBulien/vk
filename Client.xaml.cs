using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace ВКанализации
{
    public partial class Client
    {
        public static Socket socket;
        public Client()
        {
            InitializeComponent();
            SendMessage("/connect " + MainWindow.user.Name);
            ReceieveMessage();
        }
        private async void SendMessage(string message, bool close = true)
        {
            if (message == "/disconnect" && close)
            {
                ExitButton_OnClick(null, null);
                return;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(bytes, SocketFlags.None);
        }
        private async void ReceieveMessage()
        {
            while (true)
            {
                byte[] bytes = new byte[65536];
                string default_message = Encoding.UTF8.GetString(bytes);
                try
                {
                    await socket.ReceiveAsync(bytes, SocketFlags.None);
                }
                catch
                {
                    MessageBox.Show("Ошибка: потеряно соединение с сервером");
                    Exit();
                    return;
                }
                string message = Encoding.UTF8.GetString(bytes);
                if (message != default_message)
                {
                    string[] user = Server.GetUser(message);
                    string disconnect = "/disconnect";
                    bool isDisconnect = true;
                    for (int i = 0; i < disconnect.Length; i++)
                    {
                        if (disconnect[i] != user[1][i])
                        {
                            isDisconnect = false;
                            break;
                        }
                    }
                    if (isDisconnect)
                    {
                        MessageBox.Show("Ошибка: пользователь с таким именем уже есть на сервере");
                        Exit();
                        return;
                    }
                    if (user[0] == "sys")
                    {
                        string[] users = user[1].Split(',');
                        string users_string = "";
                        foreach (string user_string in users)
                        {
                            users_string += user_string + " ";
                        }
                        UsersListBox.ItemsSource = users;
                        continue;
                    }
                    if (user.Length > 1)
                    {
                        if (user[1][0] == '/')
                        {
                            continue;
                        }
                    }
                    MessagesLbx.Items.Add(message);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(MessageTbx.Text);
            MessageTbx.Text = string.Empty;
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            SendMessage(MainWindow.user.Name + " >> " + "/disconnect", false);
            Exit();
        }
        private void Exit()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            socket.Close();
            Close();
        }
    }
}