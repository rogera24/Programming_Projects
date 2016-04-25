using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    //Effects, or buffs/debuffs, are anything that effect the status of the unit that has the effect.
    class Effects
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
        public Effects(string effectName, int effectDuration, string dispelType, int magnitude){
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
    }
}
