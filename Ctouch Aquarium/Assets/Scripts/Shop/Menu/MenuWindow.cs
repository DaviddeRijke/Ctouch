using Shop.Menu;
using UnityEngine;
using UnityEngine.Events;

namespace Shop
{
    public class MenuWindow : MonoBehaviour
    {
        public GameObject target;
        public UnityAction OnClose;
        public UnityAction OnRefresh;
        
        public bool MenuIsActive => target.activeSelf;
    
        public void Open()
        {
            target.SetActive(true);
        }

        public void InvokeRefresh()
        {
            OnRefresh?.Invoke();
        }

        public void Close(bool skipCallback = false)
        {
            if (skipCallback)
            {
                target.SetActive(false);
            }
            else
            {
                OnClose?.Invoke();
            }
            
        }

        public virtual bool Validate(MenuItem item)
        {
            return true;
        }
    }
}