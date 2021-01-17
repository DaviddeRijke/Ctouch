using System.Collections.Generic;
using UnityEngine;

namespace PrototypeChangePlants
{
    public class SettingsManager : MonoBehaviour
    {
        private List<double> averageBacklightPerHour = new List<double>() { 50, 25, 20, 30, 30, 100, 50 };

        private List<double> averageBacklightPerDay = new List<double>() { 55, 30, 30, 10, 100, 100, 50 };

        public List<double> GetAverageBacklightPerHour()
        {
            return averageBacklightPerHour;
        }

        public List<double> GetAverageBacklightPerDay()
        {
            return averageBacklightPerDay;
        }
        public void AddAverageBacklightPerHour(double average)
        {
            averageBacklightPerHour.Add(average);
        }

        public void AddAverageBacklightPerDay(double average)
        {
            averageBacklightPerDay.Add(average);
        }

        public int CalculateChange()
        {
            double lastValue = averageBacklightPerHour[averageBacklightPerHour.Count -1];

            double secondlastValue = averageBacklightPerHour[averageBacklightPerHour.Count - 2];

            if(lastValue < secondlastValue)
            {
                return -1;
            }
            else if(lastValue > secondlastValue)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
