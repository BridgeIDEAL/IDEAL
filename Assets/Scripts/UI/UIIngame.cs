using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : MonoBehaviour
{
    [SerializeField] private Image visualFilter;

    public void SetVisualFilter(float ratio){
        Color color = visualFilter.color;
        color.a = ratio;
        visualFilter.color = color;
    }
}
