using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LastTimelineFadeUI : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    public void StartTimeline() { director.Play(); }
}
