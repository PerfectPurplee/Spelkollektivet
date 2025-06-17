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
        if (Physics.Raycast(mouseRay, out RaycastHit hit, 10000, physicsLayer))
        {
            transform.position = hit.point;
        }
        
        //transform.position = mouseRay.GetPoint(
        //    -Vector3.Dot(mouseRay.origin, Vector3.up) / 
        //    Vector3.Dot(mouseRay.direction, Vector3.up));
    }
}
