using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale.Gamer
{
    public class Gamer
    {
        public Gamer(string name,string ip)
        {
            FullName = name;
            IPAddress = ip;
        }

        /// <summary>
        /// Nom du joueur
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Adresse IP du joueur
        /// </summary>
        public string? IPAddress  { get; set; }

        /// <summary>
        /// "Flotte" du joueur
        /// </summary>
        public List<Ship>? Ships { get; set; }
    }
}
