using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed = 250f; //5
    [SerializeField] private float _rotationSpeed = 700f;
    [SerializeField] private float _checkGroundDistance;
    [SerializeField] private float _checkLadderDistance;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _ladderLayerMask;

    private Rigidbody _rigidbody;
    private PlayerActions _playerActions;
    private bool _isMove = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerActions = new PlayerActions();
        _playerActions.PlayerControls.Movement.performed += OnMove;
        _playerActions.PlayerControls.Movement.canceled += OnMoveStopped;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        YG.YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        YG.YandexGame.GetDataEvent -= GetLoad;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!_isMove)
        {
            if (_animator != null) _animator.SetBool("IsMoving", true);
            _isMove = true;
        }
    }

    private void OnMoveStopped(InputAction.CallbackContext context)
    {
        if (_isMove)
        {
            if (_animator != null) _animator.SetBool("IsMoving", false);
            _isMove = false;

            YG.YandexGame.savesData.playerPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void GetLoad()
    {
        var vector = new Vector3(YG.YandexGame.savesData.playerPosition.x, 3, YG.YandexGame.savesData.playerPosition.z);
        transform.position = vector;
    }

    private void Movement()
    {
        var inputPosition = _playerActions.PlayerControls.Movement.ReadValue<Vector2>();

        var movementDirection = Vector3.zero;

        if (CheckGround())
        {
            movementDirection = new Vector3(inputPosition.x, 0, inputPosition.y) * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.velocity = movementDirection;
        }

        if (CheckLadder())
        {
            movementDirection = new Vector3(inputPosition.x, inputPosition.y, inputPosition.y) * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.velocity = movementDirection;
        }

        if (movementDirection != Vector3.zero && !_animator.GetBool("IsAttacking"))
        {
            movementDirection.y = 0;
            var rotationDirection = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDirection, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private bool CheckGround()
    {
        return (Physics.CheckSphere(transform.position + Vector3.down * _checkGroundDistance, .3f, _groundLayerMask));
    }

    private bool CheckLadder()
    {
        return Physics.CheckSphere(transform.position + transform.forward * _checkLadderDistance + Vector3.up * _checkLadderDistance, .3f, _ladderLayerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * _checkGroundDistance, .3f);
        Gizmos.DrawWireSphere(transform.position + transform.forward * _checkLadderDistance + Vector3.up * _checkLadderDistance , .3f);
    }
}
