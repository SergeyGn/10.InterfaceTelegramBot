
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace _10.InterfaceTelegramBot
{
  public  class TelegramMessageClient
    {
        private static MainWindow w;
        public ObservableCollection<MessageLog> BotMessageLog { get; set; }
        public TelegramBotClient Bot;
        private int _countNewMsg;
        private string token= "1887811869:AAHoHq6KnRNwixEk7VhQVS0d-DRXWwPtU44";

        public TelegramMessageClient(MainWindow W)
        {
            BotMessageLog = new ObservableCollection<MessageLog>();
            w = W;
            Bot=new TelegramBotClient(token);
            Bot.OnMessage += MessageListener;
            Bot.StartReceiving();
        }

        public void MessageListener(object sender, MessageEventArgs e) //обработчик сообщений
        {
            w.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < BotMessageLog.Count; i++)
                {
                    if (e.Message.Chat.Id == BotMessageLog[i].Id)
                    {
                        BotMessageLog.Remove(BotMessageLog[i]);
                    }
                }
                BotMessageLog.Add(new MessageLog(
                DateTime.Now.ToLongTimeString(), e.Message.Text, e.Message.Chat.FirstName, e.Message.Chat.Id, ++_countNewMsg));

            });
        }

        public void SendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            Bot.SendTextMessageAsync(id, Text);
        }
    }
}
