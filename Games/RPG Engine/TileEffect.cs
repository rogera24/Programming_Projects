using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine{
    //Tile effects are similar to units in that they have a space host, and they're similar to effects in that they modify
    //the behavior of a space. Tile effects are often caused by abilities and are used for traps and persistent area-of-effect
    //spells.
    class TileEffect
    {
        string effectName;
        //Duration is counted in rounds. An effect with a duration of 1 round wears off on the round AFTER the affected unit's
        //next turn.
        int duration;
        //An effect dispel type determines what abilities can dispel it.
        string effectDispelType;
        //Magnitude is the strength of the effect.
        int magnitude;
        //Descriptors are used by the game to determine what the effect does.
        List<string> descriptors = new List<string>();
        //Tile Effects are always tied to a space host.
        Space host;
        public TileEffect(string effectName, int effectDuration, string dispelType, int magnitude)
        {
            this.effectName = effectName;
            this.duration = effectDuration;
            this.effectDispelType = dispelType;
            this.magnitude = magnitude;
        }
        public int getDuration()
        {
            return this.duration;
        }
        public void setDuration(int x)
        {
            this.duration = x;
        }
        public string getEffectName()
        {
            return this.effectName;
        }
        public void addDescriptor(string x)
        {
            descriptors.Add(x);
        }
        public string getDescriptorAtIndex(int index)
        {
            return descriptors[index];
        }
        public List<string> getAllDescriptors()
        {
            return this.descriptors;
        }
        public void removeDescriptorAtIndex(int index)
        {
            descriptors.RemoveAt(index);
        }
        public int getMagnitude()
        {
            return this.magnitude;
        }
        public void setMagnitude(int x)
        {
            this.magnitude = x;
        }
        public Space getHost()
        {
            return this.host;
        }
        public void setHost(Space x)
        {
            this.host = x;
        }
        //This method is just an easy way to help me get rid of effects.
        public void setToNull()
        {
            this.effectName = "null";
        }
    }
}
