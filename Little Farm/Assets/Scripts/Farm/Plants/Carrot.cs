using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant
{
    protected override void AddPlantToInventory(PlantsInventory plantsInventory)
    {
        plantsInventory.CarrotCount = 1;
    }
}
