using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
  //      if (other.gameObject.CompareTag("Monster") && GameManager.WholeEntityEvent.IsChase) {
		//	GameOverManager.Instance.GameOver("학생에게 끌려간 후 실종됨.");
		//}
      if (other.gameObject.CompareTag("Rest")){
          PenaltyPointManager.Instance.GoSafeZone(true);
        }
    }
    public void OnTriggerExit(Collider other){
      if (other.gameObject.CompareTag("Rest")){
        PenaltyPointManager.Instance.GoSafeZone(false);
      }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
            Debug.Log(collision.gameObject.name);

            
    }

    
}
