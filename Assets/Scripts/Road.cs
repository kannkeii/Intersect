using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Road:MonoBehaviour
{
    public Quaternion rotation;

    //public Vector3 vector3;

    public enum DIR
    {
        DIR_IN,
        DIR_OUT
    }

    public DIR dir = DIR.DIR_IN;
}
