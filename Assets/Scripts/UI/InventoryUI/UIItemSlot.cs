using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    /***********************************************************************
    *                               Option Fields
    ***********************************************************************/
    #region Option Fields
    [Tooltip("아이템 아이콘 이미지")]
    [SerializeField] private Image iconImage;

    [Tooltip("아이템 개수 텍스트")]
    [SerializeField] private TextMeshProUGUI amountText;

    [Tooltip("아이템 이름 텍스트")]
    [SerializeField] private TextMeshProUGUI nameText;


    [Tooltip("슬롯을 지정할 때 나타나는 하이라이트 이미지")]
    [SerializeField] private Image highlightImage;

    [Space]
    [Tooltip("하이라이트 이미지 알파 값")]
    [SerializeField] private float highlightAlpha = 0.5f;

    [Tooltip("하이라이트 소요 시간")]
    [SerializeField] private float highlightFadeDuration = 0.2f;
    #endregion

    /***********************************************************************
    *                               Properties
    ***********************************************************************/
    #region Properties
    /// <summary> 슬롯 인덱스 </summary>
    public int Index {get; private set;}

    /// <summary> 슬롯이 아이템을 보유하고 있는지 여부 </summary>
    public bool HasItem => iconImage.sprite != null;

    /// <summary> 접근 가능한 슬롯인지 여부 </summary>
    public bool IsAccessible => isAccessibleSlot && isAccessibleItem;

    public RectTransform SlotRect => slotRect;
    public RectTransform IconRect => iconRect;

    #endregion

    /***********************************************************************
    *                               Fields
    ***********************************************************************/
    #region Fields

    private UIInventory uIInventory;

    private RectTransform slotRect;
    private RectTransform iconRect;
    private RectTransform highlightRect;

    private GameObject iconGameObject;
    private GameObject amountTextGameObject;
    private GameObject nameTextGameObject;
    private GameObject highlightGameObject;

    private Image slotImage;


    /// <summary> 현재 하이라이트 알파값 </summary>
    private float currentHLAlpha = 0.0f;

    private bool isAccessibleSlot = true;
    private bool isAccessibleItem = true;

    /// <summary> 비활성화된 아이콘 색상 </summary>
    private static readonly Color InaccessibleIconColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    #endregion

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    #region Unity Events
    private void Awake(){
        InitComponents();
        InitValues();
    }

    #endregion

    /***********************************************************************
    *                               Private Methods
    ***********************************************************************/
    #region Private Methods

    private void InitComponents(){
        uIInventory = GetComponentInParent<UIInventory>();

        // Rects
        slotRect = GetComponent<RectTransform>();
        iconRect = iconImage.rectTransform;
        highlightRect = highlightImage.rectTransform;

        // Game Objects
        iconGameObject = iconRect.gameObject;
        amountTextGameObject = amountText.gameObject;
        nameTextGameObject = nameText.gameObject;
        highlightGameObject = highlightImage.gameObject;

        // Images
        slotImage = GetComponent<Image>();
    }

    private void InitValues(){
        // Image
        iconImage.raycastTarget = false;
        highlightImage.raycastTarget = false;

        // Text
        nameText.raycastTarget = false;
        amountText.raycastTarget = false;

        // Deactive Icon
        HideIcon();
        highlightGameObject.SetActive(false);
    }


    private void ShowIcon() => iconGameObject.SetActive(true);
    private void HideIcon() => iconGameObject.SetActive(false);

    private void ShowAmountText() => amountTextGameObject.SetActive(true);
    private void HideAmountText() => amountTextGameObject.SetActive(false);

    private void ShowNameText() => nameTextGameObject.SetActive(true);
    private void HideNameText() => nameTextGameObject.SetActive(false);

    #endregion


    /***********************************************************************
    *                               Public Methods
    ***********************************************************************/
    #region Public Methods

    public void SetSlotIndex(int index) => Index = index;


    /// <summary> 아이템 활성화 비활성화 여부 설정 </summary>
    public void SetItemAccessibleState(bool value){
        if(isAccessibleItem == value) return;
        if(value){
            iconImage.color = Color.white;
            amountText.color = Color.white;
            nameText.color = Color.white;
        }
        else{
            iconImage.color = InaccessibleIconColor;
            amountText.color = InaccessibleIconColor;
            nameText.color = InaccessibleIconColor;
        }

        isAccessibleItem = value;
    }


    /// 다른 슬롯과 아이템 교환 하지 않음
    

    /// <summary> 슬롯에 아이템 등록 </summary>
    public void SetItem(Sprite itemSprite){
        if(itemSprite != null){
            iconImage.sprite = itemSprite;
            ShowIcon();
        }
        else{
            RemoveItem();
        }
    }

    /// <summary> 슬롯에서 아이템 제거 </summary>
    public void RemoveItem(){
        iconImage.sprite = null;
        HideIcon();

        amountText.text = "";
        HideAmountText();

        nameText.text = "";
        HideNameText();
    }

    /// <summary> 아이템 이미지 투명도 설정 </summary>
    public void SetIconAlpha(float alpha){
        iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, alpha);
    }

    /// <summary> 아이템 개수 텍스트 설정, amount 1 이하면 텍스트 비활성화 </summary>
    public void SetItemAmount(int amount){
        amountText.text = amount.ToString();
        
        if(HasItem && amount > 1){
            ShowAmountText();
        }
        else{
            HideAmountText();
        }
    }


    /// <summary> 아이템 이름 텍스트 설정 빈 string일 경우 텍스트 비활성화 </summary>
    public void SetItemName(string name){
        nameText.text = name;
        if(name != ""){
            ShowNameText();
        }
        else{
            HideNameText();
        }
    }

    /// <summary> 슬롯에 하이라이트 표시/ 해제 </summary>
    public void Highlight(bool show){
        if(!this.IsAccessible) return;

        if(show){
            StartCoroutine(nameof(HighlightFadeInRoutine));
        }
        else{
            StartCoroutine(nameof(HighlightFadeOutRoutine));
        }
    }

    /// <summary> 하이라이트 이미지를 아이콘 아미지의 상단/하단으로 표시 </summary>
    public void SetHighlightOnTop(bool value){
        if(value){
            highlightRect.SetAsLastSibling();
        }
        else{
            highlightRect.SetAsFirstSibling();
        }
    }

    #endregion

    /***********************************************************************
    *                               Coroutines
    ***********************************************************************/
    #region Coroutines
    /// <summary> 하이라이트 알파값 서서히 증가 </summary>
    private IEnumerator HighlightFadeInRoutine(){
        StopCoroutine(nameof(HighlightFadeOutRoutine));
        highlightGameObject.SetActive(true);

        float unit = highlightAlpha / highlightFadeDuration;

        for (; currentHLAlpha <= highlightAlpha; currentHLAlpha += unit * Time.deltaTime)
        {
            highlightImage.color = new Color(
                highlightImage.color.r,
                highlightImage.color.g,
                highlightImage.color.b,
                currentHLAlpha
            );

            yield return null;
        }
    }

    /// <summary> 하이라이트 알파값 0%까지 서서히 감소 </summary>
    private IEnumerator HighlightFadeOutRoutine(){
        StopCoroutine(nameof(HighlightFadeInRoutine));

        float unit = highlightAlpha / highlightFadeDuration;

        for (; currentHLAlpha >= 0f; currentHLAlpha -= unit * Time.deltaTime)
        {
            highlightImage.color = new Color(
                highlightImage.color.r,
                highlightImage.color.g,
                highlightImage.color.b,
                currentHLAlpha
            );

            yield return null;
        }

        highlightGameObject.SetActive(false);
    }


    #endregion
}
