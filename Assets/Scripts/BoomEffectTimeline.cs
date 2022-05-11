using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BoomEffectTimeline : MonoBehaviour
{
    private PlayableDirector director;

    public GameObject controlPanel;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void StartTimeline()
    {
        Debug.Log("TIMELINE START!");
        director.Play();
    }
}
