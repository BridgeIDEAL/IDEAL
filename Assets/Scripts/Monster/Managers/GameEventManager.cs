using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use TimeLine or EntityEvent or Camera
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
        // ÈÞ½Ä °ø°£ ÁøÀÔ ½Ã
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
            Debug.Log("ÂïÈû");
            timer += Time.deltaTime;
            frontDoors[_idx].transform.rotation = Quaternion.Lerp(startRotate, endRotate, timer / rotateSpeed);
            yield return null;
        }
    }
}
