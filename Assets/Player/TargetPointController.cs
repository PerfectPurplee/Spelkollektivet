using UnityEngine;
using UnityEngine.InputSystem;

public class TargetPointController : MonoBehaviour
{
    [SerializeField]
    private LayerMask physicsLayer;

    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(mouseRay, 1000, physicsLayer);
        if (hits.Length != 0)
        {
            transform.position = hits[0].point;
        }
        //transform.position = mouseRay.GetPoint(
        //    -Vector3.Dot(mouseRay.origin, Vector3.up) / 
        //    Vector3.Dot(mouseRay.direction, Vector3.up));
    }
}
