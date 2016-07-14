using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    //The fog class is used to more easily implement fog of war. It is in the Space.cs file because it is closely
    //attached to spaces and isn't complex enough to warrant its own file.
    //The vision radius represents how far you can see into the fog.
    public class Fog
    {
        Field map;
        Space host;
        int visionRadius = 0;
        //Fog that isn't active doesn't affect visibility.
        bool isActive = false;
        string Name;
        public Fog(Field map, Space host, int visionRadius, bool startsActive, string Name)
        {
            this.map = map;
            this.visionRadius = visionRadius;
            this.isActive = startsActive;
            this.Name = Name;
        }
        public void setVisionRadius(int x)
        {
            this.visionRadius = x;
        }
        public int getVisionRadius()
        {
            return this.visionRadius;
        }
        public void setHostBySpace(Space x)
        {
            this.host = x;
        }
        public void setHostBySpaceID(int x)
        {
            this.host = map.getSpace(x);
        }
        public Space getHost()
        {
            return this.host;
        }
        public void setActive(bool x)
        {
            this.isActive = x;
        }
        public bool getActive()
        {
            return this.isActive;
        }
        public string getName()
        {
            return this.Name;
        }
    }
    public class Space
    {
        int x;
        int y;
        bool exist;
        int ID;
        //A space that is concealed to a player cannot be seen. Its unit guest is obscured and its name is not known.
        bool[] concealed = { false, false, false, false };
        //A space that is highlighted lights up a specific color.
        bool isAttackHighlighted = false;
        bool isTargetHighlighted = false;
        bool isMoveHighlighted = false;
        bool containsCurrentUnit = false;
        Unit Guest;
        Ability effect;
        Space North;
        Space South;
        Space East;
        Space West;
        List<TileEffect> activeTileEffects = new List<TileEffect>();
        List<Fog> fogOfWar = new List<Fog>();
        //The "dominant fog" is the fog with the lowest vision radius, and is chosen over all the rest.
        Fog dominantFog;
        bool analyzed = false;

        public Space(int i, int j, int id)
        {
            this.x = i;
            this.y = j;
            this.Guest = null;
            this.ID = id;
            this.exist = true;
        }

        internal int getX(){
            return this.x;
        }
        internal int getY()
        {
            return this.y;
        }
        public void setExist(bool ex){
            this.exist = ex;
        }
        public bool getExist()
        {
            return this.exist;
        }
        public void setGuest(Unit guest)
        {
            this.Guest = guest;
        }
        public void removeGuest()
        {
            this.Guest = null;
        }
        public Unit getGuest()
        {
            return this.Guest;
        }
        public bool getIsAttackHighlighted()
        {
            return this.isAttackHighlighted;
        }
        public bool getIsMoveHighlighted()
        {
            return this.isMoveHighlighted;
        }
        public bool getIsTargetHighlighted()
        {
            return this.isTargetHighlighted;
        }
        internal int getSpaceNum()
        {
            return this.ID;
        }
        internal void setNorth(Space s)
        {
            this.North = s;
        }
        internal void setSouth(Space s)
        {
            this.South = s;
        }
        internal void setEast(Space s)
        {
            this.East = s;
        }
        internal void setWest(Space s)
        {
            this.West = s;
        }
        internal Space getNorth()
        {
            return this.North;
        }
        internal Space getSouth()
        {
            return this.South;
        }
        internal Space getEast()
        {
            return this.East;
        }
        internal Space getWest()
        {
            return this.West;
        }
        //Returns true if the space is under fog of war for the given player.
        public bool getConcealed(int playerNumber)
        {
            return this.concealed[playerNumber];
        }
        public void setIsAttackHighlighted(bool x)
        {
            this.isAttackHighlighted = x;
        }
        public void setIsMoveHighlighted(bool x)
        {
            this.isMoveHighlighted = x;
        }
        public void setIsTargetHighlighted(bool x)
        {
            this.isTargetHighlighted = x;
        }
        public void setConcealed(bool x, int playerNumber)
        {
            this.concealed[playerNumber] = x;
        }
        public void setContainsCurrentUnit(bool x)
        {
            this.containsCurrentUnit = x;
        }
        public bool getContainsCurrentUnit()
        {
            return this.containsCurrentUnit;
        }
        internal int getID()
        {
            return this.ID;
        }
        internal void applyTileEffectsFromList(List<TileEffect> list)
        {
            foreach (TileEffect w in list)
            {
                activeTileEffects.Add(w);
            }
        }
        internal void addTileEffect(TileEffect x)
        {
            activeTileEffects.Add(x);
        }
        internal void removeTileEffectAtIndex(int x)
        {
            activeTileEffects.RemoveAt(x);
        }
        //This method makes it easier to deal with tile effects that ran out of duration.
        internal void removeTileEffectsWithZeroDuration()
        {
            for (int t = 0; t < activeTileEffects.Count(); t++)
            {
                if (activeTileEffects[t] == null) { }
                else if (activeTileEffects[t].getDuration() <= 0)
                {
                    activeTileEffects[t] = null;
                }
            }
        }
        internal List<TileEffect> getAllTileEffects()
        {
            return activeTileEffects;
        }
        internal void setAnalyzed(bool x)
        {
            this.analyzed = x;
        }
        internal bool getAnalyzed()
        {
            return this.analyzed;
        }
        internal void addFog(Fog x)
        {
            fogOfWar.Add(x);
        }
        internal void removeFogAtIndex(int index)
        {
            fogOfWar.RemoveAt(index);
        }
        internal Fog getFogAtIndex(int index)
        {
            return fogOfWar[index];
        }
        internal List<Fog> getAllFogs()
        {
            return fogOfWar;
        }
        internal Fog getDominantFog()
        {
            return dominantFog;
        }
        internal void updateDominantFog()
        {
            List<Fog> fogListByVisionRadius = new List<Fog>();
            for (int r = 0; r <= 999; r++)
            {
                for (int i = 0; i < fogOfWar.Count(); i++)
                {
                    //try
                    {
                        if (fogOfWar[i] == null) { }
                        else if (fogOfWar[i].getVisionRadius() == r && fogOfWar[i].getActive() == true)
                        {
                            fogListByVisionRadius.Add(fogOfWar[i]);
                        }
                    }
                    //catch
                    {

                    }
                }
            }
            if (fogListByVisionRadius.Count() <= 0) { dominantFog = null; }
            else
            {
                dominantFog = fogListByVisionRadius[0];
            }
        }

    }
}
