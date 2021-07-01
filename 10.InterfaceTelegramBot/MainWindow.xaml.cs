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
            Chat.ItemsSource = client.ChatMessageLog;
            ListUsersBox.ItemsSource = client.MessageLog;
           
            
        }

        private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            client.SendMessage(MessageSend.Text, CurrentId);
        }
        private void ListUsers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageLog message = ListUsersBox.SelectedItem as MessageLog;
            CurrentId = message.Id;
            NameUserChat.Text = CurrentId.ToString();
            //for(int i=0; i<Chat.Items.Count;i++)
            //{
            //    MessageLog item = Chat.Items[i] as MessageLog;
            //    if(item.Id!=CurrentId)
            //    {
            //        Chat.Items.Filter()
            //    }
            //}

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
    }
}
