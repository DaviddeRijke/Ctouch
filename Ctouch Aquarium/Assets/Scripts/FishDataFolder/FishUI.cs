using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FishDataFolder
{
    public class FishUI : MonoBehaviour
    {
        [SerializeField] private Image bubble;

        // [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button editButton;
        [SerializeField] private TextMeshProUGUI[] thoughtsFields;

        private Vector3 defaultPos;

        private Fish _fish;

        private VirtualKeyboard keyboard = new VirtualKeyboard();
        private bool keyboardActive;

        private string input;

        private void Awake()
        {
            defaultPos = transform.position;
            nameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (transform.position != defaultPos)
            {
                transform.position = _fish.transform.position;

                if (keyboardActive)
                {
                    input += Input.inputString;
                    nameText.text = input;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        StopEdit();
                    }
                }
                else
                {
                    nameText.text = _fish.fishName;
                }

                if (Input.GetMouseButtonDown(0) &&
                    !RectTransformUtility.RectangleContainsScreenPoint(
                        nameText.transform.parent.GetComponent<RectTransform>(),
                        Input.mousePosition,
                        Camera.main))
                {
                    Close();
                }
            }
        }

        private void OnEnable()
        {
            editButton.onClick.AddListener(StartEdit);
        }

        private void OnDisable()
        {
            editButton.onClick.RemoveListener(StopEdit);
        }

        private void StartEdit()
        {
            keyboardActive = true;
            keyboard.ShowTouchKeyboard();
        }

        private void StopEdit()
        {
            keyboardActive = false;
            keyboard.HideTouchKeyboard();
            _fish.fishName = input;
            Close();
        }

        public void Close()
        {
            _fish = null;
            transform.position = defaultPos;
            Time.timeScale = 1f;
        }

        public void Open(Fish fish)
        {
            _fish = fish;
            input = String.Empty;
            
            nameText.text = fish.fishName;
            Time.timeScale = .2f;

            LoadThoughts(fish.GetThoughts());

            Vector3 boundSize = fish.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size;
            Debug.Log(boundSize);
            float size = Mathf.Max(boundSize.x, boundSize.y);
            size = Mathf.Max(boundSize.z, size);

            bubble.transform.localScale = Vector3.one * size;
            transform.position = fish.transform.position;
        }

        private void LoadThoughts(List<string> thoughts)
        {
            for (int i = 0; i < thoughtsFields.Length; i++) thoughtsFields[i].text = String.Empty;
            for (int i = 0; i < thoughts.Count; i++) thoughtsFields[i].text = thoughts[i];
        }
    }
}