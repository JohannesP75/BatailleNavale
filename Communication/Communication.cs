using System.Net;
using System.Net.Sockets;
using System.Text;

public class Communication
{
    // This constructor arbitrarily assigns the local port number.
    static UdpClient udpClient = new UdpClient(11000);
    //IPEndPoint object will allow us to read datagrams sent from any source.
    static IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
    static ConsoleKeyInfo Clavier;
    public static bool UdpDispo = false; // cet indicateur informe de la présence d'un message reçu
                                         // que l'on doit annuler avant de pouvoir recevoir à nouveau
    public static string UDPRecu = "";
    public struct UdpState
    {
        public UdpClient uClient;
        public IPEndPoint iPEnd;
    }
    public static char InitJeton(string? IPAdversaire)
    // execute un envoi udp 'jeton' (option: avec délai rnd)
    {      // retourne 'A'bandon 'M'aître 'E'sclave
        string? LeMessage;
        bool TestReprendre;
        UdpState uState = new UdpState();
        System.DateTime Instant = new();
        int PoidsJeton = 0;
        char InitToken = 'A';

        uState.uClient = udpClient;
        uState.iPEnd = RemoteIpEndPoint;
        udpClient.BeginReceive(new AsyncCallback(UdpBienRecu), uState); // en écoute
        do
        {
            Console.Clear();
            Console.WriteLine("... en écoute udp ...");
            TestReprendre = true;
            LeMessage = "jeton" + ',' + PoidsJeton.ToString();
            Console.Write("Test jeton init ");
            SendMessage(LeMessage, IPAdversaire);
            int SecondeInit = Instant.Second;
            Console.Write($", pas encore de connexion (T= {SecondeInit}) (q)uit ?: ");
            do
            {
                Clavier = Console.ReadKey();
                switch (Clavier.KeyChar)
                {
                    case 'q': // abandon de la tentative de connexion
                        Console.WriteLine(" ... fin.");
                        TestReprendre = false;
                        InitToken = 'A';
                        udpClient.Close();
                        break;
                    default:
                        break;
                }
                if (UdpDispo) // udp reçu
                {
                    UdpDispo = false;
                    Console.WriteLine("Boucle: {0} reçu.", UDPRecu);
                    // comparaison du poids des jetons des 2 joueurs
                    if (int.Parse(UDPRecu[(1 + UDPRecu.IndexOf(','))..]) > PoidsJeton)
                    {
                        Console.WriteLine("L'adversaire débute la partie");
                        InitToken = 'E';
                    }
                    else
                    {
                        Console.WriteLine("Je débute la partie");
                        InitToken = 'M';
                        PoidsJeton++;
                        LeMessage = "jeton" + ',' + PoidsJeton.ToString();
                        // un dernier message pour confirmer le poids supérieur
                        SendMessage(LeMessage, IPAdversaire);
                    }
                    TestReprendre = false;
                    AttendreEnter();
                    break;
                }
            } while ((Instant.Second - SecondeInit) < 2);
            if (PoidsJeton++ > 100) PoidsJeton = 1;
        } while (TestReprendre);
        return InitToken;
    }

    public static void UdpBienRecu(IAsyncResult? asRes)
    {
        // identification du paquet reçu et extraction des données
        UdpClient u = ((UdpState)(asRes.AsyncState)).uClient;
        IPEndPoint e = ((UdpState)(asRes.AsyncState)).iPEnd;

        byte[] receiveBytes = u.EndReceive(asRes, ref e);
        UDPRecu = Encoding.ASCII.GetString(receiveBytes);
        UdpDispo = true;

        // réinitialisation du processus d'écoute
        UdpState uState = new UdpState();
        uState.uClient = udpClient;
        uState.iPEnd = RemoteIpEndPoint;
        udpClient.BeginReceive(new AsyncCallback(UdpBienRecu), uState);
    }

    public static void SendMessage(string? UnMessage, string? IPAdversaire)
    {
        try
        {
            udpClient.Connect(IPAdversaire, 11000); // IP distant à renseigner 192.168.1.112

            // Sends a message to the host to which you have connected.
            Byte[] sendBytes = Encoding.ASCII.GetBytes(UnMessage ?? "nop");

            udpClient.Send(sendBytes, sendBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void AttendreEnter()
    {
        Console.WriteLine("Pressez Enter ...");
        do
        {
            Clavier = Console.ReadKey();
        } while (Clavier.Key != ConsoleKey.Enter);
    }
}