
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Communication
{

    UdpClient udpClient = new UdpClient(1000);


    public  void SendMessage(string message, string address)
    {
       
        try
        {
             udpClient.Connect(address, 1000);
            // Sends a message to the host to which you have connected.
            Byte[] sendBytes = Encoding.ASCII.GetBytes(message);

            udpClient.Send(sendBytes, sendBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public string ReceiveMessage()
    {
      

        //IPEndPoint object will allow us to read datagrams sent from any source.
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        // Blocks until a message returns on this socket from a remote host.
        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
        string returnData = Encoding.ASCII.GetString(receiveBytes);

        return returnData;
    }

 
}