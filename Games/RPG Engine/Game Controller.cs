using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine
{
    class Game_Controller
    {
        internal List<Field> loadMaps()
        {
            List<Field> maps = new List<Field>();
            string[] lines = System.IO.File.ReadAllLines(@"\Scripts\Maps\Maps.txt");
            int i = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split();
                maps.Add(new Field(words[0], Convert.ToInt32(words[1]), Convert.ToInt32(words[2])));
            }
            
            return maps;
        }
        bool unitTrapped = false;
        internal void setUnitTrapped(bool x)
        {
            this.unitTrapped = x;
        }
        internal bool getUnitTrapped()
        {
            return this.unitTrapped;
        }
        public Game_Controller(){}
        internal void Move(Unit unit, Field Test_Map, int Direction, bool fogEnabled)
        {
            Space target;
            if(Direction == 0) { target = unit.getHost().getNorth();
            }
            else if (Direction == 1) { target = unit.getHost().getEast();
            }
            else if (Direction == 2) { target = unit.getHost().getSouth();
            }
            else if (Direction == 3) { target = unit.getHost().getWest();
            }
            else
            {
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1);
                return;
            }
            try
            {
                if (target.getExist() == false)
                {
                    Console.WriteLine("Sorry that Space is full");
                }
                else if (target.getGuest() == null)
                {
                    unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() - 1);
                    unit.Move(target);
                    //return false;
                }
                else
                {
                    if (fogEnabled == false)
                    {
                        Console.WriteLine("Sorry that Space is full");
                        //return false;
                    }
                    else if (fogEnabled == true)
                    {
                        if (target.getConcealed(unit.getPlayer().getPlayerID()) == false)
                        {
                            Console.WriteLine("Sorry that Space is full");
                            //return false;
                        }
                        else
                        {
                            Console.WriteLine("TRAP!!");
                            unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() - unit.getMovementPointsToSpend());
                            unitTrapped = true;
                            //return true;
                        }
                    }
                    //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Obsolete
                }
            }
            catch
            {
                //return false;
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Bug. Causes net gain of movement points.
                Console.WriteLine("Error.");
            }
        }
        internal void moveToSpace(Unit unit, Field map, Space targetSpace, bool fogEnabled, bool isTeleport)
        {
            Space target = targetSpace;
            try
            {
                if (target.getGuest() == null)
                {
                    unit.Move(target);
                    //return false;
                }
                else if (fogEnabled == false || isTeleport==true)
                {
                    Console.WriteLine("This space is full.");
                    //return false;
                }
                else if (fogEnabled == true)
                {
                    Console.WriteLine("TRAP!!");
                    unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() - unit.getMovementPointsToSpend());
                    unitTrapped = true;
                    //return true;
                }
            }
            catch
            {
                //return false;
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Bug. Causes net gain of movement points.
            }
        }
        internal Space attemptMovement(Unit unit, Space startSpace, Field map, int Direction, bool fogEnabled)
        {
            Space currSpace = startSpace;
            Space target = currSpace;
            if (Direction == 0)
            {
                target = currSpace.getNorth();
            }
            else if (Direction == 1)
            {
                target = currSpace.getEast();
            }
            else if (Direction == 2)
            {
                target = currSpace.getSouth();
            }
            else if (Direction == 3)
            {
                target = currSpace.getWest();
            }
            else
            {
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1);
                //return currSpace;
            }
            try
            {
                if (((target.getGuest() == null && fogEnabled == false) || (fogEnabled == true && target.getConcealed(unit.getPlayer().getPlayerID())==true) || (fogEnabled == true && target.getConcealed(unit.getPlayer().getPlayerID())==false && target.getGuest() == null )) && target.getExist()==true)
                {
                    unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() - 1);
                    return target;
                    //return false;
                }
                else
                {
                    if (fogEnabled == false && target.getExist()==true || (fogEnabled == true && target.getConcealed(unit.getPlayer().getPlayerID()) == false && target.getGuest() != null) || target.getExist()==false)
                    {
                        //Console.WriteLine("Sorry that Space is full");
                        target = currSpace;
                        return target;
                        //return false;
                    }
                    //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Obsolete
                }
            }
            catch
            {
                target = currSpace;
                return target;
                //return false;
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Bug. Causes net gain of movement points.
            }
            target = currSpace;
            return target;
        }
        internal int attemptMovementDirection(Unit unit, Field map, int Direction, bool fogEnabled)
        {
            Space currSpace = unit.getHost();
            Space target = currSpace;
            int targetDirection = 0;
            if (Direction == 0)
            {
                target = currSpace.getNorth();
                targetDirection = 0;
            }
            else if (Direction == 1)
            {
                target = currSpace.getEast();
                targetDirection = 1;
            }
            else if (Direction == 2)
            {
                target = currSpace.getSouth();
                targetDirection = 2;
            }
            else if (Direction == 3)
            {
                target = currSpace.getWest();
                targetDirection = 3;
            }
            else
            {
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1);
                //return currSpace;
            }
            try
            {
                if (target.getExist() == false)
                {
                    Console.WriteLine("Sorry that Space is full");
                    targetDirection = 0;
                    return targetDirection;
                }
                else if (target.getGuest() == null || (fogEnabled == true && target.getConcealed(unit.getPlayer().getPlayerID()) == true))
                {
                    unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() - 1);
                    return targetDirection;
                    //return false;
                }
                else
                {
                    if (fogEnabled == false || (fogEnabled == true && currSpace.getConcealed(unit.getPlayer().getPlayerID()) == false))
                    {
                        //Console.WriteLine("Sorry that Space is full");
                        targetDirection = 0;
                        return targetDirection;
                        //return false;
                    }
                    //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Obsolete
                }
            }
            catch
            {
                targetDirection = 0;
                currSpace = unit.getHost();
                return 0;
                //return false;
                //unit.setMovementPointsToSpend(unit.getMovementPointsToSpend() + 1); //Bug. Causes net gain of movement points.
            }
            targetDirection = 0;
            currSpace = unit.getHost();
            return 0;
        }

        internal void attack(Unit unit, Field Test_Map, Ability list,/* int Direction*/ Space targetSpace, List<Space> aoeSpaces, bool experienceGainOn, Space referredSpace)
        {
            Space target = null;
            /*if (Direction == 0) { target = unit.getHost().getNorth(); }
            else if (Direction == 1) { target = unit.getHost().getEast(); }
            else if (Direction == 2) { target = unit.getHost().getSouth(); }
            else if (Direction == 3) { target = unit.getHost().getWest(); }*/
            target = targetSpace;
            if (target.getGuest() == null && list.getRadius() == 0)
                {
                    Console.WriteLine("You sliced air. It's Super Effective.");
                    //unit.setAttackPoints(unit.getAttackPoints() + 1); //Bug. Causes net gain of attack points.
                }
                else if (unit.getPlayer().getRelationshipToPlayer(target.getGuest().getPlayer().getPlayerID()) == "self" && list.getRadius() == 0)
                {
                    Console.WriteLine("You are not allowed to attack your own units!");
                }
            else if (unit.getPlayer().getRelationshipToPlayer(target.getGuest().getPlayer().getPlayerID()) == "ally" && list.getRadius() == 0)
                {
                    Console.WriteLine("You can't attack allied units!");
                }
            else if (unit.getPlayer().getRelationshipToPlayer(target.getGuest().getPlayer().getPlayerID()) == "neutral" && list.getRadius() == 0)
                {
                    Console.WriteLine("You can't attack neutral units!");
                }
            else
            {
                double oldHealth = target.getGuest().getCurrentHP();
                double newHealth = target.getGuest().getCurrentHP();
                double healthPercentage;
                unit.setAttackPoints(unit.getAttackPoints() - 1);
                unit.setCurrentMP(unit.getCurrentMP() - list.getMPCost());
                int listDmg = list.getDmg();
                int dmgType = 0;
                if (list.getAbilityType() == 0 && list.doesAbilityDealDamage() == true && list.doesAbilityDoNegDamage() == false)
                {
                    listDmg = listDmg + Convert.ToInt32(Math.Floor(Convert.ToDouble((unit.getModifiedStrength()) / 2)));
                }
                else if (list.getAbilityType() == 0 && list.doesAbilityDealDamage() == true && list.doesAbilityDoNegDamage() == true)
                {
                    listDmg = -listDmg - Convert.ToInt32(Math.Floor(Convert.ToDouble((unit.getModifiedStrength()) / 2)));
                }
                else if (list.getAbilityType() == 0 && list.doesAbilityDealDamage() == false)
                {
                    listDmg = 0;
                }
                if (list.getAbilityType() == 1 && list.doesAbilityDealDamage() == true && list.doesAbilityDoNegDamage() == false)
                {
                    listDmg = listDmg + Convert.ToInt32(Math.Floor(Convert.ToDouble((unit.getModifiedLC()) / 2)));
                }
                else if (list.getAbilityType() == 1 && list.doesAbilityDealDamage() == true && list.doesAbilityDoNegDamage() == true)
                {
                    listDmg = -listDmg - Convert.ToInt32(Math.Floor(Convert.ToDouble((unit.getModifiedLC()) / 2)));
                }
                else if (list.getAbilityType() == 1 && list.doesAbilityDealDamage() == false)
                {
                    listDmg = 0;
                }
                foreach (Space q in aoeSpaces)
                {
                    if (q.getGuest() == null)
                    {
                        List<TileEffect> tileEffectsList = new List<TileEffect>();
                        foreach (TileEffect w in list.getTileEffectsPool())
                        {
                            TileEffect newEffectInstances = w;
                            newEffectInstances = new TileEffect(w.getEffectName(), w.getDuration(), null, w.getMagnitude());
                            foreach (string descriptor in w.getAllDescriptors())
                            {
                                newEffectInstances.addDescriptor(descriptor);
                            }
                            newEffectInstances.setHost(q);
                            unit.addTileEffect(newEffectInstances);
                            tileEffectsList.Add(newEffectInstances);
                        }
                        if (tileEffectsList.Count == 0) { }
                        else
                        {
                            q.applyTileEffectsFromList(tileEffectsList);
                        }
                    }
                    else
                    {
                        q.getGuest().dealDamage(listDmg);
                        
                        List<Effects> effectsList = new List<Effects>();
                        List<TileEffect> tileEffectsList = new List<TileEffect>();
                        foreach (Effects w in list.getEffectsPool())
                        {
                            Effects newEffectInstance = w;
                            newEffectInstance = new Effects(w.getEffectName(), w.getDuration(), null, w.getMagnitude());
                            foreach (string descriptor in w.getAllDescriptors())
                            {
                                newEffectInstance.addDescriptor(descriptor);
                            }
                            effectsList.Add(newEffectInstance);
                        }
                        foreach (TileEffect w in list.getTileEffectsPool())
                        {
                            TileEffect newEffectInstances = w;
                            newEffectInstances = new TileEffect(w.getEffectName(), w.getDuration(), null, w.getMagnitude());
                            foreach (string descriptor in w.getAllDescriptors())
                            {
                                newEffectInstances.addDescriptor(descriptor);
                            }
                            unit.addTileEffect(newEffectInstances);
                            newEffectInstances.setHost(q);
                            tileEffectsList.Add(newEffectInstances);
                        }
                        if (effectsList.Count == 0) { }
                        else
                        {
                            q.getGuest().applyEffectsFromList(effectsList);
                        }
                        if (tileEffectsList.Count == 0) { }
                        else
                        {
                            q.applyTileEffectsFromList(tileEffectsList);
                        }
                        newHealth = q.getGuest().getCurrentHP();
                        healthPercentage = (oldHealth - newHealth) / Convert.ToDouble(q.getGuest().getModifiedHP());
                        //healthPercentage = (oldHealth-newHealth) / 2;
                        if (experienceGainOn == true && list.doesAbilityDealDamage()==true)
                        {
                            //Console.WriteLine("Health Percentage: " + healthPercentage);
                            unit.setExperience(unit.getExperience() + Convert.ToInt32(Math.Floor(Convert.ToDouble(Convert.ToDouble(q.getGuest().getModifiedExperienceYield()) * healthPercentage))));
                            Console.WriteLine("You gained " + Convert.ToInt32(Math.Floor(Convert.ToDouble(Convert.ToDouble(q.getGuest().getModifiedExperienceYield()) * healthPercentage))) + " experience from the attack.");
                        }
                        if (list.doesAbilityDealDamage() == true)
                        {
                            Console.WriteLine(q.getGuest().getName() + " takes " + listDmg + " damage from " + unit.getName() + "'s " + list.getName() + ".");
                        }
                        if (list.getName() == "Deja Vu")
                        {
                            Console.WriteLine("Teleporting to " + referredSpace.getX() + "/" + referredSpace.getY());
                            moveToSpace(q.getGuest(), Test_Map, referredSpace, false, true);
                        }
                    }
                }
            }
        }
        internal List<Space> Range(Field map, Space space, int range)
        {
            List<Space> inRange = new List<Space>();
            int startX = space.getX();
            int curY = space.getY() - range;
            int xFlux = 0;
            int currRange = -range;
            while (currRange <= range)
            {
                if (curY >= 0 && curY <= (map.getHeight() - 1))
                {
                    if (xFlux == 0)
                    {
                        if (map.getSpace(curY * map.getHeight() + (startX)) == null)
                        {

                        }
                        else
                        {
                            inRange.Add(map.getSpace(curY * map.getHeight() + (startX)));
                        }
                    }
                    else
                    {
                        if (startX - xFlux >= 0 && startX + xFlux <= (map.getWidth() - 1))
                        {
                            if (curY * map.getHeight() + (startX + xFlux) < 0)
                            {

                            }
                            else
                            {
                                inRange.Add(map.getSpace(curY * map.getHeight() + (startX + xFlux)));
                            }
                        }
                        if (startX - xFlux >= 0 && startX - xFlux <= (map.getWidth()- 1))
                        {
                            if (curY * map.getHeight() + (startX - xFlux) < 0)
                            {

                            }
                            else
                            {
                                inRange.Add(map.getSpace(curY * map.getHeight() + (startX - xFlux)));
                            }
                        }
                    }
                }
                if (currRange < 0)
                {
                    xFlux = xFlux - 1;
                }
                else
                {
                    xFlux++;
                }
                curY++;
                currRange++;
            }
            return inRange;
        }
        //Similar to the range method, movementRange returns all spaces that a unit can move onto in their current turn, based
        //on the number of movement points they have. This is used to highlight spaces where the player can move, or for AI,
        //as part of the threatened range.
        //WARNING: The movement range as of the time this comment was added (11/15/2014) causes significant lag when used, due
        //to all the recursions it has to do.
        //Refrain from using until a fix is found.
        //Details: For every movement point that a unit has, this method has to do 4 recursions of itself based on remaining
        //number of unit points.
        //For really high numbers of movement points, like 7, this method would end up doing 4 recursions, and each of those
        //4 recursions do 4 more recursions, and each of THOSE 4 recursions do...I think you can see where this is going.
        internal List<Space> movementRange(Field map, Player currentPlayer, int movementPoints, Space currentSpace)
        {
            Space startingPosition = currentSpace;
            Space northPosition = startingPosition.getNorth();
            Space southPosition = startingPosition.getSouth();
            Space eastPosition = startingPosition.getEast();
            Space westPosition = startingPosition.getWest();
            bool northAnalyzed = false;
            bool southAnalyzed = false;
            bool eastAnalyzed = false;
            bool westAnalyzed = false;
            try
            {
                northAnalyzed = northPosition.getAnalyzed();
            }
            catch
            {
                northAnalyzed = true;
            }
            try
            {
                southAnalyzed = southPosition.getAnalyzed();
            }
            catch
            {
                southAnalyzed = true;
            }
            try
            {
                eastAnalyzed = eastPosition.getAnalyzed();
            }
            catch
            {
                eastAnalyzed = true;
            }
            try
            {
                westAnalyzed = westPosition.getAnalyzed();
            }
            catch
            {
                westAnalyzed = true;
            }
            bool northImpassable = false;
            bool southImpassable = false;
            bool eastImpassable = false;
            bool westImpassable = false;
            int currentMovementPoints = movementPoints;
            List<Space> candidateSpaces = new List<Space>();
            List<bool> vacuumCleaner = new List<bool>();
            List<Space> cleanCandidateSpaces = new List<Space>();
            candidateSpaces.Add(startingPosition);
            //startingPosition.setAnalyzed(true);
            try
            {
                if (northPosition.getExist() != true) { northImpassable = true; }
                if (northPosition.getGuest() != null && northPosition.getConcealed(currentPlayer.getPlayerID()) == false) { northImpassable = true; }
            }
            catch
            {
                northImpassable = true;
            }
            try
            {
                if (southPosition.getExist() != true) { southImpassable = true; }
                if (southPosition.getGuest() != null && southPosition.getConcealed(currentPlayer.getPlayerID()) == false) { southImpassable = true; }
            }
            catch
            {
                southImpassable = true;
            }
            try
            {
                if (eastPosition.getExist() != true) { eastImpassable = true; }
                if (eastPosition.getGuest() != null && eastPosition.getConcealed(currentPlayer.getPlayerID()) == false) { eastImpassable = true; }
            }
            catch
            {
                eastImpassable = true;
            }
            try
            {
                if (westPosition.getExist() != true) { westImpassable = true; }
                if (westPosition.getGuest() != null && westPosition.getConcealed(currentPlayer.getPlayerID()) == false) { westImpassable = true; }
            }
            catch
            {
                westImpassable = true;
            }
            /*foreach (Space w in candidateSpaces)
            {
                w.setAnalyzed(true);
            }*/
            //if (northImpassable == false) { northPosition.setAnalyzed(true); }
            //if (southImpassable == false) { southPosition.setAnalyzed(true); }
            //if (eastImpassable == false) { eastPosition.setAnalyzed(true); }
            //if (westImpassable == false) { westPosition.setAnalyzed(true); }
            //startingPosition.setAnalyzed(true);
            currentMovementPoints--;
            if (currentMovementPoints < 0 /* && startingPosition.getAnalyzed()==true*/)
            {
                //if (northImpassable == false) { northPosition.setAnalyzed(true); }
                //if (southImpassable == false) { southPosition.setAnalyzed(true); }
                //if (eastImpassable == false) { eastPosition.setAnalyzed(true); }
                //if (westImpassable == false) { westPosition.setAnalyzed(true); }
                //return candidateSpaces;
            }
            else
            {
                //startingPosition.setAnalyzed(true);
                //currentMovementPoints--;
                if (northImpassable == false/* && northPosition.getAnalyzed()==false*/)
                {
                    //candidateSpaces.Add(northPosition);
                    //northPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, northPosition));
                    foreach (Space w in movementRange(map, currentPlayer, currentMovementPoints, northPosition))
                    {
                        //Console.WriteLine("Adding " + w.getID());
                        candidateSpaces.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //northPosition.setAnalyzed(true);
                    //candidateSpaces.Add(northPosition);
                }
                if (southImpassable == false/* && southPosition.getAnalyzed() == false*/)
                {
                    //candidateSpaces.Add(southPosition);
                    //southPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, southPosition));
                    foreach (Space w in movementRange(map, currentPlayer, currentMovementPoints, southPosition))
                    {
                        //Console.WriteLine("Adding " + w.getID());
                        candidateSpaces.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //southPosition.setAnalyzed(true);
                    //candidateSpaces.Add(southPosition);
                }
                if (eastImpassable == false/* && eastPosition.getAnalyzed() == false*/)
                {
                    //candidateSpaces.Add(eastPosition);
                    //eastPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, eastPosition));
                    foreach (Space w in movementRange(map, currentPlayer, currentMovementPoints, eastPosition))
                    {
                        //Console.WriteLine("Adding " + w.getID());
                        candidateSpaces.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //eastPosition.setAnalyzed(true);
                    //candidateSpaces.Add(eastPosition);
                }
                if (westImpassable == false/* && westPosition.getAnalyzed() == false*/)
                {
                    //candidateSpaces.Add(westPosition);
                    //westPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, westPosition));
                    foreach (Space w in movementRange(map, currentPlayer, currentMovementPoints, westPosition))
                    {
                        //Console.WriteLine("Adding " + w.getID());
                        candidateSpaces.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //westPosition.setAnalyzed(true);
                    //candidateSpaces.Add(westPosition);
                }
            }
            //Do space clean-up. Get rid of all duplicate spaces that arise in the list before returning candidate spaces.
            foreach (Space w in candidateSpaces)
            {
                //The vacuum cleaner is my way of asking "is this space qualified to be added?"
                vacuumCleaner.Add(true);
            }
            for (int i = 0; i < candidateSpaces.Count(); i++)
            {
                for (int j = 0; j < candidateSpaces.Count(); j++)
                {
                    if (i!=j && candidateSpaces[i] == candidateSpaces[j] && vacuumCleaner[j]==true && vacuumCleaner[i]==true)
                    {
                        //Only one space, not its duplicates, can be included. We're going to filter those duplicate spaces.
                        vacuumCleaner[j] = false;
                    }
                }
            }
            for (int i = 0; i < candidateSpaces.Count(); i++)
            {
                if (vacuumCleaner[i] == true)
                {
                    cleanCandidateSpaces.Add(candidateSpaces[i]);
                }
            }
            /*foreach (Space w in candidateSpaces)
            {
                w.setAnalyzed(true);
            }*/
            //if (northImpassable == false) { northPosition.setAnalyzed(true); }
            //if (southImpassable == false) { southPosition.setAnalyzed(true); }
            //if (eastImpassable == false) { eastPosition.setAnalyzed(true); }
            //if (westImpassable == false) { westPosition.setAnalyzed(true); }
            //startingPosition.setAnalyzed(true);
            //Console.WriteLine("--------------");
            foreach (Space w in cleanCandidateSpaces)
            {
                //Console.WriteLine("Returning space " + w.getID() + ", coords (" + w.getX() + ", " + w.getY() + "), displacement x: " + map.getXFromSpace(startingPosition, w) + ", y: " + map.getYFromSpace(startingPosition, w));
            }
            if (startingPosition.getGuest() == null)
            {

            }
            else
            {
                //Console.WriteLine("Note: The surface of the recursions has been reached; this recursion contains " + startingPosition.getGuest().getName());
            }
            //Console.WriteLine("Total number of spaces returned = " + cleanCandidateSpaces.Count());
            return cleanCandidateSpaces;
        }
        internal List<Space> movementRangeNEW(Field map, Player currentPlayer, int movementPoints, Space currentSpace)
        {
            List<Space> candidateSpaces = new List<Space>();
            candidateSpaces.Add(currentSpace);
            List<Space> oldCandidateSpaces = new List<Space>();
            oldCandidateSpaces.Add(currentSpace);
            int newMovementPoints = movementPoints;
            while (newMovementPoints > 0)
            {
                for (int w = 0; w < oldCandidateSpaces.Count(); w++ )
                {
                    //if (candidateSpaces[w].getAnalyzed() == false)
                    {
                        Space northPosition = oldCandidateSpaces[w].getNorth();
                        Space southPosition = oldCandidateSpaces[w].getSouth();
                        Space eastPosition = oldCandidateSpaces[w].getEast();
                        Space westPosition = oldCandidateSpaces[w].getWest();
                        bool northImpassable = false;
                        bool southImpassable = false;
                        bool eastImpassable = false;
                        bool westImpassable = false;
                        try
                        {
                            if (northPosition.getExist() != true) { northImpassable = true; }
                            if (northPosition.getGuest() != null && northPosition.getConcealed(currentPlayer.getPlayerID()) == false) { northImpassable = true; }
                        }
                        catch
                        {
                            northImpassable = true;
                        }
                        try
                        {
                            if (southPosition.getExist() != true) { southImpassable = true; }
                            if (southPosition.getGuest() != null && southPosition.getConcealed(currentPlayer.getPlayerID()) == false) { southImpassable = true; }
                        }
                        catch
                        {
                            southImpassable = true;
                        }
                        try
                        {
                            if (eastPosition.getExist() != true) { eastImpassable = true; }
                            if (eastPosition.getGuest() != null && eastPosition.getConcealed(currentPlayer.getPlayerID()) == false) { eastImpassable = true; }
                        }
                        catch
                        {
                            eastImpassable = true;
                        }
                        try
                        {
                            if (westPosition.getExist() != true) { westImpassable = true; }
                            if (westPosition.getGuest() != null && westPosition.getConcealed(currentPlayer.getPlayerID()) == false) { westImpassable = true; }
                        }
                        catch
                        {
                            westImpassable = true;
                        }
                        if (northImpassable == false) { candidateSpaces.Add(northPosition); }
                        if (southImpassable == false) { candidateSpaces.Add(southPosition); }
                        if (eastImpassable == false) { candidateSpaces.Add(eastPosition); }
                        if (westImpassable == false) { candidateSpaces.Add(westPosition); }
                        oldCandidateSpaces[w].setAnalyzed(true);
                    }
                }
                //Update the old candidate spaces list.
                foreach (Space w in candidateSpaces)
                {
                    oldCandidateSpaces.Add(w);
                }
                //Decrement our movement points number for the next process.
                newMovementPoints--;
            }
            foreach (Space w in candidateSpaces)
            {
                w.setAnalyzed(false);
            }
            return candidateSpaces;
        }
        //This overload of movementRange will process all possible movements in addition to the paths used to take them.
        internal List<List<Space>> movementRange(Field map, Player currentPlayer, int movementPoints, Space currentSpace, List<List<Space>> path)
        {
            Space startingPosition = currentSpace;
            Space northPosition = startingPosition.getNorth();
            Space southPosition = startingPosition.getSouth();
            Space eastPosition = startingPosition.getEast();
            Space westPosition = startingPosition.getWest();
            bool northAnalyzed = false;
            bool southAnalyzed = false;
            bool eastAnalyzed = false;
            bool westAnalyzed = false;
            List<Space> truePath = new List<Space>();
            List<Space> path1 = new List<Space>();
            List<Space> path2 = new List<Space>();
            List<Space> path3 = new List<Space>();
            List<Space> path4 = new List<Space>();
            List<List<Space>> combinedPaths = new List<List<Space>>();
            try
            {
                northAnalyzed = northPosition.getAnalyzed();
            }
            catch
            {
                northAnalyzed = true;
            }
            try
            {
                southAnalyzed = southPosition.getAnalyzed();
            }
            catch
            {
                southAnalyzed = true;
            }
            try
            {
                eastAnalyzed = eastPosition.getAnalyzed();
            }
            catch
            {
                eastAnalyzed = true;
            }
            try
            {
                westAnalyzed = westPosition.getAnalyzed();
            }
            catch
            {
                westAnalyzed = true;
            }
            bool northImpassable = false;
            bool southImpassable = false;
            bool eastImpassable = false;
            bool westImpassable = false;
            int currentMovementPoints = movementPoints;
            List<Space> candidateSpaces = new List<Space>();
            List<bool> vacuumCleaner = new List<bool>();
            List<Space> cleanCandidateSpaces = new List<Space>();
            candidateSpaces.Add(startingPosition);
            truePath.Add(startingPosition);
            //path.Add(truePath);
            //startingPosition.setAnalyzed(true);
            try
            {
                if (northPosition.getExist() != true) { northImpassable = true; }
                if (northPosition.getGuest() != null && northPosition.getConcealed(currentPlayer.getPlayerID()) == false) { northImpassable = true; }
            }
            catch
            {
                northImpassable = true;
            }
            try
            {
                if (southPosition.getExist() != true) { southImpassable = true; }
                if (southPosition.getGuest() != null && southPosition.getConcealed(currentPlayer.getPlayerID()) == false) { southImpassable = true; }
            }
            catch
            {
                southImpassable = true;
            }
            try
            {
                if (eastPosition.getExist() != true) { eastImpassable = true; }
                if (eastPosition.getGuest() != null && eastPosition.getConcealed(currentPlayer.getPlayerID()) == false) { eastImpassable = true; }
            }
            catch
            {
                eastImpassable = true;
            }
            try
            {
                if (westPosition.getExist() != true) { westImpassable = true; }
                if (westPosition.getGuest() != null && westPosition.getConcealed(currentPlayer.getPlayerID()) == false) { westImpassable = true; }
            }
            catch
            {
                westImpassable = true;
            }
            /*foreach (Space w in candidateSpaces)
            {
                w.setAnalyzed(true);
            }*/
            //if (northImpassable == false) { northPosition.setAnalyzed(true); }
            //if (southImpassable == false) { southPosition.setAnalyzed(true); }
            //if (eastImpassable == false) { eastPosition.setAnalyzed(true); }
            //if (westImpassable == false) { westPosition.setAnalyzed(true); }
            //startingPosition.setAnalyzed(true);
            if (currentMovementPoints <= 0 /* && startingPosition.getAnalyzed()==true*/)
            {
                //if (northImpassable == false) { northPosition.setAnalyzed(true); }
                //if (southImpassable == false) { southPosition.setAnalyzed(true); }
                //if (eastImpassable == false) { eastPosition.setAnalyzed(true); }
                //if (westImpassable == false) { westPosition.setAnalyzed(true); }
                combinedPaths.Add(truePath);
            }
            else
            {
                //startingPosition.setAnalyzed(true);
                currentMovementPoints--;
                if (northImpassable == false/* && northPosition.getAnalyzed()==false*/)
                {
                    //truePath = new List<Space>();
                    //truePath.Add(startingPosition);
                    //candidateSpaces.Add(northPosition);
                    //northPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, northPosition));
                    foreach (List<Space> w in movementRange(map, currentPlayer, currentMovementPoints, northPosition, path))
                    {
                        w.Add(startingPosition);
                        foreach (Space l in movementRange(map, currentPlayer, currentMovementPoints, northPosition))
                        {
                            w.Add(l);
                        }
                        combinedPaths.Add(w);
                        //Console.WriteLine("Adding " + w.getID());
                        //candidateSpaces.Add(w);
                        //truePath.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //path.Add(truePath);
                    //northPosition.setAnalyzed(true);
                    //candidateSpaces.Add(northPosition);
                }
                if (southImpassable == false/* && southPosition.getAnalyzed() == false*/)
                {
                    //truePath = new List<Space>();
                    //truePath.Add(startingPosition);
                    //candidateSpaces.Add(southPosition);
                    //southPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, southPosition));
                    foreach (List<Space> w in movementRange(map, currentPlayer, currentMovementPoints, southPosition, path))
                    {
                        w.Add(startingPosition);
                        foreach (Space l in movementRange(map, currentPlayer, currentMovementPoints, southPosition))
                        {
                            w.Add(l);
                        }
                        combinedPaths.Add(w);
                        //Console.WriteLine("Adding " + w.getID());
                        //candidateSpaces.Add(w);
                        //truePath.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //path.Add(truePath);
                    //southPosition.setAnalyzed(true);
                    //candidateSpaces.Add(southPosition);
                }
                if (eastImpassable == false/* && eastPosition.getAnalyzed() == false*/)
                {
                    //truePath = new List<Space>();
                    //truePath.Add(startingPosition);
                    //candidateSpaces.Add(eastPosition);
                    //eastPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, eastPosition));
                    foreach (List<Space> w in movementRange(map, currentPlayer, currentMovementPoints, eastPosition, path))
                    {
                        w.Add(startingPosition);
                        foreach (Space l in movementRange(map, currentPlayer, currentMovementPoints, eastPosition))
                        {
                            w.Add(l);
                        }
                        combinedPaths.Add(w);
                        //Console.WriteLine("Adding " + w.getID());
                        //candidateSpaces.Add(w);
                        //truePath.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //path.Add(truePath);
                    //eastPosition.setAnalyzed(true);
                    //candidateSpaces.Add(eastPosition);
                }
                if (westImpassable == false/* && westPosition.getAnalyzed() == false*/)
                {
                    //truePath = new List<Space>();
                    //truePath.Add(startingPosition);
                    //candidateSpaces.Add(westPosition);
                    //westPosition.setAnalyzed(true);
                    //candidateSpaces.AddRange(movementRange(map, currentPlayer, currentMovementPoints, westPosition));
                    foreach (List<Space> w in movementRange(map, currentPlayer, currentMovementPoints, westPosition, path))
                    {
                        w.Add(startingPosition);
                        foreach (Space l in movementRange(map, currentPlayer, currentMovementPoints, westPosition))
                        {
                            w.Add(l);
                        }
                        combinedPaths.Add(w);
                        //Console.WriteLine("Adding " + w.getID());
                        //candidateSpaces.Add(w);
                        //truePath.Add(w);
                        //w.setAnalyzed(true);
                    }
                    //path.Add(truePath);
                    //westPosition.setAnalyzed(true);
                    //candidateSpaces.Add(westPosition);
                }
            }
            //Do space clean-up. Get rid of all duplicate spaces that arise in the list before returning candidate spaces.
            foreach (Space w in candidateSpaces)
            {
                //The vacuum cleaner is my way of asking "is this space qualified to be added?"
                vacuumCleaner.Add(true);
            }
            for (int i = 0; i < candidateSpaces.Count(); i++)
            {
                for (int j = 0; j < candidateSpaces.Count(); j++)
                {
                    if (i != j && candidateSpaces[i] == candidateSpaces[j] && vacuumCleaner[j] == true && vacuumCleaner[i] == true)
                    {
                        //Only one space, not its duplicates, can be included. We're going to filter those duplicate spaces.
                        vacuumCleaner[j] = false;
                    }
                }
            }
            for (int i = 0; i < candidateSpaces.Count(); i++)
            {
                if (vacuumCleaner[i] == true)
                {
                    cleanCandidateSpaces.Add(candidateSpaces[i]);
                }
            }
            /*foreach (Space w in candidateSpaces)
            {
                w.setAnalyzed(true);
            }*/
            //if (northImpassable == false) { northPosition.setAnalyzed(true); }
            //if (southImpassable == false) { southPosition.setAnalyzed(true); }
            //if (eastImpassable == false) { eastPosition.setAnalyzed(true); }
            //if (westImpassable == false) { westPosition.setAnalyzed(true); }
            //startingPosition.setAnalyzed(true);
            //Console.WriteLine("--------------");
            foreach (Space w in cleanCandidateSpaces)
            {
                //Console.WriteLine("Returning space " + w.getID() + ", coords (" + w.getX() + ", " + w.getY() + "), displacement x: " + map.getXFromSpace(startingPosition, w) + ", y: " + map.getYFromSpace(startingPosition, w));
            }
            if (startingPosition.getGuest() == null)
            {

            }
            else
            {
                //Console.WriteLine("Note: The surface of the recursions has been reached; this recursion contains " + startingPosition.getGuest().getName());
            }
            //Console.WriteLine("Total number of spaces returned = " + cleanCandidateSpaces.Count());
            return combinedPaths;
        }
        //Threatened range is the combination of all spaces in the range of each space a unit can move onto.
        internal List<Space> threatenedRange(Field map, Player currentPlayer, int movementPoints, Space currentSpace, Unit currentUnit)
        {
            List<Space> combinedRange = new List<Space>();
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
            foreach (Space w in movementRange(map, currentPlayer, movementPoints, currentSpace))
            {
                for (int k = 0; k < abilities.Count(); k++)
                {
                    for (int j = abilities[k].getMinRange(); j <= abilities[k].getMaxRange(); j++)
                    {
                        foreach (Space e in Range(map, w, j))
                        {
                            combinedRange.Add(e);
                        }
                    }
                }
            }
            return combinedRange;
        }
        //This simple method returns a list of spaces from the starting space to the ending space; the shortest path possible.
        internal List<Space> pathfinderSub(Field map, Unit currentUnit, Space currentSpace, Space endSpace, Player currentPlayer, int movementPoints)
        {
            List<Space> correctPath = new List<Space>();
            Space startingPosition = currentSpace;
            Space northPosition = startingPosition.getNorth();
            Space southPosition = startingPosition.getSouth();
            Space eastPosition = startingPosition.getEast();
            Space westPosition = startingPosition.getWest();
            bool northAnalyzed = false;
            bool southAnalyzed = false;
            bool eastAnalyzed = false;
            bool westAnalyzed = false;
            try
            {
                northAnalyzed = northPosition.getAnalyzed();
            }
            catch
            {
                northAnalyzed = true;
            }
            try
            {
                southAnalyzed = southPosition.getAnalyzed();
            }
            catch
            {
                southAnalyzed = true;
            }
            try
            {
                eastAnalyzed = eastPosition.getAnalyzed();
            }
            catch
            {
                eastAnalyzed = true;
            }
            try
            {
                westAnalyzed = westPosition.getAnalyzed();
            }
            catch
            {
                westAnalyzed = true;
            }
            bool northImpassable = false;
            bool southImpassable = false;
            bool eastImpassable = false;
            bool westImpassable = false;
            //int currentMovementPoints = movementPoints;

            //startingPosition.setAnalyzed(true);
            try
            {
                if (northPosition.getExist() != true) { northImpassable = true; }
                if (northPosition.getGuest() != null && northPosition.getConcealed(currentPlayer.getPlayerID()) == false) { northImpassable = true; }
            }
            catch
            {
                northImpassable = true;
            }
            try
            {
                if (southPosition.getExist() != true) { southImpassable = true; }
                if (southPosition.getGuest() != null && southPosition.getConcealed(currentPlayer.getPlayerID()) == false) { southImpassable = true; }
            }
            catch
            {
                southImpassable = true;
            }
            try
            {
                if (eastPosition.getExist() != true) { eastImpassable = true; }
                if (eastPosition.getGuest() != null && eastPosition.getConcealed(currentPlayer.getPlayerID()) == false) { eastImpassable = true; }
            }
            catch
            {
                eastImpassable = true;
            }
            try
            {
                if (westPosition.getExist() != true) { westImpassable = true; }
                if (westPosition.getGuest() != null && westPosition.getConcealed(currentPlayer.getPlayerID()) == false) { westImpassable = true; }
            }
            catch
            {
                westImpassable = true;
            }
            return correctPath;
        }
        internal List<Space> pathfinder2(Field map, Unit currentUnit, Space startSpace, Space endSpace, Player currentPlayer, int movementPoints, bool pathFound)
        {
            List<Space> correctPath = new List<Space>();
            Space currentSpace = startSpace;
            Space startingPosition = currentSpace;
            if (pathFound == false)
            {
                correctPath.Add(currentSpace);
            }
            Space northPosition = startingPosition.getNorth();
            Space southPosition = startingPosition.getSouth();
            Space eastPosition = startingPosition.getEast();
            Space westPosition = startingPosition.getWest();
            bool northAnalyzed = false;
            bool southAnalyzed = false;
            bool eastAnalyzed = false;
            bool westAnalyzed = false;
            try
            {
                northAnalyzed = northPosition.getAnalyzed();
            }
            catch
            {
                northAnalyzed = true;
            }
            try
            {
                southAnalyzed = southPosition.getAnalyzed();
            }
            catch
            {
                southAnalyzed = true;
            }
            try
            {
                eastAnalyzed = eastPosition.getAnalyzed();
            }
            catch
            {
                eastAnalyzed = true;
            }
            try
            {
                westAnalyzed = westPosition.getAnalyzed();
            }
            catch
            {
                westAnalyzed = true;
            }
            bool northImpassable = false;
            bool southImpassable = false;
            bool eastImpassable = false;
            bool westImpassable = false;
            //int currentMovementPoints = movementPoints;
            
            //startingPosition.setAnalyzed(true);
            try
            {
                if (northPosition.getExist() != true) { northImpassable = true; }
                if (northPosition.getGuest() != null && northPosition.getConcealed(currentPlayer.getPlayerID()) == false) { northImpassable = true; }
            }
            catch
            {
                northImpassable = true;
            }
            try
            {
                if (southPosition.getExist() != true) { southImpassable = true; }
                if (southPosition.getGuest() != null && southPosition.getConcealed(currentPlayer.getPlayerID()) == false) { southImpassable = true; }
            }
            catch
            {
                southImpassable = true;
            }
            try
            {
                if (eastPosition.getExist() != true) { eastImpassable = true; }
                if (eastPosition.getGuest() != null && eastPosition.getConcealed(currentPlayer.getPlayerID()) == false) { eastImpassable = true; }
            }
            catch
            {
                eastImpassable = true;
            }
            try
            {
                if (westPosition.getExist() != true) { westImpassable = true; }
                if (westPosition.getGuest() != null && westPosition.getConcealed(currentPlayer.getPlayerID()) == false) { westImpassable = true; }
            }
            catch
            {
                westImpassable = true;
            }
            if (movementPoints <= 0 || pathFound==true)
            {

            }
            else if (pathFound==false)
            {
                movementPoints--;
                if (northImpassable==false)
                {
                    currentSpace = northPosition;
                    if (currentSpace == endSpace)
                    {
                        //foreach (Space w in pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound))
                        {
                            correctPath.Add(northPosition);
                        }
                        pathFound = true;
                    }
                }
                if (southImpassable == false)
                {
                    currentSpace = southPosition;
                    if (currentSpace == endSpace)
                    {
                        //foreach (Space w in pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound))
                        {
                            correctPath.Add(southPosition);
                        }
                        pathFound = true;
                    }
                }
                if (eastImpassable == false)
                {
                    currentSpace = eastPosition;
                    if (currentSpace == endSpace)
                    {
                        //foreach (Space w in pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound))
                        {
                            correctPath.Add(eastPosition);
                        }
                        pathFound = true;
                    }
                }
                if (westImpassable == false)
                {
                    currentSpace = westPosition;
                    if (currentSpace == endSpace)
                    {
                        //foreach (Space w in pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound))
                        {
                            correctPath.Add(westPosition);
                        }
                        pathFound = true;
                    }
                }
                if (pathFound==false)
                {
                    List<Space> evaluatedList = null;
                    if (northImpassable == false)
                    {
                        currentSpace = northPosition;
                        evaluatedList = pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound);
                        foreach (Space w in evaluatedList)
                        {
                            if (w == endSpace)
                            {
                                pathFound = true;
                            }
                        }
                        if (pathFound == true)
                        {
                            foreach (Space w in evaluatedList)
                            {
                                correctPath.Add(w);
                            }
                        }
                    }
                    if (southImpassable == false)
                    {
                        currentSpace = southPosition;
                        evaluatedList = pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound);
                        foreach (Space w in evaluatedList)
                        {
                            if (w == endSpace)
                            {
                                pathFound = true;
                            }
                        }
                        if (pathFound == true)
                        {
                            foreach (Space w in evaluatedList)
                            {
                                correctPath.Add(w);
                            }
                        }
                    }
                    if (eastImpassable == false)
                    {
                        currentSpace = eastPosition;
                        evaluatedList = pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound);
                        foreach (Space w in evaluatedList)
                        {
                            if (w == endSpace)
                            {
                                pathFound = true;
                            }
                        }
                        if (pathFound == true)
                        {
                            foreach (Space w in evaluatedList)
                            {
                                correctPath.Add(w);
                            }
                        }
                    }
                    if (westImpassable == false)
                    {
                        currentSpace = westPosition;
                        evaluatedList = pathfinder2(map, currentUnit, currentSpace, endSpace, currentPlayer, movementPoints, pathFound);
                        foreach (Space w in evaluatedList)
                        {
                            if (w == endSpace)
                            {
                                pathFound = true;
                            }
                        }
                        if (pathFound == true)
                        {
                            foreach (Space w in evaluatedList)
                            {
                                correctPath.Add(w);
                            }
                        }
                    }
                }
            }
            return correctPath;
        }
        internal List<Space> pathfinder(Field map, Unit currentUnit, Space startSpace, Space endSpace)
        {
            List<Space> correctPath = new List<Space>();
            Space currentSpace = startSpace;
            Space northSpace = currentSpace.getNorth();
            Space southSpace = currentSpace.getSouth();
            Space eastSpace = currentSpace.getEast();
            Space westSpace = currentSpace.getWest();
            int xFromEnd = map.getXFromSpace(currentSpace, endSpace);
            int yFromEnd = map.getYFromSpace(currentSpace, endSpace);
            bool lookInPosX = true;
            bool lookInPosY = false;
            bool lookInNegX = false;
            bool lookInNegY = false;
            if (xFromEnd > 0)
            {
                lookInPosX = true;
                lookInPosY = false;
                lookInNegX = false;
                lookInNegY = false;
            }
            else if (yFromEnd > 0)
            {
                lookInPosX = false;
                lookInPosY = true;
                lookInNegX = false;
                lookInNegY = false;
            }
            else if (xFromEnd < 0)
            {
                lookInPosX = false;
                lookInPosY = false;
                lookInNegX = true;
                lookInNegY = false;
            }
            else if (yFromEnd < 0)
            {
                lookInPosX = false;
                lookInPosY = false;
                lookInNegX = false;
                lookInNegY = true;
            }
            int pathfindingAttempts = 0;
            while (currentSpace != endSpace)
            {
                if (pathfindingAttempts >= 300)
                {
                    break;
                }
                northSpace = currentSpace.getNorth();
                southSpace = currentSpace.getSouth();
                eastSpace = currentSpace.getEast();
                westSpace = currentSpace.getWest();
                //Console.WriteLine("Evaluating...try number " + pathfindingAttempts + "...");
                xFromEnd = map.getXFromSpace(currentSpace, endSpace);
                yFromEnd = map.getYFromSpace(currentSpace, endSpace);
                //If at any point the x and y are on the end space, break immediately.
                if (xFromEnd == 0 && yFromEnd == 0)
                {
                    break;
                }
                if (xFromEnd == 0)
                {
                    if (yFromEnd > 0)
                    {
                        lookInPosX = false;
                        lookInPosY = true;
                        lookInNegX = false;
                        lookInNegY = false;
                    }
                    else if (yFromEnd < 0)
                    {
                        lookInPosX = false;
                        lookInPosY = false;
                        lookInNegX = false;
                        lookInNegY = true;
                    }
                }
                if (yFromEnd == 0)
                {
                    if (xFromEnd > 0)
                    {
                        lookInPosX = true;
                        lookInPosY = false;
                        lookInNegX = false;
                        lookInNegY = false;
                    }
                    else if (xFromEnd < 0)
                    {
                        lookInPosX = false;
                        lookInPosY = false;
                        lookInNegX = true;
                        lookInNegY = false;
                    }
                }
                if (xFromEnd > 0 && lookInPosX == true)
                {
                    if ((eastSpace.getGuest() == null || eastSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && eastSpace.getExist() == true)
                    {
                        correctPath.Add(eastSpace);
                        currentSpace = eastSpace;
                    }
                    else
                    {
                        if (yFromEnd > 0)
                        {
                            lookInPosX = false;
                            lookInPosY = true;
                            lookInNegX = false;
                            lookInNegY = false;
                        }
                        else if (yFromEnd < 0)
                        {
                            lookInPosX = false;
                            lookInPosY = false;
                            lookInNegX = false;
                            lookInNegY = true;
                        }
                        else
                        {
                            lookInPosX = false;
                            lookInPosY = false;
                            lookInNegX = true;
                            lookInNegY = false;
                        }
                    }
                }
                else if (xFromEnd < 0 && lookInNegX == true)
                {
                    if ((westSpace.getGuest() == null || westSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && westSpace.getExist() == true)
                    {
                        correctPath.Add(westSpace);
                        currentSpace = westSpace;
                    }
                    else
                    {
                        if (yFromEnd > 0)
                        {
                            lookInPosX = false;
                            lookInPosY = true;
                            lookInNegX = false;
                            lookInNegY = false;
                        }
                        else if (yFromEnd < 0)
                        {
                            lookInPosX = false;
                            lookInPosY = false;
                            lookInNegX = false;
                            lookInNegY = true;
                        }
                        else
                        {
                            lookInPosX = true;
                            lookInPosY = false;
                            lookInNegX = false;
                            lookInNegY = false;
                        }
                    }
                }
                else if (yFromEnd > 0 && lookInPosY == true)
                {
                    if ((southSpace.getGuest() == null || southSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && southSpace.getExist() == true)
                    {
                        correctPath.Add(southSpace);
                        currentSpace = southSpace;
                    }
                    else
                    {
                        if (xFromEnd > 0)
                        {
                            lookInPosX = true;
                            lookInPosY = false;
                            lookInNegX = false;
                            lookInNegY = false;
                        }
                        else if (xFromEnd < 0)
                        {
                            lookInPosX = false;
                            lookInPosY = false;
                            lookInNegX = true;
                            lookInNegY = false;
                        }
                        else
                        {
                            lookInPosX = false;
                            lookInPosY = false;
                            lookInNegX = false;
                            lookInNegY = true;
                        }
                    }
                }
                else if (yFromEnd < 0 && lookInNegY == true)
                {
                    if ((northSpace.getGuest() == null || northSpace.getConcealed(currentUnit.getPlayer().getPlayerID()) == true) && northSpace.getExist() == true)
                    {
                        correctPath.Add(northSpace);
                        currentSpace = northSpace;
                    }
                    else
                    {
                        if (xFromEnd > 0)
                        {
                            lookInPosX = true;
                            lookInPosY = false;
                            lookInNegX = false;
                            lookInNegY = false;
                        }
                        else if (xFromEnd < 0)
                        {
                            lookInPosX = false;
                            lookInPosY = false;
                            lookInNegX = true;
                            lookInNegY = false;
                        }
                        else
                        {
                            lookInPosX = false;
                            lookInPosY = true;
                            lookInNegX = false;
                            lookInNegY = false;
                        }
                    }
                }
                pathfindingAttempts++;
            }
            return correctPath;
        }
        internal List<Space> Range2(Field map, Space space, int range)
        {
            int curx = space.getX();
            int cury = space.getY();
            int height = map.getHeight();
            int width = map.getWidth();
            int x = space.getX();
            int y = space.getY();
            List<Space> inRange = new List<Space>();
            for (int i = y; i < y + range; i++)
            {
                int maxX = 0;
                for (int j = x - maxX; j < x + maxX; j++)
                {
                    curx = j;
                    inRange.Add(map.convertCoordinatesToSpace(i, j));
                }
                if (i <= y)
                {
                    maxX = maxX + 1;
                }
                else
                {
                    maxX = maxX - 1;
                }
            }
            return inRange;
        }
        internal List<Space> oldRange(Field map, Space space, int range)
        {
            int curx = space.getX();
            int cury = space.getY();
            int height = map.getHeight();
            int width = map.getWidth();
            int stupid = 0;
            Console.WriteLine(curx + " + " + cury + " + " + height + " + " + width); //Debug
            List<Space> inRange = new List<Space>();
            //Bug: Method calls successfully up to here. If unit is not in a certain quadrant of the map, the while loop
            //below fails to call.
            while ((curx * width) + (cury * height) >= (height * width) - 1)
            {
                //Possible error: GetX and GetY methods appear to be flipped. Refer to Space.cs and Field.cs.
                int x = space.getX();
                int y = space.getY();
                int maxX = 0;
                /*Possible error: Switching from i >= y + range to i <= y + range causes the for loop to loop successfully.
                If the loop is supposed to work, this change causes it the loop to function correctly and is the potential fix.
                However, I observed that another error tends to happen upon trying to do this.
                While it gets to the second loop, it errs as soon as it reaches "inRange.Add(map.convertCoordinatesToSpace(i, j));.
                The call stack references the method "convertCoordinatesToSpace", which somehow returns a negative number (i.e.
                the space it returns doesn't exist). This problem can be derived from numerous possible sources, but some
                possible sources are that "convertCoordinatesToSpace" was not written correctly, the getX/getY methods are incorrect
                (reasoning: it derives our x and y values), the i or j values are somehow unacceptable, or any other problem
                you find here.*/
                for (int i = y - range; i <= y + range && i < height && i >= 0; i++)
                {
                    stupid++;
                    cury = i;
                    //stupid++;
                    for (int j = x - maxX; j <= x + maxX && j < width && j >= 0; j++)
                    {
                        curx = j;
                        inRange.Add(map.convertCoordinatesToSpace(i, j));
                    }
                    if (i <= y)
                    {
                        maxX = maxX + 1;
                    }
                    else
                    {
                        maxX = maxX - 1;
                    }
                }
                //Console.WriteLine("You are stupid * " + stupid);//debug
                /*How to debug: If, while running the game and making a unit attack a space, "Successful * 0" appears, then
                the for loops above failed to increment the variable stupid; the implication here is that the for loops failed to
                meet their conditions to run their instructions. The integer contained in stupid should be more than 0 if they
                are working correctly.*/
                return inRange;
            }
            return inRange;
        }
        internal List<Space> oldRange2(Field map, Space space, int range)
        {
            int curx = space.getX();
            int cury = space.getY();
            int height = map.getHeight();
            int width = map.getWidth();
            int stupid = 0;
            //Console.WriteLine(curx + " + " + cury + " + " + height + " + " + width); //Debug
            List<Space> inRange = new List<Space>();
            //Bug: Method calls successfully up to here. If unit is not in a certain quadrant of the map, the while loop
            //below fails to call.
            //while ((curx * width) + (cury * height) >= (height * width) - 1)
            //{
            //Possible error: GetX and GetY methods appear to be flipped. Refer to Space.cs and Field.cs.
            int x = space.getX();
            int y = space.getY();
            int maxX = 0;
            /*Possible error: Switching from i >= y + range to i <= y + range causes the for loop to loop successfully.
            If the loop is supposed to work, this change causes it the loop to function correctly and is the potential fix.
            However, I observed that another error tends to happen upon trying to do this.
            While it gets to the second loop, it errs as soon as it reaches "inRange.Add(map.convertCoordinatesToSpace(i, j));.
            The call stack references the method "convertCoordinatesToSpace", which somehow returns a negative number (i.e.
            the space it returns doesn't exist). This problem can be derived from numerous possible sources, but some
            possible sources are that "convertCoordinatesToSpace" was not written correctly, the getX/getY methods are incorrect
            (reasoning: it derives our x and y values), the i or j values are somehow unacceptable, or any other problem
            you find here.*/
            for (int i = y - range; i <= y + range/* && i < height && i >= 0*/; i++)
            {
                stupid++;
                cury = i;
                /*while (i > height)
                {
                    i++;
                }
                while (i < 0)
                {
                    i++;
                }*/
                //stupid++;
                for (int j = x - maxX; j <= x + maxX/* && i < width && i >= 0*/; j++)
                {
                    curx = j;
                    /*while (j > width)
                    {
                        j++;
                    }
                    while (j < 0)
                    {
                        j++;
                    }*/
                    if (j < width && j > 0 && i < height && i >= 0)
                    {
                        inRange.Add(map.convertCoordinatesToSpace(i, j));
                    }
                }
                if (i <= y)
                {
                    maxX = maxX + 1;
                }
                else
                {
                    maxX = maxX - 1;
                }
            }
            //Console.WriteLine("You are stupid * " + stupid);//debug
            /*How to debug: If, while running the game and making a unit attack a space, "Successful * 0" appears, then
            the for loops above failed to increment the variable stupid; the implication here is that the for loops failed to
            meet their conditions to run their instructions. The integer contained in stupid should be more than 0 if they
            are working correctly.*/
            //foreach (Space w in inRange)
            //{
            //Console.WriteLine(w.getX() + ", " + w.getY());
            //}
            return inRange;
            //}
            return inRange;
        }
        //Effect durations are checked on the beginning of the afflicted unit's turn.
        internal void checkEffectDurations(Unit unit)
        {
            for (int w = 0; w < unit.getAllActiveEffects().Count(); w++)
            {
                unit.getActiveEffectAtIndex(w).setDuration(unit.getActiveEffectAtIndex(w).getDuration() - 1);
                if (unit.getActiveEffectAtIndex(w).getDuration() <= 0)
                {
                    Console.WriteLine(unit.getActiveEffectAtIndex(w).getEffectName() + " has worn off of " + unit.getName());
                    unit.removeEffectAtIndex(w);
                }
            }
        }
        //Tile effect durations are checked differently from effect durations. At the start of the unit's turn who cast
        //abilities with tile effects, all of their existing tile effects duration goes down by 1.
        internal void checkTileEffectDurations(Unit castingUnit)
        {
            for (int w = 0; w < castingUnit.getAllTileEffects().Count(); w++)
            {
                castingUnit.getTileEffectAtIndex(w).setDuration(castingUnit.getTileEffectAtIndex(w).getDuration() - 1);
                if (castingUnit.getTileEffectAtIndex(w).getDuration() <= 0)
                {
                    
                    castingUnit.getTileEffectAtIndex(w).getHost().removeTileEffectsWithZeroDuration();
                    //List<TileEffect> temporaryTileEffectList = castingUnit.getTileEffectAtIndex(w).getHost().getAllTileEffects();
                    //for (int j = 0; j < temporaryTileEffectList.Count(); j++)
                    {
                        //if (castingUnit.getTileEffectAtIndex(w) == null)
                        {

                        }
                        //else if (temporaryTileEffectList[j] == null)
                        {

                        }
                        //else if (castingUnit.getTileEffectAtIndex(w) == temporaryTileEffectList[j] && castingUnit.getTileEffectAtIndex(w).getDuration()<=0)
                        {
                            //castingUnit.getTileEffectAtIndex(w).getHost().removeTileEffectAtIndex(j);
                        }
                    }
                    //castingUnit.getTileEffectAtIndex(w).setHost(null);
                    //castingUnit.removeTileEffectAtIndex(w);
                }
            }
        }
    }
}
