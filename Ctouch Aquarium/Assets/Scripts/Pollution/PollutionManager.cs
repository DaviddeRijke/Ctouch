using System.Collections.Generic;
using UnityEngine;

namespace Pollution
{
    public class PollutionManager : MonoBehaviour
    {
        [SerializeField] private Color cleanColor;
        [SerializeField] private Color dirtyColor;

        [SerializeField, Range(0, 1)] private float startPollution;
        private float _pollution;

        [SerializeField] private int startGoops;

        [SerializeField] private float maxGoops;
        private float _goopCount;

        [Header("References")] [SerializeField]
        private GameObject GoopPrefab;

        [SerializeField] private List<Light> lights;

        private void Start()
        {
            //SetPollution(startPollution);
            //CreateGoop(startGoops);
        }

        /// <summary>
        /// Sets pollution level of the aquarium, affecting light color
        /// </summary>
        /// <param name="value">Value between 0 and 1 where 0 is clean and 1 is fully polluted</param>
        public void SetPollution(float value)
        {
            _pollution = value;

            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].color = Color.Lerp(cleanColor, dirtyColor, value);
            }
        }

        /// <summary>
        /// Makes a goop appear in the aquarium
        /// </summary>
        /// <param name="count">number of goops to create</param>
        public void CreateGoop(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (_goopCount + 1 > maxGoops) return; // too many goops

                _goopCount++;
                SpawnGoop();
                SetPollution(_goopCount / 10);
            }
        }

        private void SpawnGoop()
        {
            RaycastHit hit;
            if (Physics.Raycast(
                Camera.main.ScreenPointToRay(new Vector2(
                    Random.Range(0, Screen.width),
                    Random.Range(0, Screen.height))), out hit))
            {
                Vector3 normal = hit.normal;
                if (normal == Vector3.up) normal = transform.InverseTransformDirection(hit.transform.up);

                GameObject goop = Instantiate(GoopPrefab, hit.point, Quaternion.LookRotation(normal));
                goop.GetComponent<Goop>().pollutionManager = this;
            }
        }

        public void RemoveGoop()
        {
            _goopCount--;
            SetPollution(_goopCount /10);
        }
    }
}