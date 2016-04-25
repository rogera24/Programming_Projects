using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Engine
{
    public class Player
    {
        /*Players should assume a "master-slave" relationship with their units. All units should have a player pointer
         to indicate their owner.*/
        //A player has an ID attached to them for quick reference.
        int playerID;
        //The player has a name attached to them that changes throughout the story and can be anything in multiplayer mode.
        string playerName;
        //A player is either human-controlled or AI-controlled.
        bool isAIPlayer;
        //A player may be identified by a color, which may or may not color units belonging to that player or
        //decides unit color palettes/costumes.
        string playerColor;
        //If false, the player's units cannot attack their own units. If true, the player's units can attack their own units.
        //Useful for monster infighting.
        bool friendlyFireForOwnUnits = false;
        int livingUnitsInBattle = 0;
        int numberOfUnitsOwned;
        List<Unit>UnitsOwned = new List<Unit>();
        //A player has relationships with other players. The player may attack units belonging to hostile players,
        //and may not attack units belonging to allied players or themselves. The possible relationships are
        //"hostile", which means units can attack each other, or "allied", which means units cannot attack each
        //other.
        List<String> relationships = new List<String>();
        //A team contains any number of players and can have any name. All players in the team are allied with each other.
        string team;
        List<Player> teammates = new List<Player>();
        //For reference, it is sometimes necessary to insure that the player can refer to other players in the game.
        List<Player> players = new List<Player>();
        //The space of interest is a space on the current field that the player becomes interested in. It is inaccessible to see
        //for human players, although a human player that knows how it works will know that the space of interest always changes
        //to the last attacked space(s).
        //An AI player uses the space of interest for reference; if there are no visible enemy units nearby to run towards, the AI player
        //will send their units to the space of interest by default.
        Space spaceOfInterest;
        public Player(int ID, bool isAIPlayer, string playerColor, string teamName)
        {
            this.playerID = ID;
            this.isAIPlayer = isAIPlayer;
            this.playerColor = playerColor;
            this.team = teamName;
        }
        public int getPlayerID()
        {
            return this.playerID;
        }
        //Returns true if the player is an AI player.
        public bool isAI()
        {
            return this.isAIPlayer;
        }
        public string getPlayerColor()
        {
            return this.playerColor;
        }
        public string getPlayerName()
        {
            return this.playerName;
        }
        public bool getCanAttackOwnUnits()
        {
            return this.friendlyFireForOwnUnits;
        }
        public int getLivingUnitsInBattle()
        {
            return this.livingUnitsInBattle;
        }
        public int getNumberOfUnitsOwned()
        {
            return numberOfUnitsOwned;
        }
        //Can allow the toggling of a player between AI player and human player.
        public void setIsAI(bool x)
        {
            this.isAIPlayer = x;
        }
        public void setCanAttackOwnUnits(bool x)
        {
            this.friendlyFireForOwnUnits = x;
        }
        public void setPlayerName(string x)
        {
            this.playerName = x;
        }
        public void setLivingUnitsInBattle(int x)
        {
            this.livingUnitsInBattle = x;
        }
        
        public void setPlayerColor(string x)
        {
            this.playerColor = x;
        }
        public void addToUnitsOwned(Unit x)
        {
            this.UnitsOwned.Add(x);
        }
        public void removeFromUnitsOwned(Unit x)
        {
            this.UnitsOwned.Remove(x);
        }
        /*public Unit getUnitFromUnitsOwned(Unit x)
        {
            return this.UnitsOwned[x];
        }*/ //NYI
        public List<Unit> getAllUnitsFromUnitsOwned()
        {
            return this.UnitsOwned;
        }
        //Finds this player's relationship to target player, located at player index.
        public string getRelationshipToPlayer(int playerIndex)
        {
            return relationships[playerIndex];
        }
        //Possible relationship values are "self", "hostile", "ally", and "neutral".
        //Self is the relationship of a player with itself.
        //Hostile makes all units belonging to this player enemies with the source player, allowing the source player to attack the target player.
        //Ally makes all units belonging to this player share vision and become unattackable with source player.
        //Neutral makes all units belonging to this player unattackable with source player, although they do not share vision.
        public void setRelationshipToPlayer(string x, int playerIndex)
        {
            relationships[playerIndex] = x;
        }
        //Adds a relationship to the end of the array.
        public void addRelationship(string x)
        {
            relationships.Add(x);
        }
        public string getTeam()
        {
            return this.team;
        }
        public void setTeam(string teamName)
        {
            this.team = teamName;
        }
        public void includePlayerInTeam(Player x)
        {
            teammates.Add(x);
        }
        public void updateTeamRelations(bool isMultiplayer)
        {
            //if (isMultiplayer == true)
            {
                //teammates = new List<Player>();
                for (int i = 0; i <= 3; i++)
                {
                    if (getRelationshipToPlayer(i) != "self" && players[i].getTeam() == team)
                    {
                            setRelationshipToPlayer("ally", i);
                    }
                    else if (getRelationshipToPlayer(i) != "self" && getRelationshipToPlayer(i)!="neutral")
                    {
                        setRelationshipToPlayer("hostile", i);
                    }
                }
            }
        }
        public void addPlayerToKnownPlayerList(Player x)
        {
            players.Add(x);
        }
        public void setSpaceOfInterest(Space x)
        {
            this.spaceOfInterest = x;
        }
        public Space getSpaceOfInterest()
        {
            return this.spaceOfInterest;
        }
    }
}
