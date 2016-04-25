using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    //An AI object is instantiated for every battle. The AI is used for that battle and comes in control of a single unit on
    //the AI's turn.
    class AI
    {
        string name;
        Field Map;
        Game_Controller Game_Gnome;
        Player[] Players;
        List<Unit> unitsList;
        bool isFogOfWarOn = false;
        public AI(string name, Game_Controller Game_Gnome, Field Map, Player[] Players, List<Unit> currentUnitsTurn, bool fogOfWar)
        {
            this.name = name;
            this.Map = Map;
            this.Game_Gnome = Game_Gnome;
            this.Players = Players;
            this.unitsList = currentUnitsTurn;
            this.isFogOfWarOn = fogOfWar;
        }
        public void battleAI_Evaluate(Unit currentUnit, Player currentControlledPlayer, bool pointsGranted, List<Unit> alliedUnits0, List<Unit> alliedUnits1, List<Unit> alliedUnits2, List<Unit> alliedUnits3, List<Space> visibleSpaces0, List<Space> visibleSpaces1, List<Space> visibleSpaces2, List<Space> visibleSpaces3, Player[] players, List<Space> previousSpaceList, List<Unit> previousUnitList)
        {
            //Attempt to move onto a random space. If the RNG keeps picking illegal spaces, the AI
            //is hard-coded to give up and end their turn.
            System.Threading.Thread.Sleep(1000);
            //if (rabbitHasMoved == false)
            int r = 0;
            int numberOfMovementAttempts = 0;
            bool fogOfWarEnabled = isFogOfWarOn;
            bool rabbitHasMoved = false;
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
                /*while (true)
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
                }*/

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
            List<Space> fogOfWarSpaces = new List<Space>();
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
                            for (int q = 0; q <= u.getDominantFog().getVisionRadius(); q++)
                            {
                                currentEvaluatedRange = Game_Gnome.Range(Map, alliedUnits0[selectedUnit].getHost(), q);
                                foreach (Space g in currentEvaluatedRange)
                                {
                                    if (g == u/* && u.getDominantFog().getActive()==true*/)
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
                    }*/
                    //Obsolete
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
                                    if (g == u/* && u.getDominantFog().getActive() == true*/)
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
            //With the attack behavior, AI units would only be attacking units nearby and wouldn't run to pursue farther-off units.
            //System.Threading.Thread.Sleep(1000);
            //Determine the best target within range of each ability.
            int attackAttempts = 0;
            while (currentUnit.getAttackPoints() > 0)
            {
                if (attackAttempts >= currentUnit.getAttackPoints() + 1)
                {
                    break;
                }
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
                Space targetSpaceToAttackFrom = null;
                List<Space> targetSpacePath = null;
                List<Space> aoeSpaces = new List<Space>();
                Space referredSpace = null;
                List<Unit> hitList = new List<Unit>();
                List<Ability> hitList_Abilities = new List<Ability>();
                List<int> hitList_CurrentHealths = new List<int>();
                List<int> hitList_DamagePossible = new List<int>();
                List<Space> hitList_Spaces = new List<Space>();
                List<List<Space>> hitList_Spaces_Path = new List<List<Space>>();
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
                List<Space> movementSpaces = new List<Space>();
                List<List<Space>> movementSpaces_Path = new List<List<Space>>();
                //Look at each ability and look for potential targets for attack.
                foreach (Space g in Game_Gnome.movementRange(Map, currentUnit.getPlayer(), currentUnit.getMovementPointsToSpend(), currentUnit.getHost())){
                    //movementSpaces_Path.Add(g);
                    movementSpaces.Add(g);
                }
                //for (int s = 0; s < movementSpaces_Path.Count(); s++)
                {
                    for (int g = 0; g < movementSpaces.Count(); g++)
                    {
                        //List<Space> possibleTargets = new List<Space>();
                        for (int q = 0; q < abilitiesToUse.Count(); q++)
                        {
                            for (int l = abilitiesToUse[q].getMinRange(); l <= abilitiesToUse[q].getMaxRange(); l++)
                            {
                                //for (int h = 0; h < Game_Gnome.Range(Map, movementSpaces[g], l).Count(); g++)
                                foreach (Space h in Game_Gnome.Range(Map, movementSpaces[g], l))
                                {
                                    //Spaces with no unit are disqualified.
                                    if (h.getGuest() == null)
                                    {

                                    }
                                    else
                                    {
                                        //Spaces with allied, neutral, or units of the same player are disqualified.
                                        if (h.getGuest().getPlayer() == currentUnit.getPlayer() || currentUnit.getPlayer().getRelationshipToPlayer(h.getGuest().getPlayer().getPlayerID()) == "ally" || currentUnit.getPlayer().getRelationshipToPlayer(h.getGuest().getPlayer().getPlayerID()) == "neutral")
                                        {

                                        }
                                        else
                                        {
                                            //Spaces with units concealed by fog of war are disqualified.
                                            if (h.getConcealed(currentUnit.getPlayer().getPlayerID()) == true)
                                            {

                                            }
                                            else
                                            {
                                                //Spaces that meet all conditions are the possible targets.
                                                hitList.Add(h.getGuest());
                                                //Console.WriteLine("Added " + possibleTargets[h].getGuest().getName() + " to the hit list!");
                                                hitList_Abilities.Add(abilitiesToUse[q]);
                                                hitList_CurrentHealths.Add(h.getGuest().getCurrentHP());
                                                //The spaces hit list is where the unit is able to be attacked.
                                                hitList_Spaces.Add(movementSpaces[g]);
                                                //hitList_Spaces_Path.Add(movementSpaces_Path[s]);
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
                        }
                    }
                }
                
                /*for (int q = 0; q < abilitiesToUse.Count(); q++)
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
                }*/
                //Now that we've listed all possible attacks we can make, let's give each possible decision a weight.
                //Count through all of the possible decisions and check circumstances beneficial to the AI.
                List<double> decisionsByHighestWeight = new List<double>();
                List<Ability> decisionsByHighestWeight_Abilities = new List<Ability>();
                List<Unit> decisionsByHighestWeight_HitList = new List<Unit>();
                List<Space> decisionsByHighestWeight_Spaces = new List<Space>();
                List<List<Space>> decisionsByHighestWeight_Spaces_Path = new List<List<Space>>();
                for (int q = 0; q < hitList.Count(); q++)
                {
                    Unit potentialTargetUnit = hitList[q];
                    double currentDecisionWeight = hitList_DecisionWeights[q]; //Starts at "0"
                    //If there are no other good decisions, the AI at least wants to deal as much damage as possible.
                    double weightOfDamageThatCouldBeDealt = hitList_DamagePossible[q];
                    //The AI ALWAYS prefers to kill possible targets than leave them alive. Yeah, they're dicks.
                    //Also, when presented with the choice of killing higher level units and lower level units in one attack, it will
                    //prefer to kill higher level units.
                    double weightOfTargetLevel = 1 / ((double)hitList[q].getLevel()) * 100;
                    weightOfTargetLevel = Math.Ceiling(weightOfTargetLevel);
                    double weightOfPotentialToKill = 0;
                    if (hitList_CurrentHealths[q] - hitList_DamagePossible[q] <= 0)
                    {
                        //Notice how this time the weight of target level is similarly used here, but it is DIRECTLY proportional
                        //to target level instead of inversely proportional. Higher level is prioritized here.
                        weightOfPotentialToKill = 100*(double)hitList[q].getLevel();
                    }
                    //The AI prefers to attack weaker targets in general.
                    //This weight is unused for now, but in any case, the AI would want to attack units perceived to be
                    //more useful than others. Healers are preferred to non-healers.
                    double weightOfTargetUsefulnessByClass = 0;
                    foreach (String w in potentialTargetUnit.getClass().getAllDescriptors())
                    {
                        switch (w)
                        {
                                //The more powerful the healer, the more threatening. They are huge threats in the late game and should be eliminated
                                //as soon as possible.
                            case "healer":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 25 * (potentialTargetUnit.getLevel());
                                break;
                                //The more powerful the mage, the more threatening. They are significant threats in the late game, if ley isn't factored
                                //in.
                            case "mage":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 15 * (potentialTargetUnit.getLevel());
                                break;
                                //Mezzers, mostly mages like illusionists and sorcerors, are significant threats in the late game. Their spells
                                //can easily change the tide of battle against the enemy if used at the right times in the right places.
                            case "mezzer":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 15 * (potentialTargetUnit.getLevel());
                                break;
                                //Summoner-type classes like summoners and necromancers are priotized because of their ability to summon more
                                //units onto the battlefield.
                            case "summoner":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 20 * (potentialTargetUnit.getLevel());
                                break;
                                //Tanks, boastful in their gait and their very fighting style, incur the wrath of AI units.
                            case "tank":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 25 * (potentialTargetUnit.getLevel());
                                break;
                                //Support units, like buffers and utility mages are somewhat threatening to enemy units.
                            case "support":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 10 * (potentialTargetUnit.getLevel());
                                break;
                                //Pests like small animals are merely an annoyance to enemies, who, lacking any other good targets, may dispose of this
                                //"threat" sooner.
                            case "pest":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + (potentialTargetUnit.getLevel());
                                break;
                                //Thieves that are more powerful are more likely to get away. Thieves that are more likely to get away
                                //are more likely to make off with their stolen loot. Thieves that can get away with stolen loot are 
                                //unacceptable to enemies that want to get their stolen loot back. Therefore, they are big threats.
                            case "thief":
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass + 25 * (potentialTargetUnit.getLevel());
                                break;
                            case "civilian":
                                //Note that the fact that a class is a "civilian" class decreases its target weight. Civilians are usually
                                //escorts for escort missions and are not worth the enemy's time killing when they can kill bigger threats instead.
                                weightOfTargetUsefulnessByClass = weightOfTargetUsefulnessByClass - 5 * (potentialTargetUnit.getLevel());
                                break;
                        }
                    }
                    //This weight is unused for now, but in any case, the AI will prefer to attack targets that have its aggro
                    double weightOfAggro = 0;
                    //This weight is unused for now. This is a weight for arbitrary reasons why the AI would prefer to attack
                    //particular units for others. Units perceived to be the hero, for example, will incur the hatred
                    //of their enemies, causing the AI to reflect this hatred in-game by choosing to attack them over others.
                    //This metric is also useful for AIs that are predators of other units, so that they might prefer to attack
                    //their prey.
                    //This metric is also used by AIs that will prefer to attack the hero when necessary.
                    double weightOfHatred = 0;
                    foreach (string w in potentialTargetUnit.getAllDescriptors())
                    {
                        switch (w)
                        {
                                //The leader is really important to kill, so prioritize them if they're weak.
                                //However, prioritize them nonetheless. The leader of the first player in campaign is usually Sanara, 
                                //and the leader of the enemy team varies from mission to mission.
                            case "leader":
                                weightOfHatred = weightOfHatred + 25 * Math.Ceiling((double)(1/potentialTargetUnit.getLevel()));
                                break;
                                //A hero unit (or villain unit for those rare maps where you play the bad guys) ends the game in defeat
                                //for the player that owns them if slain. These are usually the protagonists themselves
                                //or the closest friends of the protagonist that are important to the plot (or in challenge mode,
                                //simply units required to survive to beat the map). For this reason, they are prioritized because the AI
                                //are simply dicks.
                            case "hero":
                                weightOfHatred = weightOfHatred + 100;
                                break;
                            case "villain":
                                weightOfHatred = weightOfHatred + 100;
                                break;
                            case "famous":
                                weightOfHatred = weightOfHatred + 50;
                                break;
                            case "infamous":
                                weightOfHatred = weightOfHatred + 50;
                                break;
                            case "bounty":
                                weightOfHatred = weightOfHatred + 5*Math.Ceiling((double)(1/potentialTargetUnit.getLevel()));
                                break;
                                //In missions where you must keep units, especially those not under your control, alive, I'm not going
                                //to make the AI a total troll. It will still attack escort units, but if given the choice it will prefer
                                //to attack the units escorting those units so that escort missions aren't impossible.
                            case "escort":
                                weightOfHatred = weightOfHatred - 50;
                                break;

                        }
                    }
                    //The weight of fear is simply a measure of how threatened the unit feels towards another unit due to how powerful
                    //it is. If the level difference between this unit and the potential target is in favor of the potential target, it would
                    //prefer to disregard it if possible. This becomes more significant when the potential target is higher leveled.
                    double weightOfFear = 0;
                    if (currentUnit.getLevel() < potentialTargetUnit.getLevel())
                    {
                        //This will always be a negative value because the potential target unit's level will always be higher, therefore
                        //the difference will be negative.
                        //A difference of 4 or higher is significant, since the weight of fear in these cases would be less than -100.
                        weightOfFear = (currentUnit.getLevel()-potentialTargetUnit.getLevel())*25;
                    }
                    //The weight of space advantage is whether or not this space would be most advantageous to attack from. As a general rule, the spaces
                    //that are farthest away are preferred since an enemy unit would have to spend many of their movement points
                    //to reach this unit.
                    double weightOfSpaceAdvantage = 0;
                    //What an ability can't make up for in damage, it could probably make up for in novelty or usefulness.
                    //Abilities like these are typically disabling spells from the mage classes but can be any ability
                    //that doesn't do any damage.
                    double weightOfAbilityUsefulness = 0;
                    currentDecisionWeight = weightOfDamageThatCouldBeDealt + weightOfPotentialToKill + weightOfTargetLevel + weightOfTargetUsefulnessByClass + weightOfAggro + weightOfHatred + weightOfFear;
                    hitList_DecisionWeights[q] = currentDecisionWeight;
                    //Console.WriteLine("Decision weight for decision " + q + " = " + hitList_DecisionWeights[q]);
                }
                //Re-sort all the possible decisions by highest weight.
                //Decisions with a weight less than -500 won't be taken under any circumstances.
                for (int w = 99999; w >= -500; w--)
                {
                    for (int q = 0; q < hitList_DecisionWeights.Count(); q++)
                    {
                        if (hitList_DecisionWeights[q] == w)
                        {
                            decisionsByHighestWeight.Add(hitList_DecisionWeights[q]);
                            decisionsByHighestWeight_Abilities.Add(hitList_Abilities[q]);
                            decisionsByHighestWeight_HitList.Add(hitList[q]);
                            decisionsByHighestWeight_Spaces.Add(hitList_Spaces[q]);
                            //decisionsByHighestWeight_Spaces_Path.Add(hitList_Spaces_Path[q]);
                        }
                    }
                }
                //For now, our targets will be the decision with the highest weight (or the decision that got lucky enough to be first :P).
                //If it turns out there are no decisions, run towards an enemy unit or the space of interest.
                //Console.WriteLine("The number of heighest weight decisions is " + decisionsByHighestWeight.Count());
                if (decisionsByHighestWeight.Count <= 0)
                {
                    Space idealTargetSpace;
                    List<Unit> potentialUnits = new List<Unit>();
                    Random rand = new Random();
                    foreach (Space u in Map.getAllSpaces())
                    {
                        if (u.getGuest() == null)
                        {

                        }
                        else
                        {
                            if (currentUnit.getPlayer().getRelationshipToPlayer(u.getGuest().getPlayer().getPlayerID()) == "hostile" && u.getConcealed(currentUnit.getPlayer().getPlayerID()) == false)
                            {
                                potentialUnits.Add(u.getGuest());
                            }
                        }
                    }
                    if (potentialUnits.Count() > 0)
                    {
                        r = rand.Next(0, potentialUnits.Count()-1);
                        Console.WriteLine("Target found. Moving to ideal target space (ID " + potentialUnits[r].getHost().getID() + ", contains " + potentialUnits[r].getName() + ").");
                        idealTargetSpace = potentialUnits[r].getHost();
                    }
                    else
                    {
                        Console.WriteLine("No target found. Moving to ideal target space of interest (ID " + currentUnit.getPlayer().getSpaceOfInterest().getID()+ ").");
                        idealTargetSpace = currentUnit.getPlayer().getSpaceOfInterest();
                    }
                    List<int> plottedDirections = new List<int>();
                    List<Space> plottedMovements = new List<Space>();
                    Space currentUnitPosition = currentUnit.getHost();
                    int spaceNumber = 0;
                    List<Space> path = new List<Space>();
                    foreach (Space o in Game_Gnome.pathfinder(Map, currentUnit, currentUnit.getHost(), idealTargetSpace))
                    {
                        Console.WriteLine("Path space ID: " + o.getID());
                        path.Add(o);
                    }
                    Console.WriteLine("Goal space ID: " + idealTargetSpace.getID());
                    while (currentUnit.getHost() != idealTargetSpace)
                    {
                        //Immediately break the loop if there's nothing in path.
                        if (path.Count() == 0)
                        {
                            break;
                        }
                        //XorY = rand.Next(0, 1);
                        //Evaluated space is
                        //Space evaluatedSpace = currentUnitPosition;
                        //bool otherDirectionChecked = false;
                        //Try to plot movements that get the unit closer to its target space.
                        //int numberOfXFromCurrentPosition = Map.getXFromSpace(currentUnitPosition, targetSpaceToAttackFrom);
                        //int numberOfYFromCurrentPosition = Map.getYFromSpace(currentUnitPosition, targetSpaceToAttackFrom);
                        //int xDirection = numberOfXFromCurrentPosition / numberOfXFromCurrentPosition;
                        //int yDirection = numberOfYFromCurrentPosition / numberOfYFromCurrentPosition;
                        /*if (numberOfXFromCurrentPosition == 0)
                        {
                            XorY = 1;
                        }
                        if (numberOfYFromCurrentPosition == 0)
                        {
                            XorY = 0;
                        }
                        if (XorY == 0)
                        {
                            if (numberOfXFromCurrentPosition < 0)
                            {
                                evaluatedSpace = currentUnitPosition.getWest();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID())==true) && evaluatedSpace.getExist()==true)
                                {
                                    r = 3;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                            else if (numberOfXFromCurrentPosition > 0)
                            {
                                evaluatedSpace = currentUnitPosition.getEast();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && evaluatedSpace.getExist() == true)
                                {
                                    r = 1;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                        }
                        if (XorY == 1)
                        {
                            if (numberOfYFromCurrentPosition < 0)
                            {
                                evaluatedSpace = currentUnitPosition.getNorth();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && evaluatedSpace.getExist() == true)
                                {
                                    r = 0;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                            else if (numberOfYFromCurrentPosition > 0)
                            {
                                evaluatedSpace = currentUnitPosition.getSouth();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && evaluatedSpace.getExist() == true)
                                {
                                    r = 2;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                        }*/
                        //Before the RPG engine gets angry and tells you "argument out of range" exception, insure that this space can exist.
                        if (path[spaceNumber] == null)
                        {

                        }
                        else if (currentUnitPosition.getNorth() == path[spaceNumber])
                        {
                            r = 0;
                        }
                        else if (currentUnitPosition.getEast() == path[spaceNumber])
                        {
                            r = 1;
                        }
                        else if (currentUnitPosition.getSouth() == path[spaceNumber])
                        {
                            r = 2;
                        }
                        else if (currentUnitPosition.getWest() == path[spaceNumber])
                        {
                            r = 3;
                        }
                        //Ignore cases where the same space is returned again.
                        if (currentUnitPosition == path[spaceNumber])
                        {
                            plottedMovements.Add(currentUnitPosition);
                        }
                        else
                        {
                            plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                        }
                        currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                        if (spaceNumber < path.Count() - 1)
                        {
                            spaceNumber++;
                        }
                        numberOfMovementAttempts++;
                        if (currentUnit.getMovementPointsToSpend() <= 0 || plottedMovements.Count()==path.Count()/*numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() + 1*/)
                        {
                            //int ticks = 0; //Obsolete
                            //Make the system wait for 1 seconds before deciding its actions.
                            foreach (Space w in plottedMovements)
                            {
                                if (w != currentUnit.getHost())
                                {
                                    Console.WriteLine("-----");
                                    Game_Gnome.moveToSpace(currentUnit, Map, w, fogOfWarEnabled, false);
                                    Map.printMap(currentControlledPlayer, currentUnit);
                                }
                                if (Game_Gnome.getUnitTrapped() == true)
                                {
                                    Game_Gnome.setUnitTrapped(false);
                                    numberOfMovementAttempts = 0;
                                    rabbitHasMoved = true;
                                    pointsGranted = false;
                                    numberOfMovementAttempts = 0;
                                    break;
                                }
                                System.Threading.Thread.Sleep(100);
                            }
                            numberOfMovementAttempts = 0;
                            break;
                        }
                        if (numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend() + 1)
                        {
                            numberOfMovementAttempts = 0;
                            //break;
                        }
                    }
                    break;
                }
                else
                {
                    targetAbility = decisionsByHighestWeight_Abilities[0];
                    targetUnit = decisionsByHighestWeight_HitList[0];
                    targetSpace = targetUnit.getHost();
                    targetSpaceToAttackFrom = decisionsByHighestWeight_Spaces[0];
                    //targetSpacePath = decisionsByHighestWeight_Spaces_Path[0];
                    //targetSpaceToAttackFrom = targetSpacePath[decisionsByHighestWeight_Spaces_Path.Count() - 1];
                    List<int> plottedDirections = new List<int>();
                    List<Space> plottedMovements = new List<Space>();
                    Space currentUnitPosition = currentUnit.getHost();
                    Random rand = new Random();
                    int XorY = 0;
                    //Begin moving towards the target space to attack from.
                    int spaceNumber = 0;
                    List<Space> path = new List<Space>();
                    List<Space> evaluatedPath = Game_Gnome.pathfinder2(Map, currentUnit, currentUnit.getHost(), targetSpaceToAttackFrom, currentUnit.getPlayer(), currentUnit.getMovementPointsToSpend(), false);
                    foreach (Space o in evaluatedPath)
                    {
                        Console.WriteLine("Path space ID: " + o.getID());
                        path.Add(o);
                    }
                    Console.WriteLine("Goal space ID: " + targetSpaceToAttackFrom.getID());
                    //Console.WriteLine("Alternate path found. Number of spaces in path: " + Game_Gnome.pathfinder2(Map, currentUnit, currentUnit.getHost(), targetSpaceToAttackFrom, currentUnit.getPlayer(), currentUnit.getMovementPointsToSpend(), false).Count());
                    while (currentUnit.getHost() != targetSpaceToAttackFrom)
                    {
                        //Immediately break the loop if there's nothing in path.
                        if (path.Count() == 0)
                        {
                            break;
                        }
                        XorY = rand.Next(0, 1);
                        //Evaluated space is
                        Space evaluatedSpace = currentUnitPosition;
                        //bool otherDirectionChecked = false;
                        //Try to plot movements that get the unit closer to its target space.
                        int numberOfXFromCurrentPosition = Map.getXFromSpace(currentUnitPosition, targetSpaceToAttackFrom);
                        int numberOfYFromCurrentPosition = Map.getYFromSpace(currentUnitPosition, targetSpaceToAttackFrom);
                        //int xDirection = numberOfXFromCurrentPosition / numberOfXFromCurrentPosition;
                        //int yDirection = numberOfYFromCurrentPosition / numberOfYFromCurrentPosition;
                        /*if (numberOfXFromCurrentPosition == 0)
                        {
                            XorY = 1;
                        }
                        if (numberOfYFromCurrentPosition == 0)
                        {
                            XorY = 0;
                        }
                        if (XorY == 0)
                        {
                            if (numberOfXFromCurrentPosition < 0)
                            {
                                evaluatedSpace = currentUnitPosition.getWest();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID())==true) && evaluatedSpace.getExist()==true)
                                {
                                    r = 3;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                            else if (numberOfXFromCurrentPosition > 0)
                            {
                                evaluatedSpace = currentUnitPosition.getEast();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && evaluatedSpace.getExist() == true)
                                {
                                    r = 1;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                        }
                        if (XorY == 1)
                        {
                            if (numberOfYFromCurrentPosition < 0)
                            {
                                evaluatedSpace = currentUnitPosition.getNorth();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && evaluatedSpace.getExist() == true)
                                {
                                    r = 0;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                            else if (numberOfYFromCurrentPosition > 0)
                            {
                                evaluatedSpace = currentUnitPosition.getSouth();
                                if (((evaluatedSpace.getGuest() == null) || evaluatedSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && evaluatedSpace.getExist() == true)
                                {
                                    r = 2;
                                    plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                                    currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                                }
                            }
                        }*/
                        //Before the RPG engine gets angry and tells you "argument out of range" exception, insure that this space can exist.
                        if (path[spaceNumber] == null)
                        {

                        }
                        else if (currentUnitPosition.getNorth() == path[spaceNumber])
                        {
                            r = 0;
                        }
                        else if (currentUnitPosition.getEast() == path[spaceNumber])
                        {
                            r = 1;
                        }
                        else if (currentUnitPosition.getSouth() == path[spaceNumber])
                        {
                            r = 2;
                        }
                        else if (currentUnitPosition.getWest() == path[spaceNumber])
                        {
                            r = 3;
                        }
                        //Ignore cases where the same space is returned again.
                        if (currentUnitPosition == path[spaceNumber])
                        {
                            plottedMovements.Add(currentUnitPosition);
                        }
                        else
                        {
                            plottedMovements.Add(Game_Gnome.attemptMovement(currentUnit, currentUnitPosition, Map, r, fogOfWarEnabled));
                        }
                        currentUnitPosition = plottedMovements[plottedMovements.Count() - 1];
                        if (spaceNumber < path.Count()-1)
                        {
                            spaceNumber++;
                        }
                        numberOfMovementAttempts++;
                        if (currentUnit.getMovementPointsToSpend() <= 0 || plottedMovements.Count()>=path.Count()/*numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend()+1*/)
                        {
                            //int ticks = 0; //Obsolete
                            //Make the system wait for 1 seconds before deciding its actions.
                            foreach (Space w in plottedMovements)
                            {
                                if (w != currentUnit.getHost())
                                {
                                    Console.WriteLine("-----");
                                    Game_Gnome.moveToSpace(currentUnit, Map, w, fogOfWarEnabled, false);
                                    Map.printMap(currentControlledPlayer, currentUnit);
                                }
                                if (Game_Gnome.getUnitTrapped() == true)
                                {
                                    Game_Gnome.setUnitTrapped(false);
                                    numberOfMovementAttempts = 0;
                                    rabbitHasMoved = true;
                                    pointsGranted = false;
                                    numberOfMovementAttempts = 0;
                                    break;
                                }
                                System.Threading.Thread.Sleep(100);
                            }
                            numberOfMovementAttempts = 0;
                            break;
                        }
                        if (numberOfMovementAttempts >= currentUnit.getMovementPointsToSpend()+1)
                        {
                            numberOfMovementAttempts = 0;
                            //break;
                        }
                    }
                    for (int h = 0; h <= targetAbility.getRadius(); h++)
                        foreach (Space w in Game_Gnome.Range(Map, targetSpace, h))
                        {
                            {
                                aoeSpaces.Add(w);
                            }
                        }
                }
                if (currentUnit.getHost() == targetSpaceToAttackFrom)
                {
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
                    if (targetAbility.getName() == "Deja Vu")
                    {
                        for (int u = 0; u < previousUnitList.Count(); u++)
                        {
                            if (previousUnitList[u] == null) { }
                            else if (previousUnitList[u] == targetSpace.getGuest())
                            {
                                referredSpace = previousSpaceList[u];
                            }
                        }
                    }
                    Game_Gnome.attack(currentUnit, Map, targetAbility, targetSpace, aoeSpaces, true, referredSpace);
                    foreach (Player p in players)
                    {
                        p.setSpaceOfInterest(targetSpace);
                    }
                }
                attackAttempts++;
            }
        }
    }
}
