using UnityEngine;

namespace Shop.Menu
{
    public class MenuItem : MonoBehaviour
    {
        public MenuWindow Window;

        private void Awake()
        {
            if (Window == null) return;
            Window.OnRefresh += Refresh;
        }

        public virtual void Refresh()
        {
        }
    }
}