using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class InteractionOutline : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    [SerializeField] private bool reverseY = false;
    [SerializeField] private bool[] outlineMask;

    private GameObject outlineObject;
    private Renderer outlineRenderer = null;   // Renderer가 하나인 오브젝트의 경우
    private List<Renderer> outlineRenderers = new List<Renderer>();    // Renderer가 자식 오브젝트에 있어 여러 개인 경우

    void Start()
    {
        CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        SetOutlineObject(false);
    }

    void CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlineObject.transform.localScale = new Vector3(1, 1, 1);

        outlineObject.GetComponent<InteractionOutline>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        // OutlineObject가 반대로 되어 있을때 y축 반전
        if (reverseY)
        {
            Vector3 currentRotation = outlineObject.transform.rotation.eulerAngles;
            currentRotation.y += 180f;
            outlineObject.transform.rotation = Quaternion.Euler(currentRotation);
        }

        Renderer rend = outlineObject.GetComponent<Renderer>();

        if (rend != null)
        {   // Renderer가 하나인 경우
            rend.material = outlineMat;
            rend.material.SetColor("_OutlineColor", color);
            rend.material.SetFloat("_Scale", scaleFactor);
            rend.shadowCastingMode = ShadowCastingMode.Off;
            rend.enabled = true;
            outlineRenderer = rend;
        }
        else
        {   // Renderer가 여러 개인 경우, 현재는 자식까지만 가능 손자까지 탐색하진 않음
            if (outlineObject.transform.childCount > 0)
            {
                for (int i = 0; i < outlineObject.transform.childCount; i++)
                {
                    if (outlineMask.Length > 0 && outlineMask[i] == false) continue;    // OutlineMask에서 제외되는 경우 복제품의 material을 변경하지 않음)
                    Transform outlineChild = outlineObject.transform.GetChild(i);
                    rend = outlineChild.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        rend.material = outlineMat;
                        rend.material.SetColor("_OutlineColor", color);
                        rend.material.SetFloat("_Scale", scaleFactor);
                        rend.shadowCastingMode = ShadowCastingMode.Off;
                        rend.enabled = true;
                        outlineRenderers.Add(rend);
                    }
                }
            }
            else
            {
                Debug.Log("Renderer를 찾을 수 없습니다.");
                return;
            }
        }

    }

    public void SetOutlineObject(bool active)
    {
        outlineObject.SetActive(active);
    }
}
