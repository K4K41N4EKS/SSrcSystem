using System.Net;
using System.Net.Sockets;
using System.Text;

var tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

try
{
    tcpListener.Start();    // запускаем сервер
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    while (true)
    {
        // получаем подключение в виде TcpClient
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();
        // получаем объект NetworkStream для взаимодействия с клиентом
        var stream = tcpClient.GetStream();
        // определяем данные для отправки - отправляем текущее время
        byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());
        // отправляем данные
        await stream.WriteAsync(data);
        Console.WriteLine($"Клиенту {tcpClient.Client.RemoteEndPoint} отправлены данные");
    }
}
finally
{
    tcpListener.Stop();
}