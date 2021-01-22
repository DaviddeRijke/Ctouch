using System.Collections;
using UnityEngine;

public class SpawnAnimator : MonoBehaviour
{
    [SerializeField] private float spawnSpeed = 1f;
    private Vector3 scale;
    [SerializeField] private AnimationCurve appearCurve;

    private void OnEnable()
    {
        StartCoroutine(Appear(spawnSpeed));
    }

    private IEnumerator Appear(float duration)
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(Random.Range(.1f, .5f));

        for (float timePassed = 0; timePassed < duration; timePassed += Time.deltaTime)
        {
            transform.localScale =
                Vector3.LerpUnclamped(Vector3.zero, scale, appearCurve.Evaluate(timePassed / duration));
            yield return null;
        }
    }
}