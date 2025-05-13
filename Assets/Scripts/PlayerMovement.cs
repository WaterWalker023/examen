using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;
    
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;

    private CharacterController _playerController;
    private Vector3 _velocity;
    
    private bool _parseInput;

    public bool parseInput
    {
        get { return _parseInput; }
    }
    
    [SerializeField] private int speed;
    [SerializeField] private int gravity;
    [SerializeField] private int sprintMult;
    [SerializeField] private int jumpSpeed;
    [HideInInspector] public int currentJumpSpeed;
    
    [SerializeField] private float lookSens;

    [SerializeField] private Vector3 groundCheck;
    
    [SerializeField] private float groundCheckSize;
    private List<GameObject> _gameObjects;
    [SerializeField] private LayerMask groundLayerMask;
   

    private Vector3 move;

    private DoublePickup _bigObject;
    

    void Start()
    {
        _inputActionAsset = GetComponent<PlayerInput>().actions;
        _playerInputMap = _inputActionAsset.FindActionMap("PLayer");
        Cursor.lockState = CursorLockMode.Confined;
        _moveAction = _playerInputMap.FindAction("Move");
        _sprintAction = _playerInputMap.FindAction("Sprint");
        _lookAction = _playerInputMap.FindAction("Look");
        _jumpAction = _playerInputMap.FindAction("Jump");

        _playerController = GetComponent<CharacterController>();
        currentJumpSpeed = jumpSpeed;
    }
    
    void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        Vector2 lookValue = _lookAction.ReadValue<Vector2>() * lookSens;
        var totalSpeed = _sprintAction.IsPressed() ? speed + sprintMult : speed;
        bool jumpValue = _jumpAction.ReadValue<float>() == 1;
        
        move = transform.right * moveValue.x + transform.forward * moveValue.y;
        
        
        if (!_parseInput)
        {
            _playerController.Move( totalSpeed * Time.deltaTime * move);
        }
        else
        {
            _bigObject.GetComponent<TwoPlayerState>().waytogo(totalSpeed * move);
        }
        
        Vector3 rotationEulerAngles = transform.rotation.eulerAngles;
        rotationEulerAngles = new Vector3(0, rotationEulerAngles.y + lookValue.x, 0);
        
        transform.rotation = Quaternion.Euler(rotationEulerAngles);

        if (Isgrounded())
        {
            _velocity.y = -2;
        }
        
        if (jumpValue && Isgrounded())
        {
            _velocity.y = Mathf.Sqrt(currentJumpSpeed * -2f * gravity);
        }
        _velocity.y += gravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);
    }

    public void ResetJump()
    {
        currentJumpSpeed = jumpSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position - groundCheck, groundCheckSize);
        
    }
    public void StartParse(DoublePickup Object, bool parseInput = true)
    {
        _parseInput = parseInput;
        _bigObject = Object;

    }

    private bool Isgrounded()
    {
        Debug.Log("is ground");
        return Physics.CheckSphere(transform.position - groundCheck, groundCheckSize, groundLayerMask);
    }
}
