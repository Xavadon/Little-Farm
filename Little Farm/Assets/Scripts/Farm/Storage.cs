using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _wheatCount;
    private int _carrotCount;
    private int _radishCount;
    private int _potatoCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WorkerPlantsInventory workerPlantsInventory))
        {
            TryGetAllPlants(workerPlantsInventory);
        }

        if(other.TryGetComponent(out PlayerPlantsInventory playerPlantsInventory))
        {
            TryGivePlantToPlayer(playerPlantsInventory);
        }
    }

    private void TryGetAllPlants(WorkerPlantsInventory workerPlantsInventory)
    {
        if(workerPlantsInventory.WheatCount > 0)
        {
            _wheatCount += workerPlantsInventory.WheatCount;
            workerPlantsInventory.WheatCount = -workerPlantsInventory.WheatCount;
        }
       
        if(workerPlantsInventory.CarrotCount > 0)
        {
            _carrotCount += workerPlantsInventory.CarrotCount;
            workerPlantsInventory.CarrotCount = -workerPlantsInventory.CarrotCount;
        }
        
        if(workerPlantsInventory.RadishCount > 0)
        {
            _radishCount += workerPlantsInventory.RadishCount;
            workerPlantsInventory.RadishCount = -workerPlantsInventory.RadishCount;
        }
        
        if(workerPlantsInventory.PotatoCount > 0)
        {
            _potatoCount += workerPlantsInventory.PotatoCount;
            workerPlantsInventory.PotatoCount = -workerPlantsInventory.PotatoCount;
        }

    }

    private void TryGivePlantToPlayer(PlayerPlantsInventory playerPlantsInventory)
    {
        if (_wheatCount > 0) 
        {
            playerPlantsInventory.WheatCount = _wheatCount;
            _wheatCount = 0;
        }

        if (_carrotCount > 0) 
        {
            playerPlantsInventory.CarrotCount = _carrotCount;
            _carrotCount = 0;
        }

        if (_radishCount > 0) 
        {
            playerPlantsInventory.CarrotCount = _radishCount;
            _radishCount = 0;
        }

        if (_potatoCount > 0) 
        {
            playerPlantsInventory.PotatoCount = _potatoCount;
            _potatoCount = 0;
        }
    }
}
