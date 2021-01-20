using TMPro;
using UnityEngine;

namespace Shop
{
    public class ScoreLabel : MonoBehaviour
    {
        public Score score;
        public string Prefix;

        private TextMeshProUGUI tmp;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            tmp.text = $"{Prefix}{score.score}";
        }
    }
}