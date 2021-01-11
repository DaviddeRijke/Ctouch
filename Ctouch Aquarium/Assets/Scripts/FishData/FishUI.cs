using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FishData
{
    public class FishUI : MonoBehaviour
    {
        [SerializeField] private Image bubble;
        [SerializeField] private TextMeshProUGUI nameText;

        private Vector3 defaultPos;

        private Fish fish;

        private void Awake()
        {
            defaultPos = transform.position;
            nameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (transform.position != defaultPos)
                transform.position = fish.transform.position;
        }

        public void Init(Fish fish)
        {
            this.fish = fish;
            nameText.text = fish.GetName();

            Vector3 boundSize = fish.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size;
            Debug.Log(boundSize);
            float size = Mathf.Max(boundSize.x, boundSize.y);
            size = Mathf.Max(boundSize.z, size);

            bubble.transform.localScale = Vector3.one * size;
            transform.position = fish.transform.position;
        }
    }
}