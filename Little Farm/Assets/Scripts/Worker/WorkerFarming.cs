using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerFarming : PlayerFarming
{
    public bool CanGet;

    private bool _allSet;
    private bool _allGet;
    private bool _allGrown;
    private bool _firstWave;

    protected new void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Farm farm))
        {
            _allSet = farm.AllSet;
            _allGet = farm.AllGet;
            _allGrown = farm.AllGrown;
            _firstWave = farm.FirstWave;
        }
    }

    protected new void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Plant fruit))
        {
            _fruits.Remove(fruit);
        }

        if (other.TryGetComponent(out Farm farm))
        {
            CanGet = false;
            _canSet = false;
            _canGet = false;
            if (_setPlantsButton.Length > 0) _setPlantsButton[_indexUI].onClick.RemoveListener(farm.SetPlants);
            if (_setPlantsButton.Length > 0) _getPlantsButton[_indexUI].onClick.RemoveListener(farm.GetPlants);
        }
    }

    protected override IEnumerator InteractWithPlants()
    {
        //Debug.Log(_fruits.Count > 0 && CanGet && (_allGet || _allGrown || _firstWave));

        if (_fruits.Count > 0 && CanGet && (_allGet || _allGrown || _firstWave))
        {
            if (_animatorHandler != null)
            {
                if(_firstWave && !_allGet && !_animatorHandler.animator.GetBool("IsAttacking"))
                    _animatorHandler.PlayTargetAnimation("Attack", 0.10f, true);

                if (_firstWave && !_allGrown && !_animatorHandler.animator.GetBool("IsSeeding"))
                    _animatorHandler.PlayTargetAnimation("Seed", 0.10f, false, true);
            }


            foreach (var item in _fruits)
            {
                item.SetPlant();
                item.GetPlant();
            }
        }
        else
        {
            if (_animatorHandler != null)
            {
                if (_allGet) _animatorHandler.animator.SetBool("IsAttacking", false);
                if (_allSet) _animatorHandler.animator.SetBool("IsSeeding", false);
            }
        }

        yield return new WaitForSeconds(0.35f);
        StartCoroutine(InteractWithPlants());
    }

}
