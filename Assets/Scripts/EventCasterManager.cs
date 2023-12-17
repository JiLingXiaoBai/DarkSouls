using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : ActorManagerInterface
{
    public string eventName;
    public bool active;
    
    // Start is called before the first frame update
    void Start()
    {
        if (am == null)
        {
            am = GetComponentInParent<ActorManager>();
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
