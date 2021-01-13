using UnityEngine;

namespace Score
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Score")]
    public class ScoreData : ScriptableObject
    {
        public float score = 0;
        public string lastTimeStamp = "2000-01-01T00:00:00+01:00";
    }
}
