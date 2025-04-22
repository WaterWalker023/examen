using UnityEngine;

public class TestOrderObject : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject target2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        if (!target) return;
        target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, speed*Time.deltaTime);
        
        if (!target2) return;
        target2.transform.position = Vector3.MoveTowards(target2.transform.position, transform.position, speed*Time.deltaTime);
    }
}
