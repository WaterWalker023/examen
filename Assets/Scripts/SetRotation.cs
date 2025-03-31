using UnityEngine;

public class SetRotation : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.rotation = new Quaternion(0, cam.transform.rotation.y, 0, cam.transform.rotation.w);
    }
}
