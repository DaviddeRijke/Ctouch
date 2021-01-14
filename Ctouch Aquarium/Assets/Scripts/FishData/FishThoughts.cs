using System.Collections.Generic;
using DefaultNamespace;

namespace FishData
{
    public static class FishThoughts
    {
        public static string PollutionWorse = "<color=red>\"Het water stinkt een beetje...\"";
        public static string PollutionBetter = "<color=#48945a>\"Het is nu echt een stuk frisser hier!\"";
        public static string PollutionPerfect = "<color=#48945a>\"Alles is helemaal schoon!\"";
        public static string SharkArived = "<color=red>\"AaaaaaAaa! een enge haai!\"";
        public static string SharkLeft = "<color=#48945a>\"Gelukkig is die grote vis weer weg :)\"";

        public static void MakeFishThink(List<Fish> fish, string thought)
        {
            fish.Shuffle();
            for (int i = 0; i < fish.Count / 1.5; i++) fish[i].AddThought(thought);
        }
    }
}