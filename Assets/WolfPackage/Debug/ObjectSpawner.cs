using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objectsToSpawn;
    [SerializeField]
    float spawnInterval;
    [SerializeField]
    bool shouldSpawn;

    float spawnTimer;
    private void Start()
    {
        shouldSpawn = true;
        spawnTimer = spawnInterval;
        Spawn();
    }
    private void Update()
    {
        if (shouldSpawn)
        {
            if (spawnTimer <= 0)
            {
                Spawn();
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
    private void Spawn()
    {
        foreach (GameObject go in objectsToSpawn)
        {
            Instantiate(go, transform.position, Quaternion.identity);
            spawnTimer = spawnInterval;
        }
    }
}
