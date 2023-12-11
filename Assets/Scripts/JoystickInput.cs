using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : UserInput
{
    #region 摇杆轴（Stick）
    
    /// <summary>
    /// Left Stick Horizontal.
    /// Axis: X Axis.
    /// </summary>
    public string LSH { get { return "LSH"; } }
    
    /// <summary>
    /// Left Stick Vertical.
    /// Axis: Y Axis.
    /// </summary>
    public string LSV { get { return "LSV"; } }
    
    /// <summary>
    /// Right Stick Horizontal.
    /// Axis: 4th Axis.
    /// </summary>
    public string RSH { get { return "RSH"; } }
    
    /// <summary>
    /// Right Stick Vertical.
    /// Axis: 5th Axis.
    /// </summary>
    public string RSV { get { return "RSV"; } }
    
    #endregion
    
    #region 十字方向键轴（Directional Pad，D-Pad）
    
    /// <summary>
    /// D-Pad Horizontal.
    /// Axis: 6th Axis.
    /// </summary>
    public string DPadH { get { return "DPadH"; } }
    
    /// <summary>
    /// D-Pad Vertical.
    /// Axis: 7th Axis.
    /// </summary>
    public string DPadV { get { return "DPadV"; } }
    
    #endregion
    
    #region 扳机轴（Trigger）
    
    /// <summary>
    /// Left Trigger.
    /// Axis: 9th Axis.
    /// </summary>
    public string LT { get { return "LT"; } }
    
    /// <summary>
    /// Right Trigger.
    /// Axis: 10th Axis.
    /// </summary>
    public string RT { get { return "RT"; } }
    
    /// <summary>
    /// Shared Trigger.
    /// Axis: 3rd Axis.
    /// </summary>
    public string Trigger { get { return "Trigger"; } }
    
    #endregion
    
    #region ABXY键
    
    /// <summary>
    /// A.
    /// Positive Button: joystick button 0.
    /// </summary>
    public string A { get { return "joystick button 0"; } }
    /// <summary>
    /// B.
    /// Positive Button: joystick button 1.
    /// </summary>
    public string B { get { return "joystick button 1"; } }
    /// <summary>
    /// X.
    /// Positive Button: joystick button 2.
    /// </summary>
    public string X { get { return "joystick button 2"; } }
    /// <summary>
    /// Y.
    /// Positive Button: joystick button 3.
    /// </summary>
    public string Y { get { return "joystick button 3"; } }
    
    #endregion
    
    #region 缓冲键（Bumper）
    
    /// <summary>
    /// Left Bumper.
    /// Positive Button: joystick button 4.
    /// </summary>
    public string LB { get { return "joystick button 4"; } }
    
    /// <summary>
    /// Right Bumper.
    /// Positive Button: joystick button 5.
    /// </summary>
    public string RB { get { return "joystick button 5"; } }
    
    #endregion
    
    #region View(Back)、Menu(Start)、XBox键
    
    /// <summary>
    /// View.
    /// Positive Button: joystick button 6.
    /// </summary>
    public string View { get { return "joystick button 6"; } }
    
    /// <summary>
    /// Munu.
    /// Positive Button: joystick button 7.
    /// </summary>
    public string Menu { get { return "joystick button 7"; } }
    
    // public string XBox { get { return string.None; } } // 没有对应
    
    #endregion
    
    #region 摇杆键
    
    /// <summary>
    /// Left Stick Button.
    /// Positive Button: joystick button 8.
    /// </summary>
    public string LS { get { return "joystick button 8"; } } // JoystickButton8
    
    /// <summary>
    /// Right Stick Button.
    /// Positive Button: joystick button 9.
    /// </summary>
    public string RS { get { return "joystick button 9"; } } // JoystickButton9
    
    #endregion
     
    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonX = new MyButton();
    public MyButton buttonY = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRT = new MyButton();
    public MyButton buttonRS = new MyButton();
    
    
    // Update is called once per frame
    void Update()
    {
        buttonA.Tick(Input.GetKey(A));
        buttonB.Tick(Input.GetKey(B));
        buttonX.Tick(Input.GetKey(X));
        buttonY.Tick(Input.GetKey(Y));
        buttonLB.Tick(Input.GetKey(LB));
        buttonRB.Tick(Input.GetKey(RB));
        buttonLT.Tick(Input.GetAxis(LT) != 0f);
        buttonRT.Tick(Input.GetAxis(RT) != 0f);
        buttonRS.Tick(Input.GetKey(RS));
        
        Jup = Input.GetAxis(RSV);
        Jright = Input.GetAxis(RSH);
                
        targetDup = Input.GetAxis(LSV);
        targetDright = Input.GetAxis(LSH);
        
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

        run = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
        jump = buttonA.OnPressed && buttonA.IsExtending;
        roll = buttonA.OnReleased && buttonA.IsDelaying;
        
        defense = buttonLB.IsPressing;
        rb = buttonRB.OnPressed;
        rt = buttonRT.OnPressed;
        lb = buttonLB.OnPressed;
        lt = buttonLT.OnPressed;
        lockOn = buttonRS.OnPressed;
    }
}
