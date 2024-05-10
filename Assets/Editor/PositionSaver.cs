using UnityEngine;
using UnityEditor;

public class PositionSaver : MonoBehaviour
{
    [MenuItem("Custom/Save Position")]
    public static void SavePosition()
    {
        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {
            Vector3 position = obj.transform.position;
            EditorPrefs.SetFloat(obj.name + "_PosX", position.x);
            EditorPrefs.SetFloat(obj.name + "_PosY", position.y);
            EditorPrefs.SetFloat(obj.name + "_PosZ", position.z);
            Debug.Log("Position saved for " + obj.name);
        }
    }

    [MenuItem("Custom/Load Position")]
    public static void LoadPosition()
    {
        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {
            float posX = EditorPrefs.GetFloat(obj.name + "_PosX");
            float posY = EditorPrefs.GetFloat(obj.name + "_PosY");
            float posZ = EditorPrefs.GetFloat(obj.name + "_PosZ");
            obj.transform.position = new Vector3(posX, posY, posZ);
            Debug.Log("Position loaded for " + obj.name);
        }
    }
}
