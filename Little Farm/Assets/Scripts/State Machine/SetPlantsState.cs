using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetPlantsState : State
{
    [Header("Exit States")]
    [SerializeField] private State _exitState;

    [Header("Ñomponents")]
    [SerializeField] private WorkerFarming _workerFarming;
    [SerializeField] private Farm _farm;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private WorkerPlantsInventory _workerPlantsInventory;
    [SerializeField] private AnimatorHandler _animatorHandler;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _mainTransform;

    [Space(height: 10)]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _waitBeforeMove = 2f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private bool _nextState;
    private bool _farmEnabled;
    private bool _canMove = true;
    private int _currentWayPoint;
    private WaitForSeconds _waitSeconds;

    private void OnEnable()
    {
        _navMeshAgent.speed = _moveSpeed;
        _navMeshAgent.stoppingDistance = _stoppingDistance;
        _animatorHandler.animator.SetBool("IsMoving", true);
    }

    private void Start()
    {
        _waitSeconds = new WaitForSeconds(_waitBeforeMove);
    }

    private void Update()
    {
        if (!_farm.gameObject.activeSelf && !_farmEnabled)
        {
            _nextState = true;
            _canMove = false;
        }
        else if (!_farmEnabled) 
        {
            _farmEnabled = true;
            _canMove = true;
        }

        _animatorHandler.animator.SetBool("IsMoving", _canMove);

        if ((_farm.AllGet && !_farm.FirstWave) || _farm.AllSet)
            _nextState = true;
        else
            MoveToWayPoint();

        if(_workerPlantsInventory.WheatCount >= _workerPlantsInventory.MaxCount ||
            _workerPlantsInventory.CarrotCount >= _workerPlantsInventory.MaxCount ||
            _workerPlantsInventory.RadishCount >= _workerPlantsInventory.MaxCount ||
            _workerPlantsInventory.PotatoCount >= _workerPlantsInventory.MaxCount)
        {
            //_nextState = true;
            //RunCurrentState();
            //ñòåéò íà âûõîä
        }
    }

    private void MoveToWayPoint()
    {
        if (_wayPoints.Length == 0) return;
        else if (_wayPoints[0] == null) return;

        if (_currentWayPoint >= _wayPoints.Length)
            _currentWayPoint = 0;
        


        if (_canMove)
            _navMeshAgent.SetDestination(_wayPoints[_currentWayPoint].transform.position);
            
        if (Vector3.Distance(_mainTransform.position, _wayPoints[_currentWayPoint].transform.position) <= _navMeshAgent.stoppingDistance)
        {
            _workerFarming.CanGet = true;
            _workerFarming.enabled = true;
            _currentWayPoint++;
            StartCoroutine(WaitBeforeMove());
        }
    }

    private IEnumerator WaitBeforeMove()
    {
        _canMove = false;
        _animatorHandler.animator.SetBool("IsMoving", _canMove);

        yield return _waitSeconds;

        _canMove = true;
        _animatorHandler.animator.SetBool("IsMoving", _canMove);
    }

    public override State RunCurrentState()
    {
        if (_nextState)
        {
            this.enabled = false;
            _workerFarming.CanGet = false;
            _workerFarming.enabled = false;
            _nextState = false;
            _exitState.enabled = true;
            return _exitState;
        }
        else 
            return this;
    }
}
