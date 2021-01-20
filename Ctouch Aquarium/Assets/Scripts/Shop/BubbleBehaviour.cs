using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Shop
{
    public class BubbleBehaviour : MonoBehaviour
    {
        public float Speed = .5f;
        public bool Active;
        private Action<GameObject> onDone;
        private GameObject param;
        public void StartBehaviour(GameObject f, Action<GameObject> onDone)
        {
            this.onDone = onDone;
            param = f;
            Active = true;
        }

        private void Update()
        {
            if (Active)
            {
                transform.parent.position += Speed * Time.deltaTime * Vector3.down;
            }
        }

        private void OnMouseDown()
        {
            onDone.Invoke(param);
        }
    }
}