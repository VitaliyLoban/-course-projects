using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;

using System.Windows;

namespace Server
{
    class Program
    {
        static int port = 8005; // порт для приема входящих запросов
        static bool isEnter = false;
        static string message;
        static void Main(string[] args)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    
                    string[] k = builder.ToString().Split(';');
                    if (k[0] == "login")
                    {
                        using (Model1 bd_us = new Model1())
                        {
                            foreach (User a in bd_us.Users)
                            {
                                if (k[1] == a.login && k[2] == a.password.ToString())
                                {
                                    isEnter = true;
                                    message = "good";
                                }

                            }
                            if (isEnter == false)
                            {
                                message = "bad";
                            }
                            data = Encoding.Unicode.GetBytes(message);
                            handler.Send(data);
                            isEnter = false;
                        }

                        // отправляем ответ

                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}