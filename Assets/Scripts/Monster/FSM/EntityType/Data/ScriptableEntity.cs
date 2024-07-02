using UnityEngine;

[CreateAssetMenu(fileName ="Entity", menuName ="ScriptableEntity/EntityData")]
public class ScriptableEntity : ScriptableObject
{
    public string entityName;
    public string initDialogueName;
    public string currentDialogueName;
    public bool initSpawnState;
    public bool currentSpawnState;
    public virtual void Init() 
    {
        currentDialogueName = initDialogueName;
        currentSpawnState = initSpawnState;
    }
}
