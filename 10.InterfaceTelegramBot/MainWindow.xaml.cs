using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            client =new TelegramMessageClient(this);
            
            Chat.ItemsSource = client.ChatMessageLog;
            ListUsersBox.ItemsSource = client.MessageLog;
        }

        private void Chat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonScrollDawn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            client.SendMessage(MessageSend.Text, NameUserChat.Text);
        }
    }
}
