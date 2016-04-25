using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rei_Net
{
    public class Space
    {
        int x;
        int y;
        bool exist;
        int ID;
        Card guest;

        public Space(int i, int j, int id)
        {
            this.x = i;
            this.y = j;
            this.guest = null;
            this.ID = id;
            this.exist = true;
        }
        public void setExist(bool ex){
            this.exist = ex;
        }
        public bool getExist()
        {
            return this.exist;
        }
        public void setGuest(Card guest)
        {
            this.guest = guest;
        }
        public void removeGuest()
        {
            this.guest = null;
        }
        public Card getGuest()
        {
            return this.guest;
        }
    }
}
