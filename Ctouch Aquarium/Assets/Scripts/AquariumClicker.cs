using System;
using System.Collections.Generic;
using FishData;
using UnityEngine;

public class AquariumClicker : MonoBehaviour
{
    [SerializeField] private clickState state = clickState.None;

    private Dictionary<clickState, string> clickTags = new Dictionary<clickState, string>();

    private void Awake()
    {
        clickTags.Add(clickState.None, "Fish");
        clickTags.Add(clickState.Remove, "Fish");
        clickTags.Add(clickState.Clean, "Goop");
    }

    public void SetState(clickState state)
    {
        this.state = state;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.collider.CompareTag(clickTags[state]))
                {
                    switch (state)
                    {
                        case clickState.None:
                            hit.transform.GetComponent<Fish>().ShowTooltip();
                            break;
                        case clickState.Remove:
                            throw new NotImplementedException();
                            break;
                        case clickState.Clean:
                            hit.transform.GetComponent<Goop>().Remove();
                            break;
                    }
                }
            }
        }
    }

    public enum clickState
    {
        None,
        Remove,
        Clean
    }
}