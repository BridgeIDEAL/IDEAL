using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
  //      if (other.gameObject.CompareTag("Monster") && GameManager.WholeEntityEvent.IsChase) {
		//	GameOverManager.Instance.GameOver("학생에게 끌려간 후 실종됨.");
		//}
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
            Debug.Log(collision.gameObject.name);
        
      if (collision.gameObject.CompareTag("SafeZone")){
        PenaltyPointManager.Instance.GoSafeZone(true);
      }
            
    }

    public void OnCollisionExit(Collision collision){
      if (collision.gameObject.CompareTag("SafeZone")){
        PenaltyPointManager.Instance.GoSafeZone(false);
      }
    }
}
