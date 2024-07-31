using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 나중에 지울 스크립트
/// </summary>
public class GameEventManager : MonoBehaviour
{


    bool isOpen = false;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject[] frontDoors;
    #region Variable Player Place
    public PlaceTriggerType PlayerInPlace { get; set; } = PlaceTriggerType.None;
    #endregion

    public void PlayerInRestPlaceEvent()
    {
        // 휴식 공간 진입 시
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && !isOpen)
        {
            OpenFrontDoors();
            isOpen = true;
        }
    }

    public void OpenFrontDoors()
    {
        StartCoroutine(RotateDoor(0, new Vector3(0, 90, 0)));
        StartCoroutine(RotateDoor(1, new Vector3(0, -90, 0)));
    }
    
    public IEnumerator RotateDoor(int _idx, Vector3 _rotate)
    {
        float timer = 0f;
        Quaternion startRotate = frontDoors[_idx].transform.rotation;
        Quaternion endRotate = Quaternion.Euler(_rotate);
        while (timer < rotateSpeed)
        {
            Debug.Log("찍힘");
            timer += Time.deltaTime;
            frontDoors[_idx].transform.rotation = Quaternion.Lerp(startRotate, endRotate, timer / rotateSpeed);
            yield return null;
        }
    }
}
