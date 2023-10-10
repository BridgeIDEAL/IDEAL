using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetect : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    private float maxDistance = 1.0f;

    private float sphereRadius = 1.0f;

    private Vector3 playerVector;

    private RaycastHit hit = new RaycastHit();


    void Update(){

        playerVector = playerCamera.transform.localRotation * Vector3.forward;
        if(Physics.SphereCast(transform.position, sphereRadius, playerVector, out hit, maxDistance)){
            Transform objectHit = hit.transform;
            InteractionObject interactionObject = objectHit.gameObject.GetComponent<InteractionObject>();
            interactionObject?.DetectedRay();

            if(Input.GetKeyDown(KeyCode.E)){
                interactionObject?.DetectedInteraction();
            }
        }

    }
}
