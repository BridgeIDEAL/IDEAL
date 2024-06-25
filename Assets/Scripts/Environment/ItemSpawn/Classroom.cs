using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    [Header("����")]
    bool isNobodyInClass = false;
    bool isOpenAllWindow = false;
    public bool IsCleanClassroom { get; private set; } = false;
    [SerializeField] bool isSpawnNameTag = false;

    [Header("���� û��")]
    [SerializeField] ClassroomCleanType currentType = ClassroomCleanType.None;
    [SerializeField, Tooltip("û���� ��ǰ���� ����� ��ġ�� �θ� : û�� �̺�Ʈ �߻� �� �ڵ�� ����")] GameObject allTransformParent;
    [SerializeField] Transform boardCleanTf;
    [SerializeField] Transform cabinetCleanTf;
    [SerializeField] Transform[] floorCleanTf;

    [Header("��ǰ��")]
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
    /// Null Reference�� ���� ����� ������ �����ϴ� �ڵ�
    /// </summary>
    public void LinkComponent()
    {
        // â����
        int windowCnt = classroomWindow.Length;
        if (windowCnt == 0)
        {
            classroomWindow = GetComponentsInChildren<ClassroomWindow>();
        }
        // ������ ������ ĳ��ݵ�
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
        // û�ҵ�����
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
    /// û�� �̺�Ʈ�� ���� �� �ִ� ���� �ϼ��ߴ����� ���� 
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
    /// �ϳ��� ������ ����
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
    /// �ټ��� ������ ����
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
        // û�� ��!
        IsCleanClassroom = true;
        // ���� ����
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
