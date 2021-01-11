using UnityEngine;

namespace FishData
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private string fishName;

        public string GetName() => fishName;

        private void OnMouseDown() => ShowTooltip();

        private void ShowTooltip()
        {
            FishUI ui = FindObjectOfType<FishUI>();
            ui.gameObject.SetActive(true);
            ui.Init(this);
        }
    }
}