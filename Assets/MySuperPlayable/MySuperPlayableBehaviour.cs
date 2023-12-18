using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public float myFloat;

    private PlayableDirector pd;

    public override void OnPlayableCreate(Playable playable)
    {
    }

    public override void OnGraphStart(Playable playable)
    {
        pd = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void OnGraphStop(Playable playable)
    {
        if (pd != null)
        {
            pd.playableAsset = null;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockUnlockActorController(true);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        am.LockUnlockActorController(true);
    }
}