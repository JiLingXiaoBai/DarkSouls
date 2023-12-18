using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : ActorManagerInterface
{
    public PlayableDirector pd;

    [Header("=== Timeline Assets ===")]
    public TimelineAsset frontStab;

    [Header("=== Assets Settings ===")]
    public ActorManager attacker;

    public ActorManager victim;

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
    }

    // Update is called once per frame
    // void Update()
    // {
    // if (Input.GetKeyDown(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Enemy"))
    // {
    //     pd.Play();
    // }
    // }

    public void PlayFrontStab(string timelineName, ActorManager attacker, ActorManager victim)
    {
        if (pd.playableAsset != null)
        {
            return;
        }
        
        if (timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Attacker Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        mybehav.myFloat = 777f;
                        pd.SetReferenceValue(myclip.am.exposedName, attacker);
                    }
                }
                else if (track.name == "Victim Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        mybehav.myFloat = 6f;
                        pd.SetReferenceValue(myclip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Attacker Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if (track.name == "Victim Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            pd.Play();
        }
    }
}