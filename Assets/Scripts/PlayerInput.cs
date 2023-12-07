using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Variable
    [Header("===== Key Settings =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    
    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    [Header("===== Output Signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public bool run;
    public bool jump;
    private bool lastJump;

    [Header("===== Others =====")]
    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0f) - (Input.GetKey(keyDown) ? 1.0f : 0f);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0f) - (Input.GetKey(keyLeft) ? 1.0f : 0f);

        if (inputEnabled == false)
        {
            targetDup = 0f;
            targetDright = 0f;
        }
        
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        var Dright2 = tempDAxis.x;
        var Dup2 = tempDAxis.y;
        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x / 2.0f);
        return output;
    }
}
