using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    public class Field
    {
        string Name;
        int Height;
        int Width;
        Space[] Spaces;
        int[] Inaccessable;
/*-----------------------------------------------------------------------------------*/
        //Constructor override for field accepting two arguments, for compatibility reasons. This constructor is now outdated
        //in favor of the constructor below accepting three arguments.
        public Field(int height, int width)
        {
            //this.Name = name;
            this.Spaces = new Space[height * width];
            this.Height = height;
            this.Width = width;
            int curSpace = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Spaces[curSpace] = new Space(j, i, curSpace);
                    curSpace++;
                }
            }
            curSpace = 0;
            for (int l = 0; l < height; l++)
            {
                for (int k = 0; k < width; k++)
                {
                    try
                    {
                        Spaces[curSpace].setNorth(Spaces[curSpace - this.getWidth()]);
                    }
                    catch
                    {
                        Spaces[curSpace].setNorth(null);
                    }
                    if (Spaces[curSpace].getX() == this.getWidth() - 1)
                    {
                        Spaces[curSpace].setEast(null);
                    }
                    else
                    {
                        Spaces[curSpace].setEast(Spaces[curSpace + 1]);
                    }
                    try
                    {
                        Spaces[curSpace].setSouth(Spaces[curSpace + this.getWidth()]);
                    }
                    catch
                    {
                        Spaces[curSpace].setSouth(null);
                    }
                    if (Spaces[curSpace].getX() == 0)
                    {
                        Spaces[curSpace].setWest(null);
                    }
                    else
                    {
                        Spaces[curSpace].setWest(Spaces[curSpace - 1]);
                    }
                    curSpace++;
                }
            }
        }
        //Use this constructor instead when creating fields.
        public Field(string name, int height, int width)
        {
            this.Name = name;
            this.Spaces = new Space[height*width];
            this.Height = height;
            this.Width = width;
            int curSpace = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Spaces[curSpace] = new Space(j, i, curSpace);
                    curSpace++;
                }
            }
            curSpace = 0;
            for (int l = 0; l < height; l++)
            {
                for (int k = 0; k < width; k++)
                {
                    try
                    {
                        Spaces[curSpace].setNorth(Spaces[curSpace - this.getWidth()]);
                    }
                    catch
                    {
                        Spaces[curSpace].setNorth(null);
                    }
                    if(Spaces[curSpace].getX() == this.getWidth() -1)
                    {
                        Spaces[curSpace].setEast(null);
                    }
                    else
                    {
                        Spaces[curSpace].setEast(Spaces[curSpace+1]);
                    }
                    try
                    {
                        Spaces[curSpace].setSouth(Spaces[curSpace + this.getWidth()]);
                    }
                    catch
                    {
                        Spaces[curSpace].setSouth(null);
                    }
                    if (Spaces[curSpace].getX() == 0)
                    {
                        Spaces[curSpace].setWest(null);
                    }
                    else
                    {
                        Spaces[curSpace].setWest(Spaces[curSpace - 1]);
                    }
                    curSpace++;
                }
            }
        }
/*-----------------------------------------------------------------------------------*/
        public int getSize()
        {
            return this.Spaces.Length;
        }
/*-----------------------------------------------------------------------------------*/
        public Space[] getAllSpaces()
        {
            return this.Spaces;
        }
/*-----------------------------------------------------------------------------------*/
        public Space getSpace(int spaceNum)
        {
            return this.Spaces[spaceNum];
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
        public int getXFromSpace(Space spaceStart, Space spaceEnd)
        {
            int xDisplacement = 0;
            xDisplacement = spaceEnd.getX() - spaceStart.getX();
            return xDisplacement;
        }
/*-----------------------------------------------------------------------------------*/
        public int getYFromSpace(Space spaceStart, Space spaceEnd)
        {
            int yDisplacement = 0;
            yDisplacement = spaceEnd.getY() - spaceStart.getY();
            return yDisplacement;
        }
/*-----------------------------------------------------------------------------------*/
        //Gets a space from the starting space based on displacement along the x and y axis.
        /*public Space getSpaceRelativeToSpace(Space spaceStart, int x, int y)
        {
            Space spaceEnd;
            spaceEnd = 
            return spaceEnd;
        }*/
/*-----------------------------------------------------------------------------------*/
        internal Space convertCoordinatesToSpace(int x, int y)
        {
            return this.getSpace((y*this.Height) + x);
        }
/*-----------------------------------------------------------------------------------*/
        public int moveUnit(Space initial, Space destination)
        {
            if(destination.getGuest() == null)
            {
                destination.setGuest(initial.getGuest());
                initial.removeGuest();
                return 1;
            }
            else
            {
                Console.Out.WriteLine("Cannot Move, Unit Blocking your path.");
                return 0;
            }
        }
/*-----------------------------------------------------------------------------------*/
        //The playerVision argument is used to determine which player's vision will be used to print the map.
        public void printMap(Player playerVision, Unit currentUnit)
        {
            int currentUnitSpace = 0;
            try
            {
                currentUnitSpace = currentUnit.getHost().getID();
            }
            catch
            {

            }
            int w = playerVision.getPlayerID();
            int curSpace = 0;
            for (int i = 0; i < this.getSize(); i++)
            {
                /*if (getSpace(i).getGuest() == null)
                {
                    getSpace(i).setContainsCurrentUnit(false);
                }
                else if (getSpace(i).getGuest() == currentUnit)
                {
                    getSpace(i).setContainsCurrentUnit(true);
                }*/
                if (curSpace == this.getWidth())
                {
                    Console.Write("\n");
                    curSpace = 0;
                }
                if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted()==false)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ResetColor();
                }
                else if (this.getSpace(i).getIsTargetHighlighted() == true)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ResetColor();
                }
                if (this.getSpace(i).getIsMoveHighlighted() == true)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ResetColor();
                }
                int r = w;
                if (this.getSpace(i).getConcealed(w) == true)
                {
                    if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else if (this.getSpace(i).getIsTargetHighlighted() == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    if (this.getSpace(i).getIsMoveHighlighted() == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    string playerVisionS = playerVision.getPlayerName();
                    bool disqualifiedSpace = false;
                    if (this.getSpace(i).getGuest() == null)
                    {
                        disqualifiedSpace = true;
                    }
                    else if (this.getSpace(i).getGuest().getPlayer() == playerVision || this.getSpace(i).getGuest().getPlayer().getRelationshipToPlayer(playerVision.getPlayerID())=="ally")
                    {
                        if (this.getSpace(i).getGuest().getAI() == true)
                        {
                            //The colors are arbitrary for now and help identify the unit. Later, it will be improved
                            //to give units the color of the players that control them.
                            if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Green")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Red")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Yellow")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Magenta")
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            if (this.getSpace(i).getContainsCurrentUnit() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Cyan;
                            }
                            else if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write("X");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Green")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Red")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Yellow")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Magenta")
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            if (this.getSpace(i).getContainsCurrentUnit() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Cyan;
                            }
                            else if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write("U");
                            Console.ResetColor();
                        }
                    }
                    else {
                        if (this.getSpace(i).getExist() == true)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write(".");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write("/");
                        }
                        /*Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("#");*/
                    }
                    if (disqualifiedSpace == true) 
                    {
                        if (this.getSpace(i).getExist() == true)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write(".");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            Console.Write("/");
                        }
                        /*Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("#");*/
                    }
                    Console.ResetColor();
                }
                else if (this.getSpace(i).getExist() == true)
                {
                    if (this.getSpace(i).getGuest() == null)
                    {
                        if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        else if (this.getSpace(i).getIsTargetHighlighted() == true)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }
                        if (this.getSpace(i).getIsMoveHighlighted() == true)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        Console.Write(".");
                        Console.ResetColor();
                    }
                    else
                    {
                        if(this.getSpace(i).getGuest().getAI() == true)
                        {
                            //The colors are arbitrary for now and help identify the unit. Later, it will be improved
                            //to give units the color of the players that control them.
                            if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Green")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Red")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Yellow")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Magenta")
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            if (this.getSpace(i).getContainsCurrentUnit() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Cyan;
                            }
                            if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write("X");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Green")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Red")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Yellow")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (this.getSpace(i).getGuest().getPlayer().getPlayerColor() == "Magenta")
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            if (this.getSpace(i).getContainsCurrentUnit() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Cyan;
                            }
                            if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if (this.getSpace(i).getIsTargetHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (this.getSpace(i).getIsMoveHighlighted() == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.Write("U");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    if (this.getSpace(i).getIsAttackHighlighted() == true && this.getSpace(i).getIsTargetHighlighted() == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else if (this.getSpace(i).getIsTargetHighlighted() == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    if (this.getSpace(i).getIsMoveHighlighted() == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write("/");
                    Console.ResetColor();
                }
                curSpace++;
            }
            Console.Write("\n");
        }
/*-----------------------------------------------------------------------------------*/
        public void setInacces(int[] spaces)
        {
            this.Inaccessable = spaces;
            for (int i = 0; i < spaces.Length; i++)
            {
                this.Spaces[spaces[i]].setExist(false);
            }
        }

        public void PlaceUnit(Unit Test_Warrior, int p)
        {
            Spaces[p].setGuest(Test_Warrior);
            Test_Warrior.setHost(Spaces[p]);
        }
    }
}
