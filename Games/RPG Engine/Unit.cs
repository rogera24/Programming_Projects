using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Engine
{
    public class Unit
    {
        //Attach unit to its host space with a pointer.
        Space Host;
        /*Declare properties for the unit class. Its base stats are provided by level and class. Its modified stats include
        all possible modifiers such as from its stats, gear worn, buffs, and others added to its base stat.*/
        string Name;
        int unitBattleID; //An ID for the game engine to easily recognize this unit while it's in battle.
        int unitPartyID; //An ID for the game engine to easily recognize this unit in the party. Different from unitBattleID.
        //Level is the representation of a unit's combined experience. It modifies all of the base stats and MPRegen for units
        //who meet a certain threshold for Ley Connexion.
        int level = 1;
        //Experience determines how close the unit is to leveling up.
        int experience;
        int experienceCap;
        //Experience yield determines how much experience a unit gets for killing it. Base experience yield is how much experience
        //this unit will give by default.
        int baseExperienceYield = 0;
        int modifiedExperienceYield;
        //Hit Points (HP) is how many more 'hits' it will take before dying.
        int currentHP;
        int baseMaxHP;
        int modifiedHP;
        //Magic Points (MP), also known as Mana Points, is magical power accumulated from the ley and regenerates faster
        //the closer one is to the ley or nearby ley lines. It is used to power spells, activate magic items, or strengthen
        //enchantments on weapons and armor.
        int currentMP;
        int baseMaxMP;
        int modifiedMP;
        //Speed is how fast one is to act, and determines placement in the turn order. Higher speed means one is more likely
        //to go first.
        //A base stat is the stat that the unit is "born" with, and doesn't change through the course of the game.
        int baseSpeed;
        //A stat modifier includes all of the modifiers to the stat.
        int speedModifier;
        //A modified stat is the base stat plus the modified stat. It is always updated through the course of the game to reflect
        //any changes.
        int modifiedSpeed;
        //Strength is one's combined physical prowess. Strength modifies damage from its melee weapons and melee physical
        //abilities.
        int baseStrength;
        int strengthModifiers;
        int modifiedStrength;
        //Constitution is how well the body is able to resist physical blows. Constitution modifies HP.
        //Extremely high Constitution levels can grant minor Armor bonuses. These levels are impractical to achieve
        //for most characters, let alone ones the player may own.
        int baseConstitution;
        int constitutionModifiers;
        int modifiedConstitution;
        //Ley Connexion (LEC) is how attuned a person's mind is to the ley. Ley Connexion matters most to any
        //mages or mage-like units because it provides the MP to power their spells, although even physical-oriented classes
        //may use it to power the enchantments on their equipment and weapons. LC modifies MP, magic save, magic defense,
        //and MPRegen (to a lesser extent; level provides most of the MPRegen).
        int baseLeyConnexion;
        int lCModifiers;
        int modifiedLeyConnexion;
        //Combat attributes.
        //Armor is the ability of a unit's armor, natural or manufactured, to resist blows and negate some physical damage.
        int Armor;
        //Magic Defense is the combined ability of a unit's armor (natural or manufactured), and a unit's ley armor,
        //to resist magic attacks and negate some magical damage. Most magic defense is provided by the unit's ley armor,
        //which is an invisible forcefield of ley that envelops those with a moderate or stronger ley connexion. Equipment
        //that provides magic defense is difficult to come by.
        int magicDefense;
        //Magic Save is the ability of the unit to resist special magical effects; often those that cause debuffs. Magic save
        //is much higher in those with a moderate or stronger Ley Connexion.
        int magicSave;
        //Dodge is the ability to dodge attacks at melee or ranged range, and is checked against the attacker's aim.
        int Dodge;
        //Aim is the ability to hit a unit with a melee or ranged attack, and is checked against dodge.
        int Aim;
        //MPRegen represents the magic power that fills a person while they are connected to the ley.
        int MPRegen;
        //Current Initiative Priority is the unit's current placement in the turn order of the current battle.
        //Deprecate this if a new method in a "battle.class" makes this obsolete.
        int currentInitiativePriority;
        //AI attributes, only relevant for units controlled by AI for one reason or another.
        //Tactical knowledge controls how intelligently a unit controlled by AI behaves. It matters more for
        //commanders.
        float tacticalKnowledge;
        //Autonomy controls how 'autonomous' the unit behaves from members of its own team.
        //High autonomy means the unit is less likely to accept instructions from its superiors and will act
        //more-or-less on its own accord.
        //Autonomy is 0 when a unit is charmed.
        float autonomy;
        //Technical attributes.
        bool inBattle; //True if the unit is in the current battle.
        bool controlledByAI; //True if the unit is being controlled by AI, such as if a player sets unit to auto-battle.
        bool autoBattle; //If true, the unit comes under control of an AI.
        bool inPlayerParty; //True if the unit is in the active player party, false if it is a controllable, temporary NPC.
        bool belongsToPlayerTeam; //True if the unit belongs to any human player's team.
        int team; //Specifies the ID of the team that controls the unit.
        //Who is the unit's player owner?
        private  Player owner;
        //What class is the unit?
        private  Class Class;
        //What equipment does the unit have on?
        private Equipment head;
        private Equipment chest;
        private Equipment legs;
        private Equipment boots;
        private Equipment shoulders;
        private Equipment belt;
        private Equipment ring1;
        private Equipment ring2;
        private Equipment trinket;
        private Equipment relic;
        private Weapon currentWeapon;
        //List<Ability> Abilities = new List
        private List<Item> inventory = new List<Item>();
        private List<Effects> effects = new List<Effects>();
        private List<TileEffect> castedTileEffects = new List<TileEffect>();
        int currentMovementPoints;
        int movementPointsToSpend;
        int attackPoints;
        //Descriptors are used by the AI to determine whether this unit is worth targeting over others.
        //Common descriptors for units include "hero", "villain", "famous", "infamous", "leader", and "bounty".
        List<string> descriptors = new List<string>();
        //A unit that is invisible appears as terrain on the map. The terrain is colored the owner's color if the unit is an allied unit.
        bool invisible = false;
        //A unit that is enraged attacks any nearby units (including allies) and is controlled by the AI.
        bool enraged = false;
        //A unit that is confused will take a seemingly random action when its confusion check succeeds.
        //They are handed over to the AI in these scenarios. Possible actions include attacking random units (including allies),
        //hurting themselves, running randomly (or running away from a unit) and doing nothing.
        bool confused = false;
        //A unit that is feared runs away from its fear source.
        bool feared = false;
        Unit fearSource;
        //A unit that is fascinated runs towards the source of fascination, usually just a space, although sometimes also a unit.
        bool fascinated = false;
        Space fascinationSource;


        public Unit(Player owner, Class style,/* bool AI_Controlled,*/ int HP, int Level, int unitPartyID/*, int MP*/)
        {
            this.owner = owner;
            this.Class = style;
            this.baseMaxHP = HP;
            this.currentHP = HP;
            this.level = Level;
            this.unitPartyID = unitPartyID;
            /*this.baseMaxMP = MP;
            this.currentMP = MP;*/
            if (/*AI_Controlled*//*<<Obsolete*/owner.isAI() == false)
            {
                this.belongsToPlayerTeam = true;
                this.controlledByAI = false;
            }
            else
            {
                this.belongsToPlayerTeam = false;
                this.controlledByAI = true;
            }
        }

        public void setHost(Space space)
        {
            this.Host = space;
        }
        public bool getAI()
        {
            return this.controlledByAI;
        }
        public Class getStyle()
        {
            return this.Class;
        }
        public int getLevel()
        {
            return this.level;
        }
        public Player getPlayer()
        {
            return this.owner;
        }
        public int getUnitID()
        {
            return this.unitPartyID;
        }
        public string getName()
        {
            return this.Name;
        }
        public int getInitiativePriority()
        {
            return this.currentInitiativePriority;
        }
        public int getMovementPoints()
        {
            return this.currentMovementPoints;
        }
        public int getMovementPointsToSpend()
        {
            return this.movementPointsToSpend;
        }
        public int getAttackPoints()
        {
            return this.attackPoints;
        }
        public int getBaseMaxHP()
        {
            return this.baseMaxHP;
        }
        public int getCurrentHP()
        {
            return this.currentHP;
        }
        public int getCurrentMP()
        {
            return this.currentMP;
        }
        public int getBaseMaxMP()
        {
            return this.baseMaxMP;
        }
        public int getSpeed()
        {
            return this.baseSpeed;
        }
        public int getStrength()
        {
            return this.baseStrength;
        }
        public int getConstitution()
        {
            return this.baseConstitution;
        }
        public int getLC()
        {
            return this.baseLeyConnexion;
        }
        public int getArmor()
        {
            return this.Armor;
        }
        public int getMagicDefense()
        {
            return this.magicDefense;
        }
        public int getMagicSave()
        {
            return this.magicSave;
        }
        public int getDodge()
        {
            return this.Dodge;
        }
        public int getAim()
        {
            return this.Aim;
        }
        public int getMPRegen()
        {
            return this.MPRegen;
        }
        public Space getHost()
        {
            return this.Host;
        }

        public void dealDamage(int damage)
        {
            /*if (abi.getAbilityType() == 0)
            {
                this.currentHP = this.currentHP - (abi.getDmg() + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedStrength) / 2))));
            }
            else if (abi.getAbilityType() == 1)
            {
                this.currentHP = this.currentHP - (abi.getDmg() + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedLeyConnexion) / 2))));
            }
            else
            {
                this.currentHP = this.currentHP - abi.getDmg();
            }*/
            this.currentHP = this.currentHP - damage;
        }

        public bool checkAlive()
        {
            if (this.currentHP <= 0)
            {
                return false;
            }
            return true;
        }
        public void setName(string x)
        {
            this.Name = x;
        }
        public void setLevel(int x)
        {
            this.level = x;
        }
        public void setCurrentHP(int x)
        {
            this.currentHP = x;
        }
        public void setBaseMaxHP(int x)
        {
            this.baseMaxHP = x;
        }
        public void setCurrentMP(int x)
        {
            this.currentMP = x;
        }
        public void setBaseMaxMP(int x)
        {
            this.baseMaxMP = x;
        }
        public void setSpeed(int x)
        {
            this.baseSpeed = x;
        }
        public void setStrength(int x)
        {
            this.baseStrength = x;
        }
        public void setConstitution(int x)
        {
            this.baseConstitution = x;
        }
        //It's "setLC" for the sake of brevity.
        public void setLC(int x)
        {
            this.baseLeyConnexion = x;
        }
        public void setMainStats(int SPD, int STR, int CON, int LEC)
        {
            this.baseSpeed = SPD;
            this.baseStrength = STR;
            this.baseConstitution = CON;
            this.baseLeyConnexion = LEC;
        }
        public void setArmor(int x)
        {
            this.Armor = x;
        }
        public void setMagicDefense(int x)
        {
            this.magicDefense = x;
        }
        public void setMagicSave(int x)
        {
            this.magicSave = x;
        }
        public void setDodge(int x)
        {
            this.Dodge = x;
        }
        public void setAim(int x)
        {
            this.Aim = x;
        }
        public void setMPRegen(int x)
        {
            this.MPRegen = x;
        }
        public void setCombatStats(int AMR, int MDR, int MGS, int DGE, int AIM, int MPR)
        {
            this.Armor = AMR;
            this.magicDefense = MDR;
            this.magicSave = MGS;
            this.Dodge = DGE;
            this.Aim = AIM;
            this.MPRegen = MPR;
        }
        //Allows the changing of a unit's owner to another player. Useful for defecting units or mind control.
        public void setPlayerOwner(Player x)
        {
            this.owner = x;
        }
        //Allows the changing of a unit's class. If class changing is implemented, this method could be used.
        public void setClass(Class x)
        {
            this.Class = x;
        }
        public void setInitiativePriority(int x)
        {
            this.currentInitiativePriority = x;
        }
        public void setMovementPoints(int x)
        {
            this.currentMovementPoints = x;
        }
        public void setMovementPointsToSpend(int x)
        {
            this.movementPointsToSpend = x;
        }
        public void setAttackPoints(int x)
        {
            this.attackPoints = x;
        }

        internal void Move(Space target)
        {
            this.getHost().setGuest(null);
            this.setHost(target);
            target.setGuest(this);
        }
        internal void setCurrentWeapon(Weapon weapon)
        {
            this.currentWeapon = weapon;
        }
        internal Weapon getCurrentWeapon()
        {
            return this.currentWeapon;
        }
        public void setSpeedModifier(int x)
        {
            this.speedModifier = x;
        }
        public void setStrengthModifier(int x)
        {
            this.strengthModifiers = x;
        }
        public void setConstitutionModifier(int x)
        {
            this.constitutionModifiers = x;
        }
        public void setLCModifiers(int x)
        {
            this.lCModifiers = x;
        }
        public void setModifiedSpeed(int x)
        {
            this.modifiedSpeed = x;
        }
        public void setModifiedStrength(int x)
        {
            this.modifiedStrength = x;
        }
        public void setModifiedConstitution(int x)
        {
            this.modifiedConstitution = x;
        }
        public void setModifiedLC(int x)
        {
            this.modifiedLeyConnexion = x;
        }
        public int getSpeedModifier()
        {
            return this.speedModifier;
        }
        public int getStrengthModifier()
        {
            return this.strengthModifiers;
        }
        public int getConstitutionModifier()
        {
            return this.constitutionModifiers;
        }
        public int getLCModifiers()
        {
            return this.lCModifiers;
        }
        public int getModifiedSpeed()
        {
            return this.modifiedSpeed;
        }
        public int getModifiedStrength()
        {
            return this.modifiedStrength;
        }
        public int getModifiedConstitution()
        {
            return this.modifiedConstitution;
        }
        public int getModifiedLC()
        {
            return this.modifiedLeyConnexion;
        }
        public void setModifiedHP(int x)
        {
            this.modifiedHP = x;
        }
        public void setModifiedMP(int x)
        {
            this.modifiedMP = x;
        }
        public int getModifiedHP()
        {
            return this.modifiedHP;
        }
        public int getModifiedMP()
        {
            return this.modifiedMP;
        }
        public Class getClass()
        {
            return this.Class;
        }
        public int getExperience()
        {
            return this.experience;
        }
        public void setExperience(int x)
        {
            this.experience = x;
        }
        public int getExperienceCap()
        {
            return this.experienceCap;
        }
        public void setExperienceCap(int x)
        {
            this.experienceCap = x;
        }
        public int getBaseExperienceYield()
        {
            return this.baseExperienceYield;
        }
        public int getModifiedExperienceYield()
        {
            return this.modifiedExperienceYield;
        }
        public void setBaseExperienceYield(int x)
        {
            this.baseExperienceYield = x;
        }
        public void setModifiedExperienceYield(int x)
        {
            this.modifiedExperienceYield = x;
        }
        //This method adds an item to the unit's inventory.
        internal void addItem(Item item)
        {
            inventory.Add(item);
        }
        //This method destroys all instances of the specified item from the unit's inventory.
        internal void destroyAllOfItem(Item item)
        {
            inventory.Remove(item);
        }
        //This method removes a specific item at the specified inventory slot.
        public void destroyItemByNumber(int itemSlot)
        {
            inventory.RemoveAt(itemSlot);
        }
        //Gets the item at the specified inventory slot.
        internal Item getItem(int itemSlot)
        {
            return this.inventory[itemSlot];
        }
        //Gets ALL items in the unit's inventory for convenience purposes.
        internal List<Item> getInventory()
        {
            return this.inventory;
        }
        internal List<Weapon> getWeapons()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (Weapon w in inventory)
            {
                weapons.Add(w);
            }
            return weapons;
        }
        internal void applyEffect(Effects effect){
            effects.Add(effect);
        }
        public void applyEffectsFromAbility(Ability ability)
        {
            foreach (Effects w in ability.getEffectsPool())
            {
                effects.Add(w);
            }
        }
        internal void applyEffectsFromList(List<Effects> effectsList)
        {
            foreach (Effects w in effectsList)
            {
                effects.Add(w);
            }
        }
        public void removeEffectAtIndex(int x)
        {
            effects.RemoveAt(x);
        }
        internal List<Effects> getAllActiveEffects()
        {
            return this.effects;
        }
        internal Effects getActiveEffectAtIndex(int x)
        {
            return this.effects[x];
        }
        internal void removeTileEffectAtIndex(int x)
        {
            castedTileEffects.RemoveAt(x);
        }
        //For use only after casting an ability with tile effects.
        internal void addTileEffect(TileEffect x)
        {
            castedTileEffects.Add(x);
        }
        internal TileEffect getTileEffectAtIndex(int x)
        {
            return this.castedTileEffects[x];
        }
        internal List<TileEffect> getAllTileEffects()
        {
            return this.castedTileEffects;
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
        public void setInvisibility(bool x)
        {
            this.invisible = x;
        }
        public bool getIsInvisible()
        {
            return this.invisible;
        }
    }
}