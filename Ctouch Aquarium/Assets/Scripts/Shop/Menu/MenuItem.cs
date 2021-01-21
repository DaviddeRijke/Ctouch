using UnityEngine;
using UnityEngine.UI;

namespace Shop.Menu
{
    public class MenuItem : MonoBehaviour
    {
        private MenuWindow _window;
        public MenuWindow Window
        {
            get => _window;
            set
            {
                if (_window != null) _window.OnRefresh -= Refresh;
                if (value != null) value.OnRefresh += Refresh;
                _window = value;
            }
        }

        public virtual void Refresh()
        {
        }
    }
}