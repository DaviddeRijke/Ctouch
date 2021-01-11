using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkObjectScript : MonoBehaviour
{
    private SharkManager sharkManager;

    public void SetSharkManager(SharkManager sharkManager)
    {
        this.sharkManager = sharkManager;
    }

    private void OnMouseDown()
    {
        if (sharkManager != null)
        {
            sharkManager.tapOnShark();
        }
    }
}
