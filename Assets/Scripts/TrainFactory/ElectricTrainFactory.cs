using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrainFactory : TrainFactory
{
    public override Train CreateTrain()
    {
        return new ElectricTrain();
    }
}
