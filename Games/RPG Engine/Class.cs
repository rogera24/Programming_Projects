using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    public class Class
    {
        string Name;
        int movementPoints;
        bool hasMP; //True if the class uses MP, false if it doesn't.
        List<Ability> abilities;
        private string p;
        //These modifiers are unique to each class and modifies which stats are improved every time one levels up.
        int strengthModifier = 0;
        int speedModifier = 0;
        int constitutionModifier = 0;
        int LCModifier = 0;
        int magicDefenseModifier = 0;
        int magicSaveModifier = 0;
        int dodgeModifier = 0;
        int aimModifier = 0;
        int MPRegenModifier = 0;
        int experienceYieldModifier = 0;
        //The list of descriptors is used by AIs to judge whether or not this target is worth killing.
        //Common descriptors for classes include "healer", "tank", "mage", "mezzer", "support", "pest", "thief", and "civilian".
        List<string> descriptors = new List<string>();
        public Class(string name, int movementPoints, bool hasMP)
        {
            this.Name = name;
            this.abilities = new List<Ability>();
            this.movementPoints = movementPoints;
            this.hasMP = hasMP;
        }
        public string getClassName()
        {
            return this.Name;
        }
        public void addAbility(Ability abil)
        {
            this.abilities.Add(abil);
        }
        public List<Ability> getAbilities()
        {
            return abilities;
        }
        public int getMovementPoints()
        {
            return this.movementPoints;
        }
        //A function to return whether or not the class uses MP or not is described.
        public bool doesHaveMP()
        {
            return this.hasMP;
        }
        //For the base stats, a modifier of 3 is considered significant, 2 is considered considerable, 
        //1 is considered minor, and 0 is considered negligible.
        public void setBaseModifiers(int strength, int speed, int constitution, int leyConnexion, int experienceYieldModifier)
        {
            this.strengthModifier = strength;
            this.speedModifier = speed;
            this.constitutionModifier = constitution;
            this.LCModifier = leyConnexion;
            this.experienceYieldModifier = experienceYieldModifier;
        }
        public void setCombatModifiers(int magicDefense, int magicSave, int dodge, int aim, int MPRegen)
        {
            this.magicDefenseModifier = magicDefense;
            this.magicSaveModifier = magicSave;
            this.dodgeModifier = dodge;
            this.aimModifier = aim;
            this.MPRegenModifier = MPRegen;
        }
        public int getStrengthModifier()
        {
            return this.strengthModifier;
        }
        public int getSpeedModifier()
        {
            return this.speedModifier;
        }
        public int getConstitutionModifier()
        {
            return this.constitutionModifier;
        }
        public int getLCModifier()
        {
            return this.LCModifier;
        }
        public int getMagicDefenseModifier()
        {
            return this.magicDefenseModifier;
        }
        public int getMagicSaveModifier()
        {
            return this.magicSaveModifier;
        }
        public int getDodgeModifier()
        {
            return this.dodgeModifier;
        }
        public int getAimModifier()
        {
            return this.aimModifier;
        }
        public int getMPRegenModifier()
        {
            return this.MPRegenModifier;
        }
        public int getExperienceYield()
        {
            return this.experienceYieldModifier;
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
    }
}
