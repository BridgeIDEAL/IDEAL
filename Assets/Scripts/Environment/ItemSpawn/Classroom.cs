using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    [Header("청소 시스템 정보")]
    CleaningTrigger cleaningTrigger;
    [SerializeField] int classID = 0;
    [SerializeField] CleanType cleanType = CleanType.None;
    [SerializeField, Tooltip("명찰을 랜덤 생성하기 위해 필요")] ClassroomCabinet[] cabinets;

    [Header("청소 시스템 위치")]
    [SerializeField, Tooltip("청소할 위치의 부모")] GameObject spawnPosParent;
    [SerializeField, Tooltip("청소해야 하는 캐비넷 위치")] Transform[] cabinetCleanPositions;
    [SerializeField, Tooltip("청소해야 하는 칠판 위치")] Transform[] boardCleanPosisions;
    [SerializeField, Tooltip("청소해야 하는 땅 위치")] Transform[] floorCleanPositions;
    [SerializeField, Tooltip("청소를 하지 않을 때, 감지하는 위치")] Transform cleanEventTriggerPosition;

    [Header("원본 게임오브젝트")]
    [SerializeField, Tooltip("명찰")] GameObject nameTagFabs;
    [SerializeField, Tooltip("청소하지 않고 나가는 것을 감지하기 위해 필요")] GameObject cleanTriggerFab;
    [SerializeField, Tooltip("청소, (0:캐비넷, 1:칠판, 2:바닥)")] GameObject[] cleanFabs;
    
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
