using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;
    private InputAction _moveAction;
    [SerializeField] private int speedMult;
   
    void Start()
    {
        _inputActionAsset = GetComponentInChildren<PlayerInput>().actions;
        
        _playerInputMap = _inputActionAsset.FindActionMap("PLayer");
        _moveAction = _playerInputMap.FindAction("Move");
        
        _moveAction.AddBinding("<Gamepad>/leftStick");
        _moveAction.Enable();
    }
    
    void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(moveValue.x, 0, moveValue.y) * (speedMult * Time.deltaTime);
    }
}
