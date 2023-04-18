using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmState : State
{
    [Header("Exit States")]
    [SerializeField] private State _exitState;

    [Header("Ñomponents")]
    [SerializeField] private AnimatorHandler _animatorHandler;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _mainTransform;

    [Space(height: 10)]
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _waitBeforeMove = 2f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private bool _targetFound = false;
    private bool _canMove = true;
    private int _currentWayPoint;
    private WaitForSeconds _waitSeconds;

    private void OnEnable()
    {
        _navMeshAgent.speed = _moveSpeed;
        _navMeshAgent.stoppingDistance = _stoppingDistance;
    }

    private void Start()
    {
        _waitSeconds = new WaitForSeconds(_waitBeforeMove);
    }

    private void Update()
    {
        MoveToWayPoint();
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
        if (_targetFound)
        {
            _targetFound = false;
            this.enabled = false;
            _exitState.enabled = true;
            return _exitState;
        }
        else 
            return this;
    }
}
