using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class ComponentCopyPasteWindow : EditorWindow
{
    private GameObject sourceObject;
    private GameObject targetObject;
    private Component[] copiedComponents;

    [MenuItem("Tools/Component Copy Paste")]
    public static void ShowWindow()
    {
        GetWindow<ComponentCopyPasteWindow>("Component Copy Paste");
    }

    private void OnGUI()
    {
        GUILayout.Label("Copy and Paste Components", EditorStyles.boldLabel);

        // Source object field
        sourceObject = (GameObject)EditorGUILayout.ObjectField("Source Object", sourceObject, typeof(GameObject), true);

        if (GUILayout.Button("Copy Components"))
        {
            if (sourceObject != null)
            {
                copiedComponents = sourceObject.GetComponents<Component>();
                Debug.Log($"{copiedComponents.Length} components copied from {sourceObject.name}");
            }
            else
            {
                Debug.LogWarning("Please select a source object to copy components from.");
            }
        }

        // Target object field
        targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);

        if (GUILayout.Button("Paste Components"))
        {
            if (targetObject != null && copiedComponents != null)
            {
                PasteComponents();
                Debug.Log($"Components pasted to {targetObject.name}");
            }
            else
            {
                Debug.LogWarning("Please select a target object and make sure components are copied.");
            }
        }
    }

    private void PasteComponents()
    {
        foreach (Component originalComponent in copiedComponents)
        {
            System.Type type = originalComponent.GetType();
            Component copy = targetObject.AddComponent(type);

            // Copy each field's value from the original component to the new component
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                field.SetValue(copy, field.GetValue(originalComponent));
            }
        }
    }
}
