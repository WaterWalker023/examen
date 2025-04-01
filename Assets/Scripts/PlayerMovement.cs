using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;
    
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _lookAction;

    private CharacterController _playerController;
    private Vector3 _velocity;
    
    [SerializeField] private int speed;
    [SerializeField] private int gravity;
    [SerializeField] private int sprintMult;
   
    void Start()
    {
        _inputActionAsset = GetComponent<PlayerInput>().actions;
        _playerInputMap = _inputActionAsset.FindActionMap("PLayer");
        
        _moveAction = _playerInputMap.FindAction("Move");
        _sprintAction = _playerInputMap.FindAction("Sprint");
        _lookAction = _playerInputMap.FindAction("Look");

        _playerController = GetComponent<CharacterController>();
        
    }
    
    void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        Vector2 lookValue = _lookAction.ReadValue<Vector2>();

        var totalSpeed = _sprintAction.IsPressed() ? speed += sprintMult : speed;
        var move = transform.right * moveValue.x + transform.forward * moveValue.y;
        _playerController.Move( totalSpeed * Time.deltaTime * move);
        
        
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(0, rot.y+lookValue.x, 0);
        transform.rotation = Quaternion.Euler(rot);
        
        _velocity.y += gravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);
    }
}
