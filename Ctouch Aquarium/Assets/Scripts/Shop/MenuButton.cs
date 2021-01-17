using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MonoBehaviour
    {
        public Sprite sOpen;
        public Sprite sClose;
        public Image imgIcon;

        public Shop target;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("click");
                if (target.IsActive)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            });
            target.OnBuy.AddListener(Close);
        }

        void Open()
        {
            Debug.Log("open");
            target.Open();
            imgIcon.sprite = sClose;
        }

        void Close()
        {
            Debug.Log("close");
            target.Close();
            imgIcon.sprite = sOpen;
        }
    }
}