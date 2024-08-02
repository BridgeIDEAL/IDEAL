using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���߿� ���� ��ũ��Ʈ
/// </summary>
public class LastDoorOpen : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject frontDoors;
    Quaternion destQuaternion = new Quaternion(0,90,0,1);
    #region Variable Player Place
    public PlaceTriggerType PlayerInPlace { get; set; } = PlaceTriggerType.None;
    #endregion

    private void Awake()
    {
        //if (EntityDataManager.Instance.IsLastEvent)
        //    OpenFrontDoors();
    }

    public void PlayerInRestPlaceEvent()
    {
        // �޽� ���� ���� ��
    }

    public void OpenFrontDoors()
    {
        StartCoroutine(RotateDoor());
    }
    
    public IEnumerator RotateDoor()
    {
        float timer = 0f;
        Quaternion startRotate = frontDoors.transform.rotation;
        while (timer < rotateSpeed)
        {
            timer += Time.deltaTime;
            frontDoors.transform.rotation = Quaternion.Lerp(startRotate, destQuaternion, timer / rotateSpeed);
            yield return null;
        }
    }
}
