using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;

    [Header("===== Auto Generate if Null =====")]
    public BattleManager bm;

    public WeaponManager wm;
    public StateManager sm;

    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
    }

    private T Bind<T>(GameObject go) where T : ActorManagerInterface
    {
        T tempInst;
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

    public void DoDamage()
    {
        //ac.IssueTrigger("hit");
        ac.IssueTrigger("die");
    }
}