using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        Debug.Log("trigger tag: " + other.gameObject.name);
		Debug.Log("EntityEvent: " + GameManager.EntityEvent.IsChase);
        if (other.gameObject.CompareTag("Monster") && GameManager.EntityEvent.IsChase) {
			GameOverManager.Instance.GameOver("학생에게 끌려간 후 실종됨.");
		}
    }
}
