using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShark : MonoBehaviour
{
    [SerializeField]
    private GameObject shark;

    private int sharkCount = 0;
    
    private void SpawnNewShark()
    {
        if(sharkCount != 0)
        {
            return;
        }

        Instantiate(shark, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
