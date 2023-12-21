using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;

    [Header("===== Auto Generate if Null =====")]
    public BattleManager bm;

    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;

    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        GameObject sensor = null;
        try
        {
            sensor = transform.Find("sensor").gameObject;
        }
        catch (Exception e)
        {
        }
        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);

        ac.OnAction += DoAction;
    }

    public void DoAction()
    {
        if (im.overlapEcastms.Count != 0)
        {
            if (im.overlapEcastms[0].active == true && !dm.IsPlaying())
            {
                if (im.overlapEcastms[0].eventName == "frontStab")
                {
                    dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
                }
                else if (im.overlapEcastms[0].eventName == "openBox")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 15))
                    {
                        im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].am
                            .transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].am.transform, Vector3.up);
                        dm.PlayFrontStab("openBox", this, im.overlapEcastms[0].am);
                    }
                }
                else if (im.overlapEcastms[0].eventName == "leverUp")
                {
                    Debug.Log("1123123");
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 15))
                    {
                        im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].am
                            .transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].am.transform, Vector3.up);
                        dm.PlayFrontStab("leverUp", this, im.overlapEcastms[0].am);
                    }
                }
            }
        }
    }

    private T Bind<T>(GameObject go) where T : ActorManagerInterface
    {
        T tempInst;
        if (go == null)
        {
            return null;
        }
        tempInst = go.GetComponent<T>();
        if (tempInst == null)
        {
            tempInst = go.AddComponent<T>();
        }
        tempInst.am = this;
        return tempInst;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned();
            }
        }
        else if (sm.isCounterBackFailure)
        {
            if (attackValid)
            {
                HitOrDie(false);
            }
        }
        else if (sm.isImmortal)
        {
            // Do nothing;
        }
        else if (sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(true);
            }
        }
    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void HitOrDie(bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {
            // Already dead.
        }
        else
        {
            sm.AddHp(-5f);
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                }
            }
            else
            {
                Die();
            }
        }
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if (ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
        }
        ac.camcon.enabled = false;
    }

    public void TestEcho()
    {
        print("Echo Echo");
    }

    public void LockUnlockActorController(bool value)
    {
        ac.SetBool("lock", value);
    }
}