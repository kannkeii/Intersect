using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Road : MonoBehaviour
{
    public enum DIR
    {
        DIR_IN,
        DIR_OUT
    }

    public DIR dir = DIR.DIR_IN;
}
