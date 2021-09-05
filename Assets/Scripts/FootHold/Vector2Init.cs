using System;
using UnityEngine;
using System.Collections;

public class Vector2Init
{

    private int xIP, yIP;

    public Vector2Init()
    {
        x = 0;
        y = 0;
    }

    public Vector2Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int x
    {
        get
        {
            return xIP;
        }
        set
        {
            xIP = value;
        }
    }

    public int y
    {
        get
        {
            return yIP;
        }
        set
        {
            yIP = value;
        }
    }
}