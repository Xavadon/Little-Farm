using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : Plant
{
    protected override void AddPlantToInventory(PlantsInventory plantsInventory)
    {
        plantsInventory.WheatCount = 1;
    }
}
