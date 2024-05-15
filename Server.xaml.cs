using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace ВКанализации
{
    public partial class Server
    {
        private CancellationTokenSource isListening;
        readonly Socket socket;
        List<User> users = [];
        Thread listen;
        ObservableCollection<string> messages = [];
        ObservableCollection<string> logs = [];
        public Server()
        {
            InitializeComponent();
            isListening = new CancellationTokenSource();
            UsersLbx.Items.Clear();
            UsersLbx.Items.Add(MainWindow.user.Name);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listen = new(() =>
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, 7000));
                socket.Listen(100);
                ListenToUsers(isListening.Token);
            });
            listen.Start();
            logs.Add($"{DateTime.Now} - Сервер запущен");
            MessagesLbx.ItemsSource = messages;
        }
        public static string[] GetUser(string source)
        {
            return source.Split(separator, StringSplitOptions.None);
        }
        private async void ListenToUsers(CancellationToken cancellation)
        {
            while (!isListening.IsCancellationRequested)
            {
                Socket? client; 
                try
                {
                    client = await socket.AcceptAsync(cancellation);
                }
                catch (OperationCanceledException) { return; }
                ReceieveMessage(client);
            }
        }
        private void SendMessageButton(object sender, RoutedEventArgs e)
        {
            string message = MainWindow.user.Name + " >> " + MessageTbx.Text;
            MessageTbx.Text = "";
            messages.Add(message);
            foreach (var item in users)
            {
                SendMessage(message, item.Client);
            }
        }
        private User GetUser(Socket s)
        {
            foreach (var item in users)
            {
                if (item.Client == s)
                {
                    return item;
                }
            }
            return null;
        }

        private void Command(string message, string command, Socket s)
        {
            Dispatcher.Invoke(() =>
            {
                switch (command)
                {
                case "/connect":
                    case "/disconnect":
                        string connect = "";
                        string username;
                        if (command == "/connect")
                        {
                            User new_user = new(Regex.Replace(string.Join(" ", message.Split(' ').Skip(1)), @"[^\w\d]", ""), null, s);
                            username = new_user.Name;
                            if (MainWindow.user.Name == username)
                            {
                                SendMessage("sys >> /disconnect", s);
                                return;
                            }
                            foreach (var item in users)
                            {
                                if (item.Name == username)
                                {
                                    SendMessage("sys >> /disconnect", s);
                                    return;
                                }
                            }
                            users.Add(new_user);
                            connect = "Подключился";
                            UsersLbx.Items.Add(new_user.Name);
                        }
                        else
                        {
                            username = GetUser(s).Name;
                            connect = "Отключился";
                            UsersLbx.Items.Remove(username);
                            users.Remove(GetUser(s));
                        }
                        message = $"{DateTime.Now} - {connect} пользователь {username}";
                        logs.Add(message);
                        string users_string = MainWindow.user.Name;
                        foreach (var item in users)
                        {
                            users_string += "," + item.Name;
                        }
                        foreach (var item in users)
                        {
                            SendMessage($"sys >> {users_string}", item.Client);
                        }
                        break;
                }
            });
        }
        private async void ReceieveMessage(Socket s)
        {
            while (true)
            {
                // user >> messages
                byte[] bytes = new byte[65536];
                try
                {
                    await s.ReceiveAsync(bytes, SocketFlags.None);
                }
                catch
                {
                    Command(null, "/disconnect", s);
                    return;
                }
                string message = Encoding.UTF8.GetString(bytes);
                if (message[0] == '/')
                {
                    string command = message.Split(" ")[0];
                    Command(message, command, s);
                    if (command == "/connect")
                    {
                        continue;
                    }
                    if (command == "/disconnect")
                    {
                        return;
                    }
                }
                Dispatcher.Invoke(() =>
                {
                    User user = GetUser(s);
                    if (user == null)
                    {
                        return;
                    }
                    message = user.Name + " >> " + message;
                    messages.Add(message);
                    MessagesLbx.Items.Add(message);
                    foreach (var item in users)
                    {
                        SendMessage(message, item.Client);
                    }
                }); 
            }
        }

        private static readonly string[] separator = [" >> "];

        private static async void SendMessage(string message, Socket c)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await c.SendAsync(bytes, SocketFlags.None);
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            isListening.Cancel();
            socket.Close();
            isListening.Dispose();
            listen.Join();
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessagesLbx.ItemsSource == messages)
            {
                MessagesLbx.ItemsSource = logs;
                LogsButton.Content = "Посмотреть чат";
            }
            else
            {
                MessagesLbx.ItemsSource = messages;
                LogsButton.Content = "Посмотреть логи чата";
            }
        }
    }
}
