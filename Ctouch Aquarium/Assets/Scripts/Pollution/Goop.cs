using Pollution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Goop : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private Vector2 minMaxSize;

    private Coroutine coroutine;
    public PollutionManager pollutionManager;

    private void Start() => coroutine = StartCoroutine(GooMove(1f));

    public void Remove()
    {
        StopCoroutine(coroutine);
        StartCoroutine(GooSplode(1f));
    }

    private IEnumerator GooMove(float duration)
    {
        List<float> targets = new List<float>();
        for (int i = 0; i < transforms.Length; i++) targets.Add(Random.Range(minMaxSize.x, minMaxSize.y));
        List<float> currents = new List<float>();
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

        coroutine = StartCoroutine(GooMove(duration));
    }

    private IEnumerator GooSplode(float duration)
    {
        List<Vector3> targets = new List<Vector3>();
        List<Vector3> currents = new List<Vector3>();
        List<Vector3> currentScales = new List<Vector3>();
        for (int i = 0; i < transforms.Length; i++) targets.Add(transforms[i].localPosition * 2f);
        for (int i = 0; i < transforms.Length; i++) currents.Add(transforms[i].localPosition);
        for (int i = 0; i < transforms.Length; i++) currentScales.Add(transforms[i].localScale);

        for (float passedTime = 0; passedTime < duration; passedTime += Time.deltaTime)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].transform.localPosition = Vector3.Lerp(currents[i], targets[i], passedTime / duration);
                transforms[i].transform.localScale =
                    Vector3.Lerp(currentScales[i], Vector3.zero, passedTime / duration);
            }

            yield return null;
        }

        pollutionManager.RemoveGoop();
        Destroy(gameObject);
    }
}