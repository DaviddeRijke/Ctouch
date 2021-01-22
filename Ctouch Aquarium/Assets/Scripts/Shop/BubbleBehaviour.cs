using System;
using UnityEngine;

namespace Shop
{
    public class BubbleBehaviour : MonoBehaviour
    {
        public float Speed = .5f;
        public bool Active;
        private Action<GameObject> onDone;
        private GameObject param;

        public float minPos = 1;
        public void StartBehaviour(GameObject f, Action<GameObject> onDone)
        {
            this.onDone = onDone;
            param = f;
            Active = true;
        }

        private void Update()
        {
            if (Active && transform.parent.position.y > minPos)
            {
                transform.parent.position += Speed * Time.deltaTime * Vector3.down;
            }
        }

        private void OnMouseDown()
        {
            onDone.Invoke(param);
            Destroy(this);
        }
    }
}