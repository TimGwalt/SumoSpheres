using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IntVector2
{
public int x, z;

    //constuctor for a 2D vector space
    public IntVector2 (int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    //define "+" operator for addition of 2 intVectors
    public static IntVector2 operator + (IntVector2 a, IntVector2 b)
    {
        a.x += b.x;
        a.z += b.z;
        return a;
    }

}
