using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPoint : MonoBehaviour
{
    [SerializeField] private Image[] BodyImages;    // IdealBodyPart를 따름

    private Color normalColor = Color.white;
    private Color damagedColor = new Color(1.0f, 0.5f, 0.5f, 0.7f);
    private Color deletedColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);

    public void UpdateBodyImage(IdealBodyPart idealBodyPart, int hp){
        if(hp >= HealthPointManager.maxHP){
            BodyImages[(int)idealBodyPart].color = normalColor;
        }
        else if( hp > HealthPointManager.minHP){
            BodyImages[(int)idealBodyPart].color = damagedColor;
        }
        else{
            BodyImages[(int)idealBodyPart].color = deletedColor;
        }
    }

}
