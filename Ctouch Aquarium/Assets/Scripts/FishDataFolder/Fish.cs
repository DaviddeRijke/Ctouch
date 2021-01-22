using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishDataFolder
{
    public class Fish : MonoBehaviour
    {
        public string fishName;
        public bool isEaten;
        public bool isInInventory;

        [SerializeField] private List<string> thoughtData = new List<string>();
        private Queue<string> thoughts = new Queue<string>();


        private void OnEnable()
        {
            thoughts = new Queue<string>(thoughtData);
        }

        private void OnDisable() => thoughtData = thoughts.ToList();

        public void AddThought(string thought)
        {
            thoughts.Enqueue(thought);
            if (thoughts.Count > 3) thoughts.Dequeue();
        }

        public List<string> GetThoughts() => thoughts.ToList();

        public void ShowTooltip()
        {
            FishUI ui = FindObjectOfType<FishUI>();
            ui.gameObject.SetActive(true);
            ui.Open(this);
        }
    }
}