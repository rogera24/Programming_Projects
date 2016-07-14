using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    class Item
    {
        string equipmentName;

        /*public Item(string Name)
        {
            equipmentName = Name;
        }*/
    }
    //Equipment are items that can be worn.
    class Equipment : Item
    {

    }
    //Weapons are items that can be used for attacks.
    class Weapon : Item
    {
        int damage;
        int minRange;
        int maxRange;
        int MPCost;
        //Durability determines how close the weapon is to breaking. Using the weapon uses its durability.
        int maxDurability;
        int currentDurability;
        //Weight determines who wields the weapon most efficiently. Units with low Strength have a harder time wielding weapons
        //with high weight, and their ability to hit with the weapon will suffer.
        int weight;
        //It's either slashing, piercing, or blunt. Some units take more or less damage from being hit with a certain
        //damage type.
        string damageType;
        int abilityType;
        string weaponName;
        bool weaponDealsDamage = true;
        bool weaponDealsNegativeDamage = false;
        //The weapon ability is a generic ability that is unique to each weapon; weapons use abilities for combat.
        Ability weaponAbility;
        //Weapons sometimes have a radius used for its weapon ability.
        int radius = 0;
        public Weapon(string name, int dmg, int maxRange, int minRange, int type, int durability, int weight, string dmgType, bool weaponDealsDamage, bool weaponDealsNegativeDamage, int weaponRadius)
        {
            this.weaponName = name;
            this.damage = dmg;
            this.maxRange = maxRange;
            this.minRange = minRange;
            this.abilityType = type;
            this.currentDurability = durability;
            this.maxDurability = durability;
            this.weight = weight;
            this.damageType = dmgType;
            this.weaponDealsDamage = weaponDealsDamage;
            this.weaponDealsNegativeDamage = weaponDealsNegativeDamage;
            this.radius = weaponRadius;
        }
        //Deals damage to the weapon's durability. Negative numbers can instead heal the weapon.
        public void damageWeapon(int durabilityDamage)
        {
            this.currentDurability = this.currentDurability - durabilityDamage;
        }
        public int getCurrentDurability()
        {
            return this.currentDurability;
        }
        public int getMaxDurability()
        {
            return this.maxDurability;
        }
        public void setWeaponName(string name)
        {
            this.weaponName = name;
        }
        public string getWeaponName()
        {
            return this.weaponName;
        }
        public void setDamage(int x)
        {
            this.damage = x;
        }
        public int getDamage()
        {
            return this.damage;
        }
        public void setWeaponAbility(Ability x)
        {
            this.weaponAbility = x;
        }
        public Ability getWeaponAbility()
        {
            return this.weaponAbility;
        }
        public int getMaxRange()
        {
            return this.maxRange;
        }
        public int getMinRange()
        {
            return this.minRange;
        }
        public int getAbilityType()
        {
            return this.abilityType;
        }
        public bool doesWeaponDealDamage()
        {
            return this.weaponDealsDamage;
        }
        public bool doesWeaponDealNegDamage()
        {
            return this.weaponDealsNegativeDamage;
        }
        public int getRadius()
        {
            return this.radius;
        }
        public void setRadius(int x)
        {
            this.radius = x;
        }
    }
}
