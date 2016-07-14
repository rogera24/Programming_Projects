using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            Game_Controller Game_Gnome = new Game_Controller();
            //In SP campaign, player 1 is the only human player, represented by the color green.
            //In multiplayer, it is the first human player.
            Player Player1 = new Player(0, false, "Green", "Heroes");
            Player1.setPlayerName("Player Forces");
            //In SP campaign, player 2 is an AI player that almost all enemies are owned by, represented by the color red.
            //In multiplayer, it is the second human player.
            Player Player2 = new Player(1, true, "Red", "Villains");
            Player2.setPlayerName("Enemies");
            //In SP campaign, player 3 is an AI player that represents all allies of player 1, and so is unattackable by player 1.
            //Represented by the color yellow.
            //In multiplayer, it is the third human player.
            //In co-op campaign, it is the second human player, allied with player 1 and player 4.
            Player Player3 = new Player(2, true, "Yellow", "Heroes");
            Player3.setPlayerName("Allied Forces");
            //In SP campaign, player 4 is an AI player that represents a faction of enemies that are also hostile
            //towards, and attacks, player 2. Represented by the color magenta.
            //In multiplayer, it is the fourth human player.
            //In co-op campaign with three players, it is the third human player, allied with player 1 and player 3.
            //In co-op campaign with two players, it may either serve the same function as in player 3 in SP campaign, or
            //it may serve the same function as player 4 in SP campaign.
            Player Player4 = new Player (3, true, "Magenta", "Renegades");
            Player4.setPlayerName("Renegades");
            Player1.addPlayerToKnownPlayerList(Player1);
            Player1.addPlayerToKnownPlayerList(Player2);
            Player1.addPlayerToKnownPlayerList(Player3);
            Player1.addPlayerToKnownPlayerList(Player4);
            Player2.addPlayerToKnownPlayerList(Player1);
            Player2.addPlayerToKnownPlayerList(Player2);
            Player2.addPlayerToKnownPlayerList(Player3);
            Player2.addPlayerToKnownPlayerList(Player4);
            Player3.addPlayerToKnownPlayerList(Player1);
            Player3.addPlayerToKnownPlayerList(Player2);
            Player3.addPlayerToKnownPlayerList(Player3);
            Player3.addPlayerToKnownPlayerList(Player4);
            Player4.addPlayerToKnownPlayerList(Player1);
            Player4.addPlayerToKnownPlayerList(Player2);
            Player4.addPlayerToKnownPlayerList(Player3);
            Player4.addPlayerToKnownPlayerList(Player4);
            Player1.addRelationship("self");
            Player1.addRelationship("hostile");
            Player1.addRelationship("ally");
            Player1.addRelationship("hostile");
            Player2.addRelationship("hostile");
            Player2.addRelationship("self");
            Player2.addRelationship("hostile");
            Player2.addRelationship("hostile");
            Player3.addRelationship("ally");
            Player3.addRelationship("hostile");
            Player3.addRelationship("self");
            Player3.addRelationship("hostile");
            Player4.addRelationship("hostile");
            Player4.addRelationship("hostile");
            Player4.addRelationship("hostile");
            Player4.addRelationship("self");
            Field Test_Map = new Field(10, 10);
            Field Test_MultiplayerMap = new Field(8, 8);
            Field Test_MultiplayerMap2 = new Field(10, 10);
            int[] inaccess = { 15, 18, 22, 37, 44, 58, 63, 76 };
            
            Test_Map.setInacces(inaccess);
            //The warrior is a melee oriented fighter.
            Class Warrior = new Class("Warrior", 5, false);
            Warrior.setBaseModifiers(3, 2, 2, 0, 1);
            Warrior.setCombatModifiers(1, 0, 1, 1, 0);
            //Slash is a basic combat ability used with slicing weapons.
            Ability Warr_Slash = new Ability("Slash", 1, 1, 1, 0, 0, false, true, false, 0);
            //Warrior.addAbility(Warr_Slash);
            Weapon Shortsword = new Weapon("Shortsword", 1, 1, 1, 0, 30, 1, "slash", true, false, 0);
            //The skeleton warrior is a weak and generic undead enemy that wields a bow.
            Class SkeletonWarrior = new Class("Skeleton Warrior", 4, false);
            SkeletonWarrior.setBaseModifiers(2, 2, 0, 0, 1);
            SkeletonWarrior.setCombatModifiers(1, 1, 1, 2, 0);
            /*Ability Shortbow = new Ability("Shortbow", 1, 4, 2, 0, 0);*/
            Weapon Shortbow = new Weapon("Shortbow", 1, 2, 2, 0, 30, 1, "pierce", true, false, 0);
            //SkeletonWarrior.addAbility(Shortbow);
            //Warrior.addAbility(Shortbow); //debug
            //Minotaurs are bovine humanoid beasts that roam mazes and wield massive weapons. Highly aggressive.
            Class Minotaur = new Class("Minotaur", 6, false);
            Minotaur.setBaseModifiers(3, 2, 3, 0, 3);
            Minotaur.setCombatModifiers(1, 1, 0, 0, 0);
            //Body slam is a basic melee attack that deals blunt force damage. It's available for larger creatures.
            Ability BodySlam = new Ability("Body Slam", 5, 1, 1, 0, 0, false, true, false, 0);
            Minotaur.addAbility(BodySlam);
            BodySlam.addClassToClassList(Minotaur);
            BodySlam.addMinLevelRequirement(1);
            //Cleave is a dangerous slicing melee attack done with a slashing weapon.
            Ability Cleave = new Ability("Cleave", 6, 1, 1, 0, 0, false, true, false, 0);
            //Minotaur.addAbility(Cleave);
            //A cavalier is a horse-mounted fast attack unit that is generally well-rounded in its stats.
            Class Cavalier = new Class("Cavalier", 7, false);
            Cavalier.setBaseModifiers(2, 2, 2, 0, 1);
            Cavalier.setCombatModifiers(1, 1, 1, 1, 0);
            //Ability Lance = new Ability("Lance", 2, 1, 1, 0, 0);
            //Cavalier.addAbility(Lance);
            Weapon Lance = new Weapon("Lance", 2, 1, 1, 0, 30, 2, "pierce", true, false, 0);
            //Wizards are people who have meditated on the ley to strengthen their Ley Connexion, harnessing its power
            //to cast spells.
            Class Evocationist = new Class("Evocationist", 5, true);
            Evocationist.setBaseModifiers(0, 1, 1, 3, 2);
            Evocationist.setCombatModifiers(3, 3, 1, 2, 3);
            Evocationist.addDescriptor("mage");
            //Ignite is a spell that causes the target to be enveloped in short-lived flames, causing major burns.
            Ability Ignite = new Ability("Ignite", 1, 2, 1, 1, 1, false, true, false, 0);
            //Frostbite is a spell that causes the target to be experience a blast of very chilly air.
            Ability Frostbite = new Ability("Frostbite", 1, 2, 1, 1, 1, false, true, false, 0);
            Evocationist.addAbility(Ignite);
            Ignite.addClassToClassList(Evocationist);
            Ignite.addMinLevelRequirement(1);
            Evocationist.addAbility(Frostbite);
            Frostbite.addClassToClassList(Evocationist);
            Frostbite.addMinLevelRequirement(1);
            //Rabbits are mammals that form the family Leporidae, and are characterized by long ears and being fleet-of-foot.
            Class Rabbit = new Class("Rabbit", 6, false);
            Rabbit.setBaseModifiers(0, 3, 0, 0, 0);
            Rabbit.setCombatModifiers(0, 0, 3, 1, 0);
            Rabbit.addDescriptor("pest");
            Unit Test_Rabbit = new Unit(Player4, Rabbit, 1, 1, 0);
            Test_Rabbit.setName("Joe the Rabbit");
            Test_Rabbit.setMainStats(3, 0, 1, 0);
            Test_Rabbit.setCombatStats(0, 0, 0, 5, 1, 0);
            Unit Test_Warrior = new Unit(Player1, Warrior, 10, 1, 1);
            Test_Warrior.setName("Warrior");
            Test_Warrior.setMainStats(2, 4, 3, 0);
            Test_Warrior.setCombatStats(4, 0, 0, 1, 2, 0);
            Test_Warrior.addItem(Shortsword);
            Test_Warrior.addItem(Shortbow);
            Unit Test_Skeleton = new Unit(Player2, SkeletonWarrior, 3, 1, 2);
            Test_Warrior.addItem(Shortbow);
            Unit Test_Minotaur = new Unit(Player2, Minotaur, 15, 2, 2);
            Weapon Greataxe = new Weapon("Greataxe", 2, 1, 1, 0, 30, 3, "slash", true, false, 0);
            Test_Minotaur.addItem(Greataxe);
            //Null unit represents the absence of a unit, and is an arbitrary measure to enclose the unit array later.
            Unit NULL_UNIT = new Unit(Player4, Warrior, 3, 1, 0);
            Unit NULL_UNIT2 = new Unit(Player4, Warrior, 3, 1, 0);
            Test_Skeleton.setName("Skeleton");
            Test_Minotaur.setName("Minotaur");
            Test_Map.PlaceUnit(Test_Warrior, 55);
            Test_Map.PlaceUnit(Test_Skeleton, 5);
            Test_Map.PlaceUnit(Test_Minotaur, 4);
            Test_Map.PlaceUnit(Test_Rabbit, 93);
            //The hunter is an outdoorsman that specializes in survival. Highly skilled with bows and crossbows.
            Class Hunter = new Class("Hunter", 5, false);
            Hunter.setBaseModifiers(2, 3, 2, 0, 1);
            Hunter.setCombatModifiers(1, 0, 2, 3, 0);
            //Sanara is the main character of the campaign. She is a huntress who hunts wild animals with her party for profit
            //and sustenance.
            Unit Sanara = new Unit(Player1, Hunter, 10, 1, 0);
            Sanara.setName("Sanara");
            Sanara.setMainStats(4, 3, 2, 0);
            Sanara.setCombatStats(0, 0, 1, 5, 5, 0);
            Weapon Longbow = new Weapon("Longbow", 1, 3, 2 , 0, 10, 1, "pierce", true, false, 0);
            Sanara.addItem(Longbow);
            //The fairy mage is a distinct species of fairy that has a strong natural ley connexion compared to normal fairies.
            //Highly mischievous.
            Class FairyMage = new Class("Fairy Mage", 4, true);
            FairyMage.setBaseModifiers(0, 3, 1, 3, 2);
            FairyMage.setCombatModifiers(3, 3, 3, 1, 3);
            FairyMage.addDescriptor("mage");
            FairyMage.addDescriptor("mezzer");
            FairyMage.addAbility(Frostbite);
            Frostbite.addClassToClassList(FairyMage);
            Frostbite.addMinLevelRequirement(1);
            Ability Deja_Vu = new Ability("Deja Vu", 0, 10, 3, 4, 1, false, false, false, 0);
            FairyMage.addAbility(Deja_Vu);
            Deja_Vu.addClassToClassList(FairyMage);
            Deja_Vu.addMinLevelRequirement(2);
            Unit Pontoo = new Unit(Player1, FairyMage, 5, 2, 0);
            //Pontoo is a fairy mage that uses Sanara to free his friend. Accompanies Sanara on a greater quest in order to
            //further his own goals.
            Pontoo.setName("Pontoo");
            Pontoo.setMainStats(5, 1, 1, 5);
            Pontoo.setCombatStats(0, 4, 5, 3, 2, 5);
            //Illusion is a discipline of magic that specializes in altering perception of reality.
            Class Illusionist = new Class("Illusionist", 5, true);
            Illusionist.setBaseModifiers(0, 1, 1, 3, 2);
            Illusionist.setCombatModifiers(3, 3, 1, 2, 3);
            Illusionist.addDescriptor("mage");
            Illusionist.addDescriptor("mezzer");
            //Silent image is an illusion spell that creates a standing, fake copy of the caster at the target location.
            //The copy has 1 health and is destroyed if attacked.
            Ability Silent_Image = new Ability("Silent Image", 0, 3, 1, 3, 1, false, false, false, 0);
            //Obscuring mist creates fog of war of radius 2 on and around the caster. Anybody in the fog can only see 5 feet around them.
            Ability Obscuring_Mist = new Ability("Obscuring Mist", 0, 0, 0, 3, 1, false, false, false, 2);
            Illusionist.addAbility(Obscuring_Mist);
            Obscuring_Mist.addClassToClassList(Illusionist);
            Obscuring_Mist.addMinLevelRequirement(1);
            TileEffect Obscuring_Mist_Fog = new TileEffect("Obscuring Mist", 3, null, 0);
            //The denseFog descriptor is representative of all fog with a vision radius of 1.
            Obscuring_Mist_Fog.addDescriptor("denseFog");
            Obscuring_Mist.addTileEffectToTileEffectPool(Obscuring_Mist_Fog);
            //The gray wolf, canis lupus, is a social predator that hunts prey in packs. Not usually aggressive towards humans,
            //unless their prey items are scarce.
            Class Wolf = new Class("Wolf", 7, false);
            Wolf.setBaseModifiers(3, 3, 2, 0, 1);
            Wolf.setCombatModifiers(0, 0, 2, 1, 0);
            //This is an example of the range function working. It makes a range of three around the warrior and then prints the total spaces and their IDs.
            /*List<Space> list = Game_Gnome.Range(Test_Map, Test_Warrior.getHost(), 3);
            Console.WriteLine(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].getID());
            }

            List<Space> list2 = Game_Gnome.Range(Test_Map, Test_Rabbit.getHost(), 3);
            Console.WriteLine(list2.Count);
            for (int i = 0; i < list2.Count; i++)
            {
                Console.WriteLine(list2[i].getID());
            }*/ //<-- Debug


                //Test_Map.printMap();
                Console.WriteLine("Select from the following gamemodes: singleplayer, multiplayer, challenge");
            string modeInput = Console.ReadLine();
            //Unit[] currentTurn = {Test_Warrior, Test_Skeleton};
            if (modeInput == "challenge")
            {
                Field Army_of_Darkness = new Field(15, 15);
                int[] inaccessible = { 15*11, 15*10 };
                Player1.setSpaceOfInterest(Army_of_Darkness.getSpace(7*0));
                Player2.setSpaceOfInterest(Army_of_Darkness.getSpace(14*15));
                Player3.setSpaceOfInterest(Army_of_Darkness.getSpace(7 * 0));
                Player4.setSpaceOfInterest(Army_of_Darkness.getSpace(7*0));
                Army_of_Darkness.setInacces(inaccessible);
                bool fogOfWar = false;
                Console.WriteLine("Type in the number of players you have to access relevant challenges (from 1-3):");
                if (Console.ReadLine() == "3")
                {
                    Player3.setTeam("Heroes");
                    //Ask for each player's name to use in-game.
                    Console.WriteLine("What is player 1's name?");
                    Player1.setPlayerName(Console.ReadLine());
                    Console.WriteLine("What is player 2's name?");
                    Player3.setPlayerName(Console.ReadLine());
                    Console.WriteLine("Is player 2 an AI player? Say y if true.");
                    if (Console.ReadLine() == "y")
                    {
                        Player3.setIsAI(true);
                    }
                    Console.WriteLine("What is player 3's name?");
                    Player4.setPlayerName(Console.ReadLine());
                    Console.WriteLine("Is player 3 an AI player? Say y if true.");
                    if (Console.ReadLine() == "y")
                    {
                        Player4.setIsAI(true);
                    }
                    Console.WriteLine("Select from the following maps: Army_of_Darkness");
                    if (Console.ReadLine() == "Army_of_Darkness")
                    {
                        List<Unit> currentUnitsTurn = new List<Unit>();
                        List<bool> livingUnitsArray = new List<bool>();
                        List<Unit> unitsArray2 = new List<Unit>();
                        List<Unit> monstersList = new List<Unit>();
                        Sanara.setLevel(10);
                        unitsArray2.Add(Sanara);
                        Player1.addToUnitsOwned(Sanara);
                        Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                        Army_of_Darkness.PlaceUnit(Sanara, 15*14);
                        Pontoo.setLevel(10);
                        unitsArray2.Add(Pontoo);
                        Player1.addToUnitsOwned(Pontoo);
                        Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                        Army_of_Darkness.PlaceUnit(Pontoo, 15*13);
                        for (int i = 0; i < 30; i++)
                        {
                            Unit newMonster = new Unit(Player2, SkeletonWarrior, 5, 1, 0);
                            newMonster.addItem(Shortbow);
                            Player2.addToUnitsOwned(newMonster);
                            newMonster.setName("Skeleton");
                            unitsArray2.Add(newMonster);
                            Player2.setLivingUnitsInBattle(Player2.getLivingUnitsInBattle() + 1);
                            Army_of_Darkness.PlaceUnit(newMonster, i);
                        }
                        //unitsArray2.Add(NULL_UNIT);
                        //unitsArray2.Add(NULL_UNIT2);
                            foreach (Unit r in unitsArray2)
                            {
                                updateStats(r);
                            }
                        bool[] livingUnitsArray2 = { true, true, true, true, true, true };
                        Player[] playersArray1 = { Player1, Player2, Player3, Player4 };
                        List<int> speedArray = new List<int>();
                        //Test_MultiplayerMap2.printMap(Aerith_the_Warrior.getPlayer());
                        for (int w = 99; w >= 0; w--)
                        {
                            foreach (Unit r in unitsArray2)
                            {
                                if (r.getModifiedSpeed() == w)
                                {
                                    currentUnitsTurn.Add(r);
                                    livingUnitsArray.Add(true);
                                }
                            }
                        }
                        fogOfWar = true;
                        Battle2(Game_Gnome, Army_of_Darkness, playersArray1, currentUnitsTurn, livingUnitsArray, NULL_UNIT, NULL_UNIT2, Army_of_Darkness.getSize(), fogOfWar, false);
                    }
                }
            }
            if (modeInput == "multiplayer")
            {
                //Make all the players human controlled.
                Player1.setIsAI(false);
                Player2.setIsAI(false);
                Player3.setIsAI(false);
                Player4.setIsAI(false);
                bool fogOfWar = false;
                //Ask for each player's name to use in-game.
                Console.WriteLine("What is player 1's name?");
                Player1.setPlayerName(Console.ReadLine());
                Console.WriteLine("What is player 1's team?");
                Player1.setTeam(Console.ReadLine());
                Console.WriteLine("Is player 1 an AI player? Say y if true.");
                if (Console.ReadLine() == "y")
                {
                    Player1.setIsAI(true);
                }
                Console.WriteLine("What is player 2's name?");
                Player2.setPlayerName(Console.ReadLine());
                Console.WriteLine("What is player 2's team?");
                Player2.setTeam(Console.ReadLine());
                Console.WriteLine("Is player 2 an AI player? Say y if true.");
                if (Console.ReadLine() == "y")
                {
                    Player2.setIsAI(true);
                }
                Console.WriteLine("What is player 3's name?");
                Player3.setPlayerName(Console.ReadLine());
                Console.WriteLine("What is player 3's team?");
                Player3.setTeam(Console.ReadLine());
                Console.WriteLine("Is player 3 an AI player? Say y if true.");
                if (Console.ReadLine() == "y")
                {
                    Player3.setIsAI(true);
                }
                Console.WriteLine("What is player 4's name?");
                Player4.setPlayerName(Console.ReadLine());
                Console.WriteLine("What is player 4's team?");
                Player4.setTeam(Console.ReadLine());
                Console.WriteLine("Is player 4 an AI player? Say y if true.");
                if (Console.ReadLine() == "y")
                {
                    Player4.setIsAI(true);
                }
                Console.WriteLine("Is fog of war enabled? Say y if true.");
                if (Console.ReadLine() == "y")
                {
                    fogOfWar = true;
                }
                Console.WriteLine("Choose from the following maps: Open_Field, Four_Corners");
                string input = Console.ReadLine();
                if (input == "Open_Field")
                {
                    Player1.setSpaceOfInterest(Test_MultiplayerMap.getSpace(62));
                    Player2.setSpaceOfInterest(Test_MultiplayerMap.getSpace(7));
                    Player3.setSpaceOfInterest(Test_MultiplayerMap.getSpace(0 * 0));
                    Player4.setSpaceOfInterest(Test_MultiplayerMap.getSpace(0 * 0));
                    Console.WriteLine("\n");
                    Console.WriteLine("A battle has begun. There are two warriors on red team and two warriors on green team.");
                    returnPlayerColor(Player1);
                    Console.Write(Player1.getPlayerName());
                    Console.ResetColor();
                    Console.Write(" VS ");
                    returnPlayerColor(Player2);
                    Console.Write(Player2.getPlayerName());
                    Console.ResetColor();
                    Console.WriteLine("\n" + "FIGHT!");
                    Unit Aerith_the_Warrior = new Unit(Player1, Warrior, 5, 1, 0);
                    Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                    Unit Bob_the_Warrior = new Unit(Player1, Evocationist, 6, 1, 0);
                    Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                    Unit Daniel_the_Warrior = new Unit(Player2, Warrior, 5, 1, 0);
                    Player2.setLivingUnitsInBattle(Player2.getLivingUnitsInBattle() + 1);
                    Unit Lucelle_the_Warrior = new Unit(Player2, Evocationist, 6, 1, 0);
                    Player2.setLivingUnitsInBattle(Player2.getLivingUnitsInBattle() + 1);
                    Aerith_the_Warrior.setMainStats(3, 2, 2, 0);
                    Bob_the_Warrior.setMainStats(2, 3, 3, 2);
                    Daniel_the_Warrior.setMainStats(3, 2, 2, 0);
                    Lucelle_the_Warrior.setMainStats(2, 3, 3, 2);
                    Player1.addToUnitsOwned(Aerith_the_Warrior);
                    Player1.addToUnitsOwned(Bob_the_Warrior);
                    Player2.addToUnitsOwned(Daniel_the_Warrior);
                    Player2.addToUnitsOwned(Lucelle_the_Warrior);
                    Aerith_the_Warrior.setName("Aerith");
                    Bob_the_Warrior.setName("Bob");
                    Daniel_the_Warrior.setName("Daniel");
                    Lucelle_the_Warrior.setName("Lucelle");
                    Aerith_the_Warrior.addItem(Shortsword);
                    Aerith_the_Warrior.addItem(Shortbow);
                    Daniel_the_Warrior.addItem(Shortsword);
                    Daniel_the_Warrior.addItem(Shortbow);
                    Test_MultiplayerMap.PlaceUnit(Aerith_the_Warrior, 7);
                    Test_MultiplayerMap.PlaceUnit(Bob_the_Warrior, 3);
                    Test_MultiplayerMap.PlaceUnit(Daniel_the_Warrior, 62);
                    Test_MultiplayerMap.PlaceUnit(Lucelle_the_Warrior, 58);
                    List<Unit> currentUnitsTurn = new List<Unit>();
                    List<bool> livingUnitsArray = new List<bool>();
                    Unit[] unitsArray2 = { Aerith_the_Warrior, Bob_the_Warrior, Daniel_the_Warrior, Lucelle_the_Warrior, NULL_UNIT, NULL_UNIT2 };
                    foreach (Unit r in unitsArray2)
                    {
                        updateStats(r);
                    }
                    bool[] livingUnitsArray2 = { true, true, true, true, true, true };
                    Player[] playersArray1 = { Player1, Player2, Player3, Player4 };
                    List<int> speedArray = new List<int>();
                    //Test_MultiplayerMap2.printMap(Aerith_the_Warrior.getPlayer());
                    for (int w = 99; w >= 0; w--)
                    {
                        foreach (Unit r in unitsArray2)
                        {
                            if (r.getModifiedSpeed() == w)
                            {
                                currentUnitsTurn.Add(r);
                                livingUnitsArray.Add(true);
                            }
                        }
                    }
                Battle2(Game_Gnome, Test_MultiplayerMap, playersArray1, currentUnitsTurn, livingUnitsArray, NULL_UNIT, NULL_UNIT2, 64, fogOfWar, false);
                }
                else if (input == "Four_Corners")
                {
                    Player1.setSpaceOfInterest(Test_MultiplayerMap2.getSpace(50));
                    Player2.setSpaceOfInterest(Test_MultiplayerMap2.getSpace(50));
                    Player3.setSpaceOfInterest(Test_MultiplayerMap2.getSpace(50));
                    Player4.setSpaceOfInterest(Test_MultiplayerMap2.getSpace(50));
                    List<Unit> unitsArray = new List<Unit>();
                    Console.WriteLine("\n");
                    Console.WriteLine("A battle has begun. There are three warriors on green team, three warriors on red team, three warriors on yellow team, and three warriors on magenta team.");
                    returnPlayerColor(Player1);
                    Console.Write(Player1.getPlayerName());
                    Console.ResetColor();
                    Console.Write(" VS ");
                    returnPlayerColor(Player2);
                    Console.Write(Player2.getPlayerName());
                    Console.ResetColor();
                    Console.Write(" VS ");
                    returnPlayerColor(Player3);
                    Console.Write(Player3.getPlayerName());
                    Console.ResetColor();
                    Console.Write(" VS ");
                    returnPlayerColor(Player4);
                    Console.Write(Player4.getPlayerName());
                    Console.ResetColor();
                    Console.WriteLine("\n" + "FIGHT!");
                    Unit Aerith_the_Warrior = new Unit(Player1, Warrior, 5, 1, 0);
                    Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                    Player1.addToUnitsOwned(Aerith_the_Warrior);
                    Unit Bob_the_Warrior = new Unit(Player1, Illusionist, 6, 1, 0);
                    Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                    Player1.addToUnitsOwned(Bob_the_Warrior);
                    Unit Joe_the_Warrior = new Unit(Player1, Cavalier, 8, 2, 0);
                    Player1.setLivingUnitsInBattle(Player1.getLivingUnitsInBattle() + 1);
                    Player1.addToUnitsOwned(Joe_the_Warrior);
                    Unit Daniel_the_Warrior = new Unit(Player2, Warrior, 5, 1, 0);
                    Player2.setLivingUnitsInBattle(Player2.getLivingUnitsInBattle() + 1);
                    Player2.addToUnitsOwned(Daniel_the_Warrior);
                    Unit Lucelle_the_Warrior = new Unit(Player2, Evocationist, 6, 1, 0);
                    Player2.setLivingUnitsInBattle(Player2.getLivingUnitsInBattle() + 1);
                    Player2.addToUnitsOwned(Lucelle_the_Warrior);
                    Unit Woden_the_Warrior = new Unit(Player2, Cavalier, 8, 2, 0);
                    Player2.setLivingUnitsInBattle(Player2.getLivingUnitsInBattle() + 1);
                    Player2.addToUnitsOwned(Woden_the_Warrior);
                    Unit Jacob_the_Warrior = new Unit(Player3, Warrior, 6, 1, 0);
                    Player3.setLivingUnitsInBattle(Player3.getLivingUnitsInBattle() + 1);
                    Player3.addToUnitsOwned(Jacob_the_Warrior);
                    Unit Paul_the_Warrior = new Unit(Player3, Evocationist, 5, 1, 0);
                    Player3.setLivingUnitsInBattle(Player3.getLivingUnitsInBattle() + 1);
                    Player3.addToUnitsOwned(Paul_the_Warrior);
                    Unit Tempest_the_Warrior = new Unit(Player3, Cavalier, 8, 2, 0);
                    Player3.setLivingUnitsInBattle(Player3.getLivingUnitsInBattle() + 1);
                    Player3.addToUnitsOwned(Tempest_the_Warrior);
                    Unit Michael_the_Warrior = new Unit(Player4, Warrior, 6, 1, 0);
                    Player4.setLivingUnitsInBattle(Player4.getLivingUnitsInBattle() + 1);
                    Player4.addToUnitsOwned(Michael_the_Warrior);
                    Unit Lissabeth_the_Warrior = new Unit(Player4, Evocationist, 5, 1, 0);
                    Player4.setLivingUnitsInBattle(Player4.getLivingUnitsInBattle() + 1);
                    Player4.addToUnitsOwned(Lissabeth_the_Warrior);
                    Unit Joshua_the_Warrior = new Unit(Player4, Cavalier, 8, 2, 0);
                    Player4.setLivingUnitsInBattle(Player4.getLivingUnitsInBattle() + 1);
                    Player4.addToUnitsOwned(Joshua_the_Warrior);
                    Aerith_the_Warrior.setMainStats(3, 2, 2, 0);
                    Bob_the_Warrior.setMainStats(2, 3, 3, 2);
                    Joe_the_Warrior.setMainStats(4, 3, 2, 0);
                    Daniel_the_Warrior.setMainStats(3, 2, 2, 0);
                    Lucelle_the_Warrior.setMainStats(2, 3, 3, 2);
                    Woden_the_Warrior.setMainStats(4, 3, 2, 0);
                    Jacob_the_Warrior.setMainStats(3, 2, 2, 0);
                    Paul_the_Warrior.setMainStats(2, 3, 3, 2);
                    Tempest_the_Warrior.setMainStats(4, 3, 2, 0);
                    Michael_the_Warrior.setMainStats(3, 2, 2, 0);
                    Lissabeth_the_Warrior.setMainStats(2, 3, 3, 2);
                    Joshua_the_Warrior.setMainStats(4, 3, 2, 0);
                    Aerith_the_Warrior.setName("Aerith");
                    Bob_the_Warrior.setName("Bob");
                    Joe_the_Warrior.setName("Joe");
                    Daniel_the_Warrior.setName("Daniel");
                    Lucelle_the_Warrior.setName("Lucelle");
                    Woden_the_Warrior.setName("Woden");
                    Jacob_the_Warrior.setName("Jacob");
                    Paul_the_Warrior.setName("Paul");
                    Tempest_the_Warrior.setName("Tempest");
                    Michael_the_Warrior.setName("Michael");
                    Lissabeth_the_Warrior.setName("Lissabeth");
                    Joshua_the_Warrior.setName("Joshua");
                    Aerith_the_Warrior.addItem(Shortsword);
                    Aerith_the_Warrior.addItem(Shortbow);
                    Daniel_the_Warrior.addItem(Shortsword);
                    Daniel_the_Warrior.addItem(Shortbow);
                    Jacob_the_Warrior.addItem(Shortsword);
                    Jacob_the_Warrior.addItem(Shortbow);
                    Michael_the_Warrior.addItem(Shortsword);
                    Michael_the_Warrior.addItem(Shortbow);
                    Joe_the_Warrior.addItem(Lance);
                    Woden_the_Warrior.addItem(Lance);
                    Tempest_the_Warrior.addItem(Lance);
                    Joshua_the_Warrior.addItem(Lance);
                    Test_MultiplayerMap2.PlaceUnit(Aerith_the_Warrior, 1);
                    Test_MultiplayerMap2.PlaceUnit(Bob_the_Warrior, 0+10);
                    Test_MultiplayerMap2.PlaceUnit(Joe_the_Warrior, 0);
                    Test_MultiplayerMap2.PlaceUnit(Daniel_the_Warrior, 9+10);
                    Test_MultiplayerMap2.PlaceUnit(Lucelle_the_Warrior, 8);
                    Test_MultiplayerMap2.PlaceUnit(Woden_the_Warrior, 9);
                    Test_MultiplayerMap2.PlaceUnit(Jacob_the_Warrior, 99-9+1);
                    Test_MultiplayerMap2.PlaceUnit(Paul_the_Warrior, 99-10-9);
                    Test_MultiplayerMap2.PlaceUnit(Tempest_the_Warrior, 99-9);
                    Test_MultiplayerMap2.PlaceUnit(Michael_the_Warrior, 99-10);
                    Test_MultiplayerMap2.PlaceUnit(Lissabeth_the_Warrior, 99-1);
                    Test_MultiplayerMap2.PlaceUnit(Joshua_the_Warrior, 99);
                    List<Unit> currentUnitsTurn = new List<Unit>();
                    List<bool> livingUnitsArray = new List<bool>();
                    Unit[] unitsArray2 = { Aerith_the_Warrior, Bob_the_Warrior, Joe_the_Warrior, Daniel_the_Warrior, Lucelle_the_Warrior, Woden_the_Warrior, Jacob_the_Warrior, Paul_the_Warrior, Tempest_the_Warrior, Michael_the_Warrior, Lissabeth_the_Warrior, Joshua_the_Warrior};
                    bool[] livingUnitsArray2 = { true, true, true, true, true, true, true, true, true, true, true, true};
                    foreach (Unit r in unitsArray2)
                    {
                        updateStats(r);
                    }
                    Player[] playersArray1 = { Player1, Player2, Player3, Player4 };
                    List<int> speedArray = new List<int>();
                    //Test_MultiplayerMap2.printMap(Aerith_the_Warrior.getPlayer());
                    for (int w = 99; w >= 0; w-- ){
                        foreach (Unit r in unitsArray2)
                        {
                            if (r.getModifiedSpeed() == w)
                            {
                                currentUnitsTurn.Add(r);
                                livingUnitsArray.Add(true);
                            }
                        }
                    }
                        Battle2(Game_Gnome, Test_MultiplayerMap2, playersArray1, currentUnitsTurn, livingUnitsArray, NULL_UNIT, NULL_UNIT2, 10 * 10, fogOfWar, false);
                }
            }
            if (modeInput == "singleplayer")
            {
                List<Unit> currentUnitsTurn = new List<Unit>();
                List<bool> livingUnitsArray = new List<bool>();
                List<Unit> unitsArray2 = new List<Unit>();
                List<Unit> monstersList = new List<Unit>();
                unitsArray2.Add(Test_Warrior);
                unitsArray2.Add(Test_Skeleton);
                unitsArray2.Add(Test_Rabbit);
                unitsArray2.Add(Test_Minotaur);
                Player[] playersArray1 = { Player1, Player2, Player3, Player4 };
                List<int> speedArray = new List<int>();
                //Test_MultiplayerMap2.printMap(Aerith_the_Warrior.getPlayer());
                foreach (Unit r in unitsArray2)
                {
                    updateStats(r);
                }
                for (int w = 99; w >= 0; w--)
                {
                    foreach (Unit r in unitsArray2)
                    {
                        if (r.getModifiedSpeed() == w)
                        {
                            r.getPlayer().setLivingUnitsInBattle(r.getPlayer().getLivingUnitsInBattle() + 1);
                            r.getPlayer().addToUnitsOwned(r);
                            currentUnitsTurn.Add(r);
                            livingUnitsArray.Add(true);
                        }
                    }
                }
                //Battle(Game_Gnome, Test_Warrior, Test_Map, Test_Skeleton, Test_Minotaur, NULL_UNIT, Test_Rabbit); //Obsolete
                Battle2(Game_Gnome, Test_Map, playersArray1, currentUnitsTurn, livingUnitsArray, NULL_UNIT, NULL_UNIT2, Test_Map.getSize(), false, false);
            }
        }
        static void Battle2(Game_Controller Game_Gnome, Field Map, Player[] Players, List<Unit> currentUnitsTurn, List<bool> livingUnits, Unit NULL_UNIT, Unit NULL_UNIT2, int CurrentMapSize, bool fogOfWar, bool isCampaignMission)
        {
            foreach (Unit o in currentUnitsTurn)
            {
                updateStats(o);
                o.setCurrentHP(o.getModifiedHP());
                o.setCurrentMP(o.getModifiedMP());
            }
            List<Unit> previousSpaceList = new List<Unit>();
            foreach (Unit o in currentUnitsTurn)
            {
                previousSpaceList.Add(o);
            }
            List<Space> previousSpace = new List<Space>();
            foreach (Unit o in previousSpaceList)
            {
                previousSpace.Add(o.getHost());
            }
            int i = 0;
            int numberOfPlayersDefeated = 0;
            Player currentControlledPlayer = Players[0];
            bool fogOfWarEnabled = fogOfWar;
            //If the battle has just AIs, this will be set to true.
            bool AIOnlyBattle = true;
            /*Unit[] currentTurn;
            for (int j = 0; j<4 ; i++)
            {
                for (int w = 0; w<4; w++)
                {
                    if ()
                }
            }*/
            /*NYI. Intended to sort units by initiative order, adding them to the array that currently serves
        the purpose of being the current initiative order.*/
            //This array contains the initiative order for this battle.
            //Unit[] currentTurn = { NULL_UNIT }; //Obsolete
            //The following array, like the initiative order, will be used to insure that units killed in action have
            //been accounted for by the game engine.
            //bool[] currentTurnAlive = { true, true, true, true, true }; //Obsolete
            int currentTurnLength = currentUnitsTurn.Count;
            bool[] playersDefeated = { false, false, false, false };
            /*try
            {
                string unitName = currentTurn[i].getName();
                bool unitIsControlledByAI = currentTurn[i].getAI();
                Unit currentUnit = currentTurn[i];
            }
            catch
            {
                if (i > currentTurnLength || i < 0) { i = 0; }
            }*/
            //Obsolete
            /*The debug cheat below allows the player to control all units belonging to AI. Notice that the cheat
             is only designed to give the player control of the AI teams; it DOESN'T switch the allegiance of the units
             belonging under AI teams to the player's team. The resulting effect is that the enemy team's units can
             still attack the players.*/
            bool cheatControlAI = false;
            string AIBehavior = "attack+RandomMove";
            bool pointsGranted = false;
            bool hasMoved = false;
            int originalSpot = 1;
            //The variable below is for purposes of the AI. If the AI can't seem to move anywhere, it gives up trying
            //and ends its turn.
            int numberOfMovementAttempts = 0;
            foreach (Player w in Players)
            {
                if (w.isAI() == false)
                {
                    AIOnlyBattle = false;
                }
            }
            bool currentControlledPlayerHasBeenSet = false;
            for (int q = 0; q < currentUnitsTurn.Count(); q++)
            {
                if (currentUnitsTurn[q].getPlayer().isAI() == false && currentControlledPlayerHasBeenSet == false)
                {
                    currentControlledPlayer = currentUnitsTurn[q].getPlayer();
                    currentControlledPlayerHasBeenSet = true;
                }
            }
            bool setup = true;
            int numberOfHumanPlayers = 0;
            foreach (Player w in Players){
                if (w.isAI()==false){
                    numberOfHumanPlayers++;
                }
            }
            List<Unit> alliedUnits0 = new List<Unit>();
            List<Unit> alliedUnits1 = new List<Unit>();
            List<Unit> alliedUnits2 = new List<Unit>();
            List<Unit> alliedUnits3 = new List<Unit>();
            /*List<Unit> alliedUnits0 = Players[0].getAllUnitsFromUnitsOwned();
            List<Unit> alliedUnits1 = Players[1].getAllUnitsFromUnitsOwned();
            List<Unit> alliedUnits2 = Players[2].getAllUnitsFromUnitsOwned();
            List<Unit> alliedUnits3 = Players[3].getAllUnitsFromUnitsOwned();*/
            List<Unit> temporaryUnitList = new List<Unit>();
            List<String> activeTeams = new List<String>();
                foreach (Space r in Map.getAllSpaces())
                {
                    Fog newFog = new Fog(Map, r, 2, false, "Fog of War Fog");
                    r.addFog(newFog);
                }
                while (true)
                {
                    //Get the name stored in the unit object's data, and store it in a variable.
                    //Variable 'i' represents the slot in the array that the unit occupies.
                    foreach (Unit o in currentUnitsTurn)
                    {
                        while (o.getExperience() >= o.getExperienceCap())
                        {
                            o.setExperience(o.getExperience() - o.getExperienceCap());
                            o.setLevel(o.getLevel() + 1);
                            Console.WriteLine("DING!");
                            Console.WriteLine(o.getName() + " leveled up!");
                            Console.WriteLine(o.getName() + " is now level " + o.getLevel() + ".");
                        }
                        updateStats(o);
                    }
                    List<Space> visibleSpaces = new List<Space>();
                    List<Space> visibleSpaces0 = new List<Space>();
                    List<Space> visibleSpaces1 = new List<Space>();
                    List<Space> visibleSpaces2 = new List<Space>();
                    List<Space> visibleSpaces3 = new List<Space>();
                    List<Unit> alliedUnits = new List<Unit>();
                    /*List<Unit> alliedUnits0 = new List<Unit>();
                    List<Unit> alliedUnits1 = new List<Unit>();
                    List<Unit> alliedUnits2 = new List<Unit>();
                    List<Unit> alliedUnits3 = new List<Unit>();*/
                    string unitName = null;
                    bool unitIsControlledByAI = false;
                    Unit currentUnit = null;
                    AI bobTheAI = new AI("Bob", Game_Gnome, Map, Players, currentUnitsTurn, fogOfWar);
                    try
                    {
                        unitName = currentUnitsTurn[i].getName();
                        //Get whether or not the unit is controlled by an AI player in the unit object's data, and store it in a var.
                        unitIsControlledByAI = currentUnitsTurn[i].getAI();
                        //Get the unit whose turn it currently is, and store it in a variable.
                        currentUnit = currentUnitsTurn[i];
                    }
                    catch
                    {

                    }
                    /*When the game engine attempts to reference a non-existent slot in the array, it returns an exception.
                     The reason why it would be doing this is if the value of i falls out of the range of acceptable values in
                     the array. In order to avoid this, I implemented the NULL_UNIT to represent the empty values in the array,
                     so the array could return to a proper array slot
                     It is only a temporary solution to the problem unless a better solution can be implemented.*/
                    //if (i>=currentTurnLength/*currentUnit == NULL_UNIT || currentUnit == NULL_UNIT2*/ /*<--Obsolete*/)
                    {
                        //i = 0;
                        while (true)
                        {
                            if (i >= currentTurnLength)
                            {
                                i = 0;
                            }
                            //if (livingUnits[i] == false/* || currentUnitsTurn[i].checkAlive()==false*/)*/ 
                            {
                                if (currentUnitsTurn[i].getCurrentHP() > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    KillUnit(currentUnitsTurn[i], Map);
                                    livingUnits[i] = false;
                                    i = i + 1;
                                }
                            }
                        }
                    }
                    /*while (i < currentTurnLength) {
                        if (currentUnitsTurn[i].checkAlive() == false)
                        {
                            i = i + 1;
                        }
                    }*/
                    //After the engine ensures that the array used here falls on an existent slot, we can now assign values
                    //to variables representing the returned values of our units.
                    unitName = currentUnitsTurn[i].getName();
                    unitIsControlledByAI = currentUnitsTurn[i].getAI();
                    currentUnit = currentUnitsTurn[i];
                    currentUnitsTurn[i].setInitiativePriority(i); /*Deprecated. Originally created to allow me to access
                                                              the array positions of the units.*/
                    int currentUnitMovementPointsToGrant = currentUnit.getStyle().getMovementPoints();
                    int currentUnitAttackPointsToGrant = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Convert.ToDouble(currentUnit.getLevel()) / Convert.ToDouble(5))));
                    //This loop checks each unit in the current battle to see if they're living or not. If the unit
                    //isn't living, it will mark them off as dead and send an appropriate message to the console.
                    for (int m = 0; m < currentTurnLength; m++)
                    {
                        if (livingUnits[m] == true)
                        {
                            if (currentUnitsTurn[m].checkAlive() == false)
                            {
                                Console.WriteLine(currentUnitsTurn[m].getName() + " died!");
                                //The dead body is removed from the map.
                                KillUnit(currentUnitsTurn[m], Map);
                                currentUnitsTurn[m].getPlayer().setLivingUnitsInBattle(currentUnitsTurn[m].getPlayer().getLivingUnitsInBattle() - 1);
                                livingUnits[m] = false;
                            }
                        }
                        if (currentUnitsTurn[m].getCurrentHP() <= 0)
                        {
                            KillUnit(currentUnitsTurn[m], Map);
                            livingUnits[m] = false;
                        }
                    }
                    //Grant units their movement points and attack points.
                    if (pointsGranted == false)
                    {
                        if (currentUnit != NULL_UNIT && currentUnit != NULL_UNIT2)
                        {
                            //Console.WriteLine("Your previous space: " + previousSpace[i].getX() + ", " + previousSpace[i].getY());
                            previousSpace[i] = currentUnit.getHost();
                            //Console.WriteLine("Your current space: " + previousSpace[i].getX() + ", " + previousSpace[i].getY());
                        }
                        hasMoved = false;
                        currentUnit.setMovementPoints(currentUnitMovementPointsToGrant);
                        currentUnit.setMovementPointsToSpend(currentUnitMovementPointsToGrant);
                        currentUnit.setAttackPoints(currentUnitAttackPointsToGrant);
                        currentUnit.setCurrentMP(currentUnit.getCurrentMP() + currentUnit.getMPRegen());
                        //Do processing for certain common effects.
                        foreach (Effects e in currentUnit.getAllActiveEffects())
                        {
                            foreach (string s in e.getAllDescriptors())
                            {
                                if (s == "dot")
                                {
                                    currentUnit.setCurrentHP(currentUnit.getCurrentHP() - e.getMagnitude());
                                    Console.WriteLine(currentUnit.getName() + " has taken " + e.getMagnitude() + " damage from " + e.getEffectName() + ".");
                                }
                                if (s == "dmot")
                                {
                                    currentUnit.setCurrentMP(currentUnit.getCurrentMP() - e.getMagnitude());
                                    Console.WriteLine(currentUnit.getName() + " has had " + e.getMagnitude() + " MP sapped from " + e.getEffectName() + ".");
                                }
                                if (s == "hot")
                                {
                                    currentUnit.setCurrentHP(currentUnit.getCurrentHP() + e.getMagnitude());
                                    Console.WriteLine(currentUnit.getName() + " has gained " + e.getMagnitude() + " HP from " + e.getEffectName() + ".");
                                }
                                if (s == "hmot")
                                {
                                    currentUnit.setCurrentMP(currentUnit.getCurrentMP() + e.getMagnitude());
                                    Console.WriteLine(currentUnit.getName() + " has gained " + e.getMagnitude() + " MP from " + e.getEffectName() + ".");
                                }
                                if (s == "snare")
                                {
                                    currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - e.getMagnitude());
                                    Console.WriteLine(currentUnit.getName() + " has lost " + e.getMagnitude() + " movement points to " + e.getEffectName() + ".");
                                }
                                if (s == "startled")
                                {
                                    currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                    Console.WriteLine(currentUnit.getName() + " has lost an attack to " + e.getEffectName());
                                }
                            }
                        }
                        Game_Gnome.checkEffectDurations(currentUnit);
                        Game_Gnome.checkTileEffectDurations(currentUnit);
                        //Insure that "bonus MP" goes away.
                        if (currentUnit.getCurrentMP() > currentUnit.getModifiedMP())
                        {
                            currentUnit.setCurrentMP(currentUnit.getModifiedMP());
                        }
                        pointsGranted = true;
                    }
                    if (cheatControlAI == true)
                    {
                        unitIsControlledByAI = false;
                    }
                    else if (cheatControlAI == false)
                    {
                        unitIsControlledByAI = currentUnitsTurn[i].getAI();
                    }
                    if (i > currentTurnLength || i < 0) { i = 0; }
                    for (int o = 0; o <= 3; o++)
                    {
                        Players[o].updateTeamRelations(false);
                    }
                    if (fogOfWar == true)
                    {
                        foreach (Space w in Map.getAllSpaces())
                        {
                            foreach (Fog f in w.getAllFogs())
                            {
                                //if (f.getName() == "Fog of War Fog")
                                if (f == null)
                                {

                                }
                                else
                                {
                                    f.setActive(true);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Space w in Map.getAllSpaces())
                        {
                            foreach (Fog f in w.getAllFogs())
                            {
                                //try
                                {
                                    if (f == null)
                                    {
                                        //f.setActive(false);
                                    }
                                    else if (f.getName() == "Fog of War Fog")
                                    {
                                        f.setActive(false);
                                    }
                                }
                                //catch
                                {

                                }
                            }
                        }
                    }
                    foreach (Space w in Map.getAllSpaces())
                    {
                        w.updateDominantFog();
                    }
                    //Console.WriteLine("Name of dominant fog: " + currentUnit.getHost().getDominantFog().getName());
                    Console.Write("It is now "/* + unitName + "'s turn."*/ /*<<Obsolete. I can color the text.*/);
                    returnPlayerColor(currentUnit.getPlayer());
                    Console.Write(unitName + "'s ");
                    Console.ResetColor();
                    Console.Write("turn.");
                    Console.WriteLine("\n");
                    //Console.WriteLine(unitName); //debug
                    //Console.WriteLine(unitIsControlledByAI); //debug
                    //Console.WriteLine(currentUnit); //debug
                    //Console.WriteLine(currentTurnLength); //debug
                    //Console.WriteLine("It is now " + currentTurn[i].getName() + "'s turn."); Obsolete
                    //The variable below will be used to check if it's a player's turn. If it's not a player's turn, control
                    //is handed over to the AI.
                    bool isPlayerTurn = false;
                    //If the unit whose turn it is is a player unit, it turns control of the unit over to the player.
                    if (unitIsControlledByAI == false) { isPlayerTurn = true; }
                    /*if (isPlayerTurn == true || AIOnlyBattle == true || setup==true)
                    {
                        if (setup == false)
                        {
                            currentControlledPlayer = currentUnit.getPlayer();
                        }
                        alliedUnits = currentControlledPlayer.getAllUnitsFromUnitsOwned();
                        for (int selectedUnit = 0; selectedUnit < alliedUnits.Count; selectedUnit++)
                        {
                            foreach (Space w in Game_Gnome.Range(Map, alliedUnits[selectedUnit].getHost(), 2))
                            {
                                visibleSpaces.Add(w);
                            }
                        }
                        if (fogOfWarEnabled == true)
                        {
                            foreach (Space w in Map.getAllSpaces())
                            {
                                w.setConcealed(true, currentControlledPlayer.getPlayerID());
                            }
                            foreach (Space w in visibleSpaces)
                            {
                                w.setConcealed(false, currentControlledPlayer.getPlayerID());
                            }
                        }
                        setup = false;
                    }*/
                    List<Space> fogOfWarSpaces = new List<Space>();
                    foreach (Space f in Map.getAllSpaces())
                    {
                        List<Fog> temporaryFogList = f.getAllFogs();
                        for (int t = 0; t < temporaryFogList.Count(); t++)
                        {
                            if (temporaryFogList[t] == null) { }
                            else if (temporaryFogList[t].getName() == "Dense Fog")
                            {
                                temporaryFogList[t] = null;
                            }
                        }
                    }
                    foreach (Space f in Map.getAllSpaces())
                    {
                        foreach (TileEffect t in f.getAllTileEffects())
                        {
                            if (t == null)
                            {

                            }
                            else
                            {
                                foreach (string d in t.getAllDescriptors())
                                {
                                    if (d == "denseFog")
                                    {
                                        f.addFog(new Fog(Map, f, 1, true, "Dense Fog"));
                                    }
                                }
                            }
                        }
                    }
                    foreach (Space f in Map.getAllSpaces())
                    {
                        if (f.getDominantFog() == null)
                        {

                        }
                        else
                        {
                            fogOfWarSpaces.Add(f);
                        }
                    }
                    alliedUnits0.RemoveRange(0, alliedUnits0.Count());
                    alliedUnits0.AddRange(Players[0].getAllUnitsFromUnitsOwned());
                    for (int p = 0; p <= 3; p++)
                    {
                        if (Players[p].getRelationshipToPlayer(0) == "ally")
                        {
                            alliedUnits0.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                        }
                    }
                    
                    for (int selectedUnit = 0; selectedUnit < alliedUnits0.Count; selectedUnit++)
                    {
                        if (alliedUnits0[selectedUnit].checkAlive() == true)
                        {
                            foreach (Space u in Map.getAllSpaces())
                            {
                                //If there is no dominant fog over this space, ignore the rest of this instance of foreach
                                if (u.getDominantFog() == null)
                                {
                                    visibleSpaces0.Add(u);
                                }
                                else
                                {
                                    //Instanstiate a list of spaces that represent the current range of spaces.
                                    List<Space> currentEvaluatedRange = new List<Space>();
                                    //For each number from 0 to this space's vision radius, check to see if the selected unit
                                    //is in vision range. If it is, add this space to visible spaces.
                                    for (int q = 0; q <= u.getDominantFog().getVisionRadius(); q++ )
                                    {
                                        currentEvaluatedRange = Game_Gnome.Range(Map, alliedUnits0[selectedUnit].getHost(), q);
                                        foreach (Space g in currentEvaluatedRange)
                                        {
                                            if (g == u/* && u.getDominantFog().getActive() == true*/)
                                            {
                                                visibleSpaces0.Add(u);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            /*for (int r = 0; r <= 2; r++)
                            {
                                foreach (Space w in Game_Gnome.Range(Map, alliedUnits0[selectedUnit].getHost(), r))
                                {
                                    visibleSpaces0.Add(w);
                                }
                            }*/ //Obsolete
                        }
                    }
                    //if (fogOfWarEnabled == true)
                    {
                        foreach (Space w in fogOfWarSpaces)
                        {
                            w.setConcealed(true, Players[0].getPlayerID());
                        }
                        foreach (Space w in visibleSpaces0)
                        {
                            w.setConcealed(false, Players[0].getPlayerID());
                        }
                    }
                    alliedUnits1.RemoveRange(0, alliedUnits1.Count());
                    alliedUnits1.AddRange(Players[1].getAllUnitsFromUnitsOwned());
                    for (int p = 0; p <= 3; p++)
                    {
                        if (Players[p].getRelationshipToPlayer(1) == "ally")
                        {
                            alliedUnits1.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                        }
                    }
                    for (int selectedUnit = 0; selectedUnit < alliedUnits1.Count; selectedUnit++)
                    {
                        if (alliedUnits1[selectedUnit].checkAlive() == true)
                        {
                            foreach (Space u in Map.getAllSpaces())
                            {
                                //If there is no dominant fog over this space, ignore the rest of this instance of foreach
                                if (u.getDominantFog() == null)
                                {
                                    visibleSpaces1.Add(u);
                                }
                                else
                                {
                                    //Instanstiate a list of spaces that represent the current range of spaces.
                                    List<Space> currentEvaluatedRange = new List<Space>();
                                    //For each number from 0 to this space's vision radius, check to see if the selected unit
                                    //is in vision range. If it is, add this space to visible spaces.
                                    for (int q = 0; q <= u.getDominantFog().getVisionRadius(); q++)
                                    {
                                        currentEvaluatedRange = Game_Gnome.Range(Map, alliedUnits1[selectedUnit].getHost(), q);
                                        foreach (Space g in currentEvaluatedRange)
                                        {
                                            if (g == u/* && u.getDominantFog().getActive() == true*/)
                                            {
                                                visibleSpaces1.Add(u);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            /*for (int r = 0; r <= 2; r++)
                            {
                                foreach (Space w in Game_Gnome.Range(Map, alliedUnits1[selectedUnit].getHost(), r))
                                {
                                    visibleSpaces1.Add(w);
                                }
                            }*/
                        }
                    }
                    //if (fogOfWarEnabled == true)
                    {
                        foreach (Space w in fogOfWarSpaces)
                        {
                            w.setConcealed(true, Players[1].getPlayerID());
                        }
                        foreach (Space w in visibleSpaces1)
                        {
                            w.setConcealed(false, Players[1].getPlayerID());
                        }
                    }
                    alliedUnits2.RemoveRange(0, alliedUnits2.Count());
                    alliedUnits2.AddRange(Players[2].getAllUnitsFromUnitsOwned());
                    for (int p = 0; p <= 3; p++)
                    {
                        if (Players[p].getRelationshipToPlayer(2) == "ally")
                        {
                            alliedUnits2.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                        }
                    }
                    for (int selectedUnit = 0; selectedUnit < alliedUnits2.Count; selectedUnit++)
                    {
                        if (alliedUnits2[selectedUnit].checkAlive() == true)
                        {
                            foreach (Space u in Map.getAllSpaces())
                            {
                                //If there is no dominant fog over this space, ignore the rest of this instance of foreach
                                if (u.getDominantFog() == null)
                                {
                                    visibleSpaces2.Add(u);
                                }
                                else
                                {
                                    //Instanstiate a list of spaces that represent the current range of spaces.
                                    List<Space> currentEvaluatedRange = new List<Space>();
                                    //For each number from 0 to this space's vision radius, check to see if the selected unit
                                    //is in vision range. If it is, add this space to visible spaces.
                                    for (int q = 0; q <= u.getDominantFog().getVisionRadius(); q++)
                                    {
                                        currentEvaluatedRange = Game_Gnome.Range(Map, alliedUnits2[selectedUnit].getHost(), q);
                                        foreach (Space g in currentEvaluatedRange)
                                        {
                                            if (g == u/* && u.getDominantFog().getActive() == true*/)
                                            {
                                                visibleSpaces2.Add(u);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            /*for (int r = 0; r <= 2; r++)
                            {
                                foreach (Space w in Game_Gnome.Range(Map, alliedUnits2[selectedUnit].getHost(), r))
                                {
                                    visibleSpaces2.Add(w);
                                }
                            }*/
                        }
                    }
                    //if (fogOfWarEnabled == true)
                    {
                        foreach (Space w in fogOfWarSpaces)
                        {
                            w.setConcealed(true, Players[2].getPlayerID());
                        }
                        foreach (Space w in visibleSpaces2)
                        {
                            w.setConcealed(false, Players[2].getPlayerID());
                        }
                    }
                    alliedUnits3.RemoveRange(0, alliedUnits3.Count());
                    alliedUnits3.AddRange(Players[3].getAllUnitsFromUnitsOwned());
                    for (int p = 0; p <= 3; p++)
                    {
                        if (Players[p].getRelationshipToPlayer(3) == "ally")
                        {
                            alliedUnits3.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                        }
                    }
                    for (int selectedUnit = 0; selectedUnit < alliedUnits3.Count; selectedUnit++)
                    {
                        if (alliedUnits3[selectedUnit].checkAlive() == true)
                        {
                            foreach (Space u in Map.getAllSpaces())
                            {
                                //If there is no dominant fog over this space, ignore the rest of this instance of foreach
                                if (u.getDominantFog() == null)
                                {
                                    visibleSpaces3.Add(u);
                                }
                                else
                                {
                                    //Instanstiate a list of spaces that represent the current range of spaces.
                                    List<Space> currentEvaluatedRange = new List<Space>();
                                    //For each number from 0 to this space's vision radius, check to see if the selected unit
                                    //is in vision range. If it is, add this space to visible spaces.
                                    for (int q = 0; q <= u.getDominantFog().getVisionRadius(); q++)
                                    {
                                        currentEvaluatedRange = Game_Gnome.Range(Map, alliedUnits3[selectedUnit].getHost(), q);
                                        foreach (Space g in currentEvaluatedRange)
                                        {
                                            if (g == u /*&& u.getDominantFog().getActive() == true*/)
                                            {
                                                visibleSpaces3.Add(u);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            /*for (int r = 0; r <= 2; r++)
                            {
                                foreach (Space w in Game_Gnome.Range(Map, alliedUnits3[selectedUnit].getHost(), r))
                                {
                                    visibleSpaces3.Add(w);
                                }
                            }*/
                        }
                    }
                    //if (fogOfWarEnabled == true)
                    {
                        foreach (Space w in fogOfWarSpaces)
                        {
                            w.setConcealed(true, Players[3].getPlayerID());
                        }
                        foreach (Space w in visibleSpaces3)
                        {
                            w.setConcealed(false, Players[3].getPlayerID());
                        }
                    }
                    if (isPlayerTurn == true || AIOnlyBattle == true || setup == true)
                    {
                        if (setup == false)
                        {
                            currentControlledPlayer = currentUnit.getPlayer();
                        }
                        //alliedUnits = currentControlledPlayer.getAllUnitsFromUnitsOwned();
                        alliedUnits.RemoveRange(0, alliedUnits.Count());
                        alliedUnits.AddRange(currentControlledPlayer.getAllUnitsFromUnitsOwned());
                        for (int p = 0; p <= 3; p++)
                        {
                            if (Players[p].getRelationshipToPlayer(currentControlledPlayer.getPlayerID()) == "ally")
                            {
                                alliedUnits.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                            }
                        }
                        for (int selectedUnit = 0; selectedUnit < alliedUnits.Count; selectedUnit++)
                        {
                            if (alliedUnits[selectedUnit].checkAlive() == true)
                            {
                                foreach (Space u in Map.getAllSpaces())
                                {
                                    //If there is no dominant fog over this space, ignore the rest of this instance of foreach
                                    if (u.getDominantFog() == null)
                                    {
                                        visibleSpaces.Add(u);
                                    }
                                    else
                                    {
                                        //Instanstiate a list of spaces that represent the current range of spaces.
                                        List<Space> currentEvaluatedRange = new List<Space>();
                                        //For each number from 0 to this space's vision radius, check to see if the selected unit
                                        //is in vision range. If it is, add this space to visible spaces.
                                        for (int q = 0; q <= u.getDominantFog().getVisionRadius(); q++)
                                        {
                                            currentEvaluatedRange = Game_Gnome.Range(Map, alliedUnits[selectedUnit].getHost(), q);
                                            foreach (Space g in currentEvaluatedRange)
                                            {
                                                if (g == u/* && u.getDominantFog().getActive() == true*/)
                                                {
                                                    visibleSpaces.Add(u);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                /*for (int r = 0; r <= 2; r++)
                                {
                                    foreach (Space w in Game_Gnome.Range(Map, alliedUnits[selectedUnit].getHost(), r))
                                    {
                                        visibleSpaces.Add(w);
                                    }
                                }*/
                            }
                        }
                        //if (fogOfWarEnabled == true)
                        {
                            foreach (Space w in fogOfWarSpaces)
                            {
                                w.setConcealed(true, currentControlledPlayer.getPlayerID());
                            }
                            foreach (Space w in visibleSpaces)
                            {
                                w.setConcealed(false, currentControlledPlayer.getPlayerID());
                            }
                        }
                        setup = false;
                    }
                    if (currentUnit.checkAlive() == true)
                    {
                        for (int w = 0; w < Map.getSize(); w++)
                        {
                            Map.getSpace(w).setContainsCurrentUnit(false);
                            if (Map.getSpace(w).getGuest() == null)
                            {
                                //Map.getSpace(w).setContainsCurrentUnit(false);
                            }
                            else if (Map.getSpace(w).getGuest() == currentUnit)
                            {
                                Map.getSpace(w).setContainsCurrentUnit(true);
                            }
                        }
                        foreach (Space w in Map.getAllSpaces())
                        {
                            if (w.getContainsCurrentUnit() == true)
                            {
                                //Console.WriteLine(w.getX() + ", " + w.getY());
                            }
                        }
                        //If the space of interest is already visible with no unit therein, randomly select a new space of interest.
                        List<Space> visibleSoI = new List<Space>();
                        List<Space> concealedSoI = new List<Space>();
                        Random rand = new Random();
                        int RNGNumber;
                        foreach (Space w in Map.getAllSpaces())
                        {
                            if (w.getConcealed(currentUnit.getPlayer().getPlayerID()) == true)
                            {
                                concealedSoI.Add(w);
                            }
                            else
                            {
                                visibleSoI.Add(w);
                            }
                        }
                        foreach (Space w in visibleSoI)
                        {
                            if (w == currentUnit.getPlayer().getSpaceOfInterest())
                            {
                                RNGNumber = rand.Next(0, concealedSoI.Count() - 1);
                                currentUnit.getPlayer().setSpaceOfInterest(concealedSoI[RNGNumber]);
                            }
                        }
                        Map.printMap(currentControlledPlayer, currentUnit);
                    }
                    //Console.WriteLine(currentControlledPlayer.getPlayerName());
                    if (isPlayerTurn == true)
                    {
                        Console.WriteLine("Your health: " + currentUnit.getCurrentHP() + "/" + currentUnit.getModifiedHP());
                        if (currentUnit.getClass().doesHaveMP() == true)
                        {
                            Console.WriteLine("Your MP: " + currentUnit.getCurrentMP() + "/" + currentUnit.getModifiedMP());
                        }
                        Console.WriteLine("You have " + currentUnit.getMovementPointsToSpend() + " movement points.");
                        Console.WriteLine("You have " + currentUnit.getAttackPoints() + " attacks left.");
                        Console.WriteLine("Your x,y coordinates: " + currentUnit.getHost().getX() + ", " + currentUnit.getHost().getY());
                        foreach (Weapon w in currentUnit.getWeapons())
                        {
                            Console.WriteLine(w.getWeaponName());
                        }
                        Console.WriteLine("-----Decisions-----");
                        Console.WriteLine("Enter 1 to Move || Enter 2 to Attack || Enter 3 to Freelook || Enter 4 to End Turn || Enter 5 to Get Unit Stats: ");
                        //string input = Console.ReadLine();
                        ConsoleKeyInfo keyInput = Console.ReadKey(true);
                        //int input2 = Convert.ToInt32(input); //Obsolete
                        if (keyInput.Key==ConsoleKey.D1)
                        {
                            originalSpot = currentUnit.getHost().getSpaceNum();
                            if (/*currentUnit.getMovementPointsToSpend() > 0*//*<<Obsolete*/hasMoved == false)
                            {
                                int startingMovementPoints = currentUnit.getMovementPointsToSpend();
                                //Console.WriteLine("Movement points: " + startingMovementPoints);
                                Space startingSpace = currentUnit.getHost();
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setIsMoveHighlighted(false);
                                    w.setIsAttackHighlighted(false);
                                }
                                foreach (Space w in Game_Gnome.movementRange(Map, currentUnit.getPlayer(), startingMovementPoints, startingSpace))
                                {
                                    w.setIsMoveHighlighted(true);
                                    //Console.WriteLine("Highlighting space " + w.getID()); //Debug
                                }
                                foreach (Space w in Game_Gnome.threatenedRange(Map, currentUnit.getPlayer(), startingMovementPoints, startingSpace, currentUnit))
                                {
                                    w.setIsAttackHighlighted(true);
                                }
                                //Remember to set the analyzed spaces free from the clutches of movementRange!
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setAnalyzed(false);
                                }
                                while (true)
                                {
                                    Map.printMap(currentUnit.getPlayer(), currentUnit);
                                    Console.WriteLine("Which Direction would you like to move? Enter w for Up || Enter d for Right || Enter s for Down || Enter a for Left || Enter x to cancel Movement Action || Enter c to confirm Movement Action: ");
                                    keyInput = Console.ReadKey();
                                    //input2 = Convert.ToInt32(input); //Obsolete
                                    if (keyInput.Key == ConsoleKey.W && currentUnit.getMovementPointsToSpend() > 0)
                                    {
                                        Console.WriteLine("You Moved up.");
                                        /*Notice something I did in the line below (and in similar lines below): I replaced the
                                        "Test_Warrior" unit with the "currentUnit" variable. The reason why I did this is because
                                        currentUnit can represent whatever unit whose turn it is, as opposed to just the warrior.
                                        While it may be helpful to change for reference purposes, I didn't have to change the
                                        actual argument in the "Attack" and "Move" methods from "Test_Warrior", since it was
                                        only an argument that will take the value of whatever variable/value is inserted when
                                        the method is invoked. Here, move (and attack, by extension) now moves (or attacks) using
                                        the unit whose turn it currently is.*/
                                        //bool trapped = Game_Gnome.Move(currentUnit, Map, 0, fogOfWarEnabled);
                                        Game_Gnome.Move(currentUnit, Map, 0, fogOfWarEnabled);
                                        if (Game_Gnome.getUnitTrapped() == true)
                                        {
                                            foreach (Space w in Map.getAllSpaces())
                                            {
                                                w.setIsMoveHighlighted(false);
                                                w.setIsAttackHighlighted(false);
                                            }
                                            hasMoved = true;
                                            Game_Gnome.setUnitTrapped(false);
                                            break;
                                        }
                                        //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                        //Map.printMap(currentUnit.getPlayer(), currentUnit);
                                        //break; //Obsolete
                                    }
                                    else if (keyInput.Key == ConsoleKey.D && currentUnit.getMovementPointsToSpend() > 0)
                                    {
                                        Console.WriteLine("You Moved Right.");
                                        //bool trapped = Game_Gnome.Move(currentUnit, Map, 1, fogOfWarEnabled);
                                        Game_Gnome.Move(currentUnit, Map, 1, fogOfWarEnabled);
                                        if (Game_Gnome.getUnitTrapped() == true)
                                        {
                                            foreach (Space w in Map.getAllSpaces())
                                            {
                                                w.setIsMoveHighlighted(false);
                                                w.setIsAttackHighlighted(false);
                                            }
                                            hasMoved = true;
                                            Game_Gnome.setUnitTrapped(false);
                                            break;
                                        }
                                        //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                        //Map.printMap(currentUnit.getPlayer(), currentUnit);
                                        //break; //Obsolete
                                    }
                                    else if (keyInput.Key == ConsoleKey.S && currentUnit.getMovementPointsToSpend() > 0)
                                    {
                                        Console.WriteLine("You Moved Down.");
                                        //bool trapped = Game_Gnome.Move(currentUnit, Map, 2, fogOfWarEnabled);
                                        Game_Gnome.Move(currentUnit, Map, 2, fogOfWarEnabled);
                                        if (Game_Gnome.getUnitTrapped() == true)
                                        {
                                            foreach (Space w in Map.getAllSpaces())
                                            {
                                                w.setIsMoveHighlighted(false);
                                                w.setIsAttackHighlighted(false);
                                            }
                                            hasMoved = true;
                                            Game_Gnome.setUnitTrapped(false);
                                            break;
                                        }
                                        //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                        //Map.printMap(currentUnit.getPlayer(), currentUnit);
                                        //break; //Obsolete
                                    }
                                    else if (keyInput.Key == ConsoleKey.A && currentUnit.getMovementPointsToSpend() > 0)
                                    {
                                        Console.WriteLine("You Moved Left.");
                                        //bool trapped = Game_Gnome.Move(currentUnit, Map, 3, fogOfWarEnabled);
                                        Game_Gnome.Move(currentUnit, Map, 3, fogOfWarEnabled);
                                        if (Game_Gnome.getUnitTrapped() == true)
                                        {
                                            foreach (Space w in Map.getAllSpaces())
                                            {
                                                w.setIsMoveHighlighted(false);
                                                w.setIsAttackHighlighted(false);
                                            }
                                            hasMoved = true;
                                            Game_Gnome.setUnitTrapped(false);
                                            break;
                                        }
                                        //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend()-1); //Obsolete
                                        //Map.printMap(currentUnit.getPlayer(), currentUnit);
                                        //break; //Obsolete
                                    }
                                    else if ((keyInput.Key == ConsoleKey.W || keyInput.Key == ConsoleKey.A || keyInput.Key == ConsoleKey.S || keyInput.Key == ConsoleKey.D) && currentUnit.getMovementPointsToSpend() == 0)
                                    {
                                        Console.WriteLine("You don't have any more movement points to spend.");
                                    }
                                    else if (keyInput.Key == ConsoleKey.X)
                                    {
                                        //The movement action will cancel and the unit will return to their original spot.
                                        currentUnit.setMovementPointsToSpend(currentUnitMovementPointsToGrant);
                                        ReturnToOriginalSpot(currentUnit, Map, originalSpot);
                                        foreach (Space w in Map.getAllSpaces())
                                        {
                                            w.setIsMoveHighlighted(false);
                                            w.setIsAttackHighlighted(false);
                                        }
                                        break;
                                    }
                                    else if (keyInput.Key == ConsoleKey.C)
                                    {
                                        //The movement action will be completed and the movement points will have been spent.
                                        hasMoved = true;
                                        foreach (Space w in Map.getAllSpaces())
                                        {
                                            w.setIsMoveHighlighted(false);
                                            w.setIsAttackHighlighted(false);
                                        }
                                        break;
                                    }
                                    //Console.WriteLine("Invalid input.");
                                }
                            }
                            else if (/*currentUnit.getMovementPointsToSpend() == 0*/ hasMoved == true) { Console.WriteLine("You've already moved this turn!"); }
                        }
                        else if (keyInput.Key == ConsoleKey.D2)
                        {
                            if (currentUnit.getAttackPoints() > 0)
                            {
                                bool attacked = false;
                                //The attacked variable will be true whenever the unit lands an attack. It's used to break the while loop.
                                while (currentUnit.getAttackPoints() > 0)
                                {
                                    //Ask the player what weapons or abilities to use.
                                    Console.WriteLine("Choose from the following weapons or abilities to attack with. Type in the ability number. Type in -1 to cancel.");
                                    Console.Write("\n");
                                    //It goes through the player's list of abilities and displays them with a number ID.
                                    //Type the number ID to use the ability.
                                    //Make a list that will contain abilities derived from all sources.
                                    List<Ability> abilities = new List<Ability>();
                                    List<Weapon> weaponDurabilities = new List<Weapon>();
                                    //Add the weapon abilities to this list.
                                    foreach (Weapon r in currentUnit.getWeapons())
                                    {
                                        Ability weaponAbility = new Ability(r.getWeaponName(), r.getDamage(), r.getMaxRange(), r.getMinRange(), 0, r.getAbilityType(), true, r.doesWeaponDealDamage(), r.doesWeaponDealNegDamage(), r.getRadius());
                                        abilities.Add(weaponAbility);
                                        weaponDurabilities.Add(r);
                                    }
                                    //Add the abilities to the list derived from the unit's class.
                                    foreach (Ability r in currentUnit.getStyle().getAbilities())
                                    {
                                        List<Class> tempClassList = r.getClassList();
                                        List<int> tempClassLevelReq = r.getClassMinRequirementsList();
                                        for (int s = 0; s < tempClassList.Count(); s++)
                                        {
                                            if (currentUnit.getLevel() >= tempClassLevelReq[s] && currentUnit.getStyle() == tempClassList[s])
                                            {
                                                abilities.Add(r);
                                            }
                                        }
                                        //abilities.Add(r);
                                    }
                                    //Display all abilities from the list on the screen.
                                    for (int r = 0; r < abilities.Count(); r++)
                                    {
                                        Console.Write(r + " - " + abilities[r].getName() + ", ");
                                    }
                                    string input3 = Console.ReadLine();
                                    int input4 = 0;
                                    try
                                    {
                                        input4 = Convert.ToInt32(input3);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("That's not a valid ability input!");
                                        input4 = -2;
                                    }
                                    //This checks the unit's every ability to identify which ability, r, that the player has chosen.
                                    if (input4 == -1) { break; }
                                    for (int r = 0; r < abilities.Count(); r++)
                                    {
                                        if (input4 == r)
                                        {
                                            Weapon currentWeapon = null;
                                            bool isCurrentAbilityWeapon = false;
                                            if (abilities[r].abilityIsWeaponAbility() == true)
                                            {
                                                currentWeapon = weaponDurabilities[r];
                                                isCurrentAbilityWeapon = true;
                                            }
                                            if (abilities[r].getAbilityType() != 1 || currentUnit.getCurrentMP() >= abilities[r].getMPCost())
                                            {
                                                if (isCurrentAbilityWeapon == true)
                                                {
                                                    if (currentWeapon.getCurrentDurability() <= 0)
                                                    {
                                                        Console.WriteLine("This weapon is broken, and cannot be used for attacks.");
                                                    }
                                                    else if (currentWeapon.getCurrentDurability() > 0)
                                                    {
                                                        highlightAttackableSpaces(abilities, weaponDurabilities, r, Game_Gnome, Map, currentUnit, true, previousSpaceList, previousSpace, Players);
                                                    }
                                                }
                                                if (isCurrentAbilityWeapon == false)
                                                {
                                                    highlightAttackableSpaces(abilities, weaponDurabilities, r, Game_Gnome, Map, currentUnit, false, previousSpaceList, previousSpace, Players);
                                                }
                                                /*
                                                //Declare a list that we will contain all spaces to highlight.
                                                List<Space> highlightedSpaces = new List<Space>();
                                                //These spaces are below the minimum range and so are unhighlighted.
                                                List<Space> belowMinRangeSpaces = new List<Space>();
                                                Space[] highlightedSpacesArray = { }; //Obsolete
                                                //The for conditions below start from the minimum range of the selected ability and reach the maximum range
                                                //of the ability, adding all the spaces for each range in between (and including) the minRange and maxRange lower
                                                //and upper bounds.
                                                for (int k = abilities[r].getMinRange(); k <= abilities[r].getMaxRange(); k++)
                                                {
                                                    //Each space w from Game_Gnome.Range is going to be added to our highlightedSpaces list. The purpose of the
                                                    //Range method is to get all spaces from a certain range (defined as incremented "k" here) from the ability
                                                    //user. If it successfully returns spaces, they will be added to highlightedSpaces list.
                                                    //foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), k)) 
                                                    //{
                                                    //highlightedSpaces.Add(w);
                                                    How to debug: If the foreach loop above successfully added spaces to
                                                    highlightedSpaces, then it should be working correctly. If not, the
                                                    implication is that no spaces were added because the Game_Gnome.Range method
                                                    failed to return any spaces.
                                                    //Oh, and a message to Game_Gnome: You're FIRED!
                                                    //Console.WriteLine("Successful");//debug
                                                    //}
                                                    //foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), ))
                                                    //Game_Gnome.Range(Map, currentUnit.getHost(), 1);
                                                }
                                                //List<Space> gameGnome = Game_Gnome.Range(Map, currentUnit.getHost(), currentUnit.getStyle().getAbilities()[r].getMaxRange());
                                                List<Space> gameGnome = new List<Space>();
                                                Game_Gnome.Range(Map, currentUnit.getHost(), 2);
                                                Console.WriteLine("Ability: " + abilities[r].getName() + ", Max Range: " + abilities[r].getMaxRange());
                                                for (int h = abilities[r].getMinRange(); h<=abilities[r].getMaxRange(); h++ )
                                                    foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), h))
                                                    {
                                                        {
                                                            highlightedSpaces.Add(w);
                                                        }
                                                    }
                                                //gameGnome = Game_Gnome.Range(Map, currentUnit.getHost(), currentUnit.getStyle().getAbilities()[r].getMinRange() - 1);
                                                foreach (Space w in gameGnome)
                                                {
                                                    belowMinRangeSpaces.Add(w);
                                                } //Obsolete
                                                for (int w = 0; w<belowMinRangeSpaces.Count() ; w++)
                                                {
                                            
                                                }
                                                //Obsolete
                                                foreach (Space w in belowMinRangeSpaces)
                                                {
                                                    highlightedSpaces.Remove(w);
                                                } //Obsolete
                                                //One space in the highlighted spaces is designated as the selected space, or the 'target'.
                                                //It will be colored yellow instead of red.
                                                int selectedSpace = 0;
                                                //int previousSelectedSpace = 0; //Obsolete: See below at "Reasoning:"
                                                while (true)
                                                {
                                                    //This loop takes each space in the highlightedSpaces list and modifies
                                                    //the value of "setIsAttackHighlighted" and/or "setIsTargetHighlighted".
                                                    //These variables are used by Field.cs later and it should color them appropriately when the
                                                    //map is printed.
                                                    foreach (Space q in highlightedSpaces)
                                                    {
                                                        q.setIsAttackHighlighted(true); //Red
                                                        q.setIsTargetHighlighted(true); //Yellow
                                                        //The above two methods set EVERY space in the list to be the target. But we're going
                                                        //to fix that by insuring, below, that only ONE space is the target.
                                                        //Reasoning: If we just set one space to be the target, the next time we change the target
                                                        //later, we would have to make sure the previously selected space is no longer the target.
                                                        //My solution is the lazy-man's method of solving this problem. There should only ever
                                                        //be one target space, even if we designate a new target.
                                                        if (q != highlightedSpaces[selectedSpace]) { q.setIsTargetHighlighted(false); }
                                                    }
                                                    Map.printMap(currentUnit.getPlayer());
                                                    Console.WriteLine("Please select your target. The red spaces are possible targets, while yellow is your target. Press d to navigate right, press a to navigate left. Press x to cancel, and press c to confirm target.");
                                                    input3 = Console.ReadLine();
                                                    if (input3 == "d")
                                                    {
                                                        if (selectedSpace >= highlightedSpaces.Count() - 1)
                                                        {
                                                            //Wrap the space selection to the other side of the list.
                                                            selectedSpace = 0;
                                                        }
                                                        else { selectedSpace++; }
                                                    }
                                                    else if (input3 == "a")
                                                    {
                                                        if (selectedSpace <= 0)
                                                        {
                                                            //Wrap the space selection to the other side of the list.
                                                            selectedSpace = highlightedSpaces.Count() - 1;
                                                        }
                                                        else { selectedSpace--; }
                                                    }
                                                    else if (input3 == "x")
                                                    {
                                                        foreach (Space q in highlightedSpaces)
                                                        {
                                                            //We're exiting space target selection, so unhighlight every space.
                                                            q.setIsAttackHighlighted(false);
                                                            q.setIsTargetHighlighted(false);
                                                        }
                                                        break;
                                                    }
                                                    //Prevent the player from attacking their own units if they're not allowed to.
                                                    else if (input3 == "c" && currentUnit.getPlayer().getCanAttackOwnUnits() == false)
                                                    {
                                                        if (highlightedSpaces[selectedSpace].getGuest() == null)
                                                        {

                                                        }
                                                        else if (highlightedSpaces[selectedSpace].getGuest().getPlayer() == currentUnit.getPlayer())
                                                        {
                                                            Console.WriteLine("You can't attack your own units!");
                                                        }
                                                    } //Bugged
                                                    else if (input3 == "c")
                                                    {
                                                        //Confirm the attack, decrement the unit's attack points, and break the while loop
                                                        //so that the while loop above it recognizes attacked == true and ends its threads, returning
                                                        //to action selection.
                                                        //Currently, the attack itself hasn't been scripted in yet. But the major goal
                                                        //is solely to highlight the spaces we could attack, so it takes lower priority.
                                                        Game_Gnome.attack(currentUnit, Map, abilities[r], highlightedSpaces[selectedSpace], true);
                                                        foreach (Space q in highlightedSpaces)
                                                        {
                                                            //We're exiting space target selection, so unhighlight every space.
                                                            q.setIsAttackHighlighted(false);
                                                            q.setIsTargetHighlighted(false);
                                                        }
                                                        attacked = true;
                                                        break;
                                                    }
                                                }*/
                                            }
                                            else if (currentUnit.getCurrentMP() < abilities[r].getMPCost())
                                            {
                                                Console.WriteLine("You don't have enough magic power to cast this spell.");
                                            }
                                        }
                                    }

                                    /*while (true)
                                    {
                                        Console.WriteLine("Which Direction do you wish to strike? Enter w for Up || Enter d for Right || Enter s for Down || Enter a for Left || Enter x to cancel: ");
                                        input = Console.ReadLine();
                                        //input2 = Convert.ToInt32(input); //Obsolete
                                        if (input == "w")
                                        {
                                            List<Ability> list = currentUnit.getStyle().getAbilities();
                                            Console.WriteLine("You Slashed Up.");
                                            Game_Gnome.attack(currentUnit, Map, list, 0);
                                            currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                            break;
                                        }
                                        else if (input == "d")
                                        {
                                            List<Ability> list = currentUnit.getStyle().getAbilities();
                                            Console.WriteLine("You Slashed Right.");
                                            Game_Gnome.attack(currentUnit, Map, list, 1);
                                            currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                            break;
                                        }
                                        else if (input == "s")
                                        {
                                            List<Ability> list = currentUnit.getStyle().getAbilities();
                                            Console.WriteLine("You Slashed Down.");
                                            Game_Gnome.attack(currentUnit, Map, list, 2);
                                            currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                            break;
                                        }
                                        else if (input == "a")
                                        {
                                            List<Ability> list = currentUnit.getStyle().getAbilities();
                                            Console.WriteLine("You Slashed Left.");
                                            Game_Gnome.attack(currentUnit, Map, list, 3);
                                            currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                            break;
                                        }
                                        else if (input == "x")
                                        {
                                            break;
                                        }
                                        Console.WriteLine("Invalid input.");
                                    }*/
                                    //Obsolete
                                }
                            }
                            else if (currentUnit.getAttackPoints() == 0) { Console.WriteLine("You have run out of attacks for this turn."); }
                        }
                        else if (keyInput.Key == ConsoleKey.D3)
                        {
                            Space selectedSpace = currentUnit.getHost();
                            Space oldSelectedSpace = currentUnit.getHost();
                            while (true)
                            {
                                oldSelectedSpace = selectedSpace;
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setIsTargetHighlighted(false);
                                }
                                selectedSpace.setIsTargetHighlighted(true);
                                Map.printMap(currentUnit.getPlayer(), currentUnit);
                                if (selectedSpace.getGuest() == null || selectedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true)
                                {

                                }
                                else
                                {
                                    Console.WriteLine("Unit: " + selectedSpace.getGuest().getName());
                                    Console.WriteLine("Class: " + selectedSpace.getGuest().getClass().getClassName());
                                    Console.WriteLine("Level: " + selectedSpace.getGuest().getLevel());
                                    Console.WriteLine("HP: " + selectedSpace.getGuest().getCurrentHP());
                                    
                                }
                                if (selectedSpace.getExist() == false)
                                {
                                    Console.WriteLine("Inaccessible space");
                                }
                                else
                                {
                                    Console.WriteLine("Plains");
                                }
                                Console.WriteLine("You are in free-look mode. To navigate, press the w, a, s, and d keys to move north, west, south, and east, respectively. To view detailed unit stats, press c. To exit free-look, press x.");
                                keyInput = Console.ReadKey(true);
                                if (keyInput.Key == ConsoleKey.W)
                                {
                                    try { selectedSpace = selectedSpace.getNorth(); }
                                    catch { selectedSpace = selectedSpace; }
                                    if (selectedSpace == null)
                                    {
                                        selectedSpace = oldSelectedSpace;
                                    }
                                }
                                else if (keyInput.Key == ConsoleKey.S)
                                {
                                    try { selectedSpace = selectedSpace.getSouth(); }
                                    catch { selectedSpace = selectedSpace; }
                                    if (selectedSpace == null)
                                    {
                                        selectedSpace = oldSelectedSpace;
                                    }
                                }
                                else if (keyInput.Key == ConsoleKey.D)
                                {
                                    try { selectedSpace = selectedSpace.getEast(); }
                                    catch { selectedSpace = selectedSpace; }
                                    if (selectedSpace == null)
                                    {
                                        selectedSpace = oldSelectedSpace;
                                    }
                                }
                                else if (keyInput.Key == ConsoleKey.A)
                                {
                                    try { selectedSpace = selectedSpace.getWest(); }
                                    catch { selectedSpace = selectedSpace; }
                                    if (selectedSpace == null)
                                    {
                                        selectedSpace = oldSelectedSpace;
                                    }
                                }
                                else if (keyInput.Key == ConsoleKey.C)
                                {
                                    if (selectedSpace.getGuest() == null || selectedSpace.getConcealed(currentUnit.getPlayer().getPlayerID())==true)
                                    {
                                        Console.WriteLine("There doesn't appear to be a visible unit at this space.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("~" + selectedSpace.getGuest().getName() + "'s Stats~");
                                        Console.WriteLine("Base Stats");
                                        Console.WriteLine("Class: " + selectedSpace.getGuest().getClass().getClassName());
                                        Console.WriteLine("Level: " + selectedSpace.getGuest().getLevel());
                                        Console.WriteLine("Experience: " + selectedSpace.getGuest().getExperience() + "/" + selectedSpace.getGuest().getExperienceCap());
                                        Console.WriteLine("HP: " + selectedSpace.getGuest().getCurrentHP() + "/" + selectedSpace.getGuest().getModifiedHP());
                                        if (selectedSpace.getGuest().getClass().doesHaveMP() == true)
                                        {
                                            Console.WriteLine("MP: " + selectedSpace.getGuest().getCurrentMP() + "/" + selectedSpace.getGuest().getModifiedMP());
                                        }
                                        Console.WriteLine("Strength: " + selectedSpace.getGuest().getModifiedStrength());
                                        Console.WriteLine("Speed: " + selectedSpace.getGuest().getModifiedSpeed());
                                        Console.WriteLine("Constitution: " + selectedSpace.getGuest().getModifiedConstitution());
                                        Console.WriteLine("Ley Connexion: " + selectedSpace.getGuest().getModifiedLC());
                                        Console.WriteLine("Combat Stats");
                                        Console.WriteLine("Armor: " + selectedSpace.getGuest().getArmor());
                                        Console.WriteLine("Magic Defense: " + selectedSpace.getGuest().getMagicDefense());
                                        Console.WriteLine("Magic Save: " + selectedSpace.getGuest().getMagicSave());
                                        Console.WriteLine("Dodge: " + selectedSpace.getGuest().getDodge());
                                        Console.WriteLine("Aim: " + selectedSpace.getGuest().getAim());
                                        Console.WriteLine("MP Regeneration: " + selectedSpace.getGuest().getMPRegen());
                                    }
                                }
                                else if (keyInput.Key == ConsoleKey.X)
                                {
                                    foreach (Space w in Map.getAllSpaces())
                                    {
                                        w.setIsTargetHighlighted(false);
                                    }
                                    break;
                                }
                            }
                            
                        }
                        else if (keyInput.Key == ConsoleKey.D4)
                        {
                            i = i + 1;
                            pointsGranted = false;
                            if (i > currentTurnLength || i < 0) { i = 0; }
                        }
                        else if (keyInput.Key == ConsoleKey.D5)
                        {
                            Console.WriteLine("~" + currentUnit.getName() + "'s Stats~");
                            Console.WriteLine("Base Stats");
                            Console.WriteLine("Class: " + currentUnit.getClass().getClassName());
                            Console.WriteLine("Level: " + currentUnit.getLevel());
                            Console.WriteLine("Experience: " + currentUnit.getExperience() + "/" + currentUnit.getExperienceCap());
                            Console.WriteLine("HP: " + currentUnit.getCurrentHP() + "/" + currentUnit.getModifiedHP());
                            if (currentUnit.getClass().doesHaveMP() == true)
                            {
                                Console.WriteLine("MP: " + currentUnit.getCurrentMP() + "/" + currentUnit.getModifiedMP());
                            }
                            Console.WriteLine("Strength: " + currentUnit.getModifiedStrength());
                            Console.WriteLine("Speed: " + currentUnit.getModifiedSpeed());
                            Console.WriteLine("Constitution: " + currentUnit.getModifiedConstitution());
                            Console.WriteLine("Ley Connexion: " + currentUnit.getModifiedLC());
                            Console.WriteLine("Combat Stats");
                            Console.WriteLine("Armor: " + currentUnit.getArmor());
                            Console.WriteLine("Magic Defense: " + currentUnit.getMagicDefense());
                            Console.WriteLine("Magic Save: " + currentUnit.getMagicSave());
                            Console.WriteLine("Dodge: " + currentUnit.getDodge());
                            Console.WriteLine("Aim: " + currentUnit.getAim());
                            Console.WriteLine("MP Regeneration: " + currentUnit.getMPRegen());
                        }
                        else if (keyInput.Key == ConsoleKey.Enter)
                        {
                            while (true)
                            {
                                Console.WriteLine("Please enter a command. Type enter with no command to exit.");
                                string input = Console.ReadLine();
                                if (input == "9")
                                {
                                    if (cheatControlAI == false)
                                    {
                                        //While the "Control AIs" command is executed on the player's turn, all subsequent
                                        //AI unit turns will be controlled by the player until toggled off.
                                        Console.WriteLine("##DEBUG## You are now controlling AI teams. You cheater!");
                                        cheatControlAI = true;
                                    }
                                    else if (cheatControlAI == true)
                                    {
                                        Console.WriteLine("##DEBUG## You are no longer controlling AI teams.");
                                        cheatControlAI = false;
                                    }
                                }
                                else if (input == "3")
                                {
                                    Console.WriteLine("You begin to cry. Crying alot. OMG STFU BITCH!");
                                }
                                else if (input == "AIStand")
                                {
                                    AIBehavior = "stand";
                                    Console.WriteLine("##DEBUG## AI are now set to stand still.");
                                }
                                else if (input == "AIRandomMove")
                                {
                                    Console.WriteLine("##DEBUG## AI are now set to move around randomly.");
                                    AIBehavior = "randomMove";
                                }
                                else if (input == "AIAttack")
                                {
                                    Console.WriteLine("##DEBUG## AI are now set to attack enemies.");
                                    AIBehavior = "attack";
                                }
                                else if (input == "" || input == "exit" || input == "escape")
                                {
                                    break;
                                }
                                else { Console.WriteLine("That command was not recognized."); }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not a valid input. :(");
                        }
                    }
                    else if (isPlayerTurn == false)
                    {
                        /*string input = Console.ReadLine();
                        int input2 = Convert.ToInt32(input);
                        Console.WriteLine("It isn't your turn yet!");*/
                        /*NYI. The idea here was, if the player attempted
    to type a command to the command line while it's not their turn, it would say
    "It isn't your turn yet!". Console.ReadLine() waits for the user to give an input before processessing
    further instructions.*/
                        //Having no instructions (from artificial intelligence or an algorithm table), the AI ends their turn.
                        int r = 0;
                        bool AIActionsCompleted = false;
                        bool rabbitHasMoved = false;
                        //Console.WriteLine(AIBehavior); //debug
                        if (currentUnit == NULL_UNIT || currentUnit == NULL_UNIT2)
                        {
                            pointsGranted = false;
                            i = 0;
                        }
                        else if ((AIBehavior == "randomMove" || currentUnit.getClass().getClassName() == "Rabbit") && AIActionsCompleted == false)
                        {
                            //Attempt to move onto a random space. If the RNG keeps picking illegal spaces, the AI
                            //is hard-coded to give up and end their turn.
                            System.Threading.Thread.Sleep(1000);
                            if (rabbitHasMoved == false)
                            {
                                Random rand = new Random();
                                //r = rand.Next(0, 3);
                                //int ticks = 0; //Obsolete
                                //Make the system wait for 1 seconds before deciding its actions.
                                //System.Threading.Thread.Sleep(1000);
                                //List<Space> plottedMovements = new List<Space>();
                                List<int> plottedDirections = new List<int>();
                                List<Space> plottedMovements = new List<Space>();
                                Space currentUnitPosition = currentUnit.getHost();
                                while (true)
                                {
                                    r = rand.Next(0, 4);
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                    numberOfMovementAttempts++;
                                    if (currentUnit.getMovementPointsToSpend() <= 0 || numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() * 3)
                                    {
                                        //int ticks = 0; //Obsolete
                                        //Make the system wait for 1 seconds before deciding its actions.
                                        foreach (Space w in plottedMovements)
                                        {
                                            if (w != currentUnit.getHost())
                                            {
                                                Game_Gnome.moveToSpace(currentUnit, Map, w, fogOfWarEnabled, false);
                                                Map.printMap(currentControlledPlayer, currentUnit);
                                                Console.WriteLine("-----");
                                            }
                                            if (Game_Gnome.getUnitTrapped() == true)
                                            {
                                                Game_Gnome.setUnitTrapped(false);
                                                numberOfMovementAttempts = 0;
                                                rabbitHasMoved = true;
                                                pointsGranted = false;
                                                break;
                                            }
                                            System.Threading.Thread.Sleep(100);
                                        }
                                        numberOfMovementAttempts = 0;
                                        pointsGranted = false;
                                        rabbitHasMoved = true;
                                        i = i + 1;
                                        break;
                                    }
                                }
                                //Game_Gnome.Move(currentUnit, Map, r, fogOfWarEnabled);
                                //numberOfMovementAttempts++;
                                //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                /*if (currentUnit.getMovementPointsToSpend() <= 0 || numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() * 3)
                                {
                                    //int ticks = 0; //Obsolete
                                    //Make the system wait for 1 seconds before deciding its actions.
                                    System.Threading.Thread.Sleep(1000);
                                    i = i + 1;
                                    numberOfMovementAttempts = 0;
                                    pointsGranted = false;
                                    rabbitHasMoved = true;
                                }*/
                            }
                        }
                        else if (AIBehavior == "attack" && AIActionsCompleted == false)
                        {
                            //With the attack behavior, AI units would only be attacking units nearby and wouldn't run to pursue farther-off units.
                            System.Threading.Thread.Sleep(1000);
                            //Determine the best target within range of each ability.
                            while (currentUnit.getAttackPoints() > 0)
                            {
                                //Make a list that will contain abilities derived from all sources.
                                List<Ability> abilities = new List<Ability>();
                                List<Weapon> weaponDurabilities = new List<Weapon>();
                                //Add the weapon abilities to this list.
                                foreach (Weapon k in currentUnit.getWeapons())
                                {
                                    Ability weaponAbility = new Ability(k.getWeaponName(), k.getDamage(), k.getMaxRange(), k.getMinRange(), 0, k.getAbilityType(), true, k.doesWeaponDealDamage(), k.doesWeaponDealNegDamage(), k.getRadius());
                                    abilities.Add(weaponAbility);
                                    weaponDurabilities.Add(k);
                                }
                                foreach (Ability k in currentUnit.getStyle().getAbilities())
                                {
                                    List<Class> tempClassList = k.getClassList();
                                    List<int> tempClassLevelReq = k.getClassMinRequirementsList();
                                    for (int s = 0; s < tempClassList.Count(); s++)
                                    {
                                        if (currentUnit.getLevel() >= tempClassLevelReq[s] && currentUnit.getStyle() == tempClassList[s])
                                        {
                                            abilities.Add(k);
                                        }
                                    }
                                    //abilities.Add(r);
                                }
                                //In order to determine the best target for attacking, we're going to make four lists, and each item on each list is
                                //tied to an index (and therefore, each other). The first list stores a unit, the second list stores a weapon, a
                                //a third list stores the unit's health, a fourth list stores the damage that could be dealt. These lists will be used to
                                //calculate related lists with decision weights, with the highest decision weights being used to determine what will be attacked.
                                //For non-AoE abilities, a target unit is determined to attack.
                                Unit targetUnit;
                                Space targetSpace = null;
                                //The target ability is the ability that will end up being used in the attack.
                                Ability targetAbility = null;
                                List<Space> aoeSpaces = new List<Space>();
                                Space referredSpace = null;
                                List<Unit> hitList = new List<Unit>();
                                List<Ability> hitList_Abilities = new List<Ability>();
                                List<int> hitList_CurrentHealths = new List<int>();
                                List<int> hitList_DamagePossible = new List<int>();
                                List<double> hitList_DecisionWeights = new List<double>();
                                List<Ability> abilitiesToUse = new List<Ability>();
                                //Check the abilities available to the unit. Add those abilities that can actually be used.
                                //For weapons, these are abilities that have enough durability that they can be used.
                                //For spells, these are abilities that would not reduce the user's mana below 0.
                                for (int q = 0; q < abilities.Count(); q++)
                                {
                                    if (abilities[q].abilityIsWeaponAbility() == true)
                                    {
                                        if (weaponDurabilities[q].getCurrentDurability() > 0)
                                        {
                                            abilitiesToUse.Add(abilities[q]);
                                        }
                                    }
                                    else
                                    {
                                        if (currentUnit.getCurrentMP() - abilities[q].getMPCost() >= 0)
                                        {
                                            abilitiesToUse.Add(abilities[q]);
                                        }
                                    }
                                }
                                //If the unit has no abilities it can use, end its turn.
                                if (abilitiesToUse.Count() <= 0)
                                {
                                    //i = i + 1;
                                    //pointsGranted = false;
                                    break;
                                }
                                //Look at each ability and look for potential targets for attack.
                                for (int q = 0; q < abilitiesToUse.Count(); q++)
                                {
                                    List<Space> possibleTargets = new List<Space>();
                                    for (int h = abilitiesToUse[q].getMinRange(); h <= abilitiesToUse[q].getMaxRange(); h++)
                                    {
                                        possibleTargets.AddRange(Game_Gnome.Range(Map, currentUnit.getHost(), h));
                                    }
                                    for (int h = 0; h < possibleTargets.Count(); h++)
                                    {
                                        //Spaces with no unit are disqualified.
                                        if (possibleTargets[h].getGuest() == null)
                                        {

                                        }
                                        else
                                        {
                                            //Spaces with allied, neutral, or units of the same player are disqualified.
                                            if (possibleTargets[h].getGuest().getPlayer() == currentUnit.getPlayer() || currentUnit.getPlayer().getRelationshipToPlayer(possibleTargets[h].getGuest().getPlayer().getPlayerID()) == "ally" || currentUnit.getPlayer().getRelationshipToPlayer(possibleTargets[h].getGuest().getPlayer().getPlayerID()) == "neutral")
                                            {

                                            }
                                            else
                                            {
                                                //Spaces with units concealed by fog of war are disqualified.
                                                if (possibleTargets[h].getConcealed(currentUnit.getPlayer().getPlayerID()) == true)
                                                {

                                                }
                                                else
                                                {
                                                    //Spaces that meet all conditions are the possible targets.
                                                    hitList.Add(possibleTargets[h].getGuest());
                                                    //Console.WriteLine("Added " + possibleTargets[h].getGuest().getName() + " to the hit list!");
                                                    hitList_Abilities.Add(abilitiesToUse[q]);
                                                    hitList_CurrentHealths.Add(possibleTargets[h].getGuest().getCurrentHP());
                                                    //As you know, I do not yet have a function for calculating damage, which is calculated
                                                    //in a special way over at Game Controller.cs's attack method.
                                                    int listDmg = abilitiesToUse[q].getDmg();
                                                    int dmgType = 0;
                                                    if (abilitiesToUse[q].getAbilityType() == 0 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == false)
                                                    {
                                                        listDmg = listDmg + Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedStrength()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 0 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == true)
                                                    {
                                                        listDmg = -listDmg - Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedStrength()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 0 && abilitiesToUse[q].doesAbilityDealDamage() == false)
                                                    {
                                                        listDmg = 0;
                                                    }
                                                    if (abilitiesToUse[q].getAbilityType() == 1 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == false)
                                                    {
                                                        listDmg = listDmg + Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedLC()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 1 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == true)
                                                    {
                                                        listDmg = -listDmg - Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedLC()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 1 && abilitiesToUse[q].doesAbilityDealDamage() == false)
                                                    {
                                                        listDmg = 0;
                                                    }
                                                    hitList_DamagePossible.Add(listDmg);
                                                    hitList_DecisionWeights.Add(0);
                                                }
                                            }
                                        }
                                    }
                                }
                                //Now that we've listed all possible attacks we can make, let's give each possible decision a weight.
                                //Count through all of the possible decisions and check circumstances beneficial to the AI.
                                List<double> decisionsByHighestWeight = new List<double>();
                                List<Ability> decisionsByHighestWeight_Abilities = new List<Ability>();
                                List<Unit> decisionsByHighestWeight_HitList = new List<Unit>();
                                for (int q = 0; q < hitList.Count(); q++)
                                {
                                    double currentDecisionWeight = hitList_DecisionWeights[q]; //Starts at "0"
                                    //If there are no other good decisions, the AI at least wants to deal as much damage as possible.
                                    double weightOfDamageThatCouldBeDealt = hitList_DamagePossible[q];
                                    //The AI ALWAYS prefers to kill possible targets than leave them alive. Yeah, they're dicks.
                                    double weightOfPotentialToKill = 0;
                                    if (hitList_CurrentHealths[q] - hitList_DamagePossible[q] <= 0)
                                    {
                                        weightOfPotentialToKill = 100;
                                    }
                                    //The AI prefers to attack weaker targets in general. Weaker targets
                                    double weightOfTargetLevel = 1/((double)hitList[q].getLevel())*100;
                                    weightOfTargetLevel = Math.Ceiling(weightOfTargetLevel);
                                    //This weight is unused for now, but in any case, the AI would want to attack units perceived to be
                                    //more useful than others. Healers are preferred to non-healers.
                                    double weightOfTargetUsefulnessByClass = 0;
                                    //This weight is unused for now, but in any case, the AI will prefer to attack targets that have its aggro
                                    double weightOfAggro = 0;
                                    //This weight is unused for now. This is a weight for arbitrary reasons why the AI would prefer to attack
                                    //particular units for others. Units perceived to be the hero, for example, will incur the hatred
                                    //of their enemies, causing the AI to reflect this hatred in-game by choosing to attack them over others.
                                    //This metric is also useful for AIs that are predators of other units, so that they might prefer to attack
                                    //their prey.
                                    double weightOfHatred = 0;
                                    currentDecisionWeight = weightOfDamageThatCouldBeDealt+weightOfPotentialToKill+weightOfTargetLevel+weightOfTargetUsefulnessByClass+weightOfAggro+weightOfHatred;
                                    hitList_DecisionWeights[q] = currentDecisionWeight;
                                    //Console.WriteLine("Decision weight for decision " + q + " = " + hitList_DecisionWeights[q]);
                                }
                                //Re-sort all the possible decisions by highest weight.
                                for (int w = 999; w >= 0; w--)
                                {
                                    for (int q = 0; q < hitList_DecisionWeights.Count(); q++)
                                    {
                                        if (hitList_DecisionWeights[q] == w)
                                        {
                                            decisionsByHighestWeight.Add(hitList_DecisionWeights[q]);
                                            decisionsByHighestWeight_Abilities.Add(hitList_Abilities[q]);
                                            decisionsByHighestWeight_HitList.Add(hitList[q]);
                                        }
                                    }
                                }
                                //For now, our targets will be the decision with the highest weight (or the decision that got lucky enough to be first :P).
                                //If it turns out there are no decisions, end the turn.
                                //Console.WriteLine("The number of heighest weight decisions is " + decisionsByHighestWeight.Count());
                                if (decisionsByHighestWeight.Count <= 0)
                                {
                                    break;
                                }
                                else
                                {
                                    targetAbility = decisionsByHighestWeight_Abilities[0];
                                    targetUnit = decisionsByHighestWeight_HitList[0];
                                    targetSpace = targetUnit.getHost();
                                    for (int h = 0; h <= targetAbility.getRadius(); h++)
                                        foreach (Space w in Game_Gnome.Range(Map, targetSpace, h))
                                        {
                                            {
                                                aoeSpaces.Add(w);
                                            }
                                        }
                                }
                                foreach (Space q in aoeSpaces)
                                {
                                    q.setIsTargetHighlighted(true);
                                }
                                Map.printMap(currentControlledPlayer, currentUnit);
                                System.Threading.Thread.Sleep(750);
                                foreach (Space q in aoeSpaces)
                                {
                                    q.setIsTargetHighlighted(false);
                                }
                                Game_Gnome.attack(currentUnit, Map, targetAbility, targetSpace, aoeSpaces, true, referredSpace);
                            }
                            i = i + 1;
                            pointsGranted = false;
                        }
                        else if (AIBehavior == "attack+RandomMove" && AIActionsCompleted == false)
                        {
                            bobTheAI.battleAI_Evaluate(currentUnit, currentControlledPlayer, pointsGranted, alliedUnits0, alliedUnits1, alliedUnits2, alliedUnits3, visibleSpaces0, visibleSpaces1, visibleSpaces2, visibleSpaces3, Players, previousSpace, previousSpaceList);
                            /*
                            //Attempt to move onto a random space. If the RNG keeps picking illegal spaces, the AI
                            //is hard-coded to give up and end their turn.
                            System.Threading.Thread.Sleep(1000);
                            if (rabbitHasMoved == false)
                            {
                                Random rand = new Random();
                                //r = rand.Next(0, 3);
                                //int ticks = 0; //Obsolete
                                //Make the system wait for 1 seconds before deciding its actions.
                                //System.Threading.Thread.Sleep(1000);
                                //List<Space> plottedMovements = new List<Space>();
                                List<int> plottedDirections = new List<int>();
                                List<Space> plottedMovements = new List<Space>();
                                Space currentUnitPosition = currentUnit.getHost();
                                while (true)
                                {
                                    r = rand.Next(0, 4);
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                    numberOfMovementAttempts++;
                                    if (currentUnit.getMovementPointsToSpend() <= 0 || numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() * 3)
                                    {
                                        //int ticks = 0; //Obsolete
                                        //Make the system wait for 1 seconds before deciding its actions.
                                        foreach (Space w in plottedMovements)
                                        {
                                            if (w != currentUnit.getHost())
                                            {
                                                Game_Gnome.moveToSpace(currentUnit, Map, w, fogOfWarEnabled, false);
                                                Map.printMap(currentControlledPlayer, currentUnit);
                                                Console.WriteLine("-----");
                                            }
                                            if (Game_Gnome.getUnitTrapped() == true)
                                            {
                                                Game_Gnome.setUnitTrapped(false);
                                                numberOfMovementAttempts = 0;
                                                rabbitHasMoved = true;
                                                pointsGranted = false;
                                                break;
                                            }
                                            System.Threading.Thread.Sleep(100);
                                        }
                                        numberOfMovementAttempts = 0;
                                        pointsGranted = false;
                                        rabbitHasMoved = true;
                                        //i = i + 1;
                                        break;
                                    }
                                }

                                //Game_Gnome.Move(currentUnit, Map, r, fogOfWarEnabled);
                                //numberOfMovementAttempts++;
                                //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                if (currentUnit.getMovementPointsToSpend() <= 0 || numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() * 3)
                                {
                                    //int ticks = 0; //Obsolete
                                    //Make the system wait for 1 seconds before deciding its actions.
                                    System.Threading.Thread.Sleep(1000);
                                    i = i + 1;
                                    numberOfMovementAttempts = 0;
                                    pointsGranted = false;
                                    rabbitHasMoved = true;
                                }
                            }
                            alliedUnits0.RemoveRange(0, alliedUnits0.Count());
                            alliedUnits0.AddRange(Players[0].getAllUnitsFromUnitsOwned());
                            for (int p = 0; p <= 3; p++)
                            {
                                if (Players[p].getRelationshipToPlayer(0) == "ally")
                                {
                                    alliedUnits0.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                                }
                            }
                            for (int selectedUnit = 0; selectedUnit < alliedUnits0.Count; selectedUnit++)
                            {
                                if (alliedUnits0[selectedUnit].checkAlive() == true)
                                {
                                    for (int rw = 0; rw <= 2; rw++)
                                    {
                                        foreach (Space w in Game_Gnome.Range(Map, alliedUnits0[selectedUnit].getHost(), rw))
                                        {
                                            visibleSpaces0.Add(w);
                                        }
                                    }
                                }
                            }
                            if (fogOfWarEnabled == true)
                            {
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setConcealed(true, Players[0].getPlayerID());
                                }
                                foreach (Space w in visibleSpaces0)
                                {
                                    w.setConcealed(false, Players[0].getPlayerID());
                                }
                            }
                            alliedUnits1.RemoveRange(0, alliedUnits1.Count());
                            alliedUnits1.AddRange(Players[1].getAllUnitsFromUnitsOwned());
                            for (int p = 0; p <= 3; p++)
                            {
                                if (Players[p].getRelationshipToPlayer(1) == "ally")
                                {
                                    alliedUnits1.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                                }
                            }
                            for (int selectedUnit = 0; selectedUnit < alliedUnits1.Count; selectedUnit++)
                            {
                                if (alliedUnits1[selectedUnit].checkAlive() == true)
                                {
                                    for (int rw = 0; rw <= 2; rw++)
                                    {
                                        foreach (Space w in Game_Gnome.Range(Map, alliedUnits1[selectedUnit].getHost(), rw))
                                        {
                                            visibleSpaces1.Add(w);
                                        }
                                    }
                                }
                            }
                            if (fogOfWarEnabled == true)
                            {
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setConcealed(true, Players[1].getPlayerID());
                                }
                                foreach (Space w in visibleSpaces1)
                                {
                                    w.setConcealed(false, Players[1].getPlayerID());
                                }
                            }
                            alliedUnits2.RemoveRange(0, alliedUnits2.Count());
                            alliedUnits2.AddRange(Players[2].getAllUnitsFromUnitsOwned());
                            for (int p = 0; p <= 3; p++)
                            {
                                if (Players[p].getRelationshipToPlayer(2) == "ally")
                                {
                                    alliedUnits2.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                                }
                            }
                            for (int selectedUnit = 0; selectedUnit < alliedUnits2.Count; selectedUnit++)
                            {
                                if (alliedUnits2[selectedUnit].checkAlive() == true)
                                {
                                    for (int rw = 0; rw <= 2; rw++)
                                    {
                                        foreach (Space w in Game_Gnome.Range(Map, alliedUnits2[selectedUnit].getHost(), rw))
                                        {
                                            visibleSpaces2.Add(w);
                                        }
                                    }
                                }
                            }
                            if (fogOfWarEnabled == true)
                            {
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setConcealed(true, Players[2].getPlayerID());
                                }
                                foreach (Space w in visibleSpaces2)
                                {
                                    w.setConcealed(false, Players[2].getPlayerID());
                                }
                            }
                            alliedUnits3.RemoveRange(0, alliedUnits3.Count());
                            alliedUnits3.AddRange(Players[3].getAllUnitsFromUnitsOwned());
                            for (int p = 0; p <= 3; p++)
                            {
                                if (Players[p].getRelationshipToPlayer(3) == "ally")
                                {
                                    alliedUnits3.AddRange(Players[p].getAllUnitsFromUnitsOwned());
                                }
                            }
                            for (int selectedUnit = 0; selectedUnit < alliedUnits3.Count; selectedUnit++)
                            {
                                if (alliedUnits3[selectedUnit].checkAlive() == true)
                                {
                                    for (int rw = 0; rw <= 2; rw++)
                                    {
                                        foreach (Space w in Game_Gnome.Range(Map, alliedUnits3[selectedUnit].getHost(), rw))
                                        {
                                            visibleSpaces3.Add(w);
                                        }
                                    }
                                }
                            }
                            if (fogOfWarEnabled == true)
                            {
                                foreach (Space w in Map.getAllSpaces())
                                {
                                    w.setConcealed(true, Players[3].getPlayerID());
                                }
                                foreach (Space w in visibleSpaces3)
                                {
                                    w.setConcealed(false, Players[3].getPlayerID());
                                }
                            }
                            //With the attack behavior, AI units would only be attacking units nearby and wouldn't run to pursue farther-off units.
                            //System.Threading.Thread.Sleep(1000);
                            //Determine the best target within range of each ability.
                            while (currentUnit.getAttackPoints() > 0)
                            {
                                //Make a list that will contain abilities derived from all sources.
                                List<Ability> abilities = new List<Ability>();
                                List<Weapon> weaponDurabilities = new List<Weapon>();
                                //Add the weapon abilities to this list.
                                foreach (Weapon k in currentUnit.getWeapons())
                                {
                                    Ability weaponAbility = new Ability(k.getWeaponName(), k.getDamage(), k.getMaxRange(), k.getMinRange(), 0, k.getAbilityType(), true, k.doesWeaponDealDamage(), k.doesWeaponDealNegDamage(), k.getRadius());
                                    abilities.Add(weaponAbility);
                                    weaponDurabilities.Add(k);
                                }
                                foreach (Ability k in currentUnit.getStyle().getAbilities())
                                {
                                    List<Class> tempClassList = k.getClassList();
                                    List<int> tempClassLevelReq = k.getClassMinRequirementsList();
                                    for (int s = 0; s < tempClassList.Count(); s++)
                                    {
                                        if (currentUnit.getLevel() >= tempClassLevelReq[s] && currentUnit.getStyle() == tempClassList[s])
                                        {
                                            abilities.Add(k);
                                        }
                                    }
                                    //abilities.Add(r);
                                }
                                //In order to determine the best target for attacking, we're going to make four lists, and each item on each list is
                                //tied to an index (and therefore, each other). The first list stores a unit, the second list stores a weapon, a
                                //a third list stores the unit's health, a fourth list stores the damage that could be dealt. These lists will be used to
                                //calculate related lists with decision weights, with the highest decision weights being used to determine what will be attacked.
                                //For non-AoE abilities, a target unit is determined to attack.
                                Unit targetUnit;
                                Space targetSpace = null;
                                //The target ability is the ability that will end up being used in the attack.
                                Ability targetAbility = null;
                                List<Space> aoeSpaces = new List<Space>();
                                Space referredSpace = null;
                                List<Unit> hitList = new List<Unit>();
                                List<Ability> hitList_Abilities = new List<Ability>();
                                List<int> hitList_CurrentHealths = new List<int>();
                                List<int> hitList_DamagePossible = new List<int>();
                                List<double> hitList_DecisionWeights = new List<double>();
                                List<Ability> abilitiesToUse = new List<Ability>();
                                //Check the abilities available to the unit. Add those abilities that can actually be used.
                                //For weapons, these are abilities that have enough durability that they can be used.
                                //For spells, these are abilities that would not reduce the user's mana below 0.
                                for (int q = 0; q < abilities.Count(); q++)
                                {
                                    if (abilities[q].abilityIsWeaponAbility() == true)
                                    {
                                        if (weaponDurabilities[q].getCurrentDurability() > 0)
                                        {
                                            abilitiesToUse.Add(abilities[q]);
                                        }
                                    }
                                    else
                                    {
                                        if (currentUnit.getCurrentMP() - abilities[q].getMPCost() >= 0)
                                        {
                                            abilitiesToUse.Add(abilities[q]);
                                        }
                                    }
                                }
                                //If the unit has no abilities it can use, end its turn.
                                if (abilitiesToUse.Count() <= 0)
                                {
                                    //i = i + 1;
                                    //pointsGranted = false;
                                    break;
                                }
                                //Look at each ability and look for potential targets for attack.
                                for (int q = 0; q < abilitiesToUse.Count(); q++)
                                {
                                    List<Space> possibleTargets = new List<Space>();
                                    for (int h = abilitiesToUse[q].getMinRange(); h <= abilitiesToUse[q].getMaxRange(); h++)
                                    {
                                        possibleTargets.AddRange(Game_Gnome.Range(Map, currentUnit.getHost(), h));
                                    }
                                    for (int h = 0; h < possibleTargets.Count(); h++)
                                    {
                                        //Spaces with no unit are disqualified.
                                        if (possibleTargets[h].getGuest() == null)
                                        {

                                        }
                                        else
                                        {
                                            //Spaces with allied, neutral, or units of the same player are disqualified.
                                            if (possibleTargets[h].getGuest().getPlayer() == currentUnit.getPlayer() || currentUnit.getPlayer().getRelationshipToPlayer(possibleTargets[h].getGuest().getPlayer().getPlayerID()) == "ally" || currentUnit.getPlayer().getRelationshipToPlayer(possibleTargets[h].getGuest().getPlayer().getPlayerID()) == "neutral")
                                            {

                                            }
                                            else
                                            {
                                                //Spaces with units concealed by fog of war are disqualified.
                                                if (possibleTargets[h].getConcealed(currentUnit.getPlayer().getPlayerID()) == true)
                                                {

                                                }
                                                else
                                                {
                                                    //Spaces that meet all conditions are the possible targets.
                                                    hitList.Add(possibleTargets[h].getGuest());
                                                    //Console.WriteLine("Added " + possibleTargets[h].getGuest().getName() + " to the hit list!");
                                                    hitList_Abilities.Add(abilitiesToUse[q]);
                                                    hitList_CurrentHealths.Add(possibleTargets[h].getGuest().getCurrentHP());
                                                    //As you know, I do not yet have a function for calculating damage, which is calculated
                                                    //in a special way over at Game Controller.cs's attack method.
                                                    int listDmg = abilitiesToUse[q].getDmg();
                                                    int dmgType = 0;
                                                    if (abilitiesToUse[q].getAbilityType() == 0 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == false)
                                                    {
                                                        listDmg = listDmg + Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedStrength()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 0 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == true)
                                                    {
                                                        listDmg = -listDmg - Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedStrength()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 0 && abilitiesToUse[q].doesAbilityDealDamage() == false)
                                                    {
                                                        listDmg = 0;
                                                    }
                                                    if (abilitiesToUse[q].getAbilityType() == 1 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == false)
                                                    {
                                                        listDmg = listDmg + Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedLC()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 1 && abilitiesToUse[q].doesAbilityDealDamage() == true && abilitiesToUse[q].doesAbilityDoNegDamage() == true)
                                                    {
                                                        listDmg = -listDmg - Convert.ToInt32(Math.Floor(Convert.ToDouble((currentUnit.getModifiedLC()) / 2)));
                                                    }
                                                    else if (abilitiesToUse[q].getAbilityType() == 1 && abilitiesToUse[q].doesAbilityDealDamage() == false)
                                                    {
                                                        listDmg = 0;
                                                    }
                                                    hitList_DamagePossible.Add(listDmg);
                                                    hitList_DecisionWeights.Add(0);
                                                }
                                            }
                                        }
                                    }
                                }
                                //Now that we've listed all possible attacks we can make, let's give each possible decision a weight.
                                //Count through all of the possible decisions and check circumstances beneficial to the AI.
                                List<double> decisionsByHighestWeight = new List<double>();
                                List<Ability> decisionsByHighestWeight_Abilities = new List<Ability>();
                                List<Unit> decisionsByHighestWeight_HitList = new List<Unit>();
                                for (int q = 0; q < hitList.Count(); q++)
                                {
                                    double currentDecisionWeight = hitList_DecisionWeights[q]; //Starts at "0"
                                    //If there are no other good decisions, the AI at least wants to deal as much damage as possible.
                                    double weightOfDamageThatCouldBeDealt = hitList_DamagePossible[q];
                                    //The AI ALWAYS prefers to kill possible targets than leave them alive. Yeah, they're dicks.
                                    double weightOfPotentialToKill = 0;
                                    if (hitList_CurrentHealths[q] - hitList_DamagePossible[q] <= 0)
                                    {
                                        weightOfPotentialToKill = 100;
                                    }
                                    //The AI prefers to attack weaker targets in general. Weaker targets
                                    double weightOfTargetLevel = 1 / ((double)hitList[q].getLevel()) * 100;
                                    weightOfTargetLevel = Math.Ceiling(weightOfTargetLevel);
                                    //This weight is unused for now, but in any case, the AI would want to attack units perceived to be
                                    //more useful than others. Healers are preferred to non-healers.
                                    double weightOfTargetUsefulnessByClass = 0;
                                    //This weight is unused for now, but in any case, the AI will prefer to attack targets that have its aggro
                                    double weightOfAggro = 0;
                                    //This weight is unused for now. This is a weight for arbitrary reasons why the AI would prefer to attack
                                    //particular units for others. Units perceived to be the hero, for example, will incur the hatred
                                    //of their enemies, causing the AI to reflect this hatred in-game by choosing to attack them over others.
                                    //This metric is also useful for AIs that are predators of other units, so that they might prefer to attack
                                    //their prey.
                                    double weightOfHatred = 0;
                                    currentDecisionWeight = weightOfDamageThatCouldBeDealt + weightOfPotentialToKill + weightOfTargetLevel + weightOfTargetUsefulnessByClass + weightOfAggro + weightOfHatred;
                                    hitList_DecisionWeights[q] = currentDecisionWeight;
                                    //Console.WriteLine("Decision weight for decision " + q + " = " + hitList_DecisionWeights[q]);
                                }
                                //Re-sort all the possible decisions by highest weight.
                                for (int w = 999; w >= 0; w--)
                                {
                                    for (int q = 0; q < hitList_DecisionWeights.Count(); q++)
                                    {
                                        if (hitList_DecisionWeights[q] == w)
                                        {
                                            decisionsByHighestWeight.Add(hitList_DecisionWeights[q]);
                                            decisionsByHighestWeight_Abilities.Add(hitList_Abilities[q]);
                                            decisionsByHighestWeight_HitList.Add(hitList[q]);
                                        }
                                    }
                                }
                                //For now, our targets will be the decision with the highest weight (or the decision that got lucky enough to be first :P).
                                //If it turns out there are no decisions, end the turn.
                                //Console.WriteLine("The number of heighest weight decisions is " + decisionsByHighestWeight.Count());
                                if (decisionsByHighestWeight.Count <= 0)
                                {
                                    break;
                                }
                                else
                                {
                                    targetAbility = decisionsByHighestWeight_Abilities[0];
                                    targetUnit = decisionsByHighestWeight_HitList[0];
                                    targetSpace = targetUnit.getHost();
                                    for (int h = 0; h <= targetAbility.getRadius(); h++)
                                        foreach (Space w in Game_Gnome.Range(Map, targetSpace, h))
                                        {
                                            {
                                                aoeSpaces.Add(w);
                                            }
                                        }
                                }
                                foreach (Space q in aoeSpaces)
                                {
                                    q.setIsTargetHighlighted(true);
                                }
                                Map.printMap(currentControlledPlayer, currentUnit);
                                System.Threading.Thread.Sleep(750);
                                foreach (Space q in aoeSpaces)
                                {
                                    q.setIsTargetHighlighted(false);
                                }
                                Game_Gnome.attack(currentUnit, Map, targetAbility, targetSpace, aoeSpaces, true, referredSpace);
                            }*/
                            i = i + 1;
                            pointsGranted = false;
                        }
                        else if (rabbitHasMoved == true) { AIActionsCompleted = true; }
                        if (/*AIActionsCompleted == true || */(AIBehavior == "stand" || AIBehavior == "" || AIBehavior == "string") && currentUnit.getClass().getClassName() != "Rabbit")
                        {
                            //int ticks=0; //Obsolete
                            //Make the system wait for 1 seconds before deciding its actions.
                            System.Threading.Thread.Sleep(500);
                            //AIActionsCompleted = false;
                            //numberOfMovementAttempts = 0;
                            i = i + 1;
                            //rabbitHasMoved = false;
                            pointsGranted = false;
                        }
                        if (i > currentTurnLength || i < 0) { i = 0; }
                    }
                    //This loop checks each unit in the current battle to see if they're living or not. If the unit
                    //isn't living, it will mark them off as dead and send an appropriate message to the console.
                    for (int m = 0; m < currentTurnLength; m++)
                    {
                        if (livingUnits[m] == true)
                        {
                            if (currentUnitsTurn[m].checkAlive() == false)
                            {
                                Console.WriteLine(currentUnitsTurn[m].getName() + " died!");
                                //The dead body is removed from the map.
                                KillUnit(currentUnitsTurn[m], Map);
                                currentUnitsTurn[m].getPlayer().setLivingUnitsInBattle(currentUnitsTurn[m].getPlayer().getLivingUnitsInBattle() - 1);
                                livingUnits[m] = false;
                            }
                        }
                    }
                    //if (currentUnit.checkAlive() == true) { Map.printMap(currentControlledPlayer); }
                    if (Players[0].getLivingUnitsInBattle() == 0 && playersDefeated[0] == false)
                    {
                        returnPlayerColor(Players[0]);
                        Console.Write(Players[0].getPlayerName());
                        Console.ResetColor();
                        Console.Write(" has run out of units, and is defeated!" + "\n");
                        numberOfPlayersDefeated++;
                        if (Players[0].isAI() == false)
                        {
                            numberOfHumanPlayers--;
                        }
                        playersDefeated[0] = true;
                    }
                    if (Players[1].getLivingUnitsInBattle() == 0 && playersDefeated[1] == false)
                    {
                        returnPlayerColor(Players[1]);
                        Console.Write(Players[1].getPlayerName());
                        Console.ResetColor();
                        Console.Write(" has run out of units, and is defeated!" + "\n");
                        numberOfPlayersDefeated++;
                        if (Players[1].isAI() == false)
                        {
                            numberOfHumanPlayers--;
                        }
                        playersDefeated[1] = true;
                    }
                    if (Players[2].getLivingUnitsInBattle() == 0 && playersDefeated[2] == false)
                    {
                        returnPlayerColor(Players[2]);
                        Console.Write(Players[2].getPlayerName());
                        Console.ResetColor();
                        Console.Write(" has run out of units, and is defeated!" + "\n");
                        numberOfPlayersDefeated++;
                        if (Players[2].isAI() == false)
                        {
                            numberOfHumanPlayers--;
                        }
                        playersDefeated[2] = true;
                    }
                    if (Players[3].getLivingUnitsInBattle() == 0 && playersDefeated[3] == false)
                    {
                        returnPlayerColor(Players[3]);
                        Console.Write(Players[3].getPlayerName());
                        Console.ResetColor();
                        Console.Write(" has run out of units, and is defeated!" + "\n");
                        numberOfPlayersDefeated++;
                        if (Players[3].isAI() == false)
                        {
                            numberOfHumanPlayers--;
                        }
                        playersDefeated[3] = true;
                    }
                    if (numberOfHumanPlayers == 0)
                    {
                        AIOnlyBattle = true;
                    }
                    if (numberOfPlayersDefeated == 3)
                    {
                        returnPlayerColor(currentUnitsTurn[i].getPlayer());
                        Console.Write(currentUnitsTurn[i].getPlayer().getPlayerName());
                        Console.ResetColor();
                        Console.Write(" is victorious!" + "\n");
                        break;
                    }
                    /*if (Test_Warrior.checkAlive() == false)
                    {
                        Console.WriteLine("Your last unit died!");
                        Console.WriteLine("Game over, man! GAME OVER!");
                        break;
                    }
                    if (Test_Skeleton.checkAlive() == false)
                    {
                        Console.WriteLine("You Win!");
                        break;
                    }*/
                }
                }
        static void Battle(Game_Controller Game_Gnome, Unit Test_Warrior, Field Test_Map, Unit Test_Skeleton, Unit Test_Minotaur, Unit NULL_UNIT, Unit Test_Rabbit)
        {
            Console.WriteLine("\n");
            Console.WriteLine("A battle has begun. A skeleton wishes to challenge you!!");
            Console.WriteLine("You are the U on the map. Periods are open spaces. Slashes are impassible terrain. The X is your target. Fight!");
            Console.WriteLine("Your mission is to defeat the skeleton. Ignore the minotaur.");
            Test_Map.printMap(Test_Warrior.getPlayer(), NULL_UNIT);
            BattleOption(Game_Gnome, Test_Warrior, Test_Map, Test_Skeleton, Test_Minotaur, NULL_UNIT, Test_Rabbit);
        }
        static void BattleOption(Game_Controller Game_Gnome, Unit Test_Warrior, Field Test_Map, Unit Test_Skeleton, Unit Test_Minotaur, Unit NULL_UNIT, Unit Test_Rabbit)
        {
            int i = 0;
            /*Unit[] currentTurn;
            for (int j = 0; j<4 ; i++)
            {
                for (int w = 0; w<4; w++)
                {
                    if ()
                }
            }*/
            /*NYI. Intended to sort units by initiative order, adding them to the array that currently serves
        the purpose of being the current initiative order.*/
            //This array contains the initiative order for this battle.
            Unit[] currentTurn = { Test_Minotaur, Test_Warrior, Test_Rabbit, Test_Skeleton, NULL_UNIT };
            //The following array, like the initiative order, will be used to insure that units killed in action have
            //been accounted for by the game engine.
            bool[] currentTurnAlive = { true, true, true, true, true };
            int currentTurnLength = currentTurn.Length;
            /*try
            {
                string unitName = currentTurn[i].getName();
                bool unitIsControlledByAI = currentTurn[i].getAI();
                Unit currentUnit = currentTurn[i];
            }
            catch
            {
                if (i > currentTurnLength || i < 0) { i = 0; }
            }*/
            //Obsolete
            /*The debug cheat below allows the player to control all units belonging to AI. Notice that the cheat
             is only designed to give the player control of the AI teams; it DOESN'T switch the allegiance of the units
             belonging under AI teams to the player's team. The resulting effect is that the enemy team's units can
             still attack the players.*/
            bool cheatControlAI = false;
            bool pointsGranted = false;
            bool hasMoved = false;
            int originalSpot = 1;
            //The variable below is for purposes of the AI. If the AI can't seem to move anywhere, it gives up trying
            //and ends its turn.
            int numberOfMovementAttempts = 0;
            while (true)
            {
                //Get the name stored in the unit object's data, and store it in a variable.
                //Variable 'i' represents the slot in the array that the unit occupies.
                string unitName = currentTurn[i].getName();
                //Get whether or not the unit is controlled by an AI player in the unit object's data, and store it in a var.
                bool unitIsControlledByAI = currentTurn[i].getAI();
                //Get the unit whose turn it currently is, and store it in a variable.
                Unit currentUnit = currentTurn[i];
                /*When the game engine attempts to reference a non-existent slot in the array, it returns an exception.
                 The reason why it would be doing this is if the value of i falls out of the range of acceptable values in
                 the array. In order to avoid this, I implemented the NULL_UNIT to represent the empty values in the array,
                 so the array could return to a proper array slot
                 It is only a temporary solution to the problem unless a better solution can be implemented.*/
                if (currentUnit == NULL_UNIT)
                {
                    i = 0;
                    while (i < currentTurnLength)
                    {
                        if (currentTurnAlive[i] == false) { i = i + 1; }
                        else { break; }
                    }
                }
                if (currentUnit.checkAlive() == false) { i = i + 1; }
                //After the engine ensures that the array used here falls on an existent slot, we can now assign values
                //to variables representing the returned values of our units.
                unitName = currentTurn[i].getName();
                unitIsControlledByAI = currentTurn[i].getAI();
                currentUnit = currentTurn[i];
                currentTurn[i].setInitiativePriority(i); /*Deprecated. Originally created to allow me to access
                                                              the array positions of the units.*/
                int currentUnitMovementPointsToGrant = currentUnit.getStyle().getMovementPoints();
                int currentUnitAttackPointsToGrant = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Convert.ToDouble(currentUnit.getLevel()) / Convert.ToDouble(5))));
                //Grant units their movement points and attack points.
                if (pointsGranted == false)
                {
                    hasMoved = false;
                    currentUnit.setMovementPoints(currentUnitMovementPointsToGrant);
                    currentUnit.setMovementPointsToSpend(currentUnitMovementPointsToGrant);
                    currentUnit.setAttackPoints(currentUnitAttackPointsToGrant);
                    pointsGranted = true;
                }
                if (cheatControlAI == true)
                {
                    unitIsControlledByAI = false;
                }
                else if (cheatControlAI == false)
                {
                    unitIsControlledByAI = currentTurn[i].getAI();
                }
                if (i > currentTurnLength || i < 0) { i = 0; }
                Console.Write("It is now "/* + unitName + "'s turn."*/ /*<<Obsolete. I can color the text.*/);
                if (currentUnit.getPlayer().getPlayerColor() == "Green")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (currentUnit.getPlayer().getPlayerColor() == "Red")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (currentUnit.getPlayer().getPlayerColor() == "Yellow")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (currentUnit.getPlayer().getPlayerColor() == "Magenta")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                Console.Write(unitName + "'s ");
                Console.ResetColor();
                Console.Write("turn.");
                Console.WriteLine("\n");
                //Console.WriteLine(unitName); //debug
                //Console.WriteLine(unitIsControlledByAI); //debug
                //Console.WriteLine(currentUnit); //debug
                //Console.WriteLine(currentTurnLength); //debug
                //Console.WriteLine("It is now " + currentTurn[i].getName() + "'s turn."); Obsolete
                //The variable below will be used to check if it's a player's turn. If it's not a player's turn, control
                //is handed over to the AI.
                bool isPlayerTurn = false;
                //If the unit whose turn it is is a player unit, it turns control of the unit over to the player.
                if (unitIsControlledByAI == false) { isPlayerTurn = true; }
                if (isPlayerTurn == true)
                {
                    Console.WriteLine("Your health: " + currentUnit.getCurrentHP() + "/" + currentUnit.getBaseMaxHP());
                    Console.WriteLine("You have " + currentUnit.getMovementPointsToSpend() + " movement points.");
                    Console.WriteLine("You have " + currentUnit.getAttackPoints() + " attacks left.");
                    Console.WriteLine("-----Decisions-----");
                    Console.WriteLine("Enter 1 to Move || Enter 2 to Attack || Enter 3 to Cry || Enter 4 to End Turn: ");
                    string input = Console.ReadLine();
                    //int input2 = Convert.ToInt32(input); //Obsolete
                    if (input == "1")
                    {
                        originalSpot = currentUnit.getHost().getSpaceNum();
                        if (/*currentUnit.getMovementPointsToSpend() > 0*//*<<Obsolete*/hasMoved == false)
                        {
                            while (true)
                            {
                                Console.WriteLine("Which Direction would you like to move? Enter w for Up || Enter d for Right || Enter s for Down || Enter a for Left || Enter x to cancel Movement Action || Enter c to confirm Movement Action: ");
                                input = Console.ReadLine();
                                //input2 = Convert.ToInt32(input); //Obsolete
                                if (input == "w" && currentUnit.getMovementPointsToSpend() > 0)
                                {
                                    Console.WriteLine("You Moved up.");
                                    /*Notice something I did in the line below (and in similar lines below): I replaced the
                                    "Test_Warrior" unit with the "currentUnit" variable. The reason why I did this is because
                                    currentUnit can represent whatever unit whose turn it is, as opposed to just the warrior.
                                    While it may be helpful to change for reference purposes, I didn't have to change the
                                    actual argument in the "Attack" and "Move" methods from "Test_Warrior", since it was
                                    only an argument that will take the value of whatever variable/value is inserted when
                                    the method is invoked. Here, move (and attack, by extension) now moves (or attacks) using
                                    the unit whose turn it currently is.*/
                                    Game_Gnome.Move(currentUnit, Test_Map, 0, false);
                                    //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                    Test_Map.printMap(currentUnit.getPlayer(), currentUnit);
                                    //break; //Obsolete
                                }
                                else if (input == "d" && currentUnit.getMovementPointsToSpend() > 0)
                                {
                                    Console.WriteLine("You Moved Right.");
                                    Game_Gnome.Move(currentUnit, Test_Map, 1, false);
                                    //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                    Test_Map.printMap(currentUnit.getPlayer(), currentUnit);
                                    //break; //Obsolete
                                }
                                else if (input == "s" && currentUnit.getMovementPointsToSpend() > 0)
                                {
                                    Console.WriteLine("You Moved Down.");
                                    Game_Gnome.Move(currentUnit, Test_Map, 2, false);
                                    //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                                    Test_Map.printMap(currentUnit.getPlayer(), currentUnit);
                                    //break; //Obsolete
                                }
                                else if (input == "a" && currentUnit.getMovementPointsToSpend() > 0)
                                {
                                    Console.WriteLine("You Moved Left.");
                                    Game_Gnome.Move(currentUnit, Test_Map, 3, false);
                                    //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend()-1); //Obsolete
                                    Test_Map.printMap(currentUnit.getPlayer(), currentUnit);
                                    //break; //Obsolete
                                }
                                else if ((input == "w" || input == "a" || input == "s" || input == "d") && currentUnit.getMovementPointsToSpend() == 0)
                                {
                                    Console.WriteLine("You don't have any more movement points to spend.");
                                }
                                else if (input == "x")
                                {
                                    //The movement action will cancel and the unit will return to their original spot.
                                    currentUnit.setMovementPointsToSpend(currentUnitMovementPointsToGrant);
                                    ReturnToOriginalSpot(currentUnit, Test_Map, originalSpot);
                                    break;
                                }
                                else if (input == "c")
                                {
                                    //The movement action will be completed and the movement points will have been spent.
                                    hasMoved = true;
                                    break;
                                }
                                //Console.WriteLine("Invalid input.");
                            }
                        }
                        else if (/*currentUnit.getMovementPointsToSpend() == 0*/ hasMoved == true) { Console.WriteLine("You've already moved this turn!"); }
                    }
                    else if (input == "2")
                    {
                        if (currentUnit.getAttackPoints() > 0)
                        {
                            bool attacked = false;
                            //The attacked variable will be true whenever the unit lands an attack. It's used to break the while loop.
                            while (attacked == false)
                            {
                                //Ask the player what weapons or abilities to use.
                                Console.WriteLine("Choose from the following weapons or abilities to attack with. Type in the ability number. Type in -1 to cancel.");
                                Console.Write("\n");
                                //It goes through the player's list of abilities and displays them with a number ID.
                                //Type the number ID to use the ability.
                                for (int r = 0; r < currentUnit.getStyle().getAbilities().Count(); r++)
                                {
                                    Console.Write(r + " - " + currentUnit.getStyle().getAbilities()[r].getName() + ", ");
                                }
                                string input3 = Console.ReadLine();
                                int input4 = Convert.ToInt32(input3);
                                //This checks the unit's every ability to identify which ability, r, that the player has chosen.
                                if (input4 == -1) { break; }
                                for (int r = 0; r < currentUnit.getStyle().getAbilities().Count(); r++)
                                {
                                    if (input4 == r)
                                    {
                                        //Declare a list that we will contain all spaces to highlight.
                                        List<Space> highlightedSpaces = new List<Space>();
                                        List<Space> belowMinRangeSpaces = new List<Space>();
                                        Space[] highlightedSpacesArray = { }; //Obsolete
                                        //The for conditions below start from the minimum range of the selected ability and reach the maximum range
                                        //of the ability, adding all the spaces for each range in between (and including) the minRange and maxRange lower
                                        //and upper bounds.
                                        for (int k = currentUnit.getStyle().getAbilities()[r].getMinRange(); k <= currentUnit.getStyle().getAbilities()[r].getMaxRange(); k++)
                                        {
                                            //Each space w from Game_Gnome.Range is going to be added to our highlightedSpaces list. The purpose of the
                                            //Range method is to get all spaces from a certain range (defined as incremented "k" here) from the ability
                                            //user. If it successfully returns spaces, they will be added to highlightedSpaces list.
                                            //foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), k)) 
                                            //{
                                            //highlightedSpaces.Add(w);
                                            /*How to debug: If the foreach loop above successfully added spaces to
                                            highlightedSpaces, then it should be working correctly. If not, the
                                            implication is that no spaces were added because the Game_Gnome.Range method
                                            failed to return any spaces.*/
                                            //Oh, and a message to Game_Gnome: You're FIRED!
                                            //Console.WriteLine("Successful");//debug
                                            //}
                                            //foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), ))
                                            //Game_Gnome.Range(Map, currentUnit.getHost(), 1);
                                        }
                                        List<Space> gameGnome = Game_Gnome.Range(Test_Map, currentUnit.getHost(), currentUnit.getStyle().getAbilities()[r].getMaxRange());
                                        Game_Gnome.Range(Test_Map, currentUnit.getHost(), 2);
                                        Console.WriteLine("Ability: " + currentUnit.getStyle().getAbilities()[r].getName() + ", Max Range: " + currentUnit.getStyle().getAbilities()[r].getMaxRange());
                                        foreach (Space w in gameGnome)
                                        {
                                            highlightedSpaces.Add(w);
                                        }
                                        gameGnome = Game_Gnome.Range(Test_Map, currentUnit.getHost(), currentUnit.getStyle().getAbilities()[r].getMinRange() - 1);
                                        foreach (Space w in gameGnome)
                                        {
                                            belowMinRangeSpaces.Add(w);
                                        }
                                        /*for (int w = 0; w<belowMinRangeSpaces.Count() ; w++)
                                        {
                                            
                                        }*/
                                        //Obsolete
                                        foreach (Space w in belowMinRangeSpaces)
                                        {
                                            highlightedSpaces.Remove(w);
                                        }
                                        //One space in the highlighted spaces is designated as the selected space, or the 'target'.
                                        //It will be colored yellow instead of red.
                                        int selectedSpace = 0;
                                        //int previousSelectedSpace = 0; //Obsolete: See below at "Reasoning:"
                                        while (true)
                                        {
                                            //This loop takes each space in the highlightedSpaces list and modifies
                                            //the value of "setIsAttackHighlighted" and/or "setIsTargetHighlighted".
                                            //These variables are used by Field.cs later and it should color them appropriately when the
                                            //map is printed.
                                            foreach (Space q in highlightedSpaces)
                                            {
                                                q.setIsAttackHighlighted(true); //Red
                                                q.setIsTargetHighlighted(true); //Yellow
                                                //The above two methods set EVERY space in the list to be the target. But we're going
                                                //to fix that by insuring, below, that only ONE space is the target.
                                                //Reasoning: If we just set one space to be the target, the next time we change the target
                                                //later, we would have to make sure the previously selected space is no longer the target.
                                                //My solution is the lazy-man's method of solving this problem. There should only ever
                                                //be one target space, even if we designate a new target.
                                                if (q != highlightedSpaces[selectedSpace]) { q.setIsTargetHighlighted(false); }
                                            }
                                            Test_Map.printMap(currentUnit.getPlayer(), currentUnit);
                                            Console.WriteLine("Please select your target. The red spaces are possible targets, while yellow is your target. Press d to navigate right, press a to navigate left. Press x to cancel, and press c to confirm target.");
                                            input3 = Console.ReadLine();
                                            if (input3 == "d")
                                            {
                                                if (selectedSpace >= highlightedSpaces.Count() - 1)
                                                {
                                                    //Wrap the space selection to the other side of the list.
                                                    selectedSpace = 0;
                                                }
                                                else { selectedSpace++; }
                                            }
                                            else if (input3 == "a")
                                            {
                                                if (selectedSpace <= 0)
                                                {
                                                    //Wrap the space selection to the other side of the list.
                                                    selectedSpace = highlightedSpaces.Count() - 1;
                                                }
                                                else { selectedSpace--; }
                                            }
                                            else if (input3 == "x")
                                            {
                                                foreach (Space q in highlightedSpaces)
                                                {
                                                    //We're exiting space target selection, so unhighlight every space.
                                                    q.setIsAttackHighlighted(false);
                                                    q.setIsTargetHighlighted(false);
                                                }
                                                break;
                                            }
                                            //Prevent the player from attacking their own units if they're not allowed to.
                                            else if (input3 == "c" && currentUnit.getPlayer()==highlightedSpaces[selectedSpace].getGuest().getPlayer() && currentUnit.getPlayer().getCanAttackOwnUnits()==false)
                                            {
                                                Console.WriteLine("You can't attack your own units!");
                                            }
                                            else if (input3 == "c" && (currentUnit.getPlayer() != highlightedSpaces[selectedSpace].getGuest().getPlayer() || currentUnit.getPlayer().getCanAttackOwnUnits() == false))
                                            {
                                                //Confirm the attack, decrement the unit's attack points, and break the while loop
                                                //so that the while loop above it recognizes attacked == true and ends its threads, returning
                                                //to action selection.
                                                //Currently, the attack itself hasn't been scripted in yet. But the major goal
                                                //is solely to highlight the spaces we could attack, so it takes lower priority.
                                                List<Space> aoeSpaces = new List<Space>();
                                                Game_Gnome.attack(currentUnit, Test_Map, currentUnit.getStyle().getAbilities()[r], highlightedSpaces[selectedSpace], aoeSpaces, true, null);
                                                foreach (Space q in highlightedSpaces)
                                                {
                                                    //We're exiting space target selection, so unhighlight every space.
                                                    q.setIsAttackHighlighted(false);
                                                    q.setIsTargetHighlighted(false);
                                                }
                                                attacked = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                /*while (true)
                                {
                                    Console.WriteLine("Which Direction do you wish to strike? Enter w for Up || Enter d for Right || Enter s for Down || Enter a for Left || Enter x to cancel: ");
                                    input = Console.ReadLine();
                                    //input2 = Convert.ToInt32(input); //Obsolete
                                    if (input == "w")
                                    {
                                        List<Ability> list = currentUnit.getStyle().getAbilities();
                                        Console.WriteLine("You Slashed Up.");
                                        Game_Gnome.attack(currentUnit, Map, list, 0);
                                        currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                        break;
                                    }
                                    else if (input == "d")
                                    {
                                        List<Ability> list = currentUnit.getStyle().getAbilities();
                                        Console.WriteLine("You Slashed Right.");
                                        Game_Gnome.attack(currentUnit, Map, list, 1);
                                        currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                        break;
                                    }
                                    else if (input == "s")
                                    {
                                        List<Ability> list = currentUnit.getStyle().getAbilities();
                                        Console.WriteLine("You Slashed Down.");
                                        Game_Gnome.attack(currentUnit, Map, list, 2);
                                        currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                        break;
                                    }
                                    else if (input == "a")
                                    {
                                        List<Ability> list = currentUnit.getStyle().getAbilities();
                                        Console.WriteLine("You Slashed Left.");
                                        Game_Gnome.attack(currentUnit, Map, list, 3);
                                        currentUnit.setAttackPoints(currentUnit.getAttackPoints() - 1);
                                        break;
                                    }
                                    else if (input == "x")
                                    {
                                        break;
                                    }
                                    Console.WriteLine("Invalid input.");
                                }*/
                                //Obsolete
                            }
                        }
                        else if (currentUnit.getAttackPoints() == 0) { Console.WriteLine("You have run out of attacks for this turn."); }
                    }
                    else if (input == "3")
                    {
                        if (currentUnit != Test_Skeleton)
                        {
                            Console.WriteLine("You begin to cry. Crying alot. OMG STFU BITCH!");
                        }
                        else if (currentUnit == Test_Skeleton)
                        {
                            Console.WriteLine("As much as you try to weep, a skeleton doesn't have eyes, and so can't cry...");
                        }
                    }
                    else if (input == "4")
                    {
                        i = i + 1;
                        pointsGranted = false;
                        if (i > currentTurnLength || i < 0) { i = 0; }
                    }
                    else if (input == "9")
                    {
                        if (cheatControlAI == false)
                        {
                            //While the "Control AIs" command is executed on the player's turn, all subsequent
                            //AI unit turns will be controlled by the player until toggled off.
                            Console.WriteLine("##DEBUG## You are now controlling AI teams. You cheater!");
                            cheatControlAI = true;
                        }
                        else if (cheatControlAI == true)
                        {
                            Console.WriteLine("##DEBUG## You are no longer controlling AI teams.");
                            cheatControlAI = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid input. :(");
                    }
                }
                else if (isPlayerTurn == false)
                {
                    /*string input = Console.ReadLine();
                    int input2 = Convert.ToInt32(input);
                    Console.WriteLine("It isn't your turn yet!");*/
                    /*NYI. The idea here was, if the player attempted
to type a command to the command line while it's not their turn, it would say
"It isn't your turn yet!". Console.ReadLine() waits for the user to give an input before processessing
further instructions.*/
                    //Having no instructions (from artificial intelligence or an algorithm table), the AI ends their turn.
                    int r = 0;
                    bool AIActionsCompleted = false;
                    bool rabbitHasMoved = false;
                    if (currentUnit == Test_Rabbit && AIActionsCompleted == false)
                    {
                        //Attempt to move onto a random space. If the RNG keeps picking illegal spaces, the AI
                        //is hard-coded to give up and end their turn.
                        if (rabbitHasMoved == false)
                        {
                            Random rand = new Random();
                            r = rand.Next(0, 3);
                            //int ticks = 0; //Obsolete
                            //Make the system wait for 1 seconds before deciding its actions.
                            System.Threading.Thread.Sleep(1000);
                            Game_Gnome.Move(currentUnit, Test_Map, r, false);
                            numberOfMovementAttempts++;
                            //currentUnit.setMovementPointsToSpend(currentUnit.getMovementPointsToSpend() - 1); //Obsolete
                            if (currentUnit.getMovementPointsToSpend() <= 0 || numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() * 3)
                            {
                                //int ticks = 0; //Obsolete
                                //Make the system wait for 1 seconds before deciding its actions.
                                System.Threading.Thread.Sleep(1000);
                                i = i + 1;
                                numberOfMovementAttempts = 0;
                                pointsGranted = false;
                                rabbitHasMoved = true;
                            }
                        }
                        else if (rabbitHasMoved == true) { AIActionsCompleted = true; }
                    }
                    if (currentUnit != Test_Rabbit || AIActionsCompleted == true)
                    {
                        //Make the system wait for 1 seconds before deciding its actions.
                        System.Threading.Thread.Sleep(1000);
                        i = i + 1;
                        pointsGranted = false;
                    }
                    if (i > currentTurnLength || i < 0) { i = 0; }
                }
                //This loop checks each unit in the current battle to see if they're living or not. If the unit
                //isn't living, it will mark them off as dead and send an appropriate message to the console.
                for (int m = 0; m < currentTurnLength; m++)
                {
                    if (currentTurnAlive[m] == true)
                    {
                        if (currentTurn[m].checkAlive() == false)
                        {
                            Console.WriteLine(currentTurn[m].getName() + " died!");
                            //The dead body is removed from the map.
                            KillUnit(currentTurn[m], Test_Map);
                            currentTurnAlive[m] = false;
                        }
                    }
                }
                if (currentUnit.checkAlive() == true) { Test_Map.printMap(currentUnit.getPlayer(), currentUnit); }
                if (Test_Warrior.checkAlive() == false)
                {
                    Console.WriteLine("Your last unit died!");
                    Console.WriteLine("Game over, man! GAME OVER!");
                    break;
                }
                if (Test_Skeleton.checkAlive() == false)
                {
                    Console.WriteLine("You Win!");
                    break;
                }
            }
        }

        static void ReturnToOriginalSpot(Unit Test_Warrior, Field Test_Map, int oldSpot)
        {
            int currSpace = Test_Warrior.getHost().getSpaceNum();
            int oldSpace = Test_Warrior.getHost().getSpaceNum();
            currSpace = oldSpot;
            if (Test_Map.getSpace(currSpace).getGuest() == null && Test_Map.getSpace(currSpace).getExist() == true)
            {
                Test_Map.getSpace(oldSpace).setGuest(null);
                Test_Map.getSpace(currSpace).setGuest(Test_Warrior);
                Test_Warrior.setHost(Test_Map.getSpace(currSpace));
            }
        }

        static void KillUnit(Unit Test_Warrior, Field Test_Map)
        {
            try
            {
                int currSpace = 0; //Test_Warrior.getHost().getSpaceNum();
                int oldSpace = Test_Warrior.getHost().getSpaceNum();
                //if (Test_Map.getSpace(currSpace).getGuest() == null && Test_Map.getSpace(currSpace).getExist() == true)
                {
                    Console.WriteLine("Testing");
                    Test_Map.getSpace(oldSpace).setGuest(null);
                    Test_Warrior.setHost(null);
                    //Test_Map.PlaceUnit(Test_Warrior, 0);
                }
            }
            catch
            {

            }
        }
        //The method below can be used to set the color of text by player.
        static void returnPlayerColor(Player Player)
        {
            if (Player.getPlayerColor() == "Green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (Player.getPlayerColor() == "Red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (Player.getPlayerColor() == "Yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (Player.getPlayerColor() == "Magenta")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }
        //Every time this method is called, it will make sure the stats of a unit is updated.
        static void updateStats(Unit unit)
        {
            int level = unit.getLevel();
            int baseMaxHP = unit.getBaseMaxHP();
            int baseMaxMP = unit.getBaseMaxMP();
            int baseStrength = unit.getStrength();
            int baseSpeed = unit.getSpeed();
            int baseConstitution = unit.getConstitution();
            int baseLC = unit.getLC();
            int strengthModifiers;
            int speedModifiers;
            int constitutionModifiers;
            int leyConnexionModifiers;
            int modifiedHP;
            int modifiedMP;
            int modifiedStrength;
            int modifiedSpeed;
            int modifiedConstitution;
            int modifiedLeyConnexion;
            int Armor;
            int magicDefense;
            int magicSave;
            int Dodge;
            int Aim;
            int MPRegen;
            int experience;
            int experienceCap;
            int baseExperienceYield = unit.getBaseExperienceYield();
            int experienceYieldModifiers;
            int modifiedExperienceYield;
            strengthModifiers = level * (unit.getClass().getStrengthModifier());
            constitutionModifiers = level * (unit.getClass().getConstitutionModifier());
            speedModifiers = level * (unit.getClass().getSpeedModifier());
            leyConnexionModifiers = level * (unit.getClass().getLCModifier());
            experienceYieldModifiers = (level * (unit.getClass().getExperienceYield())) * 50;
            modifiedStrength = baseStrength + strengthModifiers;
            modifiedSpeed = baseSpeed + speedModifiers;
            modifiedConstitution = baseConstitution + constitutionModifiers;
            modifiedLeyConnexion = baseLC + leyConnexionModifiers;
            modifiedHP = baseMaxHP + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedConstitution-2)/2))) + level*(unit.getClass().getConstitutionModifier());
            if (modifiedHP <= 0)
            {
                modifiedHP = 1;
            }
            modifiedMP = baseMaxMP + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedLeyConnexion - 2) / 2))) + level * (unit.getClass().getLCModifier());
            Armor = Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedConstitution) / 50)));
            magicDefense = level * (unit.getClass().getLCModifier()) + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedLeyConnexion) / 2)));
            magicSave = level * (unit.getClass().getLCModifier()) + modifiedLeyConnexion*2;
            Aim = level * (unit.getClass().getAimModifier());
            Dodge = (level * (unit.getClass().getDodgeModifier())) + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedSpeed/2))));
            if (modifiedLeyConnexion >= 3)
            {
                //Guideline: A level 1 wizard of average LC for his level should be regaining 2 MP every round.
                MPRegen = (level * (unit.getClass().getMPRegenModifier())) + Convert.ToInt32(Math.Floor(Convert.ToDouble((modifiedLeyConnexion) / 2))) - 2;
            }
            else { MPRegen = 0; }
            if (MPRegen < 0)
            {
                MPRegen = 0;
            }
            modifiedExperienceYield = baseExperienceYield + experienceYieldModifiers;
            experienceCap = level * 100;
            unit.setModifiedHP(modifiedHP);
            unit.setModifiedMP(modifiedMP);
            unit.setModifiedStrength(modifiedStrength);
            unit.setModifiedSpeed(modifiedSpeed);
            unit.setModifiedConstitution(modifiedConstitution);
            unit.setModifiedLC(modifiedLeyConnexion);
            unit.setArmor(Armor);
            unit.setMagicDefense(magicDefense);
            unit.setMagicSave(magicSave);
            unit.setDodge(Dodge);
            unit.setAim(Aim);
            unit.setMPRegen(MPRegen);
            unit.setExperienceCap(experienceCap);
            unit.setModifiedExperienceYield(modifiedExperienceYield);
        }
        static void highlightAttackableSpaces(List<Ability> abilityList, List<Weapon> weaponList, int currentAbilityIndex, Game_Controller gnome, Field Map, Unit currentUnit, bool isCurrentAbilityWeapon, List<Unit> previousUnitList, List<Space> previousSpaceList, Player[] players)
        {
            //Declare a list that we will contain all spaces to highlight.
            //string input3;
            ConsoleKeyInfo input4;
            List<Space> highlightedSpaces = new List<Space>();
            //All spaces that are also part of the selection will be highlighed yellow; this is used for spells that have a radius (AoE).
            List<Space> aoeSpaces = new List<Space>();
            //These spaces are below the minimum range and so are unhighlighted.
            List<Space> belowMinRangeSpaces = new List<Space>();
            Space[] highlightedSpacesArray = { }; //Obsolete
            //The for conditions below start from the minimum range of the selected ability and reach the maximum range
            //of the ability, adding all the spaces for each range in between (and including) the minRange and maxRange lower
            //and upper bounds.
            for (int k = abilityList[currentAbilityIndex].getMinRange(); k <= abilityList[currentAbilityIndex].getMaxRange(); k++)
            {
                //Each space w from Game_Gnome.Range is going to be added to our highlightedSpaces list. The purpose of the
                //Range method is to get all spaces from a certain range (defined as incremented "k" here) from the ability
                //user. If it successfully returns spaces, they will be added to highlightedSpaces list.
                //foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), k)) 
                //{
                //highlightedSpaces.Add(w);
                /*How to debug: If the foreach loop above successfully added spaces to
                highlightedSpaces, then it should be working correctly. If not, the
                implication is that no spaces were added because the Game_Gnome.Range method
                failed to return any spaces.*/
                //Oh, and a message to Game_Gnome: You're FIRED!
                //Console.WriteLine("Successful");//debug
                //}
                //foreach (Space w in Game_Gnome.Range(Map, currentUnit.getHost(), ))
                //Game_Gnome.Range(Map, currentUnit.getHost(), 1);
            }
            //List<Space> gameGnome = Game_Gnome.Range(Map, currentUnit.getHost(), currentUnit.getStyle().getAbilities()[r].getMaxRange());
            List<Space> gameGnome = new List<Space>();
            gnome.Range(Map, currentUnit.getHost(), 2);
            Console.WriteLine("Ability: " + abilityList[currentAbilityIndex].getName() + ", Max Range: " + abilityList[currentAbilityIndex].getMaxRange());
            if (isCurrentAbilityWeapon == true)
            {
                Console.WriteLine(abilityList[currentAbilityIndex].getName() + " durability: " + weaponList[currentAbilityIndex].getCurrentDurability() + "/" + weaponList[currentAbilityIndex].getMaxDurability());
            }
            for (int h = abilityList[currentAbilityIndex].getMinRange(); h <= abilityList[currentAbilityIndex].getMaxRange(); h++)
                foreach (Space w in gnome.Range(Map, currentUnit.getHost(), h))
                {
                    {
                        highlightedSpaces.Add(w);
                    }
                }
            //gameGnome = Game_Gnome.Range(Map, currentUnit.getHost(), currentUnit.getStyle().getAbilities()[r].getMinRange() - 1);
            /*foreach (Space w in gameGnome)
            {
                belowMinRangeSpaces.Add(w);
            }*/
            //Obsolete
            /*for (int w = 0; w<belowMinRangeSpaces.Count() ; w++)
            {
                                            
            }*/
            //Obsolete
            /*foreach (Space w in belowMinRangeSpaces)
            {
                highlightedSpaces.Remove(w);
            }*/
            //Obsolete
            //One space in the highlighted spaces is designated as the selected space, or the 'target'.
            //It will be colored yellow instead of red.
            int selectedSpace = 0;
            //int previousSelectedSpace = 0; //Obsolete: See below at "Reasoning:"
            while (true)
            {
                //This loop takes each space in the highlightedSpaces list and modifies
                //the value of "setIsAttackHighlighted" and/or "setIsTargetHighlighted".
                //These variables are used by Field.cs later and it should color them appropriately when the
                //map is printed.
                List<Space> oldAOESpaces = new List<Space>();
                foreach (Space q in aoeSpaces)
                {
                    oldAOESpaces.Add(q);
                }
                aoeSpaces = new List<Space>();
                for (int h = 0; h <= abilityList[currentAbilityIndex].getRadius(); h++)
                    foreach (Space w in gnome.Range(Map, highlightedSpaces[selectedSpace], h))
                    {
                        {
                            aoeSpaces.Add(w);
                        }
                    }
                foreach (Space q in oldAOESpaces)
                {
                    q.setIsTargetHighlighted(false);
                }
                foreach (Space q in aoeSpaces)
                {
                    q.setIsTargetHighlighted(true);
                }
                foreach (Space q in highlightedSpaces)
                {
                    q.setIsAttackHighlighted(true); //Red
                    //q.setIsTargetHighlighted(true); //Yellow
                    //The above two methods set EVERY space in the list to be the target. But we're going
                    //to fix that by insuring, below, that only ONE space is the target.
                    //Reasoning: If we just set one space to be the target, the next time we change the target
                    //later, we would have to make sure the previously selected space is no longer the target.
                    //My solution is the lazy-man's method of solving this problem. There should only ever
                    //be one target space, even if we designate a new target.
                    //if (q != highlightedSpaces[selectedSpace]) { q.setIsTargetHighlighted(false); }
                }
                Map.printMap(currentUnit.getPlayer(), currentUnit);
                if (highlightedSpaces[selectedSpace].getGuest() == null || highlightedSpaces[selectedSpace].getConcealed(currentUnit.getPlayer().getPlayerID()) == true)
                {

                }
                else
                {
                    Console.WriteLine("Target: " + highlightedSpaces[selectedSpace].getGuest().getName());
                    Console.WriteLine("Level: " + highlightedSpaces[selectedSpace].getGuest().getLevel());
                    Console.WriteLine("Health: " + highlightedSpaces[selectedSpace].getGuest().getCurrentHP());
                }
                Console.WriteLine("Please select your target. The red spaces are possible targets, while yellow is your target. Press d to navigate right, press a to navigate left. Press x to cancel, and press c to confirm target.");
                //input3 = Console.ReadLine();
                input4 = Console.ReadKey(true);
                if (input4.Key==ConsoleKey.D)
                {
                    if (selectedSpace >= highlightedSpaces.Count() - 1)
                    {
                        //Wrap the space selection to the other side of the list.
                        selectedSpace = 0;
                    }
                    else { selectedSpace++; }
                }
                else if (input4.Key == ConsoleKey.A)
                {
                    if (selectedSpace <= 0)
                    {
                        //Wrap the space selection to the other side of the list.
                        selectedSpace = highlightedSpaces.Count() - 1;
                    }
                    else { selectedSpace--; }
                }
                else if (input4.Key == ConsoleKey.X)
                {
                    foreach (Space q in highlightedSpaces)
                    {
                        //We're exiting space target selection, so unhighlight every space.
                        q.setIsAttackHighlighted(false);
                        q.setIsTargetHighlighted(false);
                    }
                    foreach (Space q in aoeSpaces)
                    {
                        q.setIsTargetHighlighted(false);
                    }
                    break;
                }
                //Prevent the player from attacking their own units if they're not allowed to.
                /*else if (input3 == "c" && currentUnit.getPlayer().getCanAttackOwnUnits() == false)
                {
                    if (highlightedSpaces[selectedSpace].getGuest() == null)
                    {

                    }
                    else if (highlightedSpaces[selectedSpace].getGuest().getPlayer() == currentUnit.getPlayer())
                    {
                        Console.WriteLine("You can't attack your own units!");
                    }
                }*/ //Bugged
                else if (input4.Key == ConsoleKey.C)
                {
                    //Confirm the attack, decrement the unit's attack points, and break the while loop
                    //so that the while loop above it recognizes attacked == true and ends its threads, returning
                    //to action selection.
                    //Currently, the attack itself hasn't been scripted in yet. But the major goal
                    //is solely to highlight the spaces we could attack, so it takes lower priority.
                    if (abilityList[currentAbilityIndex].getName() == "Deja Vu")
                    {
                        Space referredSpace = null;
                        for (int u = 0; u < previousUnitList.Count(); u++)
                        {
                            if (previousUnitList[u] == null) { }
                            else if (previousUnitList[u] == highlightedSpaces[selectedSpace].getGuest())
                            {
                                referredSpace = previousSpaceList[u];
                            }
                        }
                        gnome.attack(currentUnit, Map, abilityList[currentAbilityIndex], highlightedSpaces[selectedSpace], aoeSpaces, true, referredSpace);
                        foreach (Player p in players)
                        {
                            p.setSpaceOfInterest(highlightedSpaces[selectedSpace]);
                        }
                            
                    }
                    else
                    {
                        gnome.attack(currentUnit, Map, abilityList[currentAbilityIndex], highlightedSpaces[selectedSpace], aoeSpaces, true, null);
                        foreach (Player p in players)
                        {
                            p.setSpaceOfInterest(highlightedSpaces[selectedSpace]);
                        }
                    }
                    if (isCurrentAbilityWeapon == true)
                    {
                        weaponList[currentAbilityIndex].damageWeapon(1);
                    }
                    foreach (Space q in highlightedSpaces)
                    {
                        //We're exiting space target selection, so unhighlight every space.
                        q.setIsAttackHighlighted(false);
                        q.setIsTargetHighlighted(false);
                    }
                    foreach (Space q in aoeSpaces)
                    {
                        q.setIsTargetHighlighted(false);
                    }
                    //attacked = true;
                    break;
                }
            }
        }
    }
}
