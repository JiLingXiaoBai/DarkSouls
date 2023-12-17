using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public Camera myCamera;
    public float myFloat;

    private PlayableDirector pd;
    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        pd = playable.GetGraph().GetResolver() as PlayableDirector;
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
            {
                ActorManager am = pd.GetGenericBinding(track.sourceObject) as ActorManager;
                am.LockUnlockActorController(true);
            }
        }
    }
    public override void OnGraphStop(Playable playable)
    {
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
            {
                ActorManager am = pd.GetGenericBinding(track.sourceObject) as ActorManager;
                am.LockUnlockActorController(false);
            }
        }       
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // Debug.Log("Behaviour Start");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        // Debug.Log("Behaviour Pause");
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        // Debug.Log("PrepareFrame");
    }
}
