using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rei_Net
{
    public class Board
    {
/*-----------------------------------------------------------------------------------*/
        int Height;
        int Width;
        Space[] Spaces;
        int[] p1WinSpaces;
        int[] p2WinSpaces;
/*-----------------------------------------------------------------------------------*/
        public Board(int height, int width)
        {
            this.Spaces = new Space[height*width];
            this.Height = height;
            this.Width = width;
            int curSpace = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Spaces[curSpace] = new Space(i, j, curSpace);
                    curSpace++;
                }
            }
        }
/*-----------------------------------------------------------------------------------*/
        public void setExist(int[] spaces)
        {
            for (int i = 0; i < spaces.Length; i++)
            {
                this.Spaces[spaces[i]].setExist(false);
            }
        }
/*-----------------------------------------------------------------------------------*/
        public int getSize()
        {
            return Spaces.Length;
        }
/*-----------------------------------------------------------------------------------*/
        public Space getSpace(int spaceNum)
        {
            return Spaces[spaceNum];
        }
/*-----------------------------------------------------------------------------------*/
        public int getWidth()
        {
            return this.Width;
        }
/*-----------------------------------------------------------------------------------*/
        public int getHeight()
        {
            return this.Height;
        }
/*-----------------------------------------------------------------------------------*/
        public void moveCard(Space init, Space destination, Player p)
        {
            if(destination.getGuest() == null)
            {
                destination.setGuest(init.getGuest());
                init.removeGuest();
            }
            else
            {
                destination.getGuest().capture(p);
                destination.setGuest(init.getGuest());
                init.removeGuest();
            }
        }
        public void printBoard()
        {
            int curSpace = 0;
            for (int i = 0; i < this.getSize(); i++)
            {
                if (curSpace == this.getWidth())
                {
                    Console.Write("\n");
                    curSpace = 0;
                }
                if (this.getSpace(i).getExist() == true)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write("X");
                }
                curSpace++;
            }
            Console.Write("\n");
        }
        public void firstprintBoard(Player p1, Player p2)
        {
            int curSpace = 0;
            int spacetime = 0;
            Console.WriteLine(p1.getName());
            Console.Write("\n");
            for (int i = 0; i < this.getSize(); i++)
            {
                if (curSpace == this.getWidth())
                {
                    Console.Write("\n");
                    curSpace = 0;
                }
                if (this.getSpace(i).getExist() == true)
                {
                    if (spacetime < 8)
                    {
                        Console.Write(curSpace+1+" ");
                    }
                    else if(spacetime > 55)
                    {
                        Console.Write(curSpace + 1 + " ");
                    }
                    else if (spacetime == 11 || spacetime == 12 || spacetime == 51 || spacetime == 52)
                    {
                        Console.Write(curSpace + 1+ " ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                else
                {
                    Console.Write("X ");
                }
                curSpace++;
                spacetime++;
            }
            Console.Write("\n");
            Console.Write("\n");
            Console.WriteLine(p2.getName());
            Console.Write("\n");
        }

        public void setWinSpaces(int[] p1, int[] p2)
        {
            this.p1WinSpaces = p1;
            this.p2WinSpaces = p2;
        }
    }
}
