using UnityEngine;

namespace FishData
{
    public class Fish : MonoBehaviour
    {
        public string fishName;

        private void OnMouseUp() => ShowTooltip();

        private void ShowTooltip()
        {
            FishUI ui = FindObjectOfType<FishUI>();
            ui.gameObject.SetActive(true);
            ui.Open(this);
        }
    }
}