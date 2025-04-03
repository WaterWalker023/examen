using UnityEngine;

public class Farmland : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerStanding()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("test");
        }
    }
}
