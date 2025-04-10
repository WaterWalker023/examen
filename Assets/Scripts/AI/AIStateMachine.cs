using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SeePlayer()
    {
        transform.GetComponent<WalkingState>().enabled = false;
        transform.GetComponent<AggrowState>().enabled = true;
    }
}
