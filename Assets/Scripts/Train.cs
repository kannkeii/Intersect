using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float speed;

    public Transform endRoadTransform;

    public enum DIR
    {
        DIR_IN,
        DIR_OUT
    }


    public DIR dir = DIR.DIR_IN;
}
