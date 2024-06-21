using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    [Header("û�� �ý��� ����")]
    CleaningTrigger cleaningTrigger;
    [SerializeField] int classID = 0;
    [SerializeField] CleanType cleanType = CleanType.None;
    [SerializeField, Tooltip("������ ���� �����ϱ� ���� �ʿ�")] ClassroomCabinet[] cabinets;

    [Header("û�� �ý��� ��ġ")]
    [SerializeField, Tooltip("û���� ��ġ�� �θ�")] GameObject spawnPosParent;
    [SerializeField, Tooltip("û���ؾ� �ϴ� ĳ��� ��ġ")] Transform[] cabinetCleanPositions;
    [SerializeField, Tooltip("û���ؾ� �ϴ� ĥ�� ��ġ")] Transform[] boardCleanPosisions;
    [SerializeField, Tooltip("û���ؾ� �ϴ� �� ��ġ")] Transform[] floorCleanPositions;
    [SerializeField, Tooltip("û�Ҹ� ���� ���� ��, �����ϴ� ��ġ")] Transform cleanEventTriggerPosition;

    [Header("���� ���ӿ�����Ʈ")]
    [SerializeField, Tooltip("����")] GameObject nameTagFabs;
    [SerializeField, Tooltip("û������ �ʰ� ������ ���� �����ϱ� ���� �ʿ�")] GameObject cleanTriggerFab;
    [SerializeField, Tooltip("û��, (0:ĳ���, 1:ĥ��, 2:�ٴ�)")] GameObject[] cleanFabs;
    
    private void Start()
    {
        LinkCabinets();
    }

    private void LinkCabinets()
    {
        if (cabinets.Length == 0)
            cabinets = GetComponentsInChildren<ClassroomCabinet>();
    }
    
    /// <summary>
    /// Call when you get cleaning quest
    /// </summary>
    public void SetDirtyState()
    {
        switch (cleanType){
            case CleanType.Cabinet:
                SpawnTrash(cleanFabs[classID], cabinetCleanPositions[classID]);
                SpawnTriggerArea();
                break;
            case CleanType.Board:
                SpawnTrash(cleanFabs[classID], boardCleanPosisions[classID]);
                SpawnTriggerArea();
                break;
            case CleanType.Floor:
                SpawnTrash(cleanFabs[classID], floorCleanPositions[classID]);
                SpawnTriggerArea();
                break;
            default:
                break;
        }
    }

    public void SpawnTrash(GameObject _go, Transform _tf)
    {
        GameObject go = Instantiate(_go,transform);
        Vector3 pos = _tf.position;
        go.transform.position = pos;
        if (spawnPosParent != null)
            Destroy(spawnPosParent);
    }   

    public void SpawnTriggerArea()
    {
        GameObject go = Instantiate(cleanTriggerFab, transform);
        cleaningTrigger = go.GetComponent<CleaningTrigger>();
    }

    /// <summary>
    /// Call When you clean classroom 
    /// </summary>
    public void CompleteClean()
    {
        int cabinetCounts = cabinets.Length;
        int randomNum = Random.Range(0, cabinetCounts);
        cabinets[randomNum].SpawnNameTag(nameTagFabs);
        if (cleaningTrigger == null)
            return;
        cleaningTrigger.IsCleanRoom = true;
    }
}
