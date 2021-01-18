using UnityEngine;
using UnityEngine.UI;

namespace PrototypeChangePlants
{
    public class InputUI : MonoBehaviour
    {
        private double average;
    
        [SerializeField]
        private InputField input;

        [SerializeField]
        private SettingsManager settingsManager;

        public void AddAverage(string value)
        {
            average = double.Parse(input.text);
            settingsManager.AddAverageBacklightPerHour(average);
        }
    }
}
