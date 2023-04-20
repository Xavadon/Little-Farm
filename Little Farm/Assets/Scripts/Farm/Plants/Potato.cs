using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Plant
{
    protected override void AddPlantToInventory(PlantsInventory plantsInventory)
    {
        plantsInventory.PotatoCount = 1;
    }
}
