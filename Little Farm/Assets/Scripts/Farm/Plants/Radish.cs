using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radish : Plant
{
    protected override void AddPlantToInventory(PlantsInventory plantsInventory)
    {
        plantsInventory.RadishCount = 1;
    }
}
