using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIItemTooltip : MonoBehaviour
{
    /***********************************************************************
    *                           Inspector Option Fields
    ***********************************************************************/

    #region Option Fields
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;

    #endregion

    /***********************************************************************
    *                               Private Fields
    ***********************************************************************/
    #region Private Fields
    [SerializeField] private RectTransform rectTransform;
    #endregion

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    #region Unity Events
    private void Awake(){
        Init();
    }

    #endregion

    /***********************************************************************
    *                               Private Methods
    ***********************************************************************/
    #region Private Methods
    private void Init(){
        DisableAllChildrenRaycastTarget(transform);
    }

    /// <summary> 모든 지식 UI에 레이캐스트 타겟 해제 </summary>
    private void DisableAllChildrenRaycastTarget(Transform tr){

        /// 본인이 Graphic을 상속하면 레이캐스트 타겟해제
        tr.TryGetComponent(out Graphic gr);
        if(gr != null){
            gr.raycastTarget = false;
        }

        // 자식이 없으면 종료
        int childCount = tr.childCount;
        if(childCount == 0) return;

        for(int i = 0; i < childCount; i++){
            DisableAllChildrenRaycastTarget(tr.GetChild(i));
        }
    }

    #endregion

    /***********************************************************************
    *                               Public Methods
    ***********************************************************************/
    #region Public Methods
    /// <summary> 툴팁 UI에 아이템 정보 등록 </summary>
    public void SetItemInfo(ItemData data){
        titleText.text = data.Name;
        contentText.text = data.Tooltip;
    }

    /// <summary> 툴팁의 위치 조정 </summary>
    public void SetRectPosition(){
        float tooltipWidth = rectTransform.sizeDelta.x;
        float tooltipHeight = rectTransform.sizeDelta.y;
        
        float dirY, dirX;
        dirY = (Input.mousePosition.y > (Screen.height / 2)) ? -1 : 1; 
        dirX = (Input.mousePosition.x > (Screen.height / 2)) ? -1 : 1;
        rectTransform.position = new Vector2(dirX * tooltipWidth / 2 + Input.mousePosition.x, dirY * tooltipHeight / 2 + Input.mousePosition.y);
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    #endregion

}
