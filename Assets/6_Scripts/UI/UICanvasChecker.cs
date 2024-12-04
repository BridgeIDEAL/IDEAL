using UnityEngine;
using UnityEngine.UI;

public class UICanvasChecker : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CheckCanvases();
        }
    }

    void CheckCanvases()
    {
        // 모든 Canvas 오브젝트를 찾습니다.
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        int noCanvasScalerCount = 0;
        int errorCanvasScalerCount = 0;

        foreach (Canvas canvas in canvases)
        {
            // CanvasScaler 컴포넌트를 확인합니다.
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null)
            {
                Debug.LogError($"CanvasScaler가 없는 Canvas: {GetHierarchyPath(canvas.gameObject)}");
                noCanvasScalerCount++;
                continue;
            }

            // UI Scale Mode, Reference Resolution, Match 값을 확인합니다.
            bool isCorrectScaleMode = scaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize;
            bool isCorrectResolution = scaler.referenceResolution == new Vector2(1920, 1080);
            bool isCorrectMatch = Mathf.Approximately(scaler.matchWidthOrHeight, 0.5f);

            if (!isCorrectScaleMode || !isCorrectResolution || !isCorrectMatch)
            {
                errorCanvasScalerCount++;
                Debug.LogError($"CanvasScaler 설정이 잘못된 Canvas: {GetHierarchyPath(canvas.gameObject)}");
            }
        }
        Debug.Log("CanvasScaler 체크 완료 \n CanvasScaler가 없는 Canvas: " + noCanvasScalerCount 
        +" CanvasScaler 설정이 잘못된 Canvas: " + errorCanvasScalerCount);
    }

    string GetHierarchyPath(GameObject obj)
    {
        string path = obj.name;
        Transform current = obj.transform;

        while (current.parent != null)
        {
            current = current.parent;
            path = $"{current.name}/{path}";
        }

        return path;
    }
}
