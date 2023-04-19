using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant
{
    protected override void AddPlantToInventory(PlayerFarming playerFarming)
    {
        PlayerPlantsInventory.CarrotCount = 1;
    }
}
