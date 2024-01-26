//The implementation is based on this article:http://rbarraza.com/html5-canvas-pageflip/
//As the rbarraza.com website is not live anymore you can get an archived version from web archive 
//or check an archived version that I uploaded on my website: https://dandarawy.com/html5-canvas-pageflip/

// 기존 sprite만 변경하는 기능을 개선하기 위해 Book.cs의 코드를 수정함

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;
[ExecuteInEditMode]
public class IdealBook : MonoBehaviour {
    public TempEffectSound tempEffectSound; //jun
    public Canvas canvas;

    [SerializeField]
    RectTransform BookPanel;
    public Sprite background;
    public Sprite bookCoverFront;
    public Sprite bookCoverBack;
    public Sprite bookInside_Left;
    public Sprite bookInside_Right;
    public List<string> bookPages;
    public bool interactable=true;
    public bool enableShadowEffect=true;
    //represent the index of the sprite shown in the right page
    public int currentPage = 0;
    public int TotalPageCount
    {
        get { return bookPages.Count; }
    }
    public Vector3 EndBottomLeft
    {
        get { return ebl; }
    }
    public Vector3 EndBottomRight
    {
        get { return ebr; }
    }
    public float Height
    {
        get
        {
            return BookPanel.rect.height ; 
        }
    }
    private int heightLevel;
    public Image ClippingPlane;
    public Image NextPageClip;
    public Image Shadow;
    public Image ShadowLTR;
    public Image Left;
    public TextMeshProUGUI LeftTMP;
    public Image LeftNext;
    public TextMeshProUGUI LeftNextTMP;
    public Image Right;
    public TextMeshProUGUI RightTMP;
    public Image RightNext;
    public TextMeshProUGUI RightNextTMP;

    public UnityEvent OnFlip;
    float radius1, radius2;
    //Spine Bottom
    Vector3 sb;
    //Spine Top
    Vector3 st;
    //corner of the page
    Vector3 c;
    //Edge Bottom Right
    Vector3 ebr;
    //Edge Top Right
    Vector3 etr;
    //Edge Bottom Left
    Vector3 ebl;
    //Edge Top Left
    Vector3 etl;
    //follow point 
    Vector3 f;
    bool pageDragging = false;
    //current flip mode
    FlipMode mode;

    void Start()
    {
        if (!canvas) canvas=GetComponentInParent<Canvas>();
        if (!canvas) Debug.LogError("Book should be a child to canvas");

        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        UpdateSprites();
        CalcCurlCriticalPoints();

        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        NextPageClip.rectTransform.sizeDelta = new Vector2(pageWidth, pageHeight + pageHeight * 2);


        ClippingPlane.rectTransform.sizeDelta = new Vector2(pageWidth * 2 + pageHeight, pageHeight + pageHeight * 2);

        //hypotenous (diagonal) page length
        float hyp = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
        float shadowPageHeight = pageWidth / 2 + hyp;

        Shadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        Shadow.rectTransform.pivot = new Vector2(1, (pageWidth / 2) / shadowPageHeight);

        ShadowLTR.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        ShadowLTR.rectTransform.pivot = new Vector2(0, (pageWidth / 2) / shadowPageHeight);

    }

    private void CalcCurlCriticalPoints()
    {
        sb = new Vector3(0, -BookPanel.rect.height / 2);
        ebr = new Vector3(BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        etr = new Vector3(BookPanel.rect.width / 2, BookPanel.rect.height / 2);
        ebl = new Vector3(-BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        etl = new Vector3(-BookPanel.rect.width / 2, BookPanel.rect.height / 2);
        st = new Vector3(0, BookPanel.rect.height / 2);
        radius1 = Vector2.Distance(sb, ebr);
        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        radius2 = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
    }

    public Vector3 transformPoint(Vector3 mouseScreenPos)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Vector3 mouseWorldPos = canvas.worldCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, canvas.planeDistance));
            Vector2 localPos = BookPanel.InverseTransformPoint(mouseWorldPos);

            return localPos;
        }
        else if (canvas.renderMode == RenderMode.WorldSpace)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 globalEPR = transform.TransformPoint(ebr);
            Vector3 globalEPL = transform.TransformPoint(ebl);
            if(heightLevel == 2){
                globalEPR = transform.TransformPoint(etr);
                globalEPL = transform.TransformPoint(etl);
            }
            Vector3 globalSt = transform.TransformPoint(st);
            Plane p = new Plane(globalEPR, globalEPL, globalSt);
            float distance;
            p.Raycast(ray, out distance);
            Vector2 localPos = BookPanel.InverseTransformPoint(ray.GetPoint(distance));
            return localPos;
        }
        else
        {
            //Screen Space Overlay
            Vector2 localPos = BookPanel.InverseTransformPoint(mouseScreenPos);
            return localPos;
        }
    }
    void Update()
    {
        if (pageDragging && interactable)
        {
            UpdateBook();
        }
    }

    public void OpenBook(){
        this.gameObject.SetActive(true);
        UIGuideLogManager.Instance.OpenBook();
        UpdateSprites();
    }

    public void CloseBook(){
        this.gameObject.SetActive(false);
    }

    public void UpdateBookPage(List<string> pages){
        bookPages = pages;
    }
    public void UpdateBook()
    {
        f = Vector3.Lerp(f, transformPoint(Input.mousePosition), Time.deltaTime * 10);
        tempEffectSound.PlayEffectSound(TempEffectSounds.PaperTurn); // jun
        if (mode == FlipMode.RightToLeft)
            UpdateBookRTLToPoint(f);
        else
            UpdateBookLTRToPoint(f);
    }
    public void UpdateBookLTRToPoint(Vector3 followLocation)
    {
        mode = FlipMode.LeftToRight;
        f = followLocation;
        ShadowLTR.transform.SetParent(ClippingPlane.transform, true);
        ShadowLTR.transform.localPosition = new Vector3(0, 0, 0);
        ShadowLTR.transform.localEulerAngles = new Vector3(0, 0, 0);
        Left.transform.SetParent(ClippingPlane.transform, true);

        Right.transform.SetParent(BookPanel.transform, true);
        Right.transform.localEulerAngles = Vector3.zero;
        LeftNext.transform.SetParent(BookPanel.transform, true);

        c = Calc_C_Position(followLocation);
        Vector3 t1;
        Vector3 edgePoint = ebl;
        if(heightLevel == 0){
            edgePoint = ebl;
        }
        else if(heightLevel == 2){
            edgePoint = etl;
        }
        float clipAngle = CalcClipAngle(c, edgePoint, out t1);
        //0 < T0_T1_Angle < 180
        clipAngle = (clipAngle + 180) % 180;

        ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle - 90);
        if(heightLevel == 1){
            ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);

        //page position and angle
        
        Left.transform.position = BookPanel.TransformPoint(c);
        Right.rectTransform.pivot = new Vector2(0,0);
        Left.rectTransform.pivot = new Vector2(1,0);
        if(heightLevel == 2){
            Left.rectTransform.pivot = new Vector2(1,1);
        }
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        
        
        Left.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - 90 - clipAngle);
        if(heightLevel == 1){
            Left.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        NextPageClip.transform.localEulerAngles = new Vector3(0, 0, clipAngle - 90);
        if(heightLevel == 1){
            NextPageClip.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        NextPageClip.transform.position = BookPanel.TransformPoint(t1);
        LeftNext.transform.SetParent(NextPageClip.transform, true);
        Right.transform.SetParent(ClippingPlane.transform, true);
        Right.transform.SetAsFirstSibling();

        ShadowLTR.rectTransform.SetParent(Left.rectTransform, true);
        ShadowLTR.rectTransform.pivot = new Vector2(0, 0.22f);
        if(heightLevel == 2){
            ShadowLTR.rectTransform.pivot = new Vector2(0, 0.78f);
        }
    }
    public void UpdateBookRTLToPoint(Vector3 followLocation)
    {
        mode = FlipMode.RightToLeft;
        f = followLocation;
        Shadow.transform.SetParent(ClippingPlane.transform, true);
        Shadow.transform.localPosition = Vector3.zero;
        Shadow.transform.localEulerAngles = Vector3.zero;
        Right.transform.SetParent(ClippingPlane.transform, true);

        Left.transform.SetParent(BookPanel.transform, true);
        Left.transform.localEulerAngles = Vector3.zero;
        RightNext.transform.SetParent(BookPanel.transform, true);
        c = Calc_C_Position(followLocation);
        Vector3 t1;
        Vector3 edgePoint = ebr;
        if(heightLevel == 0){
            edgePoint = ebr;
        }
        else if(heightLevel ==2){
            edgePoint = etr;
        }
        float clipAngle = CalcClipAngle(c, edgePoint, out t1);
        if (clipAngle > -90) clipAngle += 180;

        // ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        if(heightLevel == 1){
            ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);

        //page position and angle
        Right.transform.position = BookPanel.TransformPoint(c);
        Left.rectTransform.pivot = new Vector2(0,0);
        Right.rectTransform.pivot = new Vector2(0,0);
        if(heightLevel == 2){
            Right.rectTransform.pivot = new Vector2(0,1);
        }
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;

        Right.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - (clipAngle + 90));
        if(heightLevel == 1){
            Right.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        
        NextPageClip.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        if(heightLevel == 1){
            NextPageClip.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        
        NextPageClip.transform.position = BookPanel.TransformPoint(t1);
        RightNext.transform.SetParent(NextPageClip.transform, true);
        Left.transform.SetParent(ClippingPlane.transform, true);
        Left.transform.SetAsFirstSibling();

        Shadow.rectTransform.SetParent(Right.rectTransform, true);
        Shadow.rectTransform.pivot = new Vector2(1, 0.22f);
        if(heightLevel == 2){
            Shadow.rectTransform.pivot = new Vector2(1, 0.78f);
        }
    }
    private float CalcClipAngle(Vector3 c,Vector3 bookCorner,out  Vector3 t1)
    {
        Vector3 t0 = (c + bookCorner) / 2;
        float T0_CORNER_dy = bookCorner.y - t0.y;
        float T0_CORNER_dx = bookCorner.x - t0.x;
        float T0_CORNER_Angle = Mathf.Atan2(T0_CORNER_dy, T0_CORNER_dx);
        float T0_T1_Angle = 90 - T0_CORNER_Angle;
        
        float T1_X = t0.x - T0_CORNER_dy * Mathf.Tan(T0_CORNER_Angle);
        Vector3 sPoint = sb;
        if(heightLevel == 2){
            sPoint = st;
        }
        T1_X = normalizeT1X(T1_X, bookCorner, sPoint);
        t1 = new Vector3(T1_X, sPoint.y, 0);
        
        //clipping plane angle=T0_T1_Angle
        float T0_T1_dy = t1.y - t0.y;
        float T0_T1_dx = t1.x - t0.x;
        T0_T1_Angle = Mathf.Atan2(T0_T1_dy, T0_T1_dx) * Mathf.Rad2Deg;
        return T0_T1_Angle;
    }
    private float normalizeT1X(float t1,Vector3 corner,Vector3 sb)
    {
        if (t1 > sb.x && sb.x > corner.x)
            return sb.x;
        if (t1 < sb.x && sb.x < corner.x)
            return sb.x;
        return t1;
    }
    private Vector3 Calc_C_Position(Vector3 followLocation)
    {
        Vector3 c;
        f = followLocation;
        Vector3 firstPoint = sb;
        if(heightLevel == 0){
            firstPoint = sb;
        }
        else if(heightLevel == 2){
            firstPoint = st;
        }
        float F_FP_dy = f.y - firstPoint.y;
        float F_FP_dx = f.x - firstPoint.x;
        float F_FP_Angle = Mathf.Atan2(F_FP_dy, F_FP_dx);
        Vector3 r1 = new Vector3(radius1 * Mathf.Cos(F_FP_Angle),radius1 * Mathf.Sin(F_FP_Angle), 0) + firstPoint;

        float F_FP_distance = Vector2.Distance(f, firstPoint);
        if (F_FP_distance < radius1){
            c = f;
        }
        else{
            c = r1;
        }

        Vector3 secondPoint = st;
        if(heightLevel == 0){
            secondPoint = st;
        }
        else if(heightLevel == 2){
            secondPoint = sb;
        }
        float F_SP_dy = c.y - secondPoint.y;
        float F_SP_dx = c.x - secondPoint.x;
        float F_SP_Angle = Mathf.Atan2(F_SP_dy, F_SP_dx);
        Vector3 r2 = new Vector3(radius2 * Mathf.Cos(F_SP_Angle), radius2 * Mathf.Sin(F_SP_Angle), 0) + secondPoint;
        float C_SP_distance = Vector2.Distance(c, secondPoint);
        if (C_SP_distance > radius2){
            c = r2;
        }
        
        if(heightLevel == 1)
            c.y = -1 * Height / 2.0f;
        return c;
    }
    public void DragRightPageToPoint(Vector3 point)
    {
        if (currentPage >= bookPages.Count) return;
        pageDragging = true;
        mode = FlipMode.RightToLeft;
        f = point;


        NextPageClip.rectTransform.pivot = new Vector2(0, 0.12f);
        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        if(heightLevel == 2){
            NextPageClip.rectTransform.pivot = new Vector2(0, 0.88f);
            ClippingPlane.rectTransform.pivot = new Vector2(1, 0.65f);
        }

        Left.gameObject.SetActive(true);
        Left.rectTransform.pivot = new Vector2(0, 0);
        Left.transform.position = RightNext.transform.position;
        Left.transform.eulerAngles = new Vector3(0, 0, 0);
        if(currentPage < bookPages.Count){
            LeftTMP.text = bookPages[currentPage];
            // TO DO LeftSprite
            if(currentPage == 0){
                Left.sprite = bookCoverFront;
            }
            else{
                Left.sprite = bookInside_Right;
            }
        }
        else{
            // TO DO LeftBackground
            LeftTMP.text = "";
            Left.sprite = background;
        }
        // Left.sprite = (currentPage < bookPages.Count) ? bookPages[currentPage] : background;
        Left.transform.SetAsFirstSibling();
        
        Right.gameObject.SetActive(true);
        Right.transform.position = RightNext.transform.position;
        Right.transform.eulerAngles = new Vector3(0, 0, 0);

        if(currentPage < bookPages.Count - 1){
            RightTMP.text = bookPages[currentPage + 1];
            // TO DO RightSprite
            if(currentPage + 1 == bookPages.Count - 1){
                Right.sprite = bookCoverBack;
            }
            else{
                Right.sprite = bookInside_Left;
            }
        }
        else{
            // TO Do RightBackground
            RightTMP.text = "";
            Right.sprite = background;
        }
        // Right.sprite = (currentPage < bookPages.Length - 1) ? bookPages[currentPage + 1] : background;

        if(currentPage < bookPages.Count - 2){
            RightNextTMP.text = bookPages[currentPage + 2];
            // TO DO RightNextSprite
            RightNext.sprite = bookInside_Right;
        }
        else{
            // TO Do RightNextBackground
            RightNextTMP.text = "";
            RightNext.sprite = background;
        }
        
        // RightNext.sprite = (currentPage < bookPages.Length - 2) ? bookPages[currentPage + 2] : background;

        LeftNext.transform.SetAsFirstSibling();
        if (enableShadowEffect) Shadow.gameObject.SetActive(true);
        UpdateBookRTLToPoint(f);
    }
    public void OnMouseDragRightPage(int _heightLevel)
    {
        if (interactable){
            heightLevel = _heightLevel;
            DragRightPageToPoint(transformPoint(Input.mousePosition));
        }
        
    }
    public void DragLeftPageToPoint(Vector3 point)
    {
        if (currentPage <= 0) return;
        pageDragging = true;
        mode = FlipMode.LeftToRight;
        f = point;

        NextPageClip.rectTransform.pivot = new Vector2(1, 0.12f);
        ClippingPlane.rectTransform.pivot = new Vector2(0, 0.35f);
        if(heightLevel == 2){
            NextPageClip.rectTransform.pivot = new Vector2(1, 0.88f);
            ClippingPlane.rectTransform.pivot = new Vector2(0, 0.65f);
        }

        Right.gameObject.SetActive(true);
        Right.transform.position = LeftNext.transform.position;
        
        RightTMP.text = bookPages[currentPage - 1];
        // TO DO Right sprite
        if(currentPage - 1 == bookPages.Count - 1){
            Right.sprite = bookCoverBack;
        }
        else{
            Right.sprite = bookInside_Left;
        }
        // Right.sprite = bookPages[currentPage - 1];


        Right.transform.eulerAngles = new Vector3(0, 0, 0);
        Right.transform.SetAsFirstSibling();

        Left.gameObject.SetActive(true);
        Left.rectTransform.pivot = new Vector2(1, 0);
        Left.transform.position = LeftNext.transform.position;
        Left.transform.eulerAngles = new Vector3(0, 0, 0);
        if(currentPage >= 2){
            LeftTMP.text = bookPages[currentPage - 2];
            // TO DO LeftSprite
            if(currentPage - 2 == 0){
                Left.sprite = bookCoverFront;
            }
            else{
                Left.sprite = bookInside_Right;
            }
        }
        else{
            // TO DO LeftBackground
            LeftTMP.text = "";
            Left.sprite = background;
        }
        // Left.sprite = (currentPage >= 2) ? bookPages[currentPage - 2] : background;

        if(currentPage >= 3){
            LeftNextTMP.text = bookPages[currentPage - 3];
            // TO DO LeftNextSprite
            LeftNext.sprite = bookInside_Left;
        }
        else{
            // TO DO LeftNextBackground
            LeftNextTMP.text = "";
            LeftNext.sprite = background;
        }
        // LeftNext.sprite = (currentPage >= 3) ? bookPages[currentPage - 3] : background;

        RightNext.transform.SetAsFirstSibling();
        if (enableShadowEffect) ShadowLTR.gameObject.SetActive(true);
        UpdateBookLTRToPoint(f);
    }
    public void OnMouseDragLeftPage(int _heightLevel)
    {
        if (interactable){
            heightLevel = _heightLevel;
            DragLeftPageToPoint(transformPoint(Input.mousePosition));
        }
    }
    public void OnMouseRelease()
    {
        if (interactable)
            ReleasePage();
    }
    public void ReleasePage()
    {
        if (pageDragging)
        {
            pageDragging = false;
            float distanceToLeft = Vector2.Distance(c, ebl);
            float distanceToRight = Vector2.Distance(c, ebr);
            if (distanceToRight < distanceToLeft && mode == FlipMode.RightToLeft)
                TweenBack();
            else if (distanceToRight > distanceToLeft && mode == FlipMode.LeftToRight)
                TweenBack();
            else
                TweenForward();
        }
    }
    Coroutine currentCoroutine;
    void UpdateSprites()
    {
        if(currentPage > 0 && currentPage <= bookPages.Count){
            LeftNextTMP.text = bookPages[currentPage-1];
            // TO DO LeftNextSprite
            if (currentPage >= bookPages.Count - 1){
                LeftNext.sprite = bookCoverBack;
            }
            else{
                LeftNext.sprite = bookInside_Left;
            }
        }
        else{
            // TO DO LeftNextBackground
            LeftNextTMP.text = "";
            LeftNext.sprite = background;
        }
        // LeftNext.sprite= (currentPage > 0 && currentPage <= bookPages.Length) ? bookPages[currentPage-1] : background;

        if(currentPage>=0 && currentPage < bookPages.Count){
            RightNextTMP.text = bookPages[currentPage];
            // TO DO RightNextSprite
            if(currentPage == 0){
                RightNext.sprite = bookCoverFront;
            }
            else{
                RightNext.sprite = bookInside_Right;
            }
        }
        else{
            // TO DO RightNextBackground
            RightNextTMP.text = "";
            RightNext.sprite = background;
        }
        // RightNext.sprite=(currentPage>=0 && currentPage<bookPages.Length) ? bookPages[currentPage] : background;
    }
    public void TweenForward()
    {
        if(mode== FlipMode.RightToLeft){
            Vector3 edgePoint = ebl;
            if(heightLevel == 2){
                edgePoint = etl;
            }
            currentCoroutine = StartCoroutine(TweenTo(edgePoint, 0.15f, () => { Flip(); }));
        }
        else{
            Vector3 edgePoint = ebr;
            if(heightLevel == 2){
                edgePoint = etr;
            }
            currentCoroutine = StartCoroutine(TweenTo(edgePoint, 0.15f, () => { Flip(); }));
        }
    }
    void Flip()
    {
        if (mode == FlipMode.RightToLeft)
            currentPage += 2;
        else
            currentPage -= 2;
        LeftNext.transform.SetParent(BookPanel.transform, true);
        Left.transform.SetParent(BookPanel.transform, true);
        LeftNext.transform.SetParent(BookPanel.transform, true);
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Right.transform.SetParent(BookPanel.transform, true);
        RightNext.transform.SetParent(BookPanel.transform, true);
        UpdateSprites();
        Shadow.gameObject.SetActive(false);
        ShadowLTR.gameObject.SetActive(false);
        if (OnFlip != null)
            OnFlip.Invoke();
    }
    public void TweenBack()
    {
        if (mode == FlipMode.RightToLeft)
        {
            Vector3 edgePoint = ebr;
            if(heightLevel == 2){
                edgePoint = etr;
            }
            currentCoroutine = StartCoroutine(TweenTo(edgePoint, 0.15f,
                () =>
                {
                    UpdateSprites();
                    RightNext.transform.SetParent(BookPanel.transform);
                    Right.transform.SetParent(BookPanel.transform);

                    Left.gameObject.SetActive(false);
                    Right.gameObject.SetActive(false);
                    pageDragging = false;
                }
                ));
        }
        else
        {
            Vector3 edgePoint = ebl;
            if(heightLevel == 2){
                edgePoint = etl;
            }
            currentCoroutine = StartCoroutine(TweenTo(edgePoint, 0.15f,
                () =>
                {
                    UpdateSprites();

                    LeftNext.transform.SetParent(BookPanel.transform);
                    Left.transform.SetParent(BookPanel.transform);

                    Left.gameObject.SetActive(false);
                    Right.gameObject.SetActive(false);
                    pageDragging = false;
                }
                ));
        }
    }
    public IEnumerator TweenTo(Vector3 to, float duration, System.Action onFinish)
    {
        int steps = (int)(duration / 0.025f);
        Vector3 displacement = (to - f) / steps;
        for (int i = 0; i < steps-1; i++)
        {
            if(mode== FlipMode.RightToLeft)
            UpdateBookRTLToPoint( f + displacement);
            else
                UpdateBookLTRToPoint(f + displacement);

            yield return new WaitForSeconds(0.025f);
        }
        if (onFinish != null)
            onFinish();
    }
}
