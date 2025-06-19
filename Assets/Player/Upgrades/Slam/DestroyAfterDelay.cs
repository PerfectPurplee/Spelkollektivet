using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay;

    private void Awake()
    {
        StartCoroutine(DestroyDelayCoroutine());
    }

    private IEnumerator DestroyDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
