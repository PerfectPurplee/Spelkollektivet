using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float cameraLerpSpeed;
    [SerializeField]
    private float targetInfluence;

    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        //Vector3 desiredPosition = Vector3.Lerp(player.position, targetPoint.position, targetInfluence);
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraLerpSpeed);
        transform.position = player.position;
    }
}
