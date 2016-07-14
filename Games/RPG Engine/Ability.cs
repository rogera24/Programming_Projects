using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    public class Ability
    {
        //Base damage is how much damage the ability normally does.
        int baseDmg;
        int modifiedDmg;
        string name;
        int maxRange;
        int minRange;
        int MPCost;
        //An ability might reference the weapon that uses it.
        Weapon weapon;
        //Indicates the type of ability it is. Physical abilities apply physical stat modifiers, magic abilities apply magical
        //stat modifiers.
        //Integer used to indicate ability because strings are cumbersome and use more data.
        //0 indicates it's a physical ability, 1 indicates it's a magical ability, 2 indicates it's an ability that doesn't
        //have an associated damage modifier.
        int Type;
        //Determines if an ability does damage or not.
        bool doesDamage = true;
        //Determines if an ability deals negative damage. This is used by heal spells.
        bool negativeDamage = false;
        //The list below contains all effects that an ability has, if relevant. Whenever the ability is used, all effects
        //are applied to the target of the ability.
        List<Effects> effectsPool = new List<Effects>();
        //Similarly, this contains all tile effects the ability has and applies the tile effects to the tiles under the ability's radius.
        List<TileEffect> tileEffectsPool = new List<TileEffect>();
        bool isWeaponAbility = false;
        //For every ability derived from classes, there is an associated minimum level requirement. The two lists below
        //would share an index for one class.
        List<Class> classList = new List<Class>();
        List<int> classListLevels = new List<int>();
        //Every ability has an affected radius, although all abilities that aren't area-of-effect abilities have a radius of 0.
        int radius = 0;
        public Ability(string name, int dmg, int maxRange, int minRange, int MPCost, int Type, bool isWeaponAbility, bool doesDamage, bool negativeDamage, int radius)
        {
            this.baseDmg = dmg;
            this.name = name;
            this.maxRange = maxRange;
            this.minRange = minRange;
            this.MPCost = MPCost;
            this.Type = Type;
            this.isWeaponAbility = isWeaponAbility;
            this.doesDamage = doesDamage;
            this.negativeDamage = negativeDamage;
            this.radius = radius;
        }
        public int getAbilityType()
        {
            return this.Type;
        }
        public string getName()
        {
            return this.name;
        }
        public int getDmg()
        {
            return this.baseDmg;
        }
        public int getMaxRange()
        {
            return this.maxRange;
        }
        public int getMinRange()
        {
            return this.minRange;
        }
        public int getMPCost()
        {
            return this.MPCost;
        }
        public void setName(string abilityName)
        {
            this.name = abilityName;
        }
        public bool abilityIsWeaponAbility()
        {
            return this.isWeaponAbility;
        }
        internal List<Effects> getEffectsPool()
        {
            return effectsPool;
        }
        internal void addEffectToEffectPool(Effects effect)
        {
            effectsPool.Add(effect);
        }
        public void removeEffectFromEffectPoolAtIndex(int x)
        {
            effectsPool.RemoveAt(x);
        }
        internal void addTileEffectToTileEffectPool(TileEffect effect)
        {
            tileEffectsPool.Add(effect);
        }
        public void removeTileEffectFromTileEffectPoolAtIndex(int x)
        {
            tileEffectsPool.RemoveAt(x);
        }
        internal List<TileEffect> getTileEffectsPool()
        {
            return this.tileEffectsPool;
        }
        public void addClassToClassList(Class x)
        {
            classList.Add(x);
        }
        public void addMinLevelRequirement(int x)
        {
            classListLevels.Add(x);
        }
        public List<Class> getClassList()
        {
            return classList;
        }
        public List<int> getClassMinRequirementsList()
        {
            return classListLevels;
        }
        public Class getClassAtIndex(int index)
        {
            return classList[index];
        }
        public int getMinLevelRequirementAtIndex(int index)
        {
            return classListLevels[index];
        }
        public bool doesAbilityDealDamage()
        {
            return this.doesDamage;
        }
        public bool doesAbilityDoNegDamage()
        {
            return this.negativeDamage;
        }
        public void setRadius(int x)
        {
            this.radius = x;
        }
        public int getRadius()
        {
            return this.radius;
        }
    }
}
