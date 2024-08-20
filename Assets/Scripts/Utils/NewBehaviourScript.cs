using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public GameObject Parent;
    public Material Material;
    public bool DeactivateParentAfterMerge = true;
    public bool DestroyParentAfterMerge = false;

    [ContextMenu("Merge")]
    public void MergeMeshes()
    {
        MeshFilter[] meshFilters = Parent.GetComponentsInChildren<MeshFilter>();
        List<CombineInstance> combineList = new List<CombineInstance>();

        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].sharedMesh != null)
            {
                CombineInstance combineInstance = new CombineInstance
                {
                    mesh = meshFilters[i].sharedMesh,
                    transform = meshFilters[i].transform.localToWorldMatrix
                };
                combineList.Add(combineInstance);
            }
        }

        GameObject combinedObject = new GameObject("Combined Mesh");
        combinedObject.AddComponent<MeshFilter>();
        combinedObject.AddComponent<MeshRenderer>();
        combinedObject.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        combinedObject.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combineList.ToArray());
        combinedObject.GetComponent<MeshRenderer>().material = Material;

        if (DeactivateParentAfterMerge)
        {
            Parent.SetActive(false);
        }

        if (DestroyParentAfterMerge)
        {
            Destroy(Parent);
        }

    }

}