using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public Camera myCamera;
    public float myFloat;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
