using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrainFactory : TrainFactory
{
    public override GameObject CreateTrain(GameObject trainPrefab)
    {
        return UnityEngine.Object.Instantiate(trainPrefab);
    }
}
