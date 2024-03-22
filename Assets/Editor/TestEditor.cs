using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(Transform))]
public class TestEidor : Editor
{
    private Transform gravityObject;
    private Vector3 initialPosition;
    private List<Vector3> vecList = new List<Vector3>();
    public void OnEnable()
    {
        gravityObject = (Transform)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Apply Gravity"))
        {
            PlayGame();
        }

        if (GUILayout.Button("Stop Game"))
        {
            StopGame();
        }

        if (GUILayout.Button("Set"))
        {
            SetGravityPos();
        }

        if (GUILayout.Button("Apply"))
        {
            SetGravityPos();
        }

        if(GUILayout.Button("초기 구현"))
        {
            FirstGravity();
        }
    }

    private void PlayGame()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = true;
        }
    }

    private void StopGame()
    {
        if (EditorApplication.isPlaying)
        {
            gravityObject.position = initialPosition;
            EditorApplication.isPlaying = false;
        }
    }
    private void SetGravityPos()
    {
        initialPosition = gravityObject.position;
    }

    private void FirstGravity()
    {
        RaycastHit hit;
        if (Physics.Raycast(gravityObject.position, Vector3.down, out hit))
        {
            gravityObject.position = hit.point;
            
        }
    }

}
