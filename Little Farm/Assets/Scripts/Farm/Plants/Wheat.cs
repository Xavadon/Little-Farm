using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : Plant
{
    protected override void AddPlantToInventory()
    {
        PlayerPlantsInventory.WheatCount = 1;
    }
}
