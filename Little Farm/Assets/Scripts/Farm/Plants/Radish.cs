using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radish : Plant
{
    protected override void AddPlantToInventory()
    {
        PlayerPlantsInventory.RadishCount = 1;
    }
}
