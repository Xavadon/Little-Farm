using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerFarming : PlayerFarming
{
    private bool _allGet;
    private bool _allGrown;

    protected new void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Farm farm))
        {
            _allGet = farm.AllGet;
            _allGrown = farm.AllGrown;
        }
    }

    protected override IEnumerator InteractWithPlants()
    {
        if (_fruits.Count > 0 && _allGet || _allGrown)
        {
            if (_animatorHandler != null)
                _animatorHandler.PlayTargetAnimation("Attack", 0.10f, true);

            foreach (var item in _fruits)
            {
                item.SetPlant();
                item.GetPlant();
            }
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(InteractWithPlants());
    }

}
