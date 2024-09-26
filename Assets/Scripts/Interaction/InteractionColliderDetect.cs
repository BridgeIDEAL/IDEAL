using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionColliderDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){

        if(other.gameObject.layer == LayerMask.NameToLayer("Interaction")){
            other.gameObject.GetComponent<InteractionObject>().DetectedCollider();
        }
    }

    private void OnTriggerExit(Collider other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Interaction")){
            other.gameObject.GetComponent<InteractionObject>().OutOfCollider();
        }
    }
}
