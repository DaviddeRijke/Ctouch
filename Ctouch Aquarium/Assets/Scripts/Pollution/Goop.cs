using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Goop : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private Vector2 minMaxSize;
    private List<float> targets = new List<float>();
    private List<float> currents = new List<float>();

    private void Start()
    {
        StartCoroutine(GooMove(1f));
    }

    private IEnumerator GooMove(float duration)
    {
        targets.Clear();
        for (int i = 0; i < transforms.Length; i++) targets.Add(Random.Range(minMaxSize.x, minMaxSize.y));
        currents.Clear();
        for (int i = 0; i < transforms.Length; i++) currents.Add(transforms[i].localScale.x);

        for (float passedTime = 0; passedTime < duration; passedTime += Time.deltaTime)
        {
            for (int j = 0; j < transforms.Length; j++)
            {
                transforms[j].localScale = Vector3.Lerp(new
                        Vector3(currents[j], currents[j], currents[j]),
                    new Vector3(targets[j], targets[j], targets[j]),
                    passedTime / duration);
            }

            yield return null;
        }

        StartCoroutine(GooMove(duration));
    }
}