using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyUserInput : UserInput
{
    IEnumerator Start()
    {
        while (true)
        {
            rb = true;
            yield return 0;
        }
    }

    void Update()
    {
        UpdateDmagDvec(Dup, Dright);
    }
}
