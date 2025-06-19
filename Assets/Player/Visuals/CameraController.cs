using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float cameraLerpSpeed;
    [SerializeField]
    private float targetInfluence;

    private new Transform transform;

    [Header("Shake")]
    private float shakeScale;
    [SerializeField]
    private float noiseScale = 1;
    [SerializeField]
    private AnimationCurve shakeScaleOverTime;
    [SerializeField]
    private float timeToForceRatio = 1;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        Instance = this;
        StartShake(3);
    }

    private void LateUpdate()
    {
        //Vector3 desiredPosition = Vector3.Lerp(player.position, targetPoint.position, targetInfluence);
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraLerpSpeed);
        transform.position = player.position + 
            (new Vector3(
                Mathf.PerlinNoise1D(Time.time * noiseScale), 
                Mathf.PerlinNoise1D(Time.time * noiseScale + 100),
                Mathf.PerlinNoise1D(Time.time * noiseScale + 200)) - Vector3.one / 2) * shakeScale;
    }

    public static void StartShake(float force)
    {
        Instance.StartCoroutine(Instance.Shake(force));
    }

    private IEnumerator Shake(float force)
    {
        yield return new WaitForSeconds(2);
        float duration = force * timeToForceRatio;
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            shakeScale = shakeScaleOverTime.Evaluate(progress) * force;
            yield return new WaitForEndOfFrame();
        }
    }
}
