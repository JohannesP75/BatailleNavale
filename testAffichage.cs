// Test affichage

class Affichage
{
    public void Grille(LaGrille laGrille, bool leJoueur)
    {
        if (laGrille == null) return;
        if (leJoueur)
        {
            Console.Clear();
            Console.WriteLine(" Notre grille:");
        }
        else Console.WriteLine("\n Adversaire:");
        for (int Line = 0; Line < 8; Line++)
        {
            if (Line == 0) Console.WriteLine("    A B C D E F G H");
            Console.Write($"{Line + 1,2} ");
            for (int Col = 0; Col < 8; Col++)
            {
                char? cetEtat = laGrille.LesCases?[Col, Line].etat;
                Console.Write($"{ cetEtat,2}");
            }
            Console.WriteLine();
        }

    }
}
class LaGrille
{
    public Case[,] LesCases { get; set; } = new Case[8, 8]; // static fait partager le même attribut en mémoire
    public static void Genere(LaGrille cetteGrille)
    {
        if (cetteGrille.LesCases == null) return;
        Case cetteCase = new();
        for (int Line = 0; Line < 8; Line++)
        {
            for (int Col = 0; Col < 8; Col++)
            {
                cetteCase.etat = 'V'; // (char)(32 + Line + Col);
                cetteCase.identifiant = 0;
                // Object reference not set to an instance of an object
                cetteGrille.LesCases[Col, Line] = cetteCase;
            }
        }
    }
}

class Case
{
    public int identifiant { get; set; } // ref bâteau ou 0 par défaut
    public char etat { get; set; } // (I)ntact (V)ide (C)oulé
}

class ComUDP
{
    // This constructor arbitrarily assigns the local port number.
    static UdpClient udpClient = new UdpClient(11000);
    //IPEndPoint object will allow us to read datagrams sent from any source.
    static IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
    public static void TestMessage()
    {
        ConsoleKeyInfo Clavier;
        string? LeMessage;
        bool TestContinu = true, TestReprendre;
        Console.WriteLine("Pressez Enter ...");
        do
        {
            Clavier = Console.ReadKey();
        } while (Clavier.Key != ConsoleKey.Enter);
        do
        {
            Console.Clear();
            TestReprendre = false;
            Console.Write("(n)ouveau message (q)uit ?: ");
            do
            {
                Clavier = Console.ReadKey();
                switch (Clavier.KeyChar)
                {
                    case 'n':
                        Console.Write("\nEntrez le message: ");
                        LeMessage = Console.ReadLine();
                        ComUDP.EnvMessage(LeMessage);
                        TestContinu = false;
                        TestReprendre = true;
                        break;

                    case 'q':
                        Console.WriteLine(" ... fin.");
                        TestContinu = false;
                        break;
                    default:
                        break;
                }
                if (ComUDP.RecMessage())
                {
                    Console.WriteLine("\nPressez Enter ...");
                    do
                    {
                        Clavier = Console.ReadKey();
                    } while (Clavier.Key != ConsoleKey.Enter);
                }
            } while (TestContinu);
        } while (TestReprendre);
        udpClient.Close();
    }

    static bool RecMessage()
    {
        try
        {
            if (udpClient.Available == 0) return false;
            // Blocks until a message returns on this socket from a remote host.
            Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
            string returnData = Encoding.ASCII.GetString(receiveBytes);

            // Uses the IPEndPoint object to determine which of these two hosts responded.
            Console.WriteLine("This is the message you received " +
                                              returnData.ToString());
            Console.WriteLine("This message was sent from " +
                                        RemoteIpEndPoint.Address.ToString() +
                                        " on their port number " +
                                        RemoteIpEndPoint.Port.ToString());
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return true;
        }

    }
    static void EnvMessage(string? UnMessage)
    {
        try
        {
            udpClient.Connect("127.0.0.1", 11000); // IP distant à renseigner

            // Sends a message to the host to which you have connected.
            Byte[] sendBytes = Encoding.ASCII.GetBytes(UnMessage ?? "nop");

            udpClient.Send(sendBytes, sendBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}