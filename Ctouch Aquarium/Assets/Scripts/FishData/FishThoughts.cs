using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace FishData
{
    public static class FishThoughts
    {
        public static List<string> PollutionWorse = new List<string>(){
            "<color=red>\"Het water stinkt een beetje...\"",
            "<color=red>\"Oh nee, het wordt hier steeds viezer.\"",
            "<color=red>\"Jakkiebah, wat is dat groene ding daar.\"",
            "<color=red>\"Ik zie, ik zie wat jij niet ziet en het is groen en vies.\"",
            "<color=red>\"Bah, dat is vies.\""
        };

        public static List<string> PollutionBetter = new List<string>(){
            "<color=#48945a>\"Het is nu echt een stuk frisser hier!\"",
            "<color=#48945a>\"Dankje, Het is weer wat schoner.\"",
            "<color=#48945a>\"Goed zo, dat vieze groene ding is weg!\"",
            "<color=#48945a>\"Het ziet er al weer een stuk beter uit hier.\""
        };

        public static List<string> PollutionPerfect = new List<string>(){
            "<color=#48945a>\"Alles is helemaal schoon!\"",
            "<color=#48945a>\"Lekker! Je hebt het aquarium schoongepoetst.\"",
            "<color=#48945a>\"Dankjewel, dat lucht op.\"",
            "<color=#48945a>\"Goed gedaan!\""
        };

        public static List<string> SharkArived = new List<string>(){
            "<color=red>\"AaaaaaAaa! een enge haai!\"",
            "<color=red>\"Die vis is wel heel groot en eng!\"",
            "<color=red>\"AaaaaaAaaaaaaAaaah\"",
            "<color=red>\"Snel, jaag die haai weg!\"",
            "<color=red>\"Ik hoop niet dat ik word opgegeten door die haai!\""
        };

        public static List<string> SharkLeft = new List<string>(){
            "<color=#48945a>\"Gelukkig is die grote vis weer weg :)\"",
            "<color=#48945a>\"Ah dat lucht op, die grote gemene haai is weg.\"",
            "<color=#48945a>\"Haha, weg haai!\"",
            "<color=#48945a>\"Poeh dat scheelde niets, die haai is weer weg.\"",
            "<color=#48945a>\"En blijf weg haai!\""
        };

        public static void MakeFishThink(List<Fish> fish, ThoughtEnum thoughtEnum)
        {
            fish.Shuffle();
            string thought = "";

            for (int i = 0; i < fish.Count / 1.5; i++)
            {
                switch (thoughtEnum)
                {
                    case ThoughtEnum.PollutionWorse:
                        int r = Random.Range(0, PollutionWorse.Count);
                        thought = PollutionWorse[r];
                        break;
                    case ThoughtEnum.PollutionBetter:
                        r = Random.Range(0, PollutionBetter.Count);
                        thought = PollutionBetter[r];
                        break;
                    case ThoughtEnum.PollutionPerfect:
                        r = Random.Range(0, PollutionPerfect.Count);
                        thought = PollutionPerfect[r];
                        break;
                    case ThoughtEnum.SharkArived:
                        r = Random.Range(0, SharkArived.Count);
                        thought = SharkArived[r];
                        break;
                    case ThoughtEnum.SharkLeft:
                        r = Random.Range(0, SharkLeft.Count);
                        thought = SharkLeft[r];
                        break;
                }

                fish[i].AddThought(thought);
            }
        }
    }

    public enum ThoughtEnum
    {
        PollutionWorse,
        PollutionBetter,
        PollutionPerfect,
        SharkArived,
        SharkLeft
    }
}