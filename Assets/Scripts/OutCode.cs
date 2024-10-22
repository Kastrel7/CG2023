using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutCode
{
    public bool up, down, left, right;

    public OutCode(Vector2 point)
    {
        up = point.y > 1;
        down = point.y < -1;
        left = point.x < -1;
        right = point.x > 1;
    }

    public OutCode(bool upIn, bool downIn, bool leftIn, bool rightIn)
    {
        up = upIn;
        down = downIn;
        left = leftIn;
        right = rightIn;
    }

    public void DisplayOutCode()
    {
        string str = (up ? "1" : "0") + (down ? "1" : "0") + (left ? "1" : "0") + (right ? "1" : "0");
        Debug.Log(str);
    }

    public static OutCode operator +(OutCode a, OutCode b)
        => new OutCode(a.up || b.up, a.down || b.down, a.left || b.left, a.right || b.right);

    public static OutCode operator *(OutCode a, OutCode b)
        => new OutCode(a.up && b.up, a.down && b.down, a.left && b.left, a.right && b.right);

    public static bool operator ==(OutCode a, OutCode b)
    {
        return (a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == b.right);
    }

    public static bool operator !=(OutCode a, OutCode b)
    {
        return !(a == b);
    }

}
