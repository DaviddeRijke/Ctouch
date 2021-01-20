using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MonoBehaviour
    {
        public Sprite SpriteOpen;
        public Sprite SpriteClose;
        public Image Icon;

        public MenuWindow target;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (target.MenuIsActive) Close(); else Open();
            });
            target.OnClose += Close;
        }

        void Open()
        {
            target.Open();
            Icon.sprite = SpriteClose;
        }

        void Close()
        {
            target.Close(true);
            Icon.sprite = SpriteOpen;
        }
    }
}