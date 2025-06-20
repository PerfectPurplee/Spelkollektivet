using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectOverlapingObjects : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float sphereCastRadius = 2;
    [SerializeField]
    private float minAlphaPerSecond = 5;
    [SerializeField]
    private string shaderVariableName = "MinAlpha";
    [SerializeField]
    private float YOffset = -4;

    private Dictionary<MeshRenderer, float> dict = new Dictionary<MeshRenderer, float>();

    void FixedUpdate()
    {
        Debug.Log(dict.Count);
        Vector3 origin = Camera.main.transform.position + Vector3.up * YOffset;
        Vector3 direction = Player.Player.Instance.transform.position - Camera.main.transform.position;
        direction.Normalize();
        RaycastHit[] hits = Physics.SphereCastAll(origin, sphereCastRadius, direction, 1000, layerMask);
        //Debug.Log(hits.Count());
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.TryGetComponent(out MeshRenderer meshRenderer) && meshRenderer.material.HasFloat(shaderVariableName))
            {
                if (dict.ContainsKey(meshRenderer))
                {
                    float alphaValue = dict[meshRenderer];
                    if (alphaValue > 0)
                    {
                        dict[meshRenderer] -= 2 * minAlphaPerSecond * Time.fixedDeltaTime;
                    }
                    meshRenderer.material.SetFloat(shaderVariableName, dict[meshRenderer]);
                }
                else
                {
                    dict[meshRenderer] = 1 - 2 * minAlphaPerSecond * Time.fixedDeltaTime;
                }
            }
        }
        List<MeshRenderer> meshesToRemove = new List<MeshRenderer>();
        List<MeshRenderer> meshesInDict = dict.Keys.ToList();
        foreach (MeshRenderer meshRenderer in meshesInDict)
        {
            meshRenderer.material.SetFloat(shaderVariableName, dict[meshRenderer]);
            dict[meshRenderer] += minAlphaPerSecond * Time.fixedDeltaTime;
            if (dict[meshRenderer] > 1)
            {
                dict.Remove(meshRenderer);
            }
        }
    }
}
