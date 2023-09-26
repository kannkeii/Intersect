using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieselTrainFactory : TrainFactory
{
    public override GameObject CreateTrain(GameObject trainPrefab)
    {
        return UnityEngine.Object.Instantiate(trainPrefab);
    }
}
