using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionKoreanChecker : MonoBehaviour
{
    void Update()
    {
        // Check if the 'P' key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            CheckActiveInteractions();
        }
        // Check if the 'O' key is pressed
        else if (Input.GetKeyDown(KeyCode.O))
        {
            CheckAllInteractions();
        }
    }

    void CheckActiveInteractions()
    {
        Debug.Log("Korean Detect Test Start!! SetActive True Object");
        // Find all active GameObjects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the object has a component that inherits from AbstractInteraction
            AbstractInteraction interaction = obj.GetComponent<AbstractInteraction>();

            if (interaction != null)
            {
                // Call the GetDetectedString method
                string detectedString = interaction.GetTestDetectedString();

                // Check if the string contains any Korean characters
                if (ContainsKorean(detectedString))
                {
                    // Log an error with the object's full hierarchy name and the detected string
                    Debug.LogError($"Object Hierarchy: {GetFullHierarchyName(obj)}, Detected String: {detectedString}");
                }
            }
        }
    }

    void CheckAllInteractions()
    {
        Debug.Log("Korean Detect Test Start!! All Object");
        // Find all GameObjects in the scene, including inactive ones
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Skip objects that are not part of the active scene
            if (obj.hideFlags != HideFlags.None || obj.scene.name == null)
                continue;

            // Check if the object has a component that inherits from AbstractInteraction
            AbstractInteraction interaction = obj.GetComponent<AbstractInteraction>();

            if (interaction != null)
            {
                // Call the GetDetectedString method
                string detectedString = interaction.GetTestDetectedString();

                // Check if the string contains any Korean characters
                if (ContainsKorean(detectedString))
                {
                    // Log an error with the object's full hierarchy name and the detected string
                    Debug.LogError($"Object Hierarchy: {GetFullHierarchyName(obj)}, Detected String: {detectedString}");
                }
            }
        }
    }

    // Function to check if a string contains any Korean characters
    private bool ContainsKorean(string input)
    {
        foreach (char c in input)
        {
            if (c >= '\uAC00' && c <= '\uD7A3') // Unicode range for Hangul syllables
            {
                return true;
            }
        }
        return false;
    }

    // Function to get the full hierarchy name of a GameObject
    private string GetFullHierarchyName(GameObject obj)
    {
        string fullName = obj.name;
        Transform current = obj.transform.parent;

        while (current != null)
        {
            fullName = current.name + "/" + fullName;
            current = current.parent;
        }

        return fullName;
    }
}
