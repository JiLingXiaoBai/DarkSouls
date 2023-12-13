using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput : MonoBehaviour
{
    [Header("===== Output Signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;
 
    public bool run;
    public bool defense;
    public bool jump;
    public bool roll;
    public bool lockOn;
    public bool lb;
    public bool lt;
    public bool rb; 
    public bool rt; 
     
    [Header("===== Others =====")]
    public bool inputEnabled = true;
 
    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;
    
    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x / 2.0f);
        return output;
    }

    protected void UpdateDmagDvec(float _Dup, float _Dright)
    {
        Dmag = Mathf.Sqrt(_Dup * _Dup + _Dright * _Dright);
        Dvec = _Dright * transform.right + _Dup * transform.forward;
    }
}
