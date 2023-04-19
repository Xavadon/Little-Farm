using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Plant
{
    protected override void AddPlantToInventory(PlayerFarming playerFarming)
    {
        PlayerPlantsInventory.PotatoCount = 1;
    }
}
