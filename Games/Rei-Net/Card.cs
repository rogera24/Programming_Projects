using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rei_Net
{
    public class Card
    {
        int type; //0 or 1---bad or good repsectivley
        Player owner;
        Space host;
        Player end_Owner;
        public Card(Player p, Space h)
        {
            this.owner = p;
            this.host = h;
        }
        public void capture(Player p)
        {
            this.end_Owner = p;
            this.host = null;
        }

    }
}
