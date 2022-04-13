using System.Net;
using System.Net.Sockets;
using System.Text;


public class GameManagement
{
    private Communication communication;
    public GameManagement()
    {
         communication = new Communication();
        /// <summary>
        /// token est le jeton indiquant celui qui va commencer à jouer en premier lorsque si il est à True
        /// </summary>
        bool token = true;
    }



    public void SendingBlow(bool token)
    {
        if (token)
        {
            communication.SendMessage("Bonjour! ", "192.168.1.112");
            token = !token;
        }

    }

    public void WaitingForBlow(bool token)
    {

        if (token)
        {
            string message = communication.ReceiveMessage();
            Console.WriteLine("Recevoir un message : {0}", message);
            token = !token;
        }
    }

 


}





