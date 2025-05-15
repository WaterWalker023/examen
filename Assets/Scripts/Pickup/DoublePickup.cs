using System;
using System.Collections.Generic;
using UnityEngine;

public class DoublePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    private bool _isHolding;
    private bool _isFirstPlayerHolding;
    private bool _isSecondPlayerHolding;
    
    private Rigidbody _objectRigidbody;
    private Vector3 currentpos;
    
    [SerializeField] private GameObject hold1;
    private GameObject _carryPoint1;
    
    [SerializeField] private GameObject hold2;
    private GameObject _carryPoint2;
    private Vector3 holdPos2;

    
    private PlayerMovement[] _playersArray;
    private List<Vector3> _vector3List = new List<Vector3>();
    
    private void Start()
    {
        _objectRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isHolding)
        {
            if (_isSecondPlayerHolding)
            {
                transform.LookAt(hold2.transform.position);
                transform.rotation *= Quaternion.Euler(0,90,0);
                
                hold1.transform.position = _carryPoint1.transform.position;
                hold2.transform.position = _carryPoint2.transform.position;
                
                var telpos = hold1.transform.position - ((hold1.transform.position - hold2.transform.position) / 2);
                
                transform.position = telpos;
                _vector3List.Clear();
            }
            else
            {
                transform.LookAt(hold2.transform.position);
                transform.rotation *= Quaternion.Euler(0,90,0);
                var telpos = _carryPoint1.transform.position + (transform.position - hold1.transform.position);
                
                transform.position = telpos;
                hold2.transform.position = holdPos2; 
            }
               
        }
    }

    /*
    public Vector3 GetCombined(Vector3 vector3s)
    {
        
        Vector3 comb = new Vector3(0, 0, 0);

        _vector3List.Add(vector3s);
        
        for (int i = 0; i < _vector3List.Count; i++)
        {
            comb += _vector3List[i];
        }
        
        return comb / _vector3List.Count * Time.deltaTime;
    }
    */
    public bool Interact(PlayerPickup interactor, GameObject player)
    {
        _objectRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        
        if (!_isFirstPlayerHolding)
        {
            holdPos2 = hold2.transform.position;
            _carryPoint1 = interactor.carryPoint;
            _isFirstPlayerHolding = true;
            
        }
        else
        {
            _carryPoint2 = interactor.carryPoint;
            _isSecondPlayerHolding = true;
            
            _playersArray = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            for (int i = 0; i < _playersArray.Length; i++)
            {
                _playersArray[i].StartParse(this);
            }
        }
        _isHolding = true;
        return true;
    }
    
    public bool PutDown(PlayerPickup interactor,GameObject player)
    {
        if (_isFirstPlayerHolding && !_isSecondPlayerHolding)
        {
            _isFirstPlayerHolding = false;
        }
        return true;
    }

}
