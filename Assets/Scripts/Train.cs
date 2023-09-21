using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float speed;

    protected float defaultSpeed;

    public Transform endRoadTransform;

    public Road currentTrack;  // åªç›ÇÃìSìπ

    const float angleThreshold = 90f;

    public enum ACTION_MODE
    {
        ACTION_MODE_WAIT_AT_ROAD,
        ACTION_MODE_WAIT_AT_ROAD_END,
        ACTION_MODE_WAIT_AT_CENTER,
        ACTION_MODE_RUNING,
        ACTION_MODE_RUNING2,
    }

    public enum DIR
    {
        DIR_IN,
        DIR_OUT
    }


    public DIR dir = DIR.DIR_IN;
    public ACTION_MODE actionMode = ACTION_MODE.ACTION_MODE_RUNING;
}
