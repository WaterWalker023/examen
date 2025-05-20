using UnityEngine;

public class TestOrderObject : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject target2;
    [SerializeField] private GameObject target3;
    [SerializeField] private GameObject target4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        if (target != null) target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, speed*Time.deltaTime);
        
        if (target2 != null) target2.transform.position = Vector3.MoveTowards(target2.transform.position, transform.position, speed*Time.deltaTime);
        
        if (target3 != null) target3.transform.position = Vector3.MoveTowards(target3.transform.position, transform.position, speed*Time.deltaTime);
        
        if (target4 != null) target4.transform.position = Vector3.MoveTowards(target4.transform.position, transform.position, speed*Time.deltaTime);
    }
}
