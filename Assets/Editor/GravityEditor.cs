using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(ApplyGravity))]
public class GravityEditor : Editor
{
    private Vector3[] positions; // 중력 적용 후. 위치
    private Vector3[] rotations; // 중력 적용 후, 회전값
}
