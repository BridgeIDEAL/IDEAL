using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRayCaster : MonoBehaviour
{
    // Singleton 사용하기 위한 변수
    private static UIRayCaster instance = null;
    public static UIRayCaster Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> rrList;


    /***********************************************************************
    *                               Unity Event Methods
    ***********************************************************************/
    #region Unity Event Methods
    private void Awake(){
        // Singleton 할당
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        
        TryGetComponent(out graphicRaycaster);
        if(graphicRaycaster == null){
            graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        } 

        // Graphic Raycaster
        pointerEventData = new PointerEventData(EventSystem.current);
        rrList = new List<RaycastResult>(10);
    }

    private void Update(){
        pointerEventData.position = Input.mousePosition;
    }

    #endregion

    /***********************************************************************
    *                               Mouse Event Methods
    ***********************************************************************/

    #region Mouse Event Methods

    /// <summary> 레이캐스트하여 얻은 첫 번째 UI에서 컴포넌트 찾아 리턴 </summary>
    public T RaycastAndGetFirstComponent<T>() where T : Component{
        rrList.Clear();

        graphicRaycaster.Raycast(pointerEventData, rrList);
        
        if(rrList.Count == 0)
            return null;
            
        return rrList[0].gameObject.GetComponent<T>();
    }

    #endregion

}
