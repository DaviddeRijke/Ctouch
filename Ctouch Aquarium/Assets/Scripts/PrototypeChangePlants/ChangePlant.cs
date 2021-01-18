using System.Collections.Generic;
using UnityEngine;

namespace PrototypeChangePlants
{
    public class ChangePlant : MonoBehaviour
    {
        [SerializeField]
        private List<Color> colors = new List<Color>();

        public SettingsManager settingsManager;

        private int value = 0;
        private int currentValue = 1;

        // Start is called before the first frame update
        void Start()
        {
            value = settingsManager.CalculateChange();
            UpdatePlant();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdatePlant()
        {
            value = settingsManager.CalculateChange();
            switch (value)
            {
                case -1:
                    if (currentValue != 0)
                    {
                        currentValue -= 1;
                        Debug.Log(currentValue);
                        GetComponent<Renderer>().material.color = colors[currentValue];
                    }
                    break;
                case 1:
                    if (currentValue != colors.Count -1)
                    {
                        currentValue += 1;
                        Debug.Log(currentValue);
                        GetComponent<Renderer>().material.color = colors[currentValue];
                    }
                    break;
            }

        }
    }
}
