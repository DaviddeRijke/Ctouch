﻿using System;
using System.Collections;
using System.Collections.Generic;
using FishDataFolder;
using UnityEngine;

public class AquariumClicker : MonoBehaviour
{
    [SerializeField] private clickState state = clickState.None;

    private Dictionary<clickState, string> clickTags = new Dictionary<clickState, string>();

    [SerializeField] private ParticleSystem bubbles;
    private Vector3 defaultPos;
    private Coroutine bubbleCoroutine;

    private void Awake()
    {
        defaultPos = bubbles.transform.position;
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

            //show bubbles
            if (bubbleCoroutine != null) StopCoroutine(bubbleCoroutine);
                bubbleCoroutine = StartCoroutine(PlaceBubbles(1f, ray.GetPoint(5f)));

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

    private IEnumerator PlaceBubbles(float duration, Vector3 point)
    {
        bubbles.transform.position = point;
        yield return new WaitForSeconds(duration);
        bubbles.transform.position = defaultPos;
    }


    public enum clickState
    {
        None,
        Remove,
        Clean
    }
}