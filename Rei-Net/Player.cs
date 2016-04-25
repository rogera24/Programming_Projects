using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rei_Net
{
    public class Player
    {
        string Name;
        int games_played;
        int wins;
        int gcard;
        int bcard;

        public Player(string n)
        {
            this.Name = n;
            this.gcard = 0;
            this.bcard = 0;

        }
        public string getName()
        {
            return this.Name;
        }
    }
}
