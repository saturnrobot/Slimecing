using UnityEngine;

[System.Serializable]
public class PlayerInputs
{
    public bool mouseInput;
    public string xMoveAxis;
    public string yMoveAxis;
    public string xSwordAxis;
    public string ySwordAxis;
    public string thrustInput;
    public string dashInput;
    public string emoteOne;

    public bool MouseInput { get { return mouseInput; } set { mouseInput = value; } }
    public string XMoveAxis { get { return xMoveAxis; } set { xMoveAxis = value; } }
    public string YMoveAxis { get { return yMoveAxis; } set { yMoveAxis = value; } }
    public string XSwordAxis { get { return xSwordAxis; } set { xSwordAxis = value; } }
    public string YSwordAxis { get { return ySwordAxis; } set { ySwordAxis = value; } }
    public string ThrustInput { get { return thrustInput; } set { thrustInput = value; } }
    public string DashInput { get { return dashInput; } set { dashInput = value; } }
    public string EmoteOne { get { return emoteOne; } set { emoteOne = value; } }

    /*public bool MouseInput { get; set; }
    public string XMoveAxis { get; set; }
    public string YMoveAxis { get; set; }
    public string XSwordAxis { get; set; }
    public string YSwordAxis { get; set; }
    public string ThrustInput { get; set; }
    public string DashInput { get; set; }*/

    /*public PlayerInputs(bool mouse, string x, string y, string xs, string ys, string t, string d)
    {
        mouseInput = mouse;
        xMoveAxis = x;
        yMoveAxis = y;
        xSwordAxis = xs;
        ySwordAxis = ys;
        thrustInput = t;
        dashInput = d;
    }*/
}
