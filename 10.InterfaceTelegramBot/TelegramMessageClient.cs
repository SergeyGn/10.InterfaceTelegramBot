﻿
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
        public ObservableCollection<MessageLog> MessageLog { get; set; }
        public ObservableCollection<MessageLog> ChatMessageLog;
        public TelegramBotClient Bot;
        private int _countNewMsg;
        private string token= "1887811869:AAHoHq6KnRNwixEk7VhQVS0d-DRXWwPtU44";
        

        string Json;
        private List<MessageLog> SaveMassageLog;
        private bool _isDownload = false;
        private string _pathSave = @"C:\Users\Сергей\source\repos\10.InterfaceTelegramBot2\10.InterfaceTelegramBot\bin\Debug\SaveMessageLog.json";


        public TelegramMessageClient(MainWindow W)
        {
            SaveMassageLog = new List<MessageLog>();
            MessageLog = new ObservableCollection<MessageLog>();
            ChatMessageLog = new ObservableCollection<MessageLog>();
            if (_isDownload == false)
            {
                Json = System.IO.File.ReadAllText(_pathSave);
                SaveMassageLog = JsonConvert.DeserializeObject<List<MessageLog>>(Json);
                W.Dispatcher.Invoke(() =>
                {
                        for (int i = 0; i < SaveMassageLog.Count; i++)
                        {
                            for (int j = 0; j < MessageLog.Count; j++)
                            {
                                if (SaveMassageLog[i].Id == MessageLog[j].Id)
                                {
                                    MessageLog.Remove(MessageLog[j]);
                                }
                            }
                            MessageLog.Add(new MessageLog(
                            SaveMassageLog[i].Time, SaveMassageLog[i].Msg, SaveMassageLog[i].FirstName,
                            SaveMassageLog[i].Id, SaveMassageLog[i].CountNewMsg));
                            ChatMessageLog.Add(new MessageLog(
                            SaveMassageLog[i].Time, SaveMassageLog[i].Msg, SaveMassageLog[i].FirstName,
                            SaveMassageLog[i].Id, SaveMassageLog[i].CountNewMsg));
                        }
                        _isDownload = true;
                });
            }

            w = W;
            Bot=new TelegramBotClient(token);
            Bot.OnMessage += MessageListener;
            Bot.StartReceiving();
        }

        public void MessageListener(object sender, MessageEventArgs e) //обработчик сообщений
        {
            w.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < MessageLog.Count; i++)
                {
                    if (e.Message.Chat.Id == MessageLog[i].Id)
                    {
                        MessageLog.Remove(MessageLog[i]);
                    }
                }
                MessageLog messageLog = new MessageLog(
                DateTime.Now.ToLongTimeString(), e.Message.Text, e.Message.Chat.FirstName,
                e.Message.Chat.Id, ++_countNewMsg);

                MessageLog.Add(messageLog);
                ChatMessageLog.Add(messageLog);

                SaveMassageLog.Add(messageLog);
                Json = JsonConvert.SerializeObject(SaveMassageLog);
                System.IO.File.WriteAllText(_pathSave, Json);
            });
        }

        public void SendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            string name="null";
            Bot.SendTextMessageAsync(id, Text);

            ChatMessageLog.Add(new MessageLog(
            DateTime.Now.ToLongTimeString(), Text, "bot", id, ++_countNewMsg));

            for(int i=0;i<MessageLog.Count;i++)
            {
                if (MessageLog[i].Id == id)
                {
                    name = MessageLog[i].FirstName;
                    break;
                }
            }
            for (int i = 0; i < MessageLog.Count; i++)
            {
                if (id == MessageLog[i].Id)
                {
                    MessageLog.Remove(MessageLog[i]);
                }
            }
            MessageLog messageLog = new MessageLog(
            DateTime.Now.ToLongTimeString(), Text, name, id, 0);

            MessageLog.Add(messageLog);

            SaveMassageLog.Add(new MessageLog(
                DateTime.Now.ToLongTimeString(), Text, name, id, 0));
            Json = JsonConvert.SerializeObject(SaveMassageLog);
            System.IO.File.WriteAllText(_pathSave, Json);
        }
    }
}
