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
    }
    private void Update()
    {
        if (shouldSpawn)
        {
            if (spawnTimer <= 0)
            {
                foreach (GameObject go in objectsToSpawn)
                {
                    Instantiate(go, transform.position, Quaternion.identity);
                    spawnTimer = spawnInterval;
                }
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
}
