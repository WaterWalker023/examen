using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ArrowGuide : MonoBehaviour
{
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private Transform arrowGuideHome;
    
    [SerializeField] private List<GameObject> arrowGuideTargets;
    
    [SerializeField] private List<string> arrowGuideTagNames;

    [SerializeField] private GameObject player;
    
    private GameObject currentClosestObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrowGuideHome = GameObject.Find("Huisje").transform;
    }

    // Update is called once per frame
    void Update()
    {
        arrowGuideTargets.Clear();
        for (int i = 0; i < arrowGuideTagNames.Count; i++)
        {
            if (GameObject.FindGameObjectsWithTag(arrowGuideTagNames[i]).Length == 0) continue;
            
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(arrowGuideTagNames[i]))
            {
                arrowGuideTargets.Add(g);
            }
        }
        
        if (arrowGuideTargets.Count == 0) return;
        
        var smallestNumber = Mathf.Infinity;
        for (int i = 0; i < arrowGuideTargets.Count; i++)
        {
            var test = Vector3.Distance(transform.position, arrowGuideTargets[i].transform.position);
            if (test < smallestNumber)
            {
                smallestNumber = test;
                currentClosestObject = arrowGuideTargets[i];
            }
        }
        
        arrowPoint.transform.LookAt(currentClosestObject.transform.position, Vector3.forward);
        arrowPoint.transform.Rotate(new Vector3(0, -90, 90));

        if (!player.GetComponent<PlayerPickup>().havePickedUpIngredient) return;
        arrowPoint.transform.LookAt(arrowGuideHome.transform, Vector3.forward);
        arrowPoint.transform.Rotate(new Vector3(0, -90, 90));
        
        if (player.GetComponent<PlayerPickup>().stillHoldingObject) return;
        arrowPoint.transform.LookAt(currentClosestObject.transform.position, Vector3.forward);
        arrowPoint.transform.Rotate(new Vector3(0, -90, 90));
    }
}
