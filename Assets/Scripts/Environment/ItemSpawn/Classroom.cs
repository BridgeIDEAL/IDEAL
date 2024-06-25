using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    [Header("조건")]
    bool isNobodyInClass = false;
    bool isOpenAllWindow = false;
    public bool IsCleanClassroom { get; private set; } = false;
    [SerializeField] bool isSpawnNameTag = false;

    [Header("교실 청소")]
    [SerializeField] ClassroomCleanType currentType = ClassroomCleanType.None;
    [SerializeField, Tooltip("청소할 물품들이 생기는 위치의 부모 : 청소 이벤트 발생 후 코드로 삭제")] GameObject allTransformParent;
    [SerializeField] Transform boardCleanTf;
    [SerializeField] Transform cabinetCleanTf;
    [SerializeField] Transform[] floorCleanTf;

    [Header("물품들")]
    [SerializeField] string cabinetDirtyObjectName;
    [SerializeField] string boardDirtyObjectName;
    [SerializeField] string floorDirtyObjectName;
    [SerializeField] ClassroomWindow[] classroomWindow;
    [SerializeField] ClassroomCleanTool classroomCleanTool;
    [SerializeField] List<ClassroomCabinet> cabinetList = new List<ClassroomCabinet>();
    List<ClassroomDirtyObject> classroomDirtyObjectList = new List<ClassroomDirtyObject>();

    #region InitMethod 
    public void Awake()
    {
        LinkComponent();
        InitClassState();
    }

    /// <summary>
    /// Null Reference로 인해 생기는 오류를 방지하는 코드
    /// </summary>
    public void LinkComponent()
    {
        // 창문들
        int windowCnt = classroomWindow.Length;
        if (windowCnt == 0)
        {
            classroomWindow = GetComponentsInChildren<ClassroomWindow>();
        }
        // 명찰이 생성될 캐비넷들
        int cabinetCnt = cabinetList.Count;
        ClassroomCabinet[] cabinets;
        if(cabinetCnt == 0)
        {
            cabinets = GetComponentsInChildren<ClassroomCabinet>();
            cabinetCnt = cabinets.Length;
            for(int i=0; i<cabinetCnt; i++)
            {
                cabinetList.Add(cabinets[i]);
            }
        }
        // 청소도구함
        if (classroomCleanTool == null)
        {
            classroomCleanTool = GetComponentInChildren<ClassroomCleanTool>();
        }
    }

    public void InitClassState()
    {
        // Init Window
        int windowCnt = classroomWindow.Length;
        for (int i = 0; i < windowCnt; i++)
        {
            classroomWindow[i].InitWindowState(this);
        }
        // Remove Not Need Object / Script
        if (currentType == ClassroomCleanType.None)
            Destroy(allTransformParent);
        if (!isSpawnNameTag)
        {
            int cabinetCnt = cabinetList.Count;
            for(int i=cabinetCnt-1; i>=0; i--)
            {
                cabinetList[i].RemoveThis();
            }
            cabinetList.Clear();
        }
    }
    #endregion

    #region Window & Send Out Method
    public void OpenWindow()
    {
        int windowCnt = classroomWindow.Length;
        for (int i = 0; i < windowCnt; i++)
        {
            if (!classroomWindow[i].IsOpenWindow)
                return;
        }
        isOpenAllWindow = true;
        CheckCleanStartCondition();
    }

    public void SendOutStudent()
    {
        isNobodyInClass = true;
        CheckCleanStartCondition();
    }

    /// <summary>
    /// 청소 이벤트를 받을 수 있는 조건 완수했는지를 조사 
    /// </summary>
    public void CheckCleanStartCondition()
    {
        if (isNobodyInClass && isOpenAllWindow)
            classroomCleanTool.Active(this);
    }
    #endregion

    #region Clean Method
    public void InitDirtyState()
    {
        switch (currentType)
        {
            case ClassroomCleanType.Cabinet:
                InstantiateDirtyObject(cabinetDirtyObjectName,cabinetCleanTf);
                break;
            case ClassroomCleanType.Board:
                InstantiateDirtyObject(boardDirtyObjectName,boardCleanTf);
                break;
            case ClassroomCleanType.Floor:
                InstantiateDirtyObjects(floorDirtyObjectName);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 하나의 쓰레기 형성
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_transform"></param>
    public void InstantiateDirtyObject(string _name, Transform _transform)
    {
        GameObject loadGo = IdealSceneManager.Instance.CurrentGameManager.Fab_Manager.LoadPrefab(_name);
        GameObject instGo = Instantiate(loadGo, _transform.position, Quaternion.identity);
        ClassroomDirtyObject dirtyObject = instGo.GetComponent<ClassroomDirtyObject>();
        dirtyObject.Init(this);
        classroomDirtyObjectList.Add(dirtyObject);
        Destroy(allTransformParent);
    }

    /// <summary>
    /// 다수의 쓰레기 형성
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_cnt"></param>
    public void InstantiateDirtyObjects(string _name, int _cnt=3)
    { 
        GameObject loadGo = IdealSceneManager.Instance.CurrentGameManager.Fab_Manager.LoadPrefab(_name);
        for(int i=0; i<_cnt; i++)
        {
            int randomCnt = Random.Range(i * 4, i * 4 + 4);
            GameObject instGo = Instantiate(loadGo, floorCleanTf[randomCnt].position, Quaternion.identity);
            ClassroomDirtyObject dirtyObject = instGo.GetComponent<ClassroomDirtyObject>();
            dirtyObject.Init(this);
            classroomDirtyObjectList.Add(dirtyObject);
        }
        Destroy(allTransformParent);
    }

    public void CleanClassroom()
    {
        int cnt = classroomDirtyObjectList.Count;
        for (int i = 0; i < cnt; i++)
        {
            if (!classroomDirtyObjectList[i].IsCleanTrash)
                return;
        }
        // 청소 끝!
        IsCleanClassroom = true;
        // 명찰 생성
        if (isSpawnNameTag)
        {
            int cabinetCnt = cabinetList.Count;
            int randomNum = Random.Range(0, cabinetCnt);
            cabinetList[randomNum].SpawnNameTag();
            for (int i = cabinetCnt - 1; i >= 0; i--)
            {
                cabinetList[i].RemoveThis();
            }
            cabinetList.Clear();
        }
    }
    #endregion
}
