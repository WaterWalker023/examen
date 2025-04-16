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
    
    public bool parseInput;
    
    [SerializeField] private int speed;
    [SerializeField] private int gravity;
    [SerializeField] private int sprintMult;
    [SerializeField] private int jumpSpeed;

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
        
    }
    
    void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        Vector2 lookValue = _lookAction.ReadValue<Vector2>() * lookSens;

        var totalSpeed = _sprintAction.IsPressed() ? speed + sprintMult : speed;
        move = transform.right * moveValue.x + transform.forward * moveValue.y;
        if (!parseInput)
        {
            _playerController.Move( totalSpeed * Time.deltaTime * move);
        }
        else
        {
            _playerController.Move(_bigObject.GetCombined(move)/2*Time.deltaTime);
        }
        
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(0, rot.y+lookValue.x, 0);
        transform.rotation = Quaternion.Euler(rot);

        if (Physics.CheckSphere(transform.position - groundCheck, groundCheckSize, groundLayerMask))
        {
            _velocity.y = gravity;
        }
        
        float jumpValue = _jumpAction.ReadValue<float>();
        if (jumpValue == 1 && Physics.CheckSphere(transform.position - groundCheck, groundCheckSize, groundLayerMask))
        {
            _velocity.y = Mathf.Sqrt(jumpValue * jumpSpeed * -2f * gravity);
        }
        _velocity.y += gravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position - groundCheck, groundCheckSize);
        
    }
    public void StartParse(DoublePickup Object)
    {
        parseInput = true;
        _bigObject = Object;

    }
}
