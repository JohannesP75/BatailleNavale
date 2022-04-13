using System.Net;
using System.Net.Sockets;
using System.Text;


public class GameManagement
{
    private Communication communication;
    /// <summary>
    /// token est le jeton indiquant celui qui va commencer à jouer en premier lorsque si il est à True
    /// </summary>
    public bool token { get; set; } = true;
    public GameManagement()
    {
         communication = new Communication();
    }


    /// <summary>
    /// Envoie les coups d'un joueur à un autre
    /// </summary>
    /// <param name="token">Indique si le joueur à le droit d'agir</param>
    public void SendingBlow(bool token)
    {
        if (token)
        {
            communication.SendMessage("Bonjour! ", "192.168.1.112");
            token = !token;
        }

    }

    /// <summary>
    /// Vérifie si un joueur peut recevoir des coups
    /// </summary>
    /// <param name="token">Indique si le joueur à le droit d'agir</param>
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





