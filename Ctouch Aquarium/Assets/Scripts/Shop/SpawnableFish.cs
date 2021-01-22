using UnityEngine;

namespace Shop
{
    public class SpawnableFish : MonoBehaviour
    {
        private bool fall = true;
        private void Update()
        {
            if(fall) transform.position += Vector3.down * Time.deltaTime;
        }

        private void OnMouseDown()
        {
            fall = false;
            Destroy(this);
        }
    }
}