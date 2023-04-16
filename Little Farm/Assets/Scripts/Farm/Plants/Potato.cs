using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Plant
{
    protected override void AddPlantToInventory()
    {
        PlayerPlantsInventory.PotatoCount = 1;
    }
}
