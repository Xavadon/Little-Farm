using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerFarming : MonoBehaviour
{
    [SerializeField] private AnimatorHandler _animatorHandler;

    private List<Plant> _fruits = new List<Plant>(0);

    private void Start()
    {
        StartCoroutine(InteractWithPlants());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Plant fruit))
        {
            _fruits.Add(fruit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Plant fruit))
        {
            _fruits.Remove(fruit);
        }
    }

    private IEnumerator InteractWithPlants()
    {
        if (_fruits.Count > 0)
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
