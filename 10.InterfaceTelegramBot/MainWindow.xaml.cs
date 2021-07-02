using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _10.InterfaceTelegramBot;
using Telegram.Bot.Args;

namespace _10.InterfaceTelegramBot
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramMessageClient client;
        public long CurrentId;

        public MainWindow()
        {
            InitializeComponent();
            
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            client =new TelegramMessageClient(this);

            ListUsersBox.ItemsSource = client.MessageLog;
            Chat.ItemsSource = client.ChatMessageLog;

        }

        private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {

            if (CurrentId == 0)
            {
                MessageBoxResult messageBox = MessageBox.Show("Нажмите на пользователя которому нужно отправить сообщение");
            }
            else
            {
                client.SendMessage(MessageSend.Text, CurrentId);

                ObservableCollection<MessageLog> chatWithUser = new ObservableCollection<MessageLog>();
                Chat.ItemsSource = chatWithUser;
                for (int i = 0; i < client.ChatMessageLog.Count; i++)
                {
                    if (client.ChatMessageLog[i].Id == CurrentId)
                    {
                        chatWithUser.Add(client.ChatMessageLog[i]);
                    }
                }
            }
            MessageSend.Text = "Написать сообщение...";
            MessageSend.Foreground = Brushes.Gray;
        }


        private void ListUsers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageLog message = ListUsersBox.SelectedItem as MessageLog;
            CurrentId = message.Id;
            NameUserChat.Text = CurrentId.ToString();

            ObservableCollection<MessageLog> chatWithUser = new ObservableCollection<MessageLog>();
            Chat.ItemsSource = chatWithUser;
            for (int i = 0; i < client.ChatMessageLog.Count; i++)
            {
                if (client.ChatMessageLog[i].Id == CurrentId)
                {
                    chatWithUser.Add(client.ChatMessageLog[i]);
                }
            }

            MessageSend.Text = "Написать сообщение...";
            MessageSend.Foreground = Brushes.Gray;

            Chat.ScrollIntoView(Chat.Items[Chat.Items.Count - 1]);
        }

        private void MessageSend_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageSend.Text = "";
            MessageSend.Foreground = Brushes.Black;
        }
    }
}
