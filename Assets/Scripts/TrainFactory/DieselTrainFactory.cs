using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieselTrainFactory : TrainFactory
{
    public override Train CreateTrain()
    {
        return new ElectricTrain();
    }
}
