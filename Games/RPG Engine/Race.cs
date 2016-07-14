using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Engine_New
{
    class Race
    {
        //Race is a category that all living entities (and non-living entities, where applicable) apply to.
        //Race is sometimes a major determinant of stats, but classes are meant to provide most of these stats.
        string raceName;
        //A race type is a grouping of races that share similar traits. It is used by the game to determine
        //strengths, weaknesses, or gimmicks unique to that race type.
        //A race type is stored as a number representing a race.
        enum raceType {Humanoid, Animal, Vermin, Magical_Beast, Monstrous_Humanoid, Aberration, Undead, Ooze, Elemental, Plant, Giant, Construct, Dragon, Fey, Deathless, Outsider, Titan, God};
    }
}
