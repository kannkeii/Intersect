using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrainFactory
{
    public abstract GameObject CreateTrain(GameObject trainPrefab);
}
