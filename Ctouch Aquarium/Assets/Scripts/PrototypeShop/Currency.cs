using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName= "Currency")]
public class Currency : ScriptableObject
{
    public UnityEvent OnUpdate = new UnityEvent();

    public float Value { get; private set; }

    public void AddCurrency(float f)
    {
        Value += f;
        OnUpdate.Invoke();
    }

    public void RemoveCurrency(float f)
    {
        Value -= f;
        OnUpdate.Invoke();
    }

    public bool CanAfford(float price)
    {
        return Value >= price;
    }

}
