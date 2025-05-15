using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ItemSpawn : MonoBehaviour
{

    [SerializeField] private Vector3 spawnArea;
    [SerializeField] private GameObject vegToSpawn;
    [SerializeField] private int amountToKeepOnField;
    private string _vegTag;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _vegTag = vegToSpawn.tag;
        for (int i = 0; i < amountToKeepOnField; i++)
        {
            var spawn = new Vector3(Random.Range(spawnArea.x, -spawnArea.x), Random.Range(spawnArea.y, transform.localPosition.y + 1), Random.Range(spawnArea.z, -spawnArea.z));
            Instantiate(vegToSpawn,transform.position + spawn,new Quaternion());
        }
    }

    // Update is called once per frame
    void Update()
    {
        var objectInPlay = GameObject.FindGameObjectsWithTag(_vegTag);

        if (objectInPlay.Length != amountToKeepOnField)
        {
            var spawn = new Vector3(Random.Range(spawnArea.x, -spawnArea.x), Random.Range(spawnArea.y, transform.localPosition.y + 1), Random.Range(spawnArea.z, -spawnArea.z));
            Instantiate(vegToSpawn,transform.position + spawn,new Quaternion());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position,spawnArea);
    }
}
